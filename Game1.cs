using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Hexaplicate
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Grid _grid;
        private UI.UIManager _uiManager;
        private Inventory _inventory;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Constants.SCREEN_SIZE.Item1;
            _graphics.PreferredBackBufferHeight = Constants.SCREEN_SIZE.Item2;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _grid = new Grid();
            _uiManager = new UI.UIManager();
            _inventory = new Inventory();
            _grid.setCoordinates((  (int)(Constants.SCREEN_SIZE.Item1 * Constants.GRID_OFFSET.Item1),
                (int)(Constants.SCREEN_SIZE.Item2 * Constants.GRID_OFFSET.Item2)));
            _grid.RegisterHexs(_uiManager);
            _inventory.setCoordinates(((int)(Constants.SCREEN_SIZE.Item1 * Constants.INVENTORY_GRID_OFFSET.Item1),
                (int)(Constants.SCREEN_SIZE.Item2 * Constants.INVENTORY_GRID_OFFSET.Item2)));
            _inventory.RegisterHexs(_uiManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            EssenceHexagon.SetTexture(new Texture2D[] { Content.Load<Texture2D>("alphaHexagon") });
            EmptyHexagon.SetTexture(new Texture2D[] { Content.Load<Texture2D>("centerHexagon") });
            Grid.SetTexture( Content.Load<Texture2D>("connectionConnected") );
            UI.UIInfoDisplay.SetTexture(new Texture2D[] { Content.Load<Texture2D>("UIColor") });
            UI.UIInfoDisplay.font = Content.Load<SpriteFont>("file");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            _uiManager.checkState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(hexagonTexture, new Rectangle(0, 0, (int)(612 * scale),(int)(530*scale)), Color.White);
            _grid.Draw(_spriteBatch);
            _inventory.Draw(_spriteBatch);
            _uiManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}