namespace ChessGame;
public class ChessBoard : IBoard
{
	private int _size;
	
	public bool SetBoardSize(int size)
	{
		_size = size;
		return true;
	}
	
	public int GetBoardSize()
	{
		int boardSize = _size;
		return boardSize;
	}
}