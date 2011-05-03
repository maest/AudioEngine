using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AudioEnginev1
{
    public struct Trace
    {
        public int player1Notes;
        public int player2Notes;
        public int player3Notes;
        public int player4Notes;

        public Trace(int p1N, int p2N, int p3N, int p4N)
        {
            player1Notes = p1N;
            player2Notes = p2N;
            player3Notes = p3N;
            player4Notes = p4N;
        }
    }

    class EventGenerator
    {
        Random random;
        public EventGenerator()
        {
            random = new Random();
        }

        public Trace GenerateTrace()
        {
            int p1N, p2N, p3N, p4N;

            p1N = SetTraceElement();
            p2N = SetTraceElement();
            p3N = SetTraceElement();
            p4N = SetTraceElement();

            return new Trace(p1N, p2N, p3N, p4N);
        }

        public int SetTraceElement()
        {
            int decider = random.Next(32);

            if(decider < 24) return 0;
            if(decider < 30) return 1;
            if(decider < 31) return 2;
            return 3;
        }
    }
}
