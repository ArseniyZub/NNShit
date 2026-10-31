using System;
using System.IO;

namespace WindowsFormsApp.NeuroNet
{
    class InputLayer
    {
        private double[,] trainSet;
        private double[,] testSet;

        public double[,] TrainSet { get => trainSet;  }
        public double[,] TestSet { get => testSet; }

        public InputLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpArrStr;
            string[] tmpStr;

            switch (nm) 
            {
                case NetworkMode.Train:
                    tmpArrStr = File.ReadAllLines(path + "train.txt");
                    trainSet = new double[tmpArrStr.Length, 16];

                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split(' ');

                        for (int j = 0; j < 16; j++)
                        {
                            trainSet[i, j] = double.Parse(tmpStr[j]);
                        }
                    }

                    break;

                case NetworkMode.Test:
                    tmpArrStr = File.ReadAllLines(path + "test.txt");
                    testSet = new double[tmpArrStr.Length, 16];

                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split(' ');

                        for (int j = 0; j < 16; j++)
                        {
                            testSet[i, j] = double.Parse(tmpStr[j]);
                        }
                    }

                    break;
            }
        }
    }
}
