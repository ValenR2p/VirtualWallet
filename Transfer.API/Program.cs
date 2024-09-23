using Microsoft.AspNetCore.Mvc;
using TransferApplication.Interfaces;
using TransferApplication.UseCases;
using TransferInfrastructure.Command;
using TransferInfrastructure.Persistence;
using TransferInfrastructure.Query;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {

            var errorMessages = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var message = string.Join(" ", errorMessages);

            return new BadRequestObjectResult(new { message });
        };
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TransferContext>();

builder.Services.AddScoped<ITransferCommand, TransferCommand>();
builder.Services.AddScoped<ITransferQuery, TransferQuery>();
builder.Services.AddScoped<ITransferServices, TransferServices>();


var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
