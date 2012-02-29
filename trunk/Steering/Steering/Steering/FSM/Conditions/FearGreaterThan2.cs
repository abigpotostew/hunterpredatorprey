using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class FearGreaterThan2 : ICondition
    {
        Deer deer;
        Game game;
        float threshold;

        public FearGreaterThan2(Game game, Deer deer, float threshold)
        {
            this.game = game;
            this.deer = deer;
            this.threshold = threshold;
        }
        public bool test()
        {
            //if fear is less than a certain number then return true
            if (deer.fear > threshold)
                return true;
            return false;
        }
    }
}
