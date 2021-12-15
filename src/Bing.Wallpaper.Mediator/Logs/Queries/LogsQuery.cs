using Bing.Wallpaper.Data;
using Bing.Wallpaper.Mediator.Models;
using kr.bbon.EntityFrameworkCore.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using kr.bbon.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using kr.bbon.Core.Models;

namespace Bing.Wallpaper.Mediator.Logs.Queries;

public class LogsQuery : PagedModelQueryBase, IRequest<IPagedModel<LogModel>>
{
    public string Level { get; set; }
}

public class LogsQueryHandler : IRequestHandler<LogsQuery, IPagedModel<LogModel>>
{
    public LogsQueryHandler(
        DefaultDatabaseContext dbContext,
        IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<IPagedModel<LogModel>> Handle(LogsQuery request, CancellationToken cancellationToken)
    {
        var items = await dbContext.Logs
            .WhereDependsOn(!string.IsNullOrWhiteSpace(request.Level), x => x.Level == request.Level)
            .WhereDependsOn(!string.IsNullOrWhiteSpace(request.Keyword), x => EF.Functions.Like(x.Message, $"%{request.Keyword.Trim()}%"))
            .AsNoTracking()
            .OrderByDescending(x => x.Logged)
            .Select(x => mapper.Map<LogModel>(x))
            .ToPagedModelAsync(request.Page, request.Limit, cancellationToken);

        return items;
    }

    private readonly DefaultDatabaseContext dbContext;
    private readonly IMapper mapper;
}
