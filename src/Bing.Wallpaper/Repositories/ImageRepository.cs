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
    public interface IImageRepository
    {
        Task<IPagedModel<ImageItemModel>> GetAllAsync(int page, int take, CancellationToken cancellationToken = default);

        Task<ImageItemDetailModel> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    }

    public class ImageRepository : IImageRepository
    {
        public ImageRepository(DefaultDatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IPagedModel<ImageItemModel>> GetAllAsync(int page, int take, CancellationToken cancellationToken = default)
        {
            var skip = (page - 1) * take;

            var items = await dbContext.Images.Where(x => true)
                .Include(x => x.Metadata)
                .OrderByDescending(x => x.CreatedAt)
                //.Select(x => new ImageItemModel(x.Id, x.FileName, x.FileSize, x.CreatedAt.Ticks));
                .Select(x => mapper.Map<ImageItemModel>(x))
                .AsNoTracking()
                .ToPagedModelAsync(page, take, cancellationToken);

            return items;
        }

        public async Task<ImageItemDetailModel> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var item = await dbContext.Images
                .Include(x => x.Metadata)
                .Where(x => x.Id == id)
                .AsNoTracking()
               .Select(x => mapper.Map<ImageItemDetailModel>(x))
               .FirstOrDefaultAsync(cancellationToken);

            return item;
        }

        private readonly DefaultDatabaseContext dbContext;
        private readonly IMapper mapper;
    }
}
