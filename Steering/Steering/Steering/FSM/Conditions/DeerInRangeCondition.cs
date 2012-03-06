using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class DeerInRangeCondition : ICondition
    {
        float threshold;

        public DeerInRangeCondition(float range)
        {
            this.threshold = range;
        }

        public bool test(Game g, Entity e)
        {
            if (g.lion.closestDeerTarget == null ) return false;
            return (Vector2.Distance(g.lion.Position, g.lion.closestDeerTarget.Position) < threshold);
        }
    }
}
