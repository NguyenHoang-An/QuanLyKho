using Client.Components;
using Client.Data;
using Client.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientsContext")));



builder.Services.AddScoped<IDonViTinhService, DonViTinhService>();
builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<INhaCungCapService,NhaCungCapService>();
builder.Services.AddScoped<IKhoService,KhoService>();
builder.Services.AddScoped<IKhoUserService, KhoUserService>();
builder.Services.AddScoped<ILoaiSanPhamService, LoaiSanPhamService>();
builder.Services.AddScoped<INhapKhoService,NhapKhoService>();
builder.Services.AddScoped<IXuatKhoService, XuatKhoService>();
builder.Services.AddScoped<IThongKeService,ThongKeService>();
builder.Services.AddQuickGridEntityFrameworkAdapter();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
