using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace laba1
{
    public class CEnemyTemplateConverter : JsonConverter<CEnemyTemplate>
    {
        public override CEnemyTemplate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                if (!root.TryGetProperty("$type", out var typeProp))
                    throw new JsonException("Нет поля $type");

                string type = typeProp.GetString();
                string json = root.GetRawText();

                // Вот тут мы понимаем, какой тип восстанавливать
                switch (type)
                {
                    case "armored": return JsonSerializer.Deserialize<CArmoredEnemyTemplate>(json, options);
                    case "healing": return JsonSerializer.Deserialize<CHealingEnemyTemplate>(json, options);
                    default: return JsonSerializer.Deserialize<CNormalEnemyTemplate>(json, options);
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, CEnemyTemplate value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Записываем дискриминатор $type
            string type = "normal";
            if (value is CArmoredEnemyTemplate) type = "armored";
            else if (value is CHealingEnemyTemplate) type = "healing";
            writer.WriteString("$type", type);

            // Записываем остальные свойства
            writer.WriteString("Name", value.Name);
            writer.WriteString("IconName", value.IconName);
            writer.WriteNumber("BaseLife", value.BaseLife);
            writer.WriteNumber("LifeModifier", value.LifeModifier);
            writer.WriteNumber("BaseGold", value.BaseGold);
            writer.WriteNumber("GoldModifier", value.GoldModifier);
            writer.WriteNumber("SpawnChance", value.SpawnChance);

            if (value is CArmoredEnemyTemplate armored)
                writer.WriteNumber("Armor", armored.Armor);
            if (value is CHealingEnemyTemplate healing)
                writer.WriteNumber("HealPercent", healing.HealPercent);

            writer.WriteEndObject();
        }
    }
}