using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BestGameEver.Win
{
	public class Background : Entity
	{
		public Background(Texture2D texture, Vector2 position, bool flip = false) : base(texture, position)
		{
			this.Flip = flip;
		}

		public bool Flip { get; set; }

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this.Flip)
			{
				spriteBatch.Draw(this.Texture, this.Position, effects: SpriteEffects.FlipHorizontally);
			}
			else
			{
				spriteBatch.Draw(this.Texture, this.Position);	
			}
			
		}
	}
}