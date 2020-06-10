using System;
using System.Collections.Generic;
using System.Linq;

namespace Skener
{
    class Program
    {
        static void Main(string[] args)
        {
            // ----- Skener problem ------

            // https://open.kattis.com/problems/skener

            // (R,C) --> pattern dimension    ..(R: Rows, C: Columns)..
            // (ZR,ZC) --> zoom parameters

            int R, C, ZR, ZC = 0; 

            var arr = StringLineToIntArray(Console.ReadLine());
            R = arr[0];
            C = arr[1];
            ZR = arr[2];
            ZC = arr[3];

            var general = new List<List<string>>(R);
            string str;
            var strList = new List<string>(C);
            for (int i = 0; i < R; i++)
            {
                str = Console.ReadLine();

                //List of characters, each character is a string.... so it is a list of a strings
                strList = str.ToCharArray().Select(c => c.ToString()).ToList();

                general.Add(strList);
            }

            // .... Zoom operations ....
            // Console.WriteLine("---------------------------------------");

            var result = Zoom(general, ZR, ZC);
            PrintList(result);

        } // -- end main

        private static List<List<string>> Zoom(List<List<string>> list, int ZR, int ZC)
        {
            var firstList = RowZoom(list, ZR);
            var secondList = ColumnZoom(firstList, ZC);
            return secondList;
        }
        private static List<List<string>> RowZoom(List<List<string>> list, int ZR)
        {
            if (ZR <= 1 || ZR > 5)
                return list;

            else
            {
                var g = new List<List<string>>();

                for (int i = 0; i < list.Count; i++)
                {
                    for (int k = 0; k < ZR; k++)
                    {
                        g.Add(list[i]);
                    }
                }
                return g;
            }
        }

        private static List<List<string>> ColumnZoom(List<List<string>> list, int ZC)
        {
            if (ZC <= 1 || ZC > 5)
                return list;

            else
            {
                var q = new List<List<string>>();
                for (int i = 0; i < list.Count; i++)
                {
                    q.Add(ColumnZoomOneList(list[i], ZC));
                }
                return q;
            }
        }

        private static List<string> ColumnZoomOneList(List<string> list, int ZC)
        {
            if (ZC <= 1 || ZC > 5)
                return list;
            else
            {
                var h = new List<string>();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int k = 0; k < ZC; k++)
                    {
                        h.Add(list[i]);
                    }
                }
                return h;
            }
        }

        private static void PrintList(List<List<string>> lists)
        {
            foreach (var list in lists)
            {
                foreach (var item in list)
                {
                    Console.Write(item);
                }
                Console.WriteLine("");
            }
        }
        private static int[] StringLineToIntArray(string line)
        {
            var str = new string[1];
            int[] res = new int[4] { 0, 0, 0, 0 };


            int R = 0;
            int C = 0;
            int ZR = 0;
            int ZC = 0;
            try
            {
                str = line.Split(' ');

                R = int.Parse(str[0]);
                C = int.Parse(str[1]);
                ZR = int.Parse(str[2]);
                ZC = int.Parse(str[3]);

                if (!ParametersConditions(R, C, ZR, ZC))
                    throw new ArgumentException();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} || {ex.GetType().FullName}");
                line = Console.ReadLine();
                res = StringLineToIntArray(line);
                return res;
            }

            res[0] = R;
            res[1] = C;
            res[2] = ZR;
            res[3] = ZC;

            return res;
        }
        private static bool ParametersConditions(int R, int C, int ZR, int ZC)
        {
            if (C >= 1 && C <= 50 &&
                R >= 1 && R <= 50 &&
                ZC >= 1 && ZC <= 5 &&
                ZR >= 1 && ZR <= 5)
                return true;
            else 
                return false;
        }
    }
    /*
     -- input(1) --                                         -- input(2) --
     3 3 1 2                                              3 3 2 1
     .x.                                                   .x.
     x.x                                                   x.x
     .x.                                                   .x.

     -- output(1) --                                         -- output(2) --
     ..xx..                                                 .x.
     xx..xx                                                 .x.
     ..xx..                                                 x.x
     xx..xx                                                 x.x
                                                            .x.
                                                            .x. 

     */
}
