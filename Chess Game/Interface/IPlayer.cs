namespace ChessGame;
public interface IPlayer
{
	int GetUID();
	string GetName();
	bool SetUID(int uid);
	bool SetName(string name);
}