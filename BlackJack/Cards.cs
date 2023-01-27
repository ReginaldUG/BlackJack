using System;
using System.Collections.Generic;
using System.Text;

namespace FinalBlackJack
{
    class Cards
    {
        /*VARIABLES*/
        private string[] CardNames = { "Hearts", "Diamond", "Spades", "Clubs" };
        private string[] CardNumbers = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        private string cardname = "";
        private string cardnumber = "";
        private int score = 0;
        private int totalScore = 0;
        private int aceCounter = 0;
        public string choice = "";

        public void CardCreator()       //METHOD FOR CREATING THE CARDS
        {
            Random rand1 = new Random();
            int num = rand1.Next(0, 13);
            //Generates random value to determine the card number 

            Random nameRand = new Random();
            int nameNum = nameRand.Next(0, 4);
            //Generates random value to determine the card names
            for (int i = 0; i < CardNames.Length; i++)
            {
                if (nameNum == i)
                {
                    cardname = CardNames[i];
                }
            }
            for (int i = 0; i < CardNumbers.Length; i++)
            {
                if (num == i)
                {
                    cardnumber = CardNumbers[i];
                    if ((cardnumber == "Jack") || (cardnumber == "King") || (cardnumber == "Queen"))
                    {
                        score = 10;
                    }
                    else if (cardnumber == "Ace")
                    {
                        if (aceCounter == 1)    //Accounts for ace card being valued at 11 or 1
                        {
                            aceCounter = aceCounter + 1;
                            score = 1;
                        }
                        else
                        {
                            aceCounter = aceCounter + 1;
                            score = 11;
                        }

                    }
                    else
                    {
                        score = Convert.ToInt32(cardnumber);
                    }
                    totalScore = totalScore + score;
                    Console.WriteLine("Card dealt is the {0} of {1}, value {2}  ", cardnumber, cardname, score);
                }
            }

        }

        public int AceCount
        {
            get { return aceCounter; }
            set { aceCounter = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public int TotalScore
        {
            get { return totalScore; }
            set { totalScore = value; }
        }


    }
}
