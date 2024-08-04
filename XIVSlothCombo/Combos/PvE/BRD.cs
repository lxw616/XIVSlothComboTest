using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using System;
using System.Linq;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.Data;
using static FFXIVClientStructs.FFXIV.Client.UI.AddonJobHudBRD0;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class BRD
    {
        public const byte ClassID = 5;
        public const byte JobID = 23;

        public const uint
            HeavyShot = 97,
            StraightShot = 98,
            VenomousBite = 100,
            RagingStrikes = 101,
            QuickNock = 106,
            Barrage = 107,
            Bloodletter = 110,
            Windbite = 113,
            MagesBallad = 114,
            ArmysPaeon = 116,
            RainOfDeath = 117,
            BattleVoice = 118,
            EmpyrealArrow = 3558,
            WanderersMinuet = 3559,
            IronJaws = 3560,
            Sidewinder = 3562,
            PitchPerfect = 7404,
            Troubadour = 7405,
            CausticBite = 7406,
            Stormbite = 7407,
            RefulgentArrow = 7409,
            BurstShot = 16495,
            ApexArrow = 16496,
            Shadowbite = 16494,
            Ladonsbite = 25783,
            BlastArrow = 25784,
            RadiantFinale = 25785;

        public static class Buffs
        {
            public const ushort
                StraightShotReady = 122,
                RagingStrikes = 125,
                Barrage = 128,
                MagesBallad = 135,
                ArmysPaeon = 137,
                BattleVoice = 141,
                WanderersMinuet = 865,
                Troubadour = 1934,
                BlastArrowReady = 2692,
                RadiantFinale = 2722,
                ShadowbiteReady = 3002;
        }

        public static class Debuffs
        {
            public const ushort
                VenomousBite = 124,
                Windbite = 129,
                CausticBite = 1200,
                Stormbite = 1201;
        }

        public static class Config
        {
            public const string
                BRD_whenApexShot = "BRD_whenApexShot",
                BRD_ST_MagesBallad_ArmysPaeon = "BRD_ST_MagesBallad_ArmysPaeon",
                BRD_AoESecondWindThreshold = "BRD_AoESecondWindThreshold",
                BRD_VariantCure = "BRD_VariantCure";
        }

        #region Song status
        internal static bool SongIsNotNone(Song value) => value != Song.NONE;
        internal static bool SongIsNone(Song value) => value == Song.NONE;
        internal static bool SongIsWandererMinuet(Song value) => value == Song.WANDERER;
        #endregion

        internal class BRD_ST_SimpleMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BRD_ST_SimpleMode;

            internal delegate bool DotRecast(int value);

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is HeavyShot or BurstShot)
                {
                    BRDGauge? gauge = GetJobGauge<BRDGauge>();
                    bool canWeave = CanWeave(actionID);
                    bool canWeaveBuffs = CanWeave(actionID, 0.6);
                    bool songNone = gauge.Song == Song.NONE;
                    bool songWanderer = gauge.Song == Song.WANDERER;
                    bool songMage = gauge.Song == Song.MAGE;
                    bool songArmy = gauge.Song == Song.ARMY;
                    var RagingStrikesCD = GetCooldownRemainingTime(RagingStrikes);
                    var BattleVoiceCD = GetCooldownRemainingTime(BattleVoice);
                    var RadiantFinaleCD = GetCooldownRemainingTime(RadiantFinale);

                    var EmpyrealArrowCD = GetCooldownRemainingTime(EmpyrealArrow);

                    var WanderersMinuetCD = GetCooldownRemainingTime(WanderersMinuet);
                    var MagesBalladCD = GetCooldownRemainingTime(MagesBallad);
                    var ArmysPaeonCD = GetCooldownRemainingTime(ArmysPaeon);

                    //90级14秒4人团伤（例如3s的大小鸟）大于23333，低于90级设置为9500 * 14，随便吧！有需要以后再改
                    bool isEnemyHealthHigh = GetTargetHPPercent() >= 2;
                    /*
                    if (isEnemyHealthHigh)
                    {
                        float EnemyCurrentHp = EnemyHealthCurrentHp();

                        if (level >= 70)
                            isEnemyHealthHigh = EnemyCurrentHp > 23333 * 14 * 1.15 * 1.15;
                        if (level >= 90)
                            isEnemyHealthHigh = EnemyCurrentHp > 23333 * 14 * 1.15 * 1.15;
                        
                    }
                    */

                    if (IsEnabled(CustomComboPreset.ALL_Ranged_Interrupt) && CanInterruptEnemy() && ActionReady(All.HeadGraze))
                        return All.HeadGraze;

                    if (songWanderer && WanderersMinuetCD <= 78 && gauge.Repertoire > 0 && canWeave)
                        return OriginalHook(WanderersMinuet);

                    if (canWeaveBuffs && IsEnabled(CustomComboPreset.BRD_Songs))
                    {
                        if (LevelChecked(WanderersMinuet))
                        {
                            if (IsOffCooldown(WanderersMinuet))
                            {
                                if (songNone)
                                    return WanderersMinuet;
                                if (IsEnabled(CustomComboPreset.BRD_ST_WanderersMinuet) && (RagingStrikesCD <= 43 - 20 || RagingStrikesCD >= 100) &&
                                    ArmysPaeonCD <= 117 && !(RagingStrikesCD == 0 && CombatEngageDuration().TotalSeconds >= 20) )
                                    return WanderersMinuet;
                            }
                            if (ActionReady(MagesBallad) && (songNone || WanderersMinuetCD < 77.5) && (WanderersMinuetCD >= 57 || (WanderersMinuetCD > 21 && ArmysPaeonCD > WanderersMinuetCD - 21)))   
                                return MagesBallad;
                            if (ActionReady(ArmysPaeon) && songNone && WanderersMinuetCD <= 43)
                                return ArmysPaeon;
                            if (ActionReady(ArmysPaeon) && songMage && (IsEnabled(CustomComboPreset.BRD_ST_ArmysPaeon) ?
                                MagesBalladCD <= 75 + PluginConfiguration.GetCustomIntValue(Config.BRD_ST_MagesBallad_ArmysPaeon) - 0.5 : MagesBalladCD <= 77.5))
                                return ArmysPaeon;
                        }
                        else if (gauge.SongTimer / 1000 < 3)
                        {
                            if (ActionReady(MagesBallad))
                                return MagesBallad;
                            if (ActionReady(ArmysPaeon))
                                return ArmysPaeon;
                        }
                    }

                    if (!songNone || !LevelChecked(MagesBallad))
                    {

                        if (ActionReady(RagingStrikes) && IsEnabled(CustomComboPreset.BRD_RagingStrikes))
                        {
                            if (!LevelChecked(IronJaws) && isEnemyHealthHigh && !InCombat())
                                return RagingStrikes;
                            if (canWeaveBuffs)
                            {
                                if (level >= 90 || !LevelChecked(IronJaws))
                                    return RagingStrikes;
                                if (WasLastWeaponskill(OriginalHook(VenomousBite)) || CombatEngageDuration().TotalSeconds > 20)
                                    return RagingStrikes;
                            }
                        }

                        if (LevelChecked(EmpyrealArrow) && EmpyrealArrowCD <= 0.7 && !songNone && RagingStrikesCD >= 2.5&&
                            (canWeave || (RagingStrikesCD < 100 && CanWeave(actionID, 0.4))))
                            return EmpyrealArrow;
                        if (LevelChecked(Bloodletter) && RagingStrikesCD >= 2.5 && canWeave && GetRemainingCharges(Bloodletter) >= GetMaxCharges(Bloodletter) - 1 && 
                            GetCooldownChargeRemainingTime(Bloodletter) - (songMage && EmpyrealArrowCD <= 1.3 ? 7.5 : 0) <= 0.7)
                            return Bloodletter;

                        if (canWeaveBuffs)
                        {
                            if (ActionReady(BattleVoice) && RagingStrikesCD >= 100)
                                return BattleVoice;
                            if (ActionReady(RadiantFinale) && RagingStrikesCD >= 100 && (BattleVoiceCD <= 1.3 || BattleVoiceCD >= 100))
                                return RadiantFinale;
                            if (ActionReady(Barrage) && RagingStrikesCD >= 100 && BattleVoiceCD >= 100 && RadiantFinaleCD >= 90 && !HasEffect(Buffs.StraightShotReady))
                                return Barrage;
                        }

                        if (canWeave)
                        {
                            if (LevelChecked(PitchPerfect) && songWanderer)
                            {
                                if (gauge.Repertoire == 3)
                                    return OriginalHook(WanderersMinuet);
                                if (gauge.Repertoire == 2 && EmpyrealArrowCD <= 1.5)
                                    return OriginalHook(WanderersMinuet);
                                if (gauge.Repertoire >= 1 && RagingStrikesCD <= 101.5 && RagingStrikesCD >= 99.5)
                                    return OriginalHook(WanderersMinuet);
                            }

                            if (ActionReady(Sidewinder) && RagingStrikesCD >= 9)
                                return Sidewinder;
                            if (ActionReady(Bloodletter) && IsEnabled(CustomComboPreset.BRD_ST_Bloodletter) && RagingStrikesCD >= (15 * (songArmy ? 0.84 : 1) * GetMaxCharges(Bloodletter) - 5))
                            {
                                if (IsNotEnabled(CustomComboPreset.BRD_ST_TwoTarget))
                                    return Bloodletter;
                                else
                                    return RainOfDeath;
                            }
                        }
                    }

                    //GCD
                    if (IsEnabled(CustomComboPreset.BRD_ST_DoT) && isEnemyHealthHigh)
                    {
                        var venomous = TargetHasEffect(Debuffs.VenomousBite);
                        var windbite = TargetHasEffect(Debuffs.Windbite);
                        var caustic = TargetHasEffect(Debuffs.CausticBite);
                        var stormbite = TargetHasEffect(Debuffs.Stormbite);

                        var venomousDuration = FindTargetEffect(Debuffs.VenomousBite);
                        var windbiteDuration = FindTargetEffect(Debuffs.Windbite);
                        var causticDuration = FindTargetEffect(Debuffs.CausticBite);
                        var stormbiteDuration = FindTargetEffect(Debuffs.Stormbite);

                        //var ragingStrikesDuration = 20 - 120 + RagingStrikesCD;
                        //var ragingJawsRenewTime = 4;

                        DotRecast poisonRecast = delegate (int duration)
                        {
                            return (venomous && venomousDuration.RemainingTime < duration) || (caustic && causticDuration.RemainingTime < duration);
                        };
                        DotRecast windRecast = delegate (int duration)
                        {
                            return (windbite && windbiteDuration.RemainingTime < duration) || (stormbite && stormbiteDuration.RemainingTime < duration);
                        };

                        var useIronJaws = LevelChecked(IronJaws) &&
                            (poisonRecast(3) || windRecast(3) || 
                            (RagingStrikesCD >= 100 && BattleVoiceCD >= 100 && poisonRecast(31) && windRecast(31) && (RagingStrikesCD <= 102.5 || (!HasEffect(Buffs.StraightShotReady) && gauge.SoulVoice != 100))) || 
                            (RagingStrikesCD <= 60 && RagingStrikesCD > 0 && (!HasEffect(Buffs.StraightShotReady) && gauge.SoulVoice != 100) && poisonRecast(5) || windRecast(5)));

                        if (useIronJaws)
                            return IronJaws;

                        if (level < 64)
                        {
                            if (!LevelChecked(IronJaws))
                            {
                                if (windbite && windbiteDuration.RemainingTime < 4)
                                    return Windbite;
                                if (venomous && venomousDuration.RemainingTime < 4)
                                    return VenomousBite;
                            }
                            if (LevelChecked(Windbite) && !windbite)
                                return Windbite;
                            if (LevelChecked(VenomousBite) && !venomous)
                                return VenomousBite;
                        }
                        else
                        {
                            if (LevelChecked(Stormbite) && !stormbite)
                                return Stormbite;
                            if (LevelChecked(CausticBite) && !caustic)
                                return CausticBite;
                        }
                    }
                    
                    if (HasEffect(Buffs.BlastArrowReady))
                    {
                        if (GetBuffRemainingTime(Buffs.BlastArrowReady) <= 2.5)
                            return BlastArrow;
                        if (!HasEffect(Buffs.Barrage))
                            return BlastArrow;
                    }
                    if (IsEnabled(CustomComboPreset.BRD_ApexArrow) && LevelChecked(ApexArrow) && gauge.SoulVoice >= 80)
                    {
                        if (RadiantFinaleCD >= 90 && BattleVoiceCD >= 100)
                        {
                            if (gauge.SoulVoice == 100 || (gauge.SoulVoice == 95 && EmpyrealArrowCD <= 2)) 
                                return ApexArrow;
                            if (gauge.SoulVoice >= 80 && RagingStrikesCD <= 108)
                                return ApexArrow;
                        }
                        //48s恢复80点灵魂之声最短时间 - 12s爆发期最晚使用绝峰箭时间 = 36s
                        if (RagingStrikesCD >= 36 && RagingStrikesCD < 100)
                        {
                            //赶紧用掉
                            if (RagingStrikesCD <= 40)
                                return ApexArrow;
                            if (gauge.SoulVoice >= PluginConfiguration.GetCustomIntValue(Config.BRD_whenApexShot))
                                return ApexArrow;
                        }
                        if (RagingStrikesCD < 36 && RagingStrikesCD >= 15 + 12)
                            return ApexArrow;
                        if (RagingStrikesCD < 27 && RagingStrikesCD >= 15 && gauge.SoulVoice == 100)
                            return ApexArrow;
                    }

                    if (HasEffect(Buffs.StraightShotReady) || GetCooldownRemainingTime(Barrage) >= 119.5)
                        return OriginalHook(StraightShot);

                    if (IsEnabled(CustomComboPreset.BRD_ST_TwoTarget))
                    {
                        if (HasEffect(Buffs.ShadowbiteReady))
                            return Shadowbite;
                        return (LevelChecked(Ladonsbite)) ? Ladonsbite : QuickNock;
                    }
                }
                return actionID;
            }
        }

        internal class BRD_AoE_SimpleMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BRD_AoE_SimpleMode;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Ladonsbite or QuickNock)
                {
                    BRDGauge? gauge = GetJobGauge<BRDGauge>();
                    bool canWeave = CanWeave(actionID);
                    var RagingStrikesCD = GetCooldownRemainingTime(RagingStrikes);
                    var BattleVoiceCD = GetCooldownRemainingTime(BattleVoice); 

                    if (IsEnabled(CustomComboPreset.BRD_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.BRD_VariantCure))
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.BRD_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart) &&
                        canWeave)
                        return Variant.VariantRampart;

                    if (IsEnabled(CustomComboPreset.BRD_Songs) && canWeave)
                    {
                        if (gauge.Song == Song.NONE)
                        {
                            if (ActionReady(MagesBallad) && !(JustUsed(WanderersMinuet) || JustUsed(ArmysPaeon)))
                                return MagesBallad;
                            if (ActionReady(ArmysPaeon) && !(JustUsed(MagesBallad) || JustUsed(WanderersMinuet)))
                                return ArmysPaeon;
                        }
                    }

                    if ((gauge.Song != Song.NONE || !LevelChecked(MagesBallad)) && canWeave)
                    {
                        if (ActionReady(RagingStrikes) && IsEnabled(CustomComboPreset.BRD_RagingStrikes))
                            return RagingStrikes;
                        if (ActionReady(EmpyrealArrow))
                            return EmpyrealArrow;
                        if (LevelChecked(RainOfDeath) && GetRemainingCharges(RainOfDeath) == GetMaxCharges(RainOfDeath))
                            return RainOfDeath;

                        if (ActionReady(BattleVoice) && RagingStrikesCD >= 100)
                            return BattleVoice;
                        if (ActionReady(RadiantFinale) && RagingStrikesCD >= 100 && (BattleVoiceCD <= 1.3 || BattleVoiceCD >= 100))
                            return RadiantFinale;
                        if (ActionReady(Barrage) && RagingStrikesCD >= 100)
                            return Barrage;

                        if (LevelChecked(PitchPerfect) && gauge.Song == Song.WANDERER && gauge.Repertoire == 3)
                            return OriginalHook(WanderersMinuet);
                        if (ActionReady(Sidewinder))
                            return Sidewinder;
                        if (ActionReady(RainOfDeath))
                            return RainOfDeath;
                        if (ActionReady(Bloodletter))
                            return Bloodletter;

                        // healing - please move if not appropriate priority
                        if (IsEnabled(CustomComboPreset.BRD_AoE_SecondWind) && ActionReady(All.SecondWind) &&
                            PlayerHealthPercentageHp() <= PluginConfiguration.GetCustomIntValue(Config.BRD_AoESecondWindThreshold))
                            return All.SecondWind;
                    }

                    if (HasEffect(Buffs.BlastArrowReady))
                        return BlastArrow;
                    if (LevelChecked(ApexArrow) && IsEnabled(CustomComboPreset.BRD_ApexArrow) && gauge.SoulVoice >= 80 && RagingStrikesCD >= 15)
                        return ApexArrow;
                    if (HasEffect(Buffs.ShadowbiteReady))
                        return Shadowbite;
                }
                return actionID;
            }
        }

        internal class BRD_Buffs : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BRD_Buffs;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is RagingStrikes)
                {
                    if (GetCooldownRemainingTime(RagingStrikes) >= 119.5 && HasBattleTarget())
                        return RagingStrikes;
                    if (ActionReady(RagingStrikes))
                        return RagingStrikes;
                    if (ActionReady(BattleVoice))
                        return BattleVoice;
                    if (ActionReady(RadiantFinale) && GetCooldownRemainingTime(RagingStrikes) >= 100 && (GetCooldownRemainingTime(BattleVoice) <= 1.3 || GetCooldownRemainingTime(BattleVoice) >= 100))
                        return RadiantFinale;
                    if (ActionReady(Barrage) && !HasEffect(Buffs.StraightShotReady) && GetCooldownRemainingTime(RagingStrikes) > 15)
                        return Barrage;
                }
                return actionID;
            }
        }
        internal class BRD_oGCD : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BRD_oGCD;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (LocalPlayer.ClassJob.Id is BRD.JobID or BRD.ClassID && actionID is All.HeadGraze)
                {
                    if (ActionReady(All.HeadGraze) && CanInterruptEnemy())
                        return All.HeadGraze;

                    if (LevelChecked(EmpyrealArrow) && GetCooldownRemainingTime(EmpyrealArrow) <= 0.7)
                        return EmpyrealArrow;
                    if (LevelChecked(Bloodletter) && GetRemainingCharges(Bloodletter) == GetMaxCharges(Bloodletter))
                        return Bloodletter;
                    if (LevelChecked(Sidewinder) && GetCooldownRemainingTime(Sidewinder) <= 0.7)
                        return Sidewinder;
                    if (ActionReady(Barrage) && !HasEffect(Buffs.StraightShotReady))
                        return Barrage;
                    if (ActionReady(Bloodletter))
                        return Bloodletter;
                }
                return actionID;
            }
        }
    }
}
