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
        public string riffChukkId;
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
        List<short> leftChannel { get; set; } = new List<short>();
        List<short> rightChannel { get; set; } = new List<short>();

        public WavFile(string filePath)
        {
            using (var file = File.Open(filePath, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(file);
                riffChukkId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                riffChunkSize = reader.ReadInt32();
                riffFormat = Encoding.ASCII.GetString(reader.ReadBytes(4));
                string nextSection = Encoding.ASCII.GetString(reader.ReadBytes(4));
                if (nextSection != "fmt ")
                {
                    MessageBox.Show("Invalid WAV file format: 'fmt ' chunk not found.");
                }
                fmtChunkId = nextSection;
                fmtChunkSize = reader.ReadInt32();
                audioFormat = reader.ReadInt16();
                numChannels = reader.ReadInt16();
                samplePerSecond = reader.ReadInt32();
                timeStep = 1.0 / samplePerSecond;
                avgBytesPerSec = reader.ReadInt32();
                blockAlign = reader.ReadInt16();
                bitsPerSample = reader.ReadInt16();
                nextSection = Encoding.ASCII.GetString(reader.ReadBytes(4));
                if (nextSection != "data")
                {
                    MessageBox.Show("Invalid WAV file format: 'data' chunk not found.");
                }
                dataChunkId = nextSection;
                dataChunkSize = reader.ReadInt32();
                data = reader.ReadBytes(dataChunkSize);
                for (int i = 0; i < data.Length; i += 4)
                {
                    short leftSample = BitConverter.ToInt16(data, i);
                    leftChannel.Add(leftSample);

                    short rightSample = BitConverter.ToInt16(data, i + 2);
                    rightChannel.Add(rightSample);
                }
            }
        }
    }
}
