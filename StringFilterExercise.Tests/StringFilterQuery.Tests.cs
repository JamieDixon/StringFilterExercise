﻿namespace StringFilterExercise.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using StringFilterExercise.Queries;

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
            public void ThrowsExceptionWithNullInput()
            {
                Assert.Throws<Exception>(() => this.stringFilterQuery.Invoke(null).ToList());
            }
            
            [Test]
            public void ThrowsExceptionWithEmptyInput()
            {
                Assert.Throws<Exception>(() => this.stringFilterQuery.Invoke(Enumerable.Empty<string>().ToList()).ToList());
            }

            [Test]
            public void ReturnsExpectedValuesGivenSampleInput()
            {
                var expected = new List<string> { "albums", "barely", "befoul", "convex", "hereby", "jigsaw", "tailor", "weaver" };
                var result = this.stringFilterQuery.Invoke(this.sampleInput).ToList();
                Assert.AreEqual(expected, result);
            }
        }
    }
}