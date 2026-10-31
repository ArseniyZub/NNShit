namespace WindowsFormsApp.NeuroNet
{
    class OutputLayer : Layer
    {
        public OutputLayer(int non, int nopn, NeuronType nt, string type) :
            base(non, nopn, nt, type) { }

        public override void Recognize(Network net, Layer nextLayer)
        {
            double sum = 0;
            for (int i = 0; i < neurons.Length; i++)
            {
                sum += neurons[i].Output;
            }

            for (int i = 0; i < neurons.Length; i++)
            {
                net.Fact[i] = neurons[i].Output/sum;
            }
        }

        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numOfPrevNeurons];

            // градиентные спуски
            for (int j = 0; j < numOfPrevNeurons; j++) 
            {
                double sum = 0;
                for (int k = 0; k < numOfNeurons; k++)
                {
                    sum += neurons[k].Weights[j + 1] * errors[k];
                }

                gr_sum[j] = sum;
            }

            // цикл коррекции ошибок
            for (int i = 0; i < numOfNeurons; i++)
            {
                for (int n = 0; n < numOfPrevNeurons + 1; n++)
                {
                    double deltaW;

                    if (n == 0)
                    {
                        deltaW = momentum*lastDeltaWeights[i, 0] + 
                            learningRate * errors[i];
                    }
                    else
                    {
                        deltaW = momentum*lastDeltaWeights[i, n] +
                            learningRate * neurons[i].Inputs[n - 1] * errors[i];
                    }

                    lastDeltaWeights[i, n] = deltaW;
                    neurons[i].Weights[n] += deltaW; // коррекция весов
                }
            }

            return gr_sum;
        }
    }
}
