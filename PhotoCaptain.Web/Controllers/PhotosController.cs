using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoCaptain.Web.Data;
using PhotoCaptain.Web.Models.View;
using PhotoCaptain.Web.Services;

namespace PhotoCaptain.Web.Controllers {
  public class PhotosController : Controller {
    private readonly ILogger<PhotosController> logger;
    private readonly PhotoContext photoContext;
    private readonly IPhotoRepository photoRepository;

    public PhotosController(ILogger<PhotosController> logger, PhotoContext photoContext,
      IPhotoRepository photoRepository) {
      this.logger = logger;
      this.photoContext = photoContext;
      this.photoRepository = photoRepository;
    }

    public IActionResult Show(string uri) {
      var dbo = this.photoContext.Photos.FirstOrDefault(x => x.URI == uri);
      var viewModel = new PhotosShowViewModel() {
        Name = dbo.Name,
        URL = photoRepository.GetURLByURI(dbo.ViewFileURI)
      };
      return View(viewModel);
    }
  }
}
