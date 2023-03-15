namespace ExerciseOneAM.UnitTests;

public class DifferencesBasedScorerTests
{
    [Theory]
    [InlineData("gros", "gros", 0)]
    [InlineData("gros", "grosse", 0)]
    [InlineData("gros", "gras", 1)]
    [InlineData("gros", "graisse", 2)]
    [InlineData("gros", "agressif", 1)]
    [InlineData("gros", "go", -1)]
    [InlineData("gros", "gro", -1)]
    [InlineData("gros", "ros", -1)]
    public void Compute_the_correct_score_of_a_suggestion_against_a_term(
        string givenTerm,
        string givenSuggestion,
        int expectedScore
    )
    {
        Assert.Equal(expectedScore, new DifferencesBasedScorer().GetScore(givenTerm, givenSuggestion));
    }
}
