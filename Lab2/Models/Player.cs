using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    internal class Player : LevelElement
    {
        public int HP { get; set; }
        public void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP- damage);
        }
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

        public void Attack(Enemy enemy)
        {
            int attack = AttackDice.Throw();
            int defence = enemy.DefenceDice.Throw();

            int damage = attack - defence;

            if (damage > 0)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}

