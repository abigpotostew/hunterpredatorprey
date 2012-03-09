using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class SetTargetToClosestBush : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            //game.lion.Velocity *= 0.9f;
            game.lion.closestBushTarget = (Bush)game.gameWorld.ClosestBush(character.Position);
            return Steerings.arriveBush.getSteering(character, game.lion.closestBushTarget);
        }
    }
}
