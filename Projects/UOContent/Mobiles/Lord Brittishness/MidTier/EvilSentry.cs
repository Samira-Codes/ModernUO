using ModernUO.Serialization;
using Server.Engines.Events;
using Server.Items;

namespace Server.Mobiles.LordBrittishness.MidTier
{
    [SerializationGenerator(0, false)]
    public partial class EvilSentry : BaseCreature
    {
        [Constructible]
        public EvilSentry() : base(AIType.AI_Melee)
        {
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = Race.Human.RandomSkinHue();

            SetSpeed(0.15, 0.4);

            SetStr(200, 240);
            SetDex(90, 105);
            SetInt(60, 85);

            SetHits(200, 260);

            SetDamage(25, 28);
            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.Swords, 80.0, 95.0);
            SetSkill(SkillName.Tactics, 80.0, 95.0);
            SetSkill(SkillName.Anatomy, 80.0, 90.0);
            SetSkill(SkillName.MagicResist, 80.0, 95.0);

            Fame = 5500;
            Karma = -5500;

            var hue = 1157;

            var weapon = new Longsword
            {
                Movable = false,
                Hue = hue
            };
            AddItem(weapon);

            AddItem(new PlateHelm { Movable = false, Hue = hue });
            AddItem(new PlateChest { Movable = false, Hue = hue });
            AddItem(new PlateArms { Movable = false, Hue = hue });
            AddItem(new PlateGloves { Movable = false, Hue = hue });
            AddItem(new PlateLegs { Movable = false, Hue = hue });
            AddItem(new HeaterShield { Movable = false, Hue = hue });

            AddItem(new Shirt(){ Movable = false, Hue = 1175 });
            AddItem(new Boots(){ Movable = false, Hue = 1175 });

            // Mount
            new Ridgeback().Rider = this;
        }

        public override string CorpseName => "an evil sentry corpse";
        public override string DefaultName => "an evil sentry";

        public override bool AutoDispel => true;
        public override bool BardImmune => false;
        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;
        public override bool ShowFameTitle => false;

        public override int GetIdleSound() => 0x2CE;
        public override int GetDeathSound() => 0x2CC;
        public override int GetHurtSound() => 0x2D1;
        public override int GetAttackSound() => 0x2C8;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override bool OnBeforeDeath()
        {
            var mount = Mount;

            if (mount != null)
                mount.Rider = null;

            return base.OnBeforeDeath();
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
        {
            if (to is Dragon or WhiteWyrm or SwampDragon or Drake or Nightmare or Hiryu or LesserHiryu or Daemon)
            {
                damage *= 4;
            }
        }
    }
}
