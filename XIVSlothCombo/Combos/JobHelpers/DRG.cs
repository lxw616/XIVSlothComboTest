using ECommons.DalamudServices;
using System.Collections.Generic;
using System.Linq;
using XIVSlothCombo.Combos.JobHelpers.Enums;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;

namespace XIVSlothCombo.Combos.JobHelpers
{
    internal class AnimationLock
    {
        internal static readonly List<uint> FastLocks =
        [
            PvE.DRG.BattleLitany,
            PvE.DRG.LanceCharge,
            PvE.DRG.DragonSight,
            PvE.DRG.LifeSurge,
            PvE.DRG.Geirskogul,
            PvE.DRG.Nastrond,
            PvE.DRG.MirageDive,
            PvE.DRG.WyrmwindThrust,
            PvE.Content.Variant.VariantRampart
        ];

        internal static readonly List<uint> MidLocks =
        [
            PvE.DRG.Jump,
            PvE.DRG.HighJump,
            PvE.DRG.DragonfireDive,
            PvE.DRG.SpineshatterDive
        ];

        internal static uint SlowLock => PvE.DRG.Stardiver;

        internal static bool CanDRGWeave(uint oGCD)
        {
            //GCD Ready - No Weave
            if (CustomComboFunctions.IsOffCooldown(PvE.DRG.TrueThrust))
                return false;

            var gcdTimer = CustomComboFunctions.GetCooldownRemainingTime(PvE.DRG.TrueThrust);

            if (FastLocks.Any(x => x == oGCD) && gcdTimer >= 0.6f)
                return true;

            if (MidLocks.Any(x => x == oGCD) && gcdTimer >= 0.8f)
                return true;

            if (SlowLock == oGCD && gcdTimer >= 1.5f)
                return true;

            return false;
        }
    }
}