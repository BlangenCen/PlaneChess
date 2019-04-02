using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋自制1
{
    class Program
    {
        static string[] Maps = new string[30];//游戏尺寸，很麻烦(比如调道具颜色)，不如int[] Maps
        static int[] PlayerPos = new int[2];//玩家位置
        static string[] PlayerName = new string[2];//玩家姓名
        static bool[] Flags = new bool[2];//判断暂停

        static void Main(string[] args)
        {
            GameShow();
            #region 输入玩家姓名
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("请输入第一个玩家的姓名");
            PlayerName[0] = Console.ReadLine();
            while (PlayerName[0] == "")//当输入为空时，一直输入
            {
                Console.WriteLine("第一个玩家的姓名不能为空，请重新输入");
                PlayerName[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入第二个玩家的姓名");
            PlayerName[1] = Console.ReadLine();
            while (PlayerName[1] == "" || PlayerName[1] == PlayerName[0])//当第二个玩家为空或与第一个玩家姓名相同时，一直输入
            {
                if (PlayerName[1] == "")
                {
                    Console.WriteLine("第二个玩家的姓名不能为空，请重新输入");
                }
                else
                {
                    Console.WriteLine("第二个玩家的姓名不能与第一个玩家相同，请重新输入");
                }
                PlayerName[1] = Console.ReadLine();
            }
            #endregion
            Console.WriteLine("{0}的飞机用Ａ表示，{1}的飞机用Ｂ表示", PlayerName[0], PlayerName[1]);
            InitialMap();
            DrawMap();
            while (PlayerPos[0] < 29 && PlayerPos[1] < 29)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
            }
            Win();
            Console.ReadKey();
        }
        /// <summary>
        /// 游戏标题
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("********************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("********************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**--!---欢迎来到一点都不好玩的飞行棋---!--**");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("********************************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("********************************************");
        }
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitialMap()
        {
            for (int i = 0; i < Maps.Length; i++)
            {
                Maps[i] = "□";//什么都不发生
            }

            int[] luckyWheel = { 2, 6, 8, 16, 20, 23 }; //幸运转盘◎
            for (int i = 0; i < luckyWheel.Length; i++)
            {
                Maps[luckyWheel[i]] = "◎";
            }

            int[] pause = { 3, 7, 9, 17, 21, 24 };//暂停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = "▲";
            }

            int[] landMine = { 4, 10, 11, 18, 22, 25 };//地雷※
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = "※";
            }
        }
        /// <summary>
        /// 画地图
        /// </summary>
        public static void DrawMap()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("图例：幸运转盘：◎    暂停：▲    地雷：※\n");
            for (int i = 0; i < Maps.Length; i++)
            {
                if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("<>");//当玩家A和玩家B在同一位置
                }
                else if (PlayerPos[0] == i)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Ａ");//表示玩家A
                }
                else if (PlayerPos[1] == i)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Ｂ");//表示玩家B
                }
                else
                {
                    switch (Maps[i])
                    {
                        case "□":
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("□");
                            break;
                        case "◎":
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("◎");
                            break;
                        case "▲":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("▲");
                            break;
                        case "※":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("※");
                            break;
                        default:
                            break;
                    }
                }
            }//for
            Console.WriteLine();
        }
        /// <summary>
        /// 玩游戏
        /// </summary>
        /// <param name="i"></param>
        public static void PlayGame(int i)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}按任意键摇骰子", PlayerName[i]);
            Console.ReadKey(true);
            Random r = new Random();
            int x = r.Next(1, 7);//生成随机数，摇骰子
            Console.WriteLine("{0}摇到了{1}", PlayerName[i], x);
            Console.ReadKey(true);
            PlayerPos[i] += x;//玩家位置移动
            LegalizePlayerPos();

            if (PlayerPos[i] == PlayerPos[1 - i])
            {
                Console.WriteLine("{0}踩到了{1}，玩家{1}退回原点", PlayerName[i], PlayerName[1 - i]);
                PlayerPos[1 - i] = 0;
                Console.ReadKey(true);
            }
            else
            {
                switch (Maps[PlayerPos[i]])//判断玩家踩到了什么道具
                {
                    case "□":
                        Console.WriteLine("{0}什么也没踩到", PlayerName[i]);
                        Console.ReadKey(true);
                        break;
                    case "◎":
                        Console.WriteLine("{0}踩到了幸运圆盘，输入1与玩家{1}交换位置，输入2前进6格", PlayerName[i], PlayerName[1 - i]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                int temp = PlayerPos[i];
                                PlayerPos[i] = PlayerPos[1 - i];
                                PlayerPos[1 - i] = temp;
                                Console.WriteLine("交换成功！");
                                break;
                            }
                            else if (input == "2")
                            {
                                PlayerPos[i] += 6;
                                Console.WriteLine("{0}前进6格", PlayerName[i]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("输入错误！请重新输入,1与玩家{0}交换位置，2前进6格", PlayerName[1 - i]);
                                input = Console.ReadLine();
                            }
                        }
                        Console.ReadKey(true);
                        break;
                    case "▲":
                        Console.WriteLine("{0}踩到暂停，暂停一回合", PlayerName[i]);
                        Flags[i] = true;
                        Console.ReadKey(true);
                        break;
                    case "※":
                        Console.WriteLine("{0}踩到了雷区，退回原点", PlayerName[i]);
                        PlayerPos[i] = 0;
                        Console.ReadKey(true);
                        break;
                }//switch
            }//else
            LegalizePlayerPos();
            Console.Clear();
            Console.WriteLine("{0}的飞机用Ａ表示，{1}的飞机用Ｂ表示", PlayerName[0], PlayerName[1]);
            DrawMap();
        }
        /// <summary>
        /// 合法化玩家位置
        /// </summary>
        public static void LegalizePlayerPos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            else if (PlayerPos[0] >= 30)
            {
                PlayerPos[0] = 29;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            else if (PlayerPos[1] >= 30)
            {
                PlayerPos[1] = 29;
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
