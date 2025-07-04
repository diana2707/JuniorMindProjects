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

            Assert.Equal(15, viewport.GetRowLength(0));
        }

        [Fact]
        public void GetRowLengthReturnsTheDisplayedLengthOfARowWhenStartingRowIsLargerThen0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            Viewport viewport = SetupEditor(path, 5);

            Assert.Equal(15, viewport.GetRowLength(5));
        }

        [Fact]
        public void GetRowLengthReturnsTheRowLengthWhenRowIsLongerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            Assert.Equal(119, viewport.GetRowLength(0));
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

            Assert.Equal(118, viewport.LastColumnAbsoluteIndex);
        }

        [Fact]
        public void MoveRightIncresesTheColumnLimitsOfViewportWithOnePosition()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight();

            Assert.Equal(1, viewport.FirstColumnAbsoluteIndex);
            Assert.Equal(119, viewport.LastColumnAbsoluteIndex);
        }

        [Fact]
        public void MoveRightCallsRenderTextMethodWithNewLimits()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            Viewport viewport = SetupEditor(path);

            viewport.MoveRight();

            List<string> formattedText = ["his is a very long line that exceeds the console width of 120 characters. " +
                "This is a very long line that exceeds the con"];

            Assert.Equal(formattedText, viewport.RenderText());
        }

        //[Fact]
        //public void MoveLeftDecresesTheColumnLimitsOfViewportWithOnePosition()
        //{
        //    var path = GetTestFilePath("ExceedsWindowWidth.txt");
        //    Viewport viewport = SetupEditor(path);

        //    viewport.MoveRight();
        //    viewport.MoveLeft();

        //    Assert.Equal(0, viewport.FirstColumnIndex);
        //    Assert.Equal(118, viewport.LastColumnIndex);
        //}

        //[Fact]
        //public void MoveDownIncresesTheRowLimitsOfViewportWithOnePosition()
        //{
        //    var path = GetTestFilePath("ExceedsWindowHeigth.txt");
        //    Viewport viewport = SetupEditor(path);

        //    viewport.MoveDown();

        //    Assert.Equal(1, viewport.FirstRowIndex);
        //    Assert.Equal(29, viewport.LastRowIndex);
        //}

        //[Fact]
        //public void MoveUpDecresesTheRowLimitsOfViewportWithOnePosition()
        //{
        //    var path = GetTestFilePath("ExceedsWindowHeigth.txt");
        //    Viewport viewport = SetupEditor(path);

        //    viewport.MoveDown();
        //    viewport.MoveUp();

        //    Assert.Equal(0, viewport.FirstRowIndex);
        //    Assert.Equal(28, viewport.LastRowIndex);
        //}

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
