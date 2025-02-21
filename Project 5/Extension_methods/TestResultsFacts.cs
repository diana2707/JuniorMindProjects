using System;

namespace Extension_methods
{
    public class TestResultsFacts
    {
        [Fact]
        public void TestResultsClassExists()
        {
            var results = new TestResults();
            Assert.IsType<TestResults>(results);
        }

        [Fact]
        public void CanSetAndGetId()
        {
            var results = new TestResults();
            results.Id = "10";
            Assert.Equal("10", results.Id);
        }

        [Fact]
        public void CanSetAndGetFamilyId()
        {
            var results = new TestResults();
            results.FamilyId = "50";
            Assert.Equal("50", results.FamilyId);
        }

        [Fact]
        public void CanSetAndGetScore()
        {
            var results = new TestResults();
            results.Score = 80;
            Assert.Equal(80, results.Score);
        }
    }
}
