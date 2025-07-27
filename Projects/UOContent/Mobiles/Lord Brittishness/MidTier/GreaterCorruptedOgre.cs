using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Engines.Events;
using Server.Items;

namespace Server.Mobiles.LordBrittishness.MidTier
{
    [SerializationGenerator(0, false)]
    public partial class GreaterCorruptedOgre : BaseCreature
    {
        [Constructible]
        public GreaterCorruptedOgre()
            : base(AIType.AI_Melee)
        {
            Name = "a greater corrupted ogre";
            Body = 83; // Ogre Lord body
            Hue = 1175; // Sickly green/black hue for corruption
            BaseSoundID = 427;

            SetStr(300, 350);
            SetDex(70, 85);
            SetInt(40, 60);

            SetHits(350, 420);
            SetDamage(18, 24);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 60, 75);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.MagicResist, 70.0, 85.0);
            SetSkill(SkillName.Tactics, 90.0, 100.0);
            SetSkill(SkillName.Wrestling, 90.0, 105.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 50;
        }

        public override bool AlwaysMurderer => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void OnThink()
        {
            base.OnThink();

            if (Utility.RandomDouble() < 0.025)
            {
                PublicOverheadMessage(Server.MessageType.Emote, 0x44, false, "*snarls with corrupted rage*");
            }
        }
    }
}
