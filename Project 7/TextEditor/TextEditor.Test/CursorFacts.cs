using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class CursorFacts
    {
        [Fact]
        public void CursorObjectExists()
        {
            string path = GetTestFilePath("singleRow.txt");
            FileManager manager = new (path);
            Assert.IsType<Cursor>(new Cursor(manager));
        }

        //[Fact]
        //public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        //{
        //    string path = GetTestFilePath("singleRow.txt");
        //    FileManager manager = new(path);
        //    Cursor cursor = new (manager);
        //    Assert.
        //}

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
