using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class RandomCondition: ICondition
    {
        Random r = new Random();
        int testrandom;
        public RandomCondition()
        {
            //Random r = new Random();
        }
        public bool test(Game g, Entity e)
        {
            testrandom = r.Next(1, 10);
            if (testrandom == 5) return true;
            else return false;
        }
    }
}
