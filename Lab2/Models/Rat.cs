using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lab2.Models
{
    class Rat:Enemy
    {
        private static readonly Random random = new Random();

        public Rat()
        {
            Name = "Rat";
            HP = 10;
            Symbol = 'r';
            Color = ConsoleColor.Red;

            AttackDice = new Dice(1, 6, 3);
            DefenceDice = new Dice(1, 6, 3);

        }
        public override void Update()
        {
            int direction = random.Next(4);

            switch (direction)
            {
                case 0:
                    X++;
                    break;

                case 1:
                    X--;
                    break;

                case 2:
                    Y++;
                    break;

                case 3:
                    Y--;
                    break;
            }
        }
    }
}
