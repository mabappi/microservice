using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Infrastructure.Exceptions
{
    public static class ExceptionHandler
    {
        public static async Task<ActionResult<T>> TryCatch<T>([NotNull]Func<Task<T>> tryAction, [CanBeNull]ILogger logger = null)
        {
            try
            {
                return new OkObjectResult(await tryAction());
            }
            catch (NotFoundException notFoundException)
            {
                logger?.LogError(notFoundException, notFoundException.Message);
                return new NotFoundObjectResult(notFoundException.Message);
            }
            catch (DomainException domainException)
            {
                logger?.LogError(domainException, domainException.Message);
                return new BadRequestObjectResult(domainException.ToUserSummary());
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
