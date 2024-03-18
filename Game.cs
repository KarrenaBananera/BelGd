using Godot;
using System;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

public partial class Game : Control
{
	public Word CurrentWord { get; private set; }
   public bool RuOrBy;
	Node InputHistory;
	PackedScene InputResponse;
   private Random _random = new Random();
   private string _word;

   public override void _Ready()
	{
		InputHistory = GetNode(@"BackGround/MarginContainer/Rows/GameInfo/ScrollContainer/History"); 
		InputResponse = GD.Load<PackedScene>("res://InputResponse.tscn");
      var init = GetNode(@"BackGround/MarginContainer/Rows/GameInfo/ScrollContainer/History/WhatToType");
      CurrentWord = GetWord();

      init.Call("SetText", _word);
   }

   public Word GetWord()
   {
      var word = WordsManager.Words[_random.Next(WordsManager.Words.Length)];
      var chance = _random.Next(101);
      var needChance = 100 - word.WordLvl * 12;
      var activeFactor = word.Active ? 1 : 10000;
      needChance += activeFactor;
      if (chance < needChance) return GetWord();
      var random = _random.Next(2);
      if (random == 1) RuOrBy = true;
      else RuOrBy = false;
      GD.Print(random);
      _word = RuOrBy ? word.WordBY : word.Translation;
      return word;
   }
   
   
   private string needWord()
   {
      if(RuOrBy)
         return CurrentWord.Translation.ToLower();
      else
         return CurrentWord.WordBY;
   }
	private void OnTextEnter(string text)
	{
      string response;
      string trueAnswer = needWord();
      string needAnswer = trueAnswer.Replace('і', 'и').ToLower().Replace('ў', 'у');
      var filteredAnswer = text.Replace('і', 'и').ToLower().Replace('ў', 'у');
      if (filteredAnswer.Equals(needAnswer))
      {
         response = " ✓ " + trueAnswer;
         CurrentWord.RightAnswer();

      }
      else
      {
         response = " ✗ " + text + " ✓ " + trueAnswer;
         CurrentWord.WrongAnswer();
      }
      var inputResponse = InputResponse.Instantiate();
      CurrentWord = GetWord();
      string word = RuOrBy ? CurrentWord.WordBY : CurrentWord.Translation;
      inputResponse.Call("SetText", word,response);
		InputHistory.AddChild(inputResponse);
	}

   private void OnClosing()
   {
      WordsManager.SaveAsync();
   }
}
