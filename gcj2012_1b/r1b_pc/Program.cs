using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace pc
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }
    //struct subset {
    //    public int size;
    //    public bool[] used = new bool[500];
    //    public subset(int size)
    //    {
    //        this.size =size;
    //        for (int i = 0; i < size; i++)
    //        {
    //            used[i] = false;
    //        }
    //    }
    //}
    class Program
    {
        static void probCore(long T, Env env)
        {
            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            ulong A = UInt64.Parse(parts[0]);
            ulong[] n = new ulong[A];

            for (ulong i = 0; i < A; i++)
            {
                n[i] = UInt64.Parse(parts[i + 1]);
            }

            //subset current = new subset(A);
            bool[] current = new bool[A];

            current[0] = true;
            for (ulong i = 1; i < A; i++)
            {
                current[i] = false;
            }
            Dictionary<ulong, bool[]> dict = new Dictionary<ulong, bool[]>();
            while (true)
            {
                //subsetのsumを出す。
                ulong subsum = 0;
                for (ulong i = 0; i < A; i++)
                {
                    if (current[i])
                    {
                        subsum = subsum + n[i];
                    }
                }
                if (dict.ContainsKey(subsum))
                {
                    //見つかった
                    env.swr.WriteLine("Case #{0}:", T);
                    {
                        bool flag = false;
                        for (ulong i = 0; i < A; i++)
                        {
                            if (current[i])
                            {
                                if (flag)
                                {
                                    env.swr.Write(" {0}", n[i]);
                                }
                                else
                                {
                                    env.swr.Write("{0}", n[i]);
                                    flag = true;
                                }
                            }
                        }
                    }
                    env.swr.WriteLine();
                    current = dict[subsum];
                    {
                        bool flag = false;
                        for (ulong i = 0; i < A; i++)
                        {
                            if (current[i])
                            {
                                if (flag)
                                {
                                    env.swr.Write(" {0}", n[i]);
                                }
                                else
                                {
                                    env.swr.Write("{0}", n[i]);
                                    flag = true;
                                }
                            }
                        }
                    }
                    env.swr.WriteLine();
                    return;
                }
                else
                {
                    bool[] current2 = new bool[A];
                    current.CopyTo(current2, 0);
                    dict.Add(subsum, current2);
                    bool carry = true;
                    for (ulong i = 0; i < A; i++)
                    {
                        if (carry)
                        {
                            carry = current[i];
                            current[i] = !current[i];
                        }
                    }
                    if (carry)
                    {
                        env.swr.WriteLine("Case #{0}: Impossible", T);
                    }
                }
            }
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
