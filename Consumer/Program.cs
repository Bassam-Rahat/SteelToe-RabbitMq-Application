using Content.API.Events;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add Steeltoe Rabbit services
builder.Services.AddRabbitServices(true);
builder.Services.AddRabbitAdmin();
builder.Services.AddRabbitTemplate();
builder.Services.ConfigureRabbitOptions(builder.Configuration);

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add singleton that will process incoming messages
builder.Services.AddSingleton<ContentListenerService>();

// Tell steeltoe about singleton so it can wire up queues with methods to process queues
builder.Services.AddRabbitListeners<ContentListenerService>();

// Add queue to be declared
builder.Services.AddRabbitQueue(new Queue(EventBus.Constants.RECEIVE_AND_CONVERT_QUEUE));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
