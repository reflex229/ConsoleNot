using System.ComponentModel.DataAnnotations;

namespace Lib
{
    public class NotificationModel
    {
        [Key]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Iterations { get; set; }
    }
}