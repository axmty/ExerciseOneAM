using ExerciseOneAM.Contracts;

namespace ExerciseOneAM;

/// <summary>
/// Chooses top-scoring suggestions based on each suggestion score.
/// </summary>
public class GetTopScoringSuggestions : IGetSuggestions
{
    private readonly ISuggestionScorer _suggestionScorer;

    public GetTopScoringSuggestions(ISuggestionScorer suggestionScorer)
    {
        _suggestionScorer = suggestionScorer;
    }

    public IEnumerable<string> Get(string term, IEnumerable<string> choices, int numberOfSuggestions)
    {
        if (numberOfSuggestions < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numberOfSuggestions),
                "The requested number of suggestions must be strictly positive."
            );
        }

        var choiceScores = choices.Select(
            choice => ((int Score, string Choice))(_suggestionScorer.GetScore(term, choice), choice)
        );

        var scoredChoices = choiceScores.Where(scoredChoice => scoredChoice.Score >= 0);

        var orderedScoredChoices = scoredChoices
            .OrderBy(scoredChoice => scoredChoice.Score)
            .ThenBy(scoredChoice => Math.Abs(scoredChoice.Choice.Length - term.Length))
            .ThenBy(scoredChoice => scoredChoice.Choice);

        return orderedScoredChoices.Take(numberOfSuggestions).Select(scoredChoice => scoredChoice.Choice);
    }
}
