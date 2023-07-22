using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using TruyenVN;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Create the service EDM model.
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Author>("Authors");
            modelBuilder.EntitySet<User>("Users");
            modelBuilder.EntitySet<Story>("Stories");
            modelBuilder.EntitySet<Chapter>("Chapters");
            modelBuilder.EntitySet<Category>("Categories");
            modelBuilder.EntitySet<Report>("Reports");
            modelBuilder.EntitySet<Viewed>("Vieweds");
            modelBuilder.EntitySet<StoryCategory>("StoryCategories");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TruyenVNDbContext>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
                            .AddRouteComponents("odata", modelBuilder.GetEdmModel()));
            builder.Services.AddCors();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.MapControllers();

            app.Run();
        }
    }
}