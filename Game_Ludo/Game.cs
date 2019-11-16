using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_Ludo
{
    public enum GameColor { Yellow, Blue, Red, Green, White};
    public enum GameState { InPlay, Finished };

    //All Stuff that needs execution.
    public class Game
    {
        private int numberOfPlayers; 
        private PlayerSelect[] players;
        private LudoDice die = new LudoDice();
        private int playerTurn = 0;
        private int tries = 0;
        
        private Field[] Fields;
        private Field[] InnerFields;
       
        private GameState state;

        //Constructor
        public Game()
        {
            Startscreen();
            Menu(); //a menu for the player.
            SetPlayerCount();
            CreatePlayers(); //Creates players
            CreateField(); //creates both the Outer and inner Fields needed
            this.state = GameState.InPlay;
            ShowPlayers(); //shows the players who is who.
            Turn();
        }
       
        // This class allows text to be written out letter by letter with spaces inbetween, for added effect.
        public void SlowPrint(string text, int time = 100)
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
        
        //The first class to be executed dispalys to the user the text written below.
        private void Startscreen()
        {
            //slowly prints "welcome to ludo" letter for letter with 100ms per letter written.
            SlowPrint("WELCOME TO LUDO!");
            Console.WriteLine(); //Console.WriteLine is used when there should be added space between sentences.
            SlowPrint("PRESS ANY KEY TO CONTINUE.");
            Console.ReadKey();
            Clear(5); //"Clear" class is used with a 5ms delay to remove the key the user pressed. For a smooth transition. 
        }
        
        private void Menu()
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
                    Clear(1000);
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
                    Clear(5000);
                    Menu();
                    break;

                case "help": //shows all available options
                    Clear(50);
                    Console.WriteLine("start - Start the Ludo game.");
                    Console.WriteLine("help - Shows this menu.");
                    Console.WriteLine("Credits - Shows who made this game.");
                    Console.WriteLine("quit - Quits the console completely.");
                    Console.ReadLine();
                    Menu();
                    break;

                default: // if any invalid option is written.
                    Console.WriteLine("Sorry that is not a command. Please input a command.");
                    Clear(3000);
                    Menu();
                    break;
            }
        } 

        private void Clear(int dl = 3000) //Allows code to be less long
        {
            Thread.Sleep(dl);
            Console.Clear();
        }

        //Allows text to be written in a different color.
        private void ColorChange(string Data, GameColor playerColor)
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

        //asks the user for an amount of players between 1 and 4 
        private void SetPlayerCount()
        {
            Console.WriteLine("How many Players? (From 2 - 4): ");

            while (numberOfPlayers < 2 || numberOfPlayers > 4)
            {
                //Makes sure that the input is neither over 4 or lower than 2
                
                if (!int.TryParse(Console.ReadLine().ToString(), out this.numberOfPlayers))
                { 
                    Console.WriteLine("Sorry that is not a number between 2 and 4.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    Console.WriteLine("How many Players? (From 2 - 4): ");
                }
               
            }
            Thread.Sleep(500);
        }

        private void CreatePlayers() //allows the players to be made.
        {
            this.players = new PlayerSelect[this.numberOfPlayers]; //Creates "PlayerSelect" or "Players" for however many players were needed. 

            Console.Clear();
     
            for (int i = 0; i < this.numberOfPlayers; i++)
            {
                Console.WriteLine("What is the name of Player {0}: ", (i + 1));
                string name = Console.ReadLine();
                Clear(1000);

                Token[] tokens = DesignatedTkn(i);

                players[i] = new PlayerSelect(name, (i + 1), tokens);
            }
        }
        
        //Creates 4 tokens for all players.
        private Token[] DesignatedTkn(int setToken) 
        {
            Token[] tokens = new Token[4]; //Creates 4 token for 1 Player.
            
            for (int i = 0; i < 4; i++)
            {
                //For each token created. A number, color, starting point, ending point, and ADS is created and given to said token.
                
                switch(setToken)
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

        private void CreateField() //Creates the fields necessary both outer and inner.
        {
            Fields = new Field[53];
            for (int i = 0; i < 53; i++)
            {
                Fields[i] = new Field(i , GameColor.White); //allows all other fields to be white.

                //switch case is needed for each color to have a starting position
                switch(Fields[i].GetID)
                {
                    case 2:
                       Fields[i].FieldColor = GameColor.Red;
                        break;
                    case 15:
                        Fields[i].FieldColor = GameColor.Green;
                        break;
                    case 28:
                        Fields[i].FieldColor = GameColor.Yellow;
                        break;
                    case 41:
                        Fields[i].FieldColor = GameColor.Blue;
                        break;
                }
            }

            InnerFields = new Field[6]; //InnerFields is created for the purpose of tokens entering 
            for (int i = 0; i < 6; i++)
            {
                InnerFields[i] = new Field(i, GameColor.White, 0);
            }
        }

        //Displays the players name and color.
        private void ShowPlayers()
        {
            Console.WriteLine("Okay, here are your players");
            foreach (PlayerSelect pl in this.players)
            {                                
                Console.WriteLine(pl.GetName);
                Console.Write("Your tokens are: ");
                Console.WriteLine(pl.GetColor);
                Clear();
            }      
        }

        private void Turn()
        {
            while (this.state == GameState.InPlay)
            {
                foreach (PlayerSelect pl in this.players)
                {
                    PlayerSelect personalTurn = players[(playerTurn)];
                    ColorChange(pl.GetName, pl.GetColor);
                    Console.WriteLine();
                    DieThrower();
                    ShowTknOptions(personalTurn.GetTokens());
                    ChangeTurns();
                }                 
            }
        }

        //The Die which to roll
        private void DieThrower()
        {
            do
            {
              Console.Write("Ready? Press 'K' to roll: "); // Using a Do/While allows for only 'k' to be Pressed to continue.
            }
            while (Console.ReadKey().KeyChar != 'k');
            Console.WriteLine(" ");
            Console.Write("You rolled: ");
            int x = die.ThrowDice();
         //   Thread.Sleep(2000);
            Console.WriteLine(x);
            Clear();
        }

        //shows the player the options of token choice 
        private void ShowTknOptions(Token[] tokens)
        {
            int choice = 0;
            int FinishedTkn = 0;

            foreach (Token tk in tokens)
            {
                Console.Write("Your "); ColorChange("Token", players[(playerTurn)].GetColor); tk.GetTokenId(); Console.WriteLine(" is placed: " + tk.TokenState);

                switch (tk.TokenState)
                {
                    case TokenState.Home:
                        if (die.GetValue() == 6) {
                            Console.WriteLine();
                            Console.Write("This Token is Home. ");
                            Console.WriteLine();
                            choice++;
                        }
                        else
                        {
                            Console.WriteLine("This Token is Unavailable.");
                            Console.WriteLine();
                        }
                        break;

                    case TokenState.EndZone:

                        Console.WriteLine();
                        Console.WriteLine("this token is " + tk.TokenPosition);
                        Console.WriteLine();
                        choice++;

                        break;

                    case TokenState.PlayField:

                        Console.WriteLine();
                        Console.WriteLine(" Available " + "at field: " + tk.TokenPosition);
                        Console.WriteLine();
                        choice++;
                        break;

                    case TokenState.Finished:

                        Console.WriteLine();
                        Console.WriteLine(" Finished. Not Available");
                        Console.WriteLine();
                        FinishedTkn++;
                        break;
                }
            }
          
            //Todo: 
            //Move Token of choice with the number rolled.
            Console.WriteLine();
            Console.WriteLine("you have "+ choice +" token(s) to choose from.");
            Console.WriteLine("you hit a: " + die.GetValue());
            //Clear(5000);
            if (FinishedTkn == 4)
            {
                FinishedGame();
            }

            if (choice >= 1)
            {
                MoveToken();
            }
            
            else
            {
                for (tries = 0; tries < 2; tries++)
                {
                    Clear();
                    do
                    {
                        Console.Write("Try again. Ready? Press 'k' to roll: "); 
                    }
                    while (Console.ReadKey().KeyChar != 'k');
                    Console.WriteLine(" ");
                    int x = die.ThrowDice();
                    Thread.Sleep(1000);
                    Console.WriteLine("next try: " + x);

                    if (x == 6)
                    {
                        Console.WriteLine("choose one of your tokens (1 - 4)");
                        MoveToken();
                        tries = 3;
                    }
                    
                }
                
            }

        }

        //Moves the chosen token with the die value
        private void MoveToken()
        {
            int x = 0;
            // x = Convert.ToInt32(Console.ReadLine().ToString()); //choice of token
            while ((!int.TryParse(Console.ReadLine(), out x)) || (x >= 5) || (x < 1))
                Console.WriteLine("Sorry that is not a valid input");
            
            Token ptrToken = players[playerTurn].GetToken[x - 1]; //Pointer to make code less long
            
            switch (ptrToken.TokenState)
            {
                case TokenState.Home:

                    if (ptrToken.TokenState == TokenState.Home && die.GetValue() == 6)
                    {
                        Fields[ptrToken.GetStartPos()].Occupy(ptrToken);
                        ptrToken.TokenState = TokenState.PlayField;  //sets the token from home to playfield.
                    }

                    else if (ptrToken.TokenState == TokenState.Home && die.GetValue() != 6)
                    {
                        Console.WriteLine("cannot move this token");
                        MoveToken();
                    }

                    break;

                case TokenState.PlayField:

                    Fields[ptrToken.TokenPosition + die.GetValue()].Occupy(ptrToken);

                    if (ptrToken.Getcolor() == GameColor.Red)
                    {
                        if (ptrToken.TokenPosition >= ptrToken.GetEndZonePos())
                        {
                            ptrToken.GetATS = true;
                            TransfertknField(ptrToken);
                        }
                    }
                        
                    if (ptrToken.TokenPosition >= 53)
                    {
                        ptrToken.TokenPosition += die.GetValue() - 52;
                        ptrToken.GetATS = true;
                    }

                    else if (ptrToken.TokenPosition >= ptrToken.GetEndZonePos() && ptrToken.ATS() == true)
                    {
                        TransfertknField(ptrToken);
                    }     

                    break;

                case TokenState.Finished:
                    Console.WriteLine("this token is at the finish line and can NOT be played.");
                    MoveToken();
                    
                    break;

                case TokenState.EndZone:
                    
                    if (ptrToken.TokenPosition + die.GetValue() > 5)
                    {
                        ptrToken.TokenState = TokenState.Finished;
                        ptrToken.TokenPosition = 6;
                    }

                    else 
                    InnerFields[ptrToken.TokenPosition + die.GetValue()].Occupy(ptrToken);
      
                break;
            }
        } 

        //changes turn between the players
        private void ChangeTurns()
        {
            Console.WriteLine("changing players");
            Clear();

            if (playerTurn == (numberOfPlayers - 1))

            {
                playerTurn = 0;
            }
            else
            {
                playerTurn++;
            }
        }
        
        //Transfers the chosen token into the innerField.
        private void TransfertknField(Token tknToMove)
        {
            tknToMove.TokenState = TokenState.EndZone;
            tknToMove.TokenPosition = 0 + die.GetValue();
            Fields[tknToMove.TokenPosition].Leave(tknToMove);
            InnerFields[tknToMove.TokenPosition - 1].Occupy(tknToMove);
        }

        //When used finishes the game and quits the application
        private void FinishedGame()
        {
            Clear(1000);
            SlowPrint("CONGRATULATIONS." + players[playerTurn].GetName + "HAS WON.");
            this.state = GameState.Finished;
            Environment.Exit(0);
        }
    }
}