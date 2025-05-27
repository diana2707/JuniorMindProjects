using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class CursorFacts
    {
        [Fact]
        public void CursorObjectExists()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new (path);
            Assert.IsType<Cursor>(new Cursor(manager));
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveRight();
            Assert.Equal(1, cursor.Column);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorDownWhenCursorIsAtLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveRight();
            cursor.MoveRight();

            Assert.Equal(0, cursor.Column);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
