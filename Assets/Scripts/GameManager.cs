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
enum GameState
{
	GameStart,
	ClassRoomStart,
	OnWayHomeStart,
	BackToClassroom
}

class GameManager
{
	public static GameState state = GameState.GameStart;
}