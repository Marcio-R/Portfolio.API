using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PortfolioAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioAPIContext") ?? throw new InvalidOperationException("Connection string 'PortfolioAPIContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(p => p
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod());

app.MapPost("/contacts", async (PortfolioAPIContext context, Contact contact) =>
{
    await context.Contacts.AddAsync(contact);
    await context.SaveChangesAsync();

    return Results.Ok(contact);
})
.WithOpenApi();

app.MapGet("/contacts", async (PortfolioAPIContext context) =>
{
    var contacts = await context.Contacts.ToListAsync();
    return Results.Ok(contacts);
})
.WithOpenApi();

app.Run();

public class Contact
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}