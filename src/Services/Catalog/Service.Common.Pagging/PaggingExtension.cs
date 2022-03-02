﻿using Service.Common.Collection;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Service.Common.Pagging
{
    public static class PaggingExtension
    {
        public static async Task<DataCollection<T>> GetPagedAsync<T>(
            this IQueryable<T> query, 
            int page, 
            int take)
        {
            var originalPages = page;

            page--;

            if (page > 0)
                page = page + take;

            var result = new DataCollection<T>()
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };

            if(result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total / take)));
            }

            return result;
        }
    }
}
