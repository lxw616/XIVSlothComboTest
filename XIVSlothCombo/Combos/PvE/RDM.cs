using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;

namespace XIVSlothCombo.Combos.PvE
{
    internal class RDM
    {
        public const byte JobID = 35;

        public const uint
            Verthunder = 7505,
            Veraero = 7507,
            Veraero2 = 16525,
            Veraero3 = 25856,
            Verthunder2 = 16524,
            Verthunder3 = 25855,
            Impact = 16526,
            Redoublement = 7516,
            EnchantedRedoublement = 7529,
            Zwerchhau = 7512,
            EnchantedZwerchhau = 7528,
            Riposte = 7504,
            EnchantedRiposte = 7527,
            Scatter = 7509,
            Verstone = 7511,
            Verfire = 7510,
            Vercure = 7514,
            Jolt = 7503,
            Jolt2 = 7524,
            Verholy = 7526,
            Verflare = 7525,
            Fleche = 7517,
            ContreSixte = 7519,
            Engagement = 16527,
            Verraise = 7523,
            Scorch = 16530,
            Resolution = 25858,
            Moulinet = 7513,
            EnchantedMoulinet = 7530,
            Corpsacorps = 7506,
            Displacement = 7515,
            Reprise = 16529,
            MagickBarrier = 25857,

            //Buffs
            Acceleration = 7518,
            Manafication = 7521,
            Embolden = 7520;

        public static class Buffs
        {
            public const ushort
                VerfireReady = 1234,
                VerstoneReady = 1235,
                Dualcast = 1249,
                Chainspell = 2560,
                Acceleration = 1238,
                Embolden = 1239;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        protected static RDMGauge Gauge => CustomComboFunctions.GetJobGauge<RDMGauge>();

        public static class Config
        {

        }

        internal class RDM_ST_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RDM_ST_DPS;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Verthunder or Verthunder3)
                {
                    int blackMana = Gauge.BlackMana;
                    int whiteMana = Gauge.WhiteMana;
                    int maxMana = Math.Max(blackMana, whiteMana);
                    int minMana = Math.Min(blackMana, whiteMana);
                    int differenceMana = Math.Abs(blackMana - whiteMana);
                    float emboldenCD = GetCooldownRemainingTime(Embolden);
                    float targetDistance = GetTargetDistance();
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                    if (InCombat())
                    {
                        if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 2500)
                            return All.LucidDreaming;
                        //oGCD
                        if (ActionReady(Embolden) && IsEnabled(CustomComboPreset.RDM_Embolden) && (CombatEngageDuration().TotalSeconds >= 20 || GetCooldownRemainingTime(Manafication) >= 5))
                            return Embolden;
                        if (ActionReady(Fleche) && (CanSpellWeave(actionID, 0.2) || IsMoving))
                            return Fleche;
                        if (ActionReady(ContreSixte) && IsOffCooldown(Jolt) && IsMoving)
                            return ContreSixte;

                        if (CanSpellWeave(actionID))
                        {
                            if (ActionReady(Manafication) && ActionReady(Embolden) && CanDelayedWeave(actionID, 1.6, 0.6) && 
                                !HasEffect(Buffs.Dualcast) && !HasEffect(Buffs.Acceleration) && !HasEffect(All.Buffs.Swiftcast))
                            {
                                if (ActionReady(All.Swiftcast))
                                    return All.Swiftcast;
                                if (ActionReady(Acceleration))
                                    return Acceleration;
                            }
                            if (ActionReady(ContreSixte))
                                return ContreSixte;
                            if (ActionReady(Manafication) && IsEnabled(CustomComboPreset.RDM_ST_DPS_Manafication)
                                && !(lastComboMove is Riposte or EnchantedRiposte or Zwerchhau or EnchantedZwerchhau) && maxMana <= 50 &&
                                (ActionReady(Embolden) || Gauge.ManaStacks == 3 || !(HasEffect(Buffs.VerfireReady) && HasEffect(Buffs.VerstoneReady) && maxMana <= 99 && !IsMoving)))
                                return Manafication;

                            if (ActionReady(Engagement) && IsEnabled(CustomComboPreset.RDM_ST_DPS_Engagement) && targetDistance <= 3 &&
                                GetRemainingCharges(Engagement) >= 1 && GetCooldownChargeRemainingTime(Engagement) <= 2.5 && (emboldenCD > 0.7 || !LevelChecked(Embolden)))
                                return Engagement;
                            if (ActionReady(Corpsacorps) && IsEnabled(CustomComboPreset.RDM_ST_DPS_Corpsacorps) && !IsMoving && //targetDistance <= 1 && 
                                GetRemainingCharges(Corpsacorps) >= 1 && GetCooldownChargeRemainingTime(Corpsacorps) <= 2.5)
                                return Corpsacorps;
                            if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 8500)
                                return All.LucidDreaming;
                        }
                    }

