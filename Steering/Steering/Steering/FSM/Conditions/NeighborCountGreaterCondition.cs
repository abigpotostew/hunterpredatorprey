using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class NeighborCountGreaterCondition : ICondition
    {
        int threshold;

        public NeighborCountGreaterCondition(int threshold)
        {
            this.threshold = threshold;
        }

        public bool test(Game g, Entity e)
        {
            if (e.neighbors.Count > threshold)
                return true;
            return false;
        }
    }
}
