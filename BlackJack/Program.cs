using System;

namespace FinalBlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            string Betamount = "";
            double num = 0, bet = 0, minbet = 49.99;
            double bonus = 0;
            bool decide = false, bust = false, answer = false;
            int count = 0;

            Console.WriteLine("-------Minimum Bet is {0}euros---", minbet);
            //Loop to ensure the value inputted is a number, and is above the minimum bet amount
            do
            {
                Console.Write("Enter you bet amount:");
                Betamount = Console.ReadLine();

                if (string.IsNullOrEmpty(Betamount) == true)
                {
                    answer = false;
                }
                else
                {
                    if (double.TryParse(Betamount, out num) == false)
                    {
                        Console.WriteLine("!!!!!INPUT MUST BE A NUMBER!!!!");
                    }
                    else
                    {
                        bet = Convert.ToDouble(Betamount);
                        if (bet > minbet)
                        {
                            answer = true;
                        }
                        else if (bet < minbet)
                        {
                            Console.WriteLine("!!!!Minimum bet is {0}euros", minbet);
                            answer = false;
                        }
                    }
                }

            } while (answer == false);

            Console.WriteLine("\n---------GAME BEGINS------\n");

            Player PlayerOne = new Player();
            Dealer DealerOne = new Dealer();    

            PlayerOne.TotalScore = PlayerInfo(bust, decide, count,ref bet);     //Calling the "PlayerInfo" method
            //Decision if player BUSTS
            if (PlayerOne.TotalScore == -1)
            {
                Console.WriteLine("\n-----SORRY, you are BUST----");
            }
            else
            {
                DealerOne.TotalScore = DealerInfo(bust, decide, count);   //calling the "DealerInfo" method
                //Decision if dealer BUSTS
                if (DealerOne.TotalScore == -1)
                {
                    Console.WriteLine("DEALER has BUST");
                    Console.WriteLine("\n\n--PLAYER WINS---");
                    bonus = bet;
                    bet = bet * 2;
                    Console.WriteLine("Amount Earned = {0}\nTotal Payout = {1} euros", bonus, bet);
                }
                else
                {
                    
                    //Decisions to get the winner or declare a draw
                    if (PlayerOne.TotalScore > DealerOne.TotalScore)
                    {

                        Console.WriteLine("\n\n--PLAYER WINS");
                        //Payout if player gets BLACKJACK
                        if (PlayerOne.TotalScore == 100)  
                        {
                            bonus = bet * 1.5;
                            bet = bonus + bet;
                            Console.WriteLine("Amount Earned = {0}\nTotal Payout = {1} euros", bonus, bet);
                        }
                        //Normal payout on a win
                        else
                        {
                            bonus = bet;
                            bet = bet * 2;
                            Console.WriteLine("Amount Earned = {0}\nTotal Payout = {1} euros", bonus, bet);
                        }
                    }
                        //Dealer wins
                    if (DealerOne.TotalScore > PlayerOne.TotalScore)
                    {
                        Console.WriteLine("\n\n--DEALER WINS");
                    }
                        //Game is drawn
                    if (DealerOne.TotalScore == PlayerOne.TotalScore)
                    {
                        Console.WriteLine("\n\nGAME IS A PUSH, NO WINNER");
                        bonus = 0;
                        Console.WriteLine("Amount Earned = {0}\nTotal Payout = {1} euros", bonus, bet);
                    }
                }
            }



        }
        static int PlayerInfo(bool bust, bool decide, int count, ref double bet)
        {
            /*  ------------------THE PLAYER OBJECT CODE--------------------*/
            Player Player1 = new Player();
            count = 0;
            while (count < 2)   //CREATING THE PLAYER FIRST TWO CARDS
            {
                Player1.CardCreator();
                count = count + 1;
            }
            Console.WriteLine("Your score is {0}", Player1.TotalScore);

            //Decision to determine if player can double down  
            bool canDouble = false;                                         
            if ((Player1.TotalScore == 10) || (Player1.TotalScore == 11))
            {
                canDouble = true;
            }

            //If they already have 21 jump to the dealer
            if (Player1.TotalScore == 21)
            {
                Player1.TotalScore = 100;       //Returns hundred if players two cards are = 21 (meaning blackjack)
                Console.WriteLine("\n\n-----DEALER PLAYS-------\n");
                //Program will ignore all decision below and just return the totalscore
            } 
            //Decision to prompt player to stand or hit or Double down
            else
            {                
                //Loop continues till player is BUST or decides to STAND
                while ((bust == false) && (Player1.choice != "S")&&(Player1.choice!="D"))
                {                    
                    //Assigns -1 value if player is BUST
                    if (Player1.TotalScore > 21)
                    {
                        bust = true;
                        Player1.TotalScore = -1;
                    }                   
                    else
                    {                        
                        //Loop to prevent incorrect input crashing the program
                        do
                        {
                            decide = false;             
                             
                            /*Decision to ensure the player can only choice double down immediately their first two cards have been drawn 
                                and they have a value of 10 or 11*/
                            if (canDouble==false)
                            {
                                Console.WriteLine("Do you want to Stand or Hit - s/h?");
                                Player1.choice = Console.ReadLine().ToUpper();
                                if ((Player1.choice == "S") || (Player1.choice == "H"))
                                {
                                    decide = true;
                                }
                                else
                                {
                                    Console.WriteLine("-----ERROR, Invalid input-----");
                                }
                                
                            }                            
                            else
                            {
                                Console.WriteLine("Do you want to Stand, Hit or Double Down - s/h/d");
                                Player1.choice = Console.ReadLine().ToUpper();
                                if ((Player1.choice == "S") || (Player1.choice == "H") || (Player1.choice=="D"))
                                {
                                    decide = true;
                                }
                                else
                                {
                                    Console.WriteLine("-----ERROR, Invalid input-----");
                                }
                            }
                            

                        } while (decide == false);

                        //Decision to initiate when player choses to Hit
                        if (Player1.choice == "H")
                        {
                            Player1.CardCreator();
                            Console.WriteLine("Your new Score is {0}", Player1.TotalScore);
                            canDouble = false;
                        }
                        //Decision to inititae when player chooses to Double down
                        if (Player1.choice=="D")
                        {
                            bet = bet * 2;
                            Console.WriteLine("------\nYour Bet has been doubled, New amount Bet = {0} euros \n ------",bet);
                            Player1.CardCreator();
                            Console.WriteLine("Your new Score is {0}",Player1.TotalScore);
                            if (Player1.TotalScore > 21)    //Checks if player is bust after doubling down
                            {
                                bust = true;
                                Player1.TotalScore = -1;
                            }
                        }
                        
                    }                   
                }
            }
            if ((Player1.choice == "S") || (Player1.choice == "D")||(Player1.TotalScore==21))
            {
                Console.WriteLine("\n\n-----DEALER PLAYS-------\n");
            }
            return Player1.TotalScore;

        }
        static int DealerInfo(bool bust, bool decide, int count)
        {
            /*  ------------------THE DEALER OBJECT CODE---------------------*/
            Dealer Dealer1 = new Dealer();
            bust = false;
            count = 0;
            while ((count < 2) && (bust == false))
            {
                Dealer1.CardCreator();
                count = count + 1;

                //Assigns -1 to dealers totalscore if they are BUST
                if (Dealer1.TotalScore > 21)
                {
                    bust = true;
                    Dealer1.TotalScore = -1;
                }
                //Ensures dealers score is >= 17
                else
                {
                    if ((count == 2) && (Dealer1.TotalScore < 17))
                    {
                        Console.WriteLine("Dealer Score = {0}\n !!!! DEALER SCORE IS BELOW 17, another card will be dealt", Dealer1.TotalScore);
                        count = count - 1;
                    }
                }

            }
            if (Dealer1.TotalScore != -1)
            {
                Console.WriteLine("Dealer score is {0}", Dealer1.TotalScore);
            }


            return Dealer1.TotalScore;
        }

    }
}
