using Microsoft.Xna.Framework;
using System;

namespace Steering.FSM.Actions
{
    class SetLionTargetToDeerAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.pounceMisses++;

            //set the lion's pounce target to a little behind the target deer's position
            Vector2 target = game.lion.closestDeerTarget.Position;
            Vector2 direction = target - game.lion.Position;
            direction.Normalize();
            target += direction * 20;

            game.lion.pounceTarget = new Entity(target);
            return new SteeringOutput();
        }
    }
}
