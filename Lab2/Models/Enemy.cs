using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    abstract class Enemy: LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
        }
        public Dice AttackDice { get; set; }

        public Dice DefenceDice { get; set; }

        public abstract void Update();

    }
}
