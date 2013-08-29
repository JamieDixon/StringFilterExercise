String Filter Exercise
====================

Interview Test:

Write a program in c# that can filter a list of strings.  It should return only six letter strings that are composed of two concatenated smaller strings that are also in the list.

For example, given the list: al, albums, aver, bar, barely, be, befoul, bums, by, cat, con, convex, ely, foul, here, hereby, jig, jigsaw, or, saw, tail, tailor, vex, we, weaver

The program should return: albums, barely, befoul, convex, hereby, jigsaw, tailor, weaver

Because these are a concatenation of two other strings:

* al + bums => albums
* bar + ely => barely
* be + foul => befoul
* con + vex => convex
* here + by => hereby
* jig + saw => jigsaw
* tail + or => tailor
* we + aver => weaver

Use of TDD techniques or unit testing is required.   Please comment your code with your thoughts about what is good about your solution, and what could be improved.
