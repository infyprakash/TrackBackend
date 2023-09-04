using TrackBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;



var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrackContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("TrackContext")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000/", "https://localhost", "http://localhost:3000","http://127.0.0.1:3000").AllowAnyHeader().AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/MyStaticFiles",
     OnPrepareResponse = ctx =>
     {
         ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
         ctx.Context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
         // Add any other necessary headers here
     }
});

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();


app.Run();



