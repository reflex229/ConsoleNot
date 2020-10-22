using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Dapper;

namespace WebLib
{
    public static class DataAccess
    {

        public static List<NotificationModel> LoadNotifications()
        {
            using var cnn = Conn();
            try //TODO: Add checking to all of methods
            {
                var output = cnn.Query<NotificationModel>("SELECT t.* FROM Notifications t ORDER BY Id DESC",
                    new DynamicParameters());
                return output.ToList();
            }
            catch (Exception)
            {
                CustomSql("CREATE TABLE \"Notifications\" (\"Id\" INTEGER NOT NULL UNIQUE,\"Title\" TEXT,\"Description\" TEXT,\"Delay\" INTEGER,\"Iterations\" INTEGER,\"Slug\" TEXT,PRIMARY KEY(\"Id\" AUTOINCREMENT));");
                return null;
            }
        }

        public static List<NotificationModel> LoadNotifications(string name, string value)
        {
            using var cnn = Conn();
            var output = cnn.Query<NotificationModel>($"select * from Notifications where {name} = \"{value}\"",
                new DynamicParameters());
            return output.ToList();
        }

        public static List<NotificationModel> CustomSql(string sql)
        {
            using var cnn = Conn();
            var output = cnn.Query<NotificationModel>(sql, new DynamicParameters());
            return output.ToList();
        }

        public static void SaveNotification(NotificationModel notification)
        {
            using var cnn = Conn();
            cnn.Execute(
                "insert into Notifications (Title, Description, Delay, Iterations, Slug) values (@Title, @Description, @Delay, @Iterations, @Slug)",
                notification);
        }

        public static void DeleteNotification(string slug)
        {
            using var cnn = Conn();
            cnn.Execute(
                $"delete from Notifications where Slug = '{slug}'");
        }

        public static void UpdateParam(string name, string value, string slug)
        {
            using var cnn = Conn();
            cnn.Execute(
                $"update Notifications set {name} = '{value}' where Slug = '{slug}'");
        }

        private static SQLiteConnection Conn()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new SQLiteConnection($"Data Source={Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}" +
                       @"\consolenot.db;Pooling=true;FailIfMissing=false;Version=3");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new SQLiteConnection($"Data Source={Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}" +
                       "/consolenot.db;Pooling=true;FailIfMissing=false;Version=3");
            }
            else
            {
                throw new Exception("Your system is not supported :(");
            }
        }

        public static void EditNotification(NotificationModel notificationModel, string slug)
        {
            using var cnn = Conn();
            cnn.Execute(
                $"update Notifications set (Title, Description, Delay, Iterations, Slug) = (@Title, @Description, @Delay, @Iterations, @Slug) where Slug = '{slug}'",
                notificationModel);
        }
    }
}