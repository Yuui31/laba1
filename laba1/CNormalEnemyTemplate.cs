using System;
using System.Collections.Generic;
using System.Text;

namespace laba1
{
    public class CNormalEnemyTemplate : CEnemyTemplate
    {
        public CNormalEnemyTemplate(string name, string iconName, int baseLife, double lifeModifier, int baseGold, double goldModifier, double spawnChance)
            : base(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance) { }
    }
}