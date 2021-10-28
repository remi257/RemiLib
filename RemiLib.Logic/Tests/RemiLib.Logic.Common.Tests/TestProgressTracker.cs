using System;
using NUnit.Framework;



namespace RemiLib.Logic.Common.Tests
{
    public class TestProgressBar
    {
        public double Value
        {
            get => currentValue;
            set
            {
                if (value < min)
                    throw new ArgumentException("");
                if (value > max)
                    throw new ArgumentException("");
                if (value < currentValue)
                    throw new ArgumentException("");
                currentValue = value;
            }
        }

        public static readonly double min = 0;
        public static readonly double max = 100;
        double currentValue = 0;

    }
    public class TestProgressTracker
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            updateProgress = p => progressBar.Value = TestProgressBar.max * p;
        }

        [SetUp]
        public void SetUp()
        {
            progressBar = new TestProgressBar();
        }

        TestProgressBar progressBar;
        Action<double> updateProgress;
        double[] array = { 0.0, 0.2, 0.4, 0.6, 0.8, 1.0 };

        [Test]
        public void TestBasicTracker()
        {
            BasicProgressTracker basicProgressTracker = new BasicProgressTracker(updateProgress);
            foreach(double p in array)
            {
                Assert.DoesNotThrow(() => basicProgressTracker.UpdateProgress(p));
            }
            Assert.AreEqual(TestProgressBar.max, progressBar.Value);
        }
        
        [Test]
        public void TestBasicTracker2()
        {
            BasicProgressTracker basicProgressTracker = new BasicProgressTracker(updateProgress);

            Assert.Throws<ArgumentException>(() => basicProgressTracker.UpdateProgress(-0.01));
            Assert.Throws<ArgumentException>(() => basicProgressTracker.UpdateProgress(1.01));
            Assert.DoesNotThrow(() => basicProgressTracker.UpdateProgress(0.5));
            Assert.Throws<ArgumentException>(() => basicProgressTracker.UpdateProgress(0.4));
        }

        [Test]
        public void TestPartialTracker()
        {
            BasicProgressTracker basicProgressTracker = new BasicProgressTracker(updateProgress);
            PartialProgressTracker partialProgressTracker1 = new PartialProgressTracker(basicProgressTracker, 0, 0.5);

            foreach(double p in array)
                {
                    Assert.DoesNotThrow(() => partialProgressTracker1.UpdateProgress(p));
            }

            PartialProgressTracker partialProgressTracker2 = new PartialProgressTracker(basicProgressTracker, 0.5, 1);
            foreach(double p in array)
            {
                Assert.DoesNotThrow(() => partialProgressTracker2.UpdateProgress(p));
            }
            Assert.AreEqual(TestProgressBar.max, progressBar.Value);
        }

        [Test]
        public void TestNestedTracker()
        {
            BasicProgressTracker basicProgressTracker = new BasicProgressTracker(updateProgress);
            PartialProgressTracker partialProgressTracker1 = new PartialProgressTracker(basicProgressTracker, 0, 0.5);
            PartialProgressTracker partialProgressTracker1_1 = new PartialProgressTracker(partialProgressTracker1, 0, 0.5);
            PartialProgressTracker partialProgressTracker1_2 = new PartialProgressTracker(partialProgressTracker1, 0.5, 1);

            foreach(double p in array)
            {
                Assert.DoesNotThrow(() => partialProgressTracker1_1.UpdateProgress(p));
            }
            foreach(double p in array)
            {
                Assert.DoesNotThrow(() => partialProgressTracker1_2.UpdateProgress(p));
            }

            PartialProgressTracker partialProgressTracker2 = new PartialProgressTracker(basicProgressTracker, 0.5, 1);
            PartialProgressTracker partialProgressTracker2_1 = new PartialProgressTracker(partialProgressTracker2, 0, 0.6);

            foreach(double p in array)
            {
                Assert.DoesNotThrow(() => partialProgressTracker2_1.UpdateProgress(p));
            }

            double[] array08 = { 0.6, 0.7, 0.8, 0.9, 1.0 };
            foreach (double p in array08)
            {
                Assert.DoesNotThrow(() => partialProgressTracker2.UpdateProgress(p));
            }
            Assert.AreEqual(TestProgressBar.max, progressBar.Value);
        }
    }
}