using System;
using System.IO;
using System.Security;

namespace Summerizer
{
	//Interacts with the user to accepts user input
	public class UserPrompt
	{
		public UserPrompt()
		{
			Banner();
		}

		public void Banner()
		{
			Console.WriteLine("=====================================================================================================================================");
			Console.WriteLine("==888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888==");
			Console.WriteLine("==8'''''''''''888''88888888''88''''888888888''''88''''88888''''88''''''''''''88''''''''''88''''''''''''888''''''''''88888''''''8888==");
			Console.WriteLine("==8''888888888888''88888888''88''88''88888''88''88''8''888''8''88''888888888888''888888''8888888''88888888888888'''88888''88888''88==");
			Console.WriteLine("==8''888888888888''88888888''88''888''888''888''88''88''8''88''88''888888888888''888888''8888888''88888888888888''888888''88888''88==");
			Console.WriteLine("==88'''''''''8888''88888888''88''888888''88888''88''8888''888''88'''''''''''888''8''''''88888888''888888888888'''8888888'''''''''88==");
			Console.WriteLine("==88888888888''88''88888888''88''8888888888888''88''888888888''88''888888888888''888888''8888888''8888888888'''888888888''88888''88==");
			Console.WriteLine("==88888888888''88''88888888''88''8888888888888''88''888888888''88''888888888888''888888''8888888''888888888'''8888888888''88888''88==");
			Console.WriteLine("==88''''''''''88888''''''''8888''8888888888888''88''888888888''88''''''''''''88''888888''88''''''''''''8888'''''''''''88''88888''88==");
			Console.WriteLine("==888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888==");
			Console.WriteLine("=====================================================================================================================================");
			Console.WriteLine(" ------------------------------------------------------AUTHOR : IBOK KEGBOKOKIM MISSANG----------------------------------------------");
			Console.WriteLine(" ------------------------------------------------------SID : 1439692-----------------------------------------------------------------");
			Console.WriteLine(" ------------------------------------------------------DESCRIPTION : Summerizes a given text-----------------------------------------");
			Console.WriteLine(" ------------------------------------------------------based on a given summerization factor-----------------------------------------");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");

		}
		//gets the name of the file from the User and returns the contents of the file
		public string GetFile()
		{
			string file;
			Console.WriteLine("Please Enter a the name of a txt file to enter you will like to summerizer");
			Console.WriteLine("**The longer the text the better this summeriser works**");
			Console.WriteLine("");
			while (true) 
			{ 
				string filename = Console.ReadLine();
				try
				{
					file = System.IO.File.ReadAllText(@filename);
					break;
				}
				catch (ArgumentException)
				{
					Console.WriteLine("Sorry, You entered a null value.");	
				}
				catch (FileNotFoundException)
				{
					Console.WriteLine("Sorry, The file dosent exit.");
				}
				catch (IOException)
				{
					Console.WriteLine("Sorry, An I/O error occurred while opening the file.");
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("Sorry, You do not have the required permission to access that File.");
				}
				catch (NotSupportedException)
				{
					Console.WriteLine("Sorry, path is in an invalid format.");
				}
				Console.WriteLine("Please Try again...");
				Console.WriteLine("");
			}
			return file;
		}

		//Gets the summerization factor for the user
		public int GetSummerizationFactor()
		{
			int factor;
			Console.WriteLine("Please Enter the Summerization Factor between 10 and 99");
			Console.WriteLine("**This is the percentage of text generated from inital test**");
			Console.WriteLine("");
			while(true)
			{ 
				string value = Console.ReadLine();
				bool result = Int32.TryParse(value, out factor);
				if (result)
				{
					if (factor < 99 && factor > 10)
					{
						break;
					}
					else
					{
						Console.WriteLine("Please enter a value between 10 and 99");
					}
				}
				else
				{
					Console.WriteLine("Please Enter a valid integer");
				}
				Console.WriteLine("Please Try again...");
				Console.WriteLine("");
			}
			return factor;
			
		}
		public string Restart()
		{
			Console.WriteLine("To read another file press 'C' or 'E' to exit");
			Console.WriteLine("");
			string stat;
			while (true)
			{
				stat = Console.ReadLine();
				if (stat == "C")
				{
					break;
				}
				if (stat == "E")
				{
					break;
				}
				Console.WriteLine("Please Enter either a C to continue or E to exit");
			}
			return stat;

		}
	}
}
