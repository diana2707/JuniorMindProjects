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
        public void ColumnPropertyReturnsColumnPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            Assert.Equal(0, cursor.Column);
        }

        [Fact]
        public void RowPropertyReturnsRowPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
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
            Assert.Equal(1, cursor.Row);
        }


        [Fact]
        public void MoveRightMethodDoNotMovesCursorWhenIsAtFileEnd()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveRight();
            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorToLefttWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveRight();
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorUpWhenCursorIsAtLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveRight();
            cursor.MoveRight();
            cursor.MoveLeft();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDowntWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveDown();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodDoMotMovesTheCursorDownWhenIsLastRow()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveDown();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDownAndSetsColumnToLastPositionWhenLineIsShorterThenPrevious()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveDown();
            for(int i = 0; i < manager.GetLineLength(cursor.Row) - 2; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveDown();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveDown();
            cursor.MoveUp();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRow()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);
            cursor.MoveUp();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpAndSetsColumnToLastPositionIfRowIsShorter()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager);

            cursor.MoveDown();

            for (int i = 0; i < manager.GetLineLength(cursor.Row); i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveUp();

            Assert.Equal(15, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
