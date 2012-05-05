using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr_pa
{
    class Env
    {
        public StreamReader sr;
        public StreamWriter sw;
    }

    class Program
    {

        static char grepl(char from)
        {
            string[,] replacetable = {
                {" "    ," "},
                {"a"    ,"y"},
                {"b"    ,"h"},
                {"c"    ,"e"},
                {"d"    ,"s"},
                {"e"    ,"o"},
                {"f"    ,"c"},
                {"g"    ,"v"},
                {"h"    ,"x"},
                {"i"    ,"d"},
                {"j"    ,"u"},
                {"k"    ,"i"},
                {"l"    ,"g"},
                {"m"    ,"l"},
                {"n"    ,"b"},
                {"o"    ,"k"},
                {"p"    ,"r"},
                {"q"    ,"z"},
                {"r"    ,"t"},
                {"s"    ,"n"},
                {"t"    ,"w"},
                {"u"    ,"j"},
                {"v"    ,"p"},
                {"w"    ,"f"},
                {"x"    ,"m"},
                {"y"    ,"a"},
                {"z"    ,"q"},
            };

            for (int i = 0; i < replacetable.GetLength(0); i++)
            {
                if (from.Equals(replacetable[i,0][0])) {
                    return replacetable[i,1][0];
                }
            }
            return '*';
        }

        static void pAcore(long t, Env env)
        {

            string line_from = env.sr.ReadLine();
            string line_to = "";
            int length = line_from.Length;
            for (int i = 0; i < length; i++) {
                char char_from = line_from[i];
                char char_to = grepl(char_from);
                line_to += char_to;
                Console.WriteLine("{0}{1}{2}", char_from, char_to, line_to);
            }
            env.sw.WriteLine("Case #{0}: {1}", t, line_to);



/*
            int k;
            int[] e;
            {

                string line = env.sr.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                k = Int32.Parse(parts[0]);
                e = new int[k];
            }
            {
                string line = env.sr.ReadLine();
                string[] parts = line.Split(Env.delim, StringSplitOptions.None);
                //Console.WriteLine("line\"{0}\" => {1}", line, parts.Length);
                for (int i = 0; i < k; i++)
                {
                    int ele = Int32.Parse(parts[i]);
                    e[i] = ele;
                }
            }
            Array.Sort(e);
            //Console.WriteLine("e{0},e{1},e{2}..", e[0], e[1], e[2]);

            double w = 0;
            w += ((double)e[0]) * e[1];
            Console.WriteLine("e{0}:{1},e{2}:{3}..", 0, e[0], 1, e[1]);
            for (int i = 0; i < (k - 2); i++)
            {
                w += ((double)e[i]) * e[i + 2];
                Console.WriteLine("e{0}:{1},e{2}:{3}..", i, e[i], i + 2, e[i + 2]);
            }
            Console.WriteLine("e{0}:{1},e{2}:{3}..", k - 2, e[k - 2], k - 1, e[k - 1]);
            w += ((double)e[k - 2]) * e[k - 1];

            w *= Math.Sin(Math.PI * 2.0 / k) / 2;
            Console.WriteLine("Case #{0}: {1}", t, w);
            env.sw.WriteLine("Case #{0}: {1}", t, w);
*/
        }

        static void paLoop(Env env)
        {
            string line = env.sr.ReadLine();
            long t = Int64.Parse(line);
            for (long i = 1; i <= t; i++)
            {
                //Console.WriteLine("case{0}", i);
                pAcore(i, env);
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

            paLoop(env);

            sw.Close();
        }
    }
}
