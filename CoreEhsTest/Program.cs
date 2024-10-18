using CoreEhsTest.Middleware;
using CoreEhsTest.Repositories.Contract;
using CoreEhsTest.Repositories.Implementation;
using CoreEhsTest.Services.Contract;
using CoreEhsTest.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICarModelRepository,CarModelRepository>(provider =>
    new CarModelRepository(builder.Configuration.GetConnectionString("CoreEhsTest_DB")));
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ISalesmanComissionRepository, SalesmanComissionRepository>(provider =>
    new SalesmanComissionRepository(builder.Configuration.GetConnectionString("CoreEhsTest_DB")));
builder.Services.AddScoped<ISalesmanComissionService, SalesmanComissionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
