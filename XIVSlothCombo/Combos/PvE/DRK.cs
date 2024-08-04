using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
//using Lumina.Excel.GeneratedSheets;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class DRK
    {
        public const byte JobID = 32;

        public const uint
            LivingDead = 3638, 
            TheBlackestNight = 7393, 
            HardSlash = 3617,
            Unleash = 3621,
            SyphonStrike = 3623,
            Souleater = 3632,
            SaltedEarth = 3639,
            AbyssalDrain = 3641,
            CarveAndSpit = 3643,
            Delirium = 7390,
            Quietus = 7391,
            Bloodspiller = 7392,
            FloodOfDarkness = 16466,
            EdgeOfDarkness = 16467,
            StalwartSoul = 16468,
            FloodOfShadow = 16469,
            EdgeOfShadow = 16470,
            LivingShadow = 16472,
            SaltAndDarkness = 25755,
            Oblation = 25754,
            Shadowbringer = 25757,
            Plunge = 3640,
            BloodWeapon = 3625,
            Unmend = 3624;

        public static class Buffs
        {
            public const ushort
                LivingDead= 810,
                WalkingDead= 811,
                TheBlackestNight = 1178, 
                BloodWeapon = 742,
                Darkside = 751,
                BlackestNight = 1178,
                Delirium = 1972,
                SaltedEarth = 749;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 1;
        }

        public static class Config
        {
            public const string
                DRK_KeepPlungeCharges = "DrkKeepPlungeCharges",
                DRK_VariantCure = "DRKVariantCure";
        }

        internal class DRK_ST_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRK_ST_DPS;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == HardSlash)
                {
                    var gauge = GetJobGauge<DRKGauge>();
                    var plungeChargesRemaining = PluginConfiguration.GetCustomIntValue(Config.DRK_KeepPlungeCharges);
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);
                    var BloodWeaponCD = GetCooldownRemainingTime(BloodWeapon);
                    var LivingShadowCD = GetCooldownRemainingTime(LivingShadow);
                    var DeliriumCD = GetCooldownRemainingTime(Delirium);

                    // oGCDs
                    if (CanWeave(actionID))
                    {
                        Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                        if (IsEnabled(CustomComboPreset.DRK_Variant_SpiritDart) &&
                            IsEnabled(Variant.VariantSpiritDart) &&
                            (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                            return Variant.VariantSpiritDart;

                        if (IsEnabled(CustomComboPreset.DRK_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                            return Variant.VariantUltimatum;

                        if (IsEnabled(CustomComboPreset.DRK_LivingShadow) && ActionReady(LivingShadow) && gauge.Blood >= 50 && CanWeave(actionID, 0.3) && DeliriumCD >= 40)
                            return LivingShadow;
                        
                        if (CanWeave(actionID))
                        {
                            if (ActionReady(BloodWeapon) && !(CombatEngageDuration().TotalSeconds >= 20))
                                return BloodWeapon;
                            if (IsEnabled(CustomComboPreset.DRK_BloodWeapon) && ActionReady(BloodWeapon) &&
                                LocalPlayer.CurrentMp <= 9400 && gauge.Blood <= 90)
                                return BloodWeapon;
                            if (ActionReady(Delirium) && InCombat() && CombatEngageDuration().TotalSeconds >= 4.5 && BloodWeaponCD >= 40)
                                return Delirium;

                            if (LevelChecked(FloodOfDarkness) && (LocalPlayer.CurrentMp >= 3000 || gauge.HasDarkArts) && (gauge.DarksideTimeRemaining <= 2500 || LocalPlayer.CurrentMp >= 9000))
                                return LevelChecked(EdgeOfDarkness) ? OriginalHook(EdgeOfDarkness) : OriginalHook(FloodOfDarkness);
                            if (ActionReady(SaltAndDarkness) && GetCooldownRemainingTime(SaltedEarth) < 78 && GetCooldownRemainingTime(SaltedEarth) > 75)
                                return OriginalHook(SaltedEarth);
                            if (gauge.HasDarkArts && (HasEffect(Buffs.TheBlackestNight) || GetCooldownRemainingTime(TheBlackestNight) >= 14.5))
                                return OriginalHook(EdgeOfDarkness);

                            if (IsEnabled(CustomComboPreset.DRK_ST_SaltedEarth) && ActionReady(SaltedEarth) && GetTargetDistance() <= 5 && DeliriumCD >= 15 &&
                                gauge.DarksideTimeRemaining > 0 && CombatEngageDuration().TotalSeconds >= 6.5)
                                return OriginalHook(SaltedEarth);

                            if ((!LevelChecked(BloodWeapon) || DeliriumCD >= 40) && CombatEngageDuration().TotalSeconds >= 6.5)
                            {
                                if (ActionReady(CarveAndSpit) && LocalPlayer.CurrentMp <= 9400)
                                    return CarveAndSpit;
                                if (ActionReady(Shadowbringer) && gauge.ShadowTimeRemaining > 0)
                                    return Shadowbringer;
                                if (IsEnabled(CustomComboPreset.DRK_ST_EdgeofDarkness) && ActionReady(EdgeOfDarkness) && 
                                    (gauge.HasDarkArts || LocalPlayer.CurrentMp >= 6000 || (LocalPlayer.CurrentMp >= 3000 && GetCooldownRemainingTime(TheBlackestNight) >= 7.5)))
                                    return OriginalHook(EdgeOfDarkness);

                                if (IsEnabled(CustomComboPreset.DRK_ST_Plunge) && ActionReady(Plunge) && GetRemainingCharges(Plunge) > plungeChargesRemaining &&
                                     GetTargetDistance() <= 1 && !IsMoving)
                                    return Plunge;
                            }
                            if (IsEnabled(CustomComboPreset.DRK_ST_EdgeofDarkness) && LevelChecked(FloodOfDarkness) && (LocalPlayer.CurrentMp >= 3000 || gauge.HasDarkArts) && 
                                (AllAttack || !LevelChecked(TheBlackestNight) || LocalPlayer.CurrentMp >= 8800 || (gauge.HasDarkArts && DeliriumCD >= 15) || (gauge.DarksideTimeRemaining <= 10000 && LocalPlayer.CurrentMp >= 3000)))
                                return LevelChecked(EdgeOfDarkness) ? OriginalHook(EdgeOfDarkness) : OriginalHook(FloodOfDarkness);
                        }
                    }

                    //GCD
                    if (IsEnabled(CustomComboPreset.DRK_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.DRK_VariantCure))
                        return Variant.VariantCure;

                    if (LevelChecked(Unmend) && HasBattleTarget() && !InMeleeRange() )
                        return Unmend;

                    if (LevelChecked(Bloodspiller))
                    {
                        if (gauge.Blood >= 90)
                            return Bloodspiller;
                        if (DeliriumCD >= 54.5 || HasEffect(Buffs.Delirium))
                            return Bloodspiller;

                        if (LevelChecked(LivingShadow))
                        {
                            if (LivingShadowCD <= 22.5)
                            {
                                int NumberOfBloodWeaponCanCastBeforeLivingShadow = LivingShadowCD >= BloodWeaponCD ? (int)((LivingShadowCD - BloodWeaponCD) / 2.5) : 
                                    (HasEffect(Buffs.BloodWeapon) ? (int)(LivingShadowCD / 2.5) : 0);
                                if (gauge.Blood >= 100 - NumberOfBloodWeaponCanCastBeforeLivingShadow -
                                    (lastComboMove == SyphonStrike || (lastComboMove == Unleash && IsEnabled(CustomComboPreset.DRK_ST_TwoTarget)) ? 20 : 0))
                                    return Bloodspiller;
                            }
                            if ((LivingShadowCD > 22.5 || DeliriumCD >= 40) && gauge.Blood >= 50)
                                return Bloodspiller;
                        }
                        if (!LevelChecked(LivingShadow))
                        {
                            if (DeliriumCD <= 22.5)
                            {
                                if (gauge.Blood >= 80)
                                    return Bloodspiller;
                                if (gauge.Blood >= 60 && DeliriumCD <= 2.5 && (lastComboMove == SyphonStrike || (lastComboMove == Unleash && IsEnabled(CustomComboPreset.DRK_ST_TwoTarget))))
                                    return Bloodspiller;
                            }
                            if (DeliriumCD >= 22.5 && gauge.Blood >= 50)
                                return Bloodspiller;
                        }
                        /*
                        if (gauge.Blood >= 50)
                        {
                            if (!HasEffect(Buffs.BloodWeapon) && !WasLastAbility(BloodWeapon))
                                return Bloodspiller;
                            if ((HasEffect(Buffs.BloodWeapon) || WasLastAbility(BloodWeapon)) && LivingShadowCD >= 12 || !LevelChecked(LivingShadow)))
                                return Bloodspiller;
                        }
                        11111111111
                        
                        if (AllAttack && (HasEffect(Buffs.Delirium) || gauge.Blood >= 50) && GetCooldownRemainingTime(LivingShadow) >= 7.5)
                            return Bloodspiller;
                        if (DeliriumCD >= 45 && GetBuffRemainingTime(Buffs.Delirium) <= (GetBuffStacks(Buffs.Delirium) - 1) * 2.5 + 0.7)
                            return Bloodspiller;
                        if (gauge.Blood >= 100 && HasEffect(Buffs.BloodWeapon))
                            return Bloodspiller;
                        if (gauge.Blood >= 90 - (HasEffect(Buffs.BloodWeapon) ? 10 : 0) && (lastComboMove == SyphonStrike || (lastComboMove == Unleash && IsEnabled(CustomComboPreset.DRK_ST_TwoTarget))))
                            return Bloodspiller;

                        if (DeliriumCD >= 40 && CombatEngageDuration().TotalSeconds >= 6.5)
                        {
                            if (DeliriumCD >= 54.5 || HasEffect(Buffs.Delirium))
                                return Bloodspiller;
                            if (gauge.Blood >= 50 && (!LevelChecked(LivingShadow) || GetCooldownRemainingTime(LivingShadow) >= 20))
                                return Bloodspiller;
                        }*/
                    }

                    if (comboTime > 0 && lastComboMove == Unleash && LevelChecked(StalwartSoul) && InActionRange(StalwartSoul))
                        return StalwartSoul;
                    if (IsEnabled(CustomComboPreset.DRK_ST_TwoTarget) && lastComboMove != SyphonStrike && LevelChecked(Unleash) && InActionRange(Unleash))
                        return Unleash;

                    // 1-2-3 combo
                    if (comboTime > 0)
                    {
                        if (lastComboMove == HardSlash && LevelChecked(SyphonStrike))
                            return SyphonStrike;
                        if (lastComboMove == SyphonStrike && LevelChecked(Souleater))
                            return Souleater;
                    }
                }
                return actionID;
            }
        }

        internal class DRK_AoE : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRK_AoE;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == Unleash)
                {
                    var gauge = GetJobGauge<DRKGauge>();
                    var BloodWeaponCD = GetCooldownRemainingTime(BloodWeapon);
                    var DeliriumCD = GetCooldownRemainingTime(Delirium);

                    if (ActionReady(AbyssalDrain) && PlayerHealthPercentageHp() <= 25 && !(HasEffect(Buffs.LivingDead) || WasLastAbility(LivingDead) || HasEffect(Buffs.WalkingDead)))
                        return AbyssalDrain;

                    if (IsEnabled(CustomComboPreset.DRK_LivingShadow) && ActionReady(LivingShadow) && gauge.Blood >= 50 && CanWeave(actionID, 0.3) && DeliriumCD >= 40)
                        return LivingShadow;
                    
                    if (CanWeave(actionID))
                    {
                        if (IsEnabled(CustomComboPreset.DRK_BloodWeapon) && ActionReady(BloodWeapon) && LocalPlayer.CurrentMp <= 9400 && gauge.Blood <= 90)
                            return BloodWeapon;
                        if (ActionReady(Delirium) && InCombat() && CombatEngageDuration().TotalSeconds >= 4.5 && BloodWeaponCD >= 40)
                            return Delirium;

                        Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                        if (IsEnabled(CustomComboPreset.DRK_Variant_SpiritDart) &&
                            IsEnabled(Variant.VariantSpiritDart) &&
                            (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                            return Variant.VariantSpiritDart;

                        if (IsEnabled(CustomComboPreset.DRK_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                            return Variant.VariantUltimatum;

                        if (LevelChecked(FloodOfDarkness) && (LocalPlayer.CurrentMp >= 3000 || gauge.HasDarkArts) && (gauge.DarksideTimeRemaining < 1000 || LocalPlayer.CurrentMp >= 9000))
                            return OriginalHook(FloodOfDarkness);
                        if (ActionReady(SaltAndDarkness) && GetCooldownRemainingTime(SaltedEarth) < 78 && GetCooldownRemainingTime(SaltedEarth) > 75)
                            return OriginalHook(SaltedEarth);
                        if (gauge.HasDarkArts && (HasEffect(Buffs.TheBlackestNight) || GetCooldownRemainingTime(TheBlackestNight) >= 14.5))
                            return OriginalHook(FloodOfDarkness);

                        if (ActionReady(SaltedEarth) && gauge.DarksideTimeRemaining > 0 && DeliriumCD >= 40 && GetTargetDistance() <= 5 && !IsMoving)
                            return OriginalHook(SaltedEarth);

                        if (ActionReady(Shadowbringer) && gauge.ShadowTimeRemaining > 0 && DeliriumCD >= 40)
                            return Shadowbringer;
                        if (ActionReady(FloodOfDarkness) && (gauge.HasDarkArts || LocalPlayer.CurrentMp >= 6000 || !LevelChecked(TheBlackestNight)))
                            return OriginalHook(FloodOfDarkness);
                    }

                    //GCD
                    if (IsEnabled(CustomComboPreset.DRK_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.DRK_VariantCure))
                        return Variant.VariantCure;

                    if (LevelChecked(Quietus) && (gauge.Blood >= 50 || HasEffect(Buffs.Delirium) || WasLastAbility(Delirium)))
                        return Quietus;

                    if (comboTime > 0 && lastComboMove == Unleash && LevelChecked(StalwartSoul))
                        return StalwartSoul;
                }
                return actionID;
            }
        }
        internal class DRK_LivingShadow_BloodWeapon_Delirium : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRK_LivingShadow_BloodWeapon_Delirium;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is BloodWeapon)
                {
                    var gauge = GetJobGauge<DRKGauge>();
                    if (ActionReady(LivingShadow) && gauge.Blood >= 50)
                        return LivingShadow;
                    if (ActionReady(Delirium) && !InCombat())
                        return Delirium;
                    if (LevelChecked(FloodOfDarkness) && IsOnCooldown(BloodWeapon) && GetCooldownRemainingTime(Delirium) >= 38 && LocalPlayer.CurrentMp >= 3000)
                        return OriginalHook(FloodOfDarkness);
                }
                return actionID;
            }
        }
    }
}