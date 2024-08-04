using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using FFXIVClientStructs.FFXIV.Common.Component.BGCollision.Math;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Extensions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class WAR
    {
        public const byte ClassID = 3;
        public const byte JobID = 21;
        public const uint
            ThrillofBattle = 40,
            Equilibrium = 3552,
            ShakeItOff = 7388,
            HeavySwing = 31,
            Maim = 37,
            Berserk = 38,
            Overpower = 41,
            StormsPath = 42,
            StormsEye = 45,
            Tomahawk = 46,
            InnerBeast = 49,
            SteelCyclone = 51,
            Infuriate = 52,
            FellCleave = 3549,
            Decimate = 3550,
            Upheaval = 7387,
            InnerRelease = 7389,
            RawIntuition = 3551,
            MythrilTempest = 16462,
            ChaoticCyclone = 16463,
            NascentFlash = 16464,
            InnerChaos = 16465,
            Orogeny = 25752,
            PrimalRend = 25753,
            Onslaught = 7386;

        public static class Buffs
        {
            public const ushort
                Holmgang = 409, 
                InnerRelease = 1177,
                SurgingTempest = 2677,
                NascentChaos = 1897,
                PrimalRendReady = 2624,
                Berserk = 86;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 1;
        }

        public static class Config
        {
            public const string
                WAR_KeepOnslaughtCharges = "WarKeepOnslaughtCharges",
                WAR_VariantCure = "WAR_VariantCure";
        }

        // Replace Storm's Path with Storm's Path combo and overcap feature on main combo to fellcleave
        internal class WAR_ST : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_ST;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                

                if (actionID == HeavySwing)
                {
                    var gauge = GetJobGauge<WARGauge>().BeastGauge;
                    var onslaughtChargesRemaining = PluginConfiguration.GetCustomIntValue(Config.WAR_KeepOnslaughtCharges);
                    var InnerReleaseCD = GetCooldownRemainingTime(InnerRelease);
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                    if (IsEnabled(CustomComboPreset.WAR_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.WAR_VariantCure))
                        return Variant.VariantCure;

                    if (ActionReady(Infuriate) && InCombat() && !HasEffect(Buffs.NascentChaos))
                    {
                        if (!HasBattleTarget() && GetRemainingCharges(Infuriate) == 2)
                            return Infuriate;

                        if (CanWeave(actionID))
                        {
                            if (GetRemainingCharges(Infuriate) == 2 && gauge <= 60)
                                return Infuriate;
                            if (GetRemainingCharges(Infuriate) >= 1 && InnerReleaseCD >= 30 && gauge <= 50 && HasEffect(Buffs.SurgingTempest))
                                return Infuriate;
                            if (GetRemainingCharges(Infuriate) >= 1 && AllAttack && gauge <= 50)
                                return Infuriate;
                        }
                    }

                    if (LevelChecked(Tomahawk) && !InMeleeRange() && HasBattleTarget())
                        return Tomahawk;

                    //Sub Mythril Tempest level check
                    if (IsEnabled(CustomComboPreset.WAR_InnerRelease) && ActionReady(Berserk) && !LevelChecked(MythrilTempest) && InCombat() && CanWeave(actionID))
                        return OriginalHook(Berserk);

                    //开场提前开原初的解放的情形
                    if (HasEffect(Buffs.InnerRelease))
                    {
                        //数一下有几个FC
                        float num = GetBuffStacks(Buffs.InnerRelease) + (HasEffect(Buffs.NascentChaos) ? 1 : 0);
                        if (GetRemainingCharges(Infuriate) == 2 || (GetRemainingCharges(Infuriate) == 1 && GetCooldownChargeRemainingTime(Infuriate) <= 15))
                            num++;

                        if (GetBuffRemainingTime(Buffs.InnerRelease) <= num * 2.5)
                            return OriginalHook(InnerBeast);
                    }
                    if (HasEffect(Buffs.PrimalRendReady) && (AllAttack || GetBuffRemainingTime(Buffs.PrimalRendReady) <= 5) && !IsMoving)
                        return PrimalRend;

                    if (HasEffect(Buffs.SurgingTempest) && InCombat())
                    {
                        //oGCD
                        if (CanWeave(actionID))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.WAR_Variant_SpiritDart) &&
                                IsEnabled(Variant.VariantSpiritDart) &&
                                (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;
                            if (IsEnabled(CustomComboPreset.WAR_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                                return Variant.VariantUltimatum;

                            if (IsEnabled(CustomComboPreset.WAR_InnerRelease) && ActionReady(OriginalHook(InnerRelease)))
                                return OriginalHook(InnerRelease);
                            if (ActionReady(Upheaval) && (InnerReleaseCD >= 10 || AllAttack))
                                return Upheaval;

                            if (IsEnabled(CustomComboPreset.WAR_ST_Onslaught) && LevelChecked(Onslaught) && GetRemainingCharges(Onslaught) > onslaughtChargesRemaining &&
                                !IsMoving && GetTargetDistance() <= 1 && (InnerReleaseCD >= 40 || LevelChecked(InnerRelease)))
                                return Onslaught;
                        }

                        //GCD
                        if (LevelChecked(InnerBeast))
                        {
                            //爆发期有就放
                            if (InnerReleaseCD >= 40)
                            {
                                if (IsEnabled(CustomComboPreset.WAR_PrimalRend) && HasEffect(Buffs.PrimalRendReady) && InnerReleaseCD <= 57 
                                    && !IsMoving && GetTargetDistance() <= 1)
                                    return PrimalRend;
                                if (HasEffect(Buffs.InnerRelease) || (HasEffect(Buffs.NascentChaos) && LevelChecked(InnerChaos)))
                                    return OriginalHook(InnerBeast);
                                if (gauge >= 50)
                                    return OriginalHook(InnerBeast);
                            }

                            //非爆发期
                            if (gauge >= 50)
                            {
                                //狂魂有就放，或者延后
                                if (HasEffect(Buffs.NascentChaos) && (InnerReleaseCD >= 27.5))
                                    return OriginalHook(InnerBeast);
                                //FC有就放
                                if (gauge >= 60 && !HasEffect(Buffs.NascentChaos) && InnerReleaseCD >= 10)
                                    return OriginalHook(InnerBeast);
                                if (AllAttack)
                                    return OriginalHook(InnerBeast);
                            }

                            //防兽魂溢出
                            if (gauge >= 90 && lastComboMove == Maim && GetBuffRemainingTime(Buffs.SurgingTempest) >= 2.5)
                                return OriginalHook(InnerBeast);
                            if (gauge == 100 && lastComboMove != StormsEye && lastComboMove != StormsPath)
                                return OriginalHook(InnerBeast);
                            if (gauge >= 60 && GetRemainingCharges(Infuriate) == 1 && GetCooldownChargeRemainingTime(Infuriate) <= 7.5 && lastComboMove != StormsEye && lastComboMove != StormsPath)
                                return OriginalHook(InnerBeast);
                            //战嚎和解放一起cd转好的时候,60兽魂就让它直接溢出吧，70兽魂提前放
                            if (gauge >= 70 && IsOnCooldown(InnerRelease) && InnerReleaseCD <= 2.5 && GetRemainingCharges(Infuriate) == 1 && GetCooldownChargeRemainingTime(Infuriate) <= 31)
                                return OriginalHook(InnerBeast);
                        }
                    }

                    if (comboTime > 0 && lastComboMove == Overpower && LevelChecked(MythrilTempest) && InActionRange(MythrilTempest))
                        return MythrilTempest;
                    if (IsEnabled(CustomComboPreset.WAR_ST_TwoTarget) && lastComboMove != Maim && LevelChecked(Overpower) && InActionRange(Overpower))
                        return Overpower;

                    if (comboTime > 0)
                    {
                        if (LevelChecked(MythrilTempest) && !LevelChecked(StormsEye) && GetBuffRemainingTime(Buffs.SurgingTempest) <= 5 && lastComboMove != HeavySwing && lastComboMove != Maim)
                            return Overpower;

                        if (lastComboMove == HeavySwing && LevelChecked(Maim))
                            return Maim;
                        if (lastComboMove == Maim)
                        {
                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath) && LevelChecked(StormsPath))
                                return StormsPath;
                            if (LevelChecked(StormsEye))
                            {
                                //threshhold设置20s？其实要看过场时间，但太多不同了，麻烦就设置成常驻好了
                                if (GetBuffRemainingTime(Buffs.SurgingTempest) <= 30)
                                    return StormsEye;
                                //这个其实没用了
                                //if (LevelChecked(StormsEye) && GetBuffRemainingTime(Buffs.SurgingTempest) <= 30 && GetBuffRemainingTime(Buffs.SurgingTempest) - InnerReleaseCD <= 15)
                                //    return StormsEye;
                                
                            }
                            if (LevelChecked(StormsPath))
                                return StormsPath;
                        }
                    }
                }
                return actionID;
            }
        }

        internal class WAR_AoE_Overpower : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_AoE_Overpower;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == Overpower)
                {
                    var gauge = GetJobGauge<WARGauge>().BeastGauge;
                    var InnerReleaseCD = GetCooldownRemainingTime(InnerRelease);

                    if (IsEnabled(CustomComboPreset.WAR_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.WAR_VariantCure))
                        return Variant.VariantCure;

                    if (InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.WAR_AoE_Overpower_HealInDanger) && PlayerHealthPercentageHp() <= 15 && !HasEffect(Buffs.Holmgang))
                        {
                            if (ActionReady(ThrillofBattle))
                                return ThrillofBattle;
                            if (ActionReady(RawIntuition))
                                return RawIntuition;
                            if (ActionReady(Equilibrium))
                                return Equilibrium;
                        }
                        if (ActionReady(RawIntuition) && PlayerHealthPercentageHp() <= 25 && CanWeave(actionID))
                            return RawIntuition;
                    }

                    if (ActionReady(Infuriate)&& !HasEffect(Buffs.NascentChaos) && gauge <= 50 && InCombat() && CanWeave(actionID))
                        return Infuriate;

                    //Sub Mythril Tempest level check
                    if (IsEnabled(CustomComboPreset.WAR_InnerRelease) && ActionReady(OriginalHook(Berserk)) && !LevelChecked(MythrilTempest) && InCombat() && CanWeave(actionID))
                        return OriginalHook(Berserk);

                    if (HasEffect(Buffs.SurgingTempest) && InCombat())
                    {
                        if (CanWeave(actionID))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.WAR_Variant_SpiritDart) &&
                                IsEnabled(Variant.VariantSpiritDart) &&
                                (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;

                            if (IsEnabled(CustomComboPreset.WAR_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                                return Variant.VariantUltimatum;

                            if (IsEnabled(CustomComboPreset.WAR_InnerRelease) && ActionReady(Berserk))
                                return OriginalHook(InnerRelease);
                            if (ActionReady(Orogeny) && InnerReleaseCD >= 10 && HasEffect(Buffs.SurgingTempest))
                                return Orogeny;
                            if (ActionReady(Upheaval) && InnerReleaseCD >= 10 && HasEffect(Buffs.SurgingTempest))
                                return Upheaval;
                        }

                        if (InnerReleaseCD >= 40)
                        {
                            if (IsEnabled(CustomComboPreset.WAR_PrimalRend) && HasEffect(Buffs.PrimalRendReady) && InnerReleaseCD <= 57 && 
                                !IsMoving && GetTargetDistance() <= 1)
                                return PrimalRend;
                            if (HasEffect(Buffs.InnerRelease))
                                return OriginalHook(SteelCyclone);
                            if (gauge >= 50)
                                return OriginalHook(SteelCyclone);
                        }
                        if (InnerReleaseCD >= 15 && gauge >= 50)
                            return OriginalHook(SteelCyclone);
                    }

                    if (comboTime > 0)
                    {
                        if (lastComboMove == Overpower && LevelChecked(MythrilTempest))
                        {
                            if (gauge >= 90 && LevelChecked(SteelCyclone))
                                return OriginalHook(SteelCyclone);
                            return MythrilTempest;
                        }
                    }
                }
                return actionID;
            }
        }

        internal class WAR_Onslaught_PrimalRend : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_Onslaught_PrimalRend;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is Onslaught)
                {
                    if (LevelChecked(PrimalRend) && (HasEffect(Buffs.PrimalRendReady) || GetCooldownRemainingTime(InnerRelease) >= 59.5) && GetCooldownRemainingTime(HeavySwing) < 0.6)
                        return PrimalRend;
                }
                return actionID;
            }
        }
    }
}
