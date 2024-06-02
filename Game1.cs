using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hexaplicate
{
    public class Game1 : Game
    {

        Texture2D hexagonTexture;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Grid _grid;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _grid = new Grid();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            EssenceHexagon.SetTexture(new Texture2D[] { Content.Load<Texture2D>("alphaHexagon") });
            EmptyHexagon.SetTexture(new Texture2D[] { Content.Load<Texture2D>("centerHexagon") });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(hexagonTexture, new Rectangle(0, 0, (int)(612 * scale),(int)(530*scale)), Color.White);
            _grid.Draw(_spriteBatch, (640,360));
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}