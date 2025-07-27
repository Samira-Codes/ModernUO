using Server.Commands;
using Server.Engines.Spawners;
using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.CustomCommands
{
    public class AddBlackthornSpawnCommand
    {
        private static readonly string[] LowTier = new[]
        {
            "BrittishnessRebel", "BlacktornAcolyte", "ChaosRatman", "CorruptedFootman", "InfectedDog", "ShadowMinion", "WarpedPeasant"
        };

        private static readonly string[] MidTier = new[]
        {
            "BloodCultist", "CorruptedMage", "EliteBlackthornGuard", "EvilSentry", "GreaterCorruptedOgre"
        };

        private static readonly string[] HighTier = new[]
        {
            "BrittishnessChampion", "ShadowSerpent", "TwistedVenom", "AvatarOfBlacktorn"
        };

        public static void Initialize()
        {
            CommandSystem.Register("AddBlackthornSpawn", AccessLevel.GameMaster, e =>
            {
                e.Mobile.SendMessage("Target the center of the spawn area.");
                e.Mobile.Target = new InternalTarget();
            });
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, true, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is not IPoint3D point || from.Map == null || from.Map == Map.Internal)
                {
                    from.SendMessage("Invalid target.");
                    return;
                }

                var spawnCount = 100; // Number of spawners to place
                var spawnRadius = 125;

                for (int i = 0; i < spawnCount; i++)
                {
                    var offsetX = Utility.RandomMinMax(-spawnRadius, spawnRadius);
                    var offsetY = Utility.RandomMinMax(-spawnRadius, spawnRadius);
                    var location = new Point3D(point.X + offsetX, point.Y + offsetY, point.Z);

                    string mob = SelectMob();
                    var spawner = new Spawner(3, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2), 0, 8, mob);

                    spawner.MoveToWorld(location, from.Map);
                    from.SendMessage($"Spawner for {mob} added at {location}");
                }

                from.SendMessage("Blackthorn spawn generation complete.");
            }
        }

        private static string SelectMob()
        {
            var roll = Utility.RandomDouble();

            if (roll < 0.60) return LowTier[Utility.Random(LowTier.Length)];
            if (roll < 0.90) return MidTier[Utility.Random(MidTier.Length)];
            return HighTier[Utility.Random(HighTier.Length)];

        }
    }
}
