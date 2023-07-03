## Code test assumptions and restrictions

Search text
* The search text is matched case-sensitive.

* The search text is trimmed and will contain at least one non-whitespace character.

* The search text is treated as a substring of a line. May be a full word but is not required to be.

* The search text is matched at most one time with any given character in the file. Matching "aa" in "aaaa" yields two matches, not three overlapping matches.

\
Error handling
* Error handling of file existence, addressing, permissions and so on is done mainly by .NET. I don't see any point in letting the program manage each exception.

* File encoding is not taken into account since it was not mentioned. I guess the worst case is getting the wrong count (likely zero).

\
Test scope
* The test scope covers the business logic, not the console app's input and output. I have kept the console app itself as slimmed as possible and kept the business logic free from Console.WriteLine().

* Even though the solution is cross-platform, Windows is the test scope for file naming.

\
Optimization
* Files are not enormous. Accuracy and error handling has been prioritized over performance. No optimization or parallelization has been done, but are possible next steps. Perhaps using compiled regex and PLINQ.