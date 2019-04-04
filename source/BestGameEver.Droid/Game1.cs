using System;
using System.Collections.Generic;
using System.Linq;
using BestGameEver.Droid.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace BestGameEver.Droid
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public List<IEntity> Entities = new List<IEntity>();


		private PlayerOne playerOne;

		public Scenery Scenery = new Scenery();

		private Enemy enemy;

		private Buffer buffer;
		private Explosion explosion;

		public Game1()
		{
			this.graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 800,
				PreferredBackBufferHeight = 600
			};

			this.graphics.ApplyChanges();
			this.Content.RootDirectory = "Content";

			this.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

			
		}

		protected override void LoadContent()
		{
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			var screenWidth = 800;
			var screenHeight = 600;


			this.playerOne = new PlayerOne(this.Content.Load<Texture2D>("playerOne"), new Vector2(screenWidth * 0.1f, screenHeight * 0.6f));
			this.Entities.Add(this.playerOne);


			this.Scenery.LoadScenery(this.Content.Load<Texture2D>("background"));

			this.enemy = new Enemy(this.Content.Load<Texture2D>("enemy"), new Vector2(screenWidth * 1.2f, screenHeight * 0.6f));
			this.Entities.Add(this.enemy);


			this.explosion = new Explosion(this);
			this.explosion.LoadTexture(this.Content.Load<Texture2D>("explosion"));

			this.Window.ClientSizeChanged += this.HandleResize;

			this.buffer = new Buffer(this.graphics.GraphicsDevice);

		}

		private void HandleResize(object sender, EventArgs e)
        {
            this.buffer.Resize();

	        

        }


		protected override void Update(GameTime gameTime)
		{
			this.Scenery.Update(gameTime);

			this.Entities.ForEach(x => x.Update(gameTime));

			if (this.Scenery.XPosition > 200 && !this.enemy.IsActive)
			{
				this.enemy.Activate();
			}



			if (CollisionDetection.EntityCollision(this.playerOne, this.enemy))
			{
				this.enemy.MakeDead();
				this.explosion.Explode(this.enemy);
			}


			this.explosion.Update(gameTime);

			this.HandleInput();
			base.Update(gameTime);

		}

		private void HandleInput()
		{
			var thirdOfScreen = this.graphics.GraphicsDevice.Viewport.Width / 3;


			var touchCollection = TouchPanel.GetState();

			if (touchCollection.Any(x => x.Position.X > thirdOfScreen && x.Position.X < thirdOfScreen * 2))
			{
				this.playerOne.Jump();
			}

			if (touchCollection.Any(x => x.Position.X > thirdOfScreen * 2))
			{
				this.Scenery.Move(SceneryMovement.Backward);
			}
			else if (touchCollection.Any(x => x.Position.X < thirdOfScreen))
			{
				this.Scenery.Move(SceneryMovement.Forward);
			}
			else
			{
				this.Scenery.Move(SceneryMovement.None);
			}
		}


		protected override void Draw(GameTime gameTime)
		{
			this.buffer.InitBuffer();

			this.DrawToBuffer(gameTime);

			this.buffer.DrawBufferToScreen(this.spriteBatch);
		}

		private void DrawToBuffer(GameTime gameTime)
		{
			this.GraphicsDevice.Clear(Color.Black);

			this.spriteBatch.Begin();

			this.Scenery.Draw(this.spriteBatch);
			this.Entities.ForEach(x => x.Draw(this.spriteBatch));

			this.spriteBatch.End();

			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			this.explosion.Draw(this.spriteBatch, gameTime);
			this.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
