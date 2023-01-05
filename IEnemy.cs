using Microsoft.Xna.Framework;

namespace PlatformerGame
{
	enum PlayerEnemyContactInteraction : byte
	{
		/// <summary>
		/// The player renders above the entity
		/// </summary>
		Ignore,
		/// <summary>
		/// The entity acts like a solid block
		/// </summary>
		ActAsSolid,
		/// <summary>
		/// Damages the player
		/// </summary>
		TakeDamage_Player,
		/// <summary>
		/// Damages and throws away the player
		/// </summary>
		TakeDamageAndThrow_Player,
		/// <summary>
		/// Kills the player
		/// </summary>
		Kill_Player,
		/// <summary>
		/// Damages the enemy
		/// </summary>
		TakeDamage_Enemy,
		/// <summary>
		/// Damages, and throws away the entity
		/// </summary>
		TakeDamageAndThrow_Enemy,
		/// <summary>
		/// Kills the entity
		/// </summary>
		Kill_Enemy,
	}

	enum ProjectileInteraction : byte
	{
		/// <summary>
		/// Projectile passes trough entity
		/// </summary>
		Ignore,
		/// <summary>
		/// Entity takes damage
		/// </summary>
		TakeDamage,
		/// <summary>
		/// Projectile is absorbed by the entity
		/// </summary>
		RemoveProjectile,
	}

	interface IEnemy : IEntity
	{
		public Vector2 Position { get; set; }
		public RectangleF Hitbox { get; }
		public bool Alive { get; }
		public int Health { get; }
		public PlayerEnemyContactInteraction OnContactWithPlayer(Player p,bool from_above);
		public ProjectileInteraction OnContactWithProjectile();
		public void Hurt();
		public void Throw();
		public void Kill();
	}
}
