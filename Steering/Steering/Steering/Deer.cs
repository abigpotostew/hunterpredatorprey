﻿using System;
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
        const float maxAccelleration = 0.3f;

        //INTELLIGENCE is between 0 and 1,
        //1 is smart and will rarely wander away from herd
        //0 is very dumb and has a higher cahnce of wandering away and getting eaten
        //a deer has a higher chance of being smart than dumb
        public float intelligence;

        Game game;
        

        //public List<Entity> neighbors;

        public FiniteStateMachine fsm;

         public Deer(Game g, Texture2D image, Vector2 position)
            : base(image, position, maxAccelleration, maxDeerSpeed)
        {
            game = g;
            intelligence = 2*(float)Game.r.NextDouble();
            if (intelligence > 1) intelligence = 1;
        }

         public override void Update(SteeringOutput steering, GameTime time)
         {
             List<IAction> actions = fsm.UpdateFSM(game,this);
             foreach (IAction steeringAction in actions)
             {
                 steering += steeringAction.execute(game, this);
             }

             base.Update(Steerings.lookWhereGoing.getSteering(this) + steering, time);

             //always do look where going 
         }

         
    }
}
