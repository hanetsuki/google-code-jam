using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

// バグってます
// (どこで諦める(残り全捨てに入る)のが最適か、の情報を保存してないので、
// 最適な諦めポイントを見逃す場合があります。

namespace sol_prob_a
{

    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }

    class sol_prob_a
    {
        static int[] readArray(int length, Env env)
        {
            int[] array = new int[length];
            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);

            // 読み出し
            for (int i = 0; i < length; i++)
            {
                array[i] = Int32.Parse(parts[i]);
            }

            return array;
        }

        static int solve(int A, int N, int[] moteSize) {
            if (A <= 1) {
                //成長ができない場合 ->
                return N;
            }
            int spentCost = 0;
            int a = A;
            for (int i = 0; i < N; i++) {
                int leftMote = N - i;   //<残ったMoteの数
                int nextMote = moteSize[i]; //<次のMoteのサイズ

                int cost2 = 0;
                // 食えるまで成長する
                while (a <= nextMote) {
                    cost2++;
                    a += a - 1;
                }

                if (cost2 >= leftMote) {
                    return spentCost + leftMote;
                }
                spentCost += cost2;
                a += nextMote;
            }
            return spentCost;
        }

        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);

            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int A = Int32.Parse(parts[0]);
            int N = Int32.Parse(parts[1]);
            int[] moteSize = readArray(N, env);
            Array.Sort(moteSize);
            Console.WriteLine("N[{0}] = {1}", N-1, moteSize[N-1]);
            int result = solve(A, N, moteSize);
            env.swr.WriteLine("Case #{0}: {1}", T, result);
        }

        public static void probLoop(Env env)
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
    }

    class main
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }
            Env env = new Env();

            try
            {
                env.srd = new StreamReader(
                    args[0], Encoding.GetEncoding("Shift_JIS"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                env.swr = new StreamWriter(
                    args[1], false, Encoding.GetEncoding("Shift_JIS"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            sol_prob_a.probLoop(env);

            env.srd.Close();
            env.swr.Close();
        }

    }
}
