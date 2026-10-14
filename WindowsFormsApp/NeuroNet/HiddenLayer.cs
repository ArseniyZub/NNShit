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

        public override double[] BackwardPass(double[] stuff)
        {
            double[] gr_sum = new double[numOfPrevNeurons]; // локальный градиент
            return gr_sum;
        }
    }
}
