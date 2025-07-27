using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.LowerTier
{
    [SerializationGenerator(0, false)]
    public partial class CorruptedFootman : BaseCreature
    {
        [Constructible]
        public CorruptedFootman() : base(AIType.AI_Mage)
        {
            Name = "a Corrupted Footman";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 2101; // Pale gray/ashen hue for a corrupted look
            BaseSoundID = 0x45A;

            SetStr(96, 120);
            SetDex(66, 85);
            SetInt(46, 60);

            SetHits(85, 100);
            SetDamage(8, 12);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 20, 25);
            SetResistance(ResistanceType.Energy, 15, 25);

            SetSkill(SkillName.MagicResist, 45.0, 60.0);
            SetSkill(SkillName.Tactics, 70.0, 90.0);
            SetSkill(SkillName.Wrestling, 60.0, 80.0);

            Fame = 2000;
            Karma = -2000;

            VirtualArmor = 30;

            // Visual gear
            AddItem(new PlateChest { Movable = false, Hue = 1175 }); // Chaos purple armor
            AddItem(new PlateArms { Movable = false, Hue = 1175 });
            AddItem(new PlateLegs { Movable = false, Hue = 1175 });
            AddItem(new Shirt(){ Movable = false, Hue = 1175 });
            AddItem(new Sandals(){ Movable = false, Hue = 1175 });
            AddItem(new VikingSword(){ Movable = false, Hue = 1175 });
            AddItem(new MetalShield(){ Movable = false, Hue = 1175 });
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
