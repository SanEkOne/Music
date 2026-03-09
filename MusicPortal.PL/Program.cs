using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.DTO;

namespace MusicPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddMusicPortalContext(connection);
            builder.Services.AddUnitOfWorkService();
            builder.Services.AddTransient<IEntityService<MusicDTO>, MusicService>();
            builder.Services.AddScoped<IUserService<UserDTO>, UserService>();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Music}/{action=Index}/{id?}"); 


            app.Run();
        }
    }
}
