using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ToolsApp.Utilities
{
    public class AmazonS3Config
    {
        public string ServiceURL { get; set; }
        public bool ForcePathStyle { get; set; }
        public string AuthenticationRegion { get; set; }
    }
    public class SystemCloudflareR2 
    {
        private readonly string bucketName;
        private readonly string r2AccessKeyId;
        private readonly string r2SecretAccessKey;
        private readonly string serviceUrl;
        private AmazonS3Client client;
        public SystemCloudflareR2(string bucketName, string r2AccessKeyId, string r2SecretAccessKey, string serviceUrl)
        {
            this.bucketName = bucketName;
            this.r2AccessKeyId = r2AccessKeyId;
            this.r2SecretAccessKey = r2SecretAccessKey;
            this.serviceUrl = serviceUrl;
            var config = new Amazon.S3.AmazonS3Config
            {
                ServiceURL = serviceUrl,
                ForcePathStyle = true,
                AuthenticationRegion = "auto" 
            };
            client = new AmazonS3Client(r2AccessKeyId, r2SecretAccessKey, config);
        }
        public async Task<string> GetFileAsync(string r2Key)
        {
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = r2Key,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                string url = client.GetPreSignedURL(request);
                return url;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Lỗi S3: Mã lỗi: {e.ErrorCode}, Thông báo: {e.Message}");
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi chung khi get: {e.Message}");
                return "";
            }
        }
        public async Task<bool> UploadFileAsync(string localFilePath, string r2Key)
        {
            try
            {
                if (!File.Exists(localFilePath))
                {
                    Console.WriteLine($"Không tìm thấy file tại đường dẫn: '{localFilePath}'!");
                    return false;
                }
                Console.WriteLine($"Bắt đầu tải lên '{localFilePath}' lên R2 với tên '{r2Key}'...");
                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = r2Key,
                    FilePath = localFilePath,
                    DisablePayloadSigning = true,
                    DisableDefaultChecksumValidation = true
                };
                var response = await client.PutObjectAsync(request);
                if (File.Exists(localFilePath))
                {
                    File.Delete(localFilePath);
                }
                Console.WriteLine($"Tải lên thành công: '{r2Key}' vào bucket '{bucketName}'!");
                return true;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Lỗi S3 khi tải lên: Mã lỗi: {e.ErrorCode}, Thông báo: {e.Message}");
                return false;
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi chung khi tải lên: {e.Message}");
                return false;
            }
        }
        // Bạn cũng có thể thêm phương thức để tải xuống, xóa, liệt kê tệp, v.v.
        public async Task<bool> DeleteFileAsync(string r2Key)
        {
            try
            {
                Console.WriteLine($"Bắt đầu xóa tệp '{r2Key}' từ bucket '{bucketName}'...");
                await client.DeleteObjectAsync(bucketName, r2Key);
                Console.WriteLine($"Xóa tệp thành công: '{r2Key}'");
                return true;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Lỗi S3 khi xóa: Mã lỗi: {e.ErrorCode}, Thông báo: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi chung khi xóa: {e.Message}");
                return false;
            }
        }
    }
}