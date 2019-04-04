using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BestGameEver.Win
{
    public class Enemy : Entity
    {
	    public Vector2 initialPosition;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
	        this.initialPosition = position;
        }

		private bool isActive = false;

	    public bool IsActive
	    {
		    get { return this.isActive; }
	    }

	    public void Activate()
	    {
		    this.isActive = true;
		    this.Position = this.initialPosition;
	    }

	    public override void Update(GameTime gameTime)
	    {
		    if (this.isActive)
		    {
				if (this.Position.X + this.Texture.Width > 0)
				{
					this.Position -= new Vector2(gameTime.ElapsedGameTime.Milliseconds / 2f, 0);
				}
				else
				{
					this.isActive = false;
					this.Position = this.initialPosition;
				}
		    }
	    }

	    public override void Draw(SpriteBatch spriteBatch)
	    {
		    if (this.isActive)
		    {
				base.Draw(spriteBatch);
		    }
		    
	    }

	    public void MakeDead()
	    {
		    this.isActive = false;
	    }
    }
}
