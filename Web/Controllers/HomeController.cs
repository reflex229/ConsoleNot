using System;
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

        public IActionResult Notifications()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostResult(string title, string description, string delay, string count)
        {
            DataAccess.SavePost(new NotificationModel
            {
                Title = title,
                Description = description,
                Delay = StrToInt(delay),
                Count = StrToInt(count),
                Slug = title.Replace(" ", "-")
            });
            RemoteDbUpdate();
            return Redirect("/Home/Notifications");
        }

        [HttpPost]
        public IActionResult NotificationUpdate(string title, string description, string delay, string count)
        {
            DataAccess.EditNotification(new NotificationModel
                {
                    Title = title,
                    Description = description,
                    Delay = StrToInt(delay),
                    Count = StrToInt(count),
                    Slug = title.Replace(" ", "-")
                },
                NotificationTitle);
            RemoteDbUpdate();
            return Redirect("/Home/Notifications");
        }

        [Route("/Notification/{title}")]
        public IActionResult Notification([FromRoute] string title)
        {
            NotificationTitle = title;
            return View();
        }

        [Route("/NotificationDelete/{slug}")]
        public IActionResult DeletePost([FromRoute] string slug)
        {
            DataAccess.DeleteNotification(slug);
            RemoteDbUpdate();
            return Redirect("/Home/Blog");
        }

        private static void RemoteDbUpdate() => Process.Start("scp",
            "/home/reflex/Shit/C#/ConsoleNotSite/data/blog.db " +
            "192.168.88.40:/home/reflex/data/blog.db"); //TODO: Add a windows implementation

        public static string NotificationTitle { get; private set; }

        private static int StrToInt(string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception)
            {
                return 0; //TODO: Call a notification with error message
            }
        }
    }
}