using MyCRMNoSQL.Service;
using MyCRMNoSQL.Service.Interfaces;
using MyCRMNoSQL.Repository;
using MyCRMNoSQL.Repository.Interfaces;
using MyCRMNoSQL.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IUpcomingTaskRepository, UpcomingTaskRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IClientActivityRepository, ClientActivityRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IClientActivityService, ClientActivityService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUpcomingTaskService, UpcomingTaskService>();
builder.Services.AddScoped<IExtension, Extension>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/User/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{businessId?}/{userId?}");

app.Run();
