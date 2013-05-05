using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace sol_prob_b
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }

    class sol_prob_b
    {
        static int[,] readMatrix(Env env, int I, int J)
        {
            int[,] matrix = new int[I,J];   // row/col

            // 読み出し
            for (int i = 0; i < I; i++)
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                for (int j = 0; j < J; j++)
                {
                    matrix[i,j] = Int32.Parse(parts[j]);
                }
            }

            return matrix;
        }

        static string judge(int I, int J, int[,] matrix, Env env)
        {
            int [] row = new int[I];
            int [] column = new int[J];

            // 芝刈り機の高度をチェック
            for (int i = 0; i < I; i++) {
                for (int j = 0; j < J; j++) {
                    if (matrix[i,j] > row[i]) {
                        row[i] = matrix[i,j];
                    }
                    if (matrix[i,j] > column[j]) {
                        column[j] = matrix[i,j];
                    }
                }
            }
            // for debug
            if (false) {
                {
                    env.swr.WriteLine("");
                    for (int j = 0; j < J; j++) {
                        env.swr.Write(" {0}", column[j]);
                    }
                    env.swr.WriteLine("");
                }
                for (int i = 0; i < I; i++) {
                    for (int j = 0; j < J; j++) {
                        env.swr.Write("{0} ", matrix[i,j]);
                    }
                    env.swr.WriteLine(":{0}", row[i]);
                }
            }

            for (int i = 0; i < I; i++) {
                for (int j = 0; j < J; j++) {
                    if (
                        (matrix[i,j] < row[i]) && (matrix[i,j] < column[j])
                    ) {
                        return "NO";
                    }
                }
            }
            return "YES";
        }

        public static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);
            int I, J;
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                I = Int32.Parse(parts[0]);
                J = Int32.Parse(parts[1]);
            }
            int[,] matrix = readMatrix(env, I, J);
            string result = judge(I, J, matrix, env);
            env.swr.WriteLine("Case #{0}: {1}", T, result);
        }
    }

    class main
    {
        static void probLoop(Env env)
        {
            string line = env.srd.ReadLine();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
                sol_prob_b.probCore(i, env);    //< ターゲットごとに変更
            }
            sw.Stop();
            long millisec = sw.ElapsedMilliseconds;
            Console.WriteLine("used:{0}[ms]", millisec);
        }

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
            probLoop(env);

            env.srd.Close();
            env.swr.Close();
        }
    }
}
