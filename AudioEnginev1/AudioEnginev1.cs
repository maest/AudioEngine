using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace AudioEnginev1
{
    class AudioEnginev1
    {
        private EventGenerator eventGenerator;
        private AudioEngine audioEngine;
        private WaveBank waveBank, drumWaveBank;
        private SoundBank soundBank, drumSoundBank;
        private DrumPlayer drumPlayer;
        private MelodyPlayer melodyPlayer;

        public AudioEnginev1()
        {
            eventGenerator = new EventGenerator();
        }

        public void Initialize()
        {
            //audio
            audioEngine = new AudioEngine("Content/noDrums.xgs");
            waveBank = new WaveBank(audioEngine, "Content/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content/Sound Bank.xsb");
            //drumPlayer = new DrumPlayer(audioEngine);
            melodyPlayer = new MelodyPlayer(audioEngine);
        }

        public void Tick()
        {
            Trace trace = eventGenerator.GenerateTrace();
            //drumPlayer.Next();
            melodyPlayer.Next(trace);
        }
    }
}
