using Microsoft.Xna.Framework;
using System;

namespace Steering.FSM.Actions
{
    class SetLionTargetToHunterAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.pounceMisses++;
            Vector2 target = game.playerHunter.Position;
            Vector2 direction = target - game.lion.Position;
            direction.Normalize();
            target += direction * 20;

            game.lion.pounceTarget = new Entity(target);
            return new SteeringOutput();
        }
    }
}
