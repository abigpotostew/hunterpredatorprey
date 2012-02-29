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

        public Hunter guy;
        public Lion lion;
        //Deer deer;

        const int deerCt = 5;
        DeerManager deerManager;

        public static Rectangle bounds = new Rectangle(0,0,1024,768);
        public static KeyboardState keyboard;
        public static MouseState mouse;
            
        Texture2D jaguar;
        Texture2D hunter;

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

            deerManager = new DeerManager(this);
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
            hunter = Content.Load<Texture2D>("hunter");

            guy = new Hunter(hunter,new Vector2(200,200));
            lion = new Lion(jaguar, new Vector2(400, 400));
            //deer = new Deer(jaguar, new Vector2(600,450));
           
            for (int i = 0; i < deerCt; ++i)
            {
                Deer deerTmp = new Deer(jaguar,new Vector2((float)r.NextDouble()*bounds.Width,(float)r.NextDouble()*bounds.Height));
                deerManager.AddDeer(deerTmp);
            }

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

            guy.Update(deerManager.lookWhereGoing.getSteering(guy), gameTime);
            lion.Update(new SteeringOutput(), gameTime);
            deerManager.Update(gameTime);
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

            lion.Draw(gameTime, spriteBatch);
            guy.Draw(gameTime, spriteBatch);
            deerManager.Draw(gameTime, spriteBatch);


            spriteBatch.DrawString(Font, "Use WASD to move & Q and E to rotate", Vector2.Zero, Color.Black);
            //spriteBatch.DrawString(Font, "Deer ori: "+deer.orientation, new Vector2(0, 20), Color.Black);
            //spriteBatch.DrawString(Font, "Deer Vel: " + deer.Velocity, new Vector2(0, 40), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