                    //GCD
                    if (LevelChecked(Resolution) && lastComboMove is Scorch)
                        return Resolution;
                    if (LevelChecked(Scorch) && lastComboMove is Verholy or Verflare)
                        return Scorch;
                    if (Gauge.ManaStacks == 3)
                    {
                        if (differenceMana <= 30 - 11)
                        {
                            if (LevelChecked(Verholy) && HasEffect(Buffs.VerfireReady))
                                return Verholy;
                            if (LevelChecked(Verflare) && HasEffect(Buffs.VerstoneReady))
                                return Verflare;
                        }
                        if (LevelChecked(Verholy) && blackMana >= whiteMana)
                            return Verholy;
                        if (LevelChecked(Verflare) && blackMana < whiteMana)
                            return Verflare;
                    }
                    if (LevelChecked(Redoublement) && lastComboMove is Zwerchhau or EnchantedZwerchhau)
                        return OriginalHook(Redoublement);
                    if (LevelChecked(Zwerchhau) && lastComboMove is Riposte or EnchantedRiposte)
                        return OriginalHook(Zwerchhau);

                    if (IsEnabled(CustomComboPreset.RDM_Vercure) && LevelChecked(Vercure) && PlayerHealthPercentageHp() <= 40)
                        return Vercure;

                    if (IsEnabled(CustomComboPreset.RDM_ST_DPS_TwoTarget) && (HasEffect(Buffs.Dualcast) || HasEffect(Buffs.Acceleration) || HasEffect(All.Buffs.Swiftcast)))
                        return OriginalHook(Scatter);
                    
                    if (HasEffect(Buffs.Acceleration) && differenceMana <= 30 - 6)
                    {
                        if (LevelChecked(Verthunder) && HasEffect(Buffs.VerstoneReady))
                            return OriginalHook(Verthunder);
                        if (LevelChecked(Veraero) && HasEffect(Buffs.VerfireReady))
                            return OriginalHook(Veraero);
                    }
                    if (HasEffect(Buffs.Dualcast) || HasEffect(Buffs.Acceleration) || HasEffect(All.Buffs.Swiftcast))
                    {
                        if (blackMana > whiteMana && LevelChecked(Veraero))
                            return OriginalHook(Veraero);
                        if (blackMana <= whiteMana && LevelChecked(Verthunder))
                            return OriginalHook(Verthunder);
                    }

                    //赤飞石和赤火炎的buff同时存在并且还能打魔6连的话，先泄掉一个，即使是在爆发期内
                    if (HasEffect(Buffs.VerfireReady) && HasEffect(Buffs.VerstoneReady) && maxMana <= 99 && !IsMoving)
                    {
                        if (blackMana >= whiteMana)
                            return Verstone;
                        if (blackMana < whiteMana)
                            return Verfire;
                    }

