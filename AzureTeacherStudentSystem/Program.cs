using Azure.Identity;
using AzureTeacherStudentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(builder.Configuration["VaultUri"]);
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration["Develop"]));

builder.Services.AddRazorPages();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["StorageAccountConnectionString"]);
    clientBuilder.AddQueueServiceClient(builder.Configuration["StorageAccountConnectionString"]);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
