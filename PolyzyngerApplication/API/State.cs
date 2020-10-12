using System;

namespace PolyzyngerApplication
{
    public class State
    {
        public event EventHandler<State> RaiseStatusChange;

        public State(EventHandler<State> handler)
        {
            RaiseStatusChange += handler;

            Stage = Stage.EMPTY;
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
                PreviousStage = _stage;
                _stage = value;
                RaiseStatusChange?.Invoke(this, this);
            }
        }

        public Stage PreviousStage { get; set; }
    }

    public enum Stage
    {
        EMPTY, SCANNING, DOWNLOADING, WAITING, INSTALLING, UPDATING, CLEANING, DONE, ERROR
    }
}