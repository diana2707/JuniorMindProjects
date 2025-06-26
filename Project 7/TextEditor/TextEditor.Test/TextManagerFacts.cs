using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class TextManagerFacts
    {
        [Fact]
        public void CreateValidTextManagerObject()
        {
            string path = GetTestFilePath("singleRow.txt");
            FileManager fileManager = new(path);
            Assert.IsType<TextManager>(new TextManager(fileManager.Text, 120, 30, 0));
        }

        [Fact]
        public void FormatWithinWindowReturnsTextLimitedToTheWidthOfWindow()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 0);
            List<string> formattedText = ["This is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the co"];

            Assert.Equal(formattedText, formatter.FormatWithinWindow());
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRow()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(0, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRowWhenStartingRowIsLargerThen0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 5);

            Assert.Equal(5, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRowWhenTextExceedsWindow()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(28, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void FirstRowPositionReturnsTheStartingConsoleRowPositionOfDisplayedText()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 5);

            Assert.Equal(5, formatter.FirstDisplayedRowPosition);
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARow()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(15, formatter.GetDisplayedRowLength(0));
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARowWhenStartingRowIsLargerThen0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 5);

            Assert.Equal(15, formatter.GetDisplayedRowLength(5));
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARowWhenRealLengthIsLargerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            FileManager fileManager = new(path);
            TextManager formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(119, formatter.GetDisplayedRowLength(0));
        }

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenRowsAreLongerThenWindowHeigth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            FileManager manager = new(path);
            TextManager formatter = new(manager.Text, 120, 30, 0);

            List<string> formattedText = [];
            for (int i = 1; i <= 29; i++)
            {
                formattedText.Add($"Line {i:00}");
            }

            Assert.Equal(formattedText, formatter.FormatWithinWindow());
        }

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenTextExeedsWindowHeigthAndWidth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
            FileManager manager = new(path);
            TextManager formatter = new(manager.Text, 120, 30, 0);

            List<string> formattedText = [];
            for (int i = 1; i <= 29; i++)
            {
                formattedText.Add($"{i:00}. This is a very long line that exceeds the console width of 120 characters. " +
                    $"This is a very long line that exceeds th");
            }

            Assert.Equal(formattedText, formatter.FormatWithinWindow());
        }

        [Fact]
        public void LastDisplayedRowPositionReturnsCorrectPositionWhenDisplayedTextHeightIsEqualWithWindowHeigth()
        {
            var path = GetTestFilePath("EqualsWindowHeigth.txt");
            FileManager manager = new(path);
            TextManager formatter = new(manager.Text, 120, 30, 0);

            Assert.Equal(28, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void GetRowsCountMethodReturnsTheCountOfRowsInText()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(4, textManager.GetLinesCount());
        }

        [Fact]
        public void GetLineMethodReturnsLineAtIndex()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);

            Assert.Equal("This is a test.", textManager.GetLine(0));
        }

        [Fact]
        public void GetLineLengthMethodReturnsLineLengthAtIndex()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(15, textManager.GetLineLength(0));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
