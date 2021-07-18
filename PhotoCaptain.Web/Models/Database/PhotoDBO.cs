namespace PhotoCaptain.Web.Models.Database {
  public class PhotoDBO {
    public int Id { get; set; }
    public string Name { get; set; }
    public string URI { get; set; }
    public string OriginalFileURI { get; set; }
    public string ViewFileURI { get; set; }
    public string ThumbFileURI { get; set; }
  }
}
