using ModernUO.Serialization;
using Server.Items;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.HighTier
{
    [SerializationGenerator(0, false)]
    public partial class BrittishnessChampion : BaseCreature
    {
        [Constructible]
        public BrittishnessChampion() : base(AIType.AI_Mage)
        {
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = Race.Human.RandomSkinHue();
             Name = "Lord Brittishness Champion";

            SetSpeed(0.12, 0.30);

            SetStr(325, 375);
            SetDex(80, 100);
            SetInt(150, 200);

            SetHits(400, 475);

            SetDamage(30, 38);
            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.Magery, 90.0, 100.0);
            SetSkill(SkillName.EvalInt, 90.0, 100.0);
            SetSkill(SkillName.MagicResist, 100.0, 110.0);
            SetSkill(SkillName.Swords, 80.0, 95.0);
            SetSkill(SkillName.Macing, 80.0, 95.0);
            SetSkill(SkillName.Fencing, 80.0, 95.0);
            SetSkill(SkillName.Tactics, 85.0, 100.0);
            SetSkill(SkillName.Healing, 60.0, 90.0);

            Fame = 12000;
            Karma = -12000;

            var res = Utility.Random(6) switch
            {
                0 => CraftResource.BlackScales,
                1 => CraftResource.RedScales,
                2 => CraftResource.BlueScales,
                3 => CraftResource.YellowScales,
                4 => CraftResource.GreenScales,
                _ => CraftResource.WhiteScales
            };

            var melee = Utility.Random(3) switch
            {
                0 => (BaseWeapon)new Kryss(),
                1 => new Broadsword(),
                _ => new Katana()
            };

            melee.Movable = false;
            AddItem(melee);

            AddItem(new DragonChest { Resource = res, Movable = false });
            AddItem(new DragonLegs { Resource = res, Movable = false });
            AddItem(new DragonArms { Resource = res, Movable = false });
            AddItem(new DragonGloves { Resource = res, Movable = false });
            AddItem(new DragonHelm { Resource = res, Movable = false });
            AddItem(new ChaosShield { Movable = false });

            AddItem(new Boots(0x455));
            AddItem(new Shirt(Utility.RandomMetalHue()));

            var amount = Utility.RandomMinMax(2, 4);

            AddItem(res switch
            {
                CraftResource.BlackScales => new BlackScales(amount),
                CraftResource.RedScales => new RedScales(amount),
                CraftResource.BlueScales => new BlueScales(amount),
                CraftResource.YellowScales => new YellowScales(amount),
                CraftResource.GreenScales => new GreenScales(amount),
                _ => new WhiteScales(amount)
            });

            // Mount: Ridgeback
            var Mount = new SilverSteed();
                        Mount.Rider = this;
        }

        public override string CorpseName => "the corpse of a corrupted champion";
        public override string DefaultName => "Brittishness Champion";

        public override bool AutoDispel => true;
        public override bool BardImmune => !Core.AOS;
        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;
        public override bool ShowFameTitle => false;

        private static MonsterAbility[] _abilities = { MonsterAbilities.FireBreath };
        public override MonsterAbility[] GetMonsterAbilities() => _abilities;

        public override int GetIdleSound() => 0x2CE;
        public override int GetDeathSound() => 0x2CC;
        public override int GetHurtSound() => 0x2D1;
        public override int GetAttackSound() => 0x2C8;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich);
            AddLoot(LootPack.Gems, 2);
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.08);
        }
        public override bool OnBeforeDeath()
        {
            var mount = Mount;
            if (mount != null)
                mount.Rider = null;

            return base.OnBeforeDeath();
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
        {
            if (to is Dragon or WhiteWyrm or SwampDragon or Drake or Nightmare or Hiryu or LesserHiryu or Daemon)
                damage *= 3;
        }

        public BrittishnessChampion(Serial serial) : base(serial) { }
    }
}
