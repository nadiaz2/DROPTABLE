
enum GameState {
	GameStart,
	TalkingToJacob,
	FinishedTalking,
	PlayingGame,
	GamePaused,
	ClassRoomSubtitleStart,
	ClassRoomSeated,
	ClassRoomSubtitleEnd,
	ClassOver,
}
class GameManager {
	public static GameState state = GameState.GameStart;
}