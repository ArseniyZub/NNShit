namespace WindowsFormsApp.NeuroNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, NeuronType nt, string type) :
            base(non, nopn, nt, type) { }

        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[numOfNeurons];
            for (int i = 0; i < numOfNeurons; i++)
            {
                hidden_out[i] = neurons[i].Output;
            }

            nextLayer.Data = hidden_out; // передача в следующий слой
        }

        public override double[] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = new double[numOfPrevNeurons]; // локальный градиент

            for (int j = 0; j < numOfPrevNeurons; j++)
            {
                double sum = 0;
                for (int k = 0; k < numOfNeurons; k++)
                {
                    sum += neurons[k].Weights[j + 1] * neurons[k].Derivative * gr_sums[k];
                }

                gr_sum[j] = sum;
            }

            for (int i = 0; i < numOfNeurons; i++)
            {
                for (int n = 0; n < numOfPrevNeurons + 1; n++)
                {
                    double deltaW;

                    if (n == 0)
                    {
                        deltaW = momentum * lastDeltaWeights[i, 0] + 
                            learningRate * neurons[i].Derivative * gr_sums[i];
                    } 
                    else
                    {
                        deltaW = momentum * lastDeltaWeights[i, n] + 
                            learningRate * neurons[i].Inputs[n - 1] * neurons[i].Derivative * gr_sums[i];
                    }

                    lastDeltaWeights[i, n] = deltaW;
                    neurons[i].Weights[n] += deltaW;
                }
            }

            return gr_sum;
        }
    }
}
