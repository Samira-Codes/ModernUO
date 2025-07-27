using ModernUO.Serialization;
using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Items;
using Server.Targeting;
using Server.Network;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class EnergyDragon : BaseCreature
    {
        [Constructible]
        public EnergyDragon() : base(AIType.AI_Mage)
        {
            Body = Utility.RandomList(12, 59);
            BaseSoundID = 362;
            Hue = 111; // Electric blue 1155

            SetStr(796, 825);
            SetDex(86, 105);
            SetInt(436, 475);

            SetHits(478, 495);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 70, 80);

            SetSkill(SkillName.EvalInt, 30.1, 40.0);
            SetSkill(SkillName.Magery, 30.1, 40.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 92.5);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 93.9;
        }

        public override string CorpseName => "a Energy Dragon corpse";
        public override string DefaultName => "a Energy Dragon";

        public override bool ReacquireOnMovement => !Controlled;
        public override bool AutoDispel => !Controlled;
        public override int TreasureMapLevel => 4;
        public override int Meat => 19;
        public override int Hides => 20;
        public override HideType HideType => HideType.Barbed;
        public override int Scales => 7;
        public override ScaleType ScaleType => Body == 12 ? ScaleType.Yellow : ScaleType.Red;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;
        public override bool CanFly => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 8);
        }
        private DateTime _nextChainTime = DateTime.UtcNow;

        public override void OnThink()
        {
            base.OnThink();

            if (!Alive || Combatant == null || !InRange(Combatant, 10) || !CanBeHarmful(Combatant))
                return;

            if (DateTime.UtcNow >= _nextChainTime)
            {
                CastChainLightning();
                _nextChainTime = DateTime.UtcNow + TimeSpan.FromSeconds(10);
            }
        }

        private void CastChainLightning()
        {
            if (!Alive || Map == null)
                return;

            // 🔥 Play fire-breath animation
            Animate(6, 1, 1, true, false, 0);

            // ⚡ Chain Lightning sound effect
            Effects.PlaySound(Location, Map, 0x160);

            var targets = new List<Mobile>();

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
            {
                return;
            }
            foreach (var target in targets)
            {
                DoHarmful(target);

                // ⚡ Lightning bolt effect (electric blue)
                Effects.SendBoltEffect(target, true, 111);

                // ⚡ Damage: 17–22 energy, scaled by resist
                int baseDamage = Utility.RandomMinMax(17, 22);
                int resist = target.GetResistance(ResistanceType.Energy);
                double modifier = 1.0 - (resist / 100.0);
                int finalDamage = (int)(baseDamage * modifier);

                AOS.Damage(target, this, finalDamage, 0, 0, 0, 0, 100);
            }

            PublicOverheadMessage(MessageType.Emote, 0x3B2, false, "*crackles with surging energy*");
        }
        
    }
}
