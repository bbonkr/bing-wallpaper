using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Bing.Wallpaper.Data;
using Bing.Wallpaper.Models;

using kr.bbon.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Bing.Wallpaper.Repositories
{
    public interface IAppLogRepository
    {
        Task<IPagedModel<LogModel>> GetAllAsync(int page = 1, int take = 10,string level = "", string keyword = "", CancellationToken cancellationToken = default);
    }
    
    public class AppLogRepository : IAppLogRepository
    {
        public AppLogRepository(DefaultDatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IPagedModel<LogModel>> GetAllAsync(int page = 1, int take = 10,string level = "", string keyword = "", CancellationToken cancellationToken = default)
        {
            var skip = (page - 1) * take;
            var query = dbContext.Logs.Where(x => true);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => EF.Functions.Like(x.Message, $"%{keyword}%"));
            }

            if (!string.IsNullOrWhiteSpace(level))
            {
                query = query.Where(x => x.Level == level);
            }

            query = query.OrderByDescending(x => x.Logged);

            var items = await query.AsNoTracking()
                .Select(x => mapper.Map<LogModel>(x))
                .ToPagedModelAsync(page, take, cancellationToken);

            return items;
        }

        private readonly DefaultDatabaseContext dbContext;
        private readonly IMapper mapper;
    }
}
