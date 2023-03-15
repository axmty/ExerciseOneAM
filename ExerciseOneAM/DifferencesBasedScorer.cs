using ExerciseOneAM.Contracts;

namespace ExerciseOneAM;

/// <summary>
/// Suggestion scorer that computes the score of a suggestion, based on the number of characters to replace to retrieve the term.
/// </summary>
public class DifferencesBasedScorer : ISuggestionScorer
{
    public int GetScore(string term, string suggestion)
    {
        if (suggestion.Length < term.Length)
        {
            return -1;
        }

        return Enumerable
            .Range(0, suggestion.Length - term.Length + 1)
            .Min(startIndex => CountDifferencesInSuggestionSlice(term, suggestion.Substring(startIndex, term.Length)));
    }

    private static int CountDifferencesInSuggestionSlice(string term, string suggestionSlice)
    {
        return suggestionSlice.Zip(term).Count(chars => chars.First != chars.Second);
    }
}
