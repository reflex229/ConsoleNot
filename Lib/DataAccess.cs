using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Dapper;

namespace Lib
{
    public static class DataAccess
    {
        static DataAccess()
        {
            using var cnn = Conn();
            try
            {
                cnn.Query<NotificationModel>("SELECT t.* FROM Notifications t ORDER BY Id DESC", 
                    new DynamicParameters());
            }
            catch (Exception)
            {
                CustomSql("CREATE TABLE \"Notifications\" (\"Id\" INTEGER NOT NULL UNIQUE, \"Title\" TEXT,\"Description\" TEXT,\"Hours\" INTEGER,\"Minutes\" INTEGER,\"Seconds\" INTEGER,\"Iterations\" INTEGER,PRIMARY KEY(\"Id\" AUTOINCREMENT));");
            }
        }

        public static List<NotificationModel> LoadNotifications()
        {
            using var cnn = Conn();
            var output = cnn.Query<NotificationModel>("SELECT t.* FROM Notifications t ORDER BY Id DESC",
                    new DynamicParameters());
            return output.ToList();
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
                "insert into Notifications (Title, Description, Hours, Minutes, Seconds, Iterations) values (@Title, @Description, @Hours, @Minutes, @Seconds, @Iterations)",
                notification);
        }

        public static void DeleteNotification(string title)
        {
            using var cnn = Conn();
            cnn.Execute(
                $"delete from Notifications where Title = '{title}'");
        }

        public static void UpdateParam(string name, string value, string title)
        {
            using var cnn = Conn();
            cnn.Execute(
                $"update Notifications set {name} = '{value}' where Title = '{title}'");
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

        public static void EditNotification(NotificationModel notificationModel, string title)
        {
            using var cnn = Conn();
            cnn.Execute(
                $"update Notifications set (Title, Description, Hours, Minutes, Seconds, Iterations) = (@Title, @Description, @Hours, @Minutes, @Seconds, @Iterations) where Title = '{title}'",
                notificationModel);
        }
    }
}