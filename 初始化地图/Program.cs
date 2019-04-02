using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 初始化地图
{
    class Program
    {
        static int[] Maps = new int[100];

        static void Main(string[] args)
        {
            InitialMap();
            Console.ReadKey();
        }

        public static void InitialMap()
        {
            int[] luckyturn = { 6, 23, 40, 55,69,83};//幸运轮盘⊙
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }

            int[] landMine = { 5,13,17,33,38,50,64,80,94};//地雷※
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }

            int[] pause = { 9,27,60,93};//暂停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }

            int[] timeTunnel = { 20,25,45,63,72,88,90};//时间隧道卐
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }
    }
}
