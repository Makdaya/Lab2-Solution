using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;

namespace Lab2.Models
{
    internal class Snake : Enemy
    {
        private LevelData levelData;
        private Player player;
        
        public Snake(LevelData leveldata)
        {
            this.levelData = leveldata;
            Name = "Snake";
            Symbol = 'S';
            HP = 25;
            Color = ConsoleColor.Yellow;

            AttackDice = new Dice(3, 4, 2);
            DefenceDice = new Dice(1, 8, 5);
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }
       
        public override void Update()
        {
            int dx = player.X - X;
            int dy = player.Y - Y;

            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance > 2)
                return;

            int newX = X;
            int newY = Y;

            if (Math.Abs(dx) >= Math.Abs(dy) && dx != 0)
            {
                newX -= Math.Sign(dx);
            }
            else if (dy != 0)
            {
                newY -= Math.Sign(dy);
            }

            if (newX == player.X && newY == player.Y)
                return;

            foreach (Enemy enemy in levelData.Elements.OfType<Enemy>())
            {
                if (enemy != this && enemy.X == newX && enemy.Y == newY)
                {
                    return;
                }
            }

            if (newX >= 0 && newY >= 0 && !levelData.WallAhead(newX, newY) && newX < levelData.LevelWidth && newY < levelData.LevelHeight)

            {
                X = newX;
                Y = newY;
                
            }
        }

    }
}
