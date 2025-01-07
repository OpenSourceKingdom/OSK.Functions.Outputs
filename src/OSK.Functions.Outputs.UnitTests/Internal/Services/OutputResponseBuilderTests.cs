using Moq;
using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using Xunit;

namespace OSK.Functions.Outputs.UnitTests.Internal.Services
{
    public class OutputResponseBuilderTests
    {
        #region Variables

        private readonly OutputResponseBuilder _builder;

        #endregion

        #region Constructors

        public OutputResponseBuilderTests()
        {
            var mockOutputFactory = new Mock<IOutputFactory>();

            mockOutputFactory.Setup(m => m.CreateOutput(It.IsAny<OutputStatusCode>(),
                It.IsAny<ErrorInformation?>(), It.IsAny<OutputDetails?>()))
                .Returns((OutputStatusCode statusCode, ErrorInformation? error, OutputDetails? details)
                    => new Output(statusCode, error, details));

            mockOutputFactory.Setup(m => m.CreateOutput<int>(It.IsAny<int>(), It.IsAny<OutputStatusCode>(),
                It.IsAny<ErrorInformation?>(), It.IsAny<OutputDetails?>()))
                .Returns((int value, OutputStatusCode statusCode, ErrorInformation? error, OutputDetails? details)
                    => new Output<int>(value, statusCode, error, details));

            _builder = new OutputResponseBuilder(mockOutputFactory.Object);
        }

        #endregion

        #region BuildResponse

        [Fact]
        public void BuildResponse_NoOutputs_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(_builder.BuildResponse);
        }

        [Fact]
        public void BuildResponse_BuilderSetWithErrorInformation_HasError_ReturnsOutputWithError()
        {
            // Arrange
            var origination = "Hello World";
            var specificityCode = OutputSpecificityCode.RateLimited;
            var errorMessage = "No way";

            _builder.WithOrigination(origination)
                .AddError(errorMessage, specificityCode);

            // Act
            var response = _builder.BuildResponse();

            // Assert
            Assert.False(response.IsSuccessful);
            Assert.Single(response.Outputs);
            Assert.Null(response.AdvancedDetails);
            Assert.Equal(specificityCode, response.StatusCode.SpecificityCode);
            Assert.Equal(origination, response.StatusCode.OriginationSource);

            Assert.Equal(specificityCode, response.Outputs[0].StatusCode.SpecificityCode);
            Assert.Equal(origination, response.Outputs[0].StatusCode.OriginationSource);
            Assert.NotNull(response.Outputs[0].ErrorInformation?.Error);
            Assert.Equal(errorMessage, response.Outputs[0].ErrorInformation!.Value.Error!.Value.Message);
            Assert.Equal(errorMessage, response.Outputs[0].GetErrorString());
        }

        [Fact]
        public void BuildResponse_BuilderSetWithErrorInformation_HasException_ReturnsOutputWithException()
        {
            // Arrange
            var origination = "Hello World";
            var specificityCode = OutputSpecificityCode.UnknownError;
            var exception = new InvalidOperationException("What");

            _builder.WithOrigination(origination)
                .AddException(exception);

            // Act
            var response = _builder.BuildResponse();

            // Assert
            Assert.False(response.IsSuccessful);
            Assert.Single(response.Outputs);
            Assert.Null(response.AdvancedDetails);
            Assert.Equal(specificityCode, response.StatusCode.SpecificityCode);
            Assert.Equal(origination, response.StatusCode.OriginationSource);

            Assert.Equal(specificityCode, response.Outputs[0].StatusCode.SpecificityCode);
            Assert.Equal(origination, response.Outputs[0].StatusCode.OriginationSource);
            Assert.NotNull(response.Outputs[0].ErrorInformation?.Exception);
            Assert.IsType<InvalidOperationException>(response.Outputs[0].ErrorInformation!.Value.Exception);
            Assert.Equal(exception.Message, response.Outputs[0].ErrorInformation!.Value.Exception!.Message);
            Assert.Equal(exception.Message, response.Outputs[0].GetErrorString());
        }

        [Fact]
        public void BuildResponse_BuilderSetWithSuccessInformation_HasSuccess_ReturnsOutputWithSuccess()
        {
            // Arrange
            var origination = "Hello World";
            var specificityCode = OutputSpecificityCode.Accepted;

            _builder.WithOrigination(origination)
                .AddSuccess(specificityCode);

            // Act
            var response = _builder.BuildResponse();

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Single(response.Outputs);
            Assert.Null(response.AdvancedDetails);
            Assert.Equal(specificityCode, response.StatusCode.SpecificityCode);
            Assert.Equal(origination, response.StatusCode.OriginationSource);

            Assert.Equal(specificityCode, response.Outputs[0].StatusCode.SpecificityCode);
            Assert.Equal(origination, response.Outputs[0].StatusCode.OriginationSource);
            Assert.Null(response.Outputs[0].ErrorInformation);
        }

        [Fact]
        public void BuildResponse_BuilderSetWithMultipleOutputs_ReturnsAggregatedResponseWithMultipleOutputStatus()
        {
            // Arrange
            var origination = "Hello World";
            var successCode1 = OutputSpecificityCode.Accepted;

            var successCode2 = OutputSpecificityCode.Success;

            var errorMessage = "error1";
            var errorCode = OutputSpecificityCode.DuplicateData;

            _builder.WithOrigination(origination)
                .AddError(errorMessage, errorCode)
                .AddSuccess(successCode1)
                .WithRunTimeMetric()
                .WithTimeStamp()
                .AddSuccess(successCode2);

            // Act
            var response = _builder.BuildResponse();

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(3, response.Outputs.Length);

            Assert.Equal(OutputSpecificityCode.MultipleOutputs, response.StatusCode.SpecificityCode);
            Assert.Equal(origination, response.StatusCode.OriginationSource);

            var error1 = response.Outputs[0];
            Assert.Equal(errorCode, error1.StatusCode.SpecificityCode);
            Assert.Equal(origination, error1.StatusCode.OriginationSource);
            Assert.NotNull(error1.ErrorInformation?.Error);
            Assert.Equal(errorMessage, error1.ErrorInformation!.Value.Error!.Value.Message);
            Assert.Equal(errorMessage, error1.GetErrorString());
            Assert.Null(error1.AdvancedDetails);

            var success1 = response.Outputs[1];
            Assert.Equal(successCode1, success1.StatusCode.SpecificityCode);
            Assert.Equal(origination, success1.StatusCode.OriginationSource);
            Assert.Null(success1.ErrorInformation);
            Assert.Null(success1.AdvancedDetails);

            var success2 = response.Outputs[2];
            Assert.Equal(successCode1, success1.StatusCode.SpecificityCode);
            Assert.Equal(origination, success1.StatusCode.OriginationSource);
            Assert.Null(success2.ErrorInformation);
            Assert.NotNull(success2.AdvancedDetails);
            Assert.NotNull(success2.AdvancedDetails!.Value.RunTimeInMilliseconds);
            Assert.NotNull(success2.AdvancedDetails!.Value.CompletionTime);

            Assert.NotNull(response.AdvancedDetails);
            Assert.Equal(response.AdvancedDetails.Value.RunTimeInMilliseconds, success2.AdvancedDetails!.Value.RunTimeInMilliseconds);
            Assert.Equal(response.AdvancedDetails.Value.CompletionTime, success2.AdvancedDetails!.Value.CompletionTime);
        }

        #endregion
    }
}
