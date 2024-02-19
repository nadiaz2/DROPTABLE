/*
enum GameState
{
	GameStart,
	TalkingToJacob,
	FinishedTalking,
	PlayingGame,
	GamePaused,
	ClassRoomStart,
	ClassRoomSeated,
	ClassRoomSubtitleEnd,
	ClassOver,
	OnWayHomeStart,
	BackToClassroom,
	PickedupHeadphones,
	MorganCloseUp,
	AfterHeadphones,
}
*/
public enum GameState
{
	GameStart,
	ClassRoomStart,
	OnWayHomeStart,
	BackToClassroom,
	ReturningHomeAfterHeadphones,
	Day1LivingRoomStart,
	Day1JacobsBack,

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


}

public class GameManager
{
	public static GameState state = GameState.GameStart;
}