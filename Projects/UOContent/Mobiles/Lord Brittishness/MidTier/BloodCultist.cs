using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.MidTier
{
    [SerializationGenerator(0, false)]
    public partial class BloodCultist : BaseCreature
    {
        [Constructible]
        public BloodCultist() : base(AIType.AI_Mage)
        {
            Name = "a blood cultist";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1157;   // Blood red
            BaseSoundID = 0x165;

            SetStr(150, 180);
            SetDex(70, 90);
            SetInt(180, 220);

            SetHits(220, 260);
            SetDamage(8, 12);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 40);
            SetDamageType(ResistanceType.Energy, 40);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 85.0, 100.0);
            SetSkill(SkillName.Magery, 85.0, 100.0);
            SetSkill(SkillName.MagicResist, 70.0, 85.0);
            SetSkill(SkillName.Tactics, 60.0, 75.0);
            SetSkill(SkillName.Wrestling, 55.0, 70.0);

            Fame = 5000;
            Karma = -5000;

            AddItem(new LeatherCap { Movable = false, Hue = 1175 });
            AddItem(new BoneChest { Movable = false, Hue = 1175 });
            AddItem(new BoneArms { Movable = false, Hue = 1175 });
            AddItem(new BoneLegs { Movable = false, Hue = 1175 });
            AddItem(new BoneHelm { Movable = false, Hue = 1175 });
            AddItem(new WoodenKiteShield { Movable = false, Hue = 1175 });

            AddItem(new Shirt(){ Movable = false, Hue = 1175 });
            AddItem(new Sandals(){ Movable = false, Hue = 1175 });

            VirtualArmor = 30;
        }

        public override bool AlwaysMurderer => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
            AddLoot(LootPack.MedScrolls, 2);
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void OnThink()
        {
            base.OnThink();

            if (Utility.RandomDouble() < 0.01) // 1% chance per think to emote
            {
                PublicOverheadMessage(Server.MessageType.Emote, 0x22, false, "*chants in a forgotten, blood-soaked tongue*");
            }
        }
    }
}
