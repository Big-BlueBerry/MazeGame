using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled
{
    class Program
    {
        static void Main(string[] args)
        {
            int posX = -1 , posY = -1;
            char[][] map = File.ReadAllText("map.txt")
                .Replace("\r\n", "\n")
                .Split('\n')
                .Select(s => s.ToCharArray())
                .ToArray();
            int score = 0;
            var charmap = new Dictionary<char, char>()
            {
                { '0', '■' },
                { '1', '□' },
                { '2', '◎' },
                { '3', '★' },
            };

            for (int x = 0; x < map[0].Length; x++)
                for (int y = 0; y < map.Length; y++)
                    if (map[y][x] == '2')
                    {
                        posX = x;
                        posY = y;
                    }

            if (posX == -1)
            {
                Console.WriteLine("플레이어가 없네용!");
                Console.Read();
                return;
            }

            while (true)
            {
                DisplayMap();
                ConsoleKeyInfo key = Console.ReadKey();
                int prevX = posX, prevY = posY;
                switch (key.KeyChar)
                {
                    case 'w':
                    case 'W':
                        posY--;
                        break;

                    case 's':
                    case 'S':
                        posY++;
                        break;
                    case 'a':
                    case 'A':
                        posX--;
                        break;
                    case 'd':
                    case 'D':
                        posX++;
                        break;

                    default:
                        break;
                }
                if(!isValidPlayerPosition(posX, posY))
                {
                    posX = prevX;
                    posY = prevY;
                }
                else
                {
                    if (map[posY][posX] == '3')
                    {
                        score++;
                        // Create new fruit
                    }
                    map[prevY][prevX] = '0';
                    map[posY][posX] = '2';
                    
                }
            }

            bool isValidPlayerPosition(int x, int y)
            {
                if (x < 0 || x >= map[0].Length)
                    return false;
                if (y < 0 || y >= map.Length)
                    return false;
                if (map[y][x] == '1')
                    return false;

                return true;
            }

            void DisplayMap()
            {
                Console.Clear();
                Console.WriteLine($"Score: {score}");
                Console.WriteLine($"{posX}, {posY}: {map[posY][posX]}");

                for(int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[0].Length; x++)
                    {
                        if (posX - 3 <= x && x <= posX + 3
                            && posY - 3 <= y && y <= posY + 3)
                        {
                            Console.Write(charmap[map[y][x]]);
                        }
                        else
                            Console.Write('　');
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
