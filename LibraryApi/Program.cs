using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryContext>(o => o.UseSqlite("Data Source=library.db"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();