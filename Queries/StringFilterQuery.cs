namespace StringFilterExercise.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            // Sanity check input. Would normally do this kind of thing with Code Contracts.
            if (input == null || input.Any() == false)
            {
                throw new Exception("You must pass a non-empty, non-null parameter to the Parse method.");
            }

            if (input.Count < 3)
            {
                yield break;
            }

            // Store the input in a lookup with word length as the key. Lookup is more performant than some other solutions such as doing a Where on the input.
            var lookup = input.ToLookup(x => x.Length);

            // Using a hashset for constant time lookup when doing .Contains() (O(1)). 
            // Could have used a list but since we've got a base count of items > 5, HashSet is probably more performant.
            var possibleWords = new HashSet<string>(lookup[6]);

            // This is the expanded non-linq version. I've included two Linq versions below for comparison.
            // I'd use the Linq version in production code but it's sometimes nice to see it written long(er) form in a test too.
            // I could have also looped over the 6 char words instead of all of them and output based on whether any combination makes the current word.
            foreach (var word in input)
            {
                // Copy word variable so we don't access foreach variable in closure.
                var safeWord = word;

                // Get all the right hand segments that would create a combined word + part length equal to 6.
                var possParts = lookup[6 - word.Length].ToList();

                // This one is a bit of an edge case but I thought I'd include it since I'd written the test for it already.
                // We want to make sure that a segment isn't appended to itself to form a combination that's also a valid word
                // whilst at the same time, making sure that if a segment apears twice, and its combination is a valid word, it passes.
                var indexOfCurrentWord = possParts.FindIndex(x => x == safeWord);
                
                if (indexOfCurrentWord >= 0)
                {
                    possParts.RemoveAt(indexOfCurrentWord);    
                }
                

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
            LINQ variations of the foreach above:
              1) return from word in input let possParts = lookup[6 - word.Length] from part in possParts select word + part into comb where possibleWords.Contains(comb) select comb;
             
              2) return input
                          .SelectMany(w => lookup[6 - w.Length], (w, y) => new { Value = w + y })
                          .Where(x => possibleWords.Contains(x.Value))
                          .Select(x => x.Value);
            */
        }
    }
}
