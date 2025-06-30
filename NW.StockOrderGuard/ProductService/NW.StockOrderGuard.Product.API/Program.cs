using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Application.UseCases;
using NW.StockOrderGuard.Product.Infrastructure.Adapters;
using NW.StockOrderGuard.Product.Infrastructure.Repositories;
using MediatR;
using NW.StockOrderGuard.SharedKernel.ExceptionHandling;
using NW.StockOrderGuard.SharedKernel.ExceptionHandling.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using NW.StockOrderGuard.Product.Application.Commands.SyncProducts;
using NW.StockOrderGuard.Product.Application.Commands.UpdateProductStock;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssembly(typeof(NW.StockOrderGuard.Product.Application.Validators.ValidationBehavior<,>).Assembly);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<FakeStoreAdapter>();
builder.Services.AddHttpClient<NW.StockOrderGuard.Product.Infrastructure.IntegrationEvents.HttpEventPublisher>();

builder.Services.AddSingleton<IExternalProductCatalog, FakeStoreAdapter>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<TrackProductCatalogUseCase>();
builder.Services.AddScoped<NW.StockOrderGuard.Product.Application.UseCases.UpdateProductStockUseCase>();
builder.Services.AddScoped<UpdateProductStockCommandHandler>();
builder.Services.AddScoped<NW.StockOrderGuard.Product.Application.Queries.GetAllProductsQueryHandler>();
builder.Services.AddScoped<NW.StockOrderGuard.Product.Application.Queries.GetProductByNameQueryHandler>();
builder.Services.AddScoped<SyncProductsCommandHandler>();
builder.Services.AddAutoMapper(
    typeof(NW.StockOrderGuard.Product.Application.Mapping.ProductMappingProfile).Assembly,
    typeof(NW.StockOrderGuard.Product.Infrastructure.Adapters.FakeStoreMappingProfile).Assembly
);

builder.Services.Configure<NW.StockOrderGuard.Product.Infrastructure.Adapters.FakeStoreApiOptions>(builder.Configuration.GetSection("FakeStoreApi"));
builder.Services.Configure<NW.StockOrderGuard.Product.Infrastructure.IntegrationEvents.StockServiceOptions>(builder.Configuration.GetSection("StockService"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateProductStockCommand>());

builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(NW.StockOrderGuard.Product.Application.Validators.ValidationBehavior<,>));

builder.Services.AddScoped<NW.StockOrderGuard.Product.Application.IntegrationEvents.IEventPublisher, NW.StockOrderGuard.Product.Infrastructure.IntegrationEvents.HttpEventPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandling();

app.UseAuthorization();

app.MapControllers();

app.Run();
