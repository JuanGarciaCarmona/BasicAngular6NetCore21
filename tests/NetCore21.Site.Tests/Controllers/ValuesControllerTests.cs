using NetCore21.Site.Controllers;
using System.Collections.Generic;
using Xunit;

namespace NetCore21.Tests.Controllers
{   
    public class ValuesControllerTests
    {

        private ValuesController _objectToTest;

        [Fact]
        public void WhenGettingValuesItShouldReturnAListWithValues()
        {
            // Arrange:
            _objectToTest = new ValuesController();

            // Act:
           var result = _objectToTest.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<string>>(result.Value);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public void WhenGettingAConcreteValueItShouldReturnAString()
        {
            // Arrange:
            _objectToTest = new ValuesController();

            // Act:
            var result = _objectToTest.Get(123);

            // Assert
            Assert.IsAssignableFrom<string>(result.Value);
            Assert.NotEmpty(result.Value);
        }
    }
}
