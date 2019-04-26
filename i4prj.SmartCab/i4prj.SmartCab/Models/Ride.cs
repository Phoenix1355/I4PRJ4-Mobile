using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using i4prj.SmartCab.Interfaces;
using Prism.Mvvm;

namespace i4prj.SmartCab.Models
{
    public class Ride : BindableBase, IRide
    {
        public IAddress Origin { get; set; }
        public IAddress Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ConfirmationDeadline { get; set; }
        public int AmountOfPassengers { get; set; }
        public bool Shared { get; set; }
        public double Price { get; set; }
        public RideStatus Status { get; set; }
        public TimeSpan TimeRemaining { 
            get { 
                return ConfirmationDeadline - DateTime.Now;
            } 
        }

        public int Index { get; set; }

        public enum RideStatus
        {
            LookingForMatch,
            Debited,
            WaitingForAccept,
            Accepted,
            Expired
        }

        private Timer _timer;

        public Ride()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            RaisePropertyChanged(nameof(TimeRemaining));

            if (TimeRemaining.TotalSeconds <= 0) _timer.Stop();
        }
    }
}
