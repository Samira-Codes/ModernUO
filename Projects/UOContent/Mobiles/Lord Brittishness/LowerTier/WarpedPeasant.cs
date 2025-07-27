using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.LowerTier
{
    [SerializationGenerator(0, false)]
    public partial class WarpedPeasant : BaseCreature
    {
        [Constructible]
        public WarpedPeasant() : base(AIType.AI_Melee)
        {
            Name = "a warped peasant";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1319; // Sickly pale or unnatural

            BaseSoundID = 0; // No special sound, defaults to human

            SetStr(45, 60);
            SetDex(40, 55);
            SetInt(10, 20);

            SetHits(50, 65);
            SetDamage(4, 8);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 20);
            SetResistance(ResistanceType.Poison, 10, 20);

            SetSkill(SkillName.MagicResist, 25.0, 40.0);
            SetSkill(SkillName.Tactics, 35.0, 50.0);
            SetSkill(SkillName.Wrestling, 35.0, 50.0);

            Fame = 300;
            Karma = -300;
            AddItem(new FancyShirt(){ Movable = false, Hue = 1175 });
            AddItem(new Sandals(){ Movable = false, Hue = 1175 });
            AddItem(new LongPants(){ Movable = false, Hue = 1175 });
            VirtualArmor = 8;
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
