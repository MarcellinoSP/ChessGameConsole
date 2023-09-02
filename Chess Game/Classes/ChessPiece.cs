using System.Runtime.Serialization;

namespace ChessGame;

[DataContract]
[KnownType(typeof(Pawn))]
[KnownType(typeof(Bishop))]
[KnownType(typeof(Rook))]
[KnownType(typeof(Knight))]
[KnownType(typeof(Queen))]
[KnownType(typeof(King))]
public abstract class Piece
{
	[DataMember]
	protected bool captured;
	[DataMember]
	protected Position position;
	[DataMember]
	protected string? _pieceType;
	[DataMember]
	protected string? _pieceID;
	
	public Piece()
	{
		captured = false;
		position = new Position();
		position.SetRank(0);
		position.SetFiles(0);
	}
	
	public abstract bool SetRank(int rank);
	
	public abstract bool SetFiles(int files);
	
	public abstract int GetRank();
	
	public abstract int GetFiles();
	
	public abstract string Type();
	
	public abstract string ID();
	
	public abstract void ChangeStatus();
	
	public abstract bool GetStatus();
}

[DataContract]
public class King : Piece			//DONE
{
	[DataMember]
	private bool _castlingDone;
	[DataMember]
	private bool _isMoved = false;
	
	public King()
	{
		
	}
	
	public King(int rank, int files, string type)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
	}
	
	public King(int rank, int files, string type, string id)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
		_pieceID = id;
	}
	
	public King(string type)
	{
		_pieceType = type;	
	}
	
	public override bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public override bool SetFiles(int files)
	{
		position.SetFiles(files);
		return true;
	}
	
	public override int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public override int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}
	
	public override string Type()
	{
		return _pieceType;
	}
	
	public override string ID()
	{
		return _pieceID;
	}
	
	public override void ChangeStatus()
	{
		captured = true;
	}
	
	public override bool GetStatus()
	{
		return captured;
	}
	public bool GetCastlingStatus()
	{
		return true;
	}
	
	public bool SetCastlingStatus()
	{
		return true;
	}
	
	public bool IsInCheck()
	{
		return true;
	}
	
	public bool IsMove()
	{
		if(position.GetRank() != 5 || position.GetFiles() != 1)
		{
			_isMoved = true;
		}
		return _isMoved;
	}
}

[DataContract]
public class Pawn : Piece			//DONE
{
	[DataMember]
	private bool _promotionStatus;
	[DataMember]
	private bool _enPassantStatus;
	[DataMember]
	private bool _isMoved = false;
	
	public Pawn()
	{
		
	}
	
	public Pawn(int rank, int files, string type, string id)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
		_pieceID = id;
	}
	
	public Pawn(int rank, int files, string type)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
	}
	
	public Pawn(string type)
	{
		_pieceType = type;
	}
	
	public override bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public override bool SetFiles(int files)
	{
		position.SetFiles(files);
		return true;
	}
	
	public override int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public override int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}
	
	public override string Type()
	{
		return _pieceType;
	}
	
	public override string ID()
	{
		return _pieceID;
	}
	
	public override void ChangeStatus()
	{
		captured = true;
	}
	
	public override bool GetStatus()
	{
		return captured;
	}
	
	public bool GetPromotionStatus()
	{
		return true;
	}
	
	public bool SetPromotionStatus(bool promotionStatus)
	{
		_promotionStatus = promotionStatus;
		return true;
	}
	
	public bool GetEnPassantStatus()
	{
		return true;
	}
	
	public bool SetEnPassantStatus(bool enPassantStatus)
	{
		_enPassantStatus = enPassantStatus;
		return true;
	}
	
	public bool SetIsMoved(bool setCondition)
	{
		_isMoved = setCondition;
		return _isMoved;
	}
	
	public bool IsMoved()
	{
		return _isMoved;
	}
}

[DataContract]
public class Rook : Piece			//DONE
{
	public Rook(int rank, int files, string type)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
	}
	
	public Rook(int rank, int files, string type, string id)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
		_pieceID = id;
	}
	
	public Rook(string type)
	{
		_pieceType = type;
	}
	
	public override bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public override bool SetFiles(int files)
	{
		position.SetFiles(files);
		return true;
	}
	
	public override int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public override int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}
	
	public override string Type()
	{
		return _pieceType;
	}
	
	public override string ID()
	{
		return _pieceID;
	}
	
	public override void ChangeStatus()
	{
		captured = true;
	}
	
	public override bool GetStatus()
	{
		return captured;
	}
	
	public bool IsMoved()
	{
		return true;
	}
}

[DataContract]
public class Queen : Piece			//DONE
{
	public Queen(int rank, int files, string type)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
	}
	
	public Queen(int rank, int files, string type, string id)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
		_pieceID = id;
	}
	
	public Queen(string type)
	{
		_pieceType = type;
	}
	
	public override bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public override bool SetFiles(int files)
	{
		position.SetFiles(files);
		return true;
	}
	
	public override int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public override int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}

	public override void ChangeStatus()
	{
		captured = true;
	}
	
	public override bool GetStatus()
	{
		return captured;
	}
		
	public override string Type()
	{
		return _pieceType;
	}
	
	public override string ID()
	{
		return _pieceID;
	}
}

[DataContract]
public class Knight : Piece			//DONE
{
	public Knight()
	{
		
	}
	
	public Knight(int rank, int files, string type)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
	}
	
	public Knight(int rank, int files, string type, string id)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
		_pieceID = id;
	}
	
	public Knight(string type)
	{
		_pieceType = type;
	}
	
	public override bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public override bool SetFiles(int files)
	{
		position.SetFiles(files);
		return true;
	}
	
	public override int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public override int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}
	
	public override void ChangeStatus()
	{
		captured = true;
	}
	
	public override bool GetStatus()
	{
		return captured;
	}
	
	public override string Type()
	{
		return _pieceType;
	}
	
	public override string ID()
	{
		return _pieceID;
	}
}

[DataContract]
public class Bishop : Piece			//DONE
{
	public Bishop(int rank, int files, string type)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
	}
	
	public Bishop(int rank, int files, string type, string id)
	{
		position.SetRank(rank);
		position.SetFiles(files);
		_pieceType = type;
		_pieceID = id;
	}
	
	public Bishop(string type)
	{
		_pieceType = type;
	}
	
	public override bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public override bool SetFiles(int files)
	{
		position.SetFiles(files);
		return true;
	}
	
	public override int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public override int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}
	
	public override void ChangeStatus()
	{
		captured = true;
	}
	
	public override bool GetStatus()
	{
		return captured;
	}
	
	public override string Type()
	{
		return _pieceType;
	}
	
	public override string ID()
	{
		return _pieceID;
	}
}