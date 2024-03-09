
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


}

public class GameManager
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



    public static GameState state = GameState.GameStart;


}