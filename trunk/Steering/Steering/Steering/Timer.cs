using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering
{
    class Timer
    {
        public float milliseconds;
        public int seconds;

        public Timer()
        {
            this.milliseconds = 0;
            this.seconds = 0;
        }

        public void resetTimer()
        {
            milliseconds = 0;
            seconds = 0;
        }
        public void startTimer()
        {
            milliseconds += 1;
            if (milliseconds % 61 == 0)
                seconds += 1;
        }
  
        public void stopTimer(float millisecs)
        {
            milliseconds = millisecs;
        }
        //print this out on screen later

    }
}
