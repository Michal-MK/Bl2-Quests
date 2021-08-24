using System;
using System.Threading;

namespace Bl2Server {
	public class Program {

		private static Borderlands instance;
		internal const string PATH = "QuestIndexBL2.txt";

		static void Main(string[] args) {
			Run();

			while (true) {
				string s = Console.ReadLine();
				switch (s) {
					case "restart": {
						instance.Restart();
						break;
					}
				}
			}
		}

		private static void Run() {
			new Thread(() => {
				instance = new Borderlands();
			}).Start();
		}
	}
}
