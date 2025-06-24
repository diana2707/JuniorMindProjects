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
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Assert.IsType<Cursor>(new Cursor(formatter));
        }

        [Fact]
        public void ColumnPropertyReturnsColumnPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            Assert.Equal(15, cursor.Column);
        }

        [Fact]
        public void RowPropertyReturnsRowPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
            cursor.MoveRight();

            Assert.Equal(15, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorDownWhenCursorIsAtLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
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
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorToLefttWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
            cursor.MoveLeft();

            Assert.Equal(14, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorUpWhenCursorIsAtLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
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
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStartWhenStartingPositionIsLargerThen0()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 5);
            Cursor cursor = new(formatter);
            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(5, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDowntWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
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
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
            cursor.MoveDown();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDownAndSetsColumnToLastPositionWhenLineIsShorterThenPrevious()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
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
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
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
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);
            cursor.MoveUp();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRowAndStartingPositionIsLArgerThen0()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 5);
            Cursor cursor = new(formatter);
            cursor.MoveUp();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(5, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpAndSetsColumnToLastPositionIfRowIsShorter()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            cursor.MoveUp();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void SetToStartOfLastLineSetsTheCursorAtTheStartOfLastVisibleLine()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            cursor.SetToStartOfLastLine();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(4, cursor.Row);
        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveRightMethodWhenCursorIsAtRightmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            int resultRow = -1;
            int resultColumn = -1;

            void TestObserveble(int row, int column)
            {
                resultRow = row;
                resultColumn = column;
            }

            cursor.CursorExitedView += TestObserveble;

            cursor.MoveRight();
            //cursor.MoveRight();

            Assert.Equal(0, resultRow);
            Assert.Equal(1, resultColumn);

        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveLeftMethodWhenCursorIsAtLeftmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            int resultRow = -1;
            int resultColumn = -1;

            void TestObserveble(int row, int column)
            {
                resultRow = row;
                resultColumn = column;
            }

            cursor.CursorExitedView += TestObserveble;

            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(0, resultRow);
            Assert.Equal(0, resultColumn);

        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveDownMethodWhenCursorIsAtDownmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);
            Cursor cursor = new(formatter);

            int resultRow = -1;
            int resultColumn = -1;

            void TestObserveble(int row, int column)
            {
                resultRow = row;
                resultColumn = column;
            }

            cursor.CursorExitedView += TestObserveble;

            cursor.MoveDown();

            Assert.Equal(0, resultRow);
            Assert.Equal(1, resultColumn);

        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
