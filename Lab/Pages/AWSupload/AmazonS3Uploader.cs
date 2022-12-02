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

        public async Task<byte[]> DownloadFileAsync(string file)
        {
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = file
                };

                IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

                using (var response = await client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
