using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Ludo
{
    public enum FieldType { Home, Safe, InPlay, Finish }

    class Field
    {
        public bool IsOccupied { get { return (Occupants.Count > 1); } }
        public List<Token> Occupants { get; private set; }
        public List<Token> InnerOccupants { get; private set; }

        private GameColor color;
        private readonly int fieldId;
        private int GetFinishedTkn;
     
        public Field(int id, GameColor clr)
        {
            this.fieldId = id;
            this.color = clr;
            Occupants = new List<Token>();
        } 

        public Field(int id, GameColor clr, int Finished)
        {
            this.fieldId = id;
            this.color = clr;
            InnerOccupants = new List<Token>();
            this.GetFinishedTkn = Finished;
        }
    
        public void Occupy(Token TknOccupant)
        {
            Occupants.Add(TknOccupant);
            TknOccupant.TokenPosition = this.fieldId;

            if (IsOccupied == true && Occupants.Count > 1)
            {
                KillEnemy();
            } 
        }

        public void Leave(Token Tknleave)
        {
            Occupants.Remove(Tknleave);
        }

        public int GetID
        {
            get => fieldId;
        }

        public GameColor GetColor
        {
            get;
        }
        
        private void KillEnemy()
        {

            if (Occupants[0].Getcolor() != Occupants[1].Getcolor())
            {
                if (Occupants[0].Getcolor() != FieldColor)
                {
                    Occupants[0].TokenState = TokenState.Home;
                    Occupants[0].TokenPosition = 0;
                }

                else if (Occupants[0].Getcolor() == FieldColor)
                {
                    Occupants[1].TokenState = TokenState.Home;
                    Occupants[1].TokenPosition = 0;
                }
            }
        }
        
        public GameColor FieldColor { get => color; set => color = value; }

        public int Finishedtkn
        {
            get => GetFinishedTkn;
        }
    }
}
