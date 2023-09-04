using NLog;
namespace ChessGame;

public class GameLogger
{
	private static Logger _logger = LogManager.GetCurrentClassLogger();
	
	public GameLogger()
	{
		var fileDirectory = Directory.GetCurrentDirectory();
		var loggerDirectory = Path.Combine(fileDirectory, ".//Log//log.config");
		LogManager.LoadConfiguration(loggerDirectory);
	}
	
	public Logger GetLogger()
	{
		return _logger;
	}
}