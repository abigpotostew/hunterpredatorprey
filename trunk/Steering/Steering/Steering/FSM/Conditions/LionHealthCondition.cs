using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class LionHealthCondition : ICondition
    {
        int threshold;
        public LionHealthCondition(int healthLessThan)
        {
            this.threshold = healthLessThan;
        }

        public bool test(Game g, Entity e)
        {
            return g.lion.health < threshold;
        }
    }
}
