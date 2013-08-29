namespace StringFilterExercise.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using StringFilterExercise.Queries;

    public class StringFilterQuery : IQuery<IList<string>, IEnumerable<string>>
    {
        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<string> Invoke(IList<string> input)
        {
            if (input == null || input.Any() == false)
            {
                throw new Exception("You must pass a non-empty, non-null parameter to the Parse method.");
            }

            // Store the input in a lookup with word length as the key.
            var lookup = input.ToLookup(x => x.Length);

            // Using a hashset for constant time lookup when doing .Contains() (O(1)). 
            // Could have used a list but since we've got a base count of items > 5, HashSet is probably more performant.
            var possibleWords = new HashSet<string>(input.Where(x => x.Length == 6));

            // This is the expanded non-linq version. I've included two Linq versions below for comparison.
            // I'd use the Linq version in production code but it's sometimes nice to see it written long(er) form in a test too.
            foreach (var word in input)
            {
                // Get all the right hand segments that would create a combined word + part length equal to 6.
                var possParts = lookup[6 - word.Length];

                foreach (var part in possParts)
                {
                    var comb = word + part;

                    if (possibleWords.Contains(comb))
                    {
                        yield return comb;
                    }
                }
            }

            /*
             LINQ variations of the foreach above.
             1)
             return from word in input let possParts = lookup[6 - word.Length] from part in possParts select word + part into comb where possibleWords.Contains(comb) select comb;
             
             2)
             return input
				.SelectMany(w => lookup[6 - w.Length], (w, y) => new { Value = w + y })
				.Where(x => possibleWords.Contains(x.Value))
				.Select(x => x.Value);
             */
        }
    }
}
