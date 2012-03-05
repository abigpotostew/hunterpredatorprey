using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class ReachedBush : ICondition
    {
        public bool test(Game g, Entity e)
        {
            Vector2 direction = g.lion.closestBushTarget.posit - g.lion.Position;
            float len = direction.LengthSquared();
            if ( len < 9 )
            {
                g.lion.visible = false;
                return true;
            }
            return false;
        }
    }
}
