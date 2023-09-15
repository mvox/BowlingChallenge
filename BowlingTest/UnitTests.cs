namespace BowlingTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Frames must be >= 1")]
        public void InvalidNumberOfFrames()
        {
            var x = new ScoreCard(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid number of pins")]
        public void BowlAnInvalidPinAmount()
        {
            var x = new ScoreCard(5);
            x.Bowl(11);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "The game is over already!")]
        public void BowlAfterGameIsFinished()
        {
            var x = new ScoreCard(2);
            x.Bowl(5);
            x.Bowl(2);
            x.Bowl(5);
            x.Bowl(2);
            x.Bowl(5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Invalid number of second pins")]
        public void TooManyPinsOnSecondBowl()
        {
            var x = new ScoreCard(2);
            x.Bowl(5);
            x.Bowl(6);
        }

        [TestMethod]
        public void FinalFrameBowling1()
        {
            var x = new ScoreCard(1);
            x.Bowl(1);
            x.Bowl(2);
            Assert.AreEqual(x.Frames.Last().BowlOne, 1);
            Assert.AreEqual(x.Frames.Last().BowlTwo, 2);
            Assert.AreEqual(x.Frames.Last().FrameTotal, 3);
        }
        [TestMethod]
        public void FinalFrameBowling2()
        {
            var x = new ScoreCard(1);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            Assert.AreEqual(x.Frames.Last().BowlOne, 10);
            Assert.AreEqual(x.Frames.Last().BowlTwo, 10);
            Assert.AreEqual(((ScoreFrameFinal)x.Frames.Last()).BowlThree, 10);
            Assert.AreEqual(x.Frames.Last().FrameTotal, 30);
        }
        [TestMethod]
        public void FinalFrameBowling3()
        {
            var x = new ScoreCard(1);
            x.Bowl(10);
            x.Bowl(5);
            x.Bowl(5);
            Assert.AreEqual(x.Frames.Last().FrameTotal, 20);
        }
        [TestMethod]
        public void FinalFrameBowling4()
        {
            var x = new ScoreCard(1);
            x.Bowl(5);
            x.Bowl(5);
            x.Bowl(10);
            Assert.AreEqual(x.Frames.Last().FrameTotal, 20);
        }

        [TestMethod]
        public void FullGameNitpick()
        {
            var x = new ScoreCard(10);
            //frame 1 - open frame
            x.Bowl(5);
            Assert.AreEqual(x.Frames[0].BowlOne, 5);
            x.Bowl(3);
            Assert.AreEqual(x.Frames[0].BowlTwo, 3);
            Assert.AreEqual(x.Frames[0].FrameTotal, 8);
            //frame 2 - strike
            x.Bowl(10);
            Assert.AreEqual(x.Frames[1].BowlOne, 10);
            Assert.AreEqual(x.Frames[1].BowlTwo, 0);
            Assert.AreEqual(x.Frames[1].FrameTotal, null);
            //frame 3 - open frame
            x.Bowl(8);
            Assert.AreEqual(x.Frames[2].BowlOne, 8);
            x.Bowl(1);
            Assert.AreEqual(x.Frames[2].BowlTwo, 1);
            Assert.AreEqual(x.Frames[2].FrameTotal, 9);
            Assert.AreEqual(x.Frames[1].FrameTotal, 19);//previous frame's total has been calculated
            //frame 4 - spare
            x.Bowl(7);
            Assert.AreEqual(x.Frames[3].BowlOne, 7);
            x.Bowl(3);
            Assert.AreEqual(x.Frames[3].BowlTwo, 3);
            Assert.AreEqual(x.Frames[3].FrameTotal, null);
            //frame 5 - open frame
            x.Bowl(7);
            Assert.AreEqual(x.Frames[4].BowlOne, 7);
            Assert.AreEqual(x.Frames[3].FrameTotal, 17);//previous frame's total has been calculated
            x.Bowl(1);
            Assert.AreEqual(x.Frames[4].BowlTwo, 1);
            Assert.AreEqual(x.Frames[4].FrameTotal, 8);
            //frame 6 - strike
            x.Bowl(10);
            Assert.AreEqual(x.Frames[5].BowlOne, 10);
            Assert.AreEqual(x.Frames[5].BowlTwo, 0);
            Assert.AreEqual(x.Frames[5].FrameTotal, null);
            //frame 7 - strike
            x.Bowl(10);
            Assert.AreEqual(x.Frames[6].BowlOne, 10);
            Assert.AreEqual(x.Frames[6].BowlTwo, 0);
            Assert.AreEqual(x.Frames[6].FrameTotal, null);
            Assert.AreEqual(x.Frames[5].FrameTotal, null);//previous frame's total still null
            //frame 8 - open frame
            x.Bowl(7);
            Assert.AreEqual(x.Frames[7].BowlOne, 7);
            Assert.AreEqual(x.Frames[5].FrameTotal, 27);//previous * 2 frame's total has been calculated
            x.Bowl(2);
            Assert.AreEqual(x.Frames[7].BowlTwo, 2);
            Assert.AreEqual(x.Frames[7].FrameTotal, 9);
            Assert.AreEqual(x.Frames[6].FrameTotal, 19);//previous frame's total has been calculated
            //frame 9 - spare
            x.Bowl(8);
            Assert.AreEqual(x.Frames[8].BowlOne, 8);
            x.Bowl(2);
            Assert.AreEqual(x.Frames[8].BowlTwo, 2);
            Assert.AreEqual(x.Frames[8].FrameTotal, null);
            //final frame 
            x.Bowl(10);
            Assert.AreEqual(x.Frames[9].BowlOne, 10);
            Assert.AreEqual(x.Frames[8].FrameTotal, 20);//previous frame's total has been calculated
            x.Bowl(10);
            Assert.AreEqual(x.Frames[9].BowlTwo, 10);
            x.Bowl(1);
            Assert.AreEqual(((ScoreFrameFinal)x.Frames[9]).BowlThree, 1);
            
        }

        [TestMethod]
        public void PerfectGame()
        {
            var x = new ScoreCard(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);

            Assert.AreEqual(x.Frames[0].FrameTotal, 30);
            Assert.AreEqual(x.Frames[1].FrameTotal, 30);
            Assert.AreEqual(x.Frames[2].FrameTotal, 30);
            Assert.AreEqual(x.Frames[3].FrameTotal, 30);
            Assert.AreEqual(x.Frames[4].FrameTotal, 30);
            Assert.AreEqual(x.Frames[5].FrameTotal, 30);
            Assert.AreEqual(x.Frames[6].FrameTotal, 30);
            Assert.AreEqual(x.Frames[7].FrameTotal, 30);
            Assert.AreEqual(x.Frames[8].FrameTotal, 30);
            Assert.AreEqual(x.Frames[9].FrameTotal, 30);
        }

        [TestMethod]
        public void FullGame1()
        {
            var x = new ScoreCard(10);
            x.Bowl(6);
            x.Bowl(2);
            x.Bowl(7);
            x.Bowl(2);
            x.Bowl(3);
            x.Bowl(4);
            x.Bowl(8);
            x.Bowl(2);
            x.Bowl(9);
            x.Bowl(0);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(10);
            x.Bowl(6);
            x.Bowl(3);
            x.Bowl(8);
            x.Bowl(2);
            x.Bowl(7);

            Assert.AreEqual(x.Frames[0].FrameTotal, 8);
            Assert.AreEqual(x.Frames[1].FrameTotal, 9);
            Assert.AreEqual(x.Frames[2].FrameTotal, 7);
            Assert.AreEqual(x.Frames[3].FrameTotal, 19);
            Assert.AreEqual(x.Frames[4].FrameTotal, 9);
            Assert.AreEqual(x.Frames[5].FrameTotal, 30);
            Assert.AreEqual(x.Frames[6].FrameTotal, 26);
            Assert.AreEqual(x.Frames[7].FrameTotal, 19);
            Assert.AreEqual(x.Frames[8].FrameTotal, 9);
            Assert.AreEqual(x.Frames[9].FrameTotal, 17);
        }

        [TestMethod]
        public void FullGame2()
        {
            var x = new ScoreCard(10);
            x.Bowl(8);
            x.Bowl(1);
            x.Bowl(0);
            x.Bowl(9);
            x.Bowl(2);
            x.Bowl(8);
            x.Bowl(10);
            x.Bowl(6);
            x.Bowl(3);
            x.Bowl(7);
            x.Bowl(0);
            x.Bowl(5);
            x.Bowl(2);
            x.Bowl(10);
            x.Bowl(0);
            x.Bowl(6);
            x.Bowl(2);
            x.Bowl(8);
            x.Bowl(10);

            Assert.AreEqual(x.Frames[0].FrameTotal, 9);
            Assert.AreEqual(x.Frames[1].FrameTotal, 9);
            Assert.AreEqual(x.Frames[2].FrameTotal, 20);
            Assert.AreEqual(x.Frames[3].FrameTotal, 19);
            Assert.AreEqual(x.Frames[4].FrameTotal, 9);
            Assert.AreEqual(x.Frames[5].FrameTotal, 7);
            Assert.AreEqual(x.Frames[6].FrameTotal, 7);
            Assert.AreEqual(x.Frames[7].FrameTotal, 16);
            Assert.AreEqual(x.Frames[8].FrameTotal, 6);
            Assert.AreEqual(x.Frames[9].FrameTotal, 20);
        }

    }
}