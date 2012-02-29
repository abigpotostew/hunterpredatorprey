using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Steering.Collision;
using Steering.FSM;

namespace Steering
{ 
    public class Deer : Entity
    {
        const float maxDeerSpeed = 2f;
        const float maxAccelleration = 0.3f;

        

        //public List<Entity> neighbors;

        //FiniteStateMachine fsm = new FiniteStateMachine();

         public Deer(Texture2D image, Vector2 position)
            : base(image, position, maxAccelleration, maxDeerSpeed)
        {
            
            //boundingCircle = new Collision.Circle(position.X, position.Y, 75);
            //debugCircle = new PrimitiveLine(position, Color.Pink);
        }

         /*public override void Update(SteeringOutput steering, GameTime time)
         {
             base.Update(steering, time);
             //Console.WriteLine(Orientation);
         }*/

         
    }
}
