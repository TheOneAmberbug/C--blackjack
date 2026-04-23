using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Blackjack
    {
        static void Main(string[] args)
        {
            Random rng = new Random();
            int gold = 250;
            List<int> hand = new List<int>();
            int addhandamount = 2;
            List<int> handvalue = new List<int>();
            string action;
            int dealer = rng.Next(15, 24);

            Console.WriteLine("Blackjack \n" +
                "made by Amberbug \n"
                );
            int bet;
            int wantbet;
            while (gold > 0)
            {
                bet = 0;
                while (bet == 0)
                {
                    Console.WriteLine("Your gold:" + gold);
                    Console.WriteLine("Please place your bet");
                    wantbet = Convert.ToInt32(Console.ReadLine());
                    if (wantbet > gold)
                    {
                        Console.WriteLine("You too poor for that bet :/");
                        bet = 0;
                    }
                    else
                    {
                        bet = wantbet;
                    }
                }

                for (int i = 0; i < addhandamount; i = 0)
                {
                    addhandamount -= 1;
                    hand.Add(rng.Next(1, 52));
                }
                Console.WriteLine("Your Hand:");
                foreach (int card in hand)
                {
                    Console.WriteLine(CardFromNumber(card));
                    handvalue.Add(ValueFromCard(card));
                }
                Console.WriteLine("Total Value:" + handvalue.Sum());

                Console.WriteLine("Hit or Stay:");
                action = Console.ReadLine().ToLower();


                

                    while (action == "hit")
                    {
                        hand.Add(rng.Next(1, 52));

                        Console.WriteLine("Your Hand:");
                        Console.WriteLine(hand);
                        handvalue.Clear();
                        foreach (int card in hand)
                        {
                            Console.WriteLine(CardFromNumber(card));
                            handvalue.Add(ValueFromCard(card));
                        }
                        Console.WriteLine("Total Value:" + handvalue.Sum());
                        if (handvalue.Sum() > 21)
                        {
                        action = "stay";
                        }
                        action = "";
                        Console.WriteLine("Hit or Stay:");
                        action = Console.ReadLine().ToLower();
                    }
                    


                    if (action == "stay")
                    {
                        //Finish
                        Console.WriteLine("Dealerhandvalue: " + dealer);
                        
                        //Get delta
                        int dedealer = Math.Abs(dealer - 21);
                        int devalue = Math.Abs(handvalue.Sum() - 21);


                        //Basic über 21
                        if (dealer >= 21)
                        {
                            Console.WriteLine("You win: " + bet);
                            gold = gold + bet;
                        }
                        else if (handvalue.Sum() > 21)
                        {
                            Console.WriteLine("You loose: " + bet);
                            gold = gold - bet;
                        }
                        

                        //Check for closer
                        else if (dedealer >= devalue)
                        {
                            Console.WriteLine("You win: " + bet);
                            gold = gold + bet;
                        }
                        else if (dedealer < devalue)
                        {
                            Console.WriteLine("You loose: " + bet);
                            gold = gold - bet;

                        }

                        dealer = rng.Next(15, 24);
                        hand.Clear();
                        handvalue.Clear();
                        addhandamount = 2;
                    
                }
            }
            
        }
        static string CardFromNumber(int n)
        {
            if (n < 1 || n > 52) throw new ArgumentOutOfRangeException(nameof(n), "Value must be between 1 and 52.");

            string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            string[] suits = { "♣", "♦", "♥", "♠" }; // clubs, diamonds, hearts, spades

            int index = n - 1;
            int suitIndex = index / 13;
            int rankIndex = index % 13;

            return $"{ranks[rankIndex]}{suits[suitIndex]}";
        }
        static int ValueFromCard(int n)
        {
            if (n < 1 || n > 52) throw new ArgumentOutOfRangeException(nameof(n), "Value must be between 1 and 52.");

            int[] ranks = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 10, 10};


            int index = n - 1;
            int rankIndex = index % 13;

            return ranks[rankIndex];
        }

    }
}
