using Amazon.S3;
using Microsoft.Extensions.Options;

namespace PhotoCaptain.Web.Services {
  public class S3PhotoRepository : IPhotoRepository {
    private readonly IAmazonS3 client;
    private readonly string bucket;

    public S3PhotoRepository(IAmazonS3 client, IOptions<S3Configuration> s3Config) {
      this.client = client;
      this.bucket = s3Config.Value.Bucket;
    }

    public string GetURLByURI(string uri) {
      return $"{this.client.Config.DetermineServiceURL()}{this.bucket}/photos/{uri}";
    }
  }
}
