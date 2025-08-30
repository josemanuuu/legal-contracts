using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LegalContractsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

// SWAGGER
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// MIGRATIONS
using (IServiceScope? scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LegalContractsDbContext>();
    db.Database.Migrate();
}

app.MapControllers();
app.Run();