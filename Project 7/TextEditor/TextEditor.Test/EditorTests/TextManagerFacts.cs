using System;
using TextEditor.Domain.Editor;

namespace TextEditor.Test.EditorTests
{
    public class TextManagerFacts
    {
        [Fact]
        public void CreateValidTextManagerObject()
        {
            string path = GetTestFilePath("singleRow.txt");
            FileManager fileManager = new(path);
            Assert.IsType<TextManager>(new TextManager(fileManager.Text));
        }

        [Fact]
        public void GetRowsCountMethodReturnsTheCountOfRowsInText()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);

            Assert.Equal(4, textManager.GetLinesCount());
        }

        [Fact]
        public void GetLineMethodReturnsLineAtIndex()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);

            Assert.Equal("This is a test.", textManager.GetLine(0));
        }

        [Fact]
        public void GetLineLengthMethodReturnsLineLengthAtIndex()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);

            Assert.Equal(15, textManager.GetLineLength(0));
        }

        [Fact]
        public void AbsoluteCursorPositionReturnsInitialValues()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);

            Assert.Equal((0, 0), textManager.AbsoluteCursorPosition);
        }

        [Fact]
        public void OnCharacterKeyPressedMethodInsertCharacterAtAbsolutePositionInEmptyFile()
        {
            var path = GetTestFilePath("EmptyFile.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);

            textManager.OnCharacterKeyPressed('a', 0, 0);

            Assert.Equal("a", textManager.GetLine(0));
        }

        [Fact]
        public void OnCharacterKeyPressedMethodInsertCharacterAtAbsolutePositionInWrittenFile()
        {
            var path = GetTestFilePath("SingleChar.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);

            textManager.OnCharacterKeyPressed('a', 0, 0);

            Assert.Equal("aa", textManager.GetLine(0));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
