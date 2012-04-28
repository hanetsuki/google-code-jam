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
        //static int hikaku(
        //  KeyValuePair<string, int> kvp1,
        //  KeyValuePair<string, int> kvp2)
        //{
        //    return kvp1.Key.CompareTo(kvp2.Key);
        //}

        static void probCore(long T, Env env)
        {
            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int A = Int32.Parse(parts[0]);
            int B = Int32.Parse(parts[1]);

            line = env.srd.ReadLine();
            parts = line.Split(Env.delim, StringSplitOptions.None);

            double[] p = new double[A];
            double[] yExp = new double[A];
            for (int i = 0; i < A; i++)
            {
                p[i] = Double.Parse(parts[i]);
                yExp[i] = 
            }

            //戦略
            //スルーするX
            //  success (B-A)+1
            //  fail    (B-A)+1 + B+1
            //戻るY
            //  success (A-i)*2 + (B-A)+1
            //  fail    (A-i)*2 + (B-A)+1 + B+1
            //捨てるZ
            //  success B+2
            //  fail    B+2

            int x_success = (B - A + 1);
            int x_fail    = (B - A + 1) + B + 1;
            double xExp = p[A - 1] * x_success + (p[A - 1] - 1) * x_fail;
            double zExp = B + 2;
            double xzExp = xExp < zExp ? xExp : zExp;


            for (int i = 0; i < A; i++)
            {
                p[i] = Double.Parse(parts[i]);
            }



            
            //    string S = parts[1];
        //    int N = S.Length;

        //    List<uint> list = new List<uint>();
        //    list.Sort();
        //    bool flag = false;
        //    uint lastPrint = 0;
        //    env.swr.Write("Case #{0}:", T);
        //    for (int i = 1; i < (list.Count); i++)
        //    {
        //        if ((lastPrint != list[i])
        //            && (list[i - 1] == list[i]))
        //        {
        //            flag = true;
        //            lastPrint = list[i];
        //            env.swr.Write(" {0}", lastPrint);
        //        }
        //    }
        //    if (!flag)
        //    {
        //        env.swr.Write(" NONE", T);
        //    }
        //    env.swr.WriteLine();
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
