using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.LowerTier
{
    [SerializationGenerator(0, false)]
    public partial class InfectedDog : BaseCreature
    {
        [Constructible]
        public InfectedDog() : base(AIType.AI_Melee)
        {
            Name = "an Infected Dog";
            Body = 225; // Dog
            BaseSoundID = 0xE5;
            Hue = 1425; // Sickly green/grey hue

            SetStr(50, 65);
            SetDex(90, 110);
            SetInt(10, 20);

            SetHits(40, 55);
            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 30);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Fire, 10, 20);

            SetSkill(SkillName.MagicResist, 20.0, 30.0);
            SetSkill(SkillName.Tactics, 40.0, 55.0);
            SetSkill(SkillName.Wrestling, 35.0, 50.0);

            Fame = 500;
            Karma = -500;

            VirtualArmor = 12;
        }

        public override bool AlwaysMurderer => true;
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
        }
    }
}
