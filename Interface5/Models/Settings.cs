using System.IO;
using System.Text.Json;

namespace Interface5.Models
{
    public class Settings
    {
        public int Size { get; set; } = 10;
        public string PathStats { get; set; } = "C:\\Users\\pavel\\source\\repos\\Interface5\\Interface5\\stats.json";

        private static readonly string SettingsFilePath = "C:\\Users\\pavel\\source\\repos\\Interface5\\Interface5\\settings.json";

        public void Save()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }

        public static Settings Load()
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                return JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
            }

            return new Settings();
        }
    }
}
