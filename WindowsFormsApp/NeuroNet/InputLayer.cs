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

        //Перестановка строк методом Фишера-Йетса.
        public void Shuffling_Array_Rows(double[,] arr)
        {
            Random rand = new Random();

            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int i = rows - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);

                for (int k = 0; k < cols; k++)
                {
                    double temp = arr[i, k];
                    arr[i, k] = arr[j, k];
                    arr[j, k] = temp;
                }
            }
        }
    }
}
