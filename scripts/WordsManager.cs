﻿using System;
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
   public static Word[] Words = new Word[2];

   static WordsManager()
   {
      using (FileStream fs = new FileStream("Words.json", FileMode.OpenOrCreate))
      {
         Words = JsonSerializer.Deserialize<Word[]>(fs, _option);
      }
      if (Words == null) Reset();
   }
   /// <summary>
   /// Saves words state to Words.Json
   /// </summary>
   public static async void SaveAsync()
   {
      using (FileStream fs = new FileStream("Words.json", FileMode.OpenOrCreate))
      {
          await JsonSerializer.SerializeAsync<Word[]>(fs, Words,_option);
      }
   }
   public static void Reset()
   {
      int i = 0;
      Words[i] = new Word("кудзеркі", "кудряшки", "Завивающийся тип волос", i);
      i++;
      Words[i] = new Word("сланечнік", "подсолнух", "Цветок с желтыми листями, из него получают семечки", i);
      i++;
      SaveAsync();
   }
}  