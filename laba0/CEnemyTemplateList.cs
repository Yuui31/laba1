using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace laba1
{
    public class CEnemyTemplateList
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
            CEnemyTemplate newEnemy = new CEnemyTemplate(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance);
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
        public void SaveToJson(string path)
        {
            // список enemies в JSONстроку
            string json = JsonSerializer.Serialize(enemies);

            // записываем строку в файл по указанному пути
            File.WriteAllText(path, json);
        }
        public void LoadFromJson(string path)
        {
            if (!File.Exists(path))
                return;

            // читаем содержимое файла
            string json = File.ReadAllText(path);

            // JSONстроку обратно в список
            // Если файл гавно —  null, тогда + пустой список
            enemies = JsonSerializer.Deserialize<List<CEnemyTemplate>>(json)
                      ?? new List<CEnemyTemplate>();
        }
    }
}