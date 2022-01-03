using System;
using System.Threading;

namespace Game_Ludo
{
    public static class Utilities
    {
        public static LudoDice die = new LudoDice();
        
        
        //The first class to be executed dispalys to the user the text written below.
        public static void Startscreen()
        {
            //slowly prints "welcome to ludo" letter for letter with 100ms per letter written.
            SlowPrint("WELCOME TO LUDO!");
            Console.WriteLine(); //Console.WriteLine is used when there should be added space between sentences.
            SlowPrint("PRESS ANY KEY TO CONTINUE.");
            Console.ReadKey();
            Clear(5); //"Clear" class is used with a 5ms delay to remove the key the user pressed. For a smooth transition. 
        }
        
        // This class allows text to be written out letter by letter with spaces in between, for added effect.
        public static void SlowPrint(string text, int time = 100)
        {
            text.ToCharArray();
            foreach (char ch in text)
            {
                Console.Write(ch.ToString());
                Console.Write(" ");
                Thread.Sleep(time);
            }
            Console.WriteLine();
        }

        public static void Clear(int dl = 3000) //Allows code to be less long
        {
            Thread.Sleep(dl);
            Console.Clear();
        }
        
        //Allows text to be written in a different color.
        public static void ColorChange(string Data, GameColor playerColor)
        {
            //A text and a players color is needed.
            switch (playerColor)
            {
                case GameColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case GameColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case GameColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case GameColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }
            Console.Write(Data);           
            Console.ForegroundColor = ConsoleColor.White;
            //Changes the rest of the text as white.
        }
        
        //Displays the players name and color.
        public static void ShowPlayers()
        {
            Console.WriteLine("Okay, here are your players");
            foreach (PlayerSelect pl in Game.players)
            {
                Console.WriteLine(pl.GetName);
                Console.Write("Your tokens are: ");
                Console.WriteLine(pl.GetColor);
                Utilities.Clear();
            }
        }
        
         public static void Menu()
        {
            Console.WriteLine("Write 'start' to play the game");
            Console.WriteLine();
            Console.WriteLine("Write 'help' to see all available commands");
            string Decision = Console.ReadLine().ToLower(); //Decision string allows for the choices below.

            switch(Decision)
            {
                case "start":
                    // It actually just skips all the other options and allows the game to start.
                    break;

                case "quit":
                    Environment.Exit(0); // quits the program.
                    break;

                case "credits":
                    Utilities.Clear(1000);
                    Thread.Sleep(1024);
                    Console.WriteLine("Made By: Vedran Zelen.");
                    Thread.Sleep(1024);
                    Console.WriteLine();
                    Console.WriteLine("Honorable Mentions:");
                    Thread.Sleep(1024);
                    Console.WriteLine();
                    Console.WriteLine("Jacob Mørk Søfeldt");
                    Thread.Sleep(1024);
                    Console.WriteLine();
                    Console.WriteLine("Andreas Guldbrand");
                    Thread.Sleep(1024);
                    Console.WriteLine();
                    Console.WriteLine("SebaTheUnknownDude");
                    Thread.Sleep(1024);
                    Console.WriteLine();
                    Console.WriteLine("Thank you all for helping me make this game a thing.");
                    Utilities.Clear(5000);
                    Menu();
                    break;

                case "help": //shows all available options
                    Utilities.Clear(50);
                    Console.WriteLine("start - Start the Ludo game.");
                    Console.WriteLine("help - Shows this menu.");
                    Console.WriteLine("Credits - Shows who made this game.");
                    Console.WriteLine("quit - Quits the console completely.");
                    Menu();
                    break;

                default: // if any invalid option is written.
                    Console.WriteLine("Sorry that is not a command. Please input a command.");
                    Utilities.Clear(3000);
                    Menu();
                    break;
            }
        } 

         //Creates 4 tokens for all players.
         public static Token[] DesignatedTkn(int setToken)
         {
             Token[] tokens = new Token[4]; //Creates 4 token for 1 Player.

             for (int i = 0; i < 4; i++)
             {
                 //For each token created. A number, color, starting point, ending point, and ADS is created and given to said token.

                 switch (setToken)
                 {
                     case 0:
                         tokens[i] = new Token((i + 1), GameColor.Red, 2, 53, false);
                         break;
                     case 1:
                         tokens[i] = new Token((i + 1), GameColor.Green, 15, 13, false);
                         break;
                     case 2:
                         tokens[i] = new Token((i + 1), GameColor.Yellow, 28, 26, false);
                         break;
                     case 3:
                         tokens[i] = new Token((i + 1), GameColor.Blue, 41, 39, false);
                         break;
                 }
             }

             return tokens;
         }
         
         //The Die which to roll
         public static void DieThrower()
         {
             do {
                 Console.Write("Ready? Press 'K' to roll: "); // Using a Do/While allows for only 'k' to be Pressed to continue.
             } while (Console.ReadKey().KeyChar != 'k');

             Console.WriteLine(" ");
             Console.Write("You rolled a: ");
             int x = Utilities.die.ThrowDice();
             //   Thread.Sleep(2000);
             Console.WriteLine(x);
             Utilities.Clear();
         }
         
    }
}