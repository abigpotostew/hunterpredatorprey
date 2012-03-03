using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class NeighborCountLessCondition : ICondition
    {
        int threshold;

        public NeighborCountLessCondition(int threshold)
        {
            this.threshold = threshold;
        }
            
        public bool test(Game g, Entity e)
        {
            if (e.neighbors.Count < threshold)
                return true;
            return false;
        }
    }
}
