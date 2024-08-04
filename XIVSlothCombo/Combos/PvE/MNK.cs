using System;
using System.Linq;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using Lumina.Excel.GeneratedSheets2;
using XIVSlothCombo.Combos.JobHelpers;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Extensions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class MNK
    {
        public const byte ClassID = 2;
        public const byte JobID = 20;

        public const uint
            Mantra = 65,
            Bootshine = 53,
            DragonKick = 74,
            SnapPunch = 56,
            TwinSnakes = 61,
            Demolish = 66,
            ArmOfTheDestroyer = 62,
            Rockbreaker = 70,
            FourPointFury = 16473,
            PerfectBalance = 69,
            TrueStrike = 54,
            Meditation = 3546,
            HowlingFist = 25763,
            Enlightenment = 16474,
            MasterfulBlitz = 25764,
            ElixirField = 3545,
            FlintStrike = 25882,
            RisingPhoenix = 25768,
            ShadowOfTheDestroyer = 25767,
            RiddleOfFire = 7395,
            RiddleOfWind = 25766,
            Brotherhood = 7396,
            ForbiddenChakra = 3546,
            FormShift = 4262,
            Thunderclap = 25762;

        public static class Buffs
        {
            public const ushort
                TwinSnakes = 101,
                OpoOpoForm = 107,
                RaptorForm = 108,
                CoerlForm = 109,
                PerfectBalance = 110,
                RiddleOfFire = 1181,
                LeadenFist = 1861,
                FormlessFist = 2513,
                DisciplinedFist = 3001,
                Brotherhood = 1185;
        }

        public static class Debuffs
        {
            public const ushort
                Demolish = 246;
        }

        public static class Config
        {
            public static UserInt
                MNK_AoESecondWindThreshold = new("MNK_AoESecondWindThreshold"),
                MNK_AoEBloodbathThreshold = new("MNK_AoEBloodbathThreshold"),
                MNK_VariantCure = new("MNK_VariantCure");
        }

        internal class MNK_ST_SimpleMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MNK_ST_SimpleMode;

            internal static bool inOpener = false;
            internal static bool openerFinished = false;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == Bootshine)
                {
                    var gauge = GetJobGauge<MNKGauge>();
                    var twinsnakeDuration = GetBuffRemainingTime(Buffs.DisciplinedFist);
                    var demolishDuration = GetDebuffRemainingTime(Debuffs.Demolish);
                    var RiddleOfFireCD = GetCooldownRemainingTime(RiddleOfFire);

                    if (!InCombat())
                    {
                        if (LevelChecked(FormShift) && !HasEffect(Buffs.FormlessFist) && comboTime <= 0)
                            return FormShift;
                        if (LevelChecked(Meditation) && gauge.Chakra < 5)
                            return Meditation;

                        if (IsEnabled(CustomComboPreset.DRG_Variant_Cure) && IsEnabled(Variant.VariantCure) && GetBuffRemainingTime(Variant.Buffs.Rehabilitation) <= 5)
                            return Variant.VariantCure;
                        if (IsEnabled(CustomComboPreset.MNK_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart))
                            return Variant.VariantRampart;
                    }

                    if (!HasBattleTarget() && GetBuffStacks(Buffs.PerfectBalance) == 1 && GetBuffRemainingTime(Buffs.PerfectBalance) <= 1)
                        return OriginalHook(ArmOfTheDestroyer);

                    if (LevelChecked(Meditation) && InCombat() && gauge.Chakra < 5 && IsMoving)
                    {
                        if (!HasBattleTarget())
                            return Meditation;
                        if (GetTargetDistance() >= 4.5)
                            return Meditation;
                    }

                    if (InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.MNK_Bozja_LostAssassination) && IsEnabled(Bozja.LostAssassination) &&
                            HasEffect(Bozja.Buffs.BeastEssence) && !HasEffect(Bozja.Buffs.FontOfPower) && GetTargetDistance() <= 3 && EnemyHealthCurrentHp() > 17500 * 3 * 18)
                            return Bozja.LostAssassination;

                        if (ActionReady(RiddleOfFire) && IsEnabled(CustomComboPreset.MNK_RiddleOfFire) && HasEffect(Buffs.DisciplinedFist) && CanDelayedWeave(actionID, 1.35, 0.55))
                            return RiddleOfFire;

                        if (CanWeave(actionID, 0.55))
                        {
                            if (IsEnabled(CustomComboPreset.MNK_Bozja_LostFontofPowerThenBannerOfHonoredSacrifice) && IsEnabled(Bozja.FontOfPower) && IsEnabled(Bozja.BannerOfHonoredSacrifice) &&
                                IsOffCooldown(Bozja.BannerOfHonoredSacrifice) && (HasEffect(Bozja.Buffs.FontOfPower) || WasLastAbility(Bozja.FontOfPower)) && GetTargetDistance() <= 3)
                                return Bozja.BannerOfHonoredSacrifice;
                            if (IsEnabled(CustomComboPreset.MNK_Bozja_LostRendArmor) && IsEnabled(Bozja.LostRendArmor) && IsOffCooldown(Bozja.LostRendArmor) &&
                                !TargetHasEffectAny(Bozja.Debuffs.LostRendArmor) && EnemyHealthCurrentHp() > 17500 * 3 * 30 && GetTargetDistance() <= 4)
                                return Bozja.LostRendArmor;

                            if (IsEnabled(CustomComboPreset.MNK_ST_PerfectBalance) && ActionReady(PerfectBalance) && !HasEffect(Buffs.PerfectBalance) && !HasEffect(Buffs.FormlessFist) &&
                                (OriginalHook(MasterfulBlitz) == MasterfulBlitz || !LevelChecked(MasterfulBlitz)) && (lastComboMove == Bootshine || lastComboMove == DragonKick))
                            {
                                if (IsOnCooldown(RiddleOfFire) && (RiddleOfFireCD <= 3 || RiddleOfFireCD >= 44))//46.6
                                {
                                    //尽量打阴必杀
                                    if (twinsnakeDuration > 9)
                                        return PerfectBalance;
                                    //有2个震脚
                                    if (GetRemainingCharges(PerfectBalance) >= 1 && GetCooldownChargeRemainingTime(PerfectBalance) <= 10)
                                        return PerfectBalance;
                                    if (GetRemainingCharges(PerfectBalance) == 1 && GetCooldownChargeRemainingTime(PerfectBalance) >= 40 - 14.8)
                                        return PerfectBalance;
                                    //
                                    if (RiddleOfFireCD < 51.5 && RiddleOfFireCD >= 44)
                                        return PerfectBalance;
                                }
                                if (GetRemainingCharges(PerfectBalance) == 2 && RiddleOfFireCD <= 24.5 && IsOnCooldown(RiddleOfFire))
                                    return PerfectBalance;
                                if (!LevelChecked(RiddleOfFire) && twinsnakeDuration >= 8 && demolishDuration >= 6.6)
                                    return PerfectBalance;
                            }
                        }

                        if (IsEnabled(CustomComboPreset.MNK_ST_TwoTarget) &&
                            LevelChecked(HowlingFist) && gauge.Chakra == 5 && (twinsnakeDuration > 0 || !LevelChecked(TwinSnakes)) && (!LevelChecked(RiddleOfFire) || RiddleOfFireCD >= 4))
                        {
                            if (RiddleOfFireCD >= 40 && CanWeave(actionID, 0.67))
                                return OriginalHook(HowlingFist);
                            if (RiddleOfFireCD < 40)
                            {
                                if (lastComboMove is Demolish or SnapPunch && HasEffect(Buffs.LeadenFist) && CanWeave(actionID, 0.35))
                                    return OriginalHook(HowlingFist);
                                if (!(lastComboMove is Demolish or SnapPunch && HasEffect(Buffs.LeadenFist)) && CanWeave(actionID, 0.6))
                                    return OriginalHook(HowlingFist);
                            }
                        }

                        if (LevelChecked(Meditation) && gauge.Chakra == 5 && (twinsnakeDuration > 0 || !LevelChecked(TwinSnakes)) && (!LevelChecked(RiddleOfFire) || RiddleOfFireCD >= 4))
                        {
                            if (RiddleOfFireCD >= 40 && CanWeave(actionID, 0.67))
                                return OriginalHook(Meditation);
                            if (RiddleOfFireCD < 40)
                            {
                                if (lastComboMove is Demolish or SnapPunch && HasEffect(Buffs.LeadenFist) && CanWeave(actionID, 0.35))
                                    return OriginalHook(Meditation);
                                if (!(lastComboMove is Demolish or SnapPunch && HasEffect(Buffs.LeadenFist)) && CanWeave(actionID, 0.6))
                                    return OriginalHook(Meditation);
                            }
                        }

                        if (IsEnabled(CustomComboPreset.MNK_ST_Brotherhood) && ActionReady(Brotherhood) && RiddleOfFireCD >= 40 && CanWeave(actionID, 0.67))
                            return Brotherhood;
                        if (IsEnabled(CustomComboPreset.MNK_ST_RiddleOfWind) && ActionReady(RiddleOfWind) && RiddleOfFireCD >= 12 && CanWeave(actionID, 0.67))
                            return RiddleOfWind;

                        if (IsEnabled(CustomComboPreset.MNK_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart) && CanWeave(actionID))
                            return Variant.VariantRampart;
                    }

                    if (LevelChecked(MasterfulBlitz) && !HasEffect(Buffs.PerfectBalance) && OriginalHook(MasterfulBlitz) != MasterfulBlitz && HasBattleTarget() &&
                        (RiddleOfFireCD >= 19 || gauge.BlitzTimeRemaining <= 5000 || !LevelChecked(RiddleOfFire)) && twinsnakeDuration > 0 && GetTargetDistance() <= 5)
                        return OriginalHook(MasterfulBlitz);

                    // Perfect Balance
                    if (HasEffect(Buffs.PerfectBalance))
                    {
                        var lunarNadi = gauge.Nadi == Nadi.LUNAR;
                        var solarNadi = gauge.Nadi == Nadi.SOLAR;
                        bool opoopoChakra = Array.Exists(gauge.BeastChakra, e => e == BeastChakra.OPOOPO);//连击123的1
                        bool coeurlChakra = Array.Exists(gauge.BeastChakra, e => e == BeastChakra.COEURL);//3
                        bool raptorChakra = Array.Exists(gauge.BeastChakra, e => e == BeastChakra.RAPTOR);//2
                        bool canSolar = gauge.BeastChakra.Where(e => e == BeastChakra.OPOOPO).Count() != 2;
                        //solarNadi指的是之前搓了个阳球，+阴lunarNadi=梦幻斗舞

                        if (twinsnakeDuration <= 2)
                            return TwinSnakes;
                        if (demolishDuration <= 2)
                            return Demolish;
                        if (!opoopoChakra)
                            return HasEffect(Buffs.LeadenFist) ? Bootshine : DragonKick;
                        if (solarNadi && lunarNadi)
                        {
                            if (demolishDuration <= 3.68)
                                return Demolish;
                            if (twinsnakeDuration <= 4)
                                return TwinSnakes;
                            return HasEffect(Buffs.LeadenFist) ? Bootshine : DragonKick;
                        }
                        if (solarNadi && !lunarNadi)
                            return HasEffect(Buffs.LeadenFist) ? Bootshine : DragonKick;

                        if (!solarNadi)// && canSolar)
                        {
                            if (lunarNadi)
                            {
                                if (!coeurlChakra && demolishDuration <= 6.93)
                                    return Demolish;
                                if (!raptorChakra && twinsnakeDuration <= 6.6)
                                    return TwinSnakes;
                            }

                            if (!lunarNadi && canSolar)
                            {
                                if (coeurlChakra && !raptorChakra)
                                    return TwinSnakes;
                                //连续要打2个震脚时
                                if (!coeurlChakra && RiddleOfFireCD >= 42 && (GetRemainingCharges(PerfectBalance) == 1 || GetCooldownChargeRemainingTime(PerfectBalance) <= 7.4))
                                {
                                    if (demolishDuration <= 8.4)
                                        return Demolish;
                                    if (twinsnakeDuration <= 6.6)
                                        return SnapPunch;
                                }
                                if (raptorChakra && !coeurlChakra)
                                    return demolishDuration <= 5.08 ? Demolish : SnapPunch;
                                if (demolishDuration <= 4.9 || twinsnakeDuration <= 5.8)
                                    return Demolish;
                            }
                        }

                        //保底
                        return HasEffect(Buffs.LeadenFist) ? Bootshine : DragonKick;
                    }

                    if (IsEnabled(CustomComboPreset.MNK_Variant_Cure) &&
                        IsEnabled(Variant.VariantCure) &&
                        PlayerHealthPercentageHp() <= Config.MNK_VariantCure)
                        return Variant.VariantCure;

                    if (InCombat() && IsOnCooldown(PerfectBalance) && HasEffect(Buffs.FormlessFist))
                    {
                        if (LevelChecked(TwinSnakes) && twinsnakeDuration == 0)
                            return TwinSnakes;
                        if (LevelChecked(Demolish) && demolishDuration <= 2.25)
                            return Demolish;
                    }

                    if (RiddleOfFireCD >= 45 && IsOffCooldown(PerfectBalance) && HasEffect(Buffs.RaptorForm))
                        return TwinSnakes;
                    // Monk Rotation
                    if ((LevelChecked(DragonKick) && HasEffect(Buffs.OpoOpoForm)) || HasEffect(Buffs.FormlessFist))
                        return HasEffect(Buffs.LeadenFist) ? Bootshine : DragonKick;
                    if (LevelChecked(TrueStrike) && HasEffect(Buffs.RaptorForm))
                        return (LevelChecked(TwinSnakes) && twinsnakeDuration <= 6.7) ? TwinSnakes : TrueStrike;
                    if (LevelChecked(SnapPunch) && HasEffect(Buffs.CoerlForm))
                        return (!IsEnabled(CustomComboPreset.MNK_ST_NoDoT) && LevelChecked(Demolish) && demolishDuration <= 4.2 + 0.4) ? Demolish : SnapPunch;
                }
                return actionID;
            }
        }

        internal class MNK_AoE_SimpleMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MNK_AoE_SimpleMode;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == ArmOfTheDestroyer)
                {
                    var gauge = GetJobGauge<MNKGauge>();
                    var lunarNadi = gauge.Nadi == Nadi.LUNAR;
                    var nadiNONE = gauge.Nadi == Nadi.NONE;
                    var RiddleOfFireCD = GetCooldownRemainingTime(RiddleOfFire);

                    if (!InCombat())
                    {
                        if (LevelChecked(FormShift) && !HasEffect(Buffs.FormlessFist) && comboTime <= 0)
                            return FormShift;
                        if (LevelChecked(Meditation) && gauge.Chakra < 5)
                            return Meditation;

                        if (IsEnabled(CustomComboPreset.DRG_Variant_Cure) && IsEnabled(Variant.VariantCure) && GetBuffRemainingTime(Variant.Buffs.Rehabilitation) <= 5)
                            return Variant.VariantCure;
                        if (IsEnabled(CustomComboPreset.MNK_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart))
                            return Variant.VariantRampart;
                    }

                    if (LevelChecked(Meditation) && InCombat() && gauge.Chakra < 5)
                    {
                        if (GetTargetDistance() >= 4.5 && IsMoving)
                            return Meditation;
                    }

                    if (InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.MNK_Bozja_LostAssassination) && IsEnabled(Bozja.LostAssassination) &&
                            HasEffect(Bozja.Buffs.BeastEssence) && !HasEffect(Bozja.Buffs.FontOfPower)  && GetTargetDistance() <= 5 && EnemyHealthCurrentHp() > 17500 * 3 * 18)
                            return Bozja.LostAssassination;

                        if (ActionReady(RiddleOfFire) && IsEnabled(CustomComboPreset.MNK_RiddleOfFire) && CanDelayedWeave(actionID))
                            return RiddleOfFire;
                        if (ActionReady(HowlingFist) && gauge.Chakra == 5 && CanWeave(actionID))
                            return OriginalHook(HowlingFist);
                        if (ActionReady(Brotherhood) && RiddleOfFireCD >= 40 && CanWeave(actionID))
                            return Brotherhood;

                        if (CanWeave(actionID, 0.55))
                        {
                            if (IsEnabled(CustomComboPreset.MNK_Bozja_LostFontofPowerThenBannerOfHonoredSacrifice) && IsEnabled(Bozja.FontOfPower) && IsEnabled(Bozja.BannerOfHonoredSacrifice) &&
                                IsOffCooldown(Bozja.BannerOfHonoredSacrifice) && (HasEffect(Bozja.Buffs.FontOfPower) || WasLastAbility(Bozja.FontOfPower)) && GetTargetDistance() <= 5)
                                return Bozja.BannerOfHonoredSacrifice;

                            if (IsEnabled(CustomComboPreset.MNK_ST_PerfectBalance) && ActionReady(PerfectBalance) && !HasEffect(Buffs.PerfectBalance) && !HasEffect(Buffs.FormlessFist) &&
                                (OriginalHook(MasterfulBlitz) == MasterfulBlitz || !LevelChecked(MasterfulBlitz)) && GetBuffRemainingTime(Buffs.DisciplinedFist) >= 6.2 && 
                                (lastComboMove == (LevelChecked(ShadowOfTheDestroyer) ? ShadowOfTheDestroyer : Rockbreaker)))
                            {
                                if (IsOnCooldown(RiddleOfFire) && (RiddleOfFireCD <= 2.5 || RiddleOfFireCD >= 44))
                                    return PerfectBalance;
                                if (GetRemainingCharges(PerfectBalance) == 2)
                                    return PerfectBalance;
                                if (!LevelChecked(RiddleOfFire))
                                    return PerfectBalance;
                            }
                        }

                        if (ActionReady(RiddleOfWind) && RiddleOfFireCD >= 12 && CanWeave(actionID))
                            return RiddleOfWind;

                        if (ActionReady(Meditation) && gauge.Chakra == 5 && CanWeave(actionID))
                            return OriginalHook(Meditation);
                    }

                    if (IsEnabled(CustomComboPreset.MNK_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart) && CanWeave(actionID))
                        return Variant.VariantRampart;

                    // healing - please move if not appropriate this high priority
                    if (IsEnabled(CustomComboPreset.MNK_AoE_ComboHeals))
                    {
                        if (ActionReady(All.SecondWind) && PlayerHealthPercentageHp() <= Config.MNK_AoESecondWindThreshold)
                            return All.SecondWind;
                        if (ActionReady(All.Bloodbath) && PlayerHealthPercentageHp() <= Config.MNK_AoEBloodbathThreshold)
                            return All.Bloodbath;
                    }

                    // Masterful Blitz
                    if (LevelChecked(MasterfulBlitz) && !HasEffect(Buffs.PerfectBalance) && OriginalHook(MasterfulBlitz) != MasterfulBlitz && HasBattleTarget() &&
                        (RiddleOfFireCD >= 19 || gauge.BlitzTimeRemaining <= 5000 || !LevelChecked(RiddleOfFire)) && HasEffect(Buffs.DisciplinedFist) && GetTargetDistance() <= 5)
                        return OriginalHook(MasterfulBlitz);

                    // Perfect Balance
                    if (HasEffect(Buffs.PerfectBalance))
                    {
                        if (nadiNONE || !lunarNadi)
                            return LevelChecked(ShadowOfTheDestroyer) ? OriginalHook(ShadowOfTheDestroyer) : Rockbreaker;
                        if (lunarNadi)
                        {
                            switch (GetBuffStacks(Buffs.PerfectBalance))
                            {
                                case 3:
                                    return OriginalHook(ArmOfTheDestroyer);
                                case 2:
                                    return FourPointFury;
                                case 1:
                                    return Rockbreaker;
                            }
                        }
                    }

                    if (IsEnabled(CustomComboPreset.MNK_Variant_Cure) &&
                        IsEnabled(Variant.VariantCure) &&
                        PlayerHealthPercentageHp() <= Config.MNK_VariantCure)
                        return Variant.VariantCure;

                    if (HasEffect(Buffs.FormlessFist))
                        return LevelChecked(ShadowOfTheDestroyer) ? OriginalHook(ShadowOfTheDestroyer) : Rockbreaker;

                    // Monk Rotation
                    if (HasEffect(Buffs.OpoOpoForm))
                        return OriginalHook(ArmOfTheDestroyer);
                    if (HasEffect(Buffs.RaptorForm))
                    {
                        if (LevelChecked(FourPointFury))
                            return FourPointFury;
                        if (LevelChecked(TwinSnakes) && GetBuffRemainingTime(Buffs.DisciplinedFist) <= 4)
                            return TwinSnakes;
                    }
                    if (LevelChecked(Rockbreaker) && HasEffect(Buffs.CoerlForm))
                        return Rockbreaker;                    
                }
                return actionID;
            }
        }

        internal class MNK_PerfectBalance : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MNK_PerfectBalance;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is PerfectBalance)
                {
                    if (!HasBattleTarget() && HasEffect(Buffs.PerfectBalance))
                    {
                        var gauge = GetJobGauge<MNKGauge>();
                        var lunarNadi = gauge.Nadi == Nadi.LUNAR;
                        var solarNadi = gauge.Nadi == Nadi.SOLAR;
                        bool opoopoChakra = Array.Exists(gauge.BeastChakra, e => e == BeastChakra.OPOOPO);//连击123的1
                        bool coeurlChakra = Array.Exists(gauge.BeastChakra, e => e == BeastChakra.COEURL);//3
                        bool raptorChakra = Array.Exists(gauge.BeastChakra, e => e == BeastChakra.RAPTOR);//2

                        if (coeurlChakra && raptorChakra)
                            return OriginalHook(ArmOfTheDestroyer);
                        if (!coeurlChakra && raptorChakra)
                            return Rockbreaker;
                        if (!raptorChakra && coeurlChakra)
                            return FourPointFury;
                        
                        if (!lunarNadi && solarNadi)
                            return OriginalHook(ArmOfTheDestroyer);
                        return Rockbreaker;
                    }
                }
                return actionID;
            }
        }
    }
}
