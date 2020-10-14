using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace WebLib
{
    public static class DataAccess
    {
        public static IConfiguration Configuration { get; set; }

        public static List<Post> LoadPosts()
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<Post>("SELECT t.* FROM Blog t ORDER BY Id DESC", new DynamicParameters());
            return output.ToList();
        }

        public static List<Post> LoadPosts(string name, string value)
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<Post>($"select * from Blog where {name} = \"{value}\"",
                new DynamicParameters());
            return output.ToList();
        }

        public static List<Post> CustomSql(string sql)
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            var output = cnn.Query<Post>(sql, new DynamicParameters());
            return output.ToList();
        }

        public static void SavePost(Post post)
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Execute(
                "insert into Blog (Title, Content, Slug, PublishDate, IsDraft, Keywords, Category) values (@Title, @Content, @Slug, @PublishDate, @IsDraft, @Keywords, @Category)",
                post);
        }

        public static void DeletePost(string slug)
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Execute(
                $"delete from Blog where Slug = '{slug}'");
        }

        public static void UpdateParam(string name, string value, string slug)
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Execute(
                $"update Blog set {name} = '{value}' where Id = '{slug}'");
        }

        private static string LoadConnectionString()
        {
            return Configuration.GetConnectionString("BlogContext");
        }

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        public static void EditPost(Post post, string slug)
        {
            using var cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Execute(
                $"update Blog set (Title, Content, Slug, PublishDate, IsDraft, Keywords, Category) = (@Title, @Content, @Slug, @PublishDate, @IsDraft, @Keywords, @Category) where Slug = '{slug}';",
                post);
        }
    }
}