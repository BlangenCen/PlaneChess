using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游戏头
{
    class Program
    {
        static void Main(string[] args)
        {
            GameShow();
            Console.ReadKey();
        }

        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*******飞行棋一点都不好玩！*******");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************************");
        }
    }
}
