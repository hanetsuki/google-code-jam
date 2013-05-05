using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace sol_prob_b
{

    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader srd;
        public StreamWriter swr;
    }

    class sol_prob_b
    {

        static bool check(int N, int X, int Y, int flag)
        {
            //       V even[20] = 2
            //      < >
            //     < X   odd[19] = 1
            //---< >< X >
            int center = 1; //(最初のダイヤ)
            int [,] side = new int[2,N+2];

            for (int i = 0; i < (N - 1); i++) {
                int direction;
                // 左の方が高い
                if (side[0,1] >= center) {
                    // 右の方が高い
                    if (side[1,1] >= center) {
                        center++;
                        continue;
                    }
                    else {
                        direction = 0;
                    }
                } else {
                    // 右の方が高い
                    if (side[1,1] >= center) {
                        direction = 1;
                    }
                    else {
                        direction = (0 != (1 & (flag >> i))) ? 1 : 0;
                    }
                }
                for (int j = 1;; j++) {
                    try
                    {
                        if ((j % 2) != 0) {
                            //奇数モード
                            if (side[direction,j] < side[direction,j+1]) {
                                side[direction,j]++;
                                break;
                            }
                        }
                        else {
                            //偶数モード
                            if (side[direction,j] <= side[direction,j+1]) {
                                side[direction,j]++;
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("N:{0}", N);
                        Console.WriteLine("j:{0}", j);
                        Console.WriteLine("length:{0}", side.Length);
                        Console.WriteLine(e.Message);
                        throw e;
                    }
                }
            }
            
            int target;
            if (X == 0) {
                target = center;
            }
            else if (Math.Abs(X) < N) {
                if (X < 0) {
                    target = side[0,-X];
                }
                else {
                    target = side[1,X];
                }
            }
            else {
                target = 0;
            }
            return (target > (Y / 2));
        }

        static double solve(int N, int X, int Y) {
            int nTest = 2 << (N - 1);
            int succ = 0;
            for (int i = 0; i < nTest; i++) {
                bool flag = check(N, X, Y, i);
                if (flag) {
                    succ++;
                }
            }
            return (double)succ / nTest;
#if false
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
#endif
        }

        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);

            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int N = Int32.Parse(parts[0]);
            int X = Int32.Parse(parts[1]);
            int Y = Int32.Parse(parts[2]);
            double result = solve(N, X, Y);
            env.swr.WriteLine("Case #{0}: {1}", T, result);
#if false
#endif
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
            sol_prob_b.probLoop(env);

            env.srd.Close();
            env.swr.Close();
        }

    }
}
