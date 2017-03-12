using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] IN = new double[][]
            {
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 },
                new double[] { 0, 0 }
            };
            double[] OUT = new double[]
            {
                1,
                1,
                1,
                0
            };

            Neuron n = new Neuron(2, 1, new double[] { 0, 1 }, 0.1);
            double ge = 0;
            do
            {
                ge = 0;
                for(int i = 0; i < IN.Length; i++)
                {
                    n.SetIn(IN[i]);
                    n.Out();
                    List<double> le = new List<double>();
                    for(int j = 0; j < n._out.Length; j++)
                    {
                        le.Add(OUT[i] - n._out[j]);
                        ge += Math.Abs(le[j]);
                    }
                    n.Study(le.ToArray());
                }
            }
            while (ge > 0);
            Console.WriteLine("complate");
            foreach(var v in IN)
            {
                n.SetIn(v);
                n.Out();
                foreach(var vv in n._out)
                {
                    Console.WriteLine(vv);
                }
            }
            Console.ReadLine();

            IN = new double[][]
            {
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 },
                new double[] { 0, 0 }
            };
            OUT = new double[]
            {
                1,
                0.5,
                1.255,
                0
            };

            n = new Neuron(2, 8, new double[] { 0, 1.255, 0.5, 1 }, 0.1);
            Neuron nn = new Neuron(8, 1, new double[] { 0, 1.255, 0.5, 1 }, 0.1);

            ge = 0;
            do
            {
                Console.WriteLine(ge);
                ge = 0;
                for(int i = 0; i < IN.Length; i++)
                {
                    n.SetIn(IN[i]);
                    n.Out();
                    nn.SetIn(n._out);
                    nn.Out();

                    List<double> le = new List<double>();
                    foreach(double d in nn._out)
                    {
                        le.Add(OUT[i] - d);
                        ge += Math.Abs(OUT[i] - d);
                    }
                    List<double> he = new List<double>();

                    for(int j = 0; j < le.Count; j++)
                    {
                        for(int k = 0; k < nn._in.Length; k++)
                        {
                            he.Add(le[j] * nn._wio[k, j]);
                        }
                    }

                    nn.Study(le.ToArray());
                    n.Study(he.ToArray());

                }
            }
            while (ge > 0);

            Console.WriteLine("OK");
            foreach(var v in IN)
            {
                n.SetIn(v);
                n.Out();
                nn.SetIn(n._out);
                nn.Out();
                foreach(var vv in nn._out)
                {
                    Console.WriteLine(vv);
                }
            }

            Console.ReadLine();
        }
    }
}