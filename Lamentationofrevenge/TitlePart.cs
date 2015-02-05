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
		private string _takePass;
		private string[] titleGraphicPass =
		{
			"/Application/data/title/titlebackground.jpg",
			"/Application/data/title/titlelogo.png",
		};

		private string[] iconGraphicPass =
		{
			"/Application/data/title/newgame.png",
			"/Application/data/title/load.png",
			"/Application/data/title/gallery.png",
			"/Application/data/title/option.png",
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
			Initialize();
		}
		
		public override void AddGraphic (string dataPass, Vector2 position)
		{
			base.AddGraphic (dataPass, position);
		}		
		
		public override void Initialize ()
		{
			for(int i = 0 ; i < titleGraphicPass.Count();i++)
			{
				AddGraphic(titleGraphicPass[i] , graphicPositon[i]);
			}

			for(int i = 0 ; i < iconGraphicPass.Count();i++)
			{
				AddGraphic(iconGraphicPass[i] , graphicPositon[i + 2]);
			}
			
			ContorolSound();
			
			_nextScene = "";
		}
		
		public override void ContorolSound ()
		{
			_bgm = new Bgm("/Application/data/title/main_141208.mp3");
			
			_bgmPlayer = _bgm.CreatePlayer();
			
			_bgmPlayer.Play();
		}
		
		private void ButtonContorol()
		{
			GamePadData data = GamePad.GetData(0);
		
			if(Input2.GamePad0.Start.Press)
			{
				
			}
			
			if(Input2.GamePad0.Circle.Press || Input2.GamePad0.Up.Press)
			{
				_takePass = "/Application/data/text/TutorialText.txt";
				_nextScene = "ADVPart";
				_bgmPlayer.Dispose();
			}
		}

		public override string NextSceneName()
		{
			return _nextScene;
		}
		
		public override string TakeTextPass ()
		{
			return _takePass;
		}
		
		public override void Update ()
		{
			base.Update ();
			ButtonContorol();
		}
		
		public override void Render ()
		{
			base.Render ();
		}
	}
}

