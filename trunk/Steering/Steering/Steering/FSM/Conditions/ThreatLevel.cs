using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class ThreatLevel : ICondition
    {
        float threshold;

        public ThreatLevel(float threshold)
        {
            this.threshold = threshold;
        }
        public bool test(Game game, Entity character)
        {
            if (game.guy.threat > threshold)
                return true;
            return false;
        }
    }
}
