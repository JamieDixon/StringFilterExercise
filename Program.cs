namespace StringFilterExercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using StringFilterExercise.Queries;

    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "al", "albums", "aver", "bar", "barely", "be", "befoul", "bums", "by", "cat", "con", "convex", "ely", "foul", "here", "hereby", "jig", "jigsaw", "or", "saw", "tail", "tailor", "vex", "we", "weaver", "Jamiee" };
            
            // This would normally be injected via an interface.
            var stringFilter = new StringFilterQuery();

            var output = stringFilter.Invoke(input);
            output.ToList().ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
