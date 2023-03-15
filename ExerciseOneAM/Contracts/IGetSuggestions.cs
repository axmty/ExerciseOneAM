namespace ExerciseOneAM.Contracts;

/// <summary>
/// Defines a method to choose a given number of suggestions based on a term.
/// </summary>
public interface IGetSuggestions
{
    /// <summary>
    /// Chooses a given number of suggestions among a given list of choices.
    /// </summary>
    IEnumerable<string> Get(string term, IEnumerable<string> choices, int numberOfSuggestions);
}
