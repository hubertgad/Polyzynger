using System;

namespace PolyzyngerApplication
{
    public class State
    {
        public event EventHandler<State> RaiseStatusChange;

        public State(EventHandler<State> handler)
        {
            RaiseStatusChange += handler;
        }

        private double _downloadProgress;

        public double DownloadProgress
        {
            get
            {
                return _downloadProgress;
            }
            set
            {
                _downloadProgress = value;
                RaiseStatusChange?.Invoke(this, this);
            }
        }

        private Stage _stage;

        public Stage Stage
        {
            get
            {
                return _stage;
            }
            set
            {
                _stage = value;
                RaiseStatusChange?.Invoke(this, this);
            }
        }
    }

    public enum Stage
    {
        Empty, Checking, Downloading, Waiting, Installing, Updating, Cleaning, Done, Error
    }
}