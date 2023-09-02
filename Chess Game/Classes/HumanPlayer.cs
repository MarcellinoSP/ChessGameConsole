namespace ChessGame;

public class HumanPlayer : IPlayer
{
	private string? _playerName;
	private int _playerID;
	
	public bool SetName(string name)
	{
		_playerName = name;
		return true;
	}
	
	public bool SetUID(int uid)
	{
		_playerID = uid;
		return true;
	}
	
	public string GetName()
	{
		string name = _playerName;
		return name;
	}
	
	public int GetUID()
	{
		int uid = _playerID;
		return uid;
	}
}