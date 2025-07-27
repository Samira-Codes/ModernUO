using ModernUO.Serialization;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.HighTier
{
    [SerializationGenerator(0, false)]
    public partial class ShadowSerpent : BaseCreature
    {
        [Constructible]
        public ShadowSerpent()
            : base(AIType.AI_Melee)
        {
            Name = "Shadow Serpent";
            Body = 89; // Serpent body
            Hue = 1175; // Dark shadow hue
            BaseSoundID = 219;

            SetStr(400, 450);
            SetDex(150, 180);
            SetInt(20, 30); // Not magical

            SetHits(500, 550);
            SetDamage(15, 22);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 80, 90);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.Poisoning, 100.0);
            SetSkill(SkillName.Tactics, 90.0, 100.0);
            SetSkill(SkillName.Wrestling, 85.0, 95.0);
            SetSkill(SkillName.MagicResist, 50.0, 70.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 45;

            Tamable = false;
            ControlSlots = 0;
        }

        public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Deadly;
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.08);
        }
        public override bool BleedImmune => true;
        public override bool CanRummageCorpses => false;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }
    }
}
