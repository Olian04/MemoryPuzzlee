using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MemoryPuzzlee.src.Objects;
using MemoryPuzzlee.src.Interfaces;

namespace MemoryPuzzlee
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle cursorRect;
        List<Object> tiles;
        MouseState ms, oldms;
        float windowScaler = 2;

        List<Object> tilesShowing;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = (int)(720 * windowScaler);
            graphics.PreferredBackBufferWidth = (int)(1080 * windowScaler);
            graphics.ApplyChanges();
            Window.Position = new Point(300, 100);
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            setupTiles();

            cursorRect = new Rectangle(0,0,1,1);

            base.Initialize();
        }

        private void setupTiles() {
            Tile.Init(Content, windowScaler);
            tiles = new List<Object>();
            tilesShowing = new List<Object>();

            Point margin = new Point(10, 10);
            Point rows = new Point(5,4);
            Point pointScalar = new Point(graphics.PreferredBackBufferWidth / rows.X - margin.X - 2, graphics.PreferredBackBufferHeight / rows.Y - margin.Y - 2);

            Tile.setSize(pointScalar);

            for (int X = 0; X < rows.X; X++)
                for (int Y = 0; Y < rows.Y; Y++)
                    tiles.Add(new Tile(new Point(
                        X * (Tile.getSize().X + margin.X) + margin.X, 
                        Y * (Tile.getSize().Y + margin.Y) + margin.Y
                        )));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ms = Mouse.GetState();

            cursorRect.Location = ms.Position;

            if (ms.LeftButton == ButtonState.Pressed && oldms.LeftButton == ButtonState.Released)
                foreach (Object t in tiles)
                    if (cursorRect.Intersects(t.getRect())) {
                        if (tilesShowing.Count > 1)
                        {
                            foreach (Object T in tiles)
                                T.setIsShown(false);
                        }
                        t.toggleIsShown();
                    }

            tilesShowing.Clear();
            foreach (Object t in tiles) {
                if (t.getIsShown()) {
                    tilesShowing.Add(t);
                }
            }

            if (tilesShowing.Count == 2) {
                if (tilesShowing[0].getTexture() == tilesShowing[1].getTexture()) {
                    tiles.Remove(tilesShowing[0]);
                    tiles.Remove(tilesShowing[1]);
                }
            }

            if (tiles.Count == 0) {
                setupTiles();
            }

            oldms = ms;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (Object t in tiles) {
                t.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
