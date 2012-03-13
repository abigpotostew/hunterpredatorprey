using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class RandomTimerCondition : ICondition
    {
        int timer;
        int seconds;
        TimeSpan originalTime;
 
        public RandomTimerCondition(TimeSpan intialTime, int seconds)
        {
            originalTime = intialTime;
            this.seconds = seconds;
            //timer = random.Next(seconds - 400, seconds);
            timer = StaticRandom.random.Next(seconds - 400, seconds);
            //timer = seconds;
            
        }

        public bool test(Game game, Entity character)
        {
            /*  if (gameTime.ElapsedGameTime.Seconds - originalTime.Seconds >= timer)
              {
                  originalTime = gameTime.ElapsedGameTime;
                  return true;
              }
              else
                --timer; */
            --timer;
            if (timer == 0)
            {
                timer = StaticRandom.random.Next(seconds - 400, seconds);
                return true;
            }
            return false;
        }
    }
}
