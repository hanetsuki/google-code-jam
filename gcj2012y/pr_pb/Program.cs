using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pr_pb
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader sr;
        public StreamWriter sw;
    }

    class Program
    {
/*
        static void inttobyte(long x)
        {
            List<byte> lst = new List<byte>();

            while (x > 0)
            {
                byte lsd = (byte)(x % 10);
                lst.Add(lsd);
                x =  x / 10;
            }
            byte[] array = lst.ToArray();
        }
        static void pbCore(long t, Env env)
        {
            uint a;
            uint b;
            {
                string line = env.sr.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                a = UInt32.Parse(parts[0]);
                b = UInt32.Parse(parts[1]);
            }
            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();

            uint res = 0;

            for (uint i = a; i <= b; i++)
            {
                //res = res + pbsub(i, dict);

            }

            / *
                        string line_to = "";
                        int length = line_from.Length;
                        for (int i = 0; i < length; i++)
                        {
                            char char_from = line_from[i];
                            char char_to = grepl(char_from);
                            line_to += char_to;
                            Console.WriteLine("{0}{1}{2}", char_from, char_to, line_to);
                        }
                        env.sw.WriteLine("Case #{0}: {1}", t, line_to);
            * /
        }
*/



        static void pcCore(long t, Env env)
        {
            int n;
            int s;
            int p;
            int ans = 0;

            {
                string line = env.sr.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                n = Int32.Parse(parts[0]);
                s = Int32.Parse(parts[1]);
                p = Int32.Parse(parts[2]);
                //Console.WriteLine("t:{0}", t);
                //Console.WriteLine("n:{0} s:{1} p:{2}", n, s, p);
                for (int i = 0; i < n; i++)
                {
                    int ta = Int32.Parse(parts[i + 3]);
                    int d3c = (ta + 2) / 3;
                    //if (t == 3)
                    //{
                    //    Console.WriteLine("d3c:{0} ta:{1}", d3c, ta);
                    //}
                    int m3 = ta % 3;
                    if (d3c >= p)
                    {
                        //if (t == 3)
                        //{
                        //    Console.WriteLine("passa d3c:{0}", d3c);
                        //}
                        ans++;
                    }
//大会中                    else if (((d3c + 1) >= p) && (s > 0) && (m3 != 1) && (d3c >= 2))
//At least S of the ti values will be between 2 and 28, inclusive.
                    else if (((d3c + 1) >= p) && (s > 0) && (m3 != 1) && (ta >= 2))
                    {
                        //if (t == 3)
                        //{
                        //    Console.WriteLine("passb d3c:{0}", d3c);
                        //}
                        s--;
                        ans++;
                    }
                }
            }
            env.sw.WriteLine("Case #{0}: {1}", t, ans);
            //Console.WriteLine("ans:{0}", ans);
        }

        static void pbLoop(Env env)
        {
            string line = env.sr.ReadLine();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
                //Console.WriteLine("case{0}", i);
                pcCore(i, env);
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

            pbLoop(env);

            sw.Close();
        }
    }
}
