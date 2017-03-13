using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace NN
{
    public class Neuron
    {
        public double[] _in;
        public double[] _out;
        public double[,] _wio;
        double[] _lim;
        double _speed = 0.1;



        public Neuron(int inputs, int outputs, double[] limits, double speed)
        {
            _in = new double[inputs];
            _out = new double[outputs];
            _wio = new double[inputs, outputs];
            SetWeights();
            _lim = limits;
            Array.Sort(_lim);
            _speed = speed;
        }

        void SetWeights()
        {
            Random r = new Random();

            for(int i = 0; i < _in.Length; i++)
            {
                for(int j = 0; j < _out.Length; j++)
                {
                    _wio[i, j] = r.NextDouble() * 0.3 + 0.1;
                }
            }
        }

        public void SetIn(double[] IN)
        {
            if(_in.Length == IN.Length)
            {
                _in = IN;
            }
            else
            {
                for (int i = 0; i < _in.Length; i++)
                {
                    _in[i] = IN[i];
                }
            }
        }

        public void Out()
        {
            for(int i = 0; i < _out.Length; i++)
            {
                _out[i] = 0;
                for(int j = 0; j < _in.Length; j++)
                {
                    _out[i] += _in[j] * _wio[j, i];
                }

                double o = 0;
                for(int j = 1; j < _lim.Length; j++)
                {
                    if(_out[i] > _lim[j] / 2)
                    {
                        o = _lim[j];
                    }
                }
                _out[i] = o;
            }
        }

        public void Study(double[] error)
        {
            for(int i = 0; i < _in.Length; i++)
            {
                for(int j = 0; j < _out.Length; j++)
                {
                    _wio[i, j] += _speed * _in[i] * error[j];
                }
            }
        }
    }
}
