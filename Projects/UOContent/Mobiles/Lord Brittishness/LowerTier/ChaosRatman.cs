using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.LowerTier
{
    [SerializationGenerator(0, false)]
    public partial class ChaosRatman : BaseCreature
    {
        [Constructible]
        public ChaosRatman() : base(AIType.AI_Mage)
        {
            Name = "a Chaos Ratman";
            Body = 0x8F; // Ratman body
            BaseSoundID = 437;
            Hue = 1109; // Dark reddish-purple chaos hue

            SetStr(86, 105);
            SetDex(81, 100);
            SetInt(96, 120);

            SetHits(60, 75);

            SetDamage(5, 10);
            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 15, 25);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 10, 15);
            SetResistance(ResistanceType.Energy, 15, 25);

            SetSkill(SkillName.EvalInt, 70.0, 90.0);
            SetSkill(SkillName.Magery, 65.0, 85.0);
            SetSkill(SkillName.MagicResist, 55.0, 75.0);
            SetSkill(SkillName.Tactics, 50.0, 70.0);
            SetSkill(SkillName.Wrestling, 40.0, 60.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 24;

            AddItem(new Robe { Hue = 1109 }); // Chaos-colored robe
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override bool AlwaysMurderer => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }
    }
}