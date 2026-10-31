namespace WindowsFormsApp.NeuroNet
{
    class Network
    {
        private InputLayer input_layer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(70, 15, NeuronType.Hidden, nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(32, 70, NeuronType.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(10, 32, NeuronType.Output, nameof(output_layer));

        private double[] fact = new double[10]; // массив фактического вывода сети 
        private double[] e_error_avr; // среднее значение энергии огибки эпохи обучения

        public double[] Fact { get => fact; } // массив фактического вывода сети
        public double[] E_error_avr { get => e_error_avr; set => e_error_avr = value; }

        public Network() { }

        // прямой проход 
        public void ForwardPass(double[] netInput)
        {
            hidden_layer1.Data = netInput;
            hidden_layer1.Recognize(null, hidden_layer2);
            hidden_layer2.Recognize(null, output_layer);
            output_layer.Recognize(this, null);
        }


    }
}
