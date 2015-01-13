using System;
using System.Collections.Generic;

namespace WakeFarmControlR
{
    internal class Timers
    {
        private class Timer
        {
            private bool _isStarted = false;
            private DateTime _startTime = DateTime.MinValue;
            public TimeSpan _measuredTime = TimeSpan.Zero;

            public bool IsStarted
            {
                get
                {
                    return _isStarted;
                }
            }

            public TimeSpan MeasuredTime
            {
                get
                {
                    return _measuredTime + (_isStarted ? (DateTime.UtcNow - _startTime) : TimeSpan.Zero);
                }
            }

            public void Start()
            {
                if (_isStarted)
                {
                    throw new ApplicationException();
                }

                _isStarted = true;
                _startTime = DateTime.UtcNow;
            }

            public void Stop()
            {
                if (!_isStarted)
                {
                    throw new ApplicationException();
                }

                _measuredTime += (DateTime.UtcNow - _startTime);
                _isStarted = false;
                _startTime = DateTime.MinValue;
            }
        }

        private readonly Dictionary<string, Timer> timers = new Dictionary<string, Timer>();

        public string MeasuredTime
        {
            get
            {
                string measuredTime = string.Empty;
                foreach (var timerKey in timers.Keys)
                {
                    var timer = timers[timerKey];
                    measuredTime += string.Format("{0}: {1};", timerKey, timer.MeasuredTime);
                }

                return measuredTime;
            }
        }

        public void ClearTimers()
        {
            timers.Clear();
        }

        public void StartTimer(string timerKey)
        {
            Timer timer;
            if (timers.ContainsKey(timerKey))
            {
                timer = timers[timerKey];
            }
            else
            {
                timer = new Timer();
                timers.Add(timerKey, timer);
            }
            timer.Start();
        }

        public void StopTimer(string timerKey)
        {
            Timer timer;
            if (timers.ContainsKey(timerKey))
            {
                timer = timers[timerKey];
            }
            else
            {
                throw new ApplicationException();
            }
            timer.Stop();
        }
    }

    internal partial class TranslatedCode
    {
        public static readonly Timers Timers = new Timers();
    }
}
