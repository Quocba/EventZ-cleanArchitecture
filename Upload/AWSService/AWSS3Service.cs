using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DAL.MiddleWare
{
    public class AWSS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public AWSS3Service(IConfiguration configuration)
        {
            var accessKey = configuration["S3:AccessKeyId"];
            var secretKey = configuration["S3:SecretAccessKey"];
            var endpoint = configuration["S3:Endpoint"];
            _bucketName = configuration["S3:Bucket"]!;

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("AWS S3 credentials are missing in appsettings.json");
            }

            var config = new AmazonS3Config
            {
                ServiceURL = endpoint,
                ForcePathStyle = true
            };

            _s3Client = new AmazonS3Client(accessKey, secretKey, config);
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("Image is empty", nameof(image));
            }

            if (_s3Client == null)
            {
                throw new InvalidOperationException("S3 client is not initialized");
            }

            var fileName = $"EventZ/{Guid.NewGuid()}_{image.FileName}";

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                stream.Position = 0;

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = fileName,
                    BucketName = _bucketName,
                    ContentType = image.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                var transferUtility = new TransferUtility(_s3Client);
                await transferUtility.UploadAsync(uploadRequest);

                return $"https://{_bucketName}.sgp1.digitaloceanspaces.com/{fileName}";
            }
        }
    }
}
