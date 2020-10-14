using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLib;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostResult(string title, string content, string isDraft, string keywords, string category)
        {
            DataAccess.SavePost(new Post
            {
                Category = category,
                Content = content,
                IsDraft = bool.Parse(isDraft),
                Keywords = keywords,
                Slug = title.Replace(" ", "-"),
                Title = title
            });
            RemoteDbUpdate();
            return Redirect("/Home/Blog");
        }

        [HttpPost]
        public IActionResult PostUpdate(string title, string content, string isDraft, string keywords, string category)
        {
            DataAccess.EditPost(new Post
                {
                    Category = category,
                    Content = content,
                    IsDraft = bool.Parse(isDraft),
                    Keywords = keywords,
                    Slug = title.Replace(" ", "-"),
                    Title = title
                },
                PostTitle);
            RemoteDbUpdate();
            return Redirect("/Home/Blog");
        }

        [Route("/PostPreview/{postTitle}")]
        public IActionResult PostPreview([FromRoute] string postTitle)
        {
            PostTitle = postTitle;
            return View();
        }

        [Route("/Post/{postTitle}")]
        public IActionResult Post([FromRoute] string postTitle)
        {
            PostTitle = postTitle;
            return View();
        }

        [Route("/PostDelete/{slug}")]
        public IActionResult DeletePost([FromRoute] string slug)
        {
            DataAccess.DeletePost(slug);
            RemoteDbUpdate();
            return Redirect("/Home/Blog");
        }

        private static void RemoteDbUpdate() => Process.Start("scp",
            "/home/reflex/Shit/C#/ConsoleNotSite/data/blog.db " +
            "192.168.88.40:/home/reflex/data/blog.db");

        public static string PostTitle { get; private set; }
    }
}