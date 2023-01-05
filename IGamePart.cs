using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
	interface IGamePart
	{
		public void Load(ContentManager content);
		public void Update(float dt);
		public void Draw(SpriteBatch spriteBatch, float dt);
		public void Unload(ContentManager content);
	}
}
