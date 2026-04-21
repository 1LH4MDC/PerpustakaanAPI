var builder = WebApplication.CreateBuilder(args);

// ── Controllers ───────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ── CORS ──────────────────────────────────────────────────────────────────
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// ── Swagger ───────────────────────────────────────────────────────────────
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ── HTTP Pipeline ─────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();
app.MapControllers();
app.Run();