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
        private Random random;
        private DrumPlayer drumPlayer;

        public AudioEnginev1()
        {
            eventGenerator = new EventGenerator();
            random = new Random();
        }

        public void Initialize()
        {
            //audio
            audioEngine = new AudioEngine("Content/v1.xgs");
            waveBank = new WaveBank(audioEngine, "Content/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content/Sound Bank.xsb");
            drumPlayer = new DrumPlayer(audioEngine);
        }

        public void Tick()
        {
            Trace trace = eventGenerator.GenerateTrace();
            drumPlayer.Next();
            if (trace.player1Notes != 1) soundBank.PlayCue("tone" + random.Next(12).ToString());
            //if (trace.player2Notes != 1) soundBank.PlayCue("tone" + random.Next(12).ToString());
            //if (trace.player3Notes == 1) soundBank.PlayCue("tone" + random.Next(12).ToString());
            //if (trace.player4Notes == 1) soundBank.PlayCue("tone" + random.Next(12).ToString());
        }
    }
}
