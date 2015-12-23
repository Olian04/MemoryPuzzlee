using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MemoryPuzzlee.src.Objects
{
    class Tile : Interfaces.Object
    {
        private static Texture2D backgroundTexture;
        private static List<Texture2D> itemTextures;
        private static Point size;

        private static int latestItemIndex;

        public static void Init(ContentManager Content, float scale) {
            backgroundTexture = Content.Load<Texture2D>("pixel.png");
            latestItemIndex = 0;

            itemTextures = new List<Texture2D>();
            for (int i = 0; i < 10; i++) {
                itemTextures.Add(Content.Load<Texture2D>("Items/item" + i + ".png"));
            }
            itemTextures.AddRange(itemTextures);
            blendItemTextures();
        }

        private static void blendItemTextures() {
            int x = itemTextures.Count;
            Random rand = new Random();
            for (int i = 0; i<x; i++) {
                int a = rand.Next(0, x), b = rand.Next(0, x);
                Texture2D holder = itemTextures[a];
                itemTextures[a] = itemTextures[b];
                itemTextures[b] = holder;
            }
        }

        public static Point getSize()
        {
            return size;
        }
        public static void setSize(Point newSize) {
            size = newSize;
        }

        //-------------------------------------------//

        private Texture2D itemTexture;
        private Rectangle rect;
        private bool isShown;

        public Tile(Point pos)
        {         
            this.itemTexture = itemTextures[latestItemIndex++];
            this.rect = new Rectangle(pos, size);
            this.isShown = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isShown)
                spriteBatch.Draw(backgroundTexture, rect, Color.White);
            else {
                spriteBatch.Draw(backgroundTexture, rect, Color.Black);
                spriteBatch.Draw(itemTexture, rect, Color.White);
            }
        }

        public void Update(GameTime gameTime, Rectangle cursorRect)
        {
        }

        public void toggleIsShown()
        {
            isShown = !isShown;
        }

        public bool getIsShown() {
            return isShown;
        }

        public Rectangle getRect()
        {
            return rect;
        }

        public void setIsShown(bool isShown)
        {
            this.isShown = isShown;
        }

        public Texture2D getTexture() {
            return itemTexture;
        }
    }
}
