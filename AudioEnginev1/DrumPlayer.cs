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
            public int[][] loopTrace;
            public const string[] instrumentName = { "hihat", "cymbal", "cowbell", "tambourine" , "rattle",
                                                       "conga", "bongo", "triangle", "bassdrum", "kickdrum",
                                                   "snaredrum", "kettledrum", "distortedkick", "distortedsnare"};

            /*
             * 0 - HiHat
             * 1 - Cymbal
             * 2 - Cowbell
             * 3 - Rattle
             * 4 - Tambourine
             * 5 - Congo
             * 6 - Bongo
             * 7 - Triangle
             * 8 - BassDrum
             * 9 - KickDrum
             * 10 - SnareDrum
             * 11 - KettleDrum
             * 12 - DistortedKick
             * 13 - DistortedSnare
             */

            public DrumLoop(int[][] loopTrace)
            {
                this.loopTrace = loopTrace;
            }
        }

        private AudioEngine audioEngine;
        private SoundBank drumSoundBank;
        private WaveBank drumWaveBank;
        private int pointer;
        private DrumLoop loop;
        private const int SIZE = 16;
        private const int NO_OF_INSTRUMENTS = 14;
        private const int[] range = { 100, 20, 10, 10, 10, 10, 10, 10, 200, 200, 200, 10, 200, 100 };
        private Random random;
        private bool switchFlag;
        private bool[] playing;

        public DrumPlayer(AudioEngine audioEngine)
        {
            playing = new bool[NO_OF_INSTRUMENTS];
            switchFlag = false;
            random = new Random();
            int[][] loopTrace = new int[NO_OF_INSTRUMENTS][];
            for (int i = 0; i < NO_OF_INSTRUMENTS; i++)
            {
                loopTrace[i] = InitializeLoop(SIZE, 0, 1, 1);
            }


            loop = new DrumLoop(loopTrace);
            pointer = 0;
            this.audioEngine = audioEngine;
            drumSoundBank = new SoundBank(audioEngine, "Content/Drum Sound Bank.xsb");
            drumWaveBank = new WaveBank(audioEngine, "Content/Drum Bank.xwb");
        }


        public int[] InitializeLoop(int size, int b, int a, int value) //Probability of generating a sound on a click is b/a
        {
            int[] result = new int[size];

            for (int i = 0; i < size; i++)
                if (random.Next(a) >= b) result[i] = 0;
                else result[i] = value;

            return result;
        }

        public void Next()
        {
            if (pointer >= SIZE)
            {
                pointer = 0;
                if (switchFlag)
                {
                    switchFlag = false;
                    int choice = random.Next(NO_OF_INSTRUMENTS);
                    int density = random.Next(2, 8);

                    if (playing[choice] == false)
                    {
                        loop.loopTrace[choice] = InitializeLoop(SIZE, 1, density, range[choice]);
                    }
                    playing[choice] = !playing[choice];
                }
                else
                {
                    switchFlag = true;
                }
            }

            for (int i = 0; i < NO_OF_INSTRUMENTS; i++)
            {
                if (loop.loopTrace[i][pointer] != 0 && playing[i]) drumSoundBank.PlayCue(instrumentName[i] + loop.loopTrace[i][pointer].ToString());
            }

            pointer++;
        }
    }
}