                    //魔回刺，最麻烦
                    if (IsEnabled(CustomComboPreset.RDM_ST_DPS_Riposte) && minMana >= 20 + (LevelChecked(Zwerchhau) ? 1 : 0) * 15 + (LevelChecked(Redoublement) ? 1 : 0) * 15 && targetDistance <= 4)
                    {
                        //防溢出系列
                        if (AllAttack || minMana >= 92 || maxMana >= 98)
                            return OriginalHook(Riposte);
                        //AOE切单体
                        if (Gauge.ManaStacks > 0)
                            return OriginalHook(Riposte);
                        if (GetCooldownRemainingTime(Manafication) >= 95)
                            return OriginalHook(Riposte);

                        if (LevelChecked(Embolden))
                        {
                            if (emboldenCD >= 100)
                                return OriginalHook(Riposte);
                            if (minMana >= 50) //83)
                            {
                                if (level == 90 && IsOnCooldown(Embolden) && emboldenCD <= 6.2 && (GetCooldownRemainingTime(Manafication) <= 12 || (minMana >= 81 && maxMana >= 92)))
                                    return OriginalHook(Riposte);
                                if (level < 90 && level >= 80 && IsOnCooldown(Embolden) && emboldenCD <= 3.7 && (GetCooldownRemainingTime(Manafication) <= 9.5 || (minMana >= 85 && maxMana >= 96)))
                                    return OriginalHook(Riposte);
                                if (level < 80 && IsOnCooldown(Embolden) && emboldenCD <= 1.2 && (GetCooldownRemainingTime(Manafication) <= 9.5 || (minMana >= 85 && maxMana >= 96)))
                                    return OriginalHook(Riposte);
                            }
                            if (emboldenCD >= 27.5)//if (emboldenCD >= 42.5)
                                return OriginalHook(Riposte);
                        }
                        if (InCombat() && HasBattleTarget() && IsOffCooldown(Jolt) && IsMoving)
                            return OriginalHook(Riposte);
                        if (!LevelChecked(Embolden))
                            return OriginalHook(Riposte);
                    }

                    if (InCombat() && HasBattleTarget() && IsMoving && IsOffCooldown(Jolt) && !HasEffect(Buffs.Dualcast))
                    {
                        if (ActionReady(All.Swiftcast) && !HasEffect(Buffs.Acceleration))
                            return All.Swiftcast;
                        if (ActionReady(Acceleration) && !HasEffect(All.Buffs.Swiftcast))
                            return Acceleration;
                    }

                    /*魔续斩，但技能太烂了
                    if (InCombat() && HasBattleTarget() && IsMoving && IsOffCooldown(Jolt) && IsOnCooldown(All.Swiftcast) && GetRemainingCharges(Acceleration) == 0)
                    {
                        if (ActionReady(Reprise) && minMana >= 5)
                            return OriginalHook(Reprise);
                    }
                    */

                    if (HasEffect(Buffs.VerfireReady))
                        return Verfire;
                    if (HasEffect(Buffs.VerstoneReady))
                        return Verstone;
                    if (!LevelChecked(Verthunder))
                    {
                        if (LevelChecked(Jolt))
                            return OriginalHook(Jolt);
                        if (LevelChecked(Riposte))
                            return Riposte;
                    }

