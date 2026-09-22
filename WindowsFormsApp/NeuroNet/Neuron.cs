using System;
using static System.Math;

namespace WindowsFormsApp.NeuroNet
{
    class Neuron
    {
        private double[] inputs;
        private double[] weights;
        private double output;
        private double derivative;
        private NeuronType type;


        // константы для активации
        private double a = 1.7159;
        private double b = 2.0/3.0;

        // свойства
        public double[] Weights
        {
            get => weights; 
            set => weights = value;
        }

        public double[] Inputs
        {
            get => inputs;
            set => inputs = value;
        }

        public double Output
        {
            get => output;
        }

        public double Derivative
        {
            get => derivative;
        }

        //переделать Я
        //public Neuron(double[] memoryWeights, NeuronType typeNeuron)
        //{
        //    type = typeNeuron;
        //    weights = memoryWeights;
        //}

        public Neuron(double[] weights, NeuronType type)
        {
            this.weights = weights;
            this.type = type;
        }

        public void Activator(double[] i)
        {
            inputs = i;
            double sum = weights[0];

            for (int j = 0; j < inputs.Length; j++)
            {
                sum += inputs[j]*weights[j + 1];
            }

            switch (type)
            {
                case NeuronType.Hidden:
                    output = ScaledTanh(sum);
                    derivative = ScaledTanhDerivative(sum);
                    break;

                case NeuronType.Output:
                    output = Exp(sum);
                    // derivative = Tanh
                    break;
            }
        }

        // написать свою функцию активации и производной
        public double ScaledTanh(double sum)
        {
            double bx = b*sum;

            // Безопасный расчет базового тангенса без встроенных методов
            if (bx > 20.0)
            {
                return a*1.0;
            }

            if (bx < -20.0)
            {
                return a*(-1.0);
            }

            double expPos = Math.Exp(bx);
            double expNeg = 1.0/expPos;

            double baseTanh = (expPos - expNeg)/(expPos + expNeg);

            return a*baseTanh;
        }

        public double ScaledTanhDerivative(double sum)
        {
            double bx = b*sum;
            double baseTanh;

            // Считаем внутренний tanh вручную
            if (bx > 20.0)
            {
                baseTanh = 1.0;
            }
            else if (bx < -20.0)
            {
                baseTanh = -1.0;
            }
            else
            {
                double expPos = Math.Exp(bx);
                double expNeg = 1.0/expPos;
                baseTanh = (expPos - expNeg)/(expPos + expNeg);
            }

            // Вычисляем производную
            return a*b*(1.0 - baseTanh*baseTanh);
        }
    }
}
