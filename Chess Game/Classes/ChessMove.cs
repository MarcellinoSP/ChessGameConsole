namespace ChessGame;
public class ChessMove
{
	private ChessBoard chessBoard = new();
	private Dictionary <Piece, IMoveSet> _moveSet = new Dictionary<Piece, IMoveSet>();
	private int _moveBoundary;
	
	public ChessMove()
	{
		_moveSet.Add(new Pawn("Pawn"), new PawnMoveSingle());
		_moveSet.Add(new Knight("Knight"), new KnightMoveSet());
		_moveSet.Add(new Rook("Rook"), new RookMoveSet());
		_moveSet.Add(new Bishop("Bishop"), new BishopMoveSet());
		_moveSet.Add(new Queen("Queen"), new QueenMoveSet());
		_moveSet.Add(new King("King"), new KingMoveSet());
		_moveBoundary = chessBoard.GetBoardSize();
	}
	public bool AddPiece(KeyValuePair<Piece, IMoveSet> addPiece)	//KALAU LANGSUNG DI ASSIGN?
	{
		return true;
	}
	
	public bool SetMoveBoundary(IBoard board)
	{
		_moveBoundary = board.GetBoardSize();
		return true;
	}
	
	public IMoveSet GetMoveSet(Piece piece)
	{
		foreach(KeyValuePair <Piece, IMoveSet> moveCoordinateList in _moveSet)
		{
			var pieces = moveCoordinateList.Key;
			var moveSet = moveCoordinateList.Value;
			if(pieces.Type() == piece.Type())
			{
				return moveSet;
			}
		}
		return null;
	}
}

public class PawnMoveSingle : IMoveSet		//DONE
{
	public List<Position> movement(Piece piece)
	{
		List<Position> availablePosition = new List<Position>();
		int rank = piece.GetRank();
		int files = piece.GetFiles();
		
		if(piece.ID().Any(Char.IsUpper))
		{
			if(piece is Pawn pawn)
			{
				if(pawn.IsMoved() == false)
				{
					availablePosition.Add(new Position(rank - 1, files));
					availablePosition.Add(new Position(rank - 2, files));
				}
				else
				{
					availablePosition.Add(new Position(rank - 1, files));
				}
			}
		}
		else
		{
			if(piece is Pawn pawn)
			{
				if(pawn.IsMoved() == false)
				{
					availablePosition.Add(new Position(rank + 1, files));
					availablePosition.Add(new Position(rank + 2, files));
				}
				else
				{
					availablePosition.Add(new Position(rank + 1, files));
				}
			}
		}

		//Filter unnecessary position
		availablePosition.RemoveAll(move => move.GetRank() < 0 || move.GetFiles() > 7 || move.GetRank() > 7 || move.GetFiles() < 0);
		return availablePosition;
	}
}

// public class PawnEnPassant : IMoveSet
// {
// 	public Position pieceMove()
// 	{
// 		Position position = new();
// 		int currentRank = position.GetRank();
// 		int currentFiles = position.GetFiles();
// 		currentRank += 1;
// 		currentFiles += 1;
// 		position.SetRank(currentRank);
// 		position.SetFiles(currentFiles);
// 		return position;
// 	}
// }

public class KnightMoveSet : IMoveSet		//DONE
{
	public List<Position> movement(Piece piece)
	{
		List<Position> availablePosition = new List<Position>();
		int rank = piece.GetRank();
		int files = piece.GetFiles();
		
		availablePosition.Add(new Position (rank + 1, files + 2));
		availablePosition.Add(new Position (rank - 1, files - 2));
		availablePosition.Add(new Position (rank + 1, files - 2));
		availablePosition.Add(new Position (rank - 1, files + 2));
		availablePosition.Add(new Position (rank + 2, files + 1));
		availablePosition.Add(new Position (rank - 2, files + 1));
		availablePosition.Add(new Position (rank + 2, files - 1));
		availablePosition.Add(new Position (rank - 2, files - 1));
		
		//Filter unnecessary position
		availablePosition.RemoveAll(move => move.GetRank() < 0 || move.GetFiles() > 7 || move.GetRank() > 7 || move.GetFiles() < 0);
		return availablePosition;
	}
}

