using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using XIVSlothCombo.Combos.JobHelpers;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Extensions;

namespace XIVSlothCombo.Combos.PvE
{
    internal class DRG
    {
        public const byte ClassID = 4;
        public const byte JobID = 22;

        public const uint
            PiercingTalon = 90,
            ElusiveJump = 94,
            LanceCharge = 85,
            DragonSight = 7398,
            BattleLitany = 3557,
            Jump = 92,
            LifeSurge = 83,
            HighJump = 16478,
            MirageDive = 7399,
            BloodOfTheDragon = 3553,
            Stardiver = 16480,
            CoerthanTorment = 16477,
            DoomSpike = 86,
            SonicThrust = 7397,
            ChaosThrust = 88,
            RaidenThrust = 16479,
            TrueThrust = 75,
            Disembowel = 87,
            FangAndClaw = 3554,
            WheelingThrust = 3556,
            FullThrust = 84,
            VorpalThrust = 78,
            WyrmwindThrust = 25773,
            DraconianFury = 25770,
            ChaoticSpring = 25772,
            DragonfireDive = 96,
            SpineshatterDive = 95,
            Geirskogul = 3555,
            Nastrond = 7400,
            HeavensThrust = 25771;

        public static class Buffs
        {
            public const ushort
                LanceCharge = 1864,
                RightEye = 1910,
                BattleLitany = 786,
                SharperFangAndClaw = 802,
                EnhancedWheelingThrust = 803,
                DiveReady = 1243,
                RaidenThrustReady = 1863,
                PowerSurge = 2720,
                LifeSurge = 116,
                DraconianFire = 1863;
        }

        public static class Debuffs
        {
            public const ushort
                ChaosThrust = 118,
                ChaoticSpring = 2719;
        }

        public static class Traits
        {
            public const uint
                EnhancedSpineshatterDive = 436,
                EnhancedLifeSurge = 438;
        }

        public static class Config
        {
            public static UserInt
                DRG_ST_SpineshatterDive_HeldCharges = new("DRG_ST_SpineshatterDive_HeldCharges"),
                DRG_Variant_Cure = new("DRG_VariantCure"),
                DRG_AoE_SecondWind_Threshold = new("DRG_AoESecondWindThreshold"),
                DRG_AoE_Bloodbath_Threshold = new("DRG_AoEBloodbathThreshold"), 
                DRG_Dives_Range = new("DRG_Dives_Range");
        }

        internal class DRG_ST_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRG_ST_AdvancedMode;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                //bool canw = AnimationLock.CanDRGWeave(LifeSurge);
                DRGGauge? gauge = GetJobGauge<DRGGauge>();
                var diveRange = IsEnabled(CustomComboPreset.DRG_Dives) ? (IsEnabled(CustomComboPreset.DRG_Dives_Range) ? Config.DRG_Dives_Range : 1) : -1;
                var LanceChargeCD = GetCooldownRemainingTime(LanceCharge);
                var SpineshatterDive_HeldCharges = IsEnabled(CustomComboPreset.DRG_ST_Spineshatter) ? Config.DRG_ST_SpineshatterDive_HeldCharges : 2;
                bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                Status? ChaosDoTDebuff;
                if (LevelChecked(ChaoticSpring))
                    ChaosDoTDebuff = FindTargetEffect(Debuffs.ChaoticSpring);
                else ChaosDoTDebuff = FindTargetEffect(Debuffs.ChaosThrust);

                if (actionID is TrueThrust)
                {
                    if (!InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.DRG_Variant_Cure) && IsEnabled(Variant.VariantCure) && GetBuffRemainingTime(Variant.Buffs.Rehabilitation) <= 5)
                            return Variant.VariantCure;
                        if (IsEnabled(CustomComboPreset.MNK_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart))
                            return Variant.VariantRampart;
                    }

                    if (HasEffect(Buffs.DiveReady))
                    {
                        if (GetBuffRemainingTime(Buffs.DiveReady) <= 1.2)
                            return MirageDive;
                        if (GetBuffRemainingTime(Buffs.DiveReady) <= 2 && AnimationLock.CanDRGWeave(MirageDive))
                            return MirageDive;
                    }
                    if (IsEnabled(CustomComboPreset.DRG_Dives) && ActionReady(Stardiver) && gauge.IsLOTDActive is true && GetTargetDistance() <= diveRange)
                    {
                        if (gauge.LOTDTimer <= 2000)
                            return Stardiver;
                        if (gauge.LOTDTimer <= 5000 && CanWeave(actionID, 0.6))
                            return Stardiver;
                        if (LanceChargeCD >= 40 && LanceChargeCD <= 42 && CanWeave(actionID, 0.01))
                            return Stardiver;
                    }
                    if (gauge.FirstmindsFocusCount is 2 && ((HasEffect(Buffs.EnhancedWheelingThrust) && WasLastWeaponskill(FangAndClaw)) || (HasEffect(Buffs.SharperFangAndClaw) && WasLastWeaponskill(WheelingThrust))) && CanWeave(actionID))
                        return WyrmwindThrust;

