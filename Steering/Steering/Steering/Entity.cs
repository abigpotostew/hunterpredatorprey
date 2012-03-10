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
        protected float maxAcceleration, maxSpeed, maxSpeedSq, maxAngularAcceleration, maxRotation, wanderOrientation, wanderSpeed;
        protected Texture2D image;
        //an entity is drawn in the center of it image
        protected Vector2 offsetToCenter;

        public Circle boundingCircle;
        public PrimitiveLine debugCircle;

        public List<Entity> neighbors;
        public bool isColliding = false, wander;

        public int health;
        public int damage;

        #region Properties
        public Texture2D Image { get { return image; } set { image = value; } }
        public Vector2 Position { get { return position; } }
        public float MaxSpeed { get { return maxSpeed; } }
        public float MaxAcceleration { get { return maxAcceleration; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public float MaxAngularAcceleration { get { return maxAngularAcceleration; } }
        public float MaxRotation { get { return maxRotation; } }
        public float Orientation { get { return orientation; } set { orientation = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }
        public float WanderOrientation { get { return wanderOrientation; } set { wanderOrientation = value; } }
        public float WanderSpeed { get { return wanderSpeed; } set { wanderSpeed = value; } }
        public float fear, fearToBeAdded, maxFear = 2;
        #endregion

        #region Constructors
        public Entity(Texture2D image, Vector2 position, float maxAcc, float maxSpe)
        {
            boundingCircle = new Circle(position.X, position.Y, 50);
            debugCircle = new PrimitiveLine(position, Color.White);
            debugCircle.CreateCircle(50, 20);

            velocity = new Vector2();

            this.image = image;
            this.position = position;
            this.maxAcceleration = maxAcc;
            this.maxSpeed = maxSpe;
            rotation = orientation =  wanderOrientation = 0;
            offsetToCenter = new Vector2(image.Width/2, image.Height/2);
            position += offsetToCenter;

            //for now, make max angular rotation and rotation statndard
            this.maxRotation = 0.2f;
            this.maxAngularAcceleration = 1;

            maxSpeedSq = maxSpeed * maxSpeed;

            neighbors = new List<Entity>();

            wanderSpeed = 0.5f;
        }

        public Entity(Vector2 position)
        {
            image = null;
            this.position = position;
            offsetToCenter = Vector2.Zero;
            maxAcceleration = maxSpeed = maxSpeedSq = rotation =
                orientation = maxRotation = maxAngularAcceleration =  wanderOrientation = 0;

        }

        public Entity()
        {
            image = null;
            position = offsetToCenter = Vector2.Zero;
            maxAcceleration = maxSpeed = maxSpeedSq = rotation =
                orientation = maxRotation = maxAngularAcceleration = wanderOrientation = 0;
        }
        #endregion

        public virtual void Update(SteeringOutput steering, GameTime time)
        {
            position += velocity; //*time
            orientation += rotation; //*time

            velocity += steering.linear;
            orientation += steering.angular;

            //velocity needs to be capped based on steering maxspeed
            float tmpMaxSpeed;
            if (steering.maxSpeed > 0) tmpMaxSpeed = steering.maxSpeed;
            else tmpMaxSpeed = maxSpeed;

            if (velocity.LengthSquared() > tmpMaxSpeed*tmpMaxSpeed)
            {
                velocity.Normalize();
                velocity *= tmpMaxSpeed;
            }

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
        }

        public virtual void Draw(GameTime time, SpriteBatch sb)
        {
            if (isColliding) debugCircle.Colour = Color.Tomato;
            else debugCircle.Colour = Color.White;
            //debugCircle.Draw(sb);
            //+offsetToCenter
            sb.Draw(image, (position), null, Color.White, (float)(orientation+Math.PI/2), offsetToCenter,1f,SpriteEffects.None,0);
            if ( this is Deer ) sb.DrawString(Game.Font, "" + (int)this.fear, position, Color.White);
            //sb.DrawString(Game.Font, 
        }

        public virtual void Draw(GameTime time, SpriteBatch sb, bool invisible)
        {
            sb.Draw(image, position, null, new Color(128,128,128,0), (float)(orientation + Math.PI / 2), offsetToCenter, 1f, SpriteEffects.None, 0);
        }

        public void updateFear()
        {
            this.fear += this.fearToBeAdded;
            this.fearToBeAdded = 0;
        }

        public void addFear(float f)
        {
            if (f > maxFear) f = maxFear;
            else if (f < -maxFear) f = -maxFear;
            

            if( f + this.fear > 100)
            {
                this.fearToBeAdded = 100 + this.fearToBeAdded - this.fear;
            }
            else
            {
                this.fearToBeAdded += f;
            }
        }
        public void decayFear()
        {
            if (this.fear > 1)
            {
                this.fear *= .99f;
            }
            else
            {
                this.fear = 1f;
            }
        }
        /*public bool isColliding(Entity other)
        {
            return ( this.boundingCircle.Intersects(other.boundingCircle) );
        }*/
    }
}
