using ScottPlot.Interactivity.UserActionResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class ClipLevel
    {
        public static double CalculateVDR(WavFile wavFile, (double minVol, double maxVol)? x = null)
        {
            if(x == null)
            {
                x = wavFile.CalculateMinAndMaxVolume();
            }

            return ((x.Value.maxVol - x.Value.minVol) / x.Value.maxVol);
        }

        public static double CalculateVolumeUndulation(WavFile wavFile)
        {
            var volumes = wavFile.CalculateVolume();
            double firstLocal = -1;
            double secondLocal = -1;
            double sumOfLocalExtrema = 0;
            for (int i = 1; i < volumes.Count - 1; i++)
            {
                if(volumes[i] < volumes[i - 1] && volumes[i] < volumes[i + 1] || volumes[i] > volumes[i - 1] && volumes[i] > volumes[i + 1])
                {
                    if(firstLocal == -1)
                    {
                        firstLocal = volumes[i];
                    }
                    else if(secondLocal == -1)
                    {
                        sumOfLocalExtrema += Math.Abs(firstLocal - volumes[i]);
                        firstLocal = volumes[i];
                    }
                }
            }
            double val = (sumOfLocalExtrema / volumes.Count);
            return val;
        }

        public static double CalculateLSTER(WavFile wavFile, List<double>? steValues = null)
        {
            if(steValues == null)
            {
                steValues = wavFile.CalculateSTE();
            }
            int n = steValues.Count;
            int framesPerSec = 200;
            double[] sveAvgs = CalculateAverages(steValues, n, framesPerSec);
            double res = 0;
            for (int i = 0; i < n; i++)
            {
                int windowIndex = i / framesPerSec;
                res += Math.Sign(0.5 * sveAvgs[windowIndex] - steValues[i]) + 1;
            }
            return res / (2.0 * n);
        }
        public static double CalculateEnergyEntropy(WavFile wavFile, List<double>? steValues = null)
        {
            double totalEnergy = 0;
            if(steValues == null)
            {
                steValues = wavFile.CalculateSTE();
            }
            for(int i = 0; i < steValues.Count; i++)
            {
                totalEnergy += steValues[i];
            }
            double res = 0;
            foreach(var energy in steValues)
            {
                if (energy > 0)
                {
                    double sigma = energy / totalEnergy;
                    res += Math.Log(sigma, 2);
                }
            }
            return -res;
        }

        private static double[] CalculateAverages(List<double> values, int n, int framesPerSec)
        {
            int windowsCount = (int)Math.Ceiling((double)n / framesPerSec);
            double[] avgs = new double[windowsCount];
            for (int i = 0; i < windowsCount; i++)
            {
                double sum = 0;
                int framesInThisWindow = 0;
                int startFrame = i * framesPerSec;
                int endFrame = Math.Min(n, startFrame + framesPerSec);

                for (int j = startFrame; j < endFrame; j++)
                {
                    sum += values[j];
                    framesInThisWindow++;
                }

                avgs[i] = sum / framesInThisWindow;
            }
            return avgs;
        }

        public static double CalculateHZCRR(WavFile wavFile, List<double>? zcrValues = null)
        {
            if(zcrValues == null)
            {
                zcrValues = wavFile.CalculateZCR();
            }
            int n = zcrValues.Count;
            int framesPerSec = 200;
            double[] zcrAvgs = CalculateAverages(zcrValues, n, framesPerSec);
            double res = 0;
            for(int i = 0; i < n; i++)
            {
                int windowIndex = i / framesPerSec;
                res += Math.Sign(zcrValues[i] - 1.5 * zcrAvgs[windowIndex]) + 1;
            }
            double temp = res / (2.0 * n);
            return temp;
        }

        public static List<(int start, int end)> DetectSpeech(WavFile wavFile)
        {
            List<double> zcrValues = wavFile.CalculateZCR();
            List<double> steValues = wavFile.CalculateSTE();

            List<(int start, int end)> speechFrames = new List<(int, int)>();

            int framesPerSec = 200;
            int n = zcrValues.Count;
            int windowsCount = (int)Math.Ceiling((double)n / framesPerSec);
            double maxVol = double.MinValue;
            double minVol = double.MaxValue;

            for (int i = 0; i < windowsCount; i++)
            {
                int startFrame = i * framesPerSec;
                int endFrame = Math.Min(n, startFrame + framesPerSec);
                int clipLength = endFrame - startFrame;

                double sumSTE = 0;
                double sumZCR = 0;
                for (int j = startFrame; j < endFrame; j++)
                {
                    sumSTE += steValues[j];
                    sumZCR += zcrValues[j];
                    double vol = (wavFile.leftChannel[i + j] * wavFile.leftChannel[i + j] + wavFile.rightChannel[i + j] * wavFile.rightChannel[i + j]) / 2;
                    if(vol > maxVol) maxVol = vol;
                    if(vol < minVol) minVol = vol;
                }
                double avSTE = sumSTE / clipLength;
                double avZCR = sumZCR / clipLength;
                int lsterCount = 0;
                int hzcrrCount = 0;
                double vdr = (maxVol - minVol) / maxVol;
                for (int j = startFrame; j < endFrame; j++)
                {
                    if (steValues[j] < 0.5 * avSTE) lsterCount++;
                    if (zcrValues[j] > 1.5 * avZCR) hzcrrCount++;
                }
                double clipLster = (double)lsterCount / clipLength;
                double clipHzcrr = (double)hzcrrCount / clipLength;

                if (clipLster > 0.15 && clipHzcrr > 0.1 && vdr > 0.5)
                {
                    speechFrames.Add((startFrame, endFrame));
                }
            }

            return speechFrames;
        }

        public static List<(int start, int end)> SubtractIntervals(
    List<(int start, int end)> source,
    List<(int start, int end)> subtract)
        {
            var currentIntervals = new List<(int start, int end)>(source);

            foreach (var sub in subtract)
            {
                var nextIntervals = new List<(int start, int end)>();

                foreach (var current in currentIntervals)
                {
                    if (sub.end <= current.start || sub.start >= current.end)
                    {
                        nextIntervals.Add(current);
                    }
                    else
                    {
                        if (current.start < sub.start)
                        {
                            nextIntervals.Add((current.start, sub.start));
                        }

                        if (current.end > sub.end)
                        {
                            nextIntervals.Add((sub.end, current.end));
                        }
                    }
                }
                currentIntervals = nextIntervals;
            }

            return currentIntervals;
        }
    }
}
