using System;

namespace delegatesAndEvents
{
    public delegate void RaceWinnerHandler(int winner);

    public class Race
    {
        public event RaceWinnerHandler Winner;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;
            // first to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {
                    if (participants[i] <= laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }
            }
            TheWinner(champ);
        }

        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            Winner?.Invoke(champ);
        }
    }

    class Program
    {
        public static void Main()
        {
            Race round1 = new Race();

            // register with the footRace event
            round1.Winner += footRace;

            // trigger the event
            round1.Racing(5, 10);

            // register with the carRace event
            round1.Winner += carRace;
            round1.Winner -= footRace; // this was the only way I could think to not allow other winners to show after every race
            //trigger the event ---------- Let me know if this is improper ^^^
            round1.Racing(3, 15);

            // register a bike race event using a lambda expression
            round1.Winner += winner => Console.WriteLine($"Biker number {winner} is the winner.");
            round1.Winner -= carRace;
            round1.Winner -= footRace;

            // trigger the event
            round1.Racing(4, 20);
        }

        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }

        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}

