using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pa
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader sr;
        public StreamWriter sw;
    }
    class Program
    {
        static void probCore(long T, Env env)
        {
            string line = env.sr.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int L = Int32.Parse(parts[0]);
            int M = Int32.Parse(parts[1]);
            int maxK = 0;
            for (int i = 0; i < M; i++)
            {
                int K = Int32.Parse(parts[2+i]);
                if (K > maxK)
                {
                    maxK = K;
                }
            }

            int ans;
            if (L < maxK)
            {
                ans = -1;
            }
            // else if (0 == maxK)
            // {
            //     ans = 0;
            // }
            else
            {
                ans = L * (M - 1) + maxK;
            }
            env.sw.WriteLine("Case #{0}: {1}", T, ans);
            //env.sw.WriteLine("Case #{0}: {1}", t, ans);
            ////Console.WriteLine("ans:{0}", ans);
        }

        static void probLoop(Env env)
        {
            string line = env.sr.ReadLine();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
                //Console.WriteLine("case{0}", i);
                probCore(i, env);
            }
        }

        static void Main(string[] args)
        {
            StreamReader sr = null;
            StreamWriter sw = null;
            Env env = new Env();

            try
            {
                sr = new StreamReader(
                    args[0], Encoding.GetEncoding("Shift_JIS"));
                env.sr = sr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                sw = new StreamWriter(
                    args[1], false, Encoding.GetEncoding("Shift_JIS"));
                env.sw = sw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            probLoop(env);

            sw.Close();
        }
    }
}
