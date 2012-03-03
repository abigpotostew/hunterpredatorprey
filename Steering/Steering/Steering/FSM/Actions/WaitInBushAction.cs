using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.FSM.Actions
{
    class WaitInBushAction : IAction
    {

        SteeringOutput IAction.execute(Game game, Entity character)
        {
            character.Velocity = new Vector2();
            //return look at target
            return new SteeringOutput();
        }
    }
}
