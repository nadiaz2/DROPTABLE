
enum GameState {
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
class GameManager {
	public static GameState state = GameState.GameStart;
}