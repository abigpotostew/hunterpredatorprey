using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class TimerCondition : ICondition
    {
        int timer;
        int tics;
 
        public TimerCondition(int tics)
        {
            this.tics = tics;
            this.timer = tics;
            //timer = seconds;
            
        }

        public bool test(Game game, Entity character)
        {
            --timer;
            if (timer == 0)
            {
                timer = tics;
                return true;
            }
            return false;
        }
    }
}
