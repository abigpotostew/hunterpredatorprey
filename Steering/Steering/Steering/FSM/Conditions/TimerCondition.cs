using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class TimerCondition : ICondition
    {
        Game game;
        int timer;
        TimeSpan originalTime;
 
        public TimerCondition(Game game, TimeSpan intialTime, int seconds)
        {
            this.game = game;
            originalTime = intialTime;
            timer = seconds;
        }

        public bool test()
        {
            /*  if (gameTime.ElapsedGameTime.Seconds - originalTime.Seconds >= timer)
              {
                  originalTime = gameTime.ElapsedGameTime;
                  return true;
              }
              else
                --timer; */
            return false;
        }
    }
}
