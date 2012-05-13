using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace pa
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }
    class Program
    {
        static bool search(int current, int target, int[][] 継承, bool[] flag)
        {
            if (flag[current])
            {
                return true;
            }
            flag[current] = true;
            foreach (int m in 継承[current])
            {
                if (search(m, target, 継承, flag))
                {
                    return true;
                }
            }
            return false;
        }

        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);
            int N;
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                N = Int32.Parse(parts[0]);
            }
            int[][] 継承 = new int[N][];
            for (int i = 0; i < N; i++)
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                int M = Int32.Parse(parts[0]);
                継承[i] = new int[M];
                for (int j = 0; j < M; j++)
                {
                    継承[i][j] = Int32.Parse(parts[j + 1]) - 1;
                }
            }
            for (int i = 0; i < N; i++)
            {
                bool[] flag = new bool[N];

                //自分の子孫を全探索する
                if (search(i, i, 継承, flag)) {
                    env.swr.WriteLine("Case #{0}: Yes", T);
                    return;
                }
            }
            env.swr.WriteLine("Case #{0}: No", T);
        }

        static void probLoop(Env env)
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

        static void Main(string[] args)
        {
            StreamReader srd = null;
            StreamWriter swr = null;
            Env env = new Env();

            try
            {
                srd = new StreamReader(
                    args[0], Encoding.GetEncoding("Shift_JIS"));
                env.srd = srd;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                swr = new StreamWriter(
                    args[1], false, Encoding.GetEncoding("Shift_JIS"));
                env.swr = swr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            probLoop(env);

            swr.Close();
        }
    }
}
