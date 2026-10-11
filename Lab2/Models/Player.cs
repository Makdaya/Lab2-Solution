using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    internal class Player : LevelElement
    {
        public int HP { get; set; }

        public Dice DefenceDice { get; set; }

        public Dice AttackDice { get; set; }

        public Player()
        {
            HP = 100;
            Symbol = '@';
            Color = ConsoleColor.White;

            AttackDice = new Dice(2, 6, 2);
            DefenceDice = new Dice(2, 6, 0);
        }
    }
}

