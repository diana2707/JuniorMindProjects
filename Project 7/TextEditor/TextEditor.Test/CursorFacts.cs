using System;
using System.IO;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class CursorFacts
    {
        [Fact]
        public void CursorObjectExists()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            Assert.IsType<Cursor>(cursor);
        }

        [Fact]
        public void ColumnPropertyReturnsColumnPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            Assert.Equal(15, cursor.Column);
        }

        [Fact]
        public void RowPropertyReturnsRowPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveRight();

            Assert.Equal(15, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorDownWhenCursorIsAtLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveUp();
            cursor.MoveRight();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }


        [Fact]
        public void MoveRightMethodDoNotMovesCursorWhenIsAtFileEnd()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorToLefttWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveLeft();

            Assert.Equal(14, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorUpWhenCursorIsAtLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStart()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStartWhenStartingPositionIsLargerThen0()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path, 5);

            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(5, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDowntWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveUp();
            cursor.MoveDown();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDownAndSetsColumnToLastPositionWhenLineIsShorterThenPrevious()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveUp();
            cursor.MoveUp();
            cursor.MoveDown();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodDoNotMovesTheCursorDownWhenIsLastRow()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveDown();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }


        [Fact]
        public void MoveUpMethodMovesTheCursorUpWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveUp();
            cursor.MoveUp();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRow()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveUp();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRowAndStartingPositionIsLArgerThen0()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path, 5);

            cursor.MoveUp();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(5, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpAndSetsColumnToLastPositionIfRowIsShorter()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveUp();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void SetToStartOfLastLineSetsTheCursorAtTheStartOfLastVisibleLine()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.SetToStartOfLastLine();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(4, cursor.Row);
        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveRightMethodWhenCursorIsAtRightmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0; 

            cursor.CursorExitedView += direction => result = direction;

            cursor.MoveRight();

            Assert.Equal(Direction.Right, result);
        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveLeftMethodWhenCursorIsAtLeftmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;

            cursor.CursorExitedView += direction => result = direction;

            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(Direction.Left, result);
        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveDownMethodWhenCursorIsAtDownmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;

            cursor.CursorExitedView += direction => result = direction;
            cursor.MoveDown();

            Assert.Equal(Direction.Down, result);
        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveUpMethodWhenCursorIsAtUpmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;

            cursor.CursorExitedView += direction => result = direction;
            cursor.MoveDown();
            cursor.MoveUp();

            Assert.Equal(Direction.Up, result);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveLeftMethodWhenDirectionIsLeft()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            cursor.OnArrowKeyPressed(Direction.Left);

            Assert.Equal(0, cursor.Column);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveRightMethodWhenDirectionIsRight()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            cursor.OnArrowKeyPressed(Direction.Left);
            cursor.OnArrowKeyPressed(Direction.Right);
            
            Assert.Equal(1, cursor.Column);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveUpMethodWhenDirectionIsUp()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.OnArrowKeyPressed(Direction.Up);

            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveDowntMethodWhenDirectionIsDown()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            cursor.OnArrowKeyPressed(Direction.Up);
            cursor.OnArrowKeyPressed(Direction.Down);

            Assert.Equal(3, cursor.Row);
        }

        public void MoveRightInvokesCursorMovedEventWhenCursorMovesRight()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;
            cursor.CursorMoved += direction => result = direction;

            cursor.MoveRight();

            Assert.Equal(Direction.Right, result);
        }

        [Fact]
        public void MoveRightMethodInvokesCursorMovedEventWhenCursorMovedFromLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;
            cursor.CursorMoved += direction => result = direction;

            cursor.MoveUp();
            cursor.MoveRight();

            Assert.Equal(Direction.Right, result);
        }

        [Fact]
        public void MoveLeftMethodInvokesCursorMovedEventWhenCursorMoveLeft()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;
            cursor.CursorMoved += direction => result = direction;

            cursor.MoveLeft();

            Assert.Equal(Direction.Left, result);
        }

        [Fact]
        public void MoveLeftMethodInvokesCursorMovedEventWhenCursorMovedFromLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;
            cursor.CursorMoved += direction => result = direction;

            cursor.MoveLeft();
            cursor.MoveLeft();

            Assert.Equal(Direction.Left, result);
        }

        [Fact]
        public void MoveDownMethodInvokesCursorMovedEventWhenCursorMovedDown()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;
            cursor.CursorMoved += direction => result = direction;

            cursor.MoveUp();
            cursor.MoveDown();

            Assert.Equal(Direction.Down, result);
        }

        [Fact]
        public void MoveDownMethodInvokesCursorMovedEventWhenLineIsShorterThenPrevious()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);

            Direction result = 0;
            cursor.CursorMoved += direction => result = direction;

            cursor.MoveUp();
            cursor.MoveUp();
            cursor.MoveDown();

            Assert.Equal(Direction.Down, result);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }

        private static Cursor SetupEditor(string path, int startingRow = 0)
        {
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, startingRow);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, startingRow, textInfo);
            Cursor cursor = new(viewport);

            return cursor;
        }
    }
}
