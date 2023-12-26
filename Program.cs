using AzureFunWithDapper.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Input;

var host = new HostBuilder()
    //.ConfigureAppConfiguration(builder =>
    //{
    //    string cs = Environment.GetEnvironmentVariable("ConnectionString");
    //    builder.AddAzureAppConfiguration(cs);
    //})
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddSingleton<DapperContext>();
        //s.AddScoped<ICompany, QueryHandler>();
    })
    .Build();

host.Run();
