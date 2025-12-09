using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab5.обработка
{
    public static class Logger
    {
        public enum UserType
        {
            Guest,
            User,
            Admin
        }

        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            Security
        }

        public static UserType CurrentUser { get; private set; } = UserType.Guest;
        public static string CurrentUserName { get; private set; } = "Гость";

        private static readonly string logFilePath;
        private static readonly string usersFilePath;
        private const long MAX_LOG_FILE_SIZE = 5 * 1024 * 1024;

        static Logger()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string logDirectory = Path.Combine(baseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);

            string dataDirectory = Path.Combine(baseDirectory, "Data");
            Directory.CreateDirectory(dataDirectory);

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            logFilePath = Path.Combine(logDirectory, $"salad_log_{date}.txt");
            usersFilePath = Path.Combine(dataDirectory, "users.dat");

            InitializeLogFile();
            InitializeDefaultAdmin();
        }

        private static void InitializeLogFile()
        {
            try
            {
                if (!File.Exists(logFilePath))
                {
                    File.WriteAllText(logFilePath, $"=== LOG FILE CREATED {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===\n\n");
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(logFilePath);
                    if (fileInfo.Length > MAX_LOG_FILE_SIZE)
                    {
                        ArchiveOldLog();
                    }
                }
            }
            catch
            {
            }
        }

        private static void InitializeDefaultAdmin()
        {
            try
            {
                if (!File.Exists(usersFilePath))
                {
                    List<UserData> defaultUsers = new List<UserData>
                    {
                        new UserData { Username = "admin", Password = "admin123", UserType = UserType.Admin }
                    };
                    SaveUsers(defaultUsers);
                }
            }
            catch
            {
            }
        }

        private static void ArchiveOldLog()
        {
            try
            {
                string archiveName = $"salad_log_{DateTime.Now:yyyy-MM-dd_HHmmss}.archive.txt";
                string archivePath = Path.Combine(Path.GetDirectoryName(logFilePath), archiveName);
                File.Move(logFilePath, archivePath);
                File.WriteAllText(logFilePath, $"=== LOG FILE ARCHIVED AND CREATED NEW {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===\n\n");
            }
            catch
            {
                File.WriteAllText(logFilePath, $"=== LOG FILE CLEARED {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===\n\n");
            }
        }

        public static bool Login(string username, string password)
        {
            try
            {
                var users = LoadUsers();
                var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    CurrentUser = user.UserType;
                    CurrentUserName = username;

                    Log(LogLevel.Security, "Пользователь вошел в систему",
                        $"Пользователь: {CurrentUserName}, Тип: {CurrentUser}");

                    return true;
                }
                else
                {
                    Log(LogLevel.Security, "Неудачная попытка входа",
                        $"Имя пользователя: {username}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, "Ошибка при входе в систему", ex.Message);
                return false;
            }
        }

        public static void Logout()
        {
            string previousUser = CurrentUserName;
            UserType previousType = CurrentUser;

            CurrentUser = UserType.Guest;
            CurrentUserName = "Гость";

            Log(LogLevel.Security, "Пользователь вышел из системы",
                $"Предыдущий пользователь: {previousUser}, Тип: {previousType}");
        }

        public static void Log(LogLevel level, string message, string details = "")
        {
            try
            {
                string logEntry = FormatLogEntry(level, message, details);

                lock (logFilePath)
                {
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                }

                if (level == LogLevel.Error || level == LogLevel.Security || level == LogLevel.Warning)
                {
                    WriteColoredConsole(level, logEntry);
                }
            }
            catch
            {
            }
        }

        private static string FormatLogEntry(LogLevel level, string message, string details)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string userInfo = $"{CurrentUserName} ({CurrentUser})";
            string detailsPart = string.IsNullOrEmpty(details) ? "" : $"\n    Детали: {details}";

            return $"[{timestamp}] [{level}] [{userInfo}] {message}{detailsPart}";
        }

        private static void WriteColoredConsole(LogLevel level, string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            switch (level)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Security:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ForegroundColor = originalColor;
                    break;
            }

            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }

        public static bool IsAdmin()
        {
            return CurrentUser == UserType.Admin;
        }
        public static bool IsUser()
        {
            return CurrentUser == UserType.User;
        }
        public static void LogInfo(string message, string details = "")
        {
            Log(LogLevel.Info, message, details);
        }

        public static void LogWarning(string message, string details = "")
        {
            Log(LogLevel.Warning, message, details);
        }

        public static void LogError(string message, string details = "")
        {
            Log(LogLevel.Error, message, details);
        }

        public static void LogSecurity(string message, string details = "")
        {
            Log(LogLevel.Security, message, details);
        }

        public static void LogSaladCreated(string saladName, int ingredientCount)
        {
            LogInfo("Создан новый салат",
                $"Название: {saladName}, Ингредиентов: {ingredientCount}");
        }

        public static void LogSaladDeleted(string saladName)
        {
            LogWarning("Салат удален",
                $"Название: {saladName}, Пользователь: {CurrentUserName}");
        }

        public static void LogFileSaved(string fileName, string saladName)
        {
            LogInfo("Салат сохранен в файл",
                $"Файл: {fileName}, Салат: {saladName}");
        }

        public static void LogFileLoaded(string fileName, int saladCount)
        {
            LogInfo("Данные загружены из файла",
                $"Файл: {fileName}, Загружено салатов: {saladCount}");
        }

        private class UserData
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public UserType UserType { get; set; }
        }

        private static List<UserData> LoadUsers()
        {
            List<UserData> users = new List<UserData>();

            if (File.Exists(usersFilePath))
            {
                try
                {
                    var lines = File.ReadAllLines(usersFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            users.Add(new UserData
                            {
                                Username = parts[0],
                                Password = parts[1],
                                UserType = (UserType)Enum.Parse(typeof(UserType), parts[2])
                            });
                        }
                    }
                }
                catch
                {
                }
            }

            return users;
        }

        private static void SaveUsers(List<UserData> users)
        {
            try
            {
                var lines = users.Select(u => $"{u.Username}|{u.Password}|{u.UserType}");
                File.WriteAllLines(usersFilePath, lines);
            }
            catch
            {
            }
        }
    }
}