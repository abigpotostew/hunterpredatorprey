using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering
{
    class Timer
    {
        float milliseconds;
        int seconds;

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
            if (milliseconds == 1000)
                seconds += 1;
        }
        public void stopTimer(float milliseconds, int seconds)
        {
            this.milliseconds = milliseconds;
            this.seconds = seconds;
        }
        //print this out on screen later

    }
}
