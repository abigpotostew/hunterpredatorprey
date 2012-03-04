using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class WaitInBushAction : IAction
    {

        SteeringOutput IAction.execute(Game game, Entity character)
        {
            character.Velocity = new Vector2();
            //return look at target
            game.lion.closestDeerTarget = game.deerManager.FindClosestDeer(game.lion.Position);
            return Steerings.face.getSteering(character, game.lion.closestDeerTarget);
        }
    }
}
