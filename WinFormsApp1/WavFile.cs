using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class WavFile
    {
        public string riffChunkId;
        public int riffChunkSize;
        public string riffFormat;
        public string fmtChunkId;
        public int fmtChunkSize;
        public short audioFormat;
        public short numChannels;
        public int samplePerSecond;
        public double timeStep;
        public int avgBytesPerSec;
        public short blockAlign;
        public short bitsPerSample;
        public string dataChunkId;
        public int dataChunkSize;
        public byte[] data;
        public List<short> leftChannel { get; set; } = new List<short>();
        public List<short> rightChannel { get; set; } = new List<short>();
        double frameSize = 0.01;
        public double shift = 0.005;
        int frameSamples;
        int shiftSamples;

        private List<double>? _cachedVolume = null;
        private List<double>? _cachedZCR = null;
        private List<double>? _cachedSTE = null;
        private List<(int start, int end)>? _cachedSilenceFrames = null;
        private List<(int start, int end)>? _cachedVoicedFrames = null;
        private List<(int start, int end)>? _cachedUnvoicedFrames = null;

        public WavFile(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
            {
                return;
            }
            using (var file = File.Open(filePath, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(file))
            {
                riffChunkId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                riffChunkSize = reader.ReadInt32();
                riffFormat = Encoding.ASCII.GetString(reader.ReadBytes(4));

                if (riffChunkId != "RIFF" || riffFormat != "WAVE")
                {
                    MessageBox.Show("Wybrany plik nie jest prawidłowym plikiem WAV.");
                    return;
                }

                bool fmtFound = false;
                bool dataFound = false;

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string chunkId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int chunkSize = reader.ReadInt32();

                    if (chunkId == "fmt ")
                    {
                        fmtChunkId = chunkId;
                        fmtChunkSize = chunkSize;
                        audioFormat = reader.ReadInt16();
                        numChannels = reader.ReadInt16();
                        samplePerSecond = reader.ReadInt32();
                        avgBytesPerSec = reader.ReadInt32();
                        blockAlign = reader.ReadInt16();
                        bitsPerSample = reader.ReadInt16();
                        timeStep = 1.0 / samplePerSecond;

                        int extraBytes = chunkSize - 16;
                        if (extraBytes > 0)
                        {
                            reader.BaseStream.Seek(extraBytes, SeekOrigin.Current);
                        }
                        fmtFound = true;
                    }
                    else if (chunkId == "data")
                    {
                        dataChunkId = chunkId;
                        dataChunkSize = chunkSize;
                        data = reader.ReadBytes(chunkSize);
                        dataFound = true;
                        break;
                    }
                    else
                    {
                        reader.BaseStream.Seek(chunkSize, SeekOrigin.Current);
                    }
                }

                if (!fmtFound || !dataFound)
                {
                    MessageBox.Show("Nie znaleziono wymaganych sekcji (fmt/data). Plik może być uszkodzony.");
                    return;
                }
                if (bitsPerSample != 16)
                {
                    MessageBox.Show("Ten program obsługuje tylko pliki 16-bitowe.");
                    return;
                }

                int bytesPerSample = bitsPerSample / 8;
                for (int i = 0; i < data.Length; i += blockAlign)
                {
                    short leftSample = BitConverter.ToInt16(data, i);
                    leftChannel.Add(leftSample);

                    if (numChannels == 2)
                    {
                        short rightSample = BitConverter.ToInt16(data, i + bytesPerSample);
                        rightChannel.Add(rightSample);
                    }
                    else
                    {
                        rightChannel.Add(leftSample);
                    }
                }
                frameSamples = (int)(frameSize * samplePerSecond);
                shiftSamples = (int)(shift * samplePerSecond);
            }
        }

        public List<double> CalculateVolume()
        {
            if (_cachedVolume != null) return _cachedVolume;

            List<double> volValues = new List<double>();

            for (int i = 0; i <= leftChannel.Count - frameSamples; i += shiftSamples)
            {
                double ste = 0;
                for (int j = 0; j < frameSamples; j++)
                {
                    double valueToAdd = (leftChannel[i + j] * leftChannel[i + j] + rightChannel[i + j] * rightChannel[i + j]) / 2;
                    ste += valueToAdd;
                }
                ste /= frameSamples;
                ste = Math.Sqrt(ste);
                volValues.Add(ste);
            }
            _cachedVolume = volValues;
            return volValues;
        }

        public List<double> CalculateSTE()
        {
            if (_cachedSTE != null) return _cachedSTE;

            List<double> steValues = new List<double>();

            for (int i = 0; i <= leftChannel.Count - frameSamples; i += shiftSamples)
            {
                double vol = 0;
                for (int j = 0; j < frameSamples; j++)
                {
                    double valueToAdd = (leftChannel[i + j] * leftChannel[i + j] + rightChannel[i + j] * rightChannel[i + j]) / 2;
                    vol += valueToAdd;
                }
                vol /= frameSamples;
                steValues.Add(vol);
            }
            _cachedSTE = steValues;
            return steValues;
        }

        public List<double> CalculateZCR()
        {
            if (_cachedZCR != null) return _cachedZCR;

            List<double> zcrValues = new List<double>();

            for (int i = 0; i <= leftChannel.Count - frameSamples; i += shiftSamples)
            {
                double zcr = 0;
                for (int j = 1; j < frameSamples; j++)
                {
                    double left = Math.Abs(ClipLevel.Signum(leftChannel[i + j]) - ClipLevel.Signum(leftChannel[i + j - 1]));
                    double right = Math.Abs(ClipLevel.Signum(rightChannel[i + j]) - ClipLevel.Signum(rightChannel[i + j - 1])) / 2;
                    zcr += (left + right) / 2.0;
                }
                zcr *= (double)samplePerSecond / (2 * frameSamples);
                zcrValues.Add(zcr);
            }
            _cachedZCR = zcrValues;
            return zcrValues;
        }

        public double SetSTEThreshold()
        {
            List<double> steValues = CalculateVolume();
            double avg = 0;
            for (int i = 0; i < 10; i++)
            {
                avg += steValues[i];
            }
            return (avg / 10);
        }
        public double SetZCRThreshold()
        {
            List<double> zcrValues = CalculateZCR();
            double avg = 0;
            for (int i = 0; i < 10; i++)
            {
                avg += zcrValues[i];
            }
            return Math.Max((avg / 10.0), 50.0);
        }

        private void CalculateAllFrameTypes()
        {

            if (_cachedSilenceFrames != null) return;

            double baseSte = SetSTEThreshold();

            double speechZcrThreshold = 1500;

            List<double> steValues = CalculateVolume();
            List<double> zcrValues = CalculateZCR();

            _cachedSilenceFrames = new List<(int, int)>();
            _cachedVoicedFrames = new List<(int, int)>();
            _cachedUnvoicedFrames = new List<(int, int)>();

            bool inSilence = false; int startSilence = -1;
            bool inVoiced = false; int startVoiced = -1;
            bool inUnvoiced = false; int startUnvoiced = -1;

            for (int i = 0; i < steValues.Count; i++)
            {
                bool hasEnergy = steValues[i] > Math.Max(5 * baseSte, 50.0);

                bool isHighFreq = zcrValues[i] > speechZcrThreshold;

                bool isSilence = !hasEnergy;
                bool isUnvoiced = hasEnergy && isHighFreq;
                bool isVoiced = hasEnergy && !isHighFreq;

                if (isSilence)
                {
                    if (!inSilence)
                    {
                        startSilence = i;
                        inSilence = true;
                    }
                }
                else
                {
                    if (inSilence)
                    {
                        _cachedSilenceFrames.Add((startSilence, i));
                        inSilence = false;
                    }
                }

                if (isVoiced) 
                { 
                    if (!inVoiced) 
                    { startVoiced = i; 
                        inVoiced = true; 
                    } 
                }
                else 
                { 
                    if (inVoiced) 
                    { 
                        _cachedVoicedFrames.Add((startVoiced, i)); 
                        inVoiced = false; 
                    } 
                }

                if (isUnvoiced) 
                { 
                    if (!inUnvoiced) 
                    { 
                        startUnvoiced = i; 
                        inUnvoiced = true; 
                    } 
                }
                else 
                { 
                    if (inUnvoiced) 
                    { 
                        _cachedUnvoicedFrames.Add((startUnvoiced, i)); 
                        inUnvoiced = false; 
                    } 
                }
            }

            if (inSilence) _cachedSilenceFrames.Add((startSilence, steValues.Count));
            if (inVoiced) _cachedVoicedFrames.Add((startVoiced, steValues.Count));
            if (inUnvoiced) _cachedUnvoicedFrames.Add((startUnvoiced, steValues.Count));
        }

        public List<(int start, int end)> GetSilenceFrames()
        {
            CalculateAllFrameTypes();
            return _cachedSilenceFrames!;
        }

        public List<(int start, int end)> GetVoicedFrames()
        {
            CalculateAllFrameTypes();
            return _cachedVoicedFrames!.Where(f => (f.end - f.start) >= 5).ToList();
        }

        public List<(int start, int end)> GetUnvoicedFrames()
        {
            CalculateAllFrameTypes();
            return _cachedUnvoicedFrames!;
        }

        public (double, double) CalculateMinAndMaxVolume()
        {
            double minVolume = int.MaxValue;
            double maxVolume = int.MinValue;

            for (int i = 0; i <= leftChannel.Count - frameSamples; i += shiftSamples)
            {
                for (int j = 0; j < frameSamples; j++)
                {
                    double vol = (leftChannel[i + j] * leftChannel[i + j] + rightChannel[i + j] * rightChannel[i + j]) / 2;
                    if (vol > maxVolume)
                    {
                        maxVolume = vol;
                    }
                    if (vol < minVolume)
                    {
                        minVolume = vol;
                    }
                }
            }

            return (minVolume, maxVolume);
        }


        public List<double> FundFreq_Autocorr()
        {
            List<(int start, int end)> voicedFrames = GetVoicedFrames();

            int totalFrames = CalculateVolume().Count;
            List<double> domFrequencies = new List<double>(new double[totalFrames]);

            int minFreqHz = 50;
            int maxFreqHz = 400;

            int minK = samplePerSecond / maxFreqHz;
            int maxK = samplePerSecond / minFreqHz;

            int autoFrameSamples = (int)(0.05 * samplePerSecond);

            foreach (var (startFrame, endFrame) in voicedFrames)
            {
                for (int frameIndex = startFrame; frameIndex < endFrame; frameIndex++)
                {
                    int i = frameIndex * shiftSamples; 

                    if (i > leftChannel.Count - autoFrameSamples) break;

                    double rZero = 0;
                    for (int j = 0; j < autoFrameSamples; j++)
                    {
                        rZero += leftChannel[i + j] * leftChannel[i + j];
                    }

                    double maxSum = double.MinValue;
                    int bestDelay = 0;

                    for (int k = minK; k <= maxK; k++)
                    {
                        double sum = 0;
                        int delay = k;

                        for (int j = 0; j < autoFrameSamples - delay; j++)
                        {
                            sum += leftChannel[i + j] * leftChannel[i + j + delay];
                        }

                        if (sum > maxSum)
                        {
                            bestDelay = k;
                            maxSum = sum;
                        }
                    }

                    if (bestDelay == minK)
                    {
                        bestDelay = 0;
                    }

                    if (bestDelay > 0 && maxSum > 0.45 * rZero)
                    {
                        domFrequencies[frameIndex] = (double)samplePerSecond / bestDelay;
                    }
                }
            }

            return domFrequencies;
        }


        public List<double> FundFreq_AMDF()
        {
            List<(int start, int end)> voicedFrames = GetVoicedFrames();

            int totalFrames = CalculateVolume().Count;
            List<double> domFrequencies = new List<double>(new double[totalFrames]); 

            int minFreqHz = 50;
            int maxFreqHz = 400;

            int minK = samplePerSecond / maxFreqHz;
            int maxK = samplePerSecond / minFreqHz;

            int autoFrameSamples = (int)(0.05 * samplePerSecond);

            foreach (var (startFrame, endFrame) in voicedFrames)
            {
                for (int frameIndex = startFrame; frameIndex < endFrame; frameIndex++)
                {
                    int i = frameIndex * shiftSamples; 

                    if (i > leftChannel.Count - autoFrameSamples) break;

                    double minSum = double.MaxValue;
                    int bestDelay = 0;

                    for (int k = minK; k <= maxK; k++)
                    {
                        double sum = 0;
                        int delay = k;
                        int samplesAdded = autoFrameSamples - delay;

                        for (int j = 0; j < samplesAdded; j++)
                        {
                            sum += Math.Abs(leftChannel[i + j + delay] - leftChannel[i + j]);
                        }

                        sum /= samplesAdded;

                        if (sum < minSum)
                        {
                            bestDelay = k;
                            minSum = sum;
                        }
                    }

                    if (bestDelay == minK)
                    {
                        bestDelay = 0;
                    }

                    if (bestDelay > 0)
                    {
                        domFrequencies[frameIndex] = (double)samplePerSecond / bestDelay;
                    }
                }
            }

            return domFrequencies;
        }

    }
}