using ExerciseOneAM.Contracts;

namespace ExerciseOneAM.UnitTests;

public class GetTopScoringSuggestionsTests
{
    public static IEnumerable<object[]> TestData_Get_top_scoring_suggestions()
    {
        yield return new object[]
        {
            "gros",
            new[]
            {
                ("go", -1),
                ("gros", 0),
                ("gro", -1),
                ("graisse", 2),
                ("agressif", 1),
                ("gris", 1),
                ("gras", 1),
                ("grosse", 0),
                ("ros", -1)
            },
            3,
            new[] { "gros", "grosse", "gras" }
        };
    }

    [Theory]
    [MemberData(nameof(TestData_Get_top_scoring_suggestions))]
    public void Get_top_scoring_suggestions(
        string givenTerm,
        (string Suggestion, int Score)[] givenScoredSuggestions,
        int givenNumberOfSuggestions,
        string[] expectedTopSuggestions
    )
    {
        var suggestionScorer = new SuggestionScorerMock(givenScoredSuggestions);
        var subject = new GetTopScoringSuggestions(suggestionScorer);

        var actualTopSuggestions = subject.Get(
            givenTerm,
            givenScoredSuggestions.Select(scoredSuggestion => scoredSuggestion.Suggestion),
            givenNumberOfSuggestions
        );

        Assert.Equivalent(expectedTopSuggestions, actualTopSuggestions, strict: true);
    }

    private class SuggestionScorerMock : ISuggestionScorer
    {
        private readonly (string Suggestion, int Score)[] _scoredSuggestions;

        public SuggestionScorerMock((string Suggestion, int Score)[] scoredSuggestions)
        {
            _scoredSuggestions = scoredSuggestions;
        }

        public int GetScore(string term, string suggestion)
        {
            return _scoredSuggestions.First(scoredSuggestion => scoredSuggestion.Suggestion == suggestion).Score;
        }
    }
}
