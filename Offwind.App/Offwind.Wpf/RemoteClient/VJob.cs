using System;
using Offwind.Common;
using Offwind.Infrastructure.Models;

namespace Offwind.RemoteClient
{
    public sealed class VJob : BaseViewModel
    {
        public int Number
        {
            get { return GetProperty<int>("Number"); }
            set { SetProperty("Number", value); }
        }


        public Guid Id
        {
            get { return GetProperty<Guid>("Id"); }
            set { SetProperty("Id", value); }
        }


        public Guid ProjectId
        {
            get { return GetProperty<Guid>("ProjectId"); }
            set { SetProperty("ProjectId", value); }
        }


        public string ProjectName
        {
            get { return GetProperty<string>("ProjectName"); }
            set { SetProperty("ProjectName", value); }
        }


        public DateTime Started
        {
            get { return GetProperty<DateTime>("Started"); }
            set { SetProperty("Started", value); }
        }


        public TimeSpan Duration
        {
            get { return GetProperty<TimeSpan>("Duration"); }
            set { SetProperty("Duration", value); }
        }


        public JobState State
        {
            get { return GetProperty<JobState>("State"); }
            set { SetPropertyEnum("State", value); }
        }


        public JobResult Result
        {
            get { return GetProperty<JobResult>("Result"); }
            set { SetPropertyEnum("Result", value); }
        }


        public string Log
        {
            get { return GetProperty<string>("Log"); }
            set { SetProperty("Log", value); }
        }

    }
}
