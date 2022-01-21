using System;
using System.Linq;
using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using kr.bbon.Core;
using kr.bbon.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Wallpaper.Infrastructure.Validations;

public class ValidatorInterceptor : IValidatorInterceptor
{
    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
    {
        if (!result.IsValid)
        {
            var statusCode = HttpStatusCode.BadRequest;

            var error = new ErrorModel("Request payload is invalid",
                Code: statusCode.ToString(),
                InnerErrors: result.Errors.Select(x => new ErrorModel
                (
                    x.ErrorMessage,
                    Code: x.ErrorCode,
                    Reference: x.PropertyName
                )).ToList());

            throw new ApiException(statusCode, error);
        }

        return result;
    }

    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        return commonContext;
    }
}

