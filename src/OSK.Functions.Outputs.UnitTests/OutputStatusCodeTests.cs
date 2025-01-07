using OSK.Functions.Outputs.Abstractions;
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
            var details = new OutputStatusCode(OutputSpecificityCode.Accepted);

            // Act
            var str = details.ToString();

            // Assert
            Assert.Equal($"{(int)OutputSpecificityCode.Accepted}", str);
        }

        [Fact]
        public void StatusCodeString_IncludesOrigination_ShouldNotIncludeOrigination_ConvertsToExpectedStatusCode()
        {
            // Arrange
            var details = new OutputStatusCode(OutputSpecificityCode.Success, "Test Library");

            // Act
            var str = details.ToString(false);

            // Assert
            Assert.Equal($"{(int)OutputSpecificityCode.Success}", str);
        }

        [Fact]
        public void StatusCodeString_IncludesOrigination_ShouldIncludeOrigination_ConvertsToExpectedStatusCode()
        {
            // Arrange
            var details = new OutputStatusCode(OutputSpecificityCode.ThirdPartyServiceFailure, "Test Library");

            // Act
            var str = details.ToString(true);

            // Assert
            Assert.Equal($"{(int)OutputSpecificityCode.ThirdPartyServiceFailure},Test Library", str);
        }

        #endregion

        #region Constructor(string)

        [Theory]
        [InlineData(OutputSpecificityCode.Success)]
        public void OutputDetails_Constructor_String_StatusStringNoOrigination_ReturnsExpectedStatusCode(OutputSpecificityCode specificityCode)
        {
            // Arrange
            var details = new OutputStatusCode(specificityCode);
            var str = details.ToString();

            // Act
            var newDetails = OutputStatusCode.Parse(str);

            // Assert
            Assert.Equal(specificityCode, newDetails.SpecificityCode);
            // No origination source was included in the ToString, it should be the default
            Assert.Equal(OutputStatusCode.DefaultSource, newDetails.OriginationSource);
        }

        [Theory]
        [InlineData(OutputSpecificityCode.Created)]
        public void OutputDetails_Constructor_String_StatusStringHasOrigination_ReturnsExpectedStatusCode(OutputSpecificityCode spcecificityCode)
        {
            // Arrange
            var details = new OutputStatusCode(spcecificityCode, "Hello World");
            var str = details.ToString(true);

            // Act
            var newDetails = OutputStatusCode.Parse(str);

            // Assert
            Assert.Equal(spcecificityCode, newDetails.SpecificityCode);
            Assert.Equal("Hello World", newDetails.OriginationSource);
        }

        [Fact]
        public void OutputDetails_Constructor_String_SpecifictyCodeNotValidValue_ReturnsDetailsWithUnRecognizedSpecificityCode()
        {
            // Arrange
            var details = "918";

            // Act
            var newDetails = OutputStatusCode.Parse(details);

            // Assert
            Assert.Equal(OutputSpecificityCode.SpecificityNotRecognized, newDetails.SpecificityCode);
        }

        #endregion
    }
}
