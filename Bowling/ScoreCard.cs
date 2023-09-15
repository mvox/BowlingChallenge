using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    //todo - list of ScoreCard or new class for multiplayer
    public class ScoreCard
    {
        private readonly List<ScoreFrame> _frames;
        public ReadOnlyCollection<ScoreFrame> Frames { get { return _frames.AsReadOnly(); } }

        //passes in number of frames so games can be 1, 5, 10, etc. if user wants nonstandard game length
        public ScoreCard(int frames)
        {
            if(frames < 1)
            {
                throw new ArgumentOutOfRangeException("Frames must be >= 1");
            }

            _frames = new List<ScoreFrame> { };
            for (int i = 0; i < frames - 1; i++)
            {
                _frames.Add(new ScoreFrame());
            }
            _frames.Add(new ScoreFrameFinal());
        }

        public void Bowl(int pins)
        {
            if(pins < 0 || pins > 10)
            {
                throw new ArgumentException("Invalid number of pins");
            }
            if(Frames.All(f => f.IsCompleted()))
            {
                throw new Exception("The game is over already!"); //todo - define custom exceptions
            }

            var currentFrame = Frames.First(f => ! f.IsCompleted());


            if (currentFrame.IsLastFrame())//special last frame
            {
                var lastFrame = (ScoreFrameFinal)currentFrame;
                //first bowl
                if (lastFrame.BowlOne == null)
                {
                    lastFrame.BowlOne = pins;
                }
                //second bowl
                else if (lastFrame.BowlTwo == null)
                {
                    if(lastFrame.BowlOne == 10)
                    {
                        lastFrame.BowlTwo = pins;
                    }
                    else if(lastFrame.BowlOne + pins > 10) 
                    {
                        throw new Exception("Invalid number of second pins");
                    }
                    else if (lastFrame.BowlOne + pins == 10)
                    {
                        lastFrame.BowlTwo = pins;
                    }
                    else
                    {
                        lastFrame.BowlTwo = pins;
                        lastFrame.BowlThree = 0;//set third equal to 3 so that it can't be bowled later
                    }
                }
                //possible third bowl
                else if (lastFrame.BowlThree == null)
                {
                    lastFrame.BowlThree = pins;
                }
            }
            else//normal frames
            {
                //first bowl
                if (currentFrame.BowlOne == null)
                {
                    if (pins == 10)
                    {
                        currentFrame.BowlOne = pins;
                        currentFrame.BowlTwo = 0;
                    }
                    else
                    {
                        currentFrame.BowlOne = pins;
                    }
                }
                //second bowl
                else if (currentFrame.BowlTwo == null)
                {
                    if (currentFrame.BowlOne + pins > 10)
                    {
                        throw new Exception("Invalid number of second pins");
                    }

                    currentFrame.BowlTwo = pins;
                }
            }
            PopulateFrameTotals(); //performance hit (not needed to be called literally every bowl) not worth fixing
        }

        private void PopulateFrameTotals()
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                var currentFrame = Frames[i];

                if(currentFrame.FrameTotal == null)
                {
                    if (! currentFrame.IsCompleted())
                    {
                        //since the frame is incomplete, the population is done
                        return;
                    }
                    else
                    {
                        if (currentFrame.IsLastFrame())//special last frame
                        {
                            var lastFrame = (ScoreFrameFinal)currentFrame;
                            lastFrame.FrameTotal = lastFrame.BowlOne + lastFrame.BowlTwo + lastFrame.BowlThree;
                        }
                        else//normal frames
                        {
                            if(currentFrame.BowlOne == 10)//strike frame
                            {
                                var nextFrame = Frames[i + 1];
                                if (nextFrame.BowlOne == null || nextFrame.BowlTwo == null)
                                {
                                    //next frame incomplete so can't compute strike frame
                                    return;
                                }
                                else if (nextFrame.BowlOne == 10 && ! nextFrame.IsLastFrame())//needed for proper calculation of doubles, turkeys, and longer chains
                                {
                                    var frameAfter = Frames[i + 2];
                                    if (frameAfter.BowlOne == null)
                                    {
                                        //frame after incomplete so can't compute strike frame
                                        return;
                                    }
                                    else
                                    {
                                        currentFrame.FrameTotal = 10 + nextFrame.BowlOne + frameAfter.BowlOne;
                                    }
                                }
                                else
                                {
                                    currentFrame.FrameTotal = 10 + nextFrame.BowlOne + nextFrame.BowlTwo;
                                }
                            }
                            else if(currentFrame.BowlOne + currentFrame.BowlTwo == 10) //spare frame
                            {
                                var nextFrame = Frames[i + 1];
                                if (nextFrame.BowlOne == null)
                                {
                                    //next frame incomplete so can't compute spare frame
                                    return;
                                }
                                else
                                {
                                    currentFrame.FrameTotal = 10 + nextFrame.BowlOne;
                                }
                            }
                            else//open frame
                            {
                                currentFrame.FrameTotal = currentFrame.BowlOne + currentFrame.BowlTwo;
                            }
                        }
                    }
                }
            }
        }
    }
}
