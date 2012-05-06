using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace pb
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }
    class Program
    {
        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);
            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int N = Int32.Parse(parts[0]);
            int[] s = new int[N];
            for (int i = 0; i < N; i++)
            {
                s[i] = Int32.Parse(parts[i+1]);
            }
            env.swr.Write("Case #{0}:", T);
            env.swr.WriteLine();
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
