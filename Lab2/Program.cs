
using Lab2;
using Lab2.Models;

LevelData levelData = new LevelData();
levelData.Load("Level1.txt");

Console.Clear();

foreach (LevelElement element in levelData.Elements)
{
    element.Draw();
}

Player player = new Player();

player.X = levelData.PlayerStartX;
player.Y = levelData.PlayerStartY;

player.Draw();

while (true)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

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

    bool wallAhead = false;
    foreach (LevelElement element in levelData.Elements)
    {
        if (element is Wall && element.X == newX && element.Y == newY)
        {
            wallAhead = true;
            break;
        }
    }

        if (!wallAhead)
        {
        Console.SetCursorPosition(player.X, player.Y);
        Console.Write(' ');
        player.X = newX;
        player.Y = newY;        
        player.Draw();
        
    }

}

