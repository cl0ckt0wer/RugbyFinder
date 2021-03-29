using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Authentication;
using Xabe.FFmpeg.Downloader;

namespace BlazorApp2.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //FFmpegDownloader.GetLatestVersion(FFmpegVersion.Full);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(kestrelOptions =>
                    {
                        kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
                        {
                            httpsOptions.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12;
                            
                        });
                      
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
