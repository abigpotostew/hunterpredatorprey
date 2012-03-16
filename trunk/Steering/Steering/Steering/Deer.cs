using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Steering.Collision;
using Steering.FSM;
using Steering.Steering;

namespace Steering
{ 
    public class Deer : Entity
    {
        const float maxDeerSpeed = 2f;
        const float maxAccelleration = 0.5f;
        public float intelligence;

        Game game;

        public FiniteStateMachine fsm;

         public Deer(Game g, Texture2D image, Vector2 position)
            : base(image, position, maxAccelleration, maxDeerSpeed)
        {
            game = g;
            intelligence = 2*(float)Game.r.NextDouble();
            if (intelligence > 1) intelligence = 1;

            wanderSpeed = 1f;
            health = 1;
            damage = 0;
        }

         public override void Update(SteeringOutput steering, GameTime time)
         {
             List<IAction> actions = fsm.UpdateFSM(game,this);
             foreach (IAction steeringAction in actions)
             {
                 steering += steeringAction.execute(game, this);
             }
             
             //always do look where going 
             base.Update(Steerings.lookWhereGoing.getSteering(this) + Steerings.bushSeparation.getSteering(this,game.gameWorld.getBushes()) + steering, time);
             
         }

         public override void Draw(GameTime gt, SpriteBatch sb)
         {
             base.Draw(gt, sb);
         }
         
    }
}
