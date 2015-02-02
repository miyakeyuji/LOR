using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace Lamentationofrevenge
{
	public class BaseScene : Scene
	{
		public BaseScene ()
		{
		}

		public virtual void Initialize(){return;}
		public virtual void Update(){return;}
		public virtual void Render(){return;}
		
		public virtual void AddGraphic(string dataPass , Vector2 position)
		{
			Camera.SetViewFromViewport();
				
			var texture = new Texture2D(dataPass,false);
			var textureInfo = new TextureInfo(texture);
			
			var sprite = new SpriteUV(){TextureInfo = textureInfo};
		
			sprite.Quad.S = textureInfo.TextureSizef;
			
			sprite.CenterSprite();
			
			sprite.Position = position;
			
			AddChild(sprite);
		}
		
		public virtual void ContorolSound(){return;}
		public virtual void ContorolGraphic(){return;}
		public virtual void RemoveScene(){return;}
		public virtual string NextSceneName(){return "";}
	}
}

