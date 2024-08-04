using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using System.ComponentModel;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class GNB
    {
        public const byte JobID = 37;

        public static int MaxCartridges(byte level) => level >= 88 ? 3 : 2;

        public const uint
            KeenEdge = 16137,
            NoMercy = 16138,
            BrutalShell = 16139,
            DemonSlice = 16141,
            SolidBarrel = 16145,
            GnashingFang = 16146,
            SavageClaw = 16147,
            DemonSlaughter = 16149,
            WickedTalon = 16150,
            SonicBreak = 16153,
            Continuation = 16155,
            JugularRip = 16156,
            AbdomenTear = 16157,
            EyeGouge = 16158,
            BowShock = 16159,
            HeartOfLight = 16160,
            BurstStrike = 16162,
            FatedCircle = 16163,
            Aurora = 16151,
            DoubleDown = 25760,
            DangerZone = 16144,
            BlastingZone = 16165,
            Bloodfest = 16164,
            Hypervelocity = 25759,
            RoughDivide = 16154,
            LightningShot = 16143;

        public static class Buffs
        {
            public const ushort
                NoMercy = 1831,
                Aurora = 1835,
                ReadyToRip = 1842,
                ReadyToTear = 1843,
                ReadyToGouge = 1844,
                ReadyToBlast = 2686;
        }

        public static class Debuffs
        {
            public const ushort
                BowShock = 1838,
                SonicBreak = 1837;
        }

        public static GNBGauge gauge => CustomComboFunctions.GetJobGauge<GNBGauge>();

        public static class Config
        {
            public const string
                GNB_RoughDivide_HeldCharges = "GNB_RoughDivide_HeldCharges",
                GNB_VariantCure = "GNB_VariantCure";
        }

        internal class GNB_ST_MainCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GNB_ST_MainCombo;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is KeenEdge)
                {
                    //var gauge = GetJobGauge<GNBGauge>();
                    var roughDivideChargesRemaining = PluginConfiguration.GetCustomIntValue(Config.GNB_RoughDivide_HeldCharges);
                    var NoMercyCD = GetCooldownRemainingTime(NoMercy);
                    var GnashingFangCD = GetCooldownRemainingTime(GnashingFang);
                    var DoubleDownCD = GetCooldownRemainingTime(DoubleDown);
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);
                    float TargetDistance = GetTargetDistance();

                    if (IsEnabled(CustomComboPreset.GNB_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.GNB_VariantCure))
                        return Variant.VariantCure;

                    if (!InCombat() && !InMeleeRange() && LevelChecked(LightningShot) && HasBattleTarget())
                        return LightningShot;

                    if (InCombat())
                    {
                        //Continuation
                        if ((HasEffect(Buffs.ReadyToRip) || HasEffect(Buffs.ReadyToTear) || HasEffect(Buffs.ReadyToGouge)) && !CanWeave(actionID) && TargetDistance <= 5)
                            return OriginalHook(Continuation);
                        if (HasEffect(Buffs.ReadyToBlast) && !CanWeave(actionID) && TargetDistance <= 5)
                            return Hypervelocity;

                        if (ActionReady(DangerZone) && (NoMercyCD >= 10 || AllAttack) && TargetDistance <= 3)
                        {
                            if (NoMercyCD >= 40 && CanWeave(actionID))
                                return OriginalHook(DangerZone);
                            if (NoMercyCD < 40 && CanWeave(actionID, 0.45))
                                return OriginalHook(DangerZone);
                        }

                        if (ActionReady(Bloodfest) && gauge.Ammo == 0 && CanWeave(actionID, 0.45) && NoMercyCD >= 40 &&
                            (DoubleDownCD <= 5 || DoubleDownCD >= 40))
                            return Bloodfest;

                        if (CanWeave(actionID))
                        {
                            if (ActionReady(NoMercy) && IsEnabled(CustomComboPreset.GNB_NoMercy) && CombatEngageDuration().TotalSeconds >= 3.5 &&
                                (CanDelayedWeave(actionID) || (GetCooldown(actionID).CooldownTotal > 2.45 && CanWeave(actionID))))
                                return NoMercy;

                            if (HasEffect(Buffs.ReadyToRip) || HasEffect(Buffs.ReadyToTear) || HasEffect(Buffs.ReadyToGouge))
                                return OriginalHook(Continuation);
                            if (HasEffect(Buffs.ReadyToBlast))
                                return Hypervelocity;

                            if (ActionReady(Bloodfest) && gauge.Ammo == 0 && (CombatEngageDuration().TotalSeconds >= 20 || IsEnabled(CustomComboPreset.GNB_NoMercy)) &&
                                (NoMercyCD <= 5 || NoMercyCD >= 40 || AllAttack))
                                return Bloodfest;

                            if (ActionReady(BowShock) && (IsEnabled(CustomComboPreset.GNB_ST_BowShock) || IsEnabled(CustomComboPreset.GNB_ST_TwoTarget)) 
                                && HasBattleTarget() && GetTargetDistance() <= 5 && (NoMercyCD >= 40 || AllAttack))
                                return BowShock;
                            
                            if (ActionReady(RoughDivide) && IsEnabled(CustomComboPreset.GNB_ST_RoughDivide) && TargetDistance <= 1 &&
                                (AllAttack ||GetRemainingCharges(RoughDivide) > roughDivideChargesRemaining))
                                return RoughDivide;
                        }
                        //GCD
                        if (!InMeleeRange() && LevelChecked(LightningShot) && HasBattleTarget())
                            return LightningShot;

                        if (LevelChecked(GnashingFang) && gauge.AmmoComboStep == 0 && gauge.Ammo >= 1 && GnashingFangCD <= 0.4 &&
                            (IsEnabled(CustomComboPreset.GNB_ST_GnashingFang_Go) || NoMercyCD >= 17.5 || AllAttack))
                            return GnashingFang;
                        if (IsEnabled(CustomComboPreset.GNB_ST_SonicBreak) && ActionReady(SonicBreak) && NoMercyCD >= 38)
                            return SonicBreak;

                        if ((IsEnabled(CustomComboPreset.GNB_ST_DoubleDown) || IsEnabled(CustomComboPreset.GNB_ST_TwoTarget)) && ActionReady(DoubleDown) && gauge.Ammo >= 2 &&
                            DoubleDownCD <= 0.4 && HasBattleTarget() && TargetDistance <= 5 && NoMercyCD >= 37)
                        {
                            if (gauge.Ammo == 3 || GetCooldownRemainingTime(Bloodfest) <= 5)
                                return DoubleDown;
                            if (GnashingFangCD > 8)
                                return DoubleDown;
                            if (GnashingFangCD > 5.5 && lastComboMove != SolidBarrel && !IsEnabled(CustomComboPreset.GNB_ST_TwoTarget))
                                return DoubleDown;
                            if (GnashingFangCD > 3 && lastComboMove is BrutalShell or DemonSlice)
                                return DoubleDown;
                        }

                        if (gauge.AmmoComboStep is 1 or 2)
                            return OriginalHook(GnashingFang);

                        if (IsEnabled(CustomComboPreset.GNB_ST_TwoTarget) && LevelChecked(FatedCircle) && gauge.Ammo > 0 && gauge.AmmoComboStep == 0 && TargetDistance <= 5)
                        {
                            if (gauge.Ammo == MaxCartridges(level) && lastComboMove is BrutalShell or DemonSlice)
                                return FatedCircle;

                            if (NoMercyCD >= 40)
                            {
                                if (LevelChecked(DoubleDown))
                                {
                                    if (GnashingFangCD > 5.5)
                                    {
                                        if (gauge.Ammo != 2 && GetCooldownRemainingTime(Bloodfest) <= 1.3)
                                            return FatedCircle;
                                        if (gauge.Ammo == 2 && GetCooldownRemainingTime(Bloodfest) <= 3.8 && DoubleDownCD >= 3)
                                            return FatedCircle;
                                        if (gauge.Ammo == 3 && IsOnCooldown(DoubleDown)) //&& DoubleDownCD > 10 && GnashingFangCD > 10)
                                            return FatedCircle;
                                        if (gauge.Ammo == 2 && DoubleDownCD > 7.5)
                                            return FatedCircle;
                                        if (DoubleDownCD > 40)
                                            return FatedCircle;
                                    }
                                    else
                                    {
                                        if (lastComboMove is DemonSlice or BrutalShell && GnashingFangCD > 3)
                                            return FatedCircle;
                                    }
                                }
                                else
                                {
                                    if (gauge.Ammo >= 2)
                                        return FatedCircle;
                                    if (gauge.Ammo == 1)
                                    {
                                        if (GnashingFangCD > 5.5)
                                            return FatedCircle;
                                        if (lastComboMove is BrutalShell or DemonSlice && GnashingFangCD > 3)
                                            return FatedCircle;
                                    }
                                }
                            }
                        }
                        
                        if (LevelChecked(BurstStrike) && gauge.Ammo >= 1 && gauge.AmmoComboStep == 0)
                        {
                            //·ÀÒç³ö
                            if (gauge.Ammo == MaxCartridges(level) && lastComboMove is BrutalShell or DemonSlice)
                                return BurstStrike;

                            if (NoMercyCD >= 40)
                            {
                                if (LevelChecked(DoubleDown))
                                {
                                    if (GnashingFangCD > 8)
                                    {
                                        if (gauge.Ammo != 2 && GetCooldownRemainingTime(Bloodfest) <= 1.3)
                                            return BurstStrike;
                                        if (gauge.Ammo == 2 && GetCooldownRemainingTime(Bloodfest) <= 3.8 && DoubleDownCD >= 3)
                                            return BurstStrike;
                                        if (gauge.Ammo == 3 && IsOnCooldown(DoubleDown)) //&& DoubleDownCD > 10 && GnashingFangCD > 10)
                                            return BurstStrike;
                                        if (gauge.Ammo == 2 && DoubleDownCD > 8)
                                            return BurstStrike;
                                        if (DoubleDownCD > 40)
                                            return BurstStrike;
                                    }
                                    else
                                    {
                                        if (GnashingFangCD > 5.5 && (lastComboMove != SolidBarrel || IsEnabled(CustomComboPreset.GNB_ST_TwoTarget)))
                                            return BurstStrike;
                                        if (GnashingFangCD > 3 && lastComboMove is BrutalShell or DemonSlice)
                                            return BurstStrike;
                                    }
                                }
                                else
                                {
                                    if (gauge.Ammo >= 2)
                                        return BurstStrike;
                                    if (gauge.Ammo == 1)
                                    {
                                        if (GnashingFangCD > 8)
                                            return BurstStrike;
                                        if (GnashingFangCD > 5.5 && (lastComboMove != SolidBarrel || IsEnabled(CustomComboPreset.GNB_ST_TwoTarget)))
                                            return BurstStrike;
                                        if (GnashingFangCD > 3 && lastComboMove is BrutalShell or DemonSlice)
                                            return BurstStrike;
                                    }
                                }
                            }

                            if (AllAttack)
                            {
                                if (gauge.Ammo >= 2)
                                {
                                    if (!LevelChecked(DoubleDown) || DoubleDownCD > 8)
                                        return BurstStrike;
                                    if (DoubleDownCD > 5.5 && (lastComboMove != SolidBarrel || IsEnabled(CustomComboPreset.GNB_ST_TwoTarget)))
                                        return BurstStrike;
                                    if (DoubleDownCD > 3 && lastComboMove is BrutalShell or DemonSlice)
                                        return BurstStrike;
                                }
                                if (gauge.Ammo == 1)
                                {
                                    if (GnashingFangCD > 8)
                                        return BurstStrike;
                                    if (GnashingFangCD > 5.5 && (lastComboMove != SolidBarrel || IsEnabled(CustomComboPreset.GNB_ST_TwoTarget)))
                                        return BurstStrike;
                                    if (GnashingFangCD > 3 && lastComboMove is BrutalShell or DemonSlice)
                                        return BurstStrike;
                                }
                            }
                        }
                    }

                    if (comboTime > 0 && lastComboMove == DemonSlice && LevelChecked(DemonSlaughter) && InActionRange(DemonSlaughter))
                        return DemonSlaughter;
                    if (IsEnabled(CustomComboPreset.GNB_ST_TwoTarget) && lastComboMove != BrutalShell && LevelChecked(DemonSlice) && InActionRange(DemonSlice))
                        return DemonSlice;

                    // Regular 1-2-3 combo with overcap feature
                    if (comboTime > 0)
                    {
                        if (lastComboMove == KeenEdge && LevelChecked(BrutalShell))
                            return BrutalShell;
                        if (lastComboMove == BrutalShell && LevelChecked(SolidBarrel))
                            return SolidBarrel;
                    }
                }
                return actionID;
            }
        }

        internal class GNB_AoE_MainCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GNB_AoE_MainCombo;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {

                if (actionID == DemonSlice)
                {
                    var gauge = GetJobGauge<GNBGauge>();
                    var NoMercyCD = GetCooldownRemainingTime(NoMercy);

                    if (IsEnabled(CustomComboPreset.GNB_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.GNB_VariantCure))
                        return Variant.VariantCure;

                    if (InCombat())
                    {
                        if (CanWeave(actionID))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.GNB_Variant_SpiritDart) &&
                                IsEnabled(Variant.VariantSpiritDart) &&
                                (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;

                            if (IsEnabled(CustomComboPreset.GNB_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                                return Variant.VariantUltimatum;

                            if (ActionReady(DangerZone))
                                return OriginalHook(BlastingZone);
                            if (IsEnabled(CustomComboPreset.GNB_NoMercy) && ActionReady(NoMercy))
                                return NoMercy;
                            if (IsEnabled(CustomComboPreset.GNB_AoE_BowShock) && ActionReady(BowShock) && NoMercyCD >= 12.5)
                                return BowShock;
                            if (gauge.Ammo == 0 && ActionReady(Bloodfest) && HasBattleTarget() && NoMercyCD >= 40)
                                return Bloodfest;
                        }

                        if (IsEnabled(CustomComboPreset.GNB_AoE_DoubleDown) && gauge.Ammo >= 2 && ActionReady(DoubleDown))
                            return DoubleDown;
                        if (LevelChecked(FatedCircle) && gauge.Ammo != 0 && (GetCooldownRemainingTime(Bloodfest) < 6 || NoMercyCD >= 40))
                            return FatedCircle;
                    }

                    if (comboTime > 0 && lastComboMove == DemonSlice && LevelChecked(DemonSlaughter))
                        return (LevelChecked(FatedCircle) && gauge.Ammo == MaxCartridges(level)) ? FatedCircle : DemonSlaughter;
                    
                }
                return actionID;
            }
        }

        internal class GNB_AuroraProtection : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GNB_AuroraProtection;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Aurora)
                {
                    if (HasEffect(Buffs.Aurora) && ActionReady(All.LowBlow))
                        return All.LowBlow;
                }
                return actionID;
            }
        }

        internal class GNB_BloodFest_NoMercy : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GNB_BloodFest_NoMercy;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is NoMercy)
                {
                    if (ActionReady(Bloodfest) && CombatEngageDuration().TotalSeconds <= 20 && gauge.Ammo == 0)
                        return Bloodfest;
                }
                return actionID;
            }
        }
    }
}