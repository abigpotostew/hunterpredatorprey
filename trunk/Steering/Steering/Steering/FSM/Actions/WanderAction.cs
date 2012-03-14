using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class WanderAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            SteeringOutput result = Steerings.wander.getSteering(character)
                + Steerings.separationFromHunter.getSteering(character, game.playerHunter);
            /*if (result.linear.LengthSquared() >character.WanderSpeed*character.WanderSpeed ){
                result.linear.Normalize();
                result.linear *= character.WanderSpeed;
            }*/
            result.maxSpeed = character.WanderSpeed;
            return result;// +
                   //Steerings.separationFromDeer.getSteering(character, character.neighbors);// +
                   //Steerings.separationFromLion.getSteering(character, game.lion);
                   //Steerings.cohesion.getSteering(character, character.neighbors)+
                   //Steerings.velocityMatch.getSteering(character,character.neighbors);
        }
    }
}
