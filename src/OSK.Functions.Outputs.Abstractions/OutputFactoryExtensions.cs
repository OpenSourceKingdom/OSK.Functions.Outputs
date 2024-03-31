using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputFactoryExtensions
    {
        #region Success

        public static IOutput Success(this IOutputFactory factory)
        {
            return factory.Create(OutputStatusCode.Success);
        }

        public static IOutput<TValue> Success<TValue>(this IOutputFactory factory, TValue value)
        {
            return factory.Create(value, OutputStatusCode.Success);
        }

        #endregion

        #region BadRequest

        public static IOutput BadRequest(this IOutputFactory factory,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput BadRequest(this IOutputFactory factory,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput BadRequest(this IOutputFactory factory,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> BadRequest<TValue>(this IOutputFactory factory,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> BadRequest<TValue>(this IOutputFactory factory,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput BadRequest(this IOutputFactory factory, DetailCode detailCode,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSource),
                errors);
        }

        public static IOutput<TValue> BadRequest<TValue>(this IOutputFactory factory,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput<TValue> BadRequest<TValue>(this IOutputFactory factory,
            DetailCode detailCode, IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSource),
                errors);
        }

        #endregion

        #region Conflict

        public static IOutput Conflict(this IOutputFactory factory,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.Conflict, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput Conflict(this IOutputFactory factory,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.Conflict, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput Conflict(this IOutputFactory factory,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.Conflict, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> Conflict<TValue>(this IOutputFactory factory,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.Conflict, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> Conflict<TValue>(this IOutputFactory factory,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput Conflict(this IOutputFactory factory, DetailCode detailCode,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.Conflict, detailCode, originationSource),
                errors);
        }

        public static IOutput<TValue> Conflict<TValue>(this IOutputFactory factory,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.Conflict, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput<TValue> Conflict<TValue>(this IOutputFactory factory,
            DetailCode detailCode, IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.Conflict, detailCode, originationSource),
                errors);
        }

        #endregion

        #region NotFound

        public static IOutput NotFound(this IOutputFactory factory,
             IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.NotFound, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput NotFound(this IOutputFactory factory,
            DetailCode detailCode, IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.NotFound, detailCode, originationSource),
                errors);
        }

        public static IOutput NotFound(this IOutputFactory factory,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.NotFound, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput NotFound(this IOutputFactory factory,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.NotFound, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> NotFound<TValue>(this IOutputFactory factory,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.NotFound, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput<TValue> NotFound<TValue>(this IOutputFactory factory,
            DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource, params Error[] errors)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.NotFound, detailCode, originationSource),
                errors);
        }

        public static IOutput<TValue> NotFound<TValue>(this IOutputFactory factory,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.NotFound, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> NotFound<TValue>(this IOutputFactory factory,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.NotFound, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        #endregion

        #region Error

        public static IOutput Error(this IOutputFactory factory, HttpStatusCode statusCode,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(statusCode, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput Error(this IOutputFactory factory,
            HttpStatusCode statusCode, string error, 
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(statusCode, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput Error(this IOutputFactory factory, HttpStatusCode statusCode,
            string error, DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(statusCode, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> Error<TValue>(this IOutputFactory factory, HttpStatusCode statusCode,
            string error, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(statusCode, DetailCode.None, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput<TValue> Error<TValue>(this IOutputFactory factory,
            HttpStatusCode statusCode, string error, 
            DetailCode detailCode, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(statusCode, detailCode, originationSource),
                new Error[] { new Error(error) });
        }

        public static IOutput Error(this IOutputFactory factory, HttpStatusCode statusCode,
            DetailCode detailCode, IEnumerable<Error> errors, 
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(statusCode, detailCode, originationSource),
                errors);
        }

        public static IOutput<TValue> Error<TValue>(this IOutputFactory factory,
            HttpStatusCode statusCode, IEnumerable<Error> errors,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(statusCode, DetailCode.None, originationSource),
                errors);
        }

        public static IOutput<TValue> Error<TValue>(this IOutputFactory factory,
            HttpStatusCode statusCode, DetailCode detailCode,
            IEnumerable<Error> errors, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(statusCode, detailCode, originationSource),
                errors);
        }

        #endregion

        #region Exception

        public static IOutput Exception(this IOutputFactory factory,
            Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.InternalServerError, DetailCode.None, originationSource),
                ex);
        }

        public static IOutput Exception(this IOutputFactory factory,
            DetailCode detailCode, Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.InternalServerError, detailCode, originationSource),
                ex);
        }

        public static IOutput<TValue> Exception<TValue>(this IOutputFactory factory, Exception ex,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.InternalServerError, DetailCode.None, originationSource),
                ex);
        }

        public static IOutput<TValue> Exception<TValue>(this IOutputFactory factory,
            DetailCode detailCode, Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.InternalServerError, detailCode, originationSource),
                ex);
        }

        #endregion

        #region Paginated

        public static IOutput<PaginatedOutput<TValue>> Paginated<TValue>(this IOutputFactory factory,
            IEnumerable<TValue> items, long skip, long take, long? total)
        {
            return factory.Create(
                new PaginatedOutput<TValue>()
                {
                    Items = items.ToList(),
                    Skip = skip,
                    Take = take,
                    Total = total
                },
                OutputStatusCode.Success);
        }

        #endregion
    }
}
