using System;

namespace RemiLib.Logic.Common
{
    public interface IProgressTracker
    {
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
            updateProgress(progress);
        }

        private Action<double> updateProgress;
    }

    public class MinMaxProgressTracker : IProgressTracker
    {
        public MinMaxProgressTracker(IProgressTracker otherTracker, double min = 0, double max = 1)
        {
            this.otherTracker = otherTracker;
            this.min = min;
            this.max = max;
        }

        public void UpdateProgress(double progress)
        {
            otherTracker.UpdateProgress(progress / (max - min) + min);
        }

        private readonly IProgressTracker otherTracker;
        private readonly double min;
        private readonly double max;
    }

    public class ChunksProgressTracker : MinMaxProgressTracker
    {
        public ChunksProgressTracker(IProgressTracker otherTracker, int chunks, double min = 0, double max = 1) : base(otherTracker, min, max)
        {
            if (chunks < 0)
                throw new ArgumentException(nameof(chunks) + " cannot be lesser than 0.");

            this.chunks = chunks;
            currentChunk = 0;
        }

        public void Update()
        {
            if(++currentChunk > chunks)
                throw new InvalidOperationException(nameof(Update) + "() method has been called more times than expected.");
            
            UpdateProgress(((double)currentChunk) / chunks); 
        }

        private readonly int chunks;
        private int currentChunk;
    }
}