public class RookMoveSet : IMoveSet
{
	public List<Position> movement(Piece piece)
	{
		List<Position> availablePosition = new List<Position>();
		int rank = piece.GetRank();
		int files = piece.GetFiles();
		
		for(int i = 1; i <= 7; i ++)
		{
			availablePosition.Add(new Position(rank + i, files));
			availablePosition.Add(new Position(rank - i, files));
			availablePosition.Add(new Position(rank, files + i));
			availablePosition.Add(new Position(rank, files - i));
		}
		
		//Filter unnecessary position
		availablePosition.RemoveAll(move => move.GetRank() < 0 || move.GetFiles() > 7 || move.GetRank() > 7 || move.GetFiles() < 0);
		return availablePosition;
	}
}

public class BishopMoveSet : IMoveSet		//DONE
{
	public List<Position> movement(Piece piece)
	{
		List<Position> availablePosition = new List<Position>();
		int rank = piece.GetRank();
		int files = piece.GetFiles();
		
		for(int i = 1; i <= 7; i++)
		{
			availablePosition.Add(new Position(rank + i, files + i));
			availablePosition.Add(new Position(rank - i, files - i));
			availablePosition.Add(new Position(rank + i, files - i));
			availablePosition.Add(new Position(rank - i, files + i));
		}
		
		//Filter unnecessary position
		availablePosition.RemoveAll(move => move.GetRank() < 0 || move.GetFiles() > 7 || move.GetRank() > 7 || move.GetFiles() < 0);
		return availablePosition;
	}
}

public class QueenMoveSet : IMoveSet		//DONE
{
	public List<Position> movement(Piece piece)
	{
		List<Position> availablePosition = new List<Position>();
		int rank = piece.GetRank();
		int files = piece.GetFiles();
		
		for(int i = 1; i <= 7; i++)
		{
			availablePosition.Add(new Position(rank + i, files + i));
			availablePosition.Add(new Position(rank - i, files - i));
			availablePosition.Add(new Position(rank + i, files - i));
			availablePosition.Add(new Position(rank - i, files + i));
			availablePosition.Add(new Position(rank + i, files));
			availablePosition.Add(new Position(rank - i, files));
			availablePosition.Add(new Position(rank, files + i));
			availablePosition.Add(new Position(rank, files - i));
		}

		//Filter unnecessary position
		availablePosition.RemoveAll(move => move.GetRank() < 0 || move.GetFiles() > 7 || move.GetRank() > 7 || move.GetFiles() < 0);
		return availablePosition;
	}
}

public class KingMoveSet : IMoveSet			//DONE
{
	public List<Position> movement(Piece piece)
	{
		List<Position> availablePosition = new List<Position>();
		int rank = piece.GetRank();
		int files = piece.GetFiles();
		
		availablePosition.Add(new Position(rank + 1, files + 1));
		availablePosition.Add(new Position(rank - 1, files - 1));
		availablePosition.Add(new Position(rank + 1, files - 1));
		availablePosition.Add(new Position(rank - 1, files + 1));
		availablePosition.Add(new Position(rank + 1, files));
		availablePosition.Add(new Position(rank - 1, files));
		availablePosition.Add(new Position(rank, files + 1));
		availablePosition.Add(new Position(rank, files - 1));

		//Filter unnecessary position
		availablePosition.RemoveAll(move => move.GetRank() < 0 || move.GetFiles() > 7 || move.GetRank() > 7 || move.GetFiles() < 0);
		return availablePosition;
	}
}

// public class KingCastling : IMoveSet
// {
// 	public Position pieceMove()
// 	{
// 		Position position = new();
// 		int currentRank = position.GetRank();
// 		int currentFiles = position.GetFiles();
// 		currentFiles += 2;
// 		currentFiles -= 2;
// 		position.SetFiles(currentFiles);
// 		position.SetFiles(currentFiles);
// 		return position;
// 	}
// }