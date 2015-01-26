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
	public class ADVPart : BaseScene
	{
		private Novel _novel;
		private string _text;
		private string _displayName;
		private IEnumerator<object> _commandSeq;
		private List<string> _displayCharaList;
		private List<string> _displayMaterialList;
		private Bgm _bgm;
		private BgmPlayer _bgmPlayer;
		private int _playType;
		private IEnumerable<string> _selectText;
		private int _choiceSelect;
		private string _useBgm;
		private string _nextScene;
		private int _currentWordNum;
		private int _currentLineNum;
		private int _fontSize;
		
		private string[] _folderPass =　{　"/Application/date/",　};
		private string[] _nameWindowData =　{　"namewindow.png",　};
		private string[] _textWindowData =　{　"textwindow.png",　};

		private string[] _textPass =
		{
			"/Application/date/TypeAlpha/tutorialtext.txt",
			"/Application/date/TypeAlpha/text/selecttest.txt",
			"/Application/date/TypeAlpha/text/prologue.txt",
		};
		
		enum _playMode
		{
			Normal,	Skip, Auto,
		}
		
		private ImagePosition _namePosition = new ImagePosition(135,340);
		
		private static ImagePosition _messageStartPosition = new ImagePosition(210,390);	
		
		private Vector2[] _graphicPosition =
		{
			new Vector2(480,272),//背景用座標
			new Vector2(480,200),//中央
			new Vector2(260,200),//左
			new Vector2(700,200),//右
			new Vector2(480,110),//テキストウィンドウ用座標
			new Vector2(200,200),//ネームウィンドウ用座標
		};
		
		private ImagePosition[] _selectPosition =
		{
			new ImagePosition(360,110),//中央
			new ImagePosition(360,146),//中央
			new ImagePosition(360,182),//背景用座標
			new ImagePosition(360,218),//背景用座標
			new ImagePosition(360,254),//左
		};
		
		//両隣に表示させるが左右のCursorの中心点(＋－２００ずつ補正するといい感じ)
		private Vector2[] _cursolBasePosition =
		{
			new Vector2(480,416),//中央
			new Vector2(480,380),//背景用座標
			new Vector2(480,344),//背景用座標
			new Vector2(480,308),//中央
			new Vector2(480,272),//左
		};
		
		private Vector2[] _selectWindowPosition =
		{
			new Vector2(480,416),//中央
			new Vector2(480,380),//背景用座標
			new Vector2(480,344),//背景用座標
			new Vector2(480,308),//中央
			new Vector2(480,272),//左
		};
		
		private Font[] _fonts = new Font[]
		{
			new Font(FontAlias.System,25,FontStyle.Regular),
			new Font("/Application/date/GenJyuuGothic-P-Normal.ttf",35,FontStyle.Regular),
		};
		
		
		public ADVPart ()
		{			
			_novel = new Novel(_textPass[2]);
			Initialize();
			_useBgm = "";
			_bgm = null;
		}
		
		public override void Initialize ()
		{
			_displayCharaList = new List<string>();
			_commandSeq = _novel.Execute().GetEnumerator();
			_commandSeq.MoveNext();
			
			_currentWordNum = 0;
			_currentLineNum = 0;
			_fontSize = 35;
			
			_playType = 0;
			//_nowMode = (int)_modeList.Normal;
			
			AddScene();		
		}
		
		public override void AddGraphic(string dataPass , Vector2 position)
		{
			base.AddGraphic(dataPass,position);
		}
		
		
		private void AddMessage()
		{
			var posX = _currentWordNum * _fontSize + _messageStartPosition.X;
			var posY = _currentLineNum * _fontSize + _messageStartPosition.Y;
			ConvertToImage( _text[_currentWordNum].ToString() , posX , posY , _fontSize , _fontSize);
		}
		
		private void ConvertToImage(string text , int posX , int posY, int width , int height)
		{
			var position = new ImagePosition(posX,posY);
			
			Image image = new Image(ImageMode.Rgba ,new ImageSize(width,height),new ImageColor(255,0,0,0));
			//一文字ずつ追加していくロジックにする（いまは追加した結果のものを渡してる）
			image.DrawText(text,new ImageColor(0,0,0,255),_fonts[1],position);
		
			var texture = new Texture2D(width,height,false,PixelFormat.Rgba);
			
			texture.SetPixels(0,image.ToBuffer());
			
			image.Dispose();
			
			var textureInfo = new TextureInfo();
			textureInfo.Texture = texture;
			
			var sprite = new SpriteUV();
			sprite.TextureInfo = textureInfo;
			
			sprite.Quad.S = textureInfo.TextureSizef;
			sprite.CenterSprite();
			sprite.Position = Camera.CalcBounds().Center;
			
			if(_currentWordNum == 0)sprite.Name = "firstWord";
			
			AddChild(sprite);
		}	
		private void AddSelect()
		{
			var width = Director.Instance.GL.Context.GetViewport().Width;
			var height = Director.Instance.GL.Context.GetViewport().Height;
			
			if(_novel.SelectMessage().Count() == 2)
			{
				AddGraphic("/Application/date/select/select.png",_selectWindowPosition[1]);
				AddGraphic("/Application/date/select/select.png",_selectWindowPosition[3]);
				ConvertToImage(_selectText.ElementAt(0),_selectPosition[1].X , _selectPosition[1].Y , width , height);
				ConvertToImage(_selectText.ElementAt(1),_selectPosition[3].X , _selectPosition[3].Y , width , height);
			}

			if(_novel.SelectMessage().Count() == 3)
			{
				for(int i = 0 ; i < _selectText.Count();i++)
				{
					AddGraphic("/Application/date/select/select.png",_selectWindowPosition[i * 2]);
					ConvertToImage( _selectText.ElementAt(i) , _selectPosition[i * 2].X , _selectPosition[i * 2].Y , width , height);
				}
			}
		}
		
		private void AddScene()
		{
			if(_displayMaterialList[0] == "/Application/date/TypeAlpha/" || 
			   _displayMaterialList[0] == "/Application/date/TypeAlpha/.jpg" ||
			   _displayMaterialList[0] == "/Application/date/TypeAlpha/.png"
			   )AddGraphic(_displayMaterialList[0] , _graphicPosition[0]);
			
			
		}
		
		public void GoNext()
		{
			if (_commandSeq != null)
			{
				var r = _commandSeq.MoveNext();
				if (!r) _commandSeq = null;
			}
			
			if(Children.Count() > 0)RemoveScene();
			AddScene();
			if(_novel.SelectMessage() != null && _novel.IsClear() == false) 
			{
				AddSelect();
			}
		}
		
		public override void ContorolGraphic()
		{
			
		}
		
		public override void ContorolSound(string dataPass)
		{
			
		}
		
		private void ContorolText()
		{
			
		}
		
		private void ButtonContorol ()
		{
			GamePadData data = GamePad.GetData(0);
		
			if(Input2.GamePad0.Circle.Press || Input2.GamePad0.Left.Press)
			{
				if(_currentWordNum >= _text.Count())
				{
					GoNext();
				}
				else 
				{
					while(_currentWordNum < _text.Count())
					{
						AddMessage();
					}
				}			
			}
		}
		
		public override void Update ()
		{
			ButtonContorol();	
		}
	}
}

