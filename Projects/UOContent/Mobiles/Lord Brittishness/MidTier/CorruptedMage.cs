using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.MidTier
{
    [SerializationGenerator(0, false)]
    public partial class CorruptedMage : BaseCreature
    {
        [Constructible]
        public CorruptedMage() : base(AIType.AI_Mage)
        {
            Name = "a corrupted mage";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1175;   // Pale undead blue or adjust as needed
            BaseSoundID = 0x3E9; // Ghastly Lich-like sound

            SetStr(90, 110);
            SetDex(70, 90);
            SetInt(220, 250);

            SetHits(180, 220);
            SetDamage(8, 14);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Cold, 30);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.EvalInt, 100.0, 110.0);
            SetSkill(SkillName.Magery, 100.0, 110.0);
            SetSkill(SkillName.MagicResist, 90.0, 100.0);
            SetSkill(SkillName.Tactics, 60.0, 75.0);
            SetSkill(SkillName.Wrestling, 40.0, 60.0);

            Fame = 6000;
            Karma = -6000;

            AddItem(new LeatherCap { Movable = false, Hue = 1175 });
            AddItem(new LeatherChest { Movable = false, Hue = 1175 });
            AddItem(new LeatherArms { Movable = false, Hue = 1175 });
            AddItem(new LeatherLegs { Movable = false, Hue = 1175 });
            AddItem(new LeatherGorget { Movable = false, Hue = 1175 });
            AddItem(new WoodenKiteShield { Movable = false, Hue = 1175 });

            AddItem(new Shirt(){ Movable = false, Hue = 1175 });
            AddItem(new Sandals(){ Movable = false, Hue = 1175 });

            VirtualArmor = 40;
        }

        public override bool AlwaysMurderer => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.Potions);
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void OnThink()
        {
            base.OnThink();

            if (Utility.RandomDouble() < 0.02)
            {
                PublicOverheadMessage(Server.MessageType.Emote, 0x22, false, "*channels chaotic arcane energy*");
            }
        }
    }
}
