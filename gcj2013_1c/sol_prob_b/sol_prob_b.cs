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

    struct move {
        public move (char c, int x, int y) {
            this.c = c;
            this.x = x;
            this.y = y;
        }
        readonly char    c;
        readonly int     x;
        readonly int     y;
    };

    class sol_prob_b
    {
        //readonly move[] m = new move[]{
        //    new move('E',   1,  0),
        //    new move('W',   -1, 0),
        //    new move('N',   0,  1),
        //    new move('S',   0,  -1)
        //};
        static string solve(int X, int Y)
        {
            //      Y+          N
            //  X-      X+  W       E
            //      Y-          S
            //for short
            return solveShort(X, Y);
        }
        static string solveShort(int X, int Y)
        {
            int absX = Math.Abs(X);
            int absY = Math.Abs(Y);
            string result = "";
            string step;
            if (X < 0) {
                step = "EW";
            }
            else  {
                step = "WE";
            }
            for (int x = 0; x < absX; x+=1) {
                result = result + step;
            }
            if (Y < 0) {
                step = "NS";
            }
            else if (Y > 0) {
                step = "SN";
            }
            for (int y = 0; y < absY; y+=1) {
                result = result + step;
            }
            return result;
        }

        static void probCore(long T, Env env)
        {
            Console.WriteLine("start#{0}", T);

            string line = env.srd.ReadLine();
            string[] parts = line.Split(Env.delim, StringSplitOptions.None);
            int X = Int32.Parse(parts[0]);
            int Y = Int32.Parse(parts[1]);
            string result = solve(X, Y);
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
            sol_prob_b.probLoop(env);

            env.srd.Close();
            env.swr.Close();
        }
    }
}
/*

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
#if false
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
#endif

        static bool isShiin(char c)
        {
            switch (c)
            {
                case 'a':
                case 'i':
                case 'u':
                case 'e':
                case 'o':
                    return false;
                default:
                    return true;
            }
        }

        static int solve(string name, int n)
        {
            char[] aCh = name.ToCharArray();
            int L = name.Length;
            int[]  aShiin = new int[L];
            int contShiin = 0;
            for (int i = 0; i < L; i++)
            {
                if (isShiin(aCh[i]))
                {
                    contShiin++;
                }
                else
                {
                    contShiin = 0;
                }
                aShiin[i] = contShiin;
                //Console.WriteLine("i:{0} ach:{1} cs:{2}", i, aCh[i], contShiin);
            }

            //
            int result = 0;
            int e = -1;
            for (int s = 0; s < L; s++)
            {
                int o;
                if (s > 0)
                {
                    o = aShiin[s-1];
                }
                else
                {
                    o = 0;
                }
                // skip phase
                if (s > e) {
                    for (e = s; e < L; e++)
                    {
                        int shiin = aShiin[e];
                        if (o > 0)
                        {
                            if (shiin == 0) {
                                o = 0;
                            }
                            else {
                                shiin -= o;
                            }
                        }
                        if (shiin >= n)
                        {
                            break;
                        }
                    }
                }
                //Console.WriteLine("s:{0} e:{1} l:{2}", s, e, L);
                result += L - e;
            }
            return result;
            //



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
            string name = parts[0];
            int n = Int32.Parse(parts[1]);
            int result = solve(name, n);
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
*/
