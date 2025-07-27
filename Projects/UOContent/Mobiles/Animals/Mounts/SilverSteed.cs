using ModernUO.Serialization;
using Server.Mobiles;
using Server;
using System;
using System.Linq;
using Server.Network;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class SilverSteed : BaseMount
    {
        public override string DefaultName => "a silver steed";

        private DateTime _nextResurrectTime = DateTime.MinValue;

        [Constructible]
        public SilverSteed() : base(0x75, 0x3EA8, AIType.AI_Animal, FightMode.Aggressor)
        {
            // Swamp dragon-style stats
            SetStr(150, 160);
            SetDex(100, 130);
            SetInt(6, 10);

            SetHits(130, 181);
            SetDamage(2, 6);

            SetSkill(SkillName.MagicResist, 30.0, 40.0);
            SetSkill(SkillName.Tactics, 50.0, 60.0);
            SetSkill(SkillName.Wrestling, 50.0, 60.0);

            VirtualArmor = 20;

            ControlSlots = 1;
            Tamable = true;
            MinTameSkill = 105.1;
        }

        public override PackInstinct PackInstinct => PackInstinct.Equine;
        public override int StepsMax => 4480;
        public override string CorpseName => "a silver steed corpse";
        public override bool CanAngerOnTame => false;

        public override void OnThink()
        {
            base.OnThink();

            if (Controlled && ControlMaster != null)
            {
                TryResurrectNearbyEquines();
            }
        }

        private void TryResurrectNearbyEquines()
        {
            if (DateTime.UtcNow < _nextResurrectTime)
                return;

            const double range = 2.0;

            foreach (Mobile m in GetMobilesInRange((int)range))
            {
                if (m is BaseCreature bc &&
                    bc != this &&
                    !bc.Alive &&
                    bc.Corpse != null &&
                    bc.Corpse.Map == Map &&
                    bc.PackInstinct == PackInstinct.Equine)
                {
                    bc.Resurrect();
                    bc.Hits = 10; // Resurrect with low health
                    bc.PlaySound(0x214); // Resurrection sound
                    PublicOverheadMessage(Server.MessageType.Emote, 0x3B2, false, "*radiates a soft healing light*");

                    _nextResurrectTime = DateTime.UtcNow + TimeSpan.FromMinutes(4); // 5 min cooldown
                    break;
                }
            }
        }
    }
}