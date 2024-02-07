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
	LivingRoomStart,
	Day2StartTomsRoom,
	Day2HeadBackToSchool,
	Day2AfternoonClass,
	Day2HeadBackHome,

}

public class GameManager
{
	public static GameState state = GameState.GameStart;
}