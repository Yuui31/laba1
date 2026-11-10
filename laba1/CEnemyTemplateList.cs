using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace laba1
{
    public class CEnemyTemplateList : ISaveList<CEnemyTemplate>
    {
        private List<CEnemyTemplate> enemies;

        public CEnemyTemplateList()
        {
            enemies = new List<CEnemyTemplate>();
        }
        public void AddEnemy(string name, string iconName, int baseLife, 
                            double lifeModifier, int baseGold, 
                            double goldModifier, double spawnChance)
        {
            CEnemyTemplate newEnemy = new CNormalEnemyTemplate(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance);
            enemies.Add(newEnemy);
        }
        public CEnemyTemplate GetEnemyByName(string name)
        {
            foreach (CEnemyTemplate enemy in enemies)
            {
                if (enemy.Name == name)
                {
                    return enemy;
                }
            }
            return null;
        }
        public CEnemyTemplate GetEnemyByIndex(int id)
        {
            if (id >= 0 && id < enemies.Count)
            {
                return enemies[id];
            }
            return null;
        }
        public void DeleteEnemyByName(string name)
        {
            CEnemyTemplate found = GetEnemyByName(name);
            if (found != null)
            {
                enemies.Remove(found);
            }
        }
        public void DeleteEnemyByIndex(int id)
        {
            if (id >= 0 && id < enemies.Count)
            {
                enemies.RemoveAt(id);
            }
        }
        public List<string> GetListOfEnemyNames()
        {
            List<string> names = new List<string>();
            foreach (CEnemyTemplate enemy in enemies)
            {
                names.Add(enemy.Name);
            }
            return names;
        }
        public void UpdateEnemy(string oldName, string newName, string iconName,
                       int baseLife, double lifeModifier, int baseGold,
                       double goldModifier, double spawnChance)
        {
            // Находим старого противника
            CEnemyTemplate found = GetEnemyByName(oldName);
            if (found == null)
                return;

            // Обновляем все его свойства
            found.Name = newName;
            found.IconName = iconName;
            found.BaseLife = baseLife;
            found.LifeModifier = lifeModifier;
            found.BaseGold = baseGold;
            found.GoldModifier = goldModifier;
            found.SpawnChance = spawnChance;
        }
        public void SaveToJson(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new CEnemyTemplateConverter()); // Подключаем конвертер
            string json = JsonSerializer.Serialize(enemies, options);
            File.WriteAllText(path, json);
        }

        public void LoadFromJson(string path)
        {
            if (!File.Exists(path)) return;
            string json = File.ReadAllText(path);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CEnemyTemplateConverter()); // Подключаем конвертер

            enemies = JsonSerializer.Deserialize<List<CEnemyTemplate>>(json, options) ?? new List<CEnemyTemplate>();
        }
    }
}