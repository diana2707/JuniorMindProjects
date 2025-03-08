using System;

namespace LinqExercises
{
    public class ExercisesFacts
    {
        [Fact]
        public void GeneratePatternReturnDescendingRangeFromNTo1()
        {
            int n = 1;

            var result = Exercises.GeneratePattern(1);

            List<int> expected = [1];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratePatternReturnDescendingRangesForNNumberOfRows()
        {
            int n = 2;

            var result = Exercises.GeneratePattern(n);

            List<int> expected = [2, 2, 1, 1, 2, 1];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratePatternReturnDescendingRangesWithNumbersRepeatingAcordingWithRowIndex()
        {
            int n = 3;

            var result = Exercises.GeneratePattern(n);

            List<int> expected = [3, 3, 3, 2, 2, 2, 1, 1, 1, 3, 3, 2, 2, 1, 1, 3, 2, 1];

            Assert.Equal(expected, result);
        }
    }
}
