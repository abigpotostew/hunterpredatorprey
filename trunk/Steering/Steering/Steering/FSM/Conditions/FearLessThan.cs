using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class FearLessThan : ICondition
    {
        float threshold;
 
        public FearLessThan(float threshold)
        {
            this.threshold = threshold;
        }
        public bool test(Game game, Entity character)
        {
            //if fear is less than a certain number then return true
            if (character.fear < threshold)
                return true;
            return false;
        }
    }
}
