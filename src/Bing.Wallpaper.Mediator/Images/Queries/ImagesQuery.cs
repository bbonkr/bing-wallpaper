using AutoMapper;
using Bing.Wallpaper.Data;
using Bing.Wallpaper.Mediator.Models;

using kr.bbon.Core.Models;
using kr.bbon.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Wallpaper.Mediator.Images.Queries;

public class ImagesQuery : PagedModelQueryBase, IRequest<PagedModel<ImageItemModel>>
{
}

public class ImageQueryHandler : IRequestHandler<ImagesQuery, PagedModel<ImageItemModel>>
{
    public ImageQueryHandler(DefaultDatabaseContext dbContext, IMapper mapper, ILogger<ImageQueryHandler> logger)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.logger = logger;
    }
    public async Task<PagedModel<ImageItemModel>> Handle(ImagesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Query images {@request}", request);

        var result = await dbContext.Images
            .Where(x => true)
            .Include(x => x.Metadata)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => mapper.Map<ImageItemModel>(x))
            .AsNoTracking()
            .ToPagedModelAsync(request.Page, request.Limit, cancellationToken);

        foreach (var item in result.Items)
        {
            var tokens = item.FileName.Split(".");
            item.FileName = string.Join(".", tokens.Take(tokens.Length - 1));
            item.FileExtension = $".{tokens.Last()}";
        }

        return result;
    }

    private readonly DefaultDatabaseContext dbContext;
    private readonly IMapper mapper;
    private readonly ILogger logger;
}
