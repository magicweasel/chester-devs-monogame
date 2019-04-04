using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BestGameEver.Droid
{
	public class PlayerOne : Entity
	{
		public PlayerOne(Texture2D texture, Vector2 vector) : base(texture, vector)
		{

		}

		private bool isJumping = false;

		private Vector2 initialPosition;
		private Vector2 jumpPosition;

		private bool goingUp = true;

		private float jumpDivision = 2f;

		public void Jump()
		{
			if (this.isJumping) return;

			this.initialPosition = this.Position;
			this.jumpPosition = this.Position + new Vector2(0, -200);
			this.goingUp = true;
			this.jumpDivision = 2;
			this.isJumping = true;
		}


		public override void Update(GameTime gameTime)
		{
			if (this.isJumping)
			{
				if (this.goingUp)
				{
					if (this.Position.Y > this.jumpPosition.Y)
					{
						this.Position -= new Vector2(0, gameTime.ElapsedGameTime.Milliseconds / this.jumpDivision);
       
                        this.jumpDivision += gameTime.ElapsedGameTime.Milliseconds / 200f;
                 
                    }
                    else
					{
						this.goingUp = false;
					}
				}
				else
				{
					if (this.Position.Y < this.initialPosition.Y)
					{
						this.Position += new Vector2(0, gameTime.ElapsedGameTime.Milliseconds / this.jumpDivision);
              
                        this.jumpDivision -= gameTime.ElapsedGameTime.Milliseconds / 200f;
   
                    }
                    else
					{
						this.isJumping = false;
					}
				}
			}

			base.Update(gameTime);
        }

	    #region ...
	    #region animation

        public override void Draw(SpriteBatch spriteBatch)
	    {
	        base.Draw(spriteBatch);
	    }

	    #endregion
	    #endregion
    }
}