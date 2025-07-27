using ModernUO.Serialization;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.HighTier
{
    [SerializationGenerator(0, false)]
    public partial class AvatarOfCorruption : BaseCreature
    {
        [Constructible]
        public AvatarOfCorruption()
            : base(AIType.AI_Mage)
        {
            Name = "an Avatar of Corruption";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = Race.Human.RandomSkinHue();
            BaseSoundID = 357;

            SetStr(600, 700);
            SetDex(150, 180);
            SetInt(400, 500);

            SetHits(900, 1050);
            SetDamage(22, 32);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Energy, 30);
            SetDamageType(ResistanceType.Poison, 30);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 45, 55);
            SetResistance(ResistanceType.Poison, 60, 75);
            SetResistance(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.EvalInt, 100.0, 120.0);
            SetSkill(SkillName.Magery, 100.0, 120.0);
            SetSkill(SkillName.MagicResist, 100.0, 110.0);
            SetSkill(SkillName.Tactics, 100.0, 115.0);
            SetSkill(SkillName.Wrestling, 100.0, 110.0);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 70;
        }

        public override bool AlwaysMurderer => true;
        public override bool CanFly => true;
        public override bool AutoDispel => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.Gems, 4);
        }     
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.08);
        }
        public override void OnThink()
        {
            base.OnThink();

            if (Utility.RandomDouble() < 0.03)
            {
                PublicOverheadMessage(Server.MessageType.Emote, 0x23, false, "*shudders with unholy malice*");
            }
        }
    }
}
