using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace MediaTracker.UI.Models
{
    public class MediaManager
    {
        // Single shared instance so everything uses the same data
        public static MediaManager Instance = new MediaManager();

        // where the data gets saved
        private string filePath = "data.json";

        // Stores items by their group
        public Dictionary<string, List<MediaItem>> MediaByType { get; set; } = new();
        // runs once the app starts
        private MediaManager() 
        {
            LoadFromFile();
        }
        // Adds new item to list
        public void AddItem(MediaItem item)
        {
            // Don't add if name or type is empty
                if (string.IsNullOrWhiteSpace(item.Name) || string.IsNullOrWhiteSpace(item.Type))
                
                    return;

                if (!MediaByType.ContainsKey(item.Type))
                {
                // Add the item
                    MediaByType[item.Type] = new List<MediaItem>();
                }
                MediaByType[item.Type].Add(item);
            // Save after adding
                SaveToFile();
            }

        // gets items by type
            public List<MediaItem> GetItemsByType(string type)
        {
            if (MediaByType.ContainsKey(type))
            {
                return MediaByType[type];
            }

            return new List<MediaItem>();

        }
        // Removes an item
        public void RemoveItem(MediaItem item)
        {
            if (MediaByType.ContainsKey(item.Type))
            {
                MediaByType[item.Type].Remove(item);

                SaveToFile();
            }
        }

        // toggles completed status
    public void ToggleCompleted(MediaItem item)
        {
            item.IsCompleted = !item.IsCompleted;

            SaveToFile();
        }

        // Saves information to json file
        public void SaveToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(MediaByType);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error saving the file: " + ex.Message);
            }
        }
        // Loads the data from the file//
    public void LoadFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    MediaByType = JsonSerializer.Deserialize<Dictionary<string, List<MediaItem>>>(json)
                        ?? new Dictionary<string, List<MediaItem>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
            }
        }
    }
}
