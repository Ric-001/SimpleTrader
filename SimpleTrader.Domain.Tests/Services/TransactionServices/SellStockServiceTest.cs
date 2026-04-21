using SimpleTrader.Domain.Services;
using Moq;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class SellStockServiceTest
    {
        private SellStockService _sellStockService;

        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void Setup()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();
            _sellStockService = new SellStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void SellStock_WithInsufficentShares_ThrowsInsufficientSharesException()
        {
            string symbol = "T";
            Account seller = CreateAccount(symbol, 10);

            Assert.ThrowsAsync<InsufficientSharesException>(async () =>
            {
                await _sellStockService.SellStock(seller, symbol, 20);
            });
        }

        private static Account CreateAccount(string symbol, int shares)
        {
            return new Account()
            {
                AssetTransactions = new List<AssetTransaction>
                {
                    new AssetTransaction
                    {
                        Asset = new Asset { Symbol = symbol, PricePerShare = 150 },
                        Shares = shares,
                        IsPurchase = true,
                        DateProcessed = DateTime.Now
                    }
                }
            };
        }

        [Test]
        public void SellStock_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            //Arrange
            string invalidSymbol = "INVALID";
            Account seller = CreateAccount(invalidSymbol, 10);

            // Le decimos al mock: cuando te pidan el precio de "INVALID", lanza la excepción
            _mockStockPriceService
                .Setup(s => s.GetPrice(invalidSymbol))
                .ThrowsAsync(new InvalidSymbolException(invalidSymbol, "Invalid symbol"));

            
            //ACT + ASSERT (juntos porque esperamos una excepción)

            Assert.ThrowsAsync<InvalidSymbolException>(async () =>
            {
               await _sellStockService.SellStock(seller, invalidSymbol, 5);
            });
        }

        [Test]
        public void SellStock_WithGetPriceFailure_ThrowsException()
        {
            //Arrange
            string symbol = "T";
            Account seller = CreateAccount(symbol, 10);

            // Le decimos al mock: cuando te pidan el precio de "T", lanza la excepción
            _mockStockPriceService
                .Setup(s => s.GetPrice(symbol))
                .ThrowsAsync(new Exception("Failed to get price"));

            //ACT + ASSERT (juntos porque esperamos una excepción)
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _sellStockService.SellStock(seller, symbol, 5);
            });
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewTransaction()
        {
            //Arrange
            string symbol = "T";
            Account sellerAccount = CreateAccount(symbol, 10);

            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ReturnsAsync(150);
            _mockAccountService.Setup(s => s.Update(sellerAccount.Id, sellerAccount)).ReturnsAsync(sellerAccount);

            //Act
            Account result = await _sellStockService.SellStock(sellerAccount, symbol, 5);

            Assert.AreEqual(750, result.Balance); // 5 shares * $150 each
            Assert.AreEqual(2, result.AssetTransactions.Count); // One purchase and one sale
            Assert.IsFalse(result.AssetTransactions.Last().IsPurchase); // Last transaction should be a sale

        }
    }
}
