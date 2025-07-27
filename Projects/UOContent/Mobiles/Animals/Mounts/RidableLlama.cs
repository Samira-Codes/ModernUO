using ModernUO.Serialization;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class RidableLlama : BaseMount
    {
        public override string DefaultName => "a ridable llama";

        [Constructible]
        public RidableLlama() : base(0xDC, 0x3EA6, AIType.AI_Animal, FightMode.Aggressor
        )
        {
            BaseSoundID = 0x3F3;

            SetStr(21, 49);
            SetDex(56, 75);
            SetInt(16, 30);

            SetHits(15, 27);
            SetMana(0);

            SetDamage(3, 5);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 15);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.2, 29.0);
            SetSkill(SkillName.Wrestling, 19.2, 29.0);

            Fame = 300;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
            if (Utility.RandomDouble() < 0.01)
            {
                Name = "an energy llama";
                Hue = 0x76; // Electric blue
                MinTameSkill = 68.3;

                // Swamp dragon–style stats
                SetStr(150, 160);
                SetDex(100, 130);
                SetInt(6, 10);

                SetHits(195, 247);
                SetDamage(2, 6);

                SetSkill(SkillName.MagicResist, 25.0, 40.0);
                SetSkill(SkillName.Tactics, 50.0, 60.0);
                SetSkill(SkillName.Wrestling, 50.0, 60.0);

                VirtualArmor = 20;
            }
        }
            
        public override int StepsMax => 2560;
        public override string CorpseName => "a llama corpse";

        public override int Meat => 1;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.FruitsAndVeggies | FoodType.GrainsAndHay;
    }
}
