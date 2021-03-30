using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestApiSelami.LieuxData;
using RestApiSelami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace RestApiSelami
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var connectionString = GetSqlServerConnectionString();
            services.AddDbContextPool<LieuxContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ILieuxData, SqlLieuxData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LieuxContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            dataContext.Database.Migrate();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
   
    public static SqlConnectionStringBuilder GetSqlServerConnectionString()
        {
            // [START cloud_sql_sqlserver_dotnet_ado_connection_tcp]
            // Equivalent connection string:
            // "User Id=<DB_USER>;Password=<DB_PASS>;Server=<DB_HOST>;Database=<DB_NAME>;"
            var connectionString = new SqlConnectionStringBuilder()
            {
                // Remember - storing secrets in plain text is potentially unsafe. Consider using
                // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                // secrets secret.
                DataSource = Environment.GetEnvironmentVariable("DB_HOST"),     // e.g. '127.0.0.1'
                // Set Host to 'cloudsql' when deploying to App Engine Flexible environment
                UserID = Environment.GetEnvironmentVariable("DB_USER"),         // e.g. 'my-db-user'
                Password = Environment.GetEnvironmentVariable("DB_PASS"),       // e.g. 'my-db-password'
                InitialCatalog = Environment.GetEnvironmentVariable("DB_NAME"), // e.g. 'my-database'

                // The Cloud SQL proxy provides encryption between the proxy and instance
                Encrypt = false,
            };
            connectionString.Pooling = true;
            // [START_EXCLUDE]
            // The values set here are for demonstration purposes only. You 
            // should set these values to what works best for your application.
            // [START cloud_sql_sqlserver_dotnet_ado_limit]
            // MaximumPoolSize sets maximum number of connections allowed in the pool.
            connectionString.MaxPoolSize = 5;
            // MinimumPoolSize sets the minimum number of connections in the pool.
            connectionString.MinPoolSize = 0;
            // [END cloud_sql_sqlserver_dotnet_ado_limit]
            // [START cloud_sql_sqlserver_dotnet_ado_timeout]
            // ConnectionTimeout sets the time to wait (in seconds) while
            // trying to establish a connection before terminating the attempt.
            connectionString.ConnectTimeout = 15;
            // [END cloud_sql_sqlserver_dotnet_ado_timeout]
            // [START cloud_sql_sqlserver_dotnet_ado_lifetime]
            // ADO.NET connection pooler removes a connection
            // from the pool after it's been idle for approximately
            // 4-8 minutes, or if the pooler detects that the
            // connection with the server no longer exists.
            // [END cloud_sql_sqlserver_dotnet_ado_lifetime]
            // [END_EXCLUDE]
            return connectionString;
            // [END cloud_sql_sqlserver_dotnet_ado_connection_tcp]
        }
}
