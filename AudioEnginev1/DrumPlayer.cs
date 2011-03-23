using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace AudioEnginev1
{
    class DrumPlayer
    {
        private struct Loop
        {
            public int[] ride;
            public int[] snare;
            public int[] kick;
            public Loop(int[] ride, int[] snare, int[] kick)
            {
                this.ride = ride;
                this.snare = snare;
                this.kick = kick;
            }
        }

        private AudioEngine audioEngine;
        private SoundBank drumSoundBank;
        private WaveBank drumWaveBank;
        private int pointer;
        private Loop loop;
        private const int SIZE = 16 * 4;

        public DrumPlayer(AudioEngine audioEngine)
        {
            int[] ride =  { 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,  1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,  1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,  1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0 };
            int[] snare = { 0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,1,  0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,1,  0,0,0,0,1,0,0,1,0,1,0,0,0,0,1,0,  0,1,0,0,1,0,0,1,0,1,0,0,0,0,1,0 };
            int[] kick =  { 1,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,  1,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,  1,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,  0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,0 };
            loop = new Loop(ride, snare, kick);
            pointer = 0;
            this.audioEngine = audioEngine;
            drumSoundBank = new SoundBank(audioEngine, "Content/Drum Sound Bank.xsb");
            drumWaveBank = new WaveBank(audioEngine, "Content/Drum Bank.xwb");
        }

        public void Next()
        {
            if (pointer >= SIZE) pointer = 0;

            if (loop.ride[pointer] == 1) drumSoundBank.PlayCue("hatopen");
            if (loop.snare[pointer] == 1) drumSoundBank.PlayCue("snare");
            if (loop.kick[pointer] == 1) drumSoundBank.PlayCue("kick");

            pointer++;
        }
    }
}
