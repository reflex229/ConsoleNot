using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Web.Data
{
    public static class DataAccess
    {
        public static IEnumerable<NotificationModel> Load()
        {
            using var db = new NotificationsContext();
            return db.Notifications.ToList();
        }
        
        public static NotificationModel Load(string title)
        {
            using var db = new NotificationsContext();
            return db.Notifications.Find(title);
        }

        public static void Add(NotificationModel item)
        {
            using var db = new NotificationsContext();
            db.Notifications.Add(item);
            db.SaveChanges();
        }

        public static void Edit(string title, NotificationModel newItem)
        {
            using var db = new NotificationsContext();
            Remove(title);
            Add(newItem);
        }

        public static void Remove(string title)
        {
            using var db = new NotificationsContext(); 
            db.Notifications.Remove(db.Notifications.Find(title));
            db.SaveChanges();
        }
    }
}