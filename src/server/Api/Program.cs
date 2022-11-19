using Api.Chat;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionString = config.GetConnectionString("Postgres")
	?? throw new InvalidOperationException("Connection string not passed");

services.AddControllers();
services.AddSignalR();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplicationDbContext(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();