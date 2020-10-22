﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLib;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Dictionary<string, WebNotificationTimer> _notificationTimers = new Dictionary<string, WebNotificationTimer>();

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
                _notificationTimers.Add(title.Replace(" ", "-"), new WebNotificationTimer(title, description, delay, iterations));
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
                _notificationTimers.Add(title.Replace(" ", "-"), new WebNotificationTimer(title, description, delay, iterations));
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
            _notificationTimers[slug].Stop();
            //RemoteDbUpdate();
            return Redirect("/Home/Notifications");
        }

        public IActionResult Error()
        {
            return View();
        }
        
        public static string NotificationSlug { get; private set; }
    }
}