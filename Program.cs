using Project_Backend_2024.DTO.TechStack;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();
app.MapGet("/health", () => "ok");
app.MapControllers();
app.Run();