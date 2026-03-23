using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.WinForms;
using System.Media;
using System.Xml;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        WavFile? wavFile = null;
        readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WAV files (*.wav)|*.wav";
            string wavFilePath = string.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                wavFilePath = ofd.FileName;
            }
            wavFile = new WavFile(wavFilePath);
            PrintData(wavFile);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        internal void PrintData(WavFile wavFile)
        {

        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                int numSamples = wavFile.data.Length / wavFile.blockAlign;
                double[] values = new double[numSamples];

                for (int i = 0; i < numSamples; i++)
                {
                    int byteIndex = i * wavFile.blockAlign;
                    short sample = BitConverter.ToInt16(wavFile.data, byteIndex);

                    values[i] = sample;
                }
                formsPlot1.Plot.Clear();
                var sig = formsPlot1.Plot.Add.Signal(values);
                formsPlot1.Plot.Axes.SetLimits(0, numSamples, -32768, 32767);
                formsPlot1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                var values = wavFile.CalculateSTE().ToArray();
                formsPlot1.Plot.Clear();
                var sig = formsPlot1.Plot.Add.Signal(values);
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                var values = wavFile.CalculateVolume().ToArray();
                formsPlot1.Plot.Clear();
                var sig = formsPlot1.Plot.Add.Signal(values);
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                var values = wavFile.CalculateZCR().ToArray();
                formsPlot1.Plot.Clear();
                var sig = formsPlot1.Plot.Add.Signal(values);
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
        }
    }

}
