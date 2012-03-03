using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class RandomCondition: ICondition
    {
        int range, chance;
        Random r = new Random();
        int testrandom;
        public RandomCondition(int range, int chance)
        {
            this.range = range;
            this.chance = chance;
        }
        public bool test(Game g, Entity e)
        {
            testrandom = r.Next(1, range);
            if (testrandom == chance) return true;
            else return false;
        }
    }
}
