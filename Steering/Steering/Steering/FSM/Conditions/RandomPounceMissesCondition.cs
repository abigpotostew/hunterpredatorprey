using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    public class RandomPounceMissesCondition : ICondition
    {

        int threshold, maxMisses;
        Random r;

        public RandomPounceMissesCondition(int maxMisses)
        {
            this.maxMisses = maxMisses;
            threshold = -1;
            r = new Random();
        }

        public bool test(Game g, Entity e)
        {
            if (threshold == -1)
            {
                //threshold = r.Next(0, maxMisses);
                threshold = StaticRandom.random.Next(0, maxMisses);
            }
            if (g.lion.pounceMisses >= threshold)
            {
                //Console.WriteLine("Returning TRUE, thresh: " + threshold + " Lions misses: " + g.lion.pounceMisses);
                threshold = -1;
                return true;
            }
            //Console.WriteLine("Returning false, thresh: " + threshold + " Lions misses: " + g.lion.pounceMisses);
            return false;
        }
    }
}
