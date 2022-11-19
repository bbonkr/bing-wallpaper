using System;
using System.Threading.Tasks;
using kr.bbon.AspNetCore.Filters;
using kr.bbon.Core.Exceptions;
using kr.bbon.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Bing.Wallpaper.Infrastructure.Filters;

public class ApiExceptionHandlerWithLoggingFilter : ApiExceptionHandlerFilter
{
    public ApiExceptionHandlerWithLoggingFilter(
        ILogger<ApiExceptionHandlerWithLoggingFilter> logger)
    {
        this.logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        Logging(context);

        base.OnException(context);
    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        Logging(context);

        return base.OnExceptionAsync(context);
    }

    private void Logging(ExceptionContext context)
    {
        ErrorModel errors = default;

        if (context.Exception != null)
        {
            if (context.Exception is ApiException apiException)
            {
                if (apiException.Error != null)
                {
                    errors = apiException.Error;
                    LogContext.PushProperty("Errors", errors, true);
                }
                //logger.LogError(apiException, apiException.Message);
            }
            //else
            //{
            //    logger.LogError(context.Exception, context.Exception.Message);
            //}

            logger.LogError(context.Exception, $"{context.Exception.Message}");
        }
    }

    private readonly ILogger logger;
}

