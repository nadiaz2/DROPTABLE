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
	BackToClassroom
}

public class GameManager
{
	public static GameState state = GameState.GameStart;
}