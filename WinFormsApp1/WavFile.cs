using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class WavFile
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
        double shift = 0.005;
        int frameSamples;
        int shiftSamples;

        public WavFile(string filePath)
        {
            if(filePath == null || !File.Exists(filePath))
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

            return volValues;
        }

        public List<double> CalculateSTE()
        {
            List<double> steValues = new List<double>();

            for(int i = 0; i <= leftChannel.Count - frameSamples; i += shiftSamples)
            {
                double vol = 0;
                for(int j = 0; j < frameSamples; j++)
                {
                    double valueToAdd = (leftChannel[i + j] * leftChannel[i + j] + rightChannel[i + j] * rightChannel[i + j]) / 2;
                    vol += valueToAdd;
                }
                vol /= frameSamples;
                steValues.Add(vol);
            }
            return steValues;
        }

        public List<double> CalculateZCR()
        {
            List<double> zcrValues = new List<double>();

            for(int i = 0; i <= leftChannel.Count - frameSamples; i += shiftSamples)
            {
                double zcr = 0;
                for(int j = 1; j < frameSamples; j++)
                {
                    double left = Math.Abs(Signum(leftChannel[i + j]) - Signum(leftChannel[i + j - 1]));
                    double right = Math.Abs(Signum(rightChannel[i + j]) - Signum(rightChannel[i + j - 1])) / 2;
                    zcr += (left + right) / 2.0;
                }
                zcr *= (double)samplePerSecond / (2 * frameSamples);
                zcrValues.Add(zcr);
            }
            return zcrValues;
        }

        public double SetSTEThreshold()
        {
            List<double> steValues = CalculateVolume();
            double avg = 0;
            for(int i = 0; i < 10; i++)
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


        public List<double> Autocorrelation()
        {
            List<double> domFrequencies = new List<double>();

            int minFreqHz = 50;
            int maxFreqHz = 400;

            int minK = samplePerSecond / maxFreqHz; 
            int maxK = samplePerSecond / minFreqHz; 

            int autoFrameSamples = (int)(0.05 * samplePerSecond); 

            int limit = autoFrameSamples - maxK;

            for (int i = 0; i <= leftChannel.Count - autoFrameSamples; i += shiftSamples) 
            {
                long frameSum = 0;
                for (int j = 0; j < autoFrameSamples; j++)
                {
                    frameSum += leftChannel[i + j]; 
                }
                double mean = (double)frameSum / autoFrameSamples;

                double rZero = 0;
                for (int j = 0; j < limit; j++)
                {
                    double sample = leftChannel[i + j] - mean;
                    rZero += sample * sample;
                }

                double maxSum = double.MinValue;
                int bestDelay = 0;

                for (int k = minK; k <= maxK; k++)
                {
                    double sum = 0;
                    int delay = k;

                    for (int j = 0; j < limit; j++)
                    {
                        double sample1 = leftChannel[i + j] - mean; 
                        double sample2 = leftChannel[i + j + delay] - mean; 

                        sum += sample1 * sample2;
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

                if (bestDelay > 0 && maxSum > 0.45 * rZero && rZero > 1000000)
                {
                    domFrequencies.Add((double)samplePerSecond / bestDelay); 
                }
                else
                {
                    domFrequencies.Add(0);
                }
            }
            return domFrequencies;
        }

        private int Signum(short value)
        {
            if (value >= 0) return 1;
            else return 0;
        }



    }
}
