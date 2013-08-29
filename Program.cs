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

            stringFilter.Invoke(input).ToList().ForEach(Console.WriteLine);
            /*
             // Quick performance test over 10,000 iterations.
             var now = DateTime.Now;

            for (var i = 0; i <= 100000; i++)
            {
                stringFilter.Invoke(input).ToList();
            }

            Console.WriteLine(now - DateTime.Now);*/
            Console.ReadLine();
        }
    }
}
