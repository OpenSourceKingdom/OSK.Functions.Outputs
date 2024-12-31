using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using Xunit;

namespace OSK.Functions.Outputs.UnitTests.Internal.Services
{
    public class OutputFactoryTests
    {
        #region Variables

        private readonly OutputFactory _factory;

        #endregion

        #region Constructors

        public OutputFactoryTests()
        {
            _factory = new OutputFactory();
        }

        #endregion

        #region Create(OutputInformation)

        [Fact]
        public void Create_OutputInformation_NullInformation_ThrowsArgumentNullException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _factory.Create(null));
        }

        [Theory]
        [InlineData(FunctionResult.Error)]
        [InlineData(FunctionResult.Failed)]
        [InlineData(FunctionResult.Fault)]
        public void Create_OutputInformation_ErrorResults_NoErrorInformation_ThrowsInvalidOperationException(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.None, null, OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Success)]
        public void Create_OutputInformation_SuccessResults_HasErrorInformation_ThrowsInvalidOperationException(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.None, new ErrorInformation(), OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Failed)]
        [InlineData(FunctionResult.Error)]
        [InlineData(FunctionResult.Fault)]
        public void Create_OutputInformation_ErrorsResults_HasErrorInformation_ReturnsSuccessfully(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.ResourceNotFound, new ErrorInformation([new Error("A bad day")]), OutputDetails.DefaultSource);

            // Act
            var output = _factory.Create(outputInformation);

            // Assert
            Assert.False(output.IsSuccessful);
            Assert.NotNull(output.ErrorInformation);
            Assert.Equal(functionResult, output.Details.Result);
            Assert.Equal(outputInformation.OriginationSource, output.Details.OriginationSource);
            Assert.Equal(outputInformation.ResultSpecificityCode, output.Details.SpecificityCode);
        }

        [Theory]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.BadGateway)]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.UnspecifiedException)]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.ResourceNotFound)]
        public void Create_OutputInformation_SuccessResults_HasNoErrorInformation_InvalidSpecificityCode_ThrowsInvalidOperationException(FunctionResult functionResult,
            ResultSpecificityCode invalidCode)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, invalidCode, null, OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Failed, ResultSpecificityCode.Created)]
        [InlineData(FunctionResult.Error, ResultSpecificityCode.Updated)]
        [InlineData(FunctionResult.Fault, ResultSpecificityCode.Deleted)]
        public void Create_OutputInformation_ErrorResults_HasErrorInformation_InvalidSpecificityCode_ThrowsInvalidOperationException(FunctionResult functionResult,
            ResultSpecificityCode invalidCode)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, invalidCode, new ErrorInformation(), OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Success)]
        public void Create_OutputInformation_SuccessResults_NoErrorInformation_ReturnsSuccessfully(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.Accepted, null, OutputDetails.DefaultSource);

            // Act
            var output = _factory.Create(outputInformation);

            // Assert
            Assert.True(output.IsSuccessful);
            Assert.Null(output.ErrorInformation);
            Assert.Equal(functionResult, output.Details.Result);
            Assert.Equal(outputInformation.OriginationSource, output.Details.OriginationSource);
            Assert.Equal(outputInformation.ResultSpecificityCode, output.Details.SpecificityCode);
        }

        #endregion

        #region Create(Value, OutputInformation)

        [Fact]
        public void Create_Value_OutputInformation_NullInformation_ThrowsArgumentNullException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _factory.Create(1, null));
        }

        [Theory]
        [InlineData(FunctionResult.Error)]
        [InlineData(FunctionResult.Failed)]
        [InlineData(FunctionResult.Fault)]
        public void Create_Value_OutputInformation_ErrorResults_NoErrorInformation_ThrowsInvalidOperationException(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.None, null, OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Success)]
        public void Create_Value_OutputInformation_SuccessResults_HasErrorInformation_ThrowsInvalidOperationException(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.None, new ErrorInformation(), OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.BadGateway)]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.UnspecifiedException)]
        [InlineData(FunctionResult.Success, ResultSpecificityCode.ResourceNotFound)]
        public void Create_Value_OutputInformation_SuccessResults_HasNoErrorInformation_InvalidSpecificityCode_ThrowsInvalidOperationException(FunctionResult functionResult,
            ResultSpecificityCode invalidCode)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, invalidCode, null, OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Failed, ResultSpecificityCode.Created)]
        [InlineData(FunctionResult.Error, ResultSpecificityCode.Updated)]
        [InlineData(FunctionResult.Fault, ResultSpecificityCode.Deleted)]
        public void Create_Value_OutputInformation_ErrorResults_HasErrorInformation_InvalidSpecificityCode_ThrowsInvalidOperationException(FunctionResult functionResult,
            ResultSpecificityCode invalidCode)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, invalidCode, new ErrorInformation(), OutputDetails.DefaultSource);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, outputInformation));
        }

        [Theory]
        [InlineData(FunctionResult.Failed, ResultSpecificityCode.Locked)]
        [InlineData(FunctionResult.Error, ResultSpecificityCode.ResourceNotFound)]
        [InlineData(FunctionResult.Fault, ResultSpecificityCode.UnspecifiedException)]
        public void Create_Value_OutputInformation_ErrorsResults_HasErrorInformation_ReturnsSuccessfully(FunctionResult functionResult,
            ResultSpecificityCode validSpecificity)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, validSpecificity, 
                new ErrorInformation(new Exception()), OutputDetails.DefaultSource);

            // Act
            var output = _factory.Create(default(int), outputInformation);

            // Assert
            Assert.False(output.IsSuccessful);
            Assert.NotNull(output.ErrorInformation);
            Assert.Equal(functionResult, output.Details.Result);
            Assert.Equal(outputInformation.OriginationSource, output.Details.OriginationSource);
            Assert.Equal(outputInformation.ResultSpecificityCode, output.Details.SpecificityCode);
        }

        [Theory]
        [InlineData(FunctionResult.Success)]
        public void Create_Value_OutputInformation_SuccessResults_NoErrorInformation_ReturnsSuccessfully(FunctionResult functionResult)
        {
            // Arrange
            var outputInformation = new OutputInformation(functionResult, ResultSpecificityCode.Accepted, null, OutputDetails.DefaultSource);

            // Act
            var output = _factory.Create(1, outputInformation);

            // Assert
            Assert.True(output.IsSuccessful);
            Assert.Null(output.ErrorInformation);
            Assert.Equal(1, output.Value);
            Assert.Equal(functionResult, output.Details.Result);
            Assert.Equal(outputInformation.OriginationSource, output.Details.OriginationSource);
            Assert.Equal(outputInformation.ResultSpecificityCode, output.Details.SpecificityCode);
        }

        #endregion
    }
}
