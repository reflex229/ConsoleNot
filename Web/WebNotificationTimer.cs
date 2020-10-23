using System;
using System.Net.Http;
using System.Timers;
using WebLib;

namespace Web
{
    public class WebNotificationTimer
    {
        private string _title;
        private string _description;
        private int _iterations;
        private int _i;
        private string _slug; //MB Useless
        private static readonly HttpClient client = new HttpClient();

        private Timer _timer;
        
        public WebNotificationTimer(string title, string description, string delay, string iterations, string slug)
        {
            _title = title;
            _slug = slug;
            _description = description;
            var delay1 = Convert.ToInt32(delay);
            _iterations = Convert.ToInt32(iterations);
            
            _timer = new Timer(delay1*1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (_i < _iterations)
            {
                _i++;
                var response = client.PostAsync($"http://localhost:5005/Not/{_title}-{_description}", null); //TODO: Request ip from user
            }
            else
            {
                _timer.Stop();
                DataAccess.DeleteNotification(_slug);
                //TODO: Delete notification from dictionary
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}