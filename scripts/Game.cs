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
   VScrollBar scrollBar;
   
   private Random _random = new Random();
   private string _word;
   double maxScroll = 0;
   public override void _Ready()
	{
		InputHistory = GetNode(@"BackGround/MarginContainer/Rows/GameInfo/ScrollContainer/History"); 
		InputResponse = GD.Load<PackedScene>("res://InputResponse.tscn");
      var init = GetNode(@"BackGround/MarginContainer/Rows/GameInfo/ScrollContainer/History/WhatToType");
      var node = GetNode<ScrollContainer>(@"BackGround/MarginContainer/Rows/GameInfo/ScrollContainer");
      scrollBar = node.GetVScrollBar();
      Callable callable = new Callable(this, MethodName.scrollChanged);
      scrollBar.Connect("changed",callable);
      CurrentWord = GetWord();

      init.Call("SetText", _word);
   }

   private void scrollChanged()
   { 
      if (maxScroll != scrollBar.MaxValue)
      {
         maxScroll = scrollBar.MaxValue;
         scrollBar.Value = maxScroll;
      }
   }

   public Word GetWord()
   {
      var word = WordsManager.Words[_random.Next(WordsManager.Words.Count)];
      var chance = _random.Next(101);
      var needChance = 100 - word.WordLvl * 12;
      var activeFactor = word.Active ? 1 : 10000;
      needChance += activeFactor;
      if (chance < needChance) return GetWord();
      var random = _random.Next(2);
      if (random == 1) RuOrBy = true;
      else RuOrBy = false;
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
      string needAnswer = trueAnswer.Replace('і', 'и').ToLower().Replace('ў', 'у').Replace('ё','е');
      var filteredResponse = text.Replace('і', 'и').ToLower().Replace('ў', 'у').Replace('ё','е');
      if (filteredResponse.Equals(needAnswer))
      {
         response = " ✓ " + trueAnswer;
         CurrentWord.RightAnswer();

      }
      else
      {
         response = " ✗ " + text + " ✓ " + trueAnswer;
         CurrentWord.WrongAnswer();
      }
      CurrentWord = GetWord();
      GD.Print(needWord());
      string lang = RuOrBy ? "B " : "R ";
      var inputResponse = InputResponse.Instantiate();
      inputResponse.Call("SetText", lang + _word, response);
		InputHistory.AddChild(inputResponse);

      scrollBar.Value = scrollBar.MaxValue;
      if (response.Equals("save")) WordsManager.Save();
   }

   private void OnClosing()
   {
      WordsManager.SaveAsync();
   }
}
