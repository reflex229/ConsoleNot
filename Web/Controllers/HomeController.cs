﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lib;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Dictionary<string, WebNotificationTimer> _notificationTimers
            = new Dictionary<string, WebNotificationTimer>();

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
                DataAccess.SaveNotification(new NotificationModel
                {
                    Title = title,
                    Description = description, //TODO: Check exception here.
                    Hours = delay[(int) Times.Hours],
                    Minutes = delay[(int) Times.Minutes],
                    Seconds = delay[(int) Times.Seconds],
                    Iterations = Convert.ToInt32(iterations),
                    Slug = title.Replace(" ", "-")
                });
                _notificationTimers.Add(title.Replace(" ", "-"), new WebNotificationTimer(title,
                    description, delay, iterations, title.Replace(" ", "-")));
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
            try
            {
                var delay = new[] {Convert.ToInt32(hours), Convert.ToInt32(minutes), Convert.ToInt32(seconds)};
                DataAccess.EditNotification(new NotificationModel
                    {
                        Title = title,
                        Description = description,
                        Hours = delay[(int) Times.Hours],
                        Minutes = delay[(int) Times.Minutes],
                        Seconds = delay[(int) Times.Seconds],
                        Iterations = Convert.ToInt32(iterations),
                        Slug = title.Replace(" ", "-")
                    },
                    NotificationSlug);
                _notificationTimers.Add(title.Replace(" ", "-"), new WebNotificationTimer(title,
                    description, delay, iterations, title.Replace(" ", "-")));
                return Redirect("/Home/Notifications");
            }
            catch (Exception)
            {
                return Redirect("/Home/Error");
            }
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
            _notificationTimers[slug].Stop(); //TODO: Fix this shit!
            return Redirect("/Home/Notifications");
        }

        public IActionResult Error()
        {
            return View();
        }
        
        public static string NotificationSlug { get; private set; }
    }
}