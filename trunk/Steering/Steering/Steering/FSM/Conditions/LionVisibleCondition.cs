using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    
    class LionVisibleCondition : ICondition
    {

        public LionVisibleCondition()
        {
        }
        public bool test(Game game, Entity character)
        {
            //if fear is less than a certain number and if lion is visible then return true
            if (game.lion.visible == true)
                return true;
            return false;
        }
    }
}
