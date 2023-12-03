
enum GameState {
	GameStart,
	TalkingToJacob,
	FinishedTalking,
	PlayingGame,
	GamePaused,
}
class GameManager {
	public static GameState state = GameState.GameStart;
}