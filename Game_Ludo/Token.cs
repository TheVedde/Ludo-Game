using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Ludo.Interfaces;

namespace Game_Ludo
{ 
    public enum TokenState {Home, PlayField, EndZone, Safe, Finished};
    
    public class Token : IToken
    {
        //private readonly GameColor Color;
        //private TokenState PlTknState;
        //private readonly int TokenId;
        //private int tokenPosition;
        //private int startPos;
        //private int EndZonePos;
        //private bool AccessToSafety;

        public Token(int id, GameColor clr, int startPos, int endZonePos, bool ATS)
        {
            PlTknState = TokenState.Home;
            Color = clr;
            TokenId = id;
            this.startPos = startPos;
            EndZonePos = endZonePos;
            AccessToSafety = ATS;
        }

        public int GetTokenId() => TokenId;
        public GameColor GetColor() => Color;
        public int GetStartPos() => startPos;
        public int GetEndZonePos() => EndZonePos;
        public bool ATS() => AccessToSafety;


        public TokenState TokenState { get => PlTknState; set => PlTknState = value; }

        public int TokenPosition { get => tokenPosition; set => tokenPosition = value; }

        public bool GetATS { get => AccessToSafety; set => AccessToSafety = value; }
        
        public GameColor Color { get;  }
        public TokenState PlTknState { get; private set; }
        public int TokenId { get; }
        public int tokenPosition { get; private set; }
        public int startPos { get; }
        public int EndZonePos { get; }
        public bool AccessToSafety { get; private set; }
    }
}
