```cs
//FILE CADANGAN NGODING, JANGAN DIHAPUS

    //GAME RUNNER
	private List<Piece> _listOfPiece;

    public bool IsOccupied(string type, int rank, int files)
	{
		foreach(var item in _listOfPiece){
			if(rank == item.GetRank() && files == item.GetFiles())
			{
				Console.WriteLine($"Is Occupied by {item.Type()}");
				if(item.Type().Any(Char.IsUpper) && type.Any(Char.IsUpper))
				{
					Console.WriteLine("Double Uppercase Detected");
					return true;
				}
				return false;
			}
			// Console.WriteLine(item.GetRank());
		}
		return false;
	}

    public bool Move(string type, int rank, int files)
	{
		bool occupied = IsOccupied(type, rank, files);
		if(occupied)
		{
			return false;
		}
		else
		{
			Piece piece = _listOfPiece.FirstOrDefault(pieceType => pieceType.Type() == type);
			piece.SetRank(rank);
			piece.SetFiles(files);
			Console.WriteLine(piece.GetRank());
			Console.WriteLine(piece.GetFiles());
			return true;	
		}
	}

    public Piece CheckPiece(int rank, int files)
	{
		return _listOfPiece.FirstOrDefault(piece => piece.GetRank() == rank && piece.GetFiles() == files);
	}

    public bool InitializingPiece()
	{
		_listOfPiece.Add(new Pawn (6, 0, "P1"));
		_listOfPiece.Add(new Pawn (6, 1, "P2"));
		_listOfPiece.Add(new Pawn (6, 2, "P3"));
		_listOfPiece.Add(new Pawn (6, 3, "P4"));
		_listOfPiece.Add(new Pawn (6, 4, "P5"));
		_listOfPiece.Add(new Pawn (6, 5, "P6"));
		_listOfPiece.Add(new Pawn (6, 6, "P7"));
		_listOfPiece.Add(new Pawn (6, 7, "P8"));
		
		_listOfPiece.Add(new Rook (7, 0, "R1"));
		_listOfPiece.Add(new Knight (7, 1, "N1"));
		_listOfPiece.Add(new Bishop (7, 2, "B1"));
		_listOfPiece.Add(new Queen (7, 3, "Q1"));
		_listOfPiece.Add(new King (7, 4, "K1"));
		_listOfPiece.Add(new Bishop (7, 5, "B2"));
		_listOfPiece.Add(new Knight (7, 6, "N2"));
		_listOfPiece.Add(new Rook (7, 7, "R2"));

		_listOfPiece.Add(new Pawn (1, 0, "p1"));
		_listOfPiece.Add(new Pawn (1, 1, "p2"));
		_listOfPiece.Add(new Pawn (1, 2, "p3"));
		_listOfPiece.Add(new Pawn (1, 3, "p4"));
		_listOfPiece.Add(new Pawn (1, 4, "p5"));
		_listOfPiece.Add(new Pawn (1, 5, "p6"));
		_listOfPiece.Add(new Pawn (1, 6, "p7"));
		_listOfPiece.Add(new Pawn (1, 7, "p8"));
		
		_listOfPiece.Add(new Rook (0, 0, "r1"));
		_listOfPiece.Add(new Knight (0, 1, "n1"));
		_listOfPiece.Add(new Bishop (0, 2, "b1"));
		_listOfPiece.Add(new Queen (0, 3, "q1"));
		_listOfPiece.Add(new King (0, 4, "k1"));
		_listOfPiece.Add(new Bishop (0, 5, "b2"));
		_listOfPiece.Add(new Knight (0, 6, "n2"));
		_listOfPiece.Add(new Rook (0, 7, "r2"));
		
		return true;
	}

    //PROGRAM.CS
    static void PieceInit(GameRunner game)
	{
		bool check = game.InitializingPiece();
		if(check)
		{
			Console.WriteLine("Piece initializing success");
		}
		else
		{
			Console.WriteLine("Please Re-check");
		}
	}

	Console.WriteLine($"Piece to remove: {pieceList.ID()}");
					if(pieceList.ID() == "K1")
					{
						SetGameStatus(GameStatus.BLACK_WIN);
					}
					else if(pieceList.ID() == "k1")
					{
						SetGameStatus(GameStatus.WHITE_WIN);
					}
					else
					{
						SetGameStatus(GameStatus.ONGOING);
					}


	public bool Move (string pieceID, int rank, int files)		//HARUSNYA BODY METHOD DIBAWAH GET PIECE POSITION NYA
	{
		List <Position> positionAvailable = GetPieceAvailableMove(pieceID);
		List <Position> filteredMove = new List <Position>();
		foreach (var pos in positionAvailable)
		{
			int checkRank = pos.GetRank();
			int checkFiles = pos.GetFiles();
			
			bool blocked = IsOccupied(pieceID, checkRank, checkFiles);
			if (!blocked && IsPathClear(pieceID, checkRank, checkFiles, rank, files))
			{
				filteredMove.Add((new Position(checkRank, checkFiles)));
			}
		}
		foreach (var movePos in filteredMove)
		{
			Console.WriteLine($"{movePos.GetRank()}, {movePos.GetFiles()} \n");
		}
		return false;
	}

	public bool Move(string pieceID, int rank, int files)
	{
		// bool occupied = IsOccupied(pieceID, rank, files);
		// if(!occupied)
		// {
			foreach (var playerPieces in _piecesList.Values)
			{
				foreach(var piece in playerPieces)
				{
					if(piece.ID() == pieceID)
					{
						List<Position> positionAvailable = GetPieceAvailableMove(piece);
						foreach(var position in positionAvailable)
						{
							if(position.GetRank() == rank && position.GetFiles() == files)
							{
								piece.SetRank(rank);
								piece.SetFiles(files);
								return true;
							}
						}
						return false;
					}
				}
			}
		// }
		return false;
	}
	
	//Filter Movement Trial
		// List <Position> positionAvailable = GetPieceAvailableMove(pieceToMove);
		// List <Position> filteredMove = new List <Position>();
		
		// foreach(var position in positionAvailable)
		// {
		// 	int checkRank = position.GetRank();
		// 	int checkFiles = position.GetFiles();
		// 	bool blocked = IsOccupied(pieceID, checkRank, checkFiles);
			
		// 	if(pieceID.Contains('N') || pieceID.Contains('n'))
		// 	{
		// 		if(!blocked)
		// 		{
		// 			filteredMove.Add(new Position(checkRank, checkFiles));
		// 		}
		// 	}
			
		// 	if(pieceID.Contains('P'))
		// 	{
		// 		Console.WriteLine($"{pieceRank}, {pieceFiles}");
		// 		bool occupiedLeft = IsOccupied(pieceID, pieceRank - 1, pieceFiles + 1);
		// 		Console.WriteLine($"{pieceRank - 1}, {pieceFiles + 1}");
		// 		Console.WriteLine(occupiedLeft);
		// 	}
		// 	Console.WriteLine();
			
		// 	if(!blocked && IsPathClear(pieceID, checkRank, checkFiles, pieceRank, pieceFiles))
		// 	{
		// 		filteredMove.Add(new Position(checkRank, checkFiles));
		// 	}
		// }
```