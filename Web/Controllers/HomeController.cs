using System;
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
        public IActionResult PostResult(string title, string description, string delay, string iterations)
        {
            try
            {
                DataAccess.SaveNotification(new NotificationModel
                {
                    Title = title,
                    Description = description,
                    Delay = Convert.ToInt32(delay),
                    Iterations = Convert.ToInt32(iterations),
                    Slug = title.Replace(" ", "-")
                });
                return Redirect("/Home/Notifications");
            }
            catch (NullReferenceException)
            {
                return Redirect("/Home/FieldsAreRequired");
            }
            //RemoteDbUpdate();
        }

        [HttpPost]
        public IActionResult NotificationUpdate(string title, string description, string delay, string iterations)
        {
            try
            {
                DataAccess.EditNotification(new NotificationModel
                    {
                        Title = title,
                        Description = description,
                        Delay = Convert.ToInt32(delay),
                        Iterations = Convert.ToInt32(iterations),
                        Slug = title.Replace(" ", "-")
                    },
                    NotificationSlug);
                return Redirect("/Home/Notifications");
            }
            catch (NullReferenceException)
            {
                return Redirect("/Home/FieldsAreRequired");
            }
            //RemoteDbUpdate();
        }

        [Route("/Notification/{slug}")]
        public IActionResult Notification([FromRoute] string slug)
        {
            NotificationSlug = slug;
            return View();
        }

        [Route("/NotificationDelete/{slug}")]
        public IActionResult DeletePost([FromRoute] string slug)
        {
            DataAccess.DeleteNotification(slug);
            //RemoteDbUpdate();
            return Redirect("/Home/Notifications");
        }

        public IActionResult Error()
        {
            return View();
        }

        /*private static void RemoteDbUpdate() => Process.Start("scp",
            "/home/reflex/Shit/C#/ConsoleNotSite/data/blog.db " +
            "192.168.88.40:/home/reflex/data/blog.db");*/

        public static string NotificationTitle { get; private set; }
        public static string NotificationSlug { get; private set; }
    }
}