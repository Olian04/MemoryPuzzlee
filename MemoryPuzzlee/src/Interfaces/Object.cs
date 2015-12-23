using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryPuzzlee.src.Interfaces
{
    interface Object
    {
        void Update(GameTime gameTime, Rectangle cursorRect);
        void Draw(SpriteBatch spriteBatch);
        void toggleIsShown();
        void setIsShown(bool isShown);
        Rectangle getRect();
        bool getIsShown();
        Texture2D getTexture();
    }
}
