using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;

namespace WebLib
{
    public static class DataAccess
    {

        public static List<NotificationModel> LoadPosts()
        {
            using var cnn = new SQLiteConnection(Connection);
            try
            {
                var output = cnn.Query<NotificationModel>("SELECT t.* FROM Notifications t ORDER BY Id DESC",
                    new DynamicParameters());
                return output.ToList();
            }
            catch (Exception)
            {
                CustomSql("CREATE TABLE \"Notifications\" (\"Id\" INTEGER NOT NULL UNIQUE,\"Title\" TEXT,\"Description\" TEXT,\"Delay\" INTEGER,\"Count\" INTEGER,\"Slug\" TEXT,PRIMARY KEY(\"Id\" AUTOINCREMENT));");
                return null;
            }
        }

        public static List<NotificationModel> LoadNotifications(string name, string value)
        {
            using var cnn = new SQLiteConnection(Connection);
            var output = cnn.Query<NotificationModel>($"select * from Notifications where {name} = \"{value}\"",
                new DynamicParameters());
            return output.ToList();
        }

        public static List<NotificationModel> CustomSql(string sql)
        {
            using var cnn = new SQLiteConnection(Connection);
            var output = cnn.Query<NotificationModel>(sql, new DynamicParameters());
            return output.ToList();
        }

        public static void SavePost(NotificationModel post)
        {
            using var cnn = new SQLiteConnection(Connection);
            cnn.Execute(
                "insert into Notifications (Title, Description, Delay, Count, Slug) values (@Title, @Description, @Delay, @Count, @Slug)",
                post);
        }

        public static void DeleteNotification(string slug)
        {
            using var cnn = new SQLiteConnection(Connection);
            cnn.Execute(
                $"delete from Notifications where Slug = '{slug}'");
        }

        public static void UpdateParam(string name, string value, string slug)
        {
            using var cnn = new SQLiteConnection(Connection);
            cnn.Execute(
                $"update Notifications set {name} = '{value}' where Slug = '{slug}'");
        }

        private static string Connection => $"Data Source={Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}" +
                                            "/consolenot.db;Pooling=true;FailIfMissing=false;Version=3"; //TODO: Windows

        public static void EditNotification(NotificationModel notificationModel, string title)
        {
            using var cnn = new SQLiteConnection(Connection);
            cnn.Execute(
                $"update Notifications set (Title, Description, Delay, Count) = (@Title, @Description, @Delay, @Count) where Title = '{title}';",
                notificationModel);
        }
    }
}