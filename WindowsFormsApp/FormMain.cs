using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class FormMain : Form
    {
        private double[] inputPixels;
        public FormMain()
        {
            InitializeComponent();
            inputPixels = new double[15];
        }

        private void change_button_color_Click(object sender, EventArgs e)
        {
            Button clicked_button = (Button)sender;
            if (clicked_button.BackColor == Color.Black)
            {
                clicked_button.BackColor = Color.White;
                inputPixels[clicked_button.TabIndex] = 0d;
            }
            else
            {
                clicked_button.BackColor = Color.Black; 
                inputPixels[clicked_button.TabIndex] = 1d;
            }
        }


        private void saveTrainButton_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "train.txt";
            string tmpStr = numericUpDown_NecessaryOutput.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }

            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }

        private void saveTestButton_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "test.txt";
            string tmpStr = numericUpDown_NecessaryOutput.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }

            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }

    }
}
