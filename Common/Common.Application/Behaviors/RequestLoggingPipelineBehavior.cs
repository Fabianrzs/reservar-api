using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Common.Application.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger,
    IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        string moduleName = GetModuleName(typeof(TRequest).FullName!);
        string requestName = typeof(TRequest).Name;

        Activity.Current?.SetTag("request.modulo", moduleName);
        Activity.Current?.SetTag("request.name", requestName);

        using (LogContext.PushProperty("Module", moduleName))
        {
            logger.LogInformation("Processing request {RequestName}", requestName);
                TResponse result = await next(cancellationToken);

            if (result.IsSuccess)
            {
                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
                logger.LogInformation("Completed request {RequestName}", requestName);
            }
            else
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogInformation("Completed request {RequestName} with error", requestName);
                }
            }

            return result;
            
        }
    }

    private static string GetModuleName(string requestName) => requestName.Split('.')[2];
}
