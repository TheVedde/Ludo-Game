using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Ludo.Interfaces;

namespace Game_Ludo
{
    public class LudoDice : IDie
    {
        private readonly Random rand = new Random();
        public LudoDice()
        {
            ThrowDice();
        }

        public int ThrowDice()
        {
            DieValue = rand.Next(1, 7);
            return DieValue;
        }

        public int DieValue { get; private set; }
        public int GetValue()=> DieValue;
    }
}