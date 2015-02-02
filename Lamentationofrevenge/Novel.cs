using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.HighLevel.UI;

namespace Lamentationofrevenge
{
	public class Novel
	{
		private IEnumerable<NovelCommand> _commands;
		private string _backGroundGraphicPass;
		private string _backGroundMusicPass;
		private string _soundEffectPass;
		private string _charaGraphicPass;
		private IEnumerable<string> _selectMessages;
		private string _messages;
		private string _name;
		private string _miniGameTitle;
		private bool _isClear;
		private string _nextText;
		private int _selectCommandId;
		
		string[] dateType = 
		{
			".jpg",
			".png",
			".mp3",
			".avi",
			".debug",
		};
		
		public void Initialize(string datePass)
		{
			var data = File.ReadAllText(datePass);
			_commands = CommandPerser.Perse(data);
			_isClear = false;
		}
		
		public Novel(string datePass)
		{
			Initialize(datePass);
		}
		
		public string BackGroundGraphicPass(){ return _backGroundGraphicPass; }
		public string CharaGraphicPass(){ return _charaGraphicPass; }
 		public int SelectMessageIndex(){ return _selectMessages.Count(); }
 		public int SelectID(){ return _selectCommandId; }
		public IEnumerable<string> SelectMessage(){ return _selectMessages; }
		public string Message(){ return _messages; }
		public string Name(){ return _name; }
		public bool IsClear(){ return _isClear; }
		public string BackGroundMusicPass() { return _backGroundMusicPass; }
		public string SoundEffectPass(){ return _soundEffectPass; }
		public void TextNamePass() { Initialize( "/Application/data/text/" + _nextText + ".txt"); }
		public string MiniGameTitle(){ return _miniGameTitle; }
		
		public IEnumerable<object> Execute()
		{
			foreach(var c in _commands)
			{
				if(_isClear == true)_isClear = false;
				CommandExecute(c);
				yield return null;
			}
		}
			
		private void CommandExecute(NovelCommand novelCommand)
		{
			{
				var nc = novelCommand as MessageCommand;
				if (nc !=　null) { DisplayMessage(nc); } 
			}
			{
				var nc = novelCommand as SelectCommand;
				if (nc !=　null)
				{
					DisplaySelect(nc); DisplaySelectId(nc);
				} 
			}
			{
				var nc = novelCommand as BackgroundCommand;
				if (nc !=　null) { ChangeBackground(nc); } 
			}
			{
				var nc = novelCommand as BackSoundCommand;
				if (nc !=　null) { ChangeBackSound(nc); } 
			}
			{
				var nc = novelCommand as SoundEffectCommand;
				if (nc !=　null) { PlaySoundEffect(nc); } 
			}
			{
				var nc = novelCommand as MiniGameCommand;
				if (nc !=　null) { ExecuteMiniGame(nc); } 
			}
			{
				var nc = novelCommand as ClearCommand;
				if (nc !=　null) { ExecuteClearCommand(nc); } 
			}
			{
				var nc = novelCommand as NextTextCommand;
				if (nc !=　null) { LoadTextCommand(nc); } 
			}
		}
		
		private void DisplayMessage (MessageCommand nc)
		{
			_charaGraphicPass = nc.Emotion + dateType[1];
			_messages = nc.Message;
			_name = nc.Name;
		}
		private void LoadTextCommand(NextTextCommand nc)
		{
			_commands = null;
			_nextText = nc.TextName;
		}
		private void DisplaySelect (SelectCommand nc){ _selectMessages = nc.Message; }
		private void DisplaySelectId(SelectCommand nc){ _selectCommandId = nc.SelectorId; }
		private void ChangeBackground (BackgroundCommand nc){ _backGroundGraphicPass = "bgg/" + nc.datePass + dateType[0]; }
		private void ChangeBackSound (BackSoundCommand nc){ _backGroundMusicPass = "bgm/" + nc.datePass + dateType[2]; }
		private void PlaySoundEffect (SoundEffectCommand nc){ _soundEffectPass = "se/" + nc.datePass + dateType[3]; }
		private void ExecuteMiniGame (MiniGameCommand nc){ _miniGameTitle = nc.GameName; }		
		private void ExecuteClearCommand(ClearCommand nc){ _isClear = true; }
	}

	abstract public class NovelCommand{}
	
	class MessageCommand : NovelCommand
	{
		public string Name { get; private set; }
		public string Message { get; private set; }
		public string Emotion { get; private set; }
		public MessageCommand(string name , string message , string emotion) 
		{
			Name = name; Message = message; Emotion = emotion;
		}
	}
	
	class SelectCommand : NovelCommand
	{
		public IEnumerable<string> Message { get; private set; }
		public string CommandName{get;private set;}
		public int SelectorId { get; private set; }
		public SelectCommand(IEnumerable<string> message ,int selectorId) 
		{ 
			SelectorId = selectorId;Message = message;
		}
	}
	
	class BackgroundCommand : NovelCommand
	{
		public string datePass { get; private set; }
		public BackgroundCommand(string _datePass) { datePass = _datePass; }
	}
	
	class BackSoundCommand : NovelCommand
	{
		public string datePass { get; private set; }
		public BackSoundCommand(string _datePass) { datePass = _datePass; }
	}
	
	class SoundEffectCommand : NovelCommand
	{
		public string datePass { get; private set; }	
		public SoundEffectCommand(string _datePass) { datePass = _datePass; }
	}
	
	class MiniGameCommand : NovelCommand
	{
		public string GameName { get; private set; }
		public MiniGameCommand(string gameName) {  GameName = gameName; }
	}

	class ClearCommand : NovelCommand
	{
		public string Command{ get; private set;}
		public ClearCommand(string command) { Command = command; }
	}
	class NextTextCommand: NovelCommand
	{
		public string Command{ get; private set;}
		public string TextName{ get; private set;}
		public NextTextCommand(string command ,string textName) { Command = command; TextName = textName;}
	}
}	