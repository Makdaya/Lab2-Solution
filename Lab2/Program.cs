
using System;
using System.Linq;
using Lab2;
using Lab2.Models;

LevelData levelData = new LevelData();
levelData.Load("Level1.txt");

Console.Clear();

Player player = new Player();

player.X = levelData.PlayerStartX;
player.Y = levelData.PlayerStartY;

HashSet<(int X, int Y)> discoveredWalls = new();
levelData.SetPlayerForSnakes(player);
RenderWorld();

bool gameOver = false;

while (!gameOver)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key == ConsoleKey.Escape)
    {
        break;
    }

    int newX = player.X;
    int newY = player.Y;

    switch (key.Key)
    {
        case ConsoleKey.LeftArrow:
            newX--;
            break;

        case ConsoleKey.RightArrow:
            newX++;
            break;

        case ConsoleKey.UpArrow:
            newY--;
            break;

        case ConsoleKey.DownArrow:
            newY++;
            break;
    }

    bool wallAhead = levelData.Elements
        .OfType<Wall>()
        .Any(wall => wall.X == newX && wall.Y == newY);

    Enemy? enemyAhead = levelData.Elements
        .OfType<Enemy>()
        .FirstOrDefault(enemy =>
            enemy.X == newX && enemy.Y == newY);

    if (!wallAhead && enemyAhead == null)
    {
        Console.SetCursorPosition(player.X, player.Y);
        Console.Write(' ');

        player.X = newX;
        player.Y = newY;

        player.Draw();
    }
    else if (enemyAhead != null)
    {
        Fight(player, enemyAhead, true);

        if (enemyAhead.HP <= 0)
        {
            Console.SetCursorPosition(enemyAhead.X, enemyAhead.Y);
            Console.Write(' ');

            levelData.Elements.Remove(enemyAhead);
        }

        if (player.HP <= 0)
        {
            gameOver = true;
        }
    }

    if (gameOver)
    {
        break;
    }

    foreach (Enemy enemy in levelData.Elements
                 .OfType<Enemy>()
                 .ToList())
    {
        int oldX = enemy.X;
        int oldY = enemy.Y;

        Console.SetCursorPosition(oldX, oldY);
        Console.Write(' ');

        enemy.Update();

        bool wallBlocked = levelData.Elements
            .OfType<Wall>()
            .Any(wall =>
                wall.X == enemy.X && wall.Y == enemy.Y);

        bool enemyBlocked = levelData.Elements
            .OfType<Enemy>()
            .Any(other =>
                other != enemy &&
                other.X == enemy.X &&
                other.Y == enemy.Y);

        if (enemy.X == player.X && enemy.Y == player.Y)
        {
            Fight(player, enemy, false);

            if (enemy.HP <= 0)
            {
                levelData.Elements.Remove(enemy);
            }
            else
            {
                
                enemy.X = oldX;
                enemy.Y = oldY;
            }

            player.Draw();

            if (player.HP <= 0)
            {
                gameOver = true;
                break;
            }
        }
        else if (wallBlocked || enemyBlocked)
        {
            enemy.X = oldX;
            enemy.Y = oldY;
        }
    }

    if (gameOver)
    {
        break;
    }

    Console.SetCursorPosition(0, levelData.LevelHeight + 1);
    Console.Write($"Spelarens HP: {player.HP}          ");

    RenderWorld();
}



Console.SetCursorPosition(0, levelData.LevelHeight + 3);

if (player.HP <= 0)
{
    Console.WriteLine("GAME OVER! Du har besegrats.");
}
else
{
    Console.WriteLine("Spelet avslutades.");
}


void Fight(Player player, Enemy enemy, bool playerAttacksFirst)
{
    bool playerTurn = playerAttacksFirst;

    while (player.HP > 0 && enemy.HP > 0)
    {
        int damage;

        if (playerTurn)
        {
            int attack = player.AttackDice.Throw();
            int defence = enemy.DefenceDice.Throw();

            damage = attack - defence;

            if (damage > 0)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                damage = 0;
            }

            Console.SetCursorPosition(0, levelData.LevelHeight + 1);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, levelData.LevelHeight + 1);
            Console.Write($"Du attackerar {enemy.Name}: {damage} skada. Fiendens HP: {enemy.HP}");
        }
        else
        {
            int attack = enemy.AttackDice.Throw();
            int defence = player.DefenceDice.Throw();

            damage = attack - defence;

            if (damage > 0)
            {
                player.HP -= damage;

                if (player.HP < 0)
                {
                    player.HP = 0;
                }
            }

            Console.SetCursorPosition(0, levelData.LevelHeight + 2);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, levelData.LevelHeight + 2);
            Console.Write($"{enemy.Name} attackerar: {damage} skada. Dina HP: {player.HP}");
        }

        playerTurn = !playerTurn;
    }

    if (enemy.HP <= 0)
    {
        Console.SetCursorPosition(0, levelData.LevelHeight + 2);
        Console.Write($"{enemy.Name} är besegrad!          ");
    }
}

void RenderWorld()
{
    foreach (Wall wall in levelData.Elements.OfType<Wall>())
    {
        int dx = wall.X - player.X;
        int dy = wall.Y - player.Y;

        if (dx * dx + dy * dy <= 25)
        {
            discoveredWalls.Add((wall.X, wall.Y));
        }
    }

    foreach (Wall wall in levelData.Elements.OfType<Wall>())
    {
        if (discoveredWalls.Contains((wall.X, wall.Y)))
        {
            wall.Draw();
        }
    }

    foreach (Enemy enemy in levelData.Elements.OfType<Enemy>())
    {
        int dx = enemy.X - player.X;
        int dy = enemy.Y - player.Y;

        if (dx * dx + dy * dy <= 25)
        {
            enemy.Draw();
        }
        else
        {
            Console.SetCursorPosition(enemy.X, enemy.Y);
            Console.Write(' ');
        }
    }

    player.Draw();
}
