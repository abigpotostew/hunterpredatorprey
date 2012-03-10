using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    public class FleeFromHunterAction : IAction
    {
        SteeringOutput IAction.execute(Game game, Entity character)
        {
            SteeringOutput result = Steerings.flee200.getSteering(character, game.playerHunter);
            result.maxSpeed = 5f;
            return result;
        }
    }
}
