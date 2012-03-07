using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class LionHungerGreaterThanCondition : ICondition
    {
        int threshold;

        public LionHungerGreaterThanCondition(int threshold)
        {
            this.threshold = threshold;

        }

        public bool test(Game g, Entity e)
        {
            if (g.lion.hunger > threshold) return true;
            return false;
        }
    }
}
