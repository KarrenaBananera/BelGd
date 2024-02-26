using Godot;
using System;

public partial class TextToType : TextEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

   public override void _Input(InputEvent @event)	
   {
      if (@event.IsActionPressed("Enter"))
      {
         this.Text = this.Text.ToLower().Trim();
			if (String.Equals(this.Text, "reset"))
			{
				GD.Print("done");
			}
         GD.Print(WordsManager.Words[0].Translation);
      }
   }
}