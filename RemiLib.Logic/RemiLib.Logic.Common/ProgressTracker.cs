using System;

namespace RemiLib.Logic.Common
{
    public interface IProgressTracker
    {
        /// <summary>
        /// Method for updating progress outside of calculations.
        /// </summary>
        /// <param name="progress">Current progress from range [0, 1]</param>
        void UpdateProgress(double progress);
    }

    public class BasicProgressTracker : IProgressTracker
    {
        public BasicProgressTracker(Action<double> updateProgress)
        {
            this.updateProgress = updateProgress;
        }

        public void UpdateProgress(double progress)
        {
            if (progress < 0)
                throw new ArgumentException("");
            if (progress > 1)
                throw new ArgumentException("");
            if (progress < previousValue)
                throw new ArgumentException("");
            previousValue = progress;
            updateProgress(progress);
        }

        double previousValue = 0;
        readonly Action<double> updateProgress;
    }

    public class PartialProgressTracker : IProgressTracker
    {
        public PartialProgressTracker(IProgressTracker otherTracker, double from = 0, double to = 1)
        {
            if(from >= to)
                throw new ArgumentException($"Parameter {nameof(to)} cannot be lesser than parameter {nameof(from)}.");
            this.otherTracker = otherTracker;
            this.from = from;
            this.to = to;
        }

        public void UpdateProgress(double progress)
        {
            otherTracker.UpdateProgress(progress * (to - from) + from);
        }

        private readonly IProgressTracker otherTracker;
        private readonly double from;
        private readonly double to;
    }

    public class ChunksProgressTracker : PartialProgressTracker
    {
        public ChunksProgressTracker(IProgressTracker otherTracker, int chunks, double from = 0, double to = 1) : base(otherTracker, from, to)
        {
            if (chunks < 0)
                throw new ArgumentException($"Parameter {nameof(chunks)} cannot be lesser than 0.");

            this.chunks = chunks;
            currentChunk = 0;
        }

        public void Update()
        {
            if(++currentChunk > chunks)
                throw new InvalidOperationException($"{nameof(Update)}() method has been called more times than expected.");
            
            UpdateProgress(((double)currentChunk) / chunks); 
        }

        private readonly int chunks;
        private int currentChunk;
    }
}
