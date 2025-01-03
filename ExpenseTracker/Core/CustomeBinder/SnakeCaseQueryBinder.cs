// using System.ComponentModel;
// using System.Diagnostics.CodeAnalysis;
// using System.Globalization;
// using System.Reflection;
// using CodeCommandos.Shared;
// using CodeCommandos.Shared.Dtos;
// using FastEndpoints;
//
// namespace CodeCommandos.Core.CustomeBinder;
//
// [ExcludeFromCodeCoverage]
// public class SnakeCaseQueryBinder
// {
//     public static void SnakeCaseBindFunc(object request, Type tRequest, BinderContext context, CancellationToken cancellationToken)
//     {
//         object Parse(string valueToConvert, Type dataType)
//         {
//             try
//             {
//                 TypeConverter obj = TypeDescriptor.GetConverter(dataType);
//                 object value = obj.ConvertFromString(null, CultureInfo.InvariantCulture, valueToConvert);
//                 return value;
//             }
//             catch (NotSupportedException)
//             {
//                 throw new BusinessException(ErrorResponseProvider.InvalidRequestParameters.InternalCode);
//             }
//             catch (FormatException)
//             {
//                 throw new BusinessException(ErrorResponseProvider.InvalidRequestParameters.InternalCode);
//             }
//             catch(Exception)
//             {
//                 throw;
//             }
//         }
//
//         foreach (var param in context.HttpContext.Request.Query)
//         {
//             var property = request.GetType()
//                 .GetProperty(param.Key.Replace("_", ""), BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
//
//             property?.SetValue(request, Parse(param.Value[0], property.PropertyType));
//         }
//     }
// }