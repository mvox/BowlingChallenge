using Bowling;
using System;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //todo - allow for user to choose number of frames. Will need to fix PrintScorecard a little bit.
            var scoreCard = new ScoreCard(10);
            Console.WriteLine("Welcome to bowling! Please make sure that this statement fits all on one line!:)");
            PrintScoreCard(scoreCard);

            //user input and printing for all bowls but last frame
            if(scoreCard.Frames.Count > 1) 
            {
                while (! scoreCard.Frames[scoreCard.Frames.Count - 2].IsCompleted())
                {
                    var inp = getUserInput();

                    if (inp == 10)
                    {
                        scoreCard.Bowl(inp);
                        PrintScoreCard(scoreCard);
                    }
                    else
                    {
                        scoreCard.Bowl(inp);
                        var inp2 = getUserInput();
                        scoreCard.Bowl(inp2);
                        PrintScoreCard(scoreCard);
                    }
                }
            }
            //user input and printing for bowls in last frame
            var finalBowl1 = getUserInput();
            scoreCard.Bowl(finalBowl1);
            if(finalBowl1 == 10) PrintScoreCard(scoreCard);
            var finalBowl2 = getUserInput();
            scoreCard.Bowl(finalBowl2);
            PrintScoreCard(scoreCard);
            if (finalBowl1 == 10 || finalBowl1 + finalBowl2 == 10)
            {
                scoreCard.Bowl(getUserInput());
                PrintScoreCard(scoreCard);
            }

        }

        static int getUserInput()
        {
            Console.Write("What Did You Bowl? ");
            var inp = Console.ReadLine();

            int num;
            bool success = int.TryParse(inp, out num);
            if(success)
            {
                return num;
            }
            else
            { 
                Console.WriteLine("Error Detecting Inputted Integer");
                return getUserInput();
            }
        }

        static void PrintScoreCard(ScoreCard frame)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|  |--1--||--2--||--3--||--4--||--5--||--6--||--7--||--8--||--9--||--1 0--|  |");
            Console.WriteLine("------------------------------------------------------------------------------");



            Console.Write("   ");
            //bowl line for all frames except last
            for (int i = 0; i < frame.Frames.Count - 1; i++)
            {
                if (frame.Frames[i].BowlOne == 10)
                {
                    Console.Write("| |X|-|");
                }
                else if (frame.Frames[i].BowlOne + frame.Frames[i].BowlTwo == 10)
                {
                    Console.Write($"| |{frame.Frames[i].BowlOne}|/|");
                }
                else
                {
                    var bowlOne = frame.Frames[i].BowlOne.ToString() != "" ? frame.Frames[i].BowlOne.ToString() : " ";
                    var bowlTwo = frame.Frames[i].BowlTwo.ToString() != "" ? frame.Frames[i].BowlTwo.ToString() : " ";
                    Console.Write($"| |{bowlOne}|{bowlTwo}|");
                }
            }
            //bowl line for last frame
            var lastFrame = (ScoreFrameFinal)frame.Frames.Last();
            var one = lastFrame.BowlOne.ToString() != "" ? lastFrame.BowlOne.ToString() : " ";
            if (lastFrame.BowlOne == 10) one = "X";
            var two = lastFrame.BowlTwo.ToString() != "" ? lastFrame.BowlTwo.ToString() : " ";
            if (lastFrame.BowlTwo == 10) two = "X";
            else if (lastFrame.BowlOne + lastFrame.BowlTwo == 10) two = "/";
            var three = lastFrame.BowlThree.ToString() != "" ? lastFrame.BowlThree.ToString() : " ";
            if (lastFrame.BowlThree == 10) three = "X";
            Console.Write($"| |{one}|{two}|{three}|");
            Console.WriteLine("   ");


            Console.Write("   ");
            //score line for all frames except last
            int? runningTotal = 0;
            for (int i = 0; i < frame.Frames.Count - 1; i++)
            {
                runningTotal = runningTotal + frame.Frames[i].FrameTotal;
                Console.Write($"|{runningTotal, 5}|");
            }
            //score line for last frame
            runningTotal = runningTotal + frame.Frames.Last().FrameTotal;
            Console.Write($"|{runningTotal,7}|");
            Console.WriteLine("   ");


            Console.WriteLine("------------------------------------------------------------------------------");
        }
    }
}