using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pb
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader sr;
        public StreamWriter sw;
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
            string line = env.sr.ReadLine();
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
                env.sw.Write("Case #{0}:", T);
                for (int i = 0; i < list.Count; i++)
                {
                    env.sw.Write(" {0}", list[i].Key);
                }
                env.sw.WriteLine();
            }
            else
            {
                env.sw.WriteLine("Case #{0}: NONE", T);
            }



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
