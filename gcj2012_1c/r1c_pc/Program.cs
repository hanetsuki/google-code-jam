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
    class Program
    {
        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);
            int M, N;
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                M = Int32.Parse(parts[0]);
                N = Int32.Parse(parts[1]);
            }
            long[] a = new long[M];
            int[] A = new int[M];
            long[] b = new long[N];
            int[] B = new int[N];
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                for (int i = 0; i < M; i++)
                {
                    a[i] = Int64.Parse(parts[i * 2]);
                    A[i] = Int32.Parse(parts[i * 2 + 1]);
                }
            }
            {
                string line = env.srd.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                for (int i = 0; i < N; i++)
                {
                    b[i] = Int64.Parse(parts[i * 2]);
                    B[i] = Int32.Parse(parts[i * 2 + 1]);
                }
            }
            long best = 0;

            {
                //int[] splitpoint = new int[M];//default M
                int[] splitpoint = new int[3];//default M
                //次の要素を列挙する
                for (splitpoint[0] = 0; splitpoint[0] <= M; splitpoint[0]++)
                {
                    //次の要素を列挙する
                    for (splitpoint[1] = splitpoint[0]; splitpoint[1] <= M; splitpoint[1]++)
                    {
                        //次の要素を列挙する
                        splitpoint[2] = M;
                        {
                            long resion0;
                            if (M >= 1)
                            {
                                long subre = 0;
                                for (int i = 0; i < splitpoint[0]; i++)
                                {
                                    if (A[i] == B[0])
                                    {
                                        subre = subre + a[i];
                                    }
                                }
                                resion0 = b[0];
                                if (resion0 > subre)
                                {
                                    resion0 = subre;
                                }
                            }
                            else
                            {
                                resion0 = 0;
                            }
                            long resion1;
                            if (M >= 2)
                            {
                                long subre = 0;
                                for (int i = splitpoint[0]; i < splitpoint[1]; i++)
                                {
                                    if (A[i] == B[1])
                                    {
                                        subre = subre + a[i];
                                    }
                                }
                                resion1 = b[1];
                                if (resion1 > subre)
                                {
                                    resion1 = subre;
                                }
                            }
                            else
                            {
                                resion1 = 0;
                            }
                            long resion2;
                            if (M >= 3)
                            {
                                long subre = 0;
                                for (int i = splitpoint[1]; i < splitpoint[2]; i++)
                                {
                                    if (A[i] == B[2])
                                    {
                                        subre = subre + a[i];
                                    }
                                }
                                resion2 = b[2];
                                if (resion2 > subre)
                                {
                                    resion2 = subre;
                                }
                            }
                            else
                            {
                                resion2 = 0;
                            }
                            long current = resion0 + resion1 + resion2;
                            if (current > best)
                            {
                                best = current;
                            }
                        }
                    }
                }
            }

            env.swr.WriteLine("Case #{0}: {1}", T, best);
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
