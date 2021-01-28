using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KeyVaultDemo.Contexts;
using KeyVaultDemo.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace KeyVaultDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase<DemoContext>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    if (context.HostingEnvironment.IsProduction())
                    {
                        var secretClient = new SecretClient(
                            new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                            new DefaultAzureCredential());

                        config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                    }

                    if (context.HostingEnvironment.IsEnvironment("OutsideAzure"))
                    {
                        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                        store.Open(OpenFlags.ReadOnly);
                        var certs = store.Certificates.Find(X509FindType.FindByThumbprint, builtConfig["AzureADCertThumbprint"], false);

                        var directoryId = builtConfig["AzureADDirectoryId"];
                        var appId = builtConfig["AzureADApplicationId"];

                        config.AddAzureKeyVault(
                            new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                            new ClientCertificateCredential(directoryId, appId, certs.OfType<X509Certificate2>().Single()),
                            new KeyVaultSecretManager());

                        store.Close();
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
