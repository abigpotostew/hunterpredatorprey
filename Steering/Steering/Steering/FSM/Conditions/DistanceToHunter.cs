using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Conditions
{
    class DistanceToHunter : ICondition
    {
        float thresold;
        public DistanceToHunter(float thresold)
        {
            this.thresold = thresold;
        }
        public bool test(Game g, Entity e)
        {
            float dist = (g.guy.Position - e.Position).Length();
            if (dist < thresold) return true;
            return false;
        }
    }
}
