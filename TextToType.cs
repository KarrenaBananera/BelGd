using Godot;
using System;

public partial class TextToType : TextEdit
{
	public Word currentWord { get; set; }
	public override void _Ready()
	{
		currentWord = GetWord();
		GD.Print(currentWord.Translation);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
		
	public Word GetWord()
	{
		var random = new Random();
		var word = WordsManager.Words[random.Next(WordsManager.Words.Length)];
		var chance = random.Next(100);
		var needChance = 100 - word.WordLvl * 12;
		var activeFactor = word.Active ? 1 : 10000;
		needChance *= activeFactor;
		if (chance < needChance) return GetWord();
		return word;
	}

   public override void _Input(InputEvent @event)	
   {
      if (@event.IsActionPressed("Enter"))
      {
         this.Text = this.Text.ToLower().Trim();
			if (String.Equals(this.Text, currentWord.Translation))
			{
				currentWord.RightAnswer();
				GD.Print("Right");
            GD.Print((currentWord._exp % 10 + 1));
            GD.Print((sbyte)(currentWord._exp % 10 + 1));
         }
			else
			{
				currentWord.WrongAnswer();
				GD.Print("Wrong");
			}
			this.Text = "";
			currentWord = GetWord();
         GD.Print(currentWord.Translation);
      }
		if (@event.IsActionPressed("ui_cancel"))
		{
         GD.Print("done");
         WordsManager.SaveAsync();
		}
   }
}