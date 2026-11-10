using System;
using System.Collections.Generic;
using System.Text;

namespace Week_7_Album
{
    internal class Album
    {
        private string _name;
        private DateTime _releaseDate;
        private int _songs;
        private TimeSpan _duration;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; }
        }

        public int Songs
        {
            get { return _songs; }
            set { _songs = value; }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

    }
}
