using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class ReachedPounceTarget : ICondition
    {
        float threshold;

        public ReachedPounceTarget(float range)
        {
            this.threshold = range;
        }
        public bool test(Game g, Entity e)
        {
            float distToTarget = Vector2.Distance(g.lion.Position, g.lion.pounceTarget.Position);
            if (distToTarget < threshold) return true;
            return false;
        }
    }
}
