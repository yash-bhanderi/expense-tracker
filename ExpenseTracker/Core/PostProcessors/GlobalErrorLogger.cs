// using System.Diagnostics.CodeAnalysis;
// using FastEndpoints;
// using FluentValidation;
// using Newtonsoft.Json;
//
// namespace CodeCommandos.Core.PostProcessors;
//
// [ExcludeFromCodeCoverage]
// public class GlobalErrorLogger : IGlobalPostProcessor
// {
//     public Task PostProcessAsync(IPostProcessorContext context, CancellationToken ct)
//     {
//         var logger = context.HttpContext.Resolve<ILogger<GlobalErrorLogger>>();
//         var failures = context.ValidationFailures;
//         
//         if (context.ValidationFailures.Count > 0 && !context.HttpContext.Response.HasStarted)
//         {
//             logger.LogInformation("Error code: {@errorCode}, Validation errors : {@errors}", failures.First().ErrorCode,
//                 JsonConvert.SerializeObject(failures));
//             if (!context.HttpContext.Response.HasStarted)
//             {
//                 throw new ValidationException(failures);
//             }
//         }
//         return Task.CompletedTask;
//     }
// }