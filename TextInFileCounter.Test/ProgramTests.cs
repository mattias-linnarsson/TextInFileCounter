namespace TextInFileCounter.Test
{
    public class ProgramTests
    {
        private static string GetTestFilePath(string fileName) => Path.Combine(@"..\..\..\TestFiles\", fileName);

        // Enhancement: This test method could be run nightly since it
        // involves file I/O and may take some time to run for larger files.
        [TestCase("a", "empty.txt", 0)]
        [TestCase("basic", "basic.txt", 2)]
        [TestCase("Gregor", "kafka.txt", 298)]
        [TestCase("just", "kafka.txt", 46)]
        [TestCase("Just", "kafka.txt", 5)]
        [TestCase("RGB", "binary.png", 1)]
        public void ReadAndCountTextInFile_Calculates_CorrectNumberInActualFiles(string text, string testFileName, int expectedCount)
        {
            Assert.That(Program.ReadAndCountTextInFile(text, GetTestFilePath(testFileName)), Is.EqualTo(expectedCount));
        }
        
        [TestCase("", "abc", 0)]
        [TestCase("z", "", 0)]
        [TestCase("z", "abc", 0)]
        [TestCase("z", "z", 1)]
        [TestCase("z", "zz", 2)]
        [TestCase("z", "z z", 2)]
        [TestCase("zz", "z z", 0)]
        [TestCase("zz", "zz", 1)]
        [TestCase("zz", "zzz", 1)]
        [TestCase("zz", "zzzz", 2)]
        [TestCase("zz", "abczz", 1)]
        [TestCase("zz", "abc zz", 1)]
        [TestCase("zz", "zz abc zz", 2)]
        [TestCase("zz", "abc zz abc zz", 2)]
        [TestCase("z z", "showbiz zebras", 1)]
        [TestCase("åäö", "ÅÄÖ", 0)]

        public void CountSubstringInString_Calculates_CorrectNumber(string substring, string fullString, int expectedCount)
        {
            Assert.That(Program.CountSubstringInString(substring, fullString), Is.EqualTo(expectedCount));
        }

        [TestCase(".myfile", "")]
        [TestCase("myfile", "myfile")]
        [TestCase("myfile.txt", "myfile")]
        [TestCase("myfile .txt", "myfile")]
        [TestCase(".myfile.txt", ".myfile")]
        [TestCase(@"C:\temp\.myfile", "")]
        [TestCase(@"C:\temp\myfile", "myfile")]
        [TestCase(@"C:\temp\myfile.txt", "myfile")]
        [TestCase(@"C:\temp\myfile .txt", "myfile")]
        [TestCase(@"C:\temp\.myfile.txt", ".myfile")]
        [TestCase(@"\\server\share\.myfile", "")]
        [TestCase(@"\\server\share\myfile", "myfile")]
        [TestCase(@"\\server\share\myfile.txt", "myfile")]
        [TestCase(@"\\server\share\myfile .txt", "myfile")]
        [TestCase(@"\\server\share\.myfile.txt", ".myfile")]
        public void GetSearchText_Extracts_FileName(string filePath, string expectedSearchText)
        {
            Assert.That(Program.GetSearchText(filePath), Is.EqualTo(expectedSearchText));
        }
        
        [Test]
        public void ReadAndCountTextInFile_NonExistentFilePath_Throws()
        {
            // Test one exception to make sure that exceptions are thrown.
            var ex = Assert.Throws<FileNotFoundException>(() =>
                Program.ReadAndCountTextInFile("xyz", GetTestFilePath("non-existent.txt")));
            Assert.That(ex?.Message, Does.Contain("Could not find file"));
            Assert.That(ex?.Message, Does.Contain("non-existent.txt"));
        }
    }
}