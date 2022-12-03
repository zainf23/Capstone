using Amazon.S3.Model;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Net;

namespace Lab.Pages.AWSupload
{
    public class AmazonS3Uploader
    {
        public string getUrl()
        {
            string bucketURL = "https://hartmanbucket.s3.amazonaws.com/";
            return bucketURL;
        }

        private string bucketName = "hartmanbucket";

        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = bucketName,
                        ContentType = file.ContentType
                    };

                    IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
                    TransferUtility utility = new TransferUtility(client);
                    

                    await utility.UploadAsync(uploadRequest);

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
