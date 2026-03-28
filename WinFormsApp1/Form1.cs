using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.WinForms;
using System.Media;
using System.Xml;
using static OpenTK.Graphics.OpenGL.GL;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        WavFile? wavFile = null;

        public Form1()
        {
            InitializeComponent();
            
            this.AllowDrop = true; 
            
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
        }
        
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        
                if (files.Length > 0 && files[0].EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy; 
                    return;
                }
            }
            
            e.Effect = DragDropEffects.None;
        }
        
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
    
            if (files.Length > 0)
            {
                string wavFilePath = files[0]; 
                
                wavFile = new WavFile(wavFilePath);
                PrintData(wavFile);
                
                Print_Click(null, null); 
            }
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
            else
                return;
            wavFile = new WavFile(wavFilePath);
            PrintData(wavFile);
            Print_Click(sender, e);
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
                
                sig.Data.Period = 1.0 / wavFile.samplePerSecond; 
                
                double totalTime = (double)numSamples / wavFile.samplePerSecond;
                formsPlot1.Plot.Axes.SetLimits(0, totalTime, -32768, 32767);
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
                
                sig.Data.Period = wavFile.shift;
                
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
                
                sig.Data.Period = wavFile.shift;
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
                sig.Data.Period = wavFile.shift;
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
        }

        private void Silence_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                double steThreshold = wavFile.SetSTEThreshold();
                double zcrThreshold = wavFile.SetZCRThreshold();
                //double steThreshold = 2000;
                //double zcrThreshold = 200;
                List<double> steValues = wavFile.CalculateVolume();
                List<double> zcrValues = wavFile.CalculateZCR();
                List<(int, int)> isSilent = new List<(int, int)>();
                bool isCurrentlySilent = false;
                int start = -1;
                int end = -1;
                for (int i = 0; i < steValues.Count; i++)
                {
                    if (steValues[i] < 5 * steThreshold)
                    {
                        if (zcrValues[i] > 3 * zcrThreshold)
                        {
                            if (isCurrentlySilent)
                            {
                                end = i;
                                isCurrentlySilent = false;
                                isSilent.Add((start, end));
                            }
                        }
                        else if(!isCurrentlySilent)
                        {
                            start = i;
                            isCurrentlySilent = true;
                        }
                    }
                    else if (isCurrentlySilent)
                    {
                        end = i;
                        isCurrentlySilent = false;
                        isSilent.Add((start, end));
                    }
                }
                if(isCurrentlySilent)
                {
                    end = steValues.Count;
                    isSilent.Add((start, end));
                }


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
                sig.Data.Period = 1.0 / wavFile.samplePerSecond;
                
                double totalTime = (double)numSamples / wavFile.samplePerSecond;
                formsPlot1.Plot.Axes.SetLimits(0, totalTime, -32768, 32767);


                double ratio = (double)values.Length / steValues.Count;
                int minSilenceFrames = 5;
                foreach (var (s, r) in isSilent)
                {
                    if ((r - s) >= minSilenceFrames)
                    {
                        double leftTime = s * wavFile.shift;
                        double rightTime = r * wavFile.shift;
                        
                        if (rightTime > totalTime) rightTime = totalTime;

                        var hSpan = formsPlot1.Plot.Add.HorizontalSpan(leftTime, rightTime);
                        hSpan.FillColor = ScottPlot.Colors.Red.WithAlpha(100);
                    }
                }
                formsPlot1.Refresh();
            }
        }

        private void domFreq_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                double[] values;
                if (autcorRadio.Checked)
                {
                    values = wavFile.Autocorrelation().ToArray();
                }
                else
                {
                    values = wavFile.AMDF().ToArray();
                }
                formsPlot1.Plot.Clear();
                var sig = formsPlot1.Plot.Add.Signal(values);
                sig.Data.Period = wavFile.shift;
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
        }
    }
}
