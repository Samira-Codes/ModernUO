using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]


    public partial class VenomWyrm : BaseCreature
    {
        [Constructible]
        public VenomWyrm() : base(AIType.AI_Mage)
        {
            Body = Utility.RandomBool() ? 180 : 49;
            BaseSoundID = 362;
            Hue = 1267;

            SetStr(721, 760);
            SetDex(101, 130);
            SetInt(386, 425);

            SetHits(433, 456);

            SetDamage(17, 25);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 55, 70);
            SetResistance(ResistanceType.Fire, 40, 70);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 80, 90);
            SetResistance(ResistanceType.Energy, 20, 50);

            SetSkill(SkillName.EvalInt, 99.1, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 64;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 96.3;
        }

        public override string CorpseName => "a Venom wyrm corpse";
        public override string DefaultName => "a Venom wyrm";

        public override bool ReacquireOnMovement => true;
        public override int TreasureMapLevel => 4;
        public override int Meat => 19;
        public override int Hides => 20;
        public override HideType HideType => HideType.Horned;
        public override int Scales => 9;
        public override ScaleType ScaleType => ScaleType.White;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Gold;
        public override bool CanAngerOnTame => true;
        public override bool CanFly => true;
        public bool CanPoisonStrike => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, Utility.Random(1, 5));
        }
        private void CastPoisonStrike()
        {
            if (!Alive || Map == null)
                return;

            List<Mobile> targets = new();

            foreach (Mobile m in GetMobilesInRange(8))
            {
                if (m == this || !CanBeHarmful(m) || !InLOS(m) || !m.Alive)
                    continue;

                // Prevent hitting players *only if* this creature is tamed
                if (Controlled && m.Player)
                    continue;

                targets.Add(m);
            }

            if (targets.Count == 0)
                return;

            // Animation & sound (copied from Poison Strike)
            this.Animate(17, 5, 1, true, false, 0); // Adjust if animation doesn't match
            this.PlaySound(0x1E1); // Sound from Poison Strike

            foreach (var target in targets)
            {
                DoHarmful(target);

                // Graphic effect (Poison explosion type)
                Effects.SendLocationEffect(target.Location, target.Map, 0x374A, 10, 10, 61, 0); // Hue 61 = poison green

                // Apply poison
                target.ApplyPoison(this, Poison.Greater);

                // Optional: add poison damage
                AOS.Damage(target, this, Utility.RandomMinMax(15, 20), 0, 0, 0, 100, 0);
            }

            PublicOverheadMessage(MessageType.Emote, 0x44, false, "*releases a toxic surge of venom*");
        }
        private DateTime _nextPoisonStrikeTime = DateTime.UtcNow;

        public override void OnThink()
        {
            base.OnThink();

            if (!Alive || Map == null || !CanPoisonStrike)
                return;

            if (DateTime.UtcNow >= _nextPoisonStrikeTime)
            {
                    CastPoisonStrike();
                _nextPoisonStrikeTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(5, 10));
            }
        }
        

    }
}
