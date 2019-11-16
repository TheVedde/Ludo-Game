using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Ludo
{ 
    public enum TokenState {Home, PlayField, EndZone, Safe, Finished};


    public class Token
    {
        private readonly GameColor Color;
        private TokenState PlTknState;
        private readonly int TokenId;
        private int tokenPosition;
        private int startPos;
        private int EndZonePos;
        private bool AccessToSafety;

        public Token(int id, GameColor clr, int startPos, int endZonePos, bool ATS)
        {
            this.PlTknState = TokenState.Home;
            this.Color = clr;
            this.TokenId = id;
            this.startPos = startPos;
            this.EndZonePos = endZonePos;
            this.AccessToSafety = ATS;
        }

        public int GetTokenId() => TokenId;
        public GameColor Getcolor() => Color;
        public int GetStartPos() => startPos;
        public int GetEndZonePos() => EndZonePos;
        public bool ATS() => AccessToSafety;


        public TokenState TokenState { get => PlTknState; set => PlTknState = value; }

        public int TokenPosition { get => tokenPosition; set => tokenPosition = value; }

        public bool GetATS { get => AccessToSafety; set => AccessToSafety = value; }
    }
}
