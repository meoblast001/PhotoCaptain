using Microsoft.EntityFrameworkCore;
using PhotoCaptain.Web.Models.Database;

namespace PhotoCaptain.Web.Data {
  public class PhotoContext : DbContext {
    public PhotoContext(DbContextOptions<PhotoContext> options) : base(options) {
    }

    public DbSet<PhotoDBO> Photos { get; set; }
  }
}
