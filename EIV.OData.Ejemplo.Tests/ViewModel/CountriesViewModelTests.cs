
namespace EIV.OData.Ejemplo.Tests.ViewModel
{
    using Ejemplo.ViewModel;
    using Xunit;

    public class CountriesViewModelTests
    {
        private readonly CountriesViewModel countriesViewModel = null;

        public CountriesViewModelTests()
        {
            this.countriesViewModel = new CountriesViewModel();
        }

        [Fact]
        public void PassingTest()
        {
            Assert.NotNull(this.countriesViewModel);
        }
    }
}