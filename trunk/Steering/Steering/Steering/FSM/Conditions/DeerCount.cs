using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    public class DeerCount : ICondition
    {
        int threshold;

        public DeerCount(int deerCtLessThanOrEqual)
        {
            this.threshold = deerCtLessThanOrEqual;
        }


        public bool test(Game g, Entity e)
        {
            return g.deerManager.GetDeerCount() <= threshold;
        }
    }
}
