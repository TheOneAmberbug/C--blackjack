using System;
using System.Collections.Generic;

class Blackjack
{
    static Random rnd = new Random();

    static void Main()
    {
        int geld = 25000; // Startgeld

        Console.WriteLine("🃏 Blackjack mit Geld\n");
        Console.WriteLine("Startguthaben: 200€\n");

        while (geld > 0)
        {
            Console.WriteLine($"Guthaben: {geld}€");
            Console.Write("Einsatz: ");

            if (!int.TryParse(Console.ReadLine(), out int einsatz) || einsatz <= 0 || einsatz > geld)
            {
                Console.WriteLine("Ungültiger Einsatz.\n");
                continue;
            }

            geld -= einsatz; // Einsatz abziehen

            int rundenGewinn = SpieleRunde(einsatz);

            geld += rundenGewinn; // Gewinn oder 0 oder Verlust

            Console.WriteLine($"\nNeues Guthaben: {geld}€\n");

            if (geld <= 0)
            {
                Console.WriteLine("Du hast kein Geld mehr. Spiel vorbei.");
                break;
            }

            Console.Write("Nochmal spielen? (j/n): ");
            if (Console.ReadLine().ToLower() != "j")
                break;

            Console.Clear();
        }
    }

    static int SpieleRunde(int einsatz)
    {
        List<int> spieler = new List<int>();
        List<int> dealer = new List<int>();

        // Startkarten
        spieler.Add(KarteZiehen());
        spieler.Add(KarteZiehen());
        dealer.Add(KarteZiehen());
        dealer.Add(KarteZiehen());

        Console.WriteLine("\nDeine Karten:");
        ZeigeHand(spieler);
        Console.WriteLine($"Summe: {HandWert(spieler)}");

        Console.WriteLine("\nDealer zeigt:");
        Console.WriteLine($"[?] und {dealer[1]}");

        // Blackjack Check
        if (HandWert(spieler) == 21)
        {
            Console.WriteLine("\n🎉 Blackjack! Du bekommst 1.5x Einsatz.");
            return (int)(einsatz * 2.5); // Einsatz + 1.5x Gewinn
        }

        // Spielerzug
        while (true)
        {
            Console.Write("\nHit (h) oder Stand (s): ");
            string wahl = Console.ReadLine().ToLower();

            if (wahl == "h")
            {
                int karte = KarteZiehen();
                spieler.Add(karte);
                Console.WriteLine($"Du ziehst: {karte}");
                ZeigeHand(spieler);

                int wert = HandWert(spieler);
                Console.WriteLine($"Summe: {wert}");

                if (wert > 21)
                {
                    Console.WriteLine("💥 Bust! Du verlierst.");
                    return 0; // kein Gewinn
                }
            }
            else if (wahl == "s")
            {
                break;
            }
            else
            {
                Console.WriteLine("Ungültig.");
            }
        }

        // Dealerzug
        Console.WriteLine("\nDealer deckt auf:");
        ZeigeHand(dealer);
        Console.WriteLine($"Summe Dealer: {HandWert(dealer)}");

        while (HandWert(dealer) < 17)
        {
            int karte = KarteZiehen();
            dealer.Add(karte);
            Console.WriteLine($"Dealer zieht: {karte}");
            Console.WriteLine($"Summe Dealer: {HandWert(dealer)}");
        }

        int spielerWert = HandWert(spieler);
        int dealerWert = HandWert(dealer);

        Console.WriteLine("\n--- Ergebnis ---");
        Console.WriteLine($"Deine Summe: {spielerWert}");
        Console.WriteLine($"Dealer Summe: {dealerWert}");

        if (dealerWert > 21)
        {
            Console.WriteLine("🎉 Dealer bust! Du gewinnst.");
            return einsatz * 2; // Einsatz + Gewinn
        }
        else if (spielerWert > dealerWert)
        {
            Console.WriteLine("🎉 Du gewinnst!");
            return einsatz * 2;
        }
        else if (spielerWert < dealerWert)
        {
            Console.WriteLine("❌ Dealer gewinnt.");
            return 0;
        }
        else
        {
            Console.WriteLine("🤝 Unentschieden. Einsatz zurück.");
            return einsatz; // Push → Einsatz zurück
        }
    }

    static int KarteZiehen()
    {
        int k = rnd.Next(2, 15);

        if (k >= 11 && k <= 13)
            return 10; // Bube, Dame, König

        if (k == 14)
            return 11; // Ass

        return k; // 2–10
    }

    static int HandWert(List<int> hand)
    {
        int summe = 0;
        int asse = 0;

        foreach (int k in hand)
        {
            summe += k;
            if (k == 11)
                asse++;
        }

        while (summe > 21 && asse > 0)
        {

            summe -= 10; // Ass von 11 auf 1
            asse--;
        }

        return summe;
    }

    static void ZeigeHand(List<int> hand)
    {
        Console.Write("Karten: ");
        foreach (int k in hand)
            Console.Write(k + " ");
        Console.WriteLine();
    }
}
