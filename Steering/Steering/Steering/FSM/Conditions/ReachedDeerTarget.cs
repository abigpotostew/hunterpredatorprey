using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class ReachedDeerTarget : ICondition
    {
        public bool test(Game g, Entity e)
        {
            float distToTarget = Vector2.Distance(g.lion.Position, g.lion.closestDeerTarget.Position);
            if (distToTarget < 25) return true;
            return false;
        }
    }
}
