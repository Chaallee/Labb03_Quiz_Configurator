using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Labb3.Models;

namespace Labb3.Services
{
    public class QuestionPackService
    {
        private readonly string _dataFolder;
        private readonly JsonSerializerOptions _jsonOptions;

        public QuestionPackService()
        {

            _dataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Labb3",
                "QuestionPacks");


            Directory.CreateDirectory(_dataFolder);


            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task SaveQuestionPackAsync(QuestionPack pack)
        {
            string fileName = GetSafeFileName(pack.PackName) + ".json";
            string filePath = Path.Combine(_dataFolder, fileName);


            string jsonString = JsonSerializer.Serialize(pack, _jsonOptions);


            await File.WriteAllTextAsync(filePath, jsonString);
        }


        public async Task<List<QuestionPack>> LoadAllQuestionPacksAsync()
        {
            var packs = new List<QuestionPack>();

            var jsonFiles = Directory.GetFiles(_dataFolder, "*.json");

            foreach (var filePath in jsonFiles)
            {
                try
                {
 
                    string jsonString = await File.ReadAllTextAsync(filePath);

                    var pack = JsonSerializer.Deserialize<QuestionPack>(jsonString, _jsonOptions);

                    if (pack != null)
                    {
                        packs.Add(pack);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading {filePath}: {ex.Message}");
                }
            }

            return packs;
        }

        public async Task DeleteQuestionPackAsync(string packName)
        {
            string fileName = GetSafeFileName(packName) + ".json";
            string filePath = Path.Combine(_dataFolder, fileName);

            await Task.Run(() =>
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            });
        }

        private string GetSafeFileName(string packName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                packName = packName.Replace(c, '_');
            }
            return packName;
        }

        public string GetDataFolderPath() => _dataFolder;
    }
}