using System;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApp.NeuroNet
{
    abstract class Layer
    {
        protected string name_Layer;
        string pathDirWeights;
        protected string pathFileWeights;
        protected int numOfNeurons;
        protected int numOfPrevNeurons;
        protected const double learningRate = 0.05d;
        protected const double momentum = 0.05d;
        protected double[,] lastDeltaWeights;
        protected Neuron[] neurons;

        public double[] Data
        {
            set
            {
                for (int i = 0; i < numOfNeurons; i++)
                {
                    neurons[i].Activator(value);
                }
            }
        }

        //non = numOfNeurons
        //...
        protected Layer(int non, int nopn, NeuronType nt, string nm_Layer)
        {
            numOfNeurons = non;
            numOfPrevNeurons = nopn;
            neurons = new Neuron[numOfNeurons];
            name_Layer = nm_Layer;
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";

            lastDeltaWeights = new double[non, nopn + 1];
            double[,] Weights;
            
            if (File.Exists(pathFileWeights))
            {
                Weights = WeightInitialize(MemoryMode.GET, pathFileWeights);
            }
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                Weights = WeightInitialize(MemoryMode.INIT, pathFileWeights);
            }

            for (int i = 0; i < non; i++)
            {
                double[] tmp_weights = new double[nopn + 1]; // change nonp to nopn
                for (int j = 0; j < nopn + 1; j++)
                {
                    tmp_weights[j] = Weights[i, j];
                }
                neurons[i] = new Neuron(tmp_weights, nt);
            }
        }


        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ' ', ';' };
            string tmpStr; // временная строка для чтения
            string[] tmpStrWeights; // временный массив строк 
            double[,] weights = new double[numOfNeurons, numOfPrevNeurons + 1];

            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path);
                    string[] memory_element;
                    for (int i = 0; i < numOfNeurons; i++)
                    {
                        memory_element = tmpStrWeights[i].Split(delim); // разбтваем строку на элементы

                        for (int j = 0; j < numOfPrevNeurons + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_element[j].Replace(',', '.'), // преобразование строк с запятой в точку
                                System.Globalization.CultureInfo.InvariantCulture); // игнорирование культурных различий записи 
                        }
                    }
                    break;


                case MemoryMode.SET:
                    tmpStrWeights = new string[numOfNeurons];
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Файл" + name_Layer + "_memory.csv синаптические веса не найден." + 
                            "\nПосле нажатия ОК все файлы создадутся", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    for (int i = 0; i < numOfNeurons; i++)
                    {
                        tmpStr = neurons[i].Weights[0].ToString();
                        for (int j = 1; j < numOfPrevNeurons + 1; j++)
                        {
                            tmpStr += delim[0] + neurons[i].Weights[j].ToString();
                        }
                        tmpStrWeights[i] = tmpStr;
                    }

                    File.WriteAllLines(path, tmpStrWeights);
                    break;

                case MemoryMode.INIT:
                    MessageBox.Show("Файл" + name_Layer + "_memory.csv синаптических весов не найден\n" +
                        "После нажатия ОК создастья новый файл весов и нейросеть вернется к <<Новорожденному>> состоянию", 
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    Random random = new Random();
                    
                    double[] tmpArr = new double[numOfPrevNeurons + 1];// массив весов одного нейрона
                    tmpStrWeights = new string[numOfNeurons]; // массив строк синаптических весов

                    double std = 1.0 / Math.Sqrt(numOfPrevNeurons);
                    double a = Math.Sqrt(3.0) / std;

                    for (int i = 0; i < numOfNeurons; i++)
                    {
                        tmpStr = "";
                        for (int j = 0; j < numOfPrevNeurons + 1; j++)
                        {
                            tmpArr[j] = (2.0*random.NextDouble() - 1.0)*a; // случайная инициализация
                                                                         // синаптического веса от -0.01 до 0.01
                            if (j > 0)
                            {
                                tmpStr += ';';
                            }

                            tmpStr += tmpArr[j].ToString(System.Globalization.CultureInfo.InvariantCulture);
                        }
                        tmpStrWeights[i] = tmpStr;
                    }

                    File.WriteAllLines(path, tmpStrWeights);
                    break;
            }

            return weights;
            
        }

    }
}
