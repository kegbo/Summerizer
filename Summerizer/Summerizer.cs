using System;
using System.Text;
using System.Collections.Generic;

namespace Summerizer
{
	class Summerizer
	{

		static void Main()
		{
			UserPrompt prompt = new UserPrompt();
			while (true)
			{				
				TextProcessor processor = new TextProcessor(prompt.GetFile(), prompt.GetSummerizationFactor());
				processor.Summary();
				string stat = prompt.Restart();
				if (stat == "E")
				{
					break;
				}
			}
			return;
		}

	}
}
