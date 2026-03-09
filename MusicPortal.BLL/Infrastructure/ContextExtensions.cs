using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;

namespace MusicPortal.BLL.Infrastructure
{
    public static class ContextExtensions
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string? connection)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(connection));
        }
    }
}
