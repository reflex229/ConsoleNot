using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lib;
using Web.Data;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Dictionary<string, WebNotificationTimer> NotificationTimers => notificationTimers;

        private static Dictionary<string, WebNotificationTimer> notificationTimers = new Dictionary<string, WebNotificationTimer>();

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
        public IActionResult PostResult(
            string title, string description, string hours, string minutes, string seconds, string iterations)
        {
            try
            {
                var delay = new[] {Convert.ToInt32(hours), Convert.ToInt32(minutes), Convert.ToInt32(seconds)};
                DataAccess.Add(new NotificationModel
                {
                    Title = title,
                    Description = description,
                    Hours = delay[(int) Times.Hours],
                    Minutes = delay[(int) Times.Minutes],
                    Seconds = delay[(int) Times.Seconds],
                    Iterations = Convert.ToInt32(iterations),
                });
                NotificationTimers.Add(DataAccess.Unique(title), new WebNotificationTimer(title,
                    description, delay, iterations));
                return Redirect("/Home/Notifications");
            }
            catch (Exception)
            {
                return Redirect("/Home/Error");
            }
        }
        
        [HttpPost]
        public IActionResult NotificationUpdate(
            string title, string description, string hours, string minutes, string seconds, string iterations)
        {
            var delay = new[] {Convert.ToInt32(hours), Convert.ToInt32(minutes), Convert.ToInt32(seconds)};
                DataAccess.Edit(NotificationTitle,new NotificationModel
                    {
                        Title = title,
                        Description = description,
                        Hours = delay[(int) Times.Hours],
                        Minutes = delay[(int) Times.Minutes],
                        Seconds = delay[(int) Times.Seconds],
                        Iterations = Convert.ToInt32(iterations),
                    });
                NotificationTimers[NotificationTitle].Stop();
                NotificationTimers[DataAccess.Unique(title)] = new WebNotificationTimer(DataAccess.Unique(title),
                    description, delay, iterations);
                return Redirect("/Home/Notifications");
        }

        [Route("/Notification/{title}")]
        public IActionResult Notification([FromRoute] string title)
        {
            NotificationTitle = title;
            return View();
        }

        [Route("/NotificationDelete/{title}")]
        public IActionResult DeletePost([FromRoute] string title)
        {
            DataAccess.Remove(title);
            NotificationTimers[title].Stop();
            return Redirect("/Home/Notifications");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ErrorUsr()
        {
            return View();
        }
        
        public static string NotificationTitle { get; private set; }
    }
}