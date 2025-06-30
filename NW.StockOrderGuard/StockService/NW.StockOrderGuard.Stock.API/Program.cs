using MediatR;
using NW.StockOrderGuard.Stock.Application.Commands.SyncProducts;
using NW.StockOrderGuard.Stock.Application.Contracts;
using NW.StockOrderGuard.Stock.Application.Queries.GetAllOrders;
using NW.StockOrderGuard.Stock.Infrastructure.Repositories;
using NW.StockOrderGuard.SharedKernel.ExceptionHandling.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Servisleri container'a ekle.

builder.Services.AddControllers();
// Swagger/OpenAPI yapılandırması hakkında daha fazla bilgi için: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SyncProductsCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllOrdersQueryHandler>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandling();

app.Run();
