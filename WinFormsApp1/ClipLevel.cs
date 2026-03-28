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
        public static double CalculateVDR(WavFile wavFile)
        {
            (double minVol, double maxVol) = wavFile.CalculateMinAndMaxVolume();

            return ((maxVol - minVol) / maxVol);
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

        public static double CalculateLSTER(WavFile wavFile)
        {
            List<double> steValues = wavFile.CalculateSTE();
            int n = steValues.Count;
            int framesPerSec = 200;
            int windowsCount = (int)Math.Ceiling((double)n / framesPerSec);
            double[] sveAvgs = new double[windowsCount];
            for (int i = 0; i < windowsCount; i++)
            {
                double sum = 0;
                int framesInThisWindow = 0;
                int startFrame = i * framesPerSec;
                int endFrame = Math.Min(n, startFrame + framesPerSec);

                for (int j = startFrame; j < endFrame; j++)
                {
                    sum += steValues[j];
                    framesInThisWindow++;
                }

                sveAvgs[i] = sum / framesInThisWindow;
            }
            double res = 0;
            for (int i = 0; i < n; i++)
            {
                int windowIndex = i / framesPerSec;
                res += Math.Sign(0.5 * sveAvgs[windowIndex] - steValues[i]) + 1;
            }
            return res / (2.0 * n);
        }

        public static int Signum(double value)
        {
            if (value >= 0) return 1;
            else return 0;
        }
    }
}
