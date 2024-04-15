using UnityEngine;

public enum GameState
{
	GameStart,
	ClassRoomStart,
	OnWayHomeStart,
	BackToClassroom,
	ReturningHomeAfterHeadphones,
	Day1LivingRoomStart,
	Day1JacobsBack,
	Day1GameEndChoice,

	// Day 2
	Day2StartTomsRoom,
	Day2HeadBackToSchool,
	Day2AfternoonClass,
	Day2HeadBackHome,

	// Day 3
	Day3StartTomsRoom,
	Day3FinishedMiniGame,
	Day3TalkedWithJacob,
	Day3InBackBay,
	Day3InClothingStore,
	Day3End,

	// Day 4
	Day4StartTomsRoom,
	Day4HeadToJacobsLab,
	Day4InLab,
	Day4FinishedMiniGame,
	Day4JacobFlashBack,
	Day4BackToPresent,
	Day4ConfrontingTom,
	Day4ThrowWaterMiniGame,
	Day4FinishedTrowWaterMiniGame,
	Day4Run,

}

public class GameManager : MonoBehaviour
{
	public static bool cameraMove = false;

	// Day 1 talking to jacob given 2 choices
	// 0 Ends the game
	// 1 Continues the game
	public static bool day1BranchEndGame = false;

	// Day 2 Tom talking in his head given 2 choices
	// 0 Continues the story with no changes
	// 1 Unlocks the Romantic Route
	public static bool day3BranchRomanticRoute = false;

	public static string lastScene = "";


	public static bool day1Started = false;
	public static bool day2Started = false;
	public static bool day3Started = false;
	public static bool day4Started = false;


	private static GameState _state = GameState.GameStart;
	public static GameState state
	{
		get { return _state; }
		set
		{
			Debug.Log($"<color=#00CC00>Game State:</color> {_state} -> {value}");
			_state = value;
		}
	}
}