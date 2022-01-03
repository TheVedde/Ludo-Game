using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_Ludo
{
    public enum GameColor
    {
        Yellow,
        Blue,
        Red,
        Green,
        White
    };

    public enum GameState
    {
        InPlay,
        Finished
    };

    //All Stuff that needs execution.
    public class Game
    {
        private int numberOfPlayers;
        public static PlayerSelect[] players;

        private int playerTurn = 0;
        private int tries = 0;

        private Field[] Fields;
        private Field[] InnerFields;

        private GameState state;

        //Constructor
        public Game()
        {
            Utilities.Startscreen();
            Utilities.Menu(); //a menu for the player.
            SetPlayerCount();
            CreatePlayers(); //Creates players
            CreateField(); //creates both the Outer and inner Fields needed
            this.state = GameState.InPlay;
            Utilities.ShowPlayers(); //shows the players who is who.
            Turn();
        }
        
        //asks the user for an amount of players between 1 and 4 
        private void SetPlayerCount()
        {
            Console.WriteLine("How many Players? (From 2 - 4): ");

            while (numberOfPlayers < 2 || numberOfPlayers > 4)
            {
                //Makes sure that the input is neither over 4 or lower than 2
                if (!int.TryParse(Console.ReadLine(), out this.numberOfPlayers))
                {
                    Console.WriteLine("Sorry that is not a number between 2 and 4.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    Console.WriteLine("How many Players? (From 2 - 4): ");
                }
            }

            Thread.Sleep(200);
        }

        private void CreatePlayers() //allows the players to be made.
        {
            players = new PlayerSelect[this.numberOfPlayers]; //Creates "PlayerSelect" or "Players" for however many players were needed. 

            Console.Clear();

            for (int i = 0; i < this.numberOfPlayers; i++)
            {
                Console.WriteLine("What is the name of Player {0}: ", (i + 1));
                string name = Console.ReadLine();
                Utilities.Clear(1000);

                Token[] tokens = Utilities.DesignatedTkn(i);

                players[i] = new PlayerSelect(name, (i + 1), tokens);
            }
        }

        

        private void CreateField() //Creates the fields necessary both outer and inner.
        {
            Fields = new Field[53];
            for (int i = 0; i < 53; i++)
            {
                Fields[i] = new Field(i, GameColor.White); //allows all other fields to be white.

                //switch case is needed for each color to have a starting position
                switch (Fields[i].GetID)
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



        private void Turn()
        {
            while (this.state == GameState.InPlay)
            {
                foreach (PlayerSelect pl in players)
                {
                    PlayerSelect personalTurn = players[(playerTurn)];
                    Utilities.ColorChange(pl.GetName, pl.GetColor);
                    Console.WriteLine();
                    Utilities.DieThrower();
                    ShowTknOptions(personalTurn.GetTokens());
                    ChangeTurns();
                }
            }
        }

       

        //shows the player the options of token choice 
        private void ShowTknOptions(Token[] tokens)
        {
            int choice = 0;
            int FinishedTkn = 0;

            foreach (Token tk in tokens)
            {
                Console.Write("Your ");
                Utilities.ColorChange("Tokens", players[(playerTurn)].GetColor);
                tk.GetTokenId();
                Console.WriteLine(" is placed at: " + tk.TokenState);

                switch (tk.TokenState)
                {
                    case TokenState.Home:
                        if (Utilities.die.GetValue() == 6) {
                            Console.WriteLine();
                            Console.Write("This Token is Home. ");
                            Console.WriteLine();
                            choice++;
                        }
                        else {
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
                        Console.WriteLine(" Finished. Out of Game");
                        Console.WriteLine();
                        FinishedTkn++;
                        break;
                }
            }

            //Todo: 
            //Move Token of choice with the number rolled.
            Console.WriteLine();
            Console.WriteLine("you have " + choice + " token(s) to choose from.");
            Console.WriteLine("you hit a: " + Utilities.die.GetValue());
            //Clear(5000);
            
            if (FinishedTkn == 4) { FinishedGame(); }
            if (choice >= 1) { MoveToken(); }
            else {
                for (tries = 0; tries < 2; tries++) {
                    Utilities.Clear();
                    do {
                        Console.Write("Missed a 6. Try again. Ready? Press 'k' to roll: ");
                    } while (Console.ReadKey().KeyChar != 'k');

                    Console.WriteLine(" ");
                    int x = Utilities.die.ThrowDice();
                    Thread.Sleep(1000);
                    Console.WriteLine("next roll is a: " + x);

                    if (x == 6) {
                        Console.WriteLine("choose one of your tokens to move (1 - 4)");
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

                    if (ptrToken.TokenState == TokenState.Home && Utilities.die.GetValue() == 6)
                    {
                        Fields[ptrToken.GetStartPos()].Occupy(ptrToken);
                        ptrToken.TokenState = TokenState.PlayField; //sets the token from home to playfield.
                    }

                    else if (ptrToken.TokenState == TokenState.Home && Utilities.die.GetValue() != 6)
                    {
                        Console.WriteLine("cannot move this token");
                        MoveToken();
                    }

                    break;

                case TokenState.PlayField:

                    Fields[ptrToken.TokenPosition + Utilities.die.GetValue()].Occupy(ptrToken);

                    if (ptrToken.GetColor() == GameColor.Red)
                    {
                        if (ptrToken.TokenPosition >= ptrToken.GetEndZonePos())
                        {
                            ptrToken.GetATS = true;
                            TransfertknField(ptrToken);
                        }
                    }

                    if (ptrToken.TokenPosition >= 53)
                    {
                        ptrToken.TokenPosition += Utilities.die.GetValue() - 52;
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

                    if (ptrToken.TokenPosition + Utilities.die.GetValue() > 5)
                    {
                        ptrToken.TokenState = TokenState.Finished;
                        ptrToken.TokenPosition = 6;
                    }

                    else InnerFields[ptrToken.TokenPosition + Utilities.die.GetValue()].Occupy(ptrToken);

                    break;
            }
        }

        //changes turn between the players
        private void ChangeTurns()
        {
            Console.WriteLine();
            Console.WriteLine("Turn Over.");
            Console.WriteLine("Changing players");
            Utilities.Clear();

            if (playerTurn == (numberOfPlayers - 1)) playerTurn = 0;
            else playerTurn++; 
        }

        //Transfers the chosen token into the innerField.
        private void TransfertknField(Token tknToMove)
        {
            tknToMove.TokenState = TokenState.EndZone;
            tknToMove.TokenPosition = 0 + Utilities.die.GetValue();
            Fields[tknToMove.TokenPosition].Leave(tknToMove);
            InnerFields[tknToMove.TokenPosition - 1].Occupy(tknToMove);
        }

        //When used finishes the game and quits the application
        private void FinishedGame()
        {
            Utilities.Clear(1000);
            Utilities.SlowPrint("CONGRATULATIONS." + players[playerTurn].GetName + "HAS WON.");
            this.state = GameState.Finished;
            Environment.Exit(0);
        }
    }
}