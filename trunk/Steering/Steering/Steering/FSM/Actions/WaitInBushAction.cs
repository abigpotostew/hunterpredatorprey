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
        bool watchHunter;

        public WaitInBushAction(bool watchHunter)
        {
            this.watchHunter = watchHunter;
        }
        SteeringOutput IAction.execute(Game game, Entity character)
        {
            //stop the lion from moving
            character.Velocity = new Vector2();

            //return look at target
            if (watchHunter)
                return Steerings.face.getSteering(character, game.playerHunter);
            else //look at deer
            {
                game.lion.closestDeerTarget = game.deerManager.FindClosestDeer(game.lion.Position);
                return Steerings.face.getSteering(character, game.lion.closestDeerTarget);
            }
        }
    }
}
