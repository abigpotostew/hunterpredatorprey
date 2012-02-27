using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Steering.Steering;

namespace Steering
{


    /*
     * Notes from TA
     * random speeds at prey start
     * fear overwhelms group coehesion
     * fear level of dear
     *      as soon as fear gets above a certain level
     *      lion goes for easiest prey
     */ 



    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont Font;

        Hunter guy;
        //Deer deer;
        List<Entity> deers;
        const int deerCt = 20;

        public static Rectangle bounds = new Rectangle(0,0,1024,768);
        public static KeyboardState keyboard;
        public static MouseState mouse;
        ISteering face, arrive, velocityMatch, separation, separationFromHunter, lookWhereGoing, flee, cohesion;
        
        Texture2D jaguar;

        public static Texture2D whitepixel;

        World gameWorld;
        Random r;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = bounds.Width;
            graphics.PreferredBackBufferHeight = bounds.Height;

            r = new Random();

            gameWorld = new World(graphics);

        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Font");
            
            gameWorld.loadTiles(this);
            jaguar = Content.Load<Texture2D>("jaguardot");
            whitepixel = Content.Load<Texture2D>("whitepixel");

            guy = new Hunter(jaguar,new Vector2(200,200));
            //deer = new Deer(jaguar, new Vector2(600,450));
            deers = new List<Entity>();
            for (int i = 0; i < deerCt; ++i)
            {
                Deer deerTmp = new Deer(jaguar,new Vector2((float)r.NextDouble()*bounds.Width,(float)r.NextDouble()*bounds.Height));
                deers.Add ((Entity)deerTmp);
            }

            face = new Face( 0.1F, 2, 0.1f);
            arrive = new Arrive(10, 100, 0.1f);
            //velocityMatch = new VelocityMatch();
            separation = new Separation(50);
            separationFromHunter = new Separation(300);
            lookWhereGoing = new LookWhereYourGoing( 0.1f, 2, 0.1f);
            flee = new Flee(10, 10, 0.1f);
            cohesion = new Cohesion(100);
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            guy.Update(new SteeringOutput(), gameTime);
            //deer.Update(flee.getSteering(deer,guy)+ lookWhereGoing.getSteering(deer,guy), gameTime);
            for (int i = 0; i < deers.Count; ++i)
            {
                Entity d = deers[i];
                deers.Remove(d);
                d.Update(separation.getSteering(d, deers) + lookWhereGoing.getSteering(d, guy) +
                    separationFromHunter.getSteering(d, guy),gameTime);// + cohesion.getSteering(d,deers), gameTime);//+ cohesion.getSteering(d,deers)
                deers.Insert(i,d);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameWorld.draw(spriteBatch);

            guy.Draw(gameTime, spriteBatch);
            //deer.Draw(gameTime, spriteBatch);

            foreach (Entity d in deers)
            {
                d.Draw(gameTime, spriteBatch);
            }


            spriteBatch.DrawString(Font, "Use WASD to move & Q and E to rotate", Vector2.Zero, Color.Black);
            //spriteBatch.DrawString(Font, "Deer ori: "+deer.orientation, new Vector2(0, 20), Color.Black);
            //spriteBatch.DrawString(Font, "Deer Vel: " + deer.Velocity, new Vector2(0, 40), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
