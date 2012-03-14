using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class PlayerHealthCondition : ICondition
    {
        int threshold;
        public PlayerHealthCondition(int healthLessThan)
        {
            this.threshold = healthLessThan;
        }

        public bool test(Game g, Entity e)
        {
            return g.playerHunter.health < threshold;
        }
    }
}
