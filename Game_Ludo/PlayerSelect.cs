using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Ludo
{
    public class PlayerSelect
    {
        private readonly string Name;
        private readonly Token[] tokens;
        private readonly int PlayerID;

        public PlayerSelect(string PlayerName, int id, Token[] tokens)
        {
            this.Name = PlayerName;
            this.tokens = tokens;
            this.GetColor = this.tokens[0].GetColor();
            this.PlayerID = id;
        }
        public string GetName => Name;
        public int GetPlayerID() => PlayerID;

        public GameColor GetColor
        {
            get;
        }
        public Token[] GetTokens() => tokens;
        
        public Token[] GetToken
        {
            get { return tokens; }
            //set { this.tokens = value; }
        }

        //public Token GetToken(int tknId) => tokens[tknId - 1];
    }
}
