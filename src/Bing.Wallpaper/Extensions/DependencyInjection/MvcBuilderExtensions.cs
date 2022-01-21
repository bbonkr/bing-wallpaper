using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Wallpaper.Extensions.DependencyInjection;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder ConfigureDefaultJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
             options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
             options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
             options.JsonSerializerOptions.AllowTrailingCommas = true;
             options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
             options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
             options.JsonSerializerOptions.WriteIndented = true;
             options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
             options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
             options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
         });

        return builder;
    }


}


