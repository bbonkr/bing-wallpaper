using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bing.Wallpaper.Data;
using Bing.Wallpaper.Models;

using Microsoft.EntityFrameworkCore;

namespace Bing.Wallpaper.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageItemModel>> GetAllAsync(int page, int take);

        Task<ImageItemDetailModel> FindByIdAsync(string id);
    }

    public class ImageRepository : IImageRepository
    {
        public ImageRepository(DefaultDatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ImageItemModel>> GetAllAsync(int page, int take)
        {
            var skip = (page - 1) * take;

            var query = dbContext.Images.Where(x => true)
                .Include(x=>x.Metadata)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                //.Select(x => new ImageItemModel(x.Id, x.FileName, x.FileSize, x.CreatedAt.Ticks));
                .Select(x => mapper.Map<ImageItemModel>(x));

            var items = await query.AsNoTracking().ToListAsync();

            return items;
        }

        public async Task<ImageItemDetailModel> FindByIdAsync(string id)
        {
            var query = dbContext.Images
                .Include(x => x.Metadata)
                .Where(x => x.Id == id)
               .Select(x => mapper.Map<ImageItemDetailModel>(x));

            var items = await query.AsNoTracking().FirstOrDefaultAsync();

            return items;
        }

        private readonly DefaultDatabaseContext dbContext;
        private readonly IMapper mapper;
    }
}
