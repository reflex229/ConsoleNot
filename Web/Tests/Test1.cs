using Lib;
using NUnit.Framework;
using Web.Data;

namespace Web.Tests
{
    public class Tests
    {
        [Test, Order(0)]
        public void TheDataExists()
        {
            DataAccess.Add(new NotificationModel
            {
                Title = "TestTitle",
                Description = "TestDescription",
                Hours = 1,
                Minutes = 2,
                Seconds = 3,
                Iterations = 12
            });

            Assert.IsNotEmpty(DataAccess.Load());
        }

        [Test, Order(1)]
        public void TheModelExists()
        {
            Assert.IsNotNull(DataAccess.Load("TestTitle"));
        }

        [Test, Order(2)]
        public void EditingIsWorking()
        {
            DataAccess.Edit("TestTitle",
                new NotificationModel
                {
                    Title = "TestTitle2",
                    Description = "TestDescription2",
                    Hours = 3,
                    Minutes = 5,
                    Seconds = 9,
                    Iterations = 32
                });

            Assert.IsNotNull(DataAccess.Load("TestTitle2"));
        }
        
        [Test, Order(3)]
        public void RemovingIsWorking()
        {
            DataAccess.Add(new NotificationModel
            {
                Title = "TestTitle",
                Description = "TestDescription",
                Hours = 1,
                Minutes = 2,
                Seconds = 3,
                Iterations = 12
            });
            
            DataAccess.Remove("TestTitle");

            Assert.IsNull(DataAccess.Load("TestTitle"));
        }
    }
}