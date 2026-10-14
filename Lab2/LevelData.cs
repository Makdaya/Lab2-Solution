using Lab2.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.ComponentModel.Design;

namespace Lab2
{
    internal class LevelData
    {
        private List<LevelElement> elements = new List<LevelElement>();

        public int PlayerStartX { get; private set; }
        public int PlayerStartY { get; private set; }
        public void Load(string filename)
        {
            elements.Clear();

            String[] Lines = File.ReadAllLines(filename);

            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[i].Length; j++)
                {

                    char symbol = Lines[i][j];

                    if (symbol == '#')
                    {
                        Wall wall = new Wall();

                        wall.X = j;
                        wall.Y = i;
                        elements.Add(wall);
                    }

                    if (symbol == 'r')
                    {
                        Rat rat = new Rat(this);
                        rat.X = j;
                        rat.Y = i;
                        elements.Add(rat);
                    }
                    if (symbol == 's')
                    {
                        Snake snake = new Snake(this);
                        snake.X = j;
                        snake.Y = i;
                        elements.Add(snake);
                    }

                    if (symbol == '@')
                    {
                        PlayerStartX = j;
                        PlayerStartY = i;
                    }
                }

            }
        }


        public List<LevelElement> Elements
        {
            get { return elements; }
        }

        public void SetPlayerForSnakes(Player player)
        {
            foreach (LevelElement element in elements)
            {
                if (element is Snake snake)
                {
                    snake.SetPlayer(player);
                }
            }
        }
        public bool WallAhead(int newX, int newY)
        {
            foreach (LevelElement element in Elements)
            {
                if (element is Wall &&
                    element.X == newX &&
                    element.Y == newY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}