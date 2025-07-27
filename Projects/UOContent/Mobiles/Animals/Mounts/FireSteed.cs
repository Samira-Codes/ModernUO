using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class FireSteed : BaseMount
    {
        public override string DefaultName => "a fire steed";

        [Constructible]
        public FireSteed() : base(0xBE, 0x3E9E, AIType.AI_Melee)
        {
            BaseSoundID = 0xA8;
            int roll = Utility.Random(1000); // 0–999

                if (5 > roll)
                    Hue = 2771; // 0.5% chance: ultra rare
                else if (20 > roll)
                    Hue = 1360; // 1.5% chance: uber rare
                else if (100 > roll)
                    Hue = Utility.RandomList(1359, 1257); // 8.0% chance: rare
                else if (300 > roll)
                    Hue = 1128; // 20.0% chance: common
                else
                    Hue = 0; // 70.0% chance: default

            SetStr(376, 400);
            SetDex(91, 120);
            SetInt(291, 300);

            SetHits(226, 240);

            SetDamage(11, 30);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 80);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 70, 80);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 100.0, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 20000;
            Karma = -20000;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 106.0;

            PackItem(new SulfurousAsh(Utility.RandomMinMax(151, 300)));
            PackItem(new Ruby(Utility.RandomMinMax(16, 30)));
        }

        public override string CorpseName => "a fire steed corpse";
        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Daemon | PackInstinct.Equine;

        private static MonsterAbility[] _abilities = { MonsterAbilities.FireBreath };
        public override MonsterAbility[] GetMonsterAbilities() => _abilities;

        public override void GenerateLoot()
        {
        }
    }
}
