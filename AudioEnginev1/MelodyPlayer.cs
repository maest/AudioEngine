using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace AudioEnginev1
{
    class MelodyPlayer
    {
        private struct Scale
        {
            public int[] notes;

            public Scale(int[] note)
            {
                this.notes = note;
            }
        }

        enum ScaleType { Major, NatMinor, HarMinor, Blues, Pentatonic };

        private int[][] scaleSignature = new int[][]
        {
        new int[]{2,2,1,2,2,2,1},
        new int[]{2,1,2,2,1,2,2},
        new int[]{2,1,2,2,1,3,1},
        new int[]{3,2,1,1,3,2},
        new int[]{2,2,3,2,3},
        };

        private Scale currentScale;

        private AudioEngine audioEngine;
        private SoundBank melodySoundBank;
        private WaveBank melodyWaveBank;
        private Random random;

        private int traceCounter;
        private bool inTransition;

        public MelodyPlayer(AudioEngine audioEngine)
        {
            this.audioEngine = audioEngine;
            currentScale = CreateScale(1, ScaleType.Major);
            random = new Random();

            melodySoundBank = new SoundBank(audioEngine, "Content/Sound Bank.xsb");
            melodyWaveBank = new WaveBank(audioEngine, "Content/Wave Bank.xwb");

            inTransition = false;
            traceCounter = 0;
        }

        public void Next(Trace trace)
        {
            traceCounter++;

            int scaleLength = currentScale.notes.Length;

            int noteValue = random.Next(24);
            if (trace.player1Notes != 0) melodySoundBank.PlayCue(currentScale.notes[noteValue % scaleLength].ToString() + "l");
            /*noteValue = random.Next(24);
            if (trace.player2Notes != 0) melodySoundBank.PlayCue(currentScale.notes[noteValue % scaleLength].ToString() + "l");
            noteValue = random.Next(24);
            if (trace.player3Notes != 0) melodySoundBank.PlayCue(currentScale.notes[noteValue % scaleLength].ToString() + "l");
            noteValue = random.Next(24);
            if (trace.player4Notes != 0) melodySoundBank.PlayCue(currentScale.notes[noteValue % scaleLength].ToString() + "l");
            */

            if (inTransition)
            {
                if (traceCounter % 12 == 0)
                {
                    inTransition = false;
                    currentScale = CreateScale(6, ScaleType.NatMinor);
                }
            }
            else
            {
                if (traceCounter % 48 == 0)
                {
                    inTransition = true;
                    currentScale = CreateTransitionScale(currentScale, CreateScale(6,ScaleType.NatMinor));
                }
            }
        }

        private Scale CreateScale(int note, ScaleType scaleType)
        {
            int scaleLength = scaleSignature[(int)scaleType].Length;
            int len = scaleLength * 2;

            int[] result = new int[len];
            result[0] = note;
            for (int i = 0; i < len-1; i++)
            {
                result[i + 1] = (result[i] + scaleSignature[(int)scaleType][i%scaleLength]);
                if (result[i + 1] > 24)
                    result[i + 1] = (result[i + 1] % 24) + 1;
            }

            return new Scale(result);
        }

        private Scale CreateTransitionScale(Scale prev, Scale next)
        {
            int[] aux = new int[24];
            int auxPointer = 0;
            for (int i = 0; i < prev.notes.Length; i++)
            {
                for (int j = 0; j < next.notes.Length; j++)
                {
                    if (prev.notes[i] == next.notes[j])
                    {
                        aux[auxPointer] = prev.notes[i];
                        auxPointer++;
                    }
                }
            }

            auxPointer = 0;
            while (auxPointer < 24 && aux[auxPointer] != 0)
            {
                auxPointer++;
            }

            int[] result = new int[auxPointer];
            for (int i = 0; i < auxPointer; i++)
            {
                result[i] = aux[i];
            }

            return new Scale(result);
        }
    }
}
