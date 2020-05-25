using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Models;
using System.Data.SqlClient;
using Dapper;


namespace ChatApp.Data
{
    public class SqlHelper
    {
        public static bool IsExistingUser(string username)
        {
            DatabaseManager.LoggedInUser = username;

            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select case");
            sql.AppendLine("when exists");
            sql.AppendLine("(");
            sql.AppendLine("select name");
            sql.AppendLine("from Users");
            sql.AppendLine("where name = @Username");
            sql.AppendLine(")");
            sql.AppendLine("then 'true'");
            sql.AppendLine("else 'false'");
            sql.AppendLine("end as UserExists");
            #endregion

            #region Execution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var isExistingUser = connection.ExecuteScalar<string>
            (
                sql.ToString(), new { Username = username }
            );

            return isExistingUser.Equals("true");
            #endregion
        }

        public static void AddNewUser()
        {
            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("insert into users");
            sql.AppendLine("(name)");
            sql.AppendLine("values(@Name)");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            connection.Query
            (
                sql.ToString(), new { Name = DatabaseManager.LoggedInUser }
            );
            #endregion
        }

        public static List<RecipientMessageHistoryModel> GetPreviouslyMessagedRecipientsForUser()
        {
            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select distinct recipient");
            sql.AppendLine("from Messages");
            sql.AppendLine("where author = @LoggedInUser");
            sql.AppendLine("and recipient like '@%'");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var recipientList = connection.Query<RecipientMessageHistoryModel>
            (
                sql.ToString(), new { LoggedInUser = $"@{DatabaseManager.LoggedInUser }" }
            ).ToList();

            return recipientList;
            #endregion
        }

        public static List<RecipientMessageHistoryModel> GetChannelListForUser()
        {
            #region SQL
            var sql = new StringBuilder("");
            sql.AppendLine("select distinct recipient");
            sql.AppendLine("from Messages");
            sql.AppendLine("where author = @LoggedInUser");
            sql.AppendLine("and recipient like '#%'");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var channelList = connection.Query<RecipientMessageHistoryModel>
            (
                sql.ToString(),
                new { LoggedInUser = $"@{DatabaseManager.LoggedInUser}" }
            ).ToList();

            return channelList;
            #endregion
        }

        public static List<UserModel> GetUserListForAutoComplete(string searchParameter)
        {
            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select top 10 name from Users");
            sql.AppendLine($"where name like '{searchParameter}%'");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var resultSet = connection.Query<UserModel>
            (
                sql.ToString()
            ).ToList();

            return resultSet;
            #endregion
        }

        public static List<UserModel> GetUsersListByName(string username)
        {
            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select * from Users");
            sql.AppendLine($"where name like '%{username}%'");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var user = connection.Query<UserModel>
            (
                sql.ToString()
            ).ToList();

            return user;
            #endregion
        }

        public static List<ChannelModel> GetChannels(string channelName)
        {
            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select * from Channels");
            sql.AppendLine($"where name like '%{channelName}%'");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var channels = connection.Query<ChannelModel>
            (
                sql.ToString()
            ).ToList();

            return channels;
            #endregion
        }

        public static List<MessageModel> GetMessages(string recipientName)
        {
            DatabaseManager.Recipient = recipientName.StartsWith("@") ? recipientName : $"@{recipientName}";

            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select * from Messages");
            sql.AppendLine($"where recipient in('{DatabaseManager.Recipient}', '@{DatabaseManager.LoggedInUser}')");
            sql.AppendLine($"and author in('@{DatabaseManager.LoggedInUser}', '{DatabaseManager.Recipient}')");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var messages = connection.Query<MessageModel>
            (
                sql.ToString()
            ).ToList();

            return messages;
            #endregion
        }

        public static List<MessageModel> GetChannelMessages(string channelName)
        {
            DatabaseManager.Recipient = channelName.StartsWith("#") ? channelName : $"#{channelName}";

            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("select * from Messages");
            sql.AppendLine($"where recipient = @RecipientName");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            var channelMessages = connection.Query<MessageModel>
            (
                sql.ToString(),
                new { RecipientName = DatabaseManager.Recipient }
            ).ToList();

            return channelMessages;
            #endregion
        }

        public static void InsertNewMessage(string message)
        {
            #region SQL
            var sql = new StringBuilder();
            sql.AppendLine("insert into Messages");
            sql.AppendLine("(recipient, message, author)");
            sql.AppendLine("values(@Recipient, @Message, @Author)");
            #endregion

            #region QueryExecution
            using var connection = new SqlConnection(DatabaseManager.ConnectionString);
            connection.Query
            (
                sql.ToString(),
                new
                {
                    Recipient = DatabaseManager.Recipient,
                    Message = message,
                    Author = $"@{DatabaseManager.LoggedInUser}"
                }
            );
            #endregion
        }
    }
}
