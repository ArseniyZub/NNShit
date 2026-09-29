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
            return new double[1, 1];
        }

    }
}
