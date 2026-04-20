using SimpleTrader.Domain.Services;
using Moq;
using System.Collections.Generic;
using System.Text;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class BuyStockServiceTest
    {
        private BuyStockServiceTest _buyStockService;

        [SetUp]
        public void Setup()
        {
            // Configurar dependencias simuladas (mocks) para IStockPriceService y IDataService<Account>
            var stockPriceServiceMock = new Mock<IStockPriceService>();
            var accountServiceMock = new Mock<IDataService<Account>>();
            
            //// Configurar el comportamiento simulado de los servicios
            //stockPriceServiceMock.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(100.0); // Precio simulado de la acción
            //accountServiceMock.Setup(a => a.Update(It.IsAny<Guid>(), It.IsAny<Account>())).Returns(Task.CompletedTask);
            //// Crear una instancia del servicio BuyStockService con las dependencias simuladas
            //_buyStockService = new BuyStockService(stockPriceServiceMock.Object, accountServiceMock.Object);
        }

        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {

        }

        [Test]
        public void BuyStock_WithInsufficientFunds_ThrowsInsufficientFundsException()
        {

        }

        [Test]
        public void BuyStock_WithAccountUpdateFaikure_ThrowsException()
        {

        }

        [Test]
        public void BuyStock_WithSuccesfulPurchase_ReturnsAccountWithNewTransaction()
        {

        }
    }
}
