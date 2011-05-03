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

            /*
             * 0 - HiHat
             * 1 - Cymbal
             * 2 - CynbalCrash
             * 3 - Cowbell
             * 4 - Rattle
             * 5 - Tambourine
             * 6 - Congo
             * 7 - Bongo
             * 8 - Triangle
             * 9 - BassDrum
             * 10 - KickDrum
             * 11 - SnareDrum
             * 12 - KettleDrum
             * 13 - DistortedKick
             * 14 - DistortedSnare
             */

            public DrumLoop(int[][] loopTrace)
            {
                this.loopTrace = loopTrace;
            }
        }

        private AudioEngine audioEngine;
        private SoundBank drumSoundBank;
        private WaveBank drumWaveBank;
        private int positionPointer;
        private DrumLoop loop;
        private const int SIZE = 16;
        private const int NO_OF_INSTRUMENTS = 15;
        private int[] range = { 100, 10, 10, 10, 10, 10, 10, 10, 10, 200, 200, 200, 10, 100, 100 };
        public string[] instrumentName = { "hihat", "cymbal", "cymbalcrash", "cowbell", "tambourine" , "rattle",
                                                       "conga", "bongo", "triangle", "bassdrum", "kickdrum",
                                                   "snaredrum", "kettledrum", "distortedkick", "distortedsnare"};
        private Random random;
        private bool switchFlag;
        private bool[] playing;

        public DrumPlayer(AudioEngine audioEngine)
        {
            playing = new bool[NO_OF_INSTRUMENTS];
            switchFlag = true;
            random = new Random();
            int[][] loopTrace = new int[NO_OF_INSTRUMENTS][];
            for (int i = 0; i < NO_OF_INSTRUMENTS; i++)
            {
                loopTrace[i] = InitializeLoop(SIZE, 0, 1, 1);
            }


            loop = new DrumLoop(loopTrace);
            positionPointer = 0;
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
            if (positionPointer >= SIZE)
            {
                positionPointer = 0;
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
                if (loop.loopTrace[i][positionPointer] != 0 && playing[i]) drumSoundBank.PlayCue(instrumentName[i] + loop.loopTrace[i][positionPointer].ToString());
            }

            positionPointer++;
        }
    }
}