                    if (!IsEnabled(CustomComboPreset.RDM_ST_DPS_JoltFirst) && !InCombat())
                        return OriginalHook(Verthunder);
                    return OriginalHook(Jolt);
                }
                return actionID;
            }
        }

        internal class RDM_AoE : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RDM_AOE;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                int black = Gauge.BlackMana;
                int white = Gauge.WhiteMana;
                float targetDistance = GetTargetDistance();
                int minMana = Math.Min(black, white);
                float emboldenCD = GetCooldownRemainingTime(Embolden);

                if (actionID is Scatter or Impact)
                {
                    if (InCombat())
                    {
                        if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 2500)
                            return All.LucidDreaming;
                        //oGCD
                        if (CanSpellWeave(actionID, 0.4))
                        {
                            //跟单体目标的鼓励共用吧
                            if (ActionReady(Embolden) && IsEnabled(CustomComboPreset.RDM_Embolden))
                                return Embolden;
                            if (ActionReady(Manafication) && !(lastComboMove is Riposte or EnchantedRiposte or Zwerchhau or EnchantedZwerchhau) && Math.Max(Gauge.BlackMana, Gauge.WhiteMana) <= 50)
                                return Manafication;
                            if (ActionReady(Fleche))
                                return Fleche;
                            if (ActionReady(ContreSixte))
                                return ContreSixte;
                        }
                        if (CanSpellWeave(actionID))
                        {
                            if (ActionReady(Acceleration) && !HasEffect(Buffs.Acceleration))
                                return Acceleration;
                            if (ActionReady(Engagement) && IsEnabled(CustomComboPreset.RDM_ST_DPS_Engagement) && targetDistance <= 3 &&
                                GetRemainingCharges(Engagement) >= 1 && GetCooldownChargeRemainingTime(Engagement) <= 2.5)
                                return Engagement;
                            if (ActionReady(Corpsacorps) && IsEnabled(CustomComboPreset.RDM_ST_DPS_Corpsacorps) && targetDistance <= 1 && !IsMoving &&
                                GetRemainingCharges(Acceleration) >= 1 && GetCooldownChargeRemainingTime(Acceleration) <= 2.5)
                                return Corpsacorps;

                            if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 5000)
                                return All.LucidDreaming;
                        }
                    }

                    //GCD
                    if (LevelChecked(Resolution) && lastComboMove is Scorch)
                        return Resolution;
                    if (LevelChecked(Scorch) && lastComboMove is Verholy or Verflare)
                        return Scorch;
                    if (Gauge.ManaStacks == 3)
                    {
                        if (LevelChecked(Verholy) && black >= white)
                            return Verholy;
                        if (LevelChecked(Verflare) && (black < white || !LevelChecked(Verholy)))
                            return Verflare;
                    }
                    if (Gauge.ManaStacks > 0 && minMana >= 20)
                        return OriginalHook(EnchantedMoulinet);

                    if (IsEnabled(CustomComboPreset.RDM_ULK))
                    {
                        if (GetBuffRemainingTime(1687) <= 3 && PlayerHealthPercentageHp() <= 85)
                            return 12993;//文理再生
                    }
                    if (IsEnabled(CustomComboPreset.RDM_Vercure) && PlayerHealthPercentageHp() <= 40)
                        return Vercure;

                    if (HasEffect(Buffs.Dualcast) || (HasEffect(All.Buffs.Swiftcast) && LevelChecked(Scorch)))
                        return OriginalHook(Scatter);

                    if (LevelChecked(Moulinet) && minMana >= 20 && (minMana + Gauge.ManaStacks * 20 >= 60 || !LevelChecked(Verflare)) && targetDistance <= 5)
                        return OriginalHook(EnchantedMoulinet);

                    if (InCombat() && HasBattleTarget() && IsMoving && IsOffCooldown(Jolt) && !HasEffect(Buffs.Dualcast))
                    {
                        if (ActionReady(All.Swiftcast) && !HasEffect(Buffs.Acceleration))
                            return All.Swiftcast;
                        if (ActionReady(Acceleration) && !HasEffect(All.Buffs.Swiftcast))
                            return Acceleration;
                    }

                    if (HasEffect(Buffs.Acceleration) || HasEffect(All.Buffs.Swiftcast))
                        return OriginalHook(Scatter);

                    if (LevelChecked(Verthunder2) && black <= white)
                        return OriginalHook(Verthunder2);
                    if (LevelChecked(Veraero2) && black > white)
                        return OriginalHook(Veraero2);

                    if (!LevelChecked(Verthunder2) && LevelChecked(Scatter))
                        return Jolt;
                }
                return actionID;
            }
        }
        internal class RDM_Acceleration_SwiftCast : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RDM_Acceleration_SwiftCast;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Acceleration)
                {
                    if (GetCooldownRemainingTime(All.Swiftcast) >= 59.2)
                        return All.Swiftcast;
                    if (ActionReady(All.Swiftcast) && GetRemainingCharges(Acceleration) != 2)
                        return All.Swiftcast;
                }
                return actionID;
            }
        }
    }
}