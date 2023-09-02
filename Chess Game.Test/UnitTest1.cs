namespace Chess_Game.Test;
using ChessGame;

public class Tests
{
	GameRunner gameRunner;
	[SetUp]
	public void Setup()
	{
		gameRunner = new GameRunner();
	}

	[Test]
	public void TestBoardSize()
	{
		int boardSize = 8;
		var result = gameRunner.SetBoardBoundary(boardSize);
		Assert.Pass();
		Assert.IsTrue(result);
	}
	
	[Test]
	public void TestAddPlayer()
	{
		IPlayer player1 = new HumanPlayer();
		player1.SetName("Test Unit 1");
		player1.SetUID(1);

		IPlayer player2 = new HumanPlayer();
		player2.SetName("Test Unit 2");
		player2.SetUID(2);
		
		var result1 = gameRunner.AddPlayer(player1);
		var result1Duplicate = gameRunner.AddPlayer(player1);
		var result2 = gameRunner.AddPlayer(player2);
		
		Assert.IsTrue(result1);
		Assert.IsFalse(result1Duplicate);
		Assert.IsTrue(result2);
	}
}