using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class ViewportFacts
    {
        [Fact]
        public void ViewportObjectExists()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            Viewport viewport = SetupEditor(path);

            Assert.IsType<Viewport>(viewport);
        }

        [Fact]
        public void FirstRowPositionReturnsTheStartingConsoleRowPositionOfDisplayedText()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path, 5);

            Assert.Equal(5, viewport.FirstRowIndex);
        }


        [Fact]
        public void LastRowPositionReturnsThePositionOfLastRowInViewport()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(3, viewport.LastRowIndex);
        }

        [Fact]
        public void LastRowPositionReturnsThePositionOfLastRowInViewportWhenStartPositionIsGreaterThen0()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            Viewport viewport = SetupEditor(path, 5);

            Assert.Equal(8, viewport.LastRowIndex);
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRowWhenTextExceedsWindow()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(28, viewport.LastRowIndex);
        }


        [Fact]
        public void LastRowPositionReturnsCorrectPositionWhenDisplayedTextHeightIsEqualWithWindowHeigth()
        {
            var path = GetTestFilePath("EqualsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(28, viewport.LastRowIndex);
        }

        [Fact]
        public void GetRowLengthReturnsTheRowLengthWhenRowIsSmallerThenWindowWidth()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(15, viewport.GetViewportRowLength(0));
        }

        [Fact]
        public void GetRowLengthReturnsTheDisplayedLengthOfARowWhenStartingRowIsLargerThen0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path, 5);

            Assert.Equal(15, viewport.GetViewportRowLength(5));
        }

        [Fact]
        public void GetRowLengthReturnsTheRowLengthWhenRowIsLongerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(119, viewport.GetViewportRowLength(0));
        }

        [Fact]
        public void GetViewportRowLengthReturnsTheViewportLengthWhenPartOfRowisVisible()
        {
            var path = GetTestFilePath("UnevenLengthRows.txt");
            Viewport viewport = SetupEditor(path);

            for (int i = 0; i < 10; i++)
            {
                viewport.MoveRight(0);
            }

            Assert.Equal(110, viewport.GetViewportRowLength(3));
        }

        [Fact]
        public void RenderTextReturnsTextLimitedToTheWidthOfWindow()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            List<string> formattedText = ["This is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the co"];

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void RenderTextReturnsTheVisibleTextFowConsoleWhenRowsAreLongerThenWindowHeigth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            List<string> formattedText = [];
            for (int i = 1; i <= 29; i++)
            {
                formattedText.Add($"Line {i:00}");
            }

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void RenderTextReturnsTheVisibleTextFowConsoleWhenTextExeedsWindowHeigthAndWidth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
            Viewport viewport = SetupEditor(path);

            List<string> formattedText = [];

            for (int i = 1; i <= 29; i++)
            {
                formattedText.Add($"{i:00}. This is a very long line that exceeds the console width of 120 characters. " +
                    $"This is a very long line that exceeds th");
            }

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void FirstRowAbsoluteIndexReturns0AsInitialValue()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(0, viewport.FirstRowAbsoluteIndex);
        }

        [Fact]
        public void LastRowAbsoluteIndexReturnsAbsoluteIndexOfLastRow()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(28, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void LastRowAbsoluteIndexReturnsAbsoluteIndexOfLastRowWhenFirstRowIndexIsLargerThen0()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            Viewport viewport = SetupEditor(path, 5);

            Assert.Equal(3, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void LastRowAbsoluteIndexReturnsAbsoluteIndexOfLastRowWhenTextHeightExeedsWindowHeigth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(28, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void LastRowAbsoluteIndexReturnsAbsoluteIndexWhenTextHeightExeedsWindowHeigthAndStartRowLargerThen0()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path, 2);

            Assert.Equal(26, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void FirstColumnAbsoluteIndexReturns0Initially()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(0, viewport.FirstColumnAbsoluteIndex);
        }

        [Fact]
        public void LastColumnAbsoluteIndexReturnsMaximumIndexWhenRowLengthExceedsWindow()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(118, viewport.LastColumnAbsoluteIndex(0));
        }

        [Fact]
        public void LastColumnAbsoluteReturnsLastColumnWhenRowDoNotExceedWindowLength()
        {
            var path = GetTestFilePath("SingleChar.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(0, viewport.LastColumnAbsoluteIndex(0));
        }

        [Fact]
        public void LastColumnAbsoluteReturnsCorrectLastIndexForRowsOfDifferentLengths()
        {
            var path = GetTestFilePath("UnevenLengthRows.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(118, viewport.LastColumnAbsoluteIndex(0));
            Assert.Equal(20, viewport.LastColumnAbsoluteIndex(1));
        }

        [Fact]
        public void LastColumnAbsoluteReturnsNegativeOneWhenLineIsEmpty()
        {
            var path = GetTestFilePath("EmptyRow.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(118, viewport.LastColumnAbsoluteIndex(0));
            Assert.Equal(-1, viewport.LastColumnAbsoluteIndex(1));
        }

        [Fact]
        public void MoveRightIncresesTheColumnLimitsOfViewportWithOnePosition()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(0);

            Assert.Equal(1, viewport.FirstColumnAbsoluteIndex);
            Assert.Equal(119, viewport.LastColumnAbsoluteIndex(0));
        }

        [Fact]
        public void RenderTextMethodRendersViewportMovedRight()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(0);

            List<string> formattedText = ["his is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the con"];

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void MoveLeftDecresesTheColumnLimitsOfViewportWithOnePosition()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(0);
            viewport.MoveLeft();

            Assert.Equal(0, viewport.FirstColumnAbsoluteIndex);
            Assert.Equal(118, viewport.LastColumnAbsoluteIndex(0));
        }

        [Fact]
        public void RenderTextMethodRendersTextMOvedLeft()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(0);
            viewport.MoveLeft();

            List<string> formattedText = ["This is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the co"];

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void MoveDownIncresesTheRowLimitsOfViewportWithOnePosition()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveDown();

            Assert.Equal(1, viewport.FirstRowAbsoluteIndex);
            Assert.Equal(29, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void RenderTextMethodRendersTextMOvedDown()
        {
            var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveDown();

            List<string> formattedText = [];

            for (int i = 2; i <= 30; i++)
            {
                formattedText.Add($"{i:00}. This is a very long line that exceeds the console width of 120 characters. " +
                    $"This is a very long line that exceeds th");
            }

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void MoveUpDecresesTheRowLimitsOfViewportWithOnePosition()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveDown();
            viewport.MoveUp();

            Assert.Equal(0, viewport.FirstRowAbsoluteIndex);
            Assert.Equal(28, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void RenderTextMethodRendersTextMOvedUp()
        {
            var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveDown();
            viewport.MoveUp();

            List<string> formattedText = [];

            for (int i = 1; i <= 29; i++)
            {
                formattedText.Add($"{i:00}. This is a very long line that exceeds the console width of 120 characters. " +
                    $"This is a very long line that exceeds th");
            }

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void OnCursorExitedViewRenderTextToRightWhenDirectionIsRight()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.OnCursorExitedView(Direction.Right, 0);

            List<string> formattedText = ["his is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the con"];

            Assert.Equal(formattedText, viewport.RenderText());
        }

        [Fact]
        public void ViewportMovedIsInvokedWhenOnCursoExitedViewIsCalled()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            List<string> result = [];
            viewport.ViewportMoved += (renderedText, position) => result = renderedText;

            viewport.OnCursorExitedView(Direction.Right, 0);

            List<string> renderedText = ["his is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the con"];

            Assert.Equal(renderedText, viewport.RenderText());
        }

        [Fact]
        public void MovedDownMethodStopsMovingViewportAtLastLineOfText()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);

            int count = 0;
            while(count < 5)
            {
                viewport.MoveDown();
                count++;
            }

            Assert.Equal(31, viewport.LastRowAbsoluteIndex);
        }

        [Fact]
        public void MovedUpMethodStopsMovingViewportFirstLineOfText()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveUp();

            Assert.Equal(0, viewport.FirstRowAbsoluteIndex);
        }

        [Fact]
        public void RowIsAtRightLimitReturnsTrueWhenTextLineIsAtEnd()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            for (int i = 0; i < 30; i++)
            {
                viewport.MoveRight(0);
            }

            Assert.True(viewport.IsRightmostLimit(0));
        }

        [Fact]
        public void MoveRightMethodStopsMovingViewportWhenRowAtRightLimitIsTrue()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            for (int i = 0; i < 31; i++)
            {
                viewport.MoveRight(0);
            }

            Assert.Equal(148, viewport.LastColumnAbsoluteIndex(0));
        }

        [Fact]
        public void MoveRightMethodGetsCorrectTextLineFromConsoleRow()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path, 3);

            viewport.MoveRight(3);
            int r = viewport.FirstColumnAbsoluteIndex;

            Assert.Equal(1, viewport.FirstColumnAbsoluteIndex);
        }

        [Fact]
        public void RenderTextMethdRendersEmptyLine()
        {
            var path = GetTestFilePath("EmptyRow.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(2);
            List<string> result =  ["his is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the con", "",
                "his is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the con"];

            List<string> renderedText = viewport.RenderText();

            Assert.Equal(result, viewport.RenderText());
        }

        [Fact]
        public void RenderTextMethodRendersCorectlyWhenLineGotOutsideOfViewport()
        {
            var path = GetTestFilePath("UnevenLengthRows.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(0);
            viewport.MoveRight(0);

            List<string> result = ["is is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the cons",
                "is is a short line.", "", "is is a 120 charcaters line long " +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"];

            Assert.Equal(0, viewport.LastColumnAbsoluteIndex(2));
            Assert.Equal(result, viewport.RenderText());
        }

        [Fact]
        public void IsDownmostLimitReturnsTrueWhenLastDisplayedRowIsLastRowOfText()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            Viewport viewport = SetupEditor(path);
            for (int i = 0; i < 3; i++)
            {
                viewport.MoveDown();
            }
            Assert.True(viewport.IsDownmostLimit(28));
        }

        [Fact]
        public void IsLeftmostLimitVisibleReturnsFalseWhenFirstColumnAbsoluteIndexIsLargerThen0()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            for (int i = 0; i < 120; i++)
            {
                viewport.MoveRight(0);
            }

            Assert.False(viewport.IsLeftmostLimit());
        }

        [Fact]
        public void MoveMaxLeftMovesViewportAtTheStartOfTheRow()
        {
            var path = GetTestFilePath("UnevenLengthRows.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight(0);
            viewport.MoveMaxLeft();

            Assert.Equal(0, viewport.FirstColumnAbsoluteIndex);
        }

        //[Fact]
        //public void MoveMaxLeftAndDownMovesViewportAtStartOfRowAndDownOneRow()
        //{
        //    var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
        //    Viewport viewport = SetupEditor(path);

        //    for(int i = 0; i < 34; i++)
        //    {
        //        viewport.MoveRight(0);
        //    }
            
        //    viewport.MoveMaxLeftAndDown();

        //    Assert.Equal(0, viewport.FirstColumnAbsoluteIndex);
        //    Assert.Equal(29, viewport.LastRowAbsoluteIndex);
        //}

        //[Fact]
        //public void MoveMaxRightAndUpMovesViewportAtEndOfRowAndUpOneRow()
        //{
        //    var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
        //    Viewport viewport = SetupEditor(path);

        //    viewport.MoveDown();
        //    viewport.MoveMaxRightAndUp(0);

        //    Assert.Equal(34, viewport.FirstColumnAbsoluteIndex);
        //    Assert.Equal(0, viewport.FirstRowAbsoluteIndex);
        //}

        [Fact]
        public void MoveMaxRightMovesViewportAtTheEndOfTheRow()
        {
            var path = GetTestFilePath("UnevenLengthRows.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveMaxRight(0);

            Assert.Equal(30, viewport.FirstColumnAbsoluteIndex);
        }

        [Fact]
        public void IsUpmostLimitVisibleReturnsTrueWhenFirstRowAbsoluteIndexIs0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path);
            Assert.True(viewport.IsUpmostLimit(0));
        }

        [Fact]
        public void MoveToRowEndMovesViewportAtTheEndOfRow()
        {
            var path = GetTestFilePath("UnevenLengthRows.txt");
            Viewport viewport = SetupEditor(path);

            for (int i = 0; i < 30; i++)
            {
                viewport.MoveRight(0);
            }

            viewport.MoveToRowEnd(1);
            Assert.Equal(20, viewport.FirstColumnAbsoluteIndex);
        }

        [Fact]
        public void MoveToRowEndMovesViewportAtAbsolutPosition0WhenRowIsEmpty()
        {
            var path = GetTestFilePath("EmptyRow.txt");
            Viewport viewport = SetupEditor(path);

            for (int i = 0; i < 10; i++)
            {
                viewport.MoveRight(0);
            }

            viewport.MoveToRowEnd(1);
            Assert.Equal(0, viewport.FirstColumnAbsoluteIndex);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }

        private static Viewport SetupEditor(string path, int startingRow = 0)
        {
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, startingRow, textInfo);

            return viewport;
        }
    }
}
