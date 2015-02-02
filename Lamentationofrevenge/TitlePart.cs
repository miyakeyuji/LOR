using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Lamentationofrevenge
{
	public class TitlePart : BaseScene
	{
				private Bgm _bgm;
		private BgmPlayer _bgmPlayer;
		private string _useBgm;
		private string _nextScene;
		private string[] titleGraphicPass =
		{
			"/Application/date/Title/titlebackground.jpg",
			"/Application/date/Title/titlelogo.png",
		};

		private string[] iconGraphicPass =
		{
			"/Application/date/Title/newgame.png",
			"/Application/date/Title/load.png",
			"/Application/date/Title/gallery.png",
			"/Application/date/Title/option.png",
		};
		private Vector2[] graphicPositon =
		{
			new Vector2(480,272),
			new Vector2(480,362),
			new Vector2(150,112),
			new Vector2(370,112),
			new Vector2(590,112),
			new Vector2(810,112),
		};
		
		public TitlePart ()
		{
		}
		
		public override void AddGraphic (string dataPass, Vector2 position)
		{
			base.AddGraphic (dataPass, position);
		}		
		
		public override void Initialize ()
		{
			
		}
		
		public override void Update ()
		{
			base.Update ();
		}
		
		public override void Render ()
		{
			base.Render ();
		}
	}
}

