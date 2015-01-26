using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamentationofrevenge
{
	public class CommandPerser
	{
		public static IEnumerable<NovelCommand> Perse(string data)
		{
			List<NovelCommand> commandlist = new List<NovelCommand>();

			var commandTexts = SplitCommand(data);
			
			foreach(string s in commandTexts)
			{
				var list = PerseCommand(s);
				commandlist.Add(list);
			}
			
			return commandlist;
		}

		public static IEnumerable<string> SplitCommand(string data)
		{
			return data.Split('\n').Select(s => s.Trim());
		}

		public static NovelCommand PerseCommand(string data)
		{
			var commaSplitText = data.Split(',');
			
			var commandSplitText = commaSplitText[0].Split('.');
			var messageSplitText = commaSplitText[ commaSplitText.Length - 1 ].Split('.');
			
			//TODO: splitedData を解析して NovelCommand オブジェクトを生成する
			NovelCommand nc = null;

			if(commandSplitText[0] == "アルフレッド" || commandSplitText[0] == "ヒューバート" || commandSplitText[0] == "ルイス" ||
			   commandSplitText[0] == "マチルダ" || commandSplitText[0] == "エイブラム" || commandSplitText[0] == "アルフレッド" ||
			   commandSplitText[0] == "アルフレッド")
			{
				nc = new MessageCommand(commandSplitText[0], messageSplitText[0] , commandSplitText[1]);
			}
			
			if(commaSplitText[0] == "父" || commaSplitText[0] == "主人公" || commaSplitText[0] == "兄" ||
			    commaSplitText[0] == "フィオナ" )
			{
				nc = new MessageCommand(commandSplitText[0], messageSplitText[0] , "");
			}
			
			if(commandSplitText[0] == "ナレーション" || commandSplitText[0] == "ナレ")
			{
				nc = new MessageCommand("", messageSplitText[0] , "");
			}
			
			if(commandSplitText[0] == "背景")
			{
				nc = new BackgroundCommand(messageSplitText[0]);
			}
			
			if(commandSplitText[0] == "選択肢")
			{
				var messageList = messageSplitText;
				var selectorid = Int32.Parse(commandSplitText[1]);
				nc = new SelectCommand(messageList , selectorid);
			}
			
			if(commandSplitText[0] == "BGM")
			{
				nc = new BackSoundCommand(messageSplitText[0]);
			}
			
			if(commandSplitText[0] == "SE")
			{
				nc = new SoundEffectCommand(messageSplitText[0]);
			}
			
			if(commandSplitText[0] == "ミニゲーム")
			{
				nc = new MiniGameCommand(messageSplitText[0]);
			}
			
			if(commandSplitText[0] == "クリア")
			{
				nc = new ClearCommand(messageSplitText[0]);
			}
			
			return nc;
		}
	}
}

