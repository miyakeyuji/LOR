using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Lamentationofrevenge
{
	public class AppMain
	{
		private static BaseScene _scene;
		private static GraphicsContext _context;
		private static Timer _time;
		private static string[] _textPass =
		{
			"/Application/data/text/TutorialText.txt",
			"/Application/data/text/selecttest.txt",
			"/Application/data/text/prologue.txt",
		};
		
		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			_context = new GraphicsContext (960, 544, PixelFormat.None, PixelFormat.None, MultiSampleMode.None);
			
			uint sprites_capacity = 500;
			uint draw_helpers_capacity = 500;

			Director.Initialize(sprites_capacity,draw_helpers_capacity,_context);

			bool manual_loop = true;
			
			_scene = new BaseScene();
			
			Director.Instance.RunWithScene(_scene,manual_loop);

			_time = new Timer();
						
			_time.Reset();
		}
		
		public static void ReplaceScene()
		{
			CheckScene();
			if(_scene != Director.Instance.CurrentScene)
			{
				//Director.Instance.ReplaceScene(_scene);
				Director.Instance.ReplaceScene(
				new TransitionCrossFade(_scene)
				{
					Duration = 2.0f,
					Tween = (x) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(x,3.0f)
				});
			}
		}
		
		public static void CheckScene()
		{
			if(_scene.GetType().FullName == "Lamentationofrevenge.BaseScene") _scene = new ADVPart(_textPass[0]);
			
			if(_scene.NextSceneName() != null)
			{
				if(_scene.NextSceneName() == "ADVPart")
				{
					if(_scene.TakeTextPass() != null)_scene = new ADVPart(_scene.TakeTextPass());
					_scene = new ADVPart(_textPass[0]);
				}
				if(_scene.NextSceneName() == "DeliveryLetter") _scene = new DeliveryLetter();
			}
		}
		
		public static void contorolDate()
		{
		}
		
		public static void Update ()
		{
			ReplaceScene();
			_scene.Update();
			Director.Instance.Update();
		}

		public static void Render ()
		{
			Director.Instance.Render();
			
			Director.Instance.GL.Context.SwapBuffers();
			
			Director.Instance.PostSwap();

			_scene.Render();
		}
	}
}