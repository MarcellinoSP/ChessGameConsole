```cs
        if(_currentTurn == PlayerColor.WHITE)
		{
			Piece king = CheckPiece("K1");
			int kingRank = king.GetRank();
			int kingFiles = king.GetFiles();
			int boardSize = GetBoardBoundary();
			foreach(var pieceList in _piecesList.Values)
			{
				foreach(var piece in pieceList)
				{
					if(piece is Rook && piece.ID().Contains('r'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("K1", kingRank - i, kingFiles);
							bool threat2 = IsOccupied("K1", kingRank, kingFiles - i);
							bool threat3 = IsOccupied("K1", kingRank + i, kingFiles);
							bool threat4 = IsOccupied("K1", kingRank, kingFiles + i);
							Console.WriteLine("Pass Rook Check");
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
					if(piece is Bishop && piece.ID().Contains('b'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("K1", kingRank + i, kingFiles + i);
							bool threat2 = IsOccupied("K1", kingRank + i, kingFiles - i);
							bool threat3 = IsOccupied("K1", kingRank - i, kingFiles + i);
							bool threat4 = IsOccupied("K1", kingRank - i, kingFiles - i);
							Console.WriteLine("Pass Bishop Check");
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
				}
			}
		}
		else if(_currentTurn == PlayerColor.BLACK)
		{
			Piece king = CheckPiece("k1");
			int kingRank = king.GetRank();
			int kingFiles = king.GetFiles();
			int boardSize = GetBoardBoundary();
			foreach(var pieceList in _piecesList.Values)
			{
				foreach(var piece in pieceList)
				{
					if(piece is Rook && piece.ID().Contains('R'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("k1", kingRank - i, kingFiles);
							bool threat2 = IsOccupied("k1", kingRank, kingFiles - i);
							bool threat3 = IsOccupied("k1", kingRank + i, kingFiles);
							bool threat4 = IsOccupied("k1", kingRank, kingFiles + i);
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
					if(piece is Bishop && piece.ID().Contains('B'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("k1", kingRank + i, kingFiles + i);
							bool threat2 = IsOccupied("k1", kingRank + i, kingFiles - i);
							bool threat3 = IsOccupied("k1", kingRank - i, kingFiles + i);
							bool threat4 = IsOccupied("k1", kingRank - i, kingFiles - i);
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
				}
			}
		}

		// for(int i = 1; i < boardSize; i++)
							// {
							// 	bool blockedUp = IsOccupied("K1", kingRank - i, kingFiles);
							// 	bool blockedRight = IsOccupied("K1", kingRank, kingFiles - i);
							// 	bool blockedBottom = IsOccupied("K1", kingRank + i, kingFiles);
							// 	bool blockedLeft = IsOccupied("K1", kingRank, kingFiles + i);
							// 	if(!blockedUp || !blockedRight || !blockedBottom || !blockedLeft)
							// 	{
							// 		SetGameStatus(GameStatus.CHECK);
							// 		return true;
							// 	}
							// }

		for(int i = 1; i < boardSize; i++)
							{
								bool blockedUp = IsOccupied("K1", kingRank + i, kingFiles + i);
								bool blockedRight = IsOccupied("K1", kingRank + i, kingFiles - i);
								bool blockedBottom = IsOccupied("K1", kingRank - i, kingFiles + i);
								bool blockedLeft = IsOccupied("K1", kingRank - i, kingFiles - i);
								if(!blockedUp || !blockedRight || !blockedBottom || !blockedLeft)
								{
									SetGameStatus(GameStatus.CHECK);
									return true;
								}
							}
							
		for(int i = 1; i < boardSize; i++)
							{
								bool blockedUp = IsOccupied("K1", kingRank - i, kingFiles);
								bool blockedRight = IsOccupied("K1", kingRank, kingFiles - i);
								bool blockedBottom = IsOccupied("K1", kingRank + i, kingFiles);
								bool blockedLeft = IsOccupied("K1", kingRank, kingFiles + i);
								
								if(!blockedUp || !blockedRight || !blockedBottom || !blockedLeft)
								{
									SetGameStatus(GameStatus.CHECK);
									return true;
								}
							}

	public List <Position> GetPieceAvailableMove(string pieceID)
	{
		foreach (var playerPiece in _piecesList.Values)
		{
			foreach (var piece in playerPiece)
			{
				if(piece.ID() == pieceID)
				{
					IMoveSet moveSet = _chessMove.GetMoveSet(piece);
					List <Position> pieceMovement = moveSet.movement(piece);
					return pieceMovement;
				}
			}
		}
		return null;
	}
	
				pieces.Add(new Pawn (6, 0, "Pawn", "P1"));
				pieces.Add(new Pawn (6, 1, "Pawn", "P2"));
				pieces.Add(new Pawn (6, 2, "Pawn", "P3"));
				pieces.Add(new Pawn (6, 3, "Pawn", "P4"));
				pieces.Add(new Pawn (6, 4, "Pawn", "P5"));
				pieces.Add(new Pawn (6, 5, "Pawn", "P6"));
				pieces.Add(new Pawn (6, 6, "Pawn", "P7"));
				pieces.Add(new Pawn (6, 7, "Pawn", "P8"));
				
				pieces.Add(new Rook (7, 0, "Rook", "R1"));
				pieces.Add(new Knight (7, 1, "Knight", "N1"));
				pieces.Add(new Bishop (7, 2, "Bishop", "B1"));
				pieces.Add(new Queen (7, 3, "Queen", "Q1"));
				pieces.Add(new King (7, 4, "King", "K1"));
				pieces.Add(new Bishop (7, 5, "Bishop", "B2"));
				pieces.Add(new Knight (7, 6, "Knight", "N2"));
				pieces.Add(new Rook (7, 7, "Rook", "R2"));

				pieces.Add(new Pawn (1, 0, "Pawn", "p1"));
				pieces.Add(new Pawn (1, 1, "Pawn", "p2"));
				pieces.Add(new Pawn (1, 2, "Pawn", "p3"));
				pieces.Add(new Pawn (1, 3, "Pawn", "p4"));
				pieces.Add(new Pawn (1, 4, "Pawn", "p5"));
				pieces.Add(new Pawn (1, 5, "Pawn", "p6"));
				pieces.Add(new Pawn (1, 6, "Pawn", "p7"));
				pieces.Add(new Pawn (1, 7, "Pawn", "p8"));
				
				pieces.Add(new Rook (0, 0, "Rook", "r1"));
				pieces.Add(new Knight (0, 1, "Knight", "n1"));
				pieces.Add(new Bishop (0, 2, "Bishop", "b1"));
				pieces.Add(new Queen (0, 3, "Queen", "q1"));
				pieces.Add(new King (0, 4, "King", "k1"));
				pieces.Add(new Bishop (0, 5, "Bishop", "b2"));
				pieces.Add(new Knight (0, 6, "Knight", "n2"));
				pieces.Add(new Rook (0, 7, "Rook", "r2"));
```