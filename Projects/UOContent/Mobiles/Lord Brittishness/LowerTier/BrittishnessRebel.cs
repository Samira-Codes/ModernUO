using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.LowerTier
{
    [SerializationGenerator(0, false)]
    public partial class BrittishnessRebel : BaseCreature
    {
        [Constructible]
        public BrittishnessRebel() : base(AIType.AI_Melee)
        {
            Name = "a Brittishness Rebel";
           Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 0;      // Natural skin tone (adjust to your liking)
            BaseSoundID = 0x45A;

            SetStr(96, 120);
            SetDex(86, 105);
            SetInt(31, 55);

            SetHits(58, 72);

            SetDamage(6, 12);
            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 30);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 5, 15);
            SetResistance(ResistanceType.Energy, 5, 15);

            SetSkill(SkillName.MagicResist, 45.0, 60.0);
            SetSkill(SkillName.Tactics, 55.0, 70.0);
            SetSkill(SkillName.Wrestling, 50.0, 65.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 20;

            AddItem(new Boots(){ Movable = false, Hue = 1175 });
            AddItem(new RingmailChest(){ Movable = false, Hue = 1175 });
            AddItem(new RingmailLegs(){ Movable = false, Hue = 1175 });
            AddItem(new RingmailArms(){ Movable = false, Hue = 1175 });
            AddItem(new RingmailGloves(){ Movable = false, Hue = 1175 });
            AddItem(new Longsword(){ Movable = false, Hue = 1175 }); // or use Sword/Dagger if preferred
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
    }
}
