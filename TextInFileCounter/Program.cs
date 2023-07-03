using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TextInFileCounter.Test")]
namespace TextInFileCounter
{
    public static class Program
    {
        /// <summary>
        /// Gets search text from the specified file path.
        /// </summary>
        /// <param name="filePath">A file path.</param>
        /// <returns>A search text or an empty string.</returns>
        internal static string GetSearchText(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath).Trim();
        }

        /// <summary>
        /// Opens and reads the specified file and counts
        /// the number of occurrences of the specified text.
        /// </summary>
        /// <param name="text">The text to count occurrences of.</param>
        /// <param name="filePath">The path of the file to open and read.</param>
        /// <returns>The number of occurrences of the text.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="OutOfMemoryException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        internal static int ReadAndCountTextInFile(string text, string filePath)
        {
            int count = 0;

            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader file = new(fileStream))
            {
                while (true)
                {
                    string? line = file.ReadLine();
                    if (line == null)
                        break;

                    count += CountSubstringInString(text, line);
                }
            }

            return count;
        }

        /// <summary>
        /// Counts the number of times the specified substring in the specified string.
        /// </summary>
        /// <param name="substring"></param>
        /// <param name="fullString"></param>
        /// <returns>The substring occurrence count.</returns>
        internal static int CountSubstringInString(string substring, string fullString)
        {
            return fullString.Split(substring).Length - 1;
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments specified. Expected a file path.");
                return;
            }

            if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments specified. Expected a file path.");
                return;
            }

            string filePath = args[0];
            string text = GetSearchText(filePath);
            if (text.Length == 0)
            {
                Console.WriteLine($"File path '{filePath}' has no file name to search for.");
                return;
            }

            try
            {
                int count = ReadAndCountTextInFile(text, filePath);
                Console.WriteLine($"File name '{text}' occurred {count} times in file '{filePath}'.");
            }
            catch (Exception ex) when (
                ex is ArgumentException or ArgumentNullException ||
                ex is ArgumentOutOfRangeException ||
                ex is DirectoryNotFoundException ||
                ex is FileNotFoundException ||
                ex is IOException ||
                ex is NotSupportedException ||
                ex is OutOfMemoryException ||
                ex is PathTooLongException ||
                ex is UnauthorizedAccessException)
            {
                Console.Error.WriteLine(
                    $"An error occurred when opening and reading the file.{Environment.NewLine}Error: {ex.Message}");
            }
        }
    }
}