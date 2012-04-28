using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace r1a_pb
{
    class Program
    {
        class Env
        {
            static public string[] delim = { " ", "\t" };
            public StreamReader srd;
            public StreamWriter swr;
        }
        struct level
        {
            public int a;
            public int b;
            public int c;
        }
        static void probCore(long T, Env env)
        {
            string line = env.srd.ReadLine();
            int N = Int32.Parse(line);
            level[] l = new level[N];



        //    string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            //    int A = Int32.Parse(parts[0]);
            //    int B = Int32.Parse(parts[1]);


            for (int i = 0; i < N; i++)
            {
                line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                l[i].a = Int32.Parse(parts[0]);
                l[i].b = Int32.Parse(parts[1]);
                l[i].c = 0;
            }

            int s = 0;
            int a = 0;
            while (true)
            {
                bool flag = false;
                for (int i = 0; i < N; i++)
                {
                    if ((l[i].c < 2) && l[i].b <= s)
                    {
                        a++;
                        s = s + 2 - l[i].c;
                        l[i].c = 2;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }
                for (int i = 0; i < N; i++)
                {
                    if ((l[i].c < 1) && l[i].a <= s)
                    {
                        a++;
                        s = s + 1 - l[i].c;
                        l[i].c = 1;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }
                if (s >= 2 * N)
                {
                    env.swr.WriteLine("Case #{0}: {1}", T, a);
                    return;
                }
                else if (!flag)
                {
                    env.swr.WriteLine("Case #{0}: Too Bad", T);
                    return;
                }
            }





            //    env.swr.WriteLine("Case #{0}: {1}", T, xyzExp);
            //    //    for (int i = 1; i < (list.Count); i++)
        //    //    {
        //    //        if ((lastPrint != list[i])
        //    //            && (list[i - 1] == list[i]))
        //    //        {
        //    //            flag = true;
        //    //            lastPrint = list[i];
        //    //            env.swr.Write(" {0}", lastPrint);
        //    //        }
        //    //    }
        //    //    if (!flag)
        //    //    {
        //    //        env.swr.Write(" NONE", T);
        //    //    }
        //    //env.swr.WriteLine();
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
