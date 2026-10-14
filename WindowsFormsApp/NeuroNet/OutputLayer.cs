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

        public override double[] BackwardPass(double[] stuff)
        {
            throw new System.NotImplementedException();
        }
    }
}
