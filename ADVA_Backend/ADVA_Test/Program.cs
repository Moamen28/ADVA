using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using Roposityres.Interfaces;
using Roposityres.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Solve Loopin Issue
#region Solving Proplem of Looping of objects To Get All Categorey
builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}); 
#endregion
//Register Services
#region Registe Unit Of Work &  Repositories

builder.Services.AddScoped<IUnitOfWork, unitOfWork>();
builder.Services.AddScoped(typeof(IModelRepository<>), typeof(ModelRepository<>));
#endregion

//Register ADVADbContext
#region Register DBContext
builder.Services.AddDbContext<ADVADbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connStr")));

#endregion
//Register Auto Mapper
#region Redister AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion
#region Angular
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register the Exception Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();
//Enaple Cors For Angular
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
