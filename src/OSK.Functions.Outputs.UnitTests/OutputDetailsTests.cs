﻿using OSK.Functions.Outputs.Abstractions;
using Xunit;

namespace OSK.Functions.Outputs.UnitTests
{
    public class OutputDetailsTests
    {
        #region ToString

        [Fact]
        public void ToString_NoOrigination_ConvertsToExpectedStatusString()
        {
            // Arrange
            var details = new OutputDetails(FunctionResult.Success, ResultSpecificityCode.Accepted);

            // Act
            var str = details.ToString();

            // Assert
            Assert.Equal($"{(int)FunctionResult.Success}.{(int)ResultSpecificityCode.Accepted}", str);
        }

        [Fact]
        public void StatusCodeString_IncludesOrigination_ShouldNotIncludeOrigination_ConvertsToExpectedStatusCode()
        {
            // Arrange
            var details = new OutputDetails(FunctionResult.Success, ResultSpecificityCode.None, "Test Library");

            // Act
            var str = details.ToString(false);

            // Assert
            Assert.Equal($"{(int)FunctionResult.Success}.{(int)ResultSpecificityCode.None}", str);
        }

        [Fact]
        public void StatusCodeString_IncludesOrigination_ShouldIncludeOrigination_ConvertsToExpectedStatusCode()
        {
            // Arrange
            var details = new OutputDetails(FunctionResult.Failed, ResultSpecificityCode.DownStreamError, "Test Library");

            // Act
            var str = details.ToString(true);

            // Assert
            Assert.Equal($"{(int)FunctionResult.Failed}.{(int)ResultSpecificityCode.DownStreamError},Test Library", str);
        }

        #endregion

        #region Constructor(string)

        [Theory]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.None)]
        public void OutputDetails_Constructor_String_StatusStringNoOrigination_ReturnsExpecteddetails(FunctionResult functionResult,
            ResultSpecificityCode specificityCode)
        {
            // Arrange
            var details = new OutputDetails(functionResult, specificityCode);
            var str = details.ToString();

            // Act
            var newDetails = OutputDetails.Parse(str);

            // Assert
            Assert.Equal(functionResult, newDetails.Result);
            Assert.Equal(specificityCode, newDetails.SpecificityCode);
            // No origination source was included in the ToString, it should be the default
            Assert.Equal(OutputDetails.DefaultSource, newDetails.OriginationSource);
        }

        [Theory]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.None)]
        public void OutputDetails_Constructor_String_StatusStringHasOrigination_ReturnsExpecteddetails(FunctionResult functionResult,
            ResultSpecificityCode spcecificityCode)
        {
            // Arrange
            var details = new OutputDetails(functionResult, spcecificityCode, "Hello World");
            var str = details.ToString(true);

            // Act
            var newDetails = OutputDetails.Parse(str);

            // Assert
            Assert.Equal(functionResult, newDetails.Result);
            Assert.Equal(spcecificityCode, newDetails.SpecificityCode);
            Assert.Equal("Hello World", newDetails.OriginationSource);
        }

        [Fact]
        public void OutputDetails_Constructor_String_SpecifictyCodeNotValidValue_ReturnsDetailsWithUnRecognizedSpecificityCode()
        {
            // Arrange
            var details = "20.118";

            // Act
            var newDetails = OutputDetails.Parse(details);

            // Assert
            Assert.Equal(FunctionResult.Success, newDetails.Result);
            Assert.Equal(ResultSpecificityCode.SpecificityNotRecognized, newDetails.SpecificityCode);
        }

        #endregion
    }
}
