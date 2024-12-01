using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using System.Net;
using Xunit;

namespace OSK.Functions.Outputs.UnitTests.Internal.Services
{
    public class OutputFactoryTests
    {
        #region Variables

        private readonly IOutputFactory _factory;

        #endregion

        #region Constructors

        public OutputFactoryTests()
        {
            _factory = new OutputFactory();
        }

        #endregion

        #region Create(OutputStatusCode)

        [Theory]
        [InlineData(HttpStatusCode.Created, true)] // 200s
        [InlineData(HttpStatusCode.OK, true)]
        [InlineData(HttpStatusCode.Accepted, true)]
        [InlineData(HttpStatusCode.Ambiguous, false)] // 300s
        [InlineData(HttpStatusCode.RedirectMethod, false)]
        [InlineData(HttpStatusCode.NotFound, false)] // 400s
        [InlineData(HttpStatusCode.BadRequest, false)]
        [InlineData(HttpStatusCode.InternalServerError, false)] // 500s
        [InlineData(HttpStatusCode.NotImplemented, false)]
        public void Create_OutputStatusCode_VariousCodes_ThrowsExceptionOnExpectedBehavior(HttpStatusCode statusCode, bool shouldPass)
        {
            // Arrange
            var code = new OutputStatusCode(statusCode, DetailCode.None, OutputStatusCode.DefaultSource);

            // Act/Assert
            if (shouldPass)
            {
                var output = _factory.Create(code);
                Assert.NotNull(output);
                Assert.True(output.IsSuccessful);
                Assert.Null(output.ErrorInformation);
                Assert.Equal(statusCode, output.Code.StatusCode);
            }
            else
            {
                Assert.Throws<InvalidOperationException>(() => _factory.Create(code));
            }
        }

        #endregion

        #region Create(OutputStatusCode, IEnumerable<Error>)

        [Fact]
        public void Create_Errors_SuccessCodeWithErrors_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(OutputStatusCode.Success,
                new List<Error>()
                {
                    new Error("Hi")
                }));
        }

        [Fact]
        public void Create_Errors_NonSuccessCodeWithErrors_ReturnsSuccessfully()
        {
            // Arrange/Act
            var output = _factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, OutputStatusCode.DefaultSource),
                new List<Error>()
                {
                    new Error("Hi")
                });

            // Assert
            Assert.NotNull(output);
            Assert.False(output.IsSuccessful);
            Assert.NotNull(output.ErrorInformation);
            Assert.Equal("Hi", output.ErrorInformation!.Value.Errors.First().Message);
        }

        #endregion

        #region Create(OutputStatusCode, Exception)

        [Fact]
        public void Create_Exception_SuccessCodeWithException_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(OutputStatusCode.Success,
                new ArgumentNullException("Hi")));
        }

        [Fact]
        public void Create_Exception_NonSuccessCodeWithException_ReturnsSuccessfully()
        {
            // Arrange/Act
            var output = _factory.Create(
                new OutputStatusCode(HttpStatusCode.InternalServerError, DetailCode.None, OutputStatusCode.DefaultSource),
                new ArgumentNullException("Hi"));

            // Assert
            Assert.NotNull(output);
            Assert.False(output.IsSuccessful);
            Assert.NotNull(output.ErrorInformation?.Exception);
            Assert.True(output.ErrorInformation!.Value.Exception is ArgumentNullException);
            Assert.Contains("Hi", output.ErrorInformation!.Value.Exception.Message);
        }

        #endregion

        #region Create<T>(OutputStatusCode)

        [Theory]
        [InlineData(HttpStatusCode.Created, true)] // 200s
        [InlineData(HttpStatusCode.OK, true)]
        [InlineData(HttpStatusCode.Accepted, true)]
        [InlineData(HttpStatusCode.Ambiguous, false)] // 300s
        [InlineData(HttpStatusCode.RedirectMethod, false)]
        [InlineData(HttpStatusCode.NotFound, false)] // 400s
        [InlineData(HttpStatusCode.BadRequest, false)]
        [InlineData(HttpStatusCode.InternalServerError, false)] // 500s
        [InlineData(HttpStatusCode.NotImplemented, false)]
        public void Create_T_OutputStatusCode_VariousCodes_ThrowsExceptionOnExpectedBehavior(HttpStatusCode statusCode, bool shouldPass)
        {
            // Arrange
            var code = new OutputStatusCode(statusCode, DetailCode.None, OutputStatusCode.DefaultSource);

            // Act/Assert
            if (shouldPass)
            {
                var output = _factory.Create(1, code);
                Assert.NotNull(output);
                Assert.True(output.IsSuccessful);
                Assert.Null(output.ErrorInformation);
                Assert.Equal(statusCode, output.Code.StatusCode);
            }
            else
            {
                Assert.Throws<InvalidOperationException>(() => _factory.Create(1, code));
            }
        }

        #endregion

        #region Create<T>(OutputStatusCode, IEnumerable<Error>)

        [Fact]
        public void Create_T_Errors_SuccessCodeWithErrors_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create<int>(OutputStatusCode.Success,
                new List<Error>()
                {
                    new Error("Hi")
                }));
        }

        [Fact]
        public void Create_T_Errors_NonSuccessCodeWithErrors_ReturnsSuccessfully()
        {
            // Arrange/Act
            var output = _factory.Create<int>(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, OutputStatusCode.DefaultSource),
                new List<Error>()
                {
                    new Error("Hi")
                });

            // Assert
            Assert.NotNull(output);
            Assert.False(output.IsSuccessful);
            Assert.NotNull(output.ErrorInformation);
            Assert.Equal("Hi", output.ErrorInformation!.Value.Errors.First().Message);
        }

        #endregion

        #region Create<T>(OutputStatusCode, Exception)

        [Fact]
        public void Create_T_Exception_SuccessCodeWithException_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create<int>(OutputStatusCode.Success,
                new ArgumentNullException("Hi")));
        }

        [Fact]
        public void Create_T_Exception_NonSuccessCodeWithException_ReturnsSuccessfully()
        {
            // Arrange/Act
            var output = _factory.Create<int>(
                new OutputStatusCode(HttpStatusCode.InternalServerError, DetailCode.None, OutputStatusCode.DefaultSource),
                new ArgumentNullException("Hi"));

            // Assert
            Assert.NotNull(output);
            Assert.False(output.IsSuccessful);
            Assert.NotNull(output.ErrorInformation?.Exception);
            Assert.True(output.ErrorInformation!.Value.Exception is ArgumentNullException);
            Assert.Contains("Hi", output.ErrorInformation!.Value.Exception.Message);
        }

        [Fact]
        public void Create_T_SuccessWithoutValue_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _factory.Create<object>(null!,
                new OutputStatusCode(HttpStatusCode.OK, DetailCode.None, OutputStatusCode.DefaultSource)));
        }

        #endregion
    }
}
