using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Summerizer
{
	class TextProcessor
	{
		private string stop = "stop_word.txt";
		private List<string> stopWords;
		private string fileContent;
		private int outputWordCount;
		private int inputWordCount;
		private int inputSentenceCount;
		private int outputSentenceCount;
		private int sumFactor;

		//Constructoor
		public TextProcessor(string content,int sumFactor)
		{
			this.fileContent = content;
			this.stopWords = LoadStopWord();
			this.outputWordCount = WordCount(content,sumFactor);
			this.inputWordCount = WordCount(content);
			this.sumFactor = sumFactor;
			Console.WriteLine("Loading Summeriza....");
		}
		//Dictionary containing words and number of occurences
		private Dictionary<string, int> SplitIntoWords(string[] text)
		{
			Console.WriteLine("Extracting Words....");
			Dictionary<string, int> words = new Dictionary<string, int>();
			foreach (string txt in text)
			{
				var sb = new StringBuilder();
				//split into characters to get rid of punctutation
				foreach (char c in txt)
				{
					if (!char.IsPunctuation(c))
						sb.Append(c);
				}
				//Nu word without punctuation
				string nuTxt = sb.ToString().ToLower();

				//add words into the dictionary
				if (!CheckIfStopWord(nuTxt,stopWords)) {

					if (words.ContainsKey(nuTxt))
					{
						words[nuTxt] += 1;
					}
					else
					{
						words.Add(nuTxt, 1);
					}
				}

			}
			return words;
		}

		//List of sentences
		private List<string> SplitIntoSentences(string[] text)
		{
			Console.WriteLine("Constructing Sentences....");
			List<string> sentences = new List<string>();
			foreach (string txt in text)
			{
				sentences.Add(txt);
			}
			return sentences;
		}
		//Load stop words
		private List<string> LoadStopWord()
		{
			string content = FileReader(stop);
			char delimiter = '\n';
			string[] words = Spliter(delimiter, content);
			return SplitIntoSentences(words);
		}
		//Check if word is a stop word
		private bool CheckIfStopWord(string word,List<string> stopwords)
		{
			int index = stopwords.BinarySearch(word);
			bool stoper = false;
			if (index > 0)
			{
				stoper = true;
			}
			return stoper;
		}
		//Readcontent of file
		private string FileReader(string file)
		{
			Console.WriteLine("Reading File....");
			string content =  System.IO.File.ReadAllText(@file);
			return content;
		}
		//write summary to file summary to file
		private string OutputSummary(string[] summary,string fileName)
		{
			Console.WriteLine("Writing Summary....");
			System.IO.File.WriteAllLines(@fileName, summary);
			return fileName;
		}

		//Takes string input and splits it by provided delimiter
		private string[] Spliter(char a,string file)
		{

			string[] words = file.Split(a);
			return words;
		}
		//Order dictionary by value
		private Dictionary<string, int> SortDictionary(Dictionary<string,int> dict)
		{
			Dictionary<string, int> sortDict = new Dictionary<string, int>();
			//Linq query statement
			var items = from pair in dict
				orderby pair.Value descending
						select pair;
			foreach (KeyValuePair<string, int> pair in items)
			{
				sortDict.Add(pair.Key,pair.Value);
			}

			return sortDict;
		}

		private Dictionary<int, int> SortDictionary(Dictionary<int, int> dict)
		{
			Dictionary<int, int> sortDict = new Dictionary<int, int>();
			//Linq query statement
			var items = from pair in dict
						orderby pair.Value descending
						select pair;
			foreach (KeyValuePair<int, int> pair in items)
			{
				sortDict.Add(pair.Key, pair.Value);
			}

			return sortDict;
		}

		//Number of wordsin output based off summarization factor
		private int WordCount(string content, int sumFactor)
		{
			string[] words = Spliter(' ', content);
			int inputCount = words.Length;
			int outputCount = (sumFactor * inputCount) / 100 ;
			return outputCount;
		}

		//Number of words in a strting
		private int WordCount(string content)
		{
			string[] words = Spliter(' ', content);
			int count = words.Length;
			return count;
		}
		//receive output file name
		private string OutputFileName()
		{
			char[] invalidChracters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '{', '}',
										'[', ']', ':',';', '"', '|', ',', '.', '?', '/', '`', '£'};
			string fileName;
			Console.WriteLine("Please What will you like to call the output file");
			Console.WriteLine("** ALL FILES ARE SAVED IN TXT FORMAT **");
			Console.WriteLine("");
			while (true)
			{
				fileName = Console.ReadLine();
				if (fileName.IndexOfAny(invalidChracters) == -1)
				{
					break;
				}
				else
				{
					Console.WriteLine("Plsease enter a filename without special charactes");
				}
				Console.WriteLine("Please Try again...");
				Console.WriteLine("");
			}

			fileName = fileName + ".txt";
			return fileName;
		}

		//Returns the location of highest value in a list
		private int HigestValueOnList(List<int> values)
		{
			int[] arrayValues = values.ToArray();

			int max = arrayValues.Max();
			var indices =
				arrayValues.Select((number, index) => number == max ? index : -1)
						.Where(index => index != -1)
							.ToArray();
			return indices[0];

		}

		private string[] GenerateSummary()
		{
			//Sorted Dictionary of words and occurnce
			Dictionary<string,int> words = SortDictionary(SplitIntoWords(Spliter(' ', fileContent)));
			//List of words
			List<string> wordsList = words.Keys.ToList();
			//List of sentences
			List<string> sentences = SplitIntoSentences(Spliter('.', fileContent));
			//output string
			List<string> outputSummary = new List<string>();
			//word count output
			int currentOutputWordCount = 0;
			//Get the sentence with the highest occurence of the top most word
			Dictionary<int,int> sentWordCount = new Dictionary<int,int>();
			//loop till desired word count is reached
			while (currentOutputWordCount < outputWordCount)
			{
				int num = 0;
				foreach (string sentence in sentences)
				{
					num = num + 1;
					string[] sentenceWords = Spliter(' ', sentence);

					//populate new list with number of occurence in sentence
					if (sentenceWords.Contains(wordsList[0]))
					{
						var groups = sentenceWords.GroupBy(item => item);
						foreach (var group in groups)
						{
							if (group.Key == wordsList[0])
							{
								sentWordCount.Add(sentences.IndexOf(sentence),group.Count());
								break;
							}
						}
					}
				}

				string temp;
				foreach (KeyValuePair<int, int> entry in SortDictionary(sentWordCount))
				{
					temp = sentences[entry.Key];
					if (currentOutputWordCount < outputWordCount)
					{
						//append highest occuring sentence to output list
						outputSummary.Add(temp);
						currentOutputWordCount = currentOutputWordCount + WordCount(temp);
					}
					else
					{
						break;
					}
					sentences[sentences.IndexOf(temp)] = "remove";
					//sentences.Insert(sentences.IndexOf(temp), "remove");
				}

				sentences.RemoveAll(items => items == "remove");
				wordsList.RemoveAt(0);
				//remove sentence from the list
				sentWordCount.Clear();
				Console.WriteLine("Prossesing....");

				//if the word count of the sentence is less than the output word count
			}
			inputSentenceCount = sentences.Count();
			outputSentenceCount = outputSummary.Count();
			return outputSummary.ToArray();
		}
		//SUmmary of summerization
		public void Summary()
		{
			string fileName = OutputSummary(GenerateSummary(), OutputFileName());
			Console.WriteLine("Complete..");
			Console.WriteLine("...SUMMARY...");
			Console.WriteLine("Input word count");
			Console.WriteLine(inputWordCount);
			Console.WriteLine("Summarization Factor :");
			Console.WriteLine(sumFactor);
			Console.WriteLine("Output word count:");
			Console.WriteLine(outputWordCount);
			Console.WriteLine("Input sentence count");
			Console.WriteLine(inputSentenceCount);
			Console.WriteLine("Output sentence count");
			Console.WriteLine(outputSentenceCount);
			Console.WriteLine("Filename :");
			Console.WriteLine(fileName);
			Console.WriteLine("Location of output file :");
			Console.WriteLine(Directory.GetCurrentDirectory());
		}
	}
}
