using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Engines.Events;
using Server.Items;

namespace Server.Mobiles.LordBrittishness.MidTier
{
    [SerializationGenerator(0, false)]
    public partial class EliteBlackthornGuard : BaseCreature
    {
        [Constructible]
        public EliteBlackthornGuard(): base(AIType.AI_Melee)
        {
            Name = "an elite Blackthorn guard";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1109; // Dark armored tone (you can change this to match equipment visuals)
            BaseSoundID = 0x45A; // Deep grunt / warrior sounds

            SetStr(250, 280);
            SetDex(90, 110);
            SetInt(40, 60);

            SetHits(300, 340);
            SetDamage(14, 20);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Energy, 25);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.MagicResist, 80.0, 90.0);
            SetSkill(SkillName.Tactics, 100.0, 110.0);
            SetSkill(SkillName.Wrestling, 100.0, 110.0);
            SetSkill(SkillName.Anatomy, 50.0, 70.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 50;

            AddItem(new Server.Items.PlateChest { Movable = false, Hue = 1175 }); // Shadow-colored armor
            AddItem(new Server.Items.PlateLegs { Movable = false, Hue = 1175 });
            AddItem(new Server.Items.Bascinet { Movable = false, Hue = 1175 });
            AddItem(new Server.Items.VikingSword { Movable = false, Hue = 1175 });
        }

        public override bool AlwaysMurderer => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 1);
            AddLoot(LootPack.Average, 2);
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.05);
        }
        public override void OnThink()
        {
            base.OnThink();

            if (Utility.RandomDouble() < 0.01)
            {
                PublicOverheadMessage(Server.MessageType.Emote, 0x22, false, "*growls a vow of loyalty to Lord Blackthorn*");
            }
        }
    }
}
