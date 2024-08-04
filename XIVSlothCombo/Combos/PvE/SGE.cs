using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using System.Collections.Generic;
using System.Linq;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class SGE
    {
        internal const byte JobID = 40;

        // Actions
        internal const uint
            // Heals and Shields
            Diagnosis = 24284,
            Prognosis = 24286,
            Physis = 24288,
            Druochole = 24296,
            Kerachole = 24298,
            Ixochole = 24299,
            Pepsis = 24301,
            Physis2 = 24302,
            Taurochole = 24303,
            Haima = 24305,
            Panhaima = 24311,
            Holos = 24310,
            EukrasianDiagnosis = 24291,
            EukrasianPrognosis = 24292,
            Egeiro = 24287,

            // DPS
            Dosis = 24283,
            Dosis2 = 24306,
            Dosis3 = 24312,
            EukrasianDosis = 24293,
            EukrasianDosis2 = 24308,
            EukrasianDosis3 = 24314,
            Phlegma = 24289,
            Phlegma2 = 24307,
            //Phlegma3 = 24313,
            Dyskrasia = 24297,
            Dyskrasia2 = 24315,
            Toxikon = 24304,
            Pneuma = 24318,

            // Buffs
            Soteria = 24294,
            Zoe = 24300,
            Krasis = 24317,

            // Other
            Kardia = 24285,
            Eukrasia = 24290,
            Rhizomata = 24309;

        // Action Groups
        internal static readonly List<uint>
            AddersgallList = [Taurochole, Druochole, Ixochole, Kerachole],
            PhlegmaList = [Phlegma, Phlegma2]; //, Phlegma3];

        // Action Buffs
        internal static class Buffs
        {
            internal const ushort
                Kardia = 2604,
                Kardion = 2605,
                Eukrasia = 2606,
                EukrasianDiagnosis = 2607,
                EukrasianPrognosis = 2609;
        }

        internal static class Debuffs
        {
            internal const ushort
                EukrasianDosis = 2614,
                EukrasianDosis2 = 2615,
                EukrasianDosis3 = 2616;
        }

        // Debuff Pairs of Actions and Debuff
        internal static readonly Dictionary<uint, ushort>
            DosisList = new()
            {
                { Dosis,  Debuffs.EukrasianDosis  },
                { Dosis2, Debuffs.EukrasianDosis2 },
                { Dosis3, Debuffs.EukrasianDosis3 }
            };

        // Sage Gauge & Extensions
        public static SGEGauge Gauge => CustomComboFunctions.GetJobGauge<SGEGauge>();
        public static bool HasAddersgall(this SGEGauge gauge) => gauge.Addersgall > 0;
        public static bool HasAddersting(this SGEGauge gauge) => gauge.Addersting > 0;

        public static class Config
        {
            #region DPS
            public static UserBool
                SGE_ST_DPS_EDosis_Adv = new("SGE_ST_Dosis_EDosis_Adv");
            public static UserInt
                SGE_ST_DPS_EDosisHPPer = new("SGE_ST_Dosis_EDosisHPPer"),
                SGE_ST_DPS_Lucid = new("SGE_ST_DPS_Lucid"),
                SGE_ST_DPS_Rhizo = new("SGE_ST_DPS_Rhizo");
            public static UserFloat
                SGE_ST_DPS_EDosisThreshold = new("SGE_ST_Dosis_EDosisThreshold");
            #endregion

            #region Healing
            public static UserBool
                SGE_ST_Heal_Adv = new("SGE_ST_Heal_Adv"),
                SGE_ST_Heal_UIMouseOver = new("SGE_ST_Heal_UIMouseOver");
            public static UserInt
                SGE_ST_Heal_Esuna = new("SGE_ST_Heal_Esuna");
            #endregion
        }

        internal static class Traits
        {
            internal const ushort
                EnhancedKerachole = 375;
        }


        /*
         * SGE_Kardia
         * Soteria becomes Kardia when Kardia's Buff is not active or Soteria is on cooldown.
         */
        internal class SGE_Kardia : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_Kardia;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
                => actionID is Soteria && (!HasEffect(Buffs.Kardia) || IsOnCooldown(Soteria)) ? Kardia : actionID;
        }

        internal class SGE_AoE_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_AoE_DPS;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Dyskrasia or Dyskrasia2)
                {
                    //oGCD
                    if (CanSpellWeave(actionID))
                    {
                        if (IsEnabled(CustomComboPreset.SGE_DPS_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart))
                            return Variant.VariantRampart;

                        Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                        if (IsEnabled(CustomComboPreset.SGE_DPS_Variant_SpiritDart) &&
                            IsEnabled(Variant.VariantSpiritDart) &&
                            (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                            return Variant.VariantSpiritDart;

                        // Rhizomata
                        if (ActionReady(Rhizomata) && Gauge.Addersgall <= 1 && InCombat())
                            return Rhizomata;
                    }

                    //GCD
                    if (InCombat())
                    {
                        if (ActionReady(OriginalHook(Phlegma)) && InActionRange(OriginalHook(Phlegma)))
                            return OriginalHook(Phlegma);
                        if (LevelChecked(Toxikon) && Gauge.HasAddersting())
                            return OriginalHook(Toxikon);
                    }
                }
                return actionID;
            }
        }

        internal class SGE_ST_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_ST_DPS;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Dosis or Dosis2 or Dosis3)
                {
                    uint dot = OriginalHook(Dosis);   
                    Status? dotDebuff = FindTargetEffect(DosisList[dot]);
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);
                    uint phlegma = OriginalHook(Phlegma);

                    // Kardia Reminder
                    if (LevelChecked(Kardia) && FindEffect(Buffs.Kardia) is null)
                        return Kardia;
                    if (IsEnabled(CustomComboPreset.SGE_ST_DPS_Lucid) && ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 3000)
                        return All.LucidDreaming;

                    //oGCD
                    if (CanSpellWeave(actionID))
                    {
                        if (IsEnabled(CustomComboPreset.SGE_DPS_Variant_Rampart) &&
                            IsEnabled(Variant.VariantRampart) &&
                            IsOffCooldown(Variant.VariantRampart))
                            return Variant.VariantRampart;

                        // Rhizomata
                        if (IsEnabled(CustomComboPreset.SGE_ST_DPS_Rhizo) && ActionReady(Rhizomata) && Gauge.Addersgall <= Config.SGE_ST_DPS_Rhizo)
                            return Rhizomata;
                        // Lucid Dreaming
                        if (IsEnabled(CustomComboPreset.SGE_ST_DPS_Lucid) && ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= Config.SGE_ST_DPS_Lucid)
                            return All.LucidDreaming;
                    }

                    //GCD
                    if (InCombat() && HasBattleTarget() && (!HasEffect(Buffs.Eukrasia)))
                    // Buff check Above. Without it, Toxikon and any future option will interfere in the Eukrasia->Eukrasia Dosis combo
                    {
                        if (IsEnabled(CustomComboPreset.SGE_DPS_Variant_SpiritDart) && IsEnabled(Variant.VariantSpiritDart))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if ((sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) && CanSpellWeave(actionID))
                                return Variant.VariantSpiritDart;
                        }

                        if (IsEnabled(CustomComboPreset.SGE_ST_DPS_EDosis) && LevelChecked(Eukrasia) && !WasLastSpell(OriginalHook(EukrasianDosis3)))
                        {
                            float refreshtimer = Config.SGE_ST_DPS_EDosis_Adv ? Config.SGE_ST_DPS_EDosisThreshold : 3;     //Ò»°ãÊÇ1.7
                            if (!AllAttack && (dotDebuff is null || dotDebuff?.RemainingTime <= refreshtimer) &&
                                GetTargetHPPercent() > Config.SGE_ST_DPS_EDosisHPPer && (level < 90 || EnemyHealthCurrentHp() >= 45000))
                                return Eukrasia;
                        }
                        if (ActionReady(phlegma) && InActionRange(phlegma))
                        {
                            if (AllAttack)
                                return phlegma;
                            if (GetRemainingCharges(phlegma) >= 1 && GetCooldownChargeRemainingTime(phlegma) <= 5)
                                return phlegma;
                            if (IsMoving && IsOffCooldown(Dosis))
                                return phlegma;
                        }
                        if (IsMoving && IsOffCooldown(Dosis))
                        {
                            if (LevelChecked(Toxikon) && Gauge.HasAddersting())
                                return OriginalHook(Toxikon);
                            if (IsEnabled(CustomComboPreset.SGE_ST_DPS_EDosis) && LevelChecked(Eukrasia) &&
                                !AllAttack && dotDebuff?.RemainingTime <= 8 &&
                                GetTargetHPPercent() > Config.SGE_ST_DPS_EDosisHPPer && (level < 90 || EnemyHealthCurrentHp() >= 45000))
                                return Eukrasia;
                        }
                    }
                }
                return actionID;
            }
        }

        internal class SGE_ST_Heal : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_ST_Heal;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Diagnosis)
                {
                    if (HasEffect(Buffs.Eukrasia))
                        return EukrasianDiagnosis;
                    if (!InCombat())
                        return Eukrasia;

                    if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 2800)
                        return All.LucidDreaming;
                    if (ActionReady(Rhizomata) && !Gauge.HasAddersgall() && InCombat())
                        return Rhizomata;
                    
                    GameObject? healTarget = GetHealTarget(Config.SGE_ST_Heal_Adv && Config.SGE_ST_Heal_UIMouseOver);
                    if (IsEnabled(CustomComboPreset.SGE_ST_Heal_Esuna) && ActionReady(All.Esuna) &&
                        GetTargetHPPercent(healTarget) >= Config.SGE_ST_Heal_Esuna &&
                        HasCleansableDebuff(healTarget))
                        return All.Esuna;

                    if (Gauge.HasAddersgall() && LevelChecked(Druochole) && HasBattleTarget())
                        return Druochole;
                    if (IsMoving)
                        return Eukrasia;
                }
                return actionID;
            }
        }

        internal class SGE_AoE_Heal : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_AoE_Heal;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Prognosis)
                {
                    if (HasEffect(Buffs.Eukrasia))
                        return EukrasianPrognosis;
                    if (!InCombat())
                        return Eukrasia;

                    if (IsMoving)
                        return Eukrasia;
                    if (HasBattleTarget())
                        return Eukrasia;
                }
                return actionID;
            }
        }

        internal class SGE_Physis_Ixochole : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_Physis_Ixochole;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Ixochole)
                {
                    if (GetCooldownRemainingTime(OriginalHook(Physis)) >= 59.2)
                        return OriginalHook(Physis);
                    if (ActionReady(Rhizomata) && !Gauge.HasAddersgall() && InCombat())
                        return Rhizomata;

                    if (ActionReady(OriginalHook(Physis)))
                        return OriginalHook(Physis);
                }
                return actionID;
            }
        }

        internal class SGE_Krasis_Haima : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_Krasis_Haima;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Haima)
                {
                    if (GetCooldownRemainingTime(Krasis) >= 59.2)
                        return Krasis;
                    if (ActionReady(Krasis))
                        return Krasis;
                }
                return actionID;
            }
        }

        internal class SGE_Zoe_Pneuma : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_Zoe_Pneuma;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Pneuma)
                {
                    if (ActionReady(Zoe))
                        return Zoe;
                }
                return actionID;
            }
        }

        internal class SGE_Taurochole_Haima : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SGE_Taurochole_Haima;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Taurochole)
                {
                    if (GetCooldownRemainingTime(Taurochole) >= 44.2)
                        return Taurochole;
                    if (GetCooldownRemainingTime(Taurochole) <= 5 && IsOnCooldown(Taurochole))
                        return Taurochole;
                    if (ActionReady(Taurochole) && Gauge.HasAddersgall())
                        return Taurochole;
                    if (ActionReady(Haima))
                        return Haima;
                }
                return actionID;
            }
        }
    }
}