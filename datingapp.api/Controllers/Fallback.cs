using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace datingapp.api.Controllers
{
    [AllowAnonymous]
    public class Fallback :Controller
    {
        public IActionResult Index()
        {
            var result =PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","index.html"),"text/HTML");
            return result;
        }
    }
}