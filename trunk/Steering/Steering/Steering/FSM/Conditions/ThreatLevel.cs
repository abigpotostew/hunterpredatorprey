using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class ThreatLevel : ICondition
    {
        Deer deer;
        Game game;
        float threshold;

        public ThreatLevel(Game game, Deer deer, float threshold)
        {
            this.game = game;
            this.deer = deer;
            this.threshold = threshold;
        }
        public bool test()
        {
            if (game.guy.threat > threshold)
                return true;
            return false;
        }
    }
}
