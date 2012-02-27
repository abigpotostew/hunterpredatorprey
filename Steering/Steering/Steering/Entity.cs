using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steering.Collision;

namespace Steering
{
    public class Entity
    {
        //position represents the center of the entity
        protected Vector2 position, velocity;
        //rotation is changed by steering, orientation is actual direction this entity is facing.
        public float rotation, orientation;
        protected float maxAcceleration, maxSpeed, maxAngularAcceleration, maxRotation;
        protected Texture2D image;
        //an entity is drawn in the center of it image
        protected Vector2 offsetToCenter;
        //private SteeringOutput prevSteering = new SteeringOutput();

        public Circle boundingCircle;
        protected PrimitiveLine debugCircle;

        public Texture2D Image { get { return image; } set { image = value; } }
        public Vector2 Position { get { return position; } }
        public float MaxSpeed { get { return maxSpeed; } }
        public float MaxAcceleration { get { return maxAcceleration; } }
        public Vector2 Velocity { get { return velocity; } }
        public float MaxAngularAcceleration { get { return maxAngularAcceleration; } }
        public float MaxRotation { get { return maxRotation; } }
        public float Orientation { get { return orientation; } set { orientation = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }

        public Entity(Texture2D image, Vector2 position, float maxAcc, float maxSpe)
        {
            boundingCircle = new Circle(position.X, position.Y, 50);
            debugCircle = new PrimitiveLine(position, Color.Bisque);
            debugCircle.CreateCircle(50, 20);

            velocity = new Vector2();

            this.image = image;
            this.position = position;
            this.maxAcceleration = maxAcc;
            this.maxSpeed = maxSpe;
            rotation = orientation = 0;
            offsetToCenter = new Vector2(image.Width/2, image.Height/2);

            //for now, make max angular rotation and rotation statndard
            this.maxRotation = 0.2f;
            this.maxAngularAcceleration = 1;
        }

        public Entity(Vector2 position)
        {
            image = null;
            this.position = position;
            offsetToCenter = Vector2.Zero;
            maxAcceleration = maxSpeed = rotation =
                orientation = maxRotation = maxAngularAcceleration = 0;
        }

        public Entity()
        {
            image = null;
            position = offsetToCenter = Vector2.Zero;
            maxAcceleration = maxSpeed = rotation =
                orientation = maxRotation = maxAngularAcceleration = 0;
        }

        public virtual void Update(SteeringOutput steering, GameTime time)
        {
            position += velocity; //*time
            orientation += rotation; //*time

            velocity += steering.linear;
            orientation += steering.angular;

            boundingCircle.position = position;
            debugCircle.Position = position;

            if (position.X > Game.bounds.Width)
                position.X = 0;
            if (position.X < 0)
                position.X = Game.bounds.Width;
            if (position.Y > Game.bounds.Height)
                position.Y = 0;
            if (position.Y < 0)
                position.Y = Game.bounds.Height;

            //decay rotation and velocity
            rotation *= 0.97f;
            //velocity *= 0.99f;
        }

        public virtual void Draw(GameTime time, SpriteBatch sb)
        {
            debugCircle.Draw(sb);
            //+offsetToCenter
            sb.Draw(image, (position), null, Color.White, (float)(orientation+Math.PI/2), offsetToCenter,1f,SpriteEffects.None,0);
        }
    }
}
