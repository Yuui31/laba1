using System;
using System.Collections.Generic;
using System.Text;

namespace laba1
{
    public class CArmoredEnemyTemplate : CEnemyTemplate
    {
        public int Armor { get; set; } // Особенность
        public CArmoredEnemyTemplate(string name, string iconName, int baseLife, double lifeModifier, int baseGold, double goldModifier, double spawnChance, int armor)
            : base(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance) { Armor = armor; }
    }
}