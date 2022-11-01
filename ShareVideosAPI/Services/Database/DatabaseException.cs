using Npgsql;
using System.Net.Sockets;

namespace ShareVideosAPI.Services.Database
{
    public class DatabaseException : Exception
    {
        private readonly static string _defaultError = "Unexpected database error.";

        /// <summary>
        /// Instantiate a database exception
        /// </summary>
        /// <param name="error"> Error message </param>
        /// <param name="innerException"> Inner exception </param>
        public DatabaseException(string error, Exception? innerException) : base(error, innerException)
        {
            // NOOP
        }

        public static DatabaseException Generate(string error, Exception? innerException)
        {
            string fullError = string.Format("[DatabaseException] Exception - {0} \n Detail - {1}",
                error, innerException?.Message);
            string message;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                message = fullError;
            }
            else
            {
                message = GetSimplifiedError(innerException);
            }
            return new DatabaseException(message, innerException);
        }

        private static string GetSimplifiedError(Exception? exception)
        {
            if (exception == null)
            {
                return _defaultError;
            }
            Type exceptionType = exception.GetType();
            if (exceptionType == typeof(NpgsqlException))
            {
                if (exception.InnerException?.GetType() == typeof(SocketException))
                {
                    return "Failed to connect to database.";
                }
            }
            if (exceptionType == typeof(PostgresException))
            {
                string sqlState = ((PostgresException)exception).SqlState;
                string error;
                switch (sqlState)
                {
                    case "23503":
                        error = "Insert or update violates foreign key constraint.";
                        break;
                    case "23505":
                        error = "Duplicate key value violates unique constraint.";
                        break;
                    default:
                        error = "SQL state error.";
                        break;
                }
                return string.Format("{0}: {1}", sqlState, error);
            }
            return _defaultError;
        }
    }
}
