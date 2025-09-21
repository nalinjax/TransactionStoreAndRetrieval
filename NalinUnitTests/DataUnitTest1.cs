// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinTransactionCurrencyAPI;
using System.Globalization;

namespace NalinUnitTests
{
    /// <summary>
    /// Basic unit tests
    /// </summary>
    [TestClass]
    public class DataUnitTest1
    {
        [TestMethod]
        public void CurrencyOperationsProvidesKnownCurrency()
        {
            // arrange
            var currencyDataProvider = new TestingCurrencyDataProvider();
            var currencyOperations = new CurrencyOperations(currencyDataProvider); // we are testing this

            // act
            var supportedCurrencies = currencyOperations.GetAllSupportedCurrencies().Result;

            // Assert
            Assert.IsNotNull(supportedCurrencies);
            Assert.IsNotNull(supportedCurrencies.FirstOrDefault(r => r.country_currency_desc == "Australia-Dollar"));
        }

        [TestMethod]
        public void CurrencyOperationsFailsForUnknownCurrency()
        {
            // arrange
            var currencyDataProvider = new TestingCurrencyDataProvider();
            var currencyOperations = new CurrencyOperations(currencyDataProvider); // we are testing this

            // act
            var supportedCurrencies = currencyOperations.GetAllSupportedCurrencies().Result;

            // Assert
            Assert.IsNull(supportedCurrencies.FirstOrDefault(r => r.country_currency_desc == "Imaginary-Currency"));
        }

        [TestMethod]
        public void CurrencyOperationsProvidesRateWhenDataExists()
        {
            var currencyDataProvider = new TestingCurrencyDataProvider();
            var currencyOperations = new CurrencyOperations(currencyDataProvider); // we are testing this

            // act
            var transactionDate = DateTime.ParseExact("2025-09-21", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            var currencyRate = currencyOperations.GetCurrencyRatesForDate("Canada-Dollar", transactionDate, 6).Result;

            // Assert
            Assert.AreEqual("0.97", currencyRate);
        }


        [TestMethod]
        public void CurrencyOperationsProvidesRateWhenNoDataExists()
        {
            var currencyDataProvider = new TestingCurrencyDataProvider();
            var currencyOperations = new CurrencyOperations(currencyDataProvider); // we are testing this

            // act
            var transactionDate = DateTime.ParseExact("2001-09-21", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            var currencyRate = currencyOperations.GetCurrencyRatesForDate("Canada-Dollar", transactionDate, 6).Result;

            // Assert
            Assert.IsNull(currencyRate);
        }
    }
}