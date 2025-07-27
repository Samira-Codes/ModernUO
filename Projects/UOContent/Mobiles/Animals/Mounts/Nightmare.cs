using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class Nightmare : BaseMount
    {
        public override string DefaultName => "a nightmare";

        [Constructible]
        public Nightmare() : base(0x74, 0x3EA7, AIType.AI_Mage)
        {
            BaseSoundID = Core.AOS ? 0xA8 : 0x16A;

            // Publish 97
            if (Core.TOL)
            {
                if (Utility.RandomDouble() < 0.3)
                {
                    SetStr(296, 315);
                    ControlSlots = 2;
                }
                else
                {
                    SetStr(496, 525);
                    ControlSlots = 3;
                }
            }
            else
            {
                SetStr(496, 525);
                ControlSlots = 2;
            }

            SetDex(86, 105);
            SetInt(86, 125);

            SetHits(298, 315);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Fire, 40);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.EvalInt, 10.4, 50.0);
            SetSkill(SkillName.Magery, 10.4, 50.0);
            SetSkill(SkillName.MagicResist, 85.3, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 80.5, 92.5);

            Fame = 14000;
            Karma = -14000;

            VirtualArmor = 60;

            Tamable = true;
            MinTameSkill = 95.1;

            switch (Utility.Random(3))
            {
                case 0:
                    {
                        Body = 116;
                        ItemID = 16039;
                        break;
                    }
                case 1:
                    {
                        Body = 178;
                        ItemID = 16041;
                        break;
                    }
                case 2:
                    {
                        Body = 179;
                        ItemID = 16055;
                        break;
                    }
            }
            // 🎨 Hue rarity (flipped logic)
            int roll = Utility.Random(1000); // 0–999
            
                if (5 > roll)
                        Hue = Utility.RandomList(1482, 1499); // 0.5% chance: ultra rare
                    else if (45 > roll)
                        Hue = Utility.RandomList(1496, 2759); // 4.0% chance: uber rare
                    else if (145 > roll)
                        Hue = Utility.RandomList(2726, 2764, 2753); // 10.0% chance: rare
                    else if (325 > roll)
                        Hue = Utility.RandomList(1209, 2761, 2411); // 18.0% chance: common
                    else
                        Hue = 0; // 67.5% chance: default
                        
            PackItem(new SulfurousAsh(Utility.RandomMinMax(3, 5)));
        }

        public override int StepsMax => 6400;
        public override string CorpseName => "a nightmare corpse";
        public override int Meat => 5;
        public override int Hides => 10;
        public override HideType HideType => HideType.Barbed;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;

        private static MonsterAbility[] _abilities = { MonsterAbilities.FireBreath };
        public override MonsterAbility[] GetMonsterAbilities() => _abilities;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override int GetAngerSound()
        {
            if (!Controlled)
            {
                return 0x16A;
            }

            return base.GetAngerSound();
        }
    }
}
