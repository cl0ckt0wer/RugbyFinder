using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RugbyFinder.Shared.Model.Return;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xabe.FFmpeg;

namespace RugbyFinder.Server.Controllers
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-5.0
    [ApiController]
    [Route("[controller]")]
    public class FilesaveController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<FilesaveController> logger;
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public FilesaveController(IWebHostEnvironment env,
            ILogger<FilesaveController> logger, IConfiguration configuration)
        {
            this.env = env;
            this.logger = logger;
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }

        [HttpPost]
        public async Task<ActionResult<IList<UploadResult>>> PostFile(
            [FromForm] IEnumerable<IFormFile> files)
        {
            var maxAllowedFiles = 1;
            long maxFileSize = 1024 * 1024 * 15;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            IList<UploadResult> uploadResults = new List<UploadResult>();

            foreach (var file in files)
            {
                logger.LogInformation($"Processing upload of file: {file.FileName}, content: {file.ContentType}");
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);
             

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        logger.LogInformation("{FileName} length is 0",
                            trustedFileNameForDisplay);
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        logger.LogInformation("{FileName} of {Length} bytes is " +
                            "larger than the limit of {Limit} bytes",
                            trustedFileNameForDisplay, file.Length, maxFileSize);
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            trustedFileNameForFileStorage = Path.GetRandomFileName();
                            var temppath = Path.Combine(Path.GetTempPath()
                               , "unsafe_uploads",
                                trustedFileNameForFileStorage);
                            Directory.CreateDirectory(Path.GetDirectoryName(temppath));
                            using MemoryStream ms = new();
                            await file.CopyToAsync(ms);
                            await System.IO.File.WriteAllBytesAsync(temppath, ms.ToArray());
                            logger.LogInformation("{FileName} saved at {Path}",
                                trustedFileNameForDisplay, temppath);
                      
                            logger.LogInformation($"Beginning Transcode");
                          
                            //transcode the uploaded video file into webm - vp9
                            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(temppath) ;
                            string output = Path.Combine(Environment.CurrentDirectory, @"wwwroot\videos", Path.ChangeExtension(Path.GetFileName(temppath), "webm"));
                            Directory.CreateDirectory(Path.GetDirectoryName(output));
                            logger.LogInformation($"output path: {output}");
                            
                            IStream videoStream = mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.vp9);
                            IStream audioStream = mediaInfo.AudioStreams.FirstOrDefault()?.SetCodec(AudioCodec.libvorbis);
                            logger.LogInformation($"video codec: {videoStream.Codec}");

                            var result = FFmpeg.Conversions.New().AddStream(videoStream, audioStream).SetOutput(output).AddParameter("-vf scale=720:-1");
                            /*only use hardware acceleration in prod, i think it acutally takes longer. */
                            //TODO:configuration file for hardware acceleration
#if !DEBUG
                             //result = result.UseHardwareAcceleration(HardwareAccelerator.auto, Enum.Parse<VideoCodec>(videoStream.Codec), VideoCodec.vp9);
#endif
                            logger.LogInformation($"{result.Build()}");
                            var end = await result.Start();

                            logger.LogInformation($"Transcode duration: {end.Duration}");
                            logger.LogInformation($"args: {end.Arguments}");
                            var sql = "Proc_UpsertRuggerVideo";
                            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                            {
                                await conn.ExecuteAsync(sql, new { Key = untrustedFileName, filename = Path.GetFileName(output) }, commandType: CommandType.StoredProcedure);

                            }
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = Path.GetFileName(output);
                        }
                        catch (IOException ex)
                        {
                            logger.LogError("{FileName} error on upload: {Message}",
                                trustedFileNameForDisplay, ex.Message);
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    logger.LogInformation("{FileName} not uploaded because the " +
                        "request exceeded the allowed {Count} of files",
                        trustedFileNameForDisplay, maxAllowedFiles);
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }

            return new CreatedResult(resourcePath, uploadResults);
        }
    }
}
