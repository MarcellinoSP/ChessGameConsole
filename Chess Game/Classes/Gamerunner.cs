using System.Runtime.Serialization.Json;
using System.Text;

namespace ChessGame;

public delegate void SwitchPlayer();

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _chessMove;
	private PlayerColor _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList;
	private Dictionary<IPlayer, List<Piece>> _piecesList;
	private GameStatus _gameStatus;
	private SwitchPlayer _switchPlayer;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
		_playerList = new Dictionary<IPlayer, PlayerColor>();
		_piecesList = new Dictionary<IPlayer, List<Piece>>();
		_chessMove = new ChessMove();
		_currentTurn = PlayerColor.WHITE;
		_switchPlayer += SwitchTurn;
	}
	
	public List<Position> GetPieceAvailableMove(Piece piece)
	{
		IMoveSet moveSet = _chessMove.GetMoveSet(piece);
		List<Position> pieceMovement = moveSet.movement(piece);
		return pieceMovement;
	}
	
	public bool? AddPlayer(IPlayer player)
	{
		PlayerColor playerColor = new();
		if(_playerList.Count == 0)
		{
			playerColor = PlayerColor.WHITE;
		}
		else
		{
			playerColor = PlayerColor.BLACK;
		}
		if(!_playerList.ContainsKey(player))
		{
			_playerList.Add(player, playerColor);
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public Dictionary<IPlayer, PlayerColor> GetPlayerList()
	{
		return _playerList;
	}
	
	public bool SetBoardBoundary(int size)
	{
		bool condition = _chessBoard.SetBoardSize(size);
		if(size  <= 0)
		{
			return false;
		}
		return condition;
	}
	
	public int GetBoardBoundary()
	{
		int boundary = _chessBoard.GetBoardSize();
		return boundary;
	}
	
	public Dictionary<IPlayer, List<Piece>> GetPlayerPieces()
	{
		return _piecesList;
	}
	
	public bool InitializePieces()
	{
		int i = 0;
		foreach(var player in _playerList.Keys)
		{
			List <Piece> pieces = new List<Piece>();
			if(i == 0)
			{
				var whitePiece = new DataContractJsonSerializer(typeof(List <Piece>));
				string path = Directory.GetCurrentDirectory();
				string fullPath = Path.Combine(path, "WhitePiece.json");
				using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
				{
					pieces = (List <Piece>)whitePiece.ReadObject(stream);
				}
				i++;
			}
			else
			{
				var blackPiece = new DataContractJsonSerializer(typeof(List <Piece>));
				string path = Directory.GetCurrentDirectory();
				string fullPath = Path.Combine(path, "BlackPiece.json");
				using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
				{
					pieces = (List <Piece>)blackPiece.ReadObject(stream);
				}
			}
			_piecesList[player] = pieces;
		}
		return true;
	}
	
	public Piece? CheckPiece(int rank, int files)
	{
		var foundPiece = _piecesList.Values
						.SelectMany(playerPieces => playerPieces)
						.FirstOrDefault(p => p.GetRank() == rank && p.GetFiles() == files);
		return foundPiece;
	}
	
	public Piece? CheckPiece(string pieceID)
	{
		var foundPiece = _piecesList.Values
						.SelectMany(playerPieces => playerPieces)
						.FirstOrDefault(piece => piece.ID().Equals(pieceID));
		return foundPiece;
	}
	
	public List <Position> FilterMove(Piece piece)
	{
		int pieceRank = piece.GetRank();
		int pieceFiles = piece.GetFiles();
		string pieceID = piece.ID();
		
		List <Position> availableMove = GetPieceAvailableMove(piece);
		List <Position> filteredMove = new List <Position>();
		
		foreach(var position in availableMove)
		{
			int checkRank = position.GetRank();
			int checkFiles = position.GetFiles();
			bool blocked = IsOccupied(pieceID, checkRank, checkFiles);
			bool kingChecked = KingCheckStatus(checkRank, checkFiles);
			
			if (pieceID.Contains('P') || pieceID.Contains('p'))
			{
				if(!IsOccupied(checkRank, checkFiles))
				{
					filteredMove.Add(new Position(checkRank, checkFiles));
				}
			}
			else if(pieceID.Contains('K') || pieceID.Contains('k'))
			{
				if(!blocked && IsPathClear(pieceID, checkRank, checkFiles, pieceRank, pieceFiles) && !kingChecked)
				{
					filteredMove.Add(new Position(checkRank, checkFiles));
				}
			}
			else if ((pieceID.Contains('N') || pieceID.Contains('n')) && !blocked)
			{
				filteredMove.Add(new Position(checkRank, checkFiles));
			}
			else if(!blocked && IsPathClear(pieceID, checkRank, checkFiles, pieceRank, pieceFiles))
			{
				filteredMove.Add(new Position(checkRank, checkFiles));
			}
		}
		return filteredMove;
	}
	
	public bool Move(string pieceID, int rank, int files)
	{
		Piece pieceToMove = CheckPiece(pieceID);
		
		List <Position> filteredMove = FilterMove(pieceToMove);
		
		if(pieceToMove is King && filteredMove.Count == 0 && KingCheckStatus())
		{
			SetGameStatus(GameStatus.STALEMATE);
			return false;
		}
		
		foreach(var pos in filteredMove)
		{
			if(pos.GetRank() == rank && pos.GetFiles() == files)
			{
				bool capture = CapturePiece(pieceID, rank, files);
				pieceToMove.SetRank(rank);
				pieceToMove.SetFiles(files);
				if(pieceToMove is Pawn pawn)
				{
					pawn.SetIsMoved(true);
				}
				_switchPlayer?.Invoke();
				return true;
			}
		}
		return false;
	}
	
	private bool IsPathClear(string pieceID, int currentRank, int currentFile, int targetRank, int targetFile)
	{	
		int rankDelta = targetRank - currentRank;
		int fileDelta = targetFile - currentFile;

		int rankIncrement = Math.Sign(rankDelta);
		int fileIncrement = Math.Sign(fileDelta);
		
		int rank = currentRank + rankIncrement;
		int file = currentFile + fileIncrement;
		
		while (rank != targetRank || file != targetFile)
		{
			if (IsOccupied(pieceID, rank, file))
			{
				return false;
			}
			rank += rankIncrement;
			file += fileIncrement;
			if (rank == targetRank && file == targetFile)
			{
				break;
			}
		}
		return true;
	}
	
	public bool IsOccupied(string pieceID, int rank, int files)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach(var piece in playerPieces)
			{
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					if(piece.ID().Any(Char.IsUpper) && pieceID.Any(Char.IsUpper) || piece.ID().Any(Char.IsLower) && pieceID.Any(Char.IsLower))
					{
						return true;
					}
					return false;
				}
			}
		}
		return false;
	}

	public bool IsOccupied(int rank, int files)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach(var piece in playerPieces)
			{
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					return true;
				}
			}
		}
		return false;
	}
	
	public bool CapturePiece(string pieceID, int rank, int files)
	{
		bool occupied = IsOccupied(rank, files);
		if (occupied)
		{
			foreach (var playerPieces in _piecesList.Values)
			{
				foreach (var piece in playerPieces)
				{
					if(piece.GetRank() == rank && piece.GetFiles() == files)
					{
						if(piece.ID().Any(Char.IsUpper) && pieceID.Any(Char.IsUpper) || piece.ID().Any(Char.IsLower) && pieceID.Any(Char.IsLower))
						{
							
						}
						else
						{
							piece.ChangeStatus();
							playerPieces.Remove(piece);
							if(piece.ID() == "K1")
							{
								SetGameStatus(GameStatus.BLACK_WIN);
							}
							else if(piece.ID() == "k1")
							{
								SetGameStatus(GameStatus.WHITE_WIN);
							}
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	
	public bool KingCheckStatus()
	{
		int boardSize = GetBoardBoundary();
		Piece king = CheckPiece(_currentTurn == PlayerColor.WHITE ? "K1" : "k1");
		int kingRank = king.GetRank();
		int kingFiles = king.GetFiles();

		foreach (var pieceList in _piecesList.Values)
		{
			foreach (var piece in pieceList)
			{
				if (piece is Rook rook && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'r' : 'R'))
				{
					if (rook.GetRank() == kingRank || rook.GetFiles() == kingFiles)
					{
						bool blocked = IsPathClear(king.ID(), rook.GetRank(), rook.GetFiles(), kingRank, kingFiles);
						if (blocked)
						{
							SetGameStatus(GameStatus.CHECK);
							return true;
						}
					}
				}
				else if (piece is Bishop bishop && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'b' : 'B'))
				{
					int bishopRank = bishop.GetRank();
					int bishopFiles = bishop.GetFiles();
					if (Math.Abs(bishopRank - kingRank) == Math.Abs(bishopFiles - kingFiles))
					{
						bool blocked = IsPathClear(king.ID(), bishopRank, bishopFiles, kingRank, kingFiles);
						if (blocked)
						{
							SetGameStatus(GameStatus.CHECK);
							return true;
						}
					}
				}
				else if (piece is Knight knight && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'n' : 'N'))
				{
					int knightRank = knight.GetRank();
					int knightFiles = knight.GetFiles();
					int rankDistance = Math.Abs(knightRank - kingRank);
					int fileDistance = Math.Abs(knightFiles - kingFiles);
					if ((rankDistance == 2 && fileDistance == 1) || (rankDistance == 1 && fileDistance == 2))
					{
						SetGameStatus(GameStatus.CHECK);
						return true;
					}
				}
				else if (piece is Queen queen && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'q' : 'Q'))
				{
					int queenRank = queen.GetRank();
					int queenFiles = queen.GetFiles();
					int rankDistance = Math.Abs(queenRank - kingRank);
					int fileDistance = Math.Abs(queenFiles - kingFiles);
					if (queenRank == kingRank || queenFiles == kingFiles || rankDistance == fileDistance)
					{
						bool blocked = IsPathClear(king.ID(), queenRank, queenFiles, kingRank, kingFiles);
						if (blocked)
						{
							SetGameStatus(GameStatus.CHECK);
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	
	public bool KingCheckStatus(int nextRank, int nextFiles)
	{
		Piece king = CheckPiece(_currentTurn == PlayerColor.WHITE ? "K1" : "k1");
		int kingRank = nextRank;
		int kingFiles = nextFiles;
		foreach (var pieceList in _piecesList.Values)
		{
			foreach (var piece in pieceList)
			{
				if (piece is Rook rook && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'r' : 'R'))
				{
					if (rook.GetRank() == kingRank || rook.GetFiles() == kingFiles)
					{
						bool blocked = IsPathClear(king.ID(), rook.GetRank(), rook.GetFiles(), kingRank, kingFiles);
						if (blocked)
						{
							return true;
						}
					}
				}
				else if (piece is Bishop bishop && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'b' : 'B'))
				{
					int bishopRank = bishop.GetRank();
					int bishopFiles = bishop.GetFiles();
					if (Math.Abs(bishopRank - kingRank) == Math.Abs(bishopFiles - kingFiles))
					{
						bool blocked = IsPathClear(king.ID(), bishopRank, bishopFiles, kingRank, kingFiles);
						if (blocked)
						{
							return true;
						}
					}
				}
				else if (piece is Knight knight && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'n' : 'N'))
				{
					int knightRank = knight.GetRank();
					int knightFiles = knight.GetFiles();
					int rankDistance = Math.Abs(knightRank - kingRank);
					int fileDistance = Math.Abs(knightFiles - kingFiles);
					if ((rankDistance == 2 && fileDistance == 1) || (rankDistance == 1 && fileDistance == 2))
					{
						return true;
					}
				}
				else if (piece is Queen queen && piece.ID().Contains(_currentTurn == PlayerColor.WHITE ? 'q' : 'Q'))
				{
					int queenRank = queen.GetRank();
					int queenFiles = queen.GetFiles();
					int rankDistance = Math.Abs(queenRank - kingRank);
					int fileDistance = Math.Abs(queenFiles - kingFiles);
					if (queenRank == kingRank || queenFiles == kingFiles || rankDistance == fileDistance)
					{
						bool blocked = IsPathClear(king.ID(), queenRank, queenFiles, kingRank, kingFiles);
						if (blocked)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	
	public bool PawnPromotion(Piece piece, PromoteTo promoteTo)
	{
		return true;
	}
	
	public bool SetGameStatus(GameStatus status)
	{
		_gameStatus = status;
		return true;
	}
	
	public GameStatus CheckGameStatus()
	{
		return _gameStatus;
	}

	public void SwitchTurn()
	{
		_currentTurn = (_currentTurn == PlayerColor.WHITE) ? PlayerColor.BLACK : PlayerColor.WHITE;
	}

	public IPlayer? GetCurrentTurn()
	{
		foreach(KeyValuePair<IPlayer, PlayerColor> playerTurn in _playerList)
		{
			if(playerTurn.Value == _currentTurn)
			{
				return playerTurn.Key;
			}
		}
		return null;
	}
	
}