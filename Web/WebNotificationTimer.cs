using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Web
{
    public class WebNotificationTimer
    {
        private string _title;
        private string _description;
        private int _delay;
        private int _iterations;
        private int _i = 0;
        private static readonly HttpClient client = new HttpClient();

        private Timer _timer;
        
        public WebNotificationTimer(string title, string description, string delay, string iterations)
        {
            _title = title;
            _description = description;
            _delay = Convert.ToInt32(delay);
            _iterations = Convert.ToInt32(iterations);
            
            _timer = new Timer(_delay);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (_i < _iterations)
            {/*
                _i++;
                var client =
                    new RestClient($"http://localhost:5001/Not/{_title}-{_description}-{_delay}-{_iterations}")
                    {
                        Timeout = -1
                    };
                var request = new RestRequest(Method.POST);
                var response = client.Execute(request);*/
                var response = client.PostAsync($"http://localhost:5005/Not/{_title}-{_description}-{_delay}-{_iterations}", null);
            }
            else
            {
                _timer.Stop();
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}