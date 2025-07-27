using ModernUO.Serialization;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class Ridgeback : BaseMount
    {
        public override string DefaultName => "a ridgeback";

        [Constructible]
        public Ridgeback() : base(187, 0x3EBA, AIType.AI_Animal, FightMode.Aggressor)
        {
            BaseSoundID = 0x3F3;

            SetStr(201, 300);
            SetDex(66, 85);
            SetInt(61, 100);

            SetHits(121, 180);

            SetDamage(3, 4);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Poison, 25);

            SetResistance(ResistanceType.Physical, 35, 40);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 40);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Anatomy, 45.1, 55.0);
            SetSkill(SkillName.MagicResist, 45.1, 55.0);
            SetSkill(SkillName.Tactics, 45.1, 55.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);

            Fame = 300;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 83.1;

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
        }

        public override int StepsMax => 4480;
        public override string CorpseName => "a ridgeback corpse";

        public override int Meat => 1;
        public override int Hides => 12;
        public override HideType HideType => HideType.Spined;
        public override FoodType FavoriteFood => FoodType.FruitsAndVeggies | FoodType.GrainsAndHay;

        public override bool OverrideBondingReqs() => true;

        public override double GetControlChance(Mobile m, bool useBaseSkill = false) => 1.0;
    }
}
