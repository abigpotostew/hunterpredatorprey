using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Collision
{
    //NOTE: an SCircle has a position equal to the center, never use the position for real collision checks

    public class Circle
    {
        public Vector2 position;
        //private Vector2 v;
        //private Vector2 direction;
        private double distanceSquared;
        //the center is the normal collisionShape center, not accounting for in-game center
        protected Vector2 center;
        protected float radius;
        protected PrimitiveLine debugCircle;

        #region  CONSTRUCTOR /////////////////////////////////////
        public Circle(int x, int y, float radius)
        {
            this.position = new Vector2(x, y);
            this.distanceSquared = 0f;
            this.center = new Vector2(x, y);
            this.radius = radius;
        }

        public Circle(float x, float y, float radius)
        {
            this.position = new Vector2(x, y);
            this.distanceSquared = 0f;
            this.center = new Vector2(x, y);
            this.radius = radius;
        }
        #endregion

        #region  PROPERTIES  //////////////////////////////////////
        //for the SCircle, position and center are equal
        public Vector2 Position { get { return position; } }
        public Vector2 Center { get { return center; } }
        //returns 0 (rectangle) or 1 (circle)
        public int ShapeType { get { return 1; } }
        public float Radius { get { return radius; } }
        public float Bottom
        {
            get { return center.Y + radius; }
        }

        public float Right
        {
            get { return center.X + radius; }
        }

        public float Left
        {
            get { return center.X - radius; }
        }

        public float Top
        {
            get { return center.Y - radius; }
        }

        public int Height
        {
            get { return (int)(2 * radius); }
        }

        public int Width { get { return (int)(2 * radius); } }
        #endregion


        public bool Intersects(Circle shape)
        {
            distanceSquared = (shape.Center.X - Center.X) * (shape.Center.X - Center.X) + (shape.Center.Y - Center.Y) * (shape.Center.Y - Center.Y);
            return ((distanceSquared > 0) && (distanceSquared < (Radius + shape.Radius) * (Radius + shape.Radius)));
            
        }



    }
}
