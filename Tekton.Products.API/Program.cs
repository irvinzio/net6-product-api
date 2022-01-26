using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tektok.Infrastructure.Extensions;
using Tektok.Infrastructure.MockApi;
using Tektok.Infrastructure.Repositories.MockApiRepo;
using Tekton.API.Filters;
using Tekton.API.Middlewares;
using Tekton.Data.Context;
using Tekton.Infrastructure.Interfaces;
using Tekton.Infrastructure.Repositories;
using Tekton.Service;
using Tekton.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TektonContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings").GetSection("DefaultContext").Value);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMockApiRepository, MockApi>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new ModelValidation());
});

builder.Services.AddLazyCache();


var app = builder.Build();

app.InitDatabase().GetAwaiter().GetResult();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ResponseTimeMiddleware>();

app.UseExceptionHandler(
    options =>
    {
        options.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            if (null != exceptionObject)
            {
                var errorMessage = $"{exceptionObject.Error.Message}";
                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
            }
        });
    }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
