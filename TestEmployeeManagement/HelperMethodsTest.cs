using Xunit;
using EmployeeManagement.Utilities;

namespace TestEmployeeManagement
{
    public class HelperMethodsTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("~/Organization.xm")]
        public void ValidateInvalidXmlFile(string input)
        {
            var (actualName, actualNodes) = HelperMethods.ReadXml(input);

            Assert.Null(actualNodes);
            Assert.Equal("", actualName);
        }

        [Fact]
        public void ValidateValidXmlFile()
        {
            var input = "../../../../../Organization.xml";
            var (actualName, actualNodes) = HelperMethods.ReadXml(input);

            Assert.NotNull(actualNodes);

            Assert.Equal("ABC Inc.", actualName);
        }

        [Fact]
        public void ValidateEmptyInput()
        {
            var input = "";
            var actual = HelperMethods.ValidateInput(input);

            Assert.False(actual);
        }

        [Fact]
        public void ValidateValidInput()
        {
            var input = "~/Organization.xml";
            var actual = HelperMethods.ValidateInput(input);

            Assert.True(actual);
        }
    }
}
