using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Engines.Events
{
    public static class BrittishEventDrops
    {
        private static readonly int EventHue = 1175;
        private static readonly string EventPrefix = "Lord Brittishness'";

        public static void TryDropFrom(Mobile killer, double chance)
        {
            if (killer is BaseCreature pet && pet.Controlled && pet.ControlMaster is PlayerMobile master)
            {
                killer = master;
            }

            if (killer is PlayerMobile player && Utility.RandomDouble() <= chance)
            {
                CreateEventItemDrop(player);
            }
        }

        public static void CreateEventItemDrop(Mobile from)
        {
            if (from is not PlayerMobile player)
                return;

            Item drop = CreateRandomEventItem();
            if (drop != null)
                player.AddToBackpack(drop);
        }

        private static Item CreateRandomEventItem()
        {
            Type[] armorTypes = new Type[]
            {
                typeof(LeatherCap), typeof(LeatherChest), typeof(LeatherLegs),
                typeof(LeatherGloves), typeof(LeatherArms), typeof(LeatherGorget),

                typeof(StuddedChest), typeof(StuddedLegs), typeof(StuddedGloves), typeof(StuddedArms),
                typeof(StuddedGorget),

                typeof(BoneChest), typeof(BoneLegs), typeof(BoneArms), typeof(BoneGloves),
                typeof(BoneHelm), typeof(RingmailChest), typeof(RingmailLegs),
                typeof(RingmailArms), typeof(RingmailGloves),

                typeof(ChainChest), typeof(ChainLegs), typeof(ChainCoif),
                typeof(FemaleLeatherChest), typeof(FemaleStuddedChest), typeof(FemalePlateChest)
            };

            Type[] weaponTypes = new Type[]
            {
                typeof(Broadsword), typeof(Cutlass), typeof(Katana),
                typeof(Longsword), typeof(Scimitar), typeof(VikingSword),

                typeof(Dagger), typeof(ShortSpear), typeof(Spear),
                typeof(Pitchfork), typeof(WarFork), typeof(Kryss),

                typeof(Axe), typeof(BattleAxe), typeof(DoubleAxe),
                typeof(ExecutionersAxe), typeof(LargeBattleAxe),
                typeof(TwoHandedAxe), typeof(WarAxe), typeof(Hatchet),

                typeof(Club), typeof(HammerPick), typeof(Mace),
                typeof(Maul), typeof(WarHammer), typeof(WarMace),

                typeof(Bow), typeof(Crossbow), typeof(HeavyCrossbow),

                typeof(Halberd), typeof(Bardiche), typeof(Scythe),
                typeof(QuarterStaff), typeof(BlackStaff)
            };

            Type[] pool = Utility.RandomBool() ? armorTypes : weaponTypes;
            Type chosen = pool[Utility.Random(pool.Length)];

            if (Activator.CreateInstance(chosen) is not Item item)
                return null;

            item.Hue = EventHue;
            item.Name = $"{EventPrefix} {item.ItemData.Name}";
            item.LootType = LootType.Regular;

            return item;
        }
    }
}