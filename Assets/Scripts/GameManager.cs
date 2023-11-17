
enum GameState {
	GameStart,
	TalkingToJacob,
	FinishedTalking
}
class GameManager {
	public static GameState state = GameState.GameStart;
}