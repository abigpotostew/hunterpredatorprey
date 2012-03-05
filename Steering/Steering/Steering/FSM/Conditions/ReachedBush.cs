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
            Vector2 direction = g.lion.closestBushTarget.position*50 - g.lion.Position;
            float len = direction.Length();
            Console.Write(" " + len);
            if ( len < 480)
                return true;
            return false;
        }
    }
}
