
using System;

public class Word
{
   public bool Active = true;
   public readonly string WordBY;
   public readonly string Translation;
   public readonly string Description;
   public readonly int Number;
   public sbyte WordLvl { get; private set; } = 1;
   public sbyte _exp { get; private set; } = 0;


   public Word(string WordBy, string translation, string description, int number)
   {
      WordBY = WordBy;
      Translation = translation;
      Description = description;
      Number = number;
   }

   private void AnswerActions(bool Correct)
   {
      int answer = Correct ? 1 : (int)-Math.Sqrt(WordLvl * 10.0);
      _exp += (sbyte)answer;
      if (_exp > 40) _exp = 40;
      if (_exp < 0) _exp = 0;
      WordLvl = (sbyte)(_exp / 10 + 1);
   }
   public void RightAnswer()
   {
      AnswerActions(true);
   }

   public void WrongAnswer()
   {
      AnswerActions(false);
   }
}
