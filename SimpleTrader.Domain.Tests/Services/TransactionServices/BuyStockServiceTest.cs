using SimpleTrader.Domain.Services;
using Moq;
using System.Collections.Generic;
using System.Text;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class BuyStockServiceTest
    {
        private BuyStockService _buyStockService;
        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void Setup()
        {
            // Configurar dependencias simuladas (mocks) para IStockPriceService y IDataService<Account>
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _buyStockService = new BuyStockService(_mockStockPriceService.Object, _mockAccountService.Object);

        }

        private static Account CreateAccount(string symbol, int shares)
        {
            return new Account()
            {
                AssetTransactions = new List<AssetTransaction>
                {
                    //new AssetTransaction
                    //{
                    //    Asset = new Asset { Symbol = symbol, PricePerShare = 150 },
                    //    Shares = shares,
                    //    IsPurchase = true,
                    //    DateProcessed = DateTime.Now
                    //}
                }
            };
        }

        [Test]
        public void BuyStock_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            // Arrange
            string symbol = "INVÁLIDO";
            Account buyerAccount = new Account();

            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ThrowsAsync(new InvalidSymbolException(symbol, "Failed to get price"));

            //Act & Assert

            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(async () => await _buyStockService.BuyStock(buyerAccount, symbol, 10));

            Assert.AreEqual(symbol, exception.Symbol);

        }


        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {
            // Arrange
            string symbol = "AAPL";
            Account buyer = CreateAccount(symbol, 10);

            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ThrowsAsync(new Exception("Failed to get price"));

            //Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _buyStockService.BuyStock(buyer, symbol, 10));
        }

        [Test]
        public void BuyStock_WithInsufficientFunds_ThrowsInsufficientFundsException()
        {
            //Arrange
            string symbol = "AAPL";
            double saldoActual = 1000;
            double precioPorAccion = 150;
            int accionesParaComprar = 20;

            Account buyer = new Account { Balance = saldoActual }; // Set a low balance to trigger insufficient funds
            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ReturnsAsync(precioPorAccion); // Price per share is 150

            //Act & Assert
            InsufficientFundsException exception = Assert.ThrowsAsync<InsufficientFundsException>(async () => await _buyStockService.BuyStock(buyer, symbol, accionesParaComprar));

            Assert.AreEqual(saldoActual, exception.AccountBalance);
            Assert.AreEqual(precioPorAccion * accionesParaComprar, exception.RequiredBalance);


        }

        [Test]
        public void BuyStock_WithAccountUpdateFailure_ThrowsException()
        {
            //Arrange
            string symbol = It.IsAny<string>();
            Account buyer = new Account { Balance = 500 };
            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ReturnsAsync(150); // Price per share is 150
            _mockAccountService.Setup(s => s.Update(buyer.Id, buyer)).ThrowsAsync(new Exception("Failed to update account"));

            //Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _buyStockService.BuyStock(buyer, symbol, 1));
        }

        [Test]
        public async Task BuyStock_WithSuccesfulPurchase_ReturnsAccountWithNewTransaction()
        {
            //Arrange
            string symbol = "AAPL";
            Account initialBuyer = new Account { Balance = 500 };
            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ReturnsAsync(150); // Price per share is 150
            _mockAccountService.Setup(s => s.Update(initialBuyer.Id, initialBuyer)).ReturnsAsync(initialBuyer);

            //Act 
            var updatedBuyer = await _buyStockService.BuyStock(initialBuyer, symbol, 1);

            //Assert
            Assert.IsNotNull(updatedBuyer);
            Assert.IsTrue(updatedBuyer.Balance == 500 - 150); // Balance should be reduced by the cost of the purchase
            Assert.IsTrue(updatedBuyer.AssetTransactions.Count == 1); // There should be one transaction recorded
            Assert.IsTrue(updatedBuyer.AssetTransactions.First().IsPurchase);
        }


        [Test]
        public async Task BuyStock_WithSuccesfulPurchase_UpdateIsCalledExactlyOnce()
        {
            //Arrange 
            string symbol = "COORECT_SYMBOL";
            Account initialBuyer = new Account { Balance = 500 };

            _mockStockPriceService.Setup(s => s.GetPrice(symbol)).ReturnsAsync(150); // Price per share is 150
            _mockAccountService.Setup(s => s.Update(initialBuyer.Id, initialBuyer)).ReturnsAsync(initialBuyer);

            //Act
            var updatedBuyer = await _buyStockService.BuyStock(initialBuyer, symbol, 1);

            //Assert
            _mockAccountService.Verify(s => s.Update(initialBuyer.Id, initialBuyer), Times.Once()); // Verificar que Update se llamó exactamente una vez con el Account actualizado correctamente
        }
    }
}