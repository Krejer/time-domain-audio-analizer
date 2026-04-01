using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.WinForms;
using System.Media;
using System.Xml;
using static OpenTK.Graphics.OpenGL.GL;

namespace WinFormsApp1
{
    public enum DisplayMode
    {
        Signal,
        STE,
        Volume,
        ZCR,
        F0_Autocorr,
        F0_AMDF
    }
    public partial class Form1 : Form
    {
        WavFile? wavFile = null;

        private DisplayMode currentMode = DisplayMode.Signal;

        public Form1()
        {
            InitializeComponent();

            this.AllowDrop = true;

            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
        }

        private void UpdatePlot()
        {
            if (wavFile == null) return;

            formsPlot1.Plot.Clear();

            switch (currentMode)
            {
                case DisplayMode.Signal:
                    DrawSignal();
                    break;
                case DisplayMode.STE:
                    DrawSTE();
                    break;
                case DisplayMode.Volume:
                    DrawVolume();
                    break;
                case DisplayMode.ZCR:
                    DrawZCR();
                    break;
                case DisplayMode.F0_Autocorr:
                    DrawF0_Autocor();
                    break;
                case DisplayMode.F0_AMDF:
                    DrawF0_AMDF();
                    break;
            }

            if (chkShowSilence.Checked)
            {
                DrawOverlay(wavFile.GetSilenceFrames(), ScottPlot.Colors.Red.WithAlpha(100));
            }

            if (chkVoiced.Checked)
            {
                DrawOverlay(wavFile.GetVoicedFrames(), ScottPlot.Colors.Green.WithAlpha(100));
            }

            if (chkUnvoiced.Checked)
            {
                DrawOverlay(wavFile.GetUnvoicedFrames(), ScottPlot.Colors.Blue.WithAlpha(100));
            }
            if(chkSpeech.Checked)
            {
                List<(int start, int end)> speechFrames = ClipLevel.DetectSpeech(wavFile);
                List<(int start, int end)> silenceFrames = wavFile.GetSilenceFrames();
                List<(int start, int end)> finalFrames = ClipLevel.SubtractIntervals(speechFrames, silenceFrames);
                DrawOverlay(finalFrames, ScottPlot.Colors.Cyan.WithAlpha(100));
            }
            if (chkShowSilence.Checked)
            {
                DrawOverlay(wavFile.GetSilenceFrames(), ScottPlot.Colors.Red.WithAlpha(100));
            }

            formsPlot1.Refresh();
        }

        private void DrawSignal()
        {
            int numSamples = wavFile.data.Length / wavFile.blockAlign;
            double[] values = new double[numSamples];

            for (int i = 0; i < numSamples; i++)
            {
                int byteIndex = i * wavFile.blockAlign;
                short sample = BitConverter.ToInt16(wavFile.data, byteIndex);
                values[i] = sample;
            }

            var sig = formsPlot1.Plot.Add.Signal(values);
            sig.Data.Period = 1.0 / wavFile.samplePerSecond;

            double totalTime = (double)numSamples / wavFile.samplePerSecond;
            formsPlot1.Plot.Axes.SetLimits(0, totalTime, -32768, 32767);
        }

        private void DrawSTE()
        {
            var values = wavFile.CalculateSTE().ToArray();
            var sig = formsPlot1.Plot.Add.Signal(values);

            sig.Data.Period = wavFile.shift;

            formsPlot1.Plot.Axes.AutoScale();

        }

        private void DrawVolume()
        {
            var values = wavFile.CalculateVolume().ToArray();
            var sig = formsPlot1.Plot.Add.Signal(values);

            sig.Data.Period = wavFile.shift;

            formsPlot1.Plot.Axes.AutoScale();
        }

        private void DrawZCR()
        {
            var values = wavFile.CalculateZCR().ToArray();
            var sig = formsPlot1.Plot.Add.Signal(values);

            sig.Data.Period = wavFile.shift;

            formsPlot1.Plot.Axes.AutoScale();
        }

        private void DrawF0_Autocor()
        {
            var values = wavFile.FundFreq_Autocorr().ToArray();
            var sig = formsPlot1.Plot.Add.Signal(values);

            sig.Data.Period = wavFile.shift;

            formsPlot1.Plot.Axes.AutoScale();
        }

        private void DrawF0_AMDF()
        {
            var values = wavFile.FundFreq_AMDF().ToArray();
            var sig = formsPlot1.Plot.Add.Signal(values);

            sig.Data.Period = wavFile.shift;

            formsPlot1.Plot.Axes.AutoScale();
        }

        private void signalButton_Click(object sender, EventArgs e)
        {
            currentMode = DisplayMode.Signal;
            UpdatePlot();
        }

        private void steButton_Click(object sender, EventArgs e)
        {
            currentMode = DisplayMode.STE;
            UpdatePlot();
        }

        private void volumeButton_Click(object sender, EventArgs e)
        {
            currentMode = DisplayMode.Volume;
            UpdatePlot();
        }

        private void zcrButton_Click(object sender, EventArgs e)
        {
            currentMode = DisplayMode.ZCR;
            UpdatePlot();
        }

        private void domFreq_Click(object sender, EventArgs e)
        {
            currentMode = autcorRadio.Checked ? DisplayMode.F0_Autocorr : DisplayMode.F0_AMDF;
            UpdatePlot();
        }

        private void DrawOverlay(List<(int start, int end)> frames, ScottPlot.Color color)
        {
            int minFrames = 5;
            foreach (var (s, r) in frames)
            {
                if ((r - s) >= minFrames)
                {
                    double leftTime = s * wavFile.shift;
                    double rightTime = r * wavFile.shift;

                    var hSpan = formsPlot1.Plot.Add.HorizontalSpan(leftTime, rightTime);
                    hSpan.FillColor = color;
                }
            }
        }


        private void loadButton_Click(object sender, EventArgs e)
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
            signalButton_Click(sender, e);
            var vdr  = ClipLevel.CalculateVDR(wavFile);
            var hzcrr = ClipLevel.CalculateHZCRR(wavFile);
            var entropy = ClipLevel.CalculateEnergyEntropy(wavFile);
            var lster = ClipLevel.CalculateLSTER(wavFile);
            var volumeUndulation = ClipLevel.CalculateVolumeUndulation(wavFile);
            label1.Text = $"VDR: {vdr:F2}   HZCRR: {hzcrr:F4}   Energy Entropy: {entropy:F4}   LSTER: {lster:F4}   Volume Undulation: {volumeUndulation:F4}";
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
                signalButton_Click(null, null);
            }
        }

        private void chkShowSilence_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlot();
        }



        private void autcorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (currentMode != DisplayMode.F0_AMDF && currentMode != DisplayMode.F0_Autocorr) return;
            domFreq_Click(sender, e);
        }

        private void chkVoiced_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlot();
        }

        private void chkUnvoiced_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlot();
        }
        private void ClipLevelSaver_Click(object sender, EventArgs e)
        {
            if (wavFile != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = $"{wavFile.fileName}_clip_level_parameters.txt";
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.ShowDialog();
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.WriteLine($"VDR: {ClipLevel.CalculateVDR(wavFile)}");
                    sw.WriteLine($"Volume Undulation: {ClipLevel.CalculateVolumeUndulation(wavFile)}");
                    sw.WriteLine($"LSTER: {ClipLevel.CalculateLSTER(wavFile)}");
                    sw.WriteLine($"Energy Entropy: {ClipLevel.CalculateEnergyEntropy(wavFile)}");
                    sw.WriteLine($"HZCRR : {ClipLevel.CalculateHZCRR(wavFile)}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chkSpeech_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlot();
        }
    }
}
