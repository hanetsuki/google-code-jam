using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pr_pc
{
    class Env
    {
        static public string[] delim = { " ", "\t" };
        public StreamReader sr;
        public StreamWriter sw;
    }

    class Program
    {

        static uint pcCoreSub(uint n, uint a, uint b, uint keta, Dictionary<uint, uint> dict)
        {
            uint rv;

            if (dict.ContainsKey(n))
            {
                rv = dict[n] + 1u;
            }
            else
            {
                rv = 0u;
            }

            uint m = n;
            uint msdfact = (uint)System.Math.Pow(10, keta - 1);

            for (uint i = 0; i < keta; i++)
            {
                m = (m / 10) + (m % 10) * msdfact;
                if ((n <= m) && (m <= b))
                dict[m] = rv;
            }

            return rv;
        }

        static uint msdKeta(uint n)
        {
            uint r = 0;
            while (n > 0) {
                r++;
                n = n / 10;
            }
            return r;
        }

        static void pcCore(long t, Env env)
        {
            uint a;
            uint b;
            {
                string line = env.sr.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                a = UInt32.Parse(parts[0]);
                b = UInt32.Parse(parts[1]);
            }

            uint res = 0;
            Dictionary<uint, uint> dict = new Dictionary<uint, uint>();
            uint keta = msdKeta(b);

            for (uint i = a; i <= b; i++)
            {
                res = res + pcCoreSub(i, a, b, keta, dict);
            }
            env.sw.WriteLine("Case #{0}: {1}", t, res);
        }

        static void pcLoop(Env env)
        {
            string line = env.sr.ReadLine();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
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

            pcLoop(env);

            sw.Close();
        }
    }
}
