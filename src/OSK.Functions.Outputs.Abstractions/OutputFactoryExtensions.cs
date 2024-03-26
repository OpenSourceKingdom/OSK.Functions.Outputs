using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputFactoryExtensions
    {
        public static IOutput Success(this IOutputFactory factory)
        {
            return factory.Create(OutputStatusCode.Success);
        }

        public static IOutput<TValue> Success<TValue>(this IOutputFactory factory, TValue value)
        {
            return factory.Create(value, OutputStatusCode.Success);
        }

        public static IOutput BadRequest(this IOutputFactory factory, int originationSourceId,
            params Error[] errors)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, originationSourceId),
                errors);
        }

        public static IOutput BadRequest(this IOutputFactory factory, int originationSourceId,
            DetailCode detailCode, params Error[] errors)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSourceId),
                errors);
        }

        public static IOutput<TValue> BadRequest<TValue>(this IOutputFactory factory, int originationSourceId,
            params Error[] errors)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, DetailCode.None, originationSourceId),
                errors);
        }

        public static IOutput<TValue> BadRequest<TValue>(this IOutputFactory factory, int originationSourceId,
            DetailCode detailCode, params Error[] errors)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.BadRequest, detailCode, originationSourceId),
                errors);
        }

        public static IOutput NotFound(this IOutputFactory factory, int originationSourceId,
            params Error[] errors)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.NotFound, DetailCode.None, originationSourceId),
                errors);
        }

        public static IOutput NotFound(this IOutputFactory factory, int originationSourceId,
            DetailCode detailCode, params Error[] errors)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.NotFound, detailCode, originationSourceId),
                errors);
        }

        public static IOutput<TValue> NotFound<TValue>(this IOutputFactory factory, int originationSourceId,
            params Error[] errors)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.NotFound, DetailCode.None, originationSourceId),
                errors);
        }

        public static IOutput<TValue> NotFound<TValue>(this IOutputFactory factory, int originationSourceId, 
            DetailCode detailCode, params Error[] errors)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.NotFound, detailCode, originationSourceId),
                errors);
        }

        public static IOutput Exception(this IOutputFactory factory, int originationSourceId,
            Exception ex)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.InternalServerError, DetailCode.None, originationSourceId),
                ex);
        }

        public static IOutput Exception(this IOutputFactory factory, int originationSourceId,
            DetailCode detailCode, Exception ex)
        {
            return factory.Create(
                new OutputStatusCode(HttpStatusCode.InternalServerError, detailCode, originationSourceId),
                ex);
        }

        public static IOutput<TValue> Exception<TValue>(this IOutputFactory factory, int originationSourceId,
            Exception ex)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.InternalServerError, DetailCode.None, originationSourceId),
                ex);
        }

        public static IOutput<TValue> Exception<TValue>(this IOutputFactory factory, int originationSourceId,
            DetailCode detailCode, Exception ex)
        {
            return factory.Create<TValue>(
                new OutputStatusCode(HttpStatusCode.InternalServerError, detailCode, originationSourceId),
                ex);
        }

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
    }
}
