using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class LionDesperateCondition : ICondition
    {
        public bool test(Game g, Entity e)
        {
            if (g.lion.desperate == true) return true;
            return false;
        }
    }
}
