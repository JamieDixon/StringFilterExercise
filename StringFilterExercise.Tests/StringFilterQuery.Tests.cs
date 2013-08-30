namespace StringFilterExercise.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using StringFilterExercise.Queries;
    /*
     I've used a test class structure that I picked up from Phil Haack a while back. It imitates the BDD style Given,When,Then.
     This makes for quite handy reading especially when you've got a mix of unit tests and integration tests (specflow tests for example).
     The nested class per method structure gives a nice separation when testing classes with multuiple public methods (repositories etc).
     */
    [TestFixture]
    public class Given_StringFilterQuery
    {
        private StringFilterQuery stringFilterQuery;
        private IList<string> sampleInput;

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.stringFilterQuery = new StringFilterQuery();
            this.sampleInput = new List<string> { "al", "albums", "aver", "bar", "barely", "be", "befoul", "bums", "by", "cat", "con", "convex", "ely", "foul", "here", "hereby", "jig", "jigsaw", "or", "saw", "tail", "tailor", "vex", "we", "weaver", "Jamiee" };
        }

        [TestFixture]
        public class When_Invoke_Executed : Given_StringFilterQuery
        {
            [Test]
            public void ThenThrowsExceptionWithNullInput()
            {
                Assert.Throws<Exception>(() => this.stringFilterQuery.Invoke(null).ToList());
            }

            [Test]
            public void ThenThrowsExceptionWithEmptyInput()
            {
                Assert.Throws<Exception>(() => this.stringFilterQuery.Invoke(Enumerable.Empty<string>().ToList()).ToList());
            }

            [Test]
            public void ThenReturnsEmptyCollectionWhenInputCountLessThan3()
            {
                var result = this.stringFilterQuery.Invoke(new List<string> { "Hello", "Wold" }).ToList();
                Assert.IsTrue(result.Count == 0);
            }

            [Test]
            public void ThenReturnsExpectedValuesGivenSampleInput()
            {
                var expected = new List<string> { "albums", "barely", "befoul", "convex", "hereby", "jigsaw", "tailor", "weaver" };
                var result = this.stringFilterQuery.Invoke(this.sampleInput).ToList();
                Assert.AreEqual(expected, result);
            }

            [Test]
            public void ThenReturnsExpectedValuesGivenReusableParts()
            {
                var sample = new List<string> { "al", "albums", "alvors", "aliens", "allens", "bums", "vors", "iens" };

                // We don't expect allens to be returned because its right hand portion (lens) is missing.
                var expected = sample.Where(x => x.Length == 6 && x != "allens");

                var result = this.stringFilterQuery.Invoke(sample).ToList();

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void ThenReturnsCorrectType()
            {
                Assert.IsInstanceOf<IEnumerable<string>>(this.stringFilterQuery.Invoke(this.sampleInput));
            }

            [Test]
            public void ThenWordSegmentNotAppendedToSelfToFormCombination()
            {
                var input = new List<string> { "heyhey", "hey", "ho" };
                var result = this.stringFilterQuery.Invoke(input).ToList();

                Assert.IsTrue(result.Count == 0);
            }

            // Edge case
            [Test]
            public void ThenDuplicateSegmentFormsValidCombination()
            {
                var input = new List<string> { "heyhey", "hey", "ho", "hey" };
                var result = this.stringFilterQuery.Invoke(input).ToList();

                Assert.IsTrue(result.Count > 0);
            }
        }
    }
}
