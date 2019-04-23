using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {
        // ||| WEATHER |||
        public static Unit Gremist
        {
            get
            {
                return CommonFunc.weatherMage("Gremist", "Spawn Torrential Rain, Clear Skies or Bloodcurdling Roar.", Clan.skellige,
                    SpawnSpecial.TorrentialRain, SpawnSpecial.ClearSkies, SpawnSpecial.BloodcurdlingRoar, Tag.support);
            }
        }
        public static Unit IdaEmeanaepSivney
        {
            get
            {
                return CommonFunc.weatherMage("Ida Emean aep Sivney", "Spawn Impenetrable Fog, Clear Skies or Alzur's Thunder.", Clan.scoetaels,
                    SpawnSpecial.ImpenetrableFog, SpawnSpecial.ClearSkies, SpawnSpecial.AlzursThunder, Tag.elf, Tag.mage);
            }
        }
        public static Unit Dethmold
        {
            get
            {
                return CommonFunc.weatherMage("Dethmold", "Spawn Torrential Rain, Clear Skies or Alzur's Thunder.", Clan.northen,
                    SpawnSpecial.TorrentialRain, SpawnSpecial.ClearSkies, SpawnSpecial.AlzursThunder, Tag.kaedwen, Tag.mage);
            }
        }
        public static Unit Vanhemar
        {
            get
            {
                return CommonFunc.weatherMage("Vanhemar", "Spawn Biting Frost, Clear Skies or Shrike.", Clan.nilfgaard,
                    SpawnSpecial.BitingFrost, SpawnSpecial.ClearSkies, SpawnSpecial.Shrike, Tag.mage);
            }
        }
        public static Unit Vaedermakar
        {
            get
            {
                return CommonFunc.weatherMage("Vaedermakar", "Spawn Biting Frost, Impenetrable Fog or Alzur's Thunder.", Clan.neutral,
                    SpawnSpecial.BitingFrost, SpawnSpecial.ImpenetrableFog, SpawnSpecial.AlzursThunder, Tag.mage);
            }
        }
    }
}
