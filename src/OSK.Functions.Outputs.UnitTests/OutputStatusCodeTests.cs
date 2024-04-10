using OSK.Functions.Outputs.Abstractions;
using System.Net;
using Xunit;

namespace OSK.Functions.Outputs.UnitTests
{
    public class OutputStatusCodeTests
    {
        #region ToString

        [Fact]
        public void ToString_NoOrigination_ConvertsToExpectedStatusString()
        {
            // Arrange
            var outputStatus = new OutputStatusCode(HttpStatusCode.Accepted, DetailCode.DownStreamError);

            // Act
            var str = outputStatus.ToString();

            // Assert
            Assert.Equal("202.1", str);
        }

        [Fact]
        public void StatusCodeString_IncludesOrigination_ShouldNotIncludeOrigination_ConvertsToExpectedStatusCode()
        {
            // Arrange
            var outputStatus = new OutputStatusCode(HttpStatusCode.OK, DetailCode.None, "Test Library");

            // Act
            var str = outputStatus.ToString(false);

            // Assert
            Assert.Equal("200.0", str);
        }

        [Fact]
        public void StatusCodeString_IncludesOrigination_ShouldIncludeOrigination_ConvertsToExpectedStatusCode()
        {
            // Arrange
            var outputStatus = new OutputStatusCode(HttpStatusCode.BadGateway, DetailCode.DownStreamError, "Test Library");

            // Act
            var str = outputStatus.ToString(true);

            // Assert
            Assert.Equal("502.1,Test Library", str);
        }

        #endregion

        #region Constructor(string)

        [Theory]
        [InlineData(HttpStatusCode.OK, DetailCode.None)]
        public void OutputStatusCode_Constructor_String_StatusStringNoOrigination_ReturnsExpectedOutputStatus(HttpStatusCode statusCode,
            DetailCode detailCode)
        {
            // Arrange
            var outputStatus = new OutputStatusCode(statusCode, detailCode);
            var str = outputStatus.ToString();

            // Act
            var newStatus = OutputStatusCode.Parse(str);

            // Assert
            Assert.Equal(statusCode, newStatus.StatusCode);
            Assert.Equal(detailCode, newStatus.DetailCode);
            // No origination source was included in the ToString, it should be the default
            Assert.Equal(OutputStatusCode.DefaultSource, newStatus.OriginationSource);
        }

        [Theory]
        [InlineData(HttpStatusCode.OK, DetailCode.None)]
        public void OutputStatusCode_Constructor_String_StatusStringHasOrigination_ReturnsExpectedOutputStatus(HttpStatusCode statusCode,
            DetailCode detailCode)
        {
            // Arrange
            var outputStatus = new OutputStatusCode(statusCode, detailCode, "Hello World");
            var str = outputStatus.ToString(true);

            // Act
            var newStatus = OutputStatusCode.Parse(str);

            // Assert
            Assert.Equal(statusCode, newStatus.StatusCode);
            Assert.Equal(detailCode, newStatus.DetailCode);
            Assert.Equal("Hello World", newStatus.OriginationSource);
        }

        #endregion
    }
}
