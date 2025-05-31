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
            Assert.IsType<Cursor>(new Cursor(manager, (0, 0)));
        }

        [Fact]
        public void ColumnPropertyReturnsColumnPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            Assert.Equal(15, cursor.Column);
        }

        [Fact]
        public void RowPropertyReturnsRowPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Cursor cursor = new(manager, (0, 0));
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveRight();

            Assert.Equal(15, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorDownWhenCursorIsAtLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveUp();
            cursor.MoveRight();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }


        [Fact]
        public void MoveRightMethodDoNotMovesCursorWhenIsAtFileEnd()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorToLefttWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveLeft();

            Assert.Equal(14, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorUpWhenCursorIsAtLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStart()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDowntWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveUp();
            cursor.MoveDown();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodDoMotMovesTheCursorDownWhenIsLastRow()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveDown();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDownAndSetsColumnToLastPositionWhenLineIsShorterThenPrevious()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));

            cursor.MoveUp();
            cursor.MoveUp();
            cursor.MoveDown();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveUp();
            cursor.MoveUp();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRow()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));
            cursor.MoveUp();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpAndSetsColumnToLastPositionIfRowIsShorter()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            //int row = manager.LinesCount - 1;
            //int column = manager.GetLineLength(row);

            Cursor cursor = new(manager, (0, 0));

            cursor.MoveUp();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
