using Microsoft.Xna.Framework;
using System;

namespace Steering.FSM.Actions
{
    class SetLionTargetToDeerAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            Vector2 target = game.lion.closestDeerTarget.Position;
            Vector2 direction = target - game.lion.Position;
            //Console.WriteLine("Dist to target: " + direction.Length());
            direction.Normalize();
            target += direction * 20;

            game.lion.pounceTarget = new Entity(target);
            return new SteeringOutput();
        }
    }
}
