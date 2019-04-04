using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BestGameEver.Win.ParticleSystem
{
	public class Explosion
	{
		private ExplosionParticleSystem explosionParticleSystem;

		public Explosion(Game game)
		{
			this.explosionParticleSystem = new ExplosionParticleSystem(game, 1);
			this.explosionParticleSystem.Initialize();
			this.explosionParticleSystem.IsActive = true;

		}

		public void Explode(IEntity entity)
		{
			this.explosionMilliseconds = 800;
			this.position = entity.Position;
		}

		public float explosionMilliseconds;
		public Vector2 position;

		public void Update(GameTime gameTime)
		{
			this.explosionParticleSystem.Update(gameTime);

			if (this.explosionMilliseconds > 0)
			{
				this.explosionMilliseconds -= gameTime.ElapsedGameTime.Milliseconds;
				this.explosionParticleSystem.AddParticles(this.position);
			}
			
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			this.explosionParticleSystem.Draw(spriteBatch, gameTime);
		}

		public void LoadTexture(Texture2D texture)
		{
			this.explosionParticleSystem.LoadContent(texture);
		}
	}
}
