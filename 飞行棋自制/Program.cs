using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋自制
{
    class Program
    {
        //游戏尺寸
        static int[] Maps = new int[100];
        static int[] PlayerPos = new int[2];
        static string[] PlayerName = new string[2];
        static bool[] Flags = new bool[2];

        static void Main(string[] args)
        {
            GameShow();
            #region 输入玩家姓名
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("请输入玩家A的姓名");
            PlayerName[0] = Console.ReadLine();
            while (PlayerName[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入！");
                PlayerName[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名");
            PlayerName[1] = Console.ReadLine();
            while (PlayerName[1] == "" || PlayerName[0] == PlayerName[1])
            {
                if (PlayerName[1] == "")
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入！");
                    PlayerName[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的姓名不能与玩家A的姓名相同，请重新输入！");
                    PlayerName[1] = Console.ReadLine();
                }
            }
            #endregion
            Console.Clear();
            GameShow();
            InitialMap();
            DrawMap();
            while (PlayerPos[0] < 100 && PlayerPos[1] < 100)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] == 99)
                {
                    Console.Clear();
                    Console.WriteLine("{0}可耻的赢了！", PlayerName[0]);
                    Console.WriteLine("{0}可耻的赢了！", PlayerName[0]);
                    Console.WriteLine("{0}可耻的赢了！", PlayerName[0]);
                    Win();
                    break;
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] == 99)
                {
                    Console.Clear();
                    Console.WriteLine("{0}可耻的赢了！", PlayerName[1]);
                    Console.WriteLine("{0}可耻的赢了！", PlayerName[1]);
                    Console.WriteLine("{0}可耻的赢了！", PlayerName[1]);
                    Win();
                    break;
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 游戏头
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("******一点都不好玩的飞行棋！******");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************************");
        }
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitialMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘⊙
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷※
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }

            int[] pause = { 9, 27, 60, 93 };//暂停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时间隧道卐
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }
        /// <summary>
        /// 画地图
        /// </summary>
        public static void DrawMap()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0}的飞机用A表示", PlayerName[0]);
            Console.WriteLine("{0}的飞机用B表示\n ", PlayerName[1]);
            Console.WriteLine("图例：幸运轮盘:⊙   地雷:※   暂停:▲   时空隧道:卐\n");
            #region 第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }//for
            #endregion
            //换行
            Console.WriteLine();
            #region 第一竖行
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion
            #region 第二横行
            for (int i = 64; i > 34; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //换行
            Console.WriteLine();
            #region 第二竖行
            for (int i = 65; i < 70; i++)
            {     
                Console.Write(DrawStringMap(i));
                for (int j = 1; j < 29; j++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
            #endregion
            #region 第三横行
            for (int i = 70; i < 100; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //换行
            Console.WriteLine();
        }
        /// <summary>
        /// 判断i应该画什么
        /// </summary>
        public static string DrawStringMap(int i)
        {
            string s = "";
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                s = "<>";
            }
            else if (PlayerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.White;
                s = "A";
            }
            else if (PlayerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.White;
                s = "B";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Green;
                        s = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        s = "⊙";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        s = "※";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        s = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        s = "卐";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        s = "？";
                        break;
                }//switch
            }//else
            return s;
        }
        /// <summary>
        /// 玩游戏
        /// </summary>
        /// <param name="playerNumber"></param>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int x = r.Next(1, 7);
            Console.WriteLine("{0}按任意键开始掷骰子", PlayerName[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}摇到了{1}", PlayerName[playerNumber], x);
            PlayerPos[playerNumber] += x;
            Change(playerNumber);
            Console.ReadKey(true);
            Console.Clear();
            DrawMap();
            if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])
            {
                PlayerPos[1 - playerNumber] -= 6;
            }
            else
            {
                switch (Maps[PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("{0}什么事都没发生。", PlayerName[playerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("{0}踩到地雷！退6格！", PlayerName[playerNumber]);
                        PlayerPos[0] -= 6;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("{0}踩到了时空隧道！进10格子！", PlayerName[playerNumber]);
                        PlayerPos[0] += 10;
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("{0}踩到幸运转盘！输入1交换位置，输入2轰炸对方，使对方退6格", PlayerName[playerNumber]);
                        string s = Console.ReadLine();
                        while (true)
                        {
                            if (s == "1")
                            {
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1 - playerNumber];
                                PlayerPos[1 - playerNumber] = temp;
                                Console.WriteLine("交换成功！");
                                break;
                            }
                            else if (s == "2")
                            {
                                PlayerPos[1 - playerNumber] -= 6;
                                Console.WriteLine("轰炸成功！");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("输入错误！请重新输入1或者2！");
                                s = Console.ReadLine();
                            }
                        }
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("{0}踩到了暂停，暂停一回合！", PlayerName[playerNumber]);
                        Flags[playerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    default:
                        break;
                }//switch
            }//else
            Change(playerNumber);
            Console.Clear();
            DrawMap();
        }
        /// <summary>
        /// 合法化用户位置
        /// </summary>
        /// <param name="i"></param>
        public static void Change(int i)
        {
            if (PlayerPos[i] < 0)
            {
                PlayerPos[i] = 0;
            }
            if (PlayerPos[i] > 99)
            {
                PlayerPos[i] = 99;
            }
        }
        /// <summary>
        /// 胜利
        /// </summary>
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                          ◆                      ");
            Console.WriteLine("                    ■                  ◆               ■        ■");
            Console.WriteLine("      ■■■■  ■  ■                ◆■         ■    ■        ■");
            Console.WriteLine("      ■    ■  ■  ■              ◆  ■         ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■       ■■■■■■■   ■    ■        ■");
            Console.WriteLine("      ■■■■ ■   ■                ●■●       ■    ■        ■");
            Console.WriteLine("      ■    ■      ■               ● ■ ●      ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■         ●  ■  ●     ■    ■        ■");
            Console.WriteLine("      ■■■■      ■             ●   ■   ■    ■    ■        ■");
            Console.WriteLine("      ■    ■      ■            ■    ■         ■    ■        ■");
            Console.WriteLine("      ■    ■      ■                  ■               ■        ■ ");
            Console.WriteLine("     ■     ■      ■                  ■           ●  ■          ");
            Console.WriteLine("    ■    ■■ ■■■■■■             ■              ●         ●");
            Console.ResetColor();
        }
    }
}
