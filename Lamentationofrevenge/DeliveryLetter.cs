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
	public class DeliveryLetter : BaseScene
	{
		private int _frameCount;
		private int _selectBoxNum;
		private int _haveLetterId;
		private int _succesCount;
		private Random _rand;
		private string _useBgm = "" ;
		private string _nextScene;

		private string[] _materialPass = 
		{	
			"/Application/data/letter/player_room.jpg",
			"/Application/data/letter/time.png",
			"/Application/data/letter/guhiwaku.png",
		};	
		private string[] _SDGraphicPass = 
		{	
			"/Application/data/letter/aruhuSD.png",
			"/Application/data/letter/hubartSD.png",
			"/Application/data/letter/ruisuSD.png",
		};	
		private string[] _letterGraphicPass =
		{
			"/Application/data/letter/aruhuletter.png",
			"/Application/data/letter/hubartletter.png",
			"/Application/data/letter/ruisuletter.png",
		};
		
		private string[] _checkGraphicPass =
		{
			"/Application/data/letter/success.png",
			"/Application/data/letter/failure.png",
			"/Application/data/letter/selecting.png",
		};
		
		private string _boxGraphicPass = "/Application/data/letter/box.png";
		private string _cursolGraphicPass = "/Application/data/letter/yazirushi.png";
		
		private Vector2[] _materialPosition =
		{
			new Vector2(480,272),//背景用座標
			new Vector2(100,470),
			new Vector2(480,470),//背景用座標
		};
		
		private Vector2 _checkPosition = new Vector2(480,470);
		
		private Vector2[] _SDPosition =
		{
			new Vector2(160,250),
			new Vector2(480,250),
			new Vector2(800,250),
		};
		
		private Vector2[] _boxPosition =
		{
			new Vector2(160,200),
			new Vector2(480,200),
			new Vector2(800,200),
		};
		
		private Vector2[] _cursolPosition =
		{
			new Vector2(160,120),
			new Vector2(480,120),
			new Vector2(800,120),
		};
		
		private Vector2[] _letterPosition =
		{
			new Vector2(160,50),
			new Vector2(480,50),
			new Vector2(800,50),
		};
		
		public DeliveryLetter ()
		{
			_selectBoxNum = 1;
			_succesCount = 0;
			_rand = new Random();
			_haveLetterId = GetRandom();
			_frameCount = 3600;
			for(int i = 0 ; i < _materialPass.Count() ; i++)
			{
				AddGraphic(_materialPass[i],_materialPosition[i]);
			}

			for(int i = 0 ; i < _SDGraphicPass.Count() ; i++)
			{
				AddGraphic(_SDGraphicPass[i],_SDPosition[i]);
			}
			
			foreach(var v in _boxPosition)
			{
				AddGraphic(_boxGraphicPass,v);
			}
			
			AddGraphic(_cursolGraphicPass,_cursolPosition[_selectBoxNum]);
			AddGraphic(_letterGraphicPass[_haveLetterId],_letterPosition[_selectBoxNum]);
			AddGraphic(_checkGraphicPass[2],_checkPosition);
		}
		
		public override void AddGraphic (string dataPass, Vector2 position)
		{
			base.AddGraphic(dataPass,position);
		}
		
		public int GetRandom(){ return _rand.Next(0,3);	}
		
		public override void ContorolSound()
		{
			
		}
		
		public void CheckTime()
		{
			_frameCount--;
		}
		
		public void CheckHit()
		{
			if(_selectBoxNum == _haveLetterId)
			{
				RemoveChild(Children.Last(),true);
				RemoveChild(Children.Last(),true);
				RemoveChild(Children.Last(),true);
				AddGraphic(_cursolGraphicPass,_cursolPosition[_selectBoxNum]);
				_haveLetterId = GetRandom();
				AddGraphic(_letterGraphicPass[_haveLetterId],_letterPosition[_selectBoxNum]);
				AddGraphic(_checkGraphicPass[0] , _checkPosition);
				return;
			}
			RemoveChild(Children.Last(),true);
			RemoveChild(Children.Last(),true);
			RemoveChild(Children.Last(),true);
			AddGraphic(_cursolGraphicPass,_cursolPosition[_selectBoxNum]);
			_haveLetterId = GetRandom();
			AddGraphic(_letterGraphicPass[_haveLetterId],_letterPosition[_selectBoxNum]);
			AddGraphic(_checkGraphicPass[1],_checkPosition);
		}
		
		public override string TakeTextPass ()
		{
			return "/Application/data/text/TutorialText.txt";
		}
		
		private void ButtonContorol()
		{
			GamePadData data = GamePad.GetData(0);
			if(Input2.GamePad0.Cross.Press || Input2.GamePad0.Down.Press)
			{
				_nextScene = "ADVPart";
			}
		
			if(Input2.GamePad0.Circle.Press || Input2.GamePad0.Up.Press)
			{
				CheckHit();
			}
			if(Input2.GamePad0.Left.Press)
			{
				if(_selectBoxNum > 0)
				{
					_selectBoxNum--;
					RemoveChild(Children.Last(),true);
					RemoveChild(Children.Last(),true);
					RemoveChild(Children.Last(),true);
					AddGraphic(_cursolGraphicPass,_cursolPosition[_selectBoxNum]);
					AddGraphic(_letterGraphicPass[_haveLetterId],_letterPosition[_selectBoxNum]);
			AddGraphic(_checkGraphicPass[2],_checkPosition);
				}
			}
			if(Input2.GamePad0.Right.Press)
			{
				if(_selectBoxNum < 2)
				{
					_selectBoxNum++;
					RemoveChild(Children.Last(),true);
					RemoveChild(Children.Last(),true);
					RemoveChild(Children.Last(),true);
					AddGraphic(_cursolGraphicPass,_cursolPosition[_selectBoxNum]);
					AddGraphic(_letterGraphicPass[_haveLetterId],_letterPosition[_selectBoxNum]);
			AddGraphic(_checkGraphicPass[2],_checkPosition);
				}
			}
		}
		
		public override string NextSceneName ()
		{
			return _nextScene;
		}
		
		public override void Update ()
		{
			CheckTime();
			
			ButtonContorol();	
		}
		
		public override void Render ()
		{
			base.Render ();
		}
	}
}

