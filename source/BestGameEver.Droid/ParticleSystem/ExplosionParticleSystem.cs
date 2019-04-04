using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BestGameEver.Droid.ParticleSystem
{
	public class ExplosionParticleSystem : ParticleSystem
	{
		public ExplosionParticleSystem(Game game, int howManyEffects)
			: base(game, howManyEffects)
		{
		}

		protected override void InitializeConstants()
		{
			this.textureFilename = "particleExplosion";

			// high initial speed with lots of variance.  make the values closer
			// together to have more consistently circular explosions.
			this.minInitialSpeed = 40;
			this.MaxInitialSpeed = 500;

			// doesn't matter what these values are set to, acceleration is tweaked in
			// the override of InitializeParticle.
			this.minAcceleration = 0;
			this.maxAcceleration = 0;

			// explosions should be relatively short lived
			this.minLifetime = .5f;
			this.maxLifetime = 1.0f;

			this.minScale = .3f;
			this.maxScale = 1.0f;

			this.minNumParticles = 20;
			this.maxNumParticles = 25;

			this.minRotationSpeed = -MathHelper.PiOver4;
			this.maxRotationSpeed = MathHelper.PiOver4;

			// additive blending is very good at creating fiery effects.
			this.blendState = BlendState.Additive;

			this.DrawOrder = AdditiveDrawOrder;
		}

		protected override void InitializeParticle(Particle p, Vector2 where)
		{
			base.InitializeParticle(p, where);

			// The base works fine except for acceleration. Explosions move outwards,
			// then slow down and stop because of air resistance. Let's change
			// acceleration so that when the particle is at max lifetime, the velocity
			// will be zero.

			// We'll use the equation vt = v0 + (a0 * t). (If you're not familar with
			// this, it's one of the basic kinematics equations for constant
			// acceleration, and basically says:
			// velocity at time t = initial velocity + acceleration * t)
			// We'll solve the equation for a0, using t = p.Lifetime and vt = 0.
			p.Acceleration = -p.Velocity / p.Lifetime;
		}
	}
}
