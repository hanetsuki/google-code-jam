using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace sol_prob_a
{

    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }

    class sol_prob_a
    {
        static int[,] readMatrix(Env env)
        {
            int[,] matrix = new int[4,4];

            // 読み出し
            for (int i = 0; i < 4; i++)
            {
                string line = env.srd.ReadLine();
                for (int j = 0; j < 4; j++)
                {
                    switch (line[j])
                    {
                        case 'X':
                            matrix[i, j] = 1;
                            break;
                        case 'O':
                            matrix[i, j] = 2;
                            break;
                        case 'T':
                            matrix[i, j] = 3;
                            break;
                        default:
                            matrix[i, j] = 0;
                            break;
                    }
                }
            }

            // 読み捨て
            {
                string trash = env.srd.ReadLine();
            }
            return matrix;
        }

        static bool judgeSub2(int player, int cell) {
            if (cell == 3) {
                return true;
            }
            if (player == cell) {
                return true;
            }
            return false;
        }

        static int judgeSub(int a, int b, int c, int d)
        {
            if (a == 0) {
                return 0; // has empty
            }
            if (b == 0) {
                return 0; // has empty
            }
            if (c == 0) {
                return 0; // has empty
            }
            if (d == 0) {
                return 0; // has empty
            }
            for (int i = 1; i < 3; i++) {
                if (judgeSub2(i, a) && judgeSub2(i, b) && judgeSub2(i, c) && judgeSub2(i, d)) {
                    return i;
                }
            }
            return 3;//空きがある

        }
        static string judge(int[,] matrix)
        {
            bool hasEmpty = false;
            bool isXWon = false;
            bool isOWon = false;

            for (int i = 0; i < 4; i++)
            {
                int j1 = judgeSub(matrix[0, i], matrix[1, i], matrix[2, i], matrix[3, i]);
                int j2 = judgeSub(matrix[i, 0], matrix[i, 1], matrix[i, 2], matrix[i, 3]);
                if ((j1 == 0) || (j2 == 0))
                {
                    hasEmpty = true;
                }
                if ((j1 == 1) || (j2 == 1))
                {
                    isXWon = true;
                }
                if ((j1 == 2) || (j2 == 2))
                {
                    isOWon = true;
                }
            }
            {
                int j1 = judgeSub(matrix[0, 0], matrix[1, 1], matrix[2, 2], matrix[3, 3]);
                int j2 = judgeSub(matrix[3, 0], matrix[2, 1], matrix[1, 2], matrix[0, 3]);
                if ((j1 == 0) || (j2 == 0))
                {
                    hasEmpty = true;
                }
                if ((j1 == 1) || (j2 == 1))
                {
                    isXWon = true;
                }
                if ((j1 == 2) || (j2 == 2))
                {
                    isOWon = true;
                }
            }
            if (isXWon) {
                return "X won";
            }
            else if (isOWon) {
                return "O won";
            }
            else if (hasEmpty) {
                return "Game has not completed";
            } else {
                return "Draw";
            }

        }
        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);
            string result;
            result = " test";
            int[,] matrix = readMatrix(env);
            
            // 判定
            {
                result = judge(matrix);
            }

            env.swr.WriteLine("Case #{0}: {1}", T, result);
        }

        public static void probLoop(Env env)
        {
            string line = env.srd.ReadLine();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
                probCore(i, env);
            }
            sw.Stop();
            long millisec = sw.ElapsedMilliseconds;
            Console.WriteLine("used:{0}[ms]", millisec);
        }
    }

    class main
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }
            Env env = new Env();

            try
            {
                env.srd = new StreamReader(
                    args[0], Encoding.GetEncoding("Shift_JIS"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                env.swr = new StreamWriter(
                    args[1], false, Encoding.GetEncoding("Shift_JIS"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            sol_prob_a.probLoop(env);

            env.srd.Close();
            env.swr.Close();
        }

    }
}
