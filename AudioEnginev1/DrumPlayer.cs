using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace AudioEnginev1
{
    class DrumPlayer
    {
        private struct DrumLoop
        {
            public int[] ride;
            public int[] crash;
            public int[] snare;
            public int[] kick;
            public int[] tom;
            public int[] tomlight;
            public int[] hatopen3;
            public int[] hatopen2;

            public DrumLoop(int[] ride, int[] crash, int[] snare, int[] kick, int[] tom, int[] tomlight, int[]hatopen3, int[] hatopen2)
            {
                this.ride = ride;
                this.crash = crash;
                this.snare = snare;
                this.kick = kick;
                this.tom = tom;
                this.tomlight = tomlight;
                this.hatopen3 = hatopen3;
                this.hatopen2 = hatopen2;
            }
        }

        private AudioEngine audioEngine;
        private SoundBank drumSoundBank;
        private WaveBank drumWaveBank;
        private int pointer;
        private DrumLoop loop;
        private const int SIZE = 16;

        public DrumPlayer(AudioEngine audioEngine)
        {
            //int[] ride =  { 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,  1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,  1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,  1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0 };
            //int[] snare = { 0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,1,  0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,1,  0,0,0,0,1,0,0,1,0,1,0,0,0,0,1,0,  0,1,0,0,1,0,0,1,0,1,0,0,0,0,1,0 };
            //int[] kick =  { 1,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,  1,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,  1,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,  0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,0 };
            /*int[] ride =     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            int[] crash =    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            int[] snare =    { 1,0,0,1,0,0,1,0,1,0,1,0,0,1,0,0 };
            int[] kick =     { 0,0,1,0,0,0,1,0,0,1,0,0,0,0,1,0 };
            int[] tom =      { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            int[] tomlight = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            int[] hatopen3 = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            int[] hatopen2 = { 1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0 };*/

            int[] ride = new int[16];
            int[] crash = new int[16];
            int[] snare = new int[16];
            int[] kick = new int[16];
            int[] tom = new int[16];
            int[] tomlight = new int[16];
            int[] hatopen3 = new int[16];
            int[] hatopen2 = new int[16];
            
            Random random = new Random();

            for (int i = 0; i < 16; i++)
            {
                if (random.Next() % 3 == 0) snare[i] = 1;
                else snare[i] = 0;

                if (random.Next() % 3 == 0) kick[i] = 1;
                else kick[i] = 0;

                if (random.Next() % 2 == 0) hatopen2[i] = 1;
                else hatopen2[i] = 0;
            }

            loop = new DrumLoop(ride, crash, snare, kick, tom, tomlight, hatopen3, hatopen2);
            pointer = 0;
            this.audioEngine = audioEngine;
            drumSoundBank = new SoundBank(audioEngine, "Content/Drum Sound Bank.xsb");
            drumWaveBank = new WaveBank(audioEngine, "Content/Drum Bank.xwb");
        }

        public void Next()
        {
            if (pointer >= SIZE) pointer = 0;

            if (loop.ride[pointer] == 1) drumSoundBank.PlayCue("ride");
            if (loop.crash[pointer] == 1) drumSoundBank.PlayCue("crash");
            if (loop.snare[pointer] == 1) drumSoundBank.PlayCue("snare");
            if (loop.kick[pointer] == 1) drumSoundBank.PlayCue("kick");
            if (loop.tom[pointer] == 1) drumSoundBank.PlayCue("tom");
            if (loop.tomlight[pointer] == 1) drumSoundBank.PlayCue("tomlight");
            if (loop.hatopen3[pointer] == 1) drumSoundBank.PlayCue("hatopen3");
            if (loop.hatopen2[pointer] == 1) drumSoundBank.PlayCue("hatopen2");


            pointer++;
        }
    }
}
