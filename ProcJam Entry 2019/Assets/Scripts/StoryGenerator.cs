using UnityTracery;

public class StoryGenerator 
{
	private string grammar;
	private TraceryGrammar traceryGrammar;

	public StoryGenerator(string grammar)
	{
		this.grammar = grammar;
		traceryGrammar = new TraceryGrammar(grammar);
	}

	public void UpdateGrammar(string grammar)
	{
		this.grammar = grammar;
		traceryGrammar = new TraceryGrammar(grammar);
	}

	public string GenerateStory()
	{
		return traceryGrammar.Parse("#origin#");
	}
}
