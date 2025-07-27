using System;
using Server;
using Server.Items;
using Server.Mobiles;
using ModernUO.Serialization;
using Server.Engines.Events;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class BlackthornAcolyte : BaseCreature
    {
        [Constructible]
        public BlackthornAcolyte() : base(AIType.AI_Mage)
        {
            Name = "a Blackthorn Acolyte";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1175;   // Dark purple/black

            SetStr(91, 105);
            SetDex(91, 105);
            SetInt(161, 185);

            SetHits(58, 72);
            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 30);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.EvalInt, 60.0, 75.0);
            SetSkill(SkillName.Magery, 60.0, 75.0);
            SetSkill(SkillName.MagicResist, 50.0, 65.0);
            SetSkill(SkillName.Tactics, 40.0, 60.0);
            SetSkill(SkillName.Wrestling, 40.0, 55.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 28;

            AddItem(new Robe { Movable = false, Hue = 1175 });
            AddItem(new Sandals { Movable = false, Hue = 1175 });

            // Optional loot
            PackGold(25, 50);
        }

        public override bool AlwaysMurderer => true;
        public override bool CanRummageCorpses => true;
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public BlackthornAcolyte(Serial serial) : base(serial) { }
    }
}