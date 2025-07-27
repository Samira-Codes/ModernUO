using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Engines.Events;
using Server.Items;

namespace Server.Mobiles.LordBrittishness.LowerTier
{
    [SerializationGenerator(0, false)]
    public partial class ShadowMinion : BaseCreature
    {
        [Constructible]
        public ShadowMinion() : base(AIType.AI_Mage)
        {
            Name = "a Shadow Minion";
            Body = 305; // Horde Minion graphic
            BaseSoundID = 0x491;
            Hue = 1175; // Shadowy hue

            SetStr(40, 55);
            SetDex(50, 65);
            SetInt(85, 105);

            SetHits(50, 65);
            SetDamage(6, 10);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Energy, 60);

            SetResistance(ResistanceType.Physical, 15, 25);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Magery, 70.0, 85.0);
            SetSkill(SkillName.EvalInt, 70.0, 85.0);
            SetSkill(SkillName.MagicResist, 45.0, 60.0);
            SetSkill(SkillName.Tactics, 25.0, 40.0);
            SetSkill(SkillName.Wrestling, 25.0, 40.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 20;
        }

        public override bool AlwaysMurderer => true;
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }
    }
}
