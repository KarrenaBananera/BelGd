using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

static class WordsManager
{
   private static JsonSerializerOptions _option = new JsonSerializerOptions { IncludeFields = true };
   public static List<Word> Words = new(20);

   static WordsManager()
   {
      try
      {
         using (FileStream fs = new FileStream("Words.json", FileMode.OpenOrCreate))
         {
            Words = JsonSerializer.Deserialize<List<Word>>(fs, _option);
         }
         if (Words == null) Reset();

      }
      catch { 
         Reset();
            }
   }
   /// <summary>
   /// Saves words state to Words.Json
   /// </summary>
   public static async void SaveAsync()
   {
      using (FileStream fs = new FileStream("Words.json", FileMode.OpenOrCreate))
      {
          await JsonSerializer.SerializeAsync<List<Word>>(fs, Words,_option);
      }
   }
   public static void Save()
   {
      using (FileStream fs = new FileStream("Words.json", FileMode.OpenOrCreate))
      {
         JsonSerializer.Serialize<List<Word>>(fs, Words, _option);
      }
   }
   public static void Reset()
   {
      Words.Add(new Word("сланечнік", "подсолнух"));
      Words.Add(new Word("бульба", "картофель"));
      Words.Add(new Word("ажына", "ежевика"));
      Words.Add(new Word("суніца", "земляника"));
      Words.Add(new Word("алешына", "ольха"));
      Words.Add(new Word("папараць-кветка", "папоротник"));
      Words.Add(new Word("хвоя", "сосна"));
      Words.Add(new Word("ружа", "роза"));
      Words.Add(new Word("касач", "ирис"));
      Words.Add(new Word("вярба", "ива"));
      Words.Add(new Word("таполя", "тополь"));
      Words.Add(new Word("шыпшына", "шиповник"));
      Words.Add(new Word("чарніца", "черника"));
      Words.Add(new Word("парэчка", "смородина"));
      Words.Add(new Word("чаромха", "черёмуха"));
      Words.Add(new Word("бэз", "сирень"));
      Words.Add(new Word("трыпутнік", "подорожник"));
      Words.Add(new Word("старасцень", "крестовник"));
      Words.Add(new Word("казялец", "лютик"));
      Words.Add(new Word("падбел", "мать-и-мачеха"));
      Words.Add(new Word("рамон", "ромашка"));
      Words.Add(new Word("крываўнік", "тысячелистник"));
      Save();
   }
}  