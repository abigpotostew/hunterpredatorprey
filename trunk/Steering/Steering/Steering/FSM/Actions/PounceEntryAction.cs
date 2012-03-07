using Microsoft.Xna.Framework;

namespace Steering.FSM.Actions
{
    class PounceEntryAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            Vector2 target = game.lion.closestDeerTarget.Position;
            Vector2 direction = target - game.lion.Position;
            direction.Normalize();
            target += direction * 20;

            game.lion.pounceTarget = new Entity(target);
            return new SteeringOutput();
        }
    }
}
