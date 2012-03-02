using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class WanderCondition : ICondition
    {
        public bool test(Game g, Entity e)
        {
            if (e.wander == true)
                return true;
            return false;
        }
    }
}
