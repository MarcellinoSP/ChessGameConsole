using System.Runtime.Serialization.Json;
using System.Text;

namespace ChessGame;

public class PieceList
{
	public static readonly DataContractJsonSerializerSettings settings = 
					new DataContractJsonSerializerSettings
					{ UseSimpleDictionaryFormat = true };
					
	public List<Piece> pieceListWhite = new List<Piece>();
	public List<Piece> pieceListBlack = new List<Piece>();
	
	public void AddWhitePiece()
	{
		pieceListWhite.Add(new Pawn (6, 0, "Pawn", "P1"));
		pieceListWhite.Add(new Pawn (6, 1, "Pawn", "P2"));
		pieceListWhite.Add(new Pawn (6, 2, "Pawn", "P3"));
		pieceListWhite.Add(new Pawn (6, 3, "Pawn", "P4"));
		pieceListWhite.Add(new Pawn (6, 4, "Pawn", "P5"));
		pieceListWhite.Add(new Pawn (6, 5, "Pawn", "P6"));
		pieceListWhite.Add(new Pawn (6, 6, "Pawn", "P7"));
		pieceListWhite.Add(new Pawn (6, 7, "Pawn", "P8"));
		
		pieceListWhite.Add(new Rook (7, 0, "Rook", "R1"));
		pieceListWhite.Add(new Knight (7, 1, "Knight", "N1"));
		pieceListWhite.Add(new Bishop (7, 2, "Bishop", "B1"));
		pieceListWhite.Add(new Queen (7, 3, "Queen", "Q1"));
		pieceListWhite.Add(new King (7, 4, "King", "K1"));
		pieceListWhite.Add(new Bishop (7, 5, "Bishop", "B2"));
		pieceListWhite.Add(new Knight (7, 6, "Knight", "N2"));
		pieceListWhite.Add(new Rook (7, 7, "Rook", "R2"));
	}	
	
	public void AddBlackPiece()
	{
		pieceListBlack.Add(new Pawn (1, 0, "Pawn", "p1"));
		pieceListBlack.Add(new Pawn (1, 1, "Pawn", "p2"));
		pieceListBlack.Add(new Pawn (1, 2, "Pawn", "p3"));
		pieceListBlack.Add(new Pawn (1, 3, "Pawn", "p4"));
		pieceListBlack.Add(new Pawn (1, 4, "Pawn", "p5"));
		pieceListBlack.Add(new Pawn (1, 5, "Pawn", "p6"));
		pieceListBlack.Add(new Pawn (1, 6, "Pawn", "p7"));
		pieceListBlack.Add(new Pawn (1, 7, "Pawn", "p8"));
				
		pieceListBlack.Add(new Rook (0, 0, "Rook", "r1"));
		pieceListBlack.Add(new Knight (0, 1, "Knight", "n1"));
		pieceListBlack.Add(new Bishop (0, 2, "Bishop", "b1"));
		pieceListBlack.Add(new Queen (0, 3, "Queen", "q1"));
		pieceListBlack.Add(new King (0, 4, "King", "k1"));
		pieceListBlack.Add(new Bishop (0, 5, "Bishop", "b2"));
		pieceListBlack.Add(new Knight (0, 6, "Knight", "n2"));
		pieceListBlack.Add(new Rook (0, 7, "Rook", "r2"));
	}
	
	public void GenerateJSON()
	{
		FileStream stream = new FileStream("WhitePiece.json", FileMode.Create);
		using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true, "  "))
		{
			var json1 = new DataContractJsonSerializer(typeof(List <Piece>), settings);
			json1.WriteObject(writer, pieceListWhite);
			stream.Flush();
		}
		
		FileStream stream1 = new FileStream("BlackPiece.json", FileMode.Create);
		using (var writer1 = JsonReaderWriterFactory.CreateJsonWriter(stream1, Encoding.UTF8, true, true, "  "))
		{
			var json2 = new DataContractJsonSerializer(typeof(List <Piece>), settings);
			json2.WriteObject(writer1, pieceListBlack);
			stream1.Flush();
		}
	}
}