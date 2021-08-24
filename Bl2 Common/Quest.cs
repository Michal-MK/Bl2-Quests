using System;

namespace Bl2Common {
	/// <summary>
	/// Structure for an in-game quest
	/// </summary>
	[Serializable]
	public class Quest {

		/// <summary>
		/// The unique ID of this quest
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The in-game name of the quest
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The location where this quest can be obtained
		/// </summary>
		public string AcceptingLocation { get; set; }

		/// <summary>
		/// The person/thing that gives you this quest
		/// </summary>
		public string GivenBy { get; set; }

		/// <summary>
		/// Integer signifying since when the quest is available, in relation to <see cref="ID"/>
		/// </summary>
		public int AvailableSince { get; set; }

		/// <summary>
		/// The location where this quest is usually needed
		/// </summary>
		public string TriggerLocation { get; set; }

		/// <summary>
		/// Is the quest available only while we do not **** up
		/// </summary>
		public bool ConditionallyAvailable { get; set; }

		/// <summary>
		/// The usual quest level based on the average run
		/// </summary>
		public int QuestLevel { get; set; }

		/// <summary>
		/// The current state of this quest
		/// </summary>
		public QuestStatus Status { get; set; }

		/// <summary>
		/// The player that usually accepts this quest
		/// </summary>
		public PlayerCharacter AcceptedBy { get; set; }

		/// <summary>
		/// The player that usually triggers the skip with this quest
		/// </summary>
		public PlayerCharacter TriggeredBy { get; set; }
	}
}
