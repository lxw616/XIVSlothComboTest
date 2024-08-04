using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using ECommons.DalamudServices;
using System.Linq;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;
using XIVSlothCombo.Extensions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class PLD
    {
        public const byte ClassID = 1;
        public const byte JobID = 19;

        public const float CooldownThreshold = 0.5f;

        public const uint
            Cover = 27,
            Clemency = 3541,
            FastBlade = 9,
            RiotBlade = 15,
            ShieldBash = 16,
            RageOfHalone = 21,
            CircleOfScorn = 23,
            ShieldLob = 24,
            SpiritsWithin = 29,
            GoringBlade = 3538,
            RoyalAuthority = 3539,
            TotalEclipse = 7381,
            Requiescat = 7383,
            HolySpirit = 7384,
            Prominence = 16457,
            HolyCircle = 16458,
            Confiteor = 16459,
            Expiacion = 25747,
            BladeOfFaith = 25748,
            BladeOfTruth = 25749,
            BladeOfValor = 25750,
            FightOrFlight = 20,
            Atonement = 16460,
            Intervene = 16461,
            Sheltron = 3542;

        public static class Buffs
        {
            public const ushort
                Requiescat = 1368,
                SwordOath = 1902,
                FightOrFlight = 76,
                ConfiteorReady = 3019,
                DivineMight = 2673,
                HolySheltron = 2674,
                Sheltron = 1856;
        }

        public static class Debuffs
        {
            public const ushort
                BladeOfValor = 2721,
                GoringBlade = 725;
        }

        private static PLDGauge Gauge => CustomComboFunctions.GetJobGauge<PLDGauge>();

        public static class Config
        {
            public static UserInt
                PLD_Intervene_HoldCharges = new("PLDKeepInterveneCharges"),
                PLD_VariantCure = new("PLD_VariantCure");
        }

        internal class PLD_ST_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PLD_ST_AdvancedMode;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is FastBlade)
                {
                    bool HolyspiritCheck = HolySpirit.LevelChecked() && GetResourceCost(HolySpirit) <= LocalPlayer.CurrentMp;
                    float FightOrFlightCD = GetCooldownRemainingTime(FightOrFlight);
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                    if (IsEnabled(CustomComboPreset.PLD_Variant_Cure) &&
                        IsEnabled(Variant.VariantCure) &&
                        PlayerHealthPercentageHp() <= GetOptionValue(Config.PLD_VariantCure))
                        return Variant.VariantCure;
                    if (CanWeave(actionID))
                    {
                        if (IsEnabled(CustomComboPreset.PLD_Variant_SpiritDart))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(Variant.VariantSpiritDart) &&
                            (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;
                        }

                        if (IsEnabled(CustomComboPreset.PLD_Variant_Ultimatum) &&
                            IsEnabled(Variant.VariantUltimatum) &&
                            IsOffCooldown(Variant.VariantUltimatum))
                            return Variant.VariantUltimatum;
                    }


                    if (!InCombat() && HolyspiritCheck)
                        return HolySpirit;

                    //oGCD
                    if (CanWeave(actionID))
                    {
                        if (IsEnabled(CustomComboPreset.PLD_FightorFlight) && ActionReady(FightOrFlight) &&
                        (WasLastWeaponskill(RoyalAuthority) || CombatEngageDuration().TotalSeconds > 20))
                            return OriginalHook(FightOrFlight);

                        if (ActionReady(Requiescat) && (FightOrFlightCD >= 40 || AllAttack))
                            return OriginalHook(Requiescat);
                        if (ActionReady(CircleOfScorn) && FightOrFlightCD >= 5 && GetTargetDistance() <= 5)
                            return OriginalHook(CircleOfScorn);
                        if (OriginalHook(SpiritsWithin).LevelChecked() && IsOffCooldown(OriginalHook(SpiritsWithin)) && FightOrFlightCD >= 5)
                            return OriginalHook(SpiritsWithin);
                        if (IsEnabled(CustomComboPreset.PLD_ST_AdvancedMode_Intervene) && OriginalHook(Intervene).LevelChecked() && FightOrFlightCD >= 40 &&
                                GetRemainingCharges(Intervene) > GetOptionValue(Config.PLD_Intervene_HoldCharges) && !IsMoving && GetTargetDistance() <= 1)
                            return OriginalHook(Intervene);
                    }

                    //GCD
                    if (HolyspiritCheck && GetTargetDistance() > 3 && (!HasEffect(Buffs.Requiescat) || !BladeOfFaith.LevelChecked()) && (HasEffect(Buffs.DivineMight) || !IsMoving))
                        return OriginalHook(HolySpirit);
                    if (ShieldLob.LevelChecked() && !InMeleeRange() && HasBattleTarget() && (!HasEffect(Buffs.Requiescat) || !BladeOfFaith.LevelChecked()))
                        return OriginalHook(ShieldLob);

                    
                    if ((HasEffect(Buffs.ConfiteorReady) || GetCooldownRemainingTime(Requiescat) >= 59.2) && (GetBuffRemainingTime(Buffs.ConfiteorReady) <= 12 || AllAttack) 
                        && GetResourceCost(OriginalHook(Confiteor)) <= LocalPlayer.CurrentMp)
                        return OriginalHook(Confiteor);

                    if (FightOrFlightCD >= 39)
                    {
                        if (GoringBlade.LevelChecked() && IsOffCooldown(GoringBlade) && InMeleeRange())
                            return OriginalHook(GoringBlade);
                        if ((HasEffect(Buffs.ConfiteorReady) || OriginalHook(Confiteor) != Confiteor || GetCooldownRemainingTime(Requiescat) >= 59.2) 
                            && GetResourceCost(OriginalHook(Confiteor)) <= LocalPlayer.CurrentMp)
                            return OriginalHook(Confiteor);
                        if (HolyspiritCheck && !HasEffect(Buffs.ConfiteorReady) && (HasEffect(Buffs.DivineMight) || HasEffect(Buffs.Requiescat)))
                            return OriginalHook(HolySpirit);
                    }

                    if (HolyspiritCheck &&  FightOrFlightCD < 40 && HasEffect(Buffs.DivineMight) && AllAttack)
                        return OriginalHook(HolySpirit);
                    if (HasEffectAny(Buffs.SwordOath) && Atonement.LevelChecked())
                        return OriginalHook(Atonement);
                    if (HolyspiritCheck && FightOrFlightCD < 40 && HasEffect(Buffs.DivineMight) && lastComboActionID == RiotBlade)
                        return OriginalHook(HolySpirit);

                    if (comboTime > 0)
                    {
                        if (lastComboActionID == OriginalHook(FastBlade) && RiotBlade.LevelChecked())
                            return OriginalHook(RiotBlade);
                        if (lastComboActionID == OriginalHook(RiotBlade) && OriginalHook(RoyalAuthority).LevelChecked())
                            return OriginalHook(RoyalAuthority);
                    }
                }
                return actionID;
            }
        }

        internal class PLD_AoE_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PLD_AoE_AdvancedMode;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is TotalEclipse)
                {
                    var FightOrFlightCD = GetCooldownRemainingTime(FightOrFlight);

                    if (IsEnabled(CustomComboPreset.PLD_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= Config.PLD_VariantCure)
                        return Variant.VariantCure;

                    if (CanWeave(actionID))
                    {
                        Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);

                        if (IsEnabled(CustomComboPreset.PLD_Variant_SpiritDart) && IsEnabled(Variant.VariantSpiritDart) &&
                            (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                            return Variant.VariantSpiritDart;
                        if (IsEnabled(CustomComboPreset.PLD_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                            return Variant.VariantUltimatum;

                        if (IsEnabled(CustomComboPreset.PLD_FightorFlight) && ActionReady(FightOrFlight))
                            return FightOrFlight;
                        if (ActionReady(Requiescat) && FightOrFlightCD >= 40)
                            return Requiescat;
                        if (ActionReady(CircleOfScorn) && FightOrFlightCD >= 10 && GetTargetDistance() <= 5)
                            return CircleOfScorn;
                        if (ActionReady(OriginalHook(SpiritsWithin)) && FightOrFlightCD >= 10)
                            return OriginalHook(SpiritsWithin);
                    }

                    // Actions under FoF burst
                    if (FightOrFlightCD >= 40)
                    {
                        if (HasEffect(Buffs.Requiescat))
                        {
                            // Confiteor & Blades
                            if ((HasEffect(Buffs.ConfiteorReady) || OriginalHook(Confiteor) != Confiteor) && GetResourceCost(OriginalHook(Confiteor)) <= LocalPlayer.CurrentMp)
                                return OriginalHook(Confiteor);
                            // HC when Confiteor not unlocked
                            if (GetResourceCost(HolyCircle) <= LocalPlayer.CurrentMp && LevelChecked(HolyCircle))
                                return HolyCircle;
                        }

                        // HC under DM/Req
                        if (HolyCircle.LevelChecked() &&  (HasEffect(Buffs.DivineMight) || HasEffect(Buffs.Requiescat)) && GetResourceCost(HolyCircle) <= LocalPlayer.CurrentMp)
                            return HolyCircle;
                    }

                    if (comboTime > 0 && lastComboActionID is TotalEclipse && Prominence.LevelChecked())
                        return Prominence;
                }
                return actionID;
            }
        }
        internal class PLD_Cover_Clemency : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PLD_Cover_Clemency;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID == Cover)
                {
                    if (Clemency.LevelChecked() && GetResourceCost(Clemency) <= LocalPlayer.CurrentMp && (!HasBattleTarget() || GetCooldownRemainingTime(Cover) >= 10))
                        return Clemency;
                }
                return actionID;
            }
        }
    }
}
