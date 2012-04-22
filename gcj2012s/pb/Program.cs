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
        static int hikaku(
          KeyValuePair<string, int> kvp1,
          KeyValuePair<string, int> kvp2)
        {
            return kvp1.Key.CompareTo(kvp2.Key);
        }

        static void probCore(long T, Env env)
        {
            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int K = Int32.Parse(parts[0]);
            string S = parts[1];
            int N = S.Length;

            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i <= (N - K); i++)
            {
                string key = S.Substring(i, K);
                if (!dict.ContainsKey(key)) {
                    dict[key] = 1;
                }
                else
                {
                    dict[key]++;
                }
            }
            Dictionary<string, int> dict2 = new Dictionary<string, int>(dict);
            foreach (KeyValuePair<string, int> pair in dict2)
            {
                if (pair.Value <= 1) {
                    dict.Remove(pair.Key);
                }
            }
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(dict);
            if (list.Count > 0)
            {
                list.Sort(hikaku);
                env.swr.Write("Case #{0}:", T);
                for (int i = 0; i < list.Count; i++)
                {
                    env.swr.Write(" {0}", list[i].Key);
                }
                env.swr.WriteLine();
            }
            else
            {
                env.swr.WriteLine("Case #{0}: NONE", T);
            }



            //env.sw.WriteLine("Case #{0}: {1}", t, ans);
            ////Console.WriteLine("ans:{0}", ans);
        }

        static void probLoop(Env env)
        {
            string line = env.srd.ReadLine();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
                //Console.WriteLine("case{0}", i);
                probCore(i, env);
            }
            sw.Stop();
            long millisec = sw.ElapsedMilliseconds;
            Console.WriteLine("used:{0}[ms]", millisec);
            //used:193[ms]
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
