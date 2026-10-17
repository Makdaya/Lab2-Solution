
using Lab2;
using Lab2.Models;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;

namespace Lab2.Models;
class Rat : Enemy
{
    private static readonly Random random = new Random();

    private LevelData levelData;

    public Rat(LevelData levelData)
    {
        this.levelData = levelData;

        Name = "Rat";
        HP = 10;
        Symbol = 'r';
        Color = ConsoleColor.Red;

        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
    }

    public override void Update()
    {
        int direction = random.Next(4);

        int newX = X;
        int newY = Y;

        switch (direction)
        {
            case 0:
                newX++;
                break;

            case 1:
                newX--;
                break;

            case 2:
                newY++;
                break;

            case 3:
                newY--;
                break;
        }


        bool enemyAhead = levelData.Elements
            .OfType<Enemy>()
            .Any(enemy => enemy != this &&
                          enemy.X == newX &&
                          enemy.Y == newY);

        if (!levelData.WallAhead(newX, newY) && !enemyAhead)
        {
            X = newX;
            Y = newY;
        }
    }
}
