using TodoList.BLL.Configuration;
using TodoList.BLL.DependencyInjection;
using TodoList.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMapper();
var rabbitConfig = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQConfiguration>();
builder.Services.AddRabitBus(rabbitConfig);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
