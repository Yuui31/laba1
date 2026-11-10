using System;
using System.Collections.Generic;
using System.Text;

namespace laba1
{
    public class CHealingEnemyTemplate : CEnemyTemplate
    {
        public double HealPercent { get; set; } // Особенность
        public CHealingEnemyTemplate(string name, string iconName, int baseLife, double lifeModifier, int baseGold, double goldModifier, double spawnChance, double healPercent)
            : base(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance) { HealPercent = healPercent; }
    }
}