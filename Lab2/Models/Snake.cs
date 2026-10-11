using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab2.Models
{
    internal class Snake : Enemy
    {
        private static readonly Random random = new Random();
        
        public Snake()
        {
            Name = "Snake";
            Symbol = 'S';
            HP = 25;
            Color = ConsoleColor.Yellow;

            AttackDice = new Dice(3, 2, 8);
            DefenceDice = new Dice(3, 2, 8);
        }

        public override void Update()
        {
            if (Player.X < X)
            {
                X++;
            }
        }


    }
}