                    if (InCombat() && HasEffect(Buffs.PowerSurge))
                    {
                        if (IsEnabled(CustomComboPreset.DRG_ST_JumpAndGeirskogul))
                        {
                            if (ActionReady(OriginalHook(Jump)) && LanceChargeCD >= 5 && (GetCooldownRemainingTime(DragonSight) > 2.5 || !LevelChecked(DragonSight)) &&
                                (!LevelChecked(BattleLitany) || GetCooldownRemainingTime(BattleLitany) > 2.5) && CanWeave(actionID, 0.45))
                                return OriginalHook(Jump);
                            if (ActionReady(Geirskogul) && LanceChargeCD >= 5 && (GetCooldownRemainingTime(DragonSight) > 2.5 || !LevelChecked(DragonSight)) &&
                                (!LevelChecked(BattleLitany) || GetCooldownRemainingTime(BattleLitany) > 2.5) && CanWeave(actionID, 0.6))
                                return OriginalHook(Geirskogul);
                        }

                        if (CanWeave(actionID))
                        {
                            if (IsEnabled(CustomComboPreset.DRG_Lance) && ActionReady(LanceCharge))
                                return LanceCharge;
                            if (IsEnabled(CustomComboPreset.DRG_ST_DragonSight) && ActionReady(DragonSight) && LanceChargeCD >= 40)
                                return DragonSight;
                            if (ActionReady(BattleLitany) && (GetCooldownRemainingTime(DragonSight) >= 100 || !LevelChecked(DragonSight)))
                                return BattleLitany;

                            if (!IsMoving && GetTargetDistance() <= diveRange && (LanceChargeCD >= 40 || AllAttack))
                            {
                                if (ActionReady(DragonfireDive) && AnimationLock.CanDRGWeave(DragonfireDive))
                                    return DragonfireDive;
                                if (ActionReady(Stardiver) && gauge.IsLOTDActive is true && AnimationLock.CanDRGWeave(Stardiver))
                                    return Stardiver;
                            }

                            if (ActionReady(Nastrond) && gauge.LOTDTimer <= 22500 && gauge.LOTDTimer > 20000)
                                return OriginalHook(Geirskogul);
                            if (HasEffect(Buffs.DiveReady) && (GetCooldownRemainingTime(Geirskogul) >= 5 || !LevelChecked(Geirskogul)))
                                return MirageDive;

                            if (ActionReady(LifeSurge) && !HasEffect(Buffs.LifeSurge))
                            {
                                if (level >= 88)
                                {
                                    if (GetRemainingCharges(LifeSurge) == 2 && (HasEffect(Buffs.EnhancedWheelingThrust) || HasEffect(Buffs.SharperFangAndClaw)))
                                        return LifeSurge;

                                    if (LanceChargeCD >= 40)
                                    {
                                        if (WasLastWeaponskill(VorpalThrust))
                                            return LifeSurge;
                                        if ((HasEffect(Buffs.EnhancedWheelingThrust) && WasLastWeaponskill(FangAndClaw)) || (HasEffect(Buffs.SharperFangAndClaw) && WasLastWeaponskill(WheelingThrust)))
                                        {
                                            if (GetRemainingCharges(LifeSurge) == 2)
                                                return LifeSurge;
                                            if (GetCooldownChargeRemainingTime(LifeSurge) >= 6)
                                                return LifeSurge;
                                        }
                                    }
                                    //120sÇ°
                                    if (WasLastWeaponskill(VorpalThrust) && IsOnCooldown(DragonSight) && GetCooldownRemainingTime(DragonSight) >= 1.5)
                                        return LifeSurge;
                                }

                                //µÍÓÚ88¼¶
                                if (level < 88 || AllAttack)
                                {
                                    if (LevelChecked(FullThrust) && lastComboMove is VorpalThrust)
                                        return LifeSurge;
                                    if (HasEffect(Buffs.EnhancedWheelingThrust) || HasEffect(Buffs.SharperFangAndClaw))
                                        return LifeSurge;
                                    if (!LevelChecked(FullThrust) && lastComboMove is TrueThrust)
                                        return LifeSurge;
                                }
                            }

                            if (IsEnabled(CustomComboPreset.DRG_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart))
                                return Variant.VariantRampart;

                            if (ActionReady(Nastrond) && gauge.IsLOTDActive)
                                return OriginalHook(Geirskogul);
                            if (ActionReady(Stardiver) && gauge.IsLOTDActive is true && !IsMoving && GetTargetDistance() <= diveRange && (LanceChargeCD > gauge.LOTDTimer / 1000 - 5) &&
                                AnimationLock.CanDRGWeave(Stardiver))
                                return Stardiver;
                            if (gauge.FirstmindsFocusCount is 2 && (LanceChargeCD >= 12 || AllAttack))
                                return WyrmwindThrust;
                        }
                        //Dives Feature
                        if (ActionReady(SpineshatterDive) && (GetRemainingCharges(SpineshatterDive) > SpineshatterDive_HeldCharges || AllAttack) &&
                            !IsMoving && GetTargetDistance() <= diveRange && (LanceChargeCD >= 40 || AllAttack) && 
                            AnimationLock.CanDRGWeave(SpineshatterDive))
                            return SpineshatterDive;
                    }

