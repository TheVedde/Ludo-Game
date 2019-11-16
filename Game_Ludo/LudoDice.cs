using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Ludo
{
    public class LudoDice
    {
        private Random rand = new Random();

        private int diceValue;

        public LudoDice()
        {
            this.ThrowDice();
        }

        public int ThrowDice()
        {
            this.diceValue = rand.Next(1, 7);
            return this.diceValue;
        }
        public int GetValue()=> diceValue;
    }
}