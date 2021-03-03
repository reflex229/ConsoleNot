using System.Linq;
using Lib;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [Test, Order(0)]
        public void TheDataExists()
        {
            using var db = new TestNotificationsContext();
            
            db.Notifications.Add(new NotificationModel
            {
                Title = "TestTitle",
                Description = "TestDescription",
                Hours = 1,
                Minutes = 2,
                Seconds = 3,
                Iterations = 12
            });
            db.SaveChanges();
            
            Assert.IsNotEmpty(db.Notifications.ToList());
        }

        [Test, Order(1)]
        public void TheModelExists()
        {
            using var db = new TestNotificationsContext();

            Assert.IsNotNull(db.Notifications.Find("TestTitle"));
        }

        [Test, Order(3)]
        public void RemovingIsWorking()
        {
            using var db = new TestNotificationsContext();
            
            db.Notifications.Add(new NotificationModel
            {
                Title = "TestTitle2",
                Description = "TestDescription",
                Hours = 1,
                Minutes = 2,
                Seconds = 3,
                Iterations = 12
            });
            
            db.Notifications.Remove(db.Notifications.Find("TestTitle2"));
            db.SaveChanges();
            
            Assert.IsNull(db.Notifications.Find("TestTitle2"));
        }
    }
}