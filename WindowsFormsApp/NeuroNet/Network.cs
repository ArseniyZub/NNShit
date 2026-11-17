using System.IO;

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


        public void Train(Network net) //backpropogation method
        {
            input_layer = new InputLayer(NetworkMode.Train);

            int epoches = 100; // эпохи обучения (Изменить!!!) около 5-6
            double tmpSumError;

            double[] errors;// вектор сигнала ошибки входного слоя
            double[] temp_gsums1; // вектор градиента 1-го скрытого слоя 
            double[] temp_gsums2; // вектор градиента 2-го скрытого слоя 


            e_error_avr = new double[epoches];

            for (int k = 0; k < epoches; k++)
            {
                e_error_avr[k] = 0;
                input_layer.Shuffling_Array_Rows(input_layer.TrainSet); // Перетасовка

                for (int i = 0; i < input_layer.TrainSet.GetLength(0); i++) // перебор обучающий примеров
                {
                    double[] tmpTrain = new double[15]; // обучающий оюраз 
                    for (int j = 0; j < tmpTrain.Length; j++)
                    {
                        tmpTrain[j] = input_layer.TrainSet[i, j + 1];
                    }

                    //Прямой ход 
                    ForwardPass(tmpTrain);

                    // Начисление ошибки 
                    tmpSumError = 0; // для какждого обучающего образа среднее значение ошибки этого образа оюнуляется 
                    errors = new double[fact.Length]; // переопределение массива сигнала ошибки выходного слоя 

                    for (int x = 0; x < errors.Length; x++)
                    {
                        if (x == input_layer.TrainSet[i, 0]) // если номер выходного нейрона совпадает с желаемым отклиеом
                        {
                            errors[x] = 1.0 - fact[x];
                        } 
                        else
                        {
                            errors[x] = -fact[x]; // 0.0 - fact[x]
                        }

                        tmpSumError += errors[x] * errors[x] / 2;
                    }

                    e_error_avr[k] += tmpSumError / errors.Length; //Суммарное значение энергии ошибки К-ой эпохи

                    // Обратный ход 
                    temp_gsums2 = output_layer.BackwardPass(errors);
                    temp_gsums1 = hidden_layer2.BackwardPass(temp_gsums2);
                    hidden_layer1.BackwardPass(temp_gsums1);
                }

                e_error_avr[k] /= input_layer.TrainSet.GetLength(0); // Среднее энергии значение ошибки
            }

            input_layer = null; // Уборка входного слоя 

            //Запись новых весов в память 
            hidden_layer1.WeightInitialize(MemoryMode.SET, nameof(hidden_layer1) + "_memory.csv");
            hidden_layer2.WeightInitialize(MemoryMode.SET, nameof(hidden_layer2) + "_memory.csv");
            output_layer.WeightInitialize(MemoryMode.SET, nameof(output_layer) + "_memory.csv");
        }
    }
}
