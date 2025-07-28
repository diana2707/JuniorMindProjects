using System;
using TextEditor.Domain.Cursor;
using TextEditor.Domain.Display;
using TextEditor.Domain.Editor;
using TextEditor.Domain.Shared;

namespace TextEditor.Test.CursorTests
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
            cursor.SetDefaultPosition();

            Assert.Equal(0, cursor.Column);
        }

        [Fact]
        public void RowPropertyReturnsRowPosition()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorToRightWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveRightMethodMovesTheCursorDownWhenCursorIsAtLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveRight();
            cursor.MoveRight();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }


        [Fact]
        public void MoveRightMethodDoNotMovesCursorWhenIsAtFileEnd()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveRight();
            cursor.MoveRight();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorToLefttWithOneChar()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveRight();
            cursor.MoveRight();
            cursor.MoveLeft();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodMovesTheCursorUpWhenCursorIsAtLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveRight();
            cursor.MoveRight();
            cursor.MoveLeft();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStart()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);

            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveLeftMethodDoNotMoveWhenCursorIsAtFileStartWhenStartingPositionIsLargerThen0()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path, 5);
            cursor.SetDefaultPosition();

            cursor.MoveLeft();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(5, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDowntWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodMovesTheCursorDownAndSetsColumnToLastPositionWhenLineIsShorterThenPrevious()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();
            for (int i = 0; i < 24; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveDown();

            Assert.Equal(21, cursor.Column);
            Assert.Equal(2, cursor.Row);
        }

        [Fact]
        public void MoveDownMethodDoNotMovesTheCursorDownWhenIsLastRow()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodMovesTheCursorUpWithOneRowAndKeepsColumnPosition()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();
            cursor.MoveRight();
            cursor.MoveUp();

            Assert.Equal(1, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRowAndStartingPositionIsLArgerThen0()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path, 5);
            cursor.SetDefaultPosition();

            cursor.MoveUp();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(5, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesTheCursorUpWhenIsTheFirstRow()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveUp();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }


        [Fact]
        public void MoveUpMethodMovesTheCursorUpAndSetsColumnToLastPositionIfRowIsShorter()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();
            for(int i = 0; i < 24; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveUp();

            Assert.Equal(15, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void SetToStartOfLastLineSetsTheCursorAtTheStartOfLastVisibleLine()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveAfterViewport();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(4, cursor.Row);
        }

        [Fact]
        public void CursorExitedViewIsRaisedByMoveRightMethodWhenCursorIsAtRightmostPosition()
        {
            string path = GetTestFilePath("ExceedsWindowWidth.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            Direction directionResult = 0;
            int rowResult = -1;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                directionResult = direction;
                rowResult = row;
            };

            for(int i = 0; i < 120; i++)
            {
                cursor.MoveRight();
            }
            

            Assert.Equal(Direction.Right, directionResult);
            Assert.Equal(0, rowResult);
        }

        [Fact]
        public void MoveDownMethodDoNotMovesCursorWhenAtDownmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodDoNotMovesCursorWhenCursorIsAtUpmostPosition()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveUp();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveLeftMethodWhenDirectionIsLeft()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.OnArrowKeyPressed(Direction.Right);
            cursor.OnArrowKeyPressed(Direction.Left);

            Assert.Equal(0, cursor.Column);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveRightMethodWhenDirectionIsRight()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.OnArrowKeyPressed(Direction.Right);
            
            Assert.Equal(1, cursor.Column);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveUpMethodWhenDirectionIsUp()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.OnArrowKeyPressed(Direction.Down);
            cursor.OnArrowKeyPressed(Direction.Up);

            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void OnArrowKeyPressedMethodCallsMoveDowntMethodWhenDirectionIsDown()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.OnArrowKeyPressed(Direction.Down);

            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void MoveRightInvokesCursorMovedEventWhenCursorMovesRight()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (1, 0);

            cursor.MoveRight();

            Assert.Equal((1, 0), result);
        }

        [Fact]
        public void MoveRightMethodInvokesCursorMovedEventWhenCursorMovedFromLineEnd()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (0, 1);

            cursor.MoveRight();
            cursor.MoveRight();

            Assert.Equal((0, 1), result);
        }

        [Fact]
        public void MoveRightMethodInvokesCursorExitedViewWhenCursorMovesAtTheStartOfLineAndStartIsOutsideView()
        {
            string path = GetTestFilePath("UnevenLengthRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;
            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 150; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveRight();

            Assert.Equal(Direction.MaxLeft, resultDirection);
            Assert.Equal(1, resultRow);
        }

        [Fact]
        public void MoveRightMethodInvokesCursorExitedViewWithDirectionMaxleftAndDownWhenCursorIsOnLastVisibleLineAtRightmostPosition()
        {
            string path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 28;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 28; i++)
            {
                cursor.MoveDown();
            }

            for (int i = 0; i < 154; i++)
            {
                cursor.MoveRight();
            }

            Assert.Equal(Direction.MaxLeft, resultDirection);
            Assert.Equal(28, resultRow);

        }   


        [Fact]
        public void MoveLeftMethodInvokesCursorMovedEventWhenCursorMoveLeft()
        {
            string path = GetTestFilePath("SingleRow.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (0, 0);

            cursor.MoveRight();
            cursor.MoveLeft();

            Assert.Equal((0, 0), result);
        }

        [Fact]
        public void MoveLeftMethodInvokesCursorMovedEventWhenCursorMovedFromLineStart()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (1, 0);

            cursor.MoveDown();
            cursor.MoveLeft();

            Assert.Equal((1, 0), result);
        }

        [Fact]
        public void MoveLeftMethodInvokesCursorExitedViewWhenCursorMovesAtEndOfLineAndEndIsOutsideView()
        {
            string path = GetTestFilePath("UnevenLengthRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            cursor.MoveDown();
            cursor.MoveLeft();

            Assert.Equal(Direction.MaxRight, resultDirection);
            Assert.Equal(0, resultRow);
        }

        [Fact]
        public void MoveLeftMethodInvokesCursorExitedViewWhenCursorMovesLeftAndIsNotAtLeftmostPosition()
        {
            string path = GetTestFilePath("UnevenLengthRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            cursor.MoveDown();
            cursor.MoveLeft();

            Assert.Equal(Direction.MaxRight, resultDirection);
            Assert.Equal(0, resultRow);
        }

        [Fact]
        public void MoveLeftMethodInvokesCursorExitedViewWithDirectionMaxRightAndUpWhenCursorIsInFirstPositionOnFirstVisibleRow()
        {
            string path = GetTestFilePath("ExceedsWindowHeigth.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 29; i++)
            {
                cursor.MoveDown();
            }

            for (int i = 0; i < 28; i++)
            {
                cursor.MoveUp();
            }

           cursor.MoveLeft();

            Assert.Equal(Direction.Up, resultDirection);
            Assert.Equal(0, resultRow);
        }

        [Fact]
        public void MoveDownMethodInvokesCursorMovedEventWhenCursorMovedDown()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (0, 1);

            cursor.MoveDown();

            Assert.Equal((0, 1), result);
        }

        [Fact]
        public void MoveDownMethodInvokesCursorExitedViewEventWhenCursorMovesDownAndNextRowIsShorter()
        {
            string path = GetTestFilePath("UnevenLengthRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 145; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveDown();

            Assert.Equal(Direction.ToRowEnd, resultDirection);
            Assert.Equal(1, resultRow);
        }

        [Fact]
        public void MoveDownMethodInvokesCursorExitedViewWhenOnLastVisibleRowAndSetsColumnToEndOfNewLastRowWhenShorter()
        {
            string path = GetTestFilePath("UnevenLengthRowsExceedWindowHeight.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 28; i++)
            {
                cursor.MoveDown();
            }

            for (int i = 0; i < 149; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveDown();

            Assert.Equal(Direction.Down, resultDirection);
            Assert.Equal(28, cursor.Row);
            Assert.Equal(21, cursor.Column);
        }

        [Fact]
        public void MoveUpMethodInvokesCursorMovedEventWhenCursorMovedUp()
        {
            string path = GetTestFilePath("SingleCharRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (0, 0);

            cursor.MoveDown();
            cursor.MoveUp();

            Assert.Equal((0, 0), result);
        }

        [Fact]
        public void MoveUpMethodMovesCursorAtEndOfPreviousRowWhenRowIsShorter()
        {
            string path = GetTestFilePath("MultipleRows.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.MoveDown();

            for (int i = 0; i < 21; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveUp();

            Assert.Equal(15, cursor.Column);
            Assert.Equal(0, cursor.Row);
        }

        [Fact]
        public void MoveUpMethodInvokesCursorExitedViewEventWhenCursorMovesUpAndPreviousRowIsShorterAndOutsideView()
        {
            string path = GetTestFilePath("UnevenLengthRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            cursor.MoveDown();
            cursor.MoveDown();
            cursor.MoveDown();

            for (int i = 0; i < 120; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveUp();

            Assert.Equal(Direction.ToRowEnd, resultDirection);
            Assert.Equal(2, resultRow);
        }

        [Fact]
        public void MoveUpMethodInvokesCursorExitedViewEventWhenCursorIsOnFirstVisibleRowAndMovesUpAndPreviousRowIsShorter()
        {
            string path = GetTestFilePath("UnevenLengthRowsExceedWindowHeight.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 29; i++)
            {
                cursor.MoveDown();
            }

            for (int i = 0; i < 28; i++)
            {
                cursor.MoveUp();
            }

            for (int i = 0; i < 5; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveUp();

            Assert.Equal(Direction.Up, resultDirection);
            Assert.Equal(0, cursor.Row);
            Assert.Equal(1, cursor.Column);
        }

        [Fact]
        public void MoveUpMethodInvokesCursorExitedViewEventWhenCursorIsOnFirstVisibleRowAndMovesUpAndPreviousRowIsShorterAndOutsideView()
        {
            string path = GetTestFilePath("UnevenLengthRowsExceedWindowHeight.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Cursor cursor = new(viewport);
            cursor.SetDefaultPosition();

            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            Direction resultDirection = 0;
            int resultRow = 0;

            cursor.CursorExitedViewport += (direction, row) =>
            {
                resultDirection = direction;
                resultRow = row;
            };

            for (int i = 0; i < 29; i++)
            {
                cursor.MoveDown();
            }

            for (int i = 0; i < 28; i++)
            {
                cursor.MoveUp();
            }

            for (int i = 0; i < 121; i++)
            {
                cursor.MoveRight();
            }

            cursor.MoveUp();

            Assert.Equal(Direction.ToRowEnd, resultDirection);
            Assert.Equal(0, cursor.Row);
            Assert.Equal(1, cursor.Column);
        }

        [Fact]
        public void OnEscapeKeyPressedMethodCallsSetToStartOfLastLineMethod()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            cursor.OnEscapeKeyPressed();

            Assert.Equal(0, cursor.Column);
            Assert.Equal(1, cursor.Row);
        }

        [Fact]
        public void OnEscapeKeyPressedMethodInvokesCursorMovedEvent()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            (int, int) result = (0, 0);
            cursor.CursorMoved += _ => result = (0, 1);

            cursor.OnEscapeKeyPressed();

            Assert.Equal((0, 1), result);
        }

        [Fact]
        public void SetInitialPositionMethodRaisesACursorMovedEventToPlaceTheCursorAtTheEndOfTheVisibleText()
        {
            string path = GetTestFilePath("SingleChar.txt");
            Cursor cursor = SetupEditor(path);
            cursor.SetDefaultPosition();

            bool result = false;
            cursor.CursorMoved += _ => result = true;
            cursor.SetDefaultPosition();

            Assert.True(result);
        }

        [Fact]
        public void CursorMovedEventIsInvokedAfterCursorInvokesCursorExitedViewEvent()
        {
            string path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Cursor cursor = SetupEditor(path);

            for (int i = 0; i < 30; i++)
            {
                cursor.MoveDown();
            }

            bool cursorMoved = false;
            cursor.CursorMoved += _ => cursorMoved = true;
            cursor.MoveDown();

            Assert.True(cursorMoved);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }

        private static Cursor SetupEditor(string path, int startingRow = 0)
        {
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, startingRow, textInfo);
            Cursor cursor = new(viewport);

            return cursor;
        }
    }
}