                    if (IsEnabled(CustomComboPreset.DRG_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= Config.DRG_Variant_Cure)
                        return Variant.VariantCure;

                    //1-2-3 Combo
                    if (HasEffect(Buffs.SharperFangAndClaw))
                        return FangAndClaw;
                    if (HasEffect(Buffs.EnhancedWheelingThrust))
                        return WheelingThrust;
                    if (lastComboMove is Disembowel && LevelChecked(ChaosThrust))
                        return OriginalHook(ChaosThrust);
                    if (lastComboMove is VorpalThrust && LevelChecked(FullThrust))
                        return OriginalHook(FullThrust);

                    if (!AllAttack && ((LevelChecked(ChaosThrust) && (ChaosDoTDebuff is null || ChaosDoTDebuff.RemainingTime < 6)) || GetBuffRemainingTime(Buffs.PowerSurge) < 10))
                    {
                        if (lastComboMove is TrueThrust or RaidenThrust && LevelChecked(Disembowel))
                            return Disembowel;
                    }
                    if (lastComboMove is TrueThrust or RaidenThrust && LevelChecked(VorpalThrust))
                        return VorpalThrust;
                }
                return actionID;
            }
        }

        internal class DRG_AOE_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRG_AOE_AdvancedMode;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                DRGGauge? gauge = GetJobGauge<DRGGauge>();
                var LanceChargeCD = GetCooldownRemainingTime(LanceCharge);

                if (actionID is DoomSpike)
                {
                    if (!InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.DRG_Variant_Cure) && IsEnabled(Variant.VariantCure) && GetBuffRemainingTime(Variant.Buffs.Rehabilitation) <= 5)
                            return Variant.VariantCure;
                        if (IsEnabled(CustomComboPreset.DRG_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart))
                            return Variant.VariantRampart;
                    }

                    if (HasEffect(Buffs.DiveReady))
                    {
                        if (GetBuffRemainingTime(Buffs.DiveReady) <= 1.2)
                            return MirageDive;
                        if (GetBuffRemainingTime(Buffs.DiveReady) <= 2 && AnimationLock.CanDRGWeave(MirageDive))
                            return MirageDive;
                    }
                    if (IsEnabled(CustomComboPreset.DRG_Dives) && ActionReady(Stardiver) && gauge.IsLOTDActive is true)
                    {
                        if (gauge.LOTDTimer <= 2000)
                            return Stardiver;
                        if (gauge.LOTDTimer <= 5000 && CanWeave(actionID, 0.6))
                            return Stardiver;
                        if (LanceChargeCD >= 40 && LanceChargeCD <= 42 && CanWeave(actionID, 0.01))
                            return Stardiver;
                    }

                    if (InCombat() && HasEffect(Buffs.PowerSurge))
                    {
                        if (HasEffect(Buffs.DiveReady) && (GetCooldownRemainingTime(Geirskogul) >= 5 || !LevelChecked(Geirskogul)))
                            return MirageDive;
                        if (ActionReady(OriginalHook(Jump)) && LanceChargeCD >= 5 && CanWeave(actionID, 0.45))
                            return OriginalHook(Jump);
                        if (ActionReady(Geirskogul) && LanceChargeCD > 10 && CanWeave(actionID, 0.6))
                            return OriginalHook(Geirskogul);

                        if (CanWeave(actionID))
                        {
                            if (IsEnabled(CustomComboPreset.DRG_Lance) && ActionReady(LanceCharge))
                                return LanceCharge;
                            if (ActionReady(DragonSight) && LanceChargeCD >= 40)
                                return DragonSight;
                            if (ActionReady(BattleLitany) && (GetCooldownRemainingTime(DragonSight) >= 100 || !LevelChecked(DragonSight)))
                                return BattleLitany;

                            if (IsEnabled(CustomComboPreset.DRG_Dives))
                            {
                                if (ActionReady(DragonfireDive) && LanceChargeCD >= 40 && AnimationLock.CanDRGWeave(DragonfireDive))
                                    return DragonfireDive;
                                if (ActionReady(Stardiver) && gauge.IsLOTDActive is true && AnimationLock.CanDRGWeave(Stardiver) &&
                                    (LanceChargeCD >= 40 || (LanceChargeCD > gauge.LOTDTimer / 1000 - 5)))
                                    return Stardiver;
                            }
                            
                            if (ActionReady(Nastrond) && gauge.LOTDTimer <= 22500 && gauge.LOTDTimer > 20000)
                                return OriginalHook(Geirskogul);
                            
                            if (ActionReady(LifeSurge) && !HasEffect(Buffs.LifeSurge))
                            {
                                if (LevelChecked(CoerthanTorment) && lastComboMove is SonicThrust)
                                    return LifeSurge;
                                if (!LevelChecked(CoerthanTorment) && lastComboMove is DoomSpike)
                                    return LifeSurge;
                                if (!LevelChecked(SonicThrust))
                                    return LifeSurge;
                            }
                            if (ActionReady(Nastrond) && gauge.IsLOTDActive)
                                return OriginalHook(Geirskogul);
                            if (IsEnabled(CustomComboPreset.DRG_Variant_Rampart) && IsEnabled(Variant.VariantRampart) &&
                                IsOffCooldown(Variant.VariantRampart))
                                return Variant.VariantRampart;
                            if (gauge.FirstmindsFocusCount is 2)
                                return WyrmwindThrust;
                        }
                    }
                    if (ActionReady(SpineshatterDive) && IsEnabled(CustomComboPreset.DRG_AoE_Spineshatter_Dive) && LanceChargeCD >= 40 && !IsMoving && AnimationLock.CanDRGWeave(SpineshatterDive))
                        return SpineshatterDive;

                    // healing
                    if (IsEnabled(CustomComboPreset.DRG_AoE_ComboHeals))
                    {
                        if (PlayerHealthPercentageHp() <= Config.DRG_AoE_SecondWind_Threshold && ActionReady(All.SecondWind))
                            return All.SecondWind;

                        if (PlayerHealthPercentageHp() <= Config.DRG_AoE_Bloodbath_Threshold && ActionReady(All.Bloodbath))
                            return All.Bloodbath;
                    }

                    if (IsEnabled(CustomComboPreset.DRG_Variant_Cure) &&
                        IsEnabled(Variant.VariantCure) &&
                        PlayerHealthPercentageHp() <= Config.DRG_Variant_Cure)
                        return Variant.VariantCure;

                    if (comboTime > 0)
                    {
                        if (!SonicThrust.LevelChecked())
                        {
                            if (lastComboMove == TrueThrust)
                                return Disembowel;

                            if (lastComboMove == Disembowel && OriginalHook(ChaosThrust).LevelChecked())
                                return OriginalHook(ChaosThrust);
                        }
                        else
                        {
                            if (lastComboMove is DoomSpike or DraconianFury)
                                return SonicThrust;

                            if (lastComboMove == SonicThrust && CoerthanTorment.LevelChecked())
                                return CoerthanTorment;
                        }
                    }

                    return HasEffect(Buffs.PowerSurge) || SonicThrust.LevelChecked() ? OriginalHook(DoomSpike) : OriginalHook(TrueThrust);

                }

                return actionID;
            }
        }

        internal class DRG_JumpFeature : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRG_Jump;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) =>
                actionID is Jump or HighJump && HasEffect(Buffs.DiveReady) ? MirageDive : actionID;
        }
    }
}

