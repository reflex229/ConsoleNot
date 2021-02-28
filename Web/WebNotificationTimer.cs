using System;
using System.Net.Http;
using System.Timers;
using Web.Data;
using static Web.Program;

namespace Web
{
    public class WebNotificationTimer
    {
        private string _title;
        private string _description;
        private int _iterations;
        private int _i;
        private static readonly HttpClient Client = new HttpClient();

        private Timer _timer;
        
        public WebNotificationTimer(string title, string description, int[] delay, string iterations)
        {
            _title = title;
            _description = description;
            _iterations = Convert.ToInt32(iterations);

            _timer = new Timer(delay[0] * 3600000 + delay[1] * 60000 + delay[2] * 1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (_i < _iterations)
            {
                _i++;
                Client.PostAsync($"http://{Ip}:{Port}/Not/{_title}-{_description}", null);
            }
            else
            {
                _timer.Stop();
                DataAccess.Remove(_title);
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}