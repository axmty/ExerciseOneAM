namespace ExerciseOneAM.Contracts;

/// <summary>
/// Defines a method to compute the score of a suggestion against a term.
/// </summary>
public interface ISuggestionScorer
{
    /// <summary>
    /// Gets the score of the given suggestion.
    /// </summary>
    /// <returns>
    /// Returns -1 if and only if the suggestion is not scorable.
    /// Otherwise returns the suggestion score (the lower the better).
    /// </returns>
    int GetScore(string term, string suggestion);
}
