// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinTransactionCurrencyAPI;
using NalinTransactionPersistence;

namespace NalinTransactionStoreAndRetrieval
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            string currentDirectory = Directory.GetCurrentDirectory();

            // register persistence
            builder.Services.AddSingleton<IDataPersistence>(sp => new FileDataPersistence(Path.Combine(currentDirectory, "DataStorage")));

            // register transaction persistence
            builder.Services.AddSingleton<ITransactionPersistence>(sp =>
            {
                var persistence = sp.GetRequiredService<IDataPersistence>();
                return new TransactionPersistence(persistence);
            });

            builder.Services.AddSingleton<ICurrencyOperationsAPI>(sp => new CurrencyOperationsAPI());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();


            app.Run();
        }
    }
}
