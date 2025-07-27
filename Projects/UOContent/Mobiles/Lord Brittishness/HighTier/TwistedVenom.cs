using ModernUO.Serialization;
using Server;
using Server.Mobiles;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Engines.Events;

namespace Server.Mobiles.LordBrittishness.HighTier
{
    [SerializationGenerator(0, false)]
    public partial class TwistedVenom : BaseCreature
    {
        private DateTime _nextVenomBurst = DateTime.UtcNow;

        [Constructible]
        public TwistedVenom() : base(AIType.AI_Melee)
        {
            Name = "Twisted Venom";
            Body = 155; // Rotting corpse style
            Hue = 61; // Sickly poison green
            BaseSoundID = 684;

            SetStr(720, 760);
            SetDex(76, 95);
            SetInt(30, 40);

            SetHits(750, 800);
            SetDamage(20, 28);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Poison, 40);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 85, 95);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 85.0, 95.0);
            SetSkill(SkillName.Tactics, 95.0, 100.0);
            SetSkill(SkillName.Wrestling, 95.0, 105.0);
            SetSkill(SkillName.Poisoning, 100.0);

            Fame = 16000;
            Karma = -16000;

            VirtualArmor = 65;

            Tamable = false;
        }

        public override string DefaultName => "Twisted Venom";
        public override Poison HitPoison => Poison.Lethal;
        public override Poison PoisonImmune => Poison.Lethal;

        public override void OnThink()
        {
            base.OnThink();

            if (!Alive || Map == null)
                return;

            if (DateTime.UtcNow >= _nextVenomBurst)
            {
                SpreadVenom();
                _nextVenomBurst = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(8, 14));
            }
        }

        private void SpreadVenom()
        {
            PublicOverheadMessage(MessageType.Emote, 0x44, false, "*erupts with a surge of toxic gas*");
            this.Animate(17, 5, 1, true, false, 0); // Burst animation
            this.PlaySound(684); // Rotting sound

            var targets = new List<Mobile>();

            foreach (Mobile m in GetMobilesInRange(8))
            {
                targets.Add(m);
            }

            foreach (Mobile m in targets)
            {
                if (m == this || !m.Alive || !CanBeHarmful(m) || !InLOS(m))
                    continue;

                // Only affect players and their controlled/summoned pets
                if (!m.Player && !(m is BaseCreature bc && bc.Controlled))
                    continue;

                DoHarmful(m);
                Effects.SendLocationEffect(m.Location, m.Map, 0x374A, 10, 10, 61, 0); // Poison cloud visual
                m.ApplyPoison(this, Poison.Greater);
                AOS.Damage(m, this, Utility.RandomMinMax(12, 18), 0, 0, 0, 100, 0); // Poison damage
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            BrittishEventDrops.TryDropFrom(this.LastKiller, 0.08);
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            
            AddLoot(LootPack.Gems, 4);
        }
    }
}
