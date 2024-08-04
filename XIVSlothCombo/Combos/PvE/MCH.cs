using Dalamud.Game.ClientState.JobGauge.Types;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ImGuiScene;
using Lumina.Excel.GeneratedSheets2;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;
using XIVSlothCombo.Extensions;
using static XIVSlothCombo.Combos.PvE.ADV;


namespace XIVSlothCombo.Combos.PvE
{
    internal class MCH
    {
        public const byte JobID = 31;

        internal const uint
            CleanShot = 2873,
            HeatedCleanShot = 7413,
            SplitShot = 2866,
            HeatedSplitShot = 7411,
            SlugShot = 2868,
            GaussRound = 2874,
            Ricochet = 2890,
            HeatedSlugshot = 7412,
            Drill = 16498,
            HotShot = 2872,
            Reassemble = 2876,
            AirAnchor = 16500,
            Hypercharge = 17209,
            HeatBlast = 7410,
            SpreadShot = 2870,
            Scattergun = 25786,
            AutoCrossbow = 16497,
            RookAutoturret = 2864,
            RookOverdrive = 7415,
            AutomatonQueen = 16501,
            QueenOverdrive = 16502,
            Tactician = 16889,
            ChainSaw = 25788,
            BioBlaster = 16499,
            BarrelStabilizer = 7414,
            Wildfire = 2878,
            Dismantle = 2887,
            FlameThrower = 7418;

        internal static class Buffs
        {
            internal const ushort
                Reassembled = 851,
                Tactician = 1951,
                Wildfire = 1946,
                Overheated = 2688,
                Flamethrower = 1205;
        }

        internal static class Debuffs
        {
            internal const ushort
            Dismantled = 2887;
        }

        internal static class Config
        {
            public static UserInt
                MCH_VariantCure = new("MCH_VariantCure");
            public static UserFloat
                MCH_BarrelStabilizerOpenTime = new("MCH_BarrelStabilizerOpenTime");
        }

        internal class MCH_ST_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_ST_DPS;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is SplitShot or HeatedSplitShot)
                {
                    MCHGauge? gauge = GetJobGauge<MCHGauge>();

                    var BarrelStabilizerCD = GetCooldownRemainingTime(BarrelStabilizer);
                    var DrillCD = LevelChecked(Drill) ? GetCooldownRemainingTime(Drill) : 20;
                    var AirAnchorCD = LevelChecked(AirAnchor) ? GetCooldownRemainingTime(AirAnchor) : (LevelChecked(HotShot) ? GetCooldownRemainingTime(HotShot) : 40);
                    var ChainSawCD = LevelChecked(ChainSaw) ? GetCooldownRemainingTime(ChainSaw) : 60;

                    var timeForReassemble = gauge.IsOverheated ? 1 : 2;

                    var ricochetCharges = GetRemainingCharges(Ricochet);
                    var gaussRoundCharges = GetRemainingCharges(GaussRound);

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Cure) &&
                    IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= Config.MCH_VariantCure)
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart) &&
                        CanWeave(actionID))
                        return Variant.VariantRampart;

                    if (CanWeave(actionID))
                    {
                        if (ActionReady(All.HeadGraze) && IsEnabled(CustomComboPreset.ALL_Ranged_Interrupt) && CanInterruptEnemy())
                            return All.HeadGraze;

                        if (ActionReady(BarrelStabilizer) && IsEnabled(CustomComboPreset.MCH_BarrelStabilizer) && gauge.Heat <= 65 && 
                            CombatEngageDuration().TotalSeconds > PluginConfiguration.GetCustomFloatValue(Config.MCH_BarrelStabilizerOpenTime))
                            return BarrelStabilizer;

                        if (ActionReady(RookAutoturret) && IsEnabled(CustomComboPreset.MCH_ST_AutomatonQueen) && !gauge.IsRobotActive && gauge.Battery >= 50 && BarrelStabilizerCD > 5 && HasBattleTarget())
                        {
                            //防溢出
                            if (gauge.Battery >= 90 && (AirAnchorCD <= 2 || ChainSawCD <= 2))
                                return OriginalHook(RookAutoturret);
                            if (gauge.Battery >= 100 && lastComboMove is SlugShot or HeatedSlugshot && DrillCD > 2 && AirAnchorCD > 2 && ChainSawCD > 2 
                                && BarrelStabilizerCD > 1.5 && gauge.Heat < 100 && !gauge.IsOverheated &&
                                !(ActionReady(Hypercharge) && IsEnabled(CustomComboPreset.MCH_ST_Hypercharge) && gauge.Heat >= 50 && DrillCD > 8 && AirAnchorCD > 8 && ChainSawCD > 8 
                                && LevelChecked(BarrelStabilizer) && BarrelStabilizerCD < 100 && BarrelStabilizerCD > 37.5))
                                return OriginalHook(RookAutoturret);
                        }

                        if (ActionReady(Reassemble) && (!HasEffect(Buffs.Reassembled) || !WasLastAbility(Reassemble)) && HasBattleTarget() &&
                            (DrillCD <= timeForReassemble || (LevelChecked(AirAnchor) && AirAnchorCD <= timeForReassemble) || ChainSawCD <= timeForReassemble || 
                            (!LevelChecked(Drill) && ((LevelChecked(CleanShot) && lastComboMove is SlugShot) || (!LevelChecked(CleanShot) && GetCooldownRemainingTime(HotShot) <= 2)))))
                        {
                            if (BarrelStabilizerCD >= 100)
                            {
                                if (DrillCD <= timeForReassemble || (LevelChecked(AirAnchor) && AirAnchorCD <= timeForReassemble))
                                    return Reassemble;
                                if (ChainSawCD <= timeForReassemble && BarrelStabilizerCD <= 118.3 &&
                                    (DrillCD <= 10.8 || !gauge.IsOverheated || 1.5 * GetBuffStacks(Buffs.Overheated) > BarrelStabilizerCD - 100))
                                    return Reassemble;
                            }

                            if (BarrelStabilizerCD < 100 && BarrelStabilizerCD > 0)
                            {
                                if (level >= 84)
                                {
                                    if (GetCooldownChargeRemainingTime(Reassemble) <= BarrelStabilizerCD)
                                        return Reassemble;
                                }
                                else
                                {
                                    if (BarrelStabilizerCD > 15 || !LevelChecked(BarrelStabilizer))
                                        return Reassemble;
                                }
                            }
                            //防溢出
                            if (GetRemainingCharges(Reassemble) == GetMaxCharges(Reassemble))
                                return Reassemble;
                        }

                        if (ActionReady(Wildfire) && IsEnabled(CustomComboPreset.MCH_ST_Wildfire) && CanDelayedWeave(actionID) && //BarrelStabilizerCD >= 100
                            (DrillCD > 8 || DrillCD <= 2) && AirAnchorCD > 8 && ((gauge.Heat >= 100 && ChainSawCD > 4.5) || ChainSawCD <= 2 || ChainSawCD > 8) && 
                            ((GetCooldownRemainingTime(Hypercharge) <= 2.5 && gauge.Heat >= 50) || gauge.IsOverheated || GetCooldownRemainingTime(Hypercharge) >= 9.5))
                        {
                            if (LevelChecked(ChainSaw))
                            {
                                if (gauge.Battery >= 30 && gauge.Battery < 50 && (ChainSawCD <= 2.5 || AirAnchorCD <= 2.5))
                                    return Wildfire;
                                if (gauge.Battery >= 40 && gauge.Battery < 50 && DrillCD > 2.5 && lastComboMove is HeatedSlugshot or SlugShot)
                                    return Wildfire;
                                if (gauge.Battery >= 50 || gauge.IsRobotActive || IsOnCooldown(OriginalHook(RookAutoturret)))
                                    return Wildfire;
                            }
                            else
                                return Wildfire;
                        }

                        if (ActionReady(RookAutoturret) && IsEnabled(CustomComboPreset.MCH_ST_AutomatonQueen) && !gauge.IsRobotActive && gauge.Battery >= 50 && HasBattleTarget())
                        {
                            //爆发期
                            if (LevelChecked(AutomatonQueen))
                            {
                                if (BarrelStabilizerCD >= 105)
                                {
                                    if (BarrelStabilizerCD <= 118 || gauge.Battery >= 100)
                                        return OriginalHook(RookAutoturret);
                                    if (AirAnchorCD > 2 && ChainSawCD > 2 && !(lastComboMove is SlugShot or HeatedSlugshot))
                                        return OriginalHook(RookAutoturret);
                                    if (lastComboMove is SlugShot or HeatedSlugshot && (DrillCD <= 2 || gauge.IsOverheated || GetCooldownRemainingTime(Hypercharge) >= 9.5))
                                        return OriginalHook(RookAutoturret);
                                }
                            }
                            else
                            {
                                if (BarrelStabilizerCD >= 100)
                                {
                                    if (gauge.Battery >= 100 || BarrelStabilizerCD <= 110)
                                        return OriginalHook(RookAutoturret);
                                }
                            }
                            //非爆发期
                            if (BarrelStabilizerCD < 100 && BarrelStabilizerCD >= 43)
                            {
                                if (LevelChecked(ChainSaw) && BarrelStabilizerCD <= 55)
                                    return OriginalHook(RookAutoturret);
                                if (!LevelChecked(ChainSaw))
                                    return OriginalHook(RookAutoturret);
                            }
                        }

                        if (LevelChecked(Ricochet) && ricochetCharges >= GetMaxCharges(Ricochet) - 1 && GetCooldownChargeRemainingTime(Ricochet) - (gauge.IsOverheated ? 15 : 0) <= 0.7)
                            return Ricochet;
                        if (LevelChecked(GaussRound) && gaussRoundCharges >= GetMaxCharges(GaussRound))
                            return GaussRound;

                        if (ActionReady(Hypercharge) && IsEnabled(CustomComboPreset.MCH_ST_Hypercharge) && gauge.Heat >= 50 && DrillCD > 8 && AirAnchorCD > 8 && ChainSawCD > 2)// && (ChainSawCD > 8 || DrillCD >= 18))
                        {
                            //防溢出
                            if (gauge.Heat >= 100 && ChainSawCD > 4.5)
                                return Hypercharge;
                            if (LevelChecked(BarrelStabilizer))
                            {
                                //非爆发期
                                if (BarrelStabilizerCD < 100 && BarrelStabilizerCD > 37.5 && ChainSawCD > 4.5)
                                    return Hypercharge;
                                //爆发期
                                if (BarrelStabilizerCD <= 3 && BarrelStabilizerCD > 0 && ChainSawCD > 4.5 && gauge.Heat >= 60)
                                    return Hypercharge;
                                if (BarrelStabilizerCD >= 100)
                                {
                                    if (LevelChecked(ChainSaw) && (gauge.Battery >= 50 || gauge.IsRobotActive || IsOnCooldown(OriginalHook(RookAutoturret))))
                                        return Hypercharge;
                                    if (!LevelChecked(ChainSaw))
                                        return Hypercharge;
                                }
                            }

                            if (!LevelChecked(BarrelStabilizer) && LevelChecked(Wildfire) && GetCooldownRemainingTime(Wildfire) > 37.5)
                                return Hypercharge;
                            if (!LevelChecked(Wildfire))
                                return Hypercharge;
                        }

                        if (ActionReady(Ricochet) && (IsEnabled(CustomComboPreset.MCH_ST_Ricochet) || BarrelStabilizerCD >= 100) && ricochetCharges >= gaussRoundCharges && 
                            (BarrelStabilizerCD > 0 || CombatEngageDuration().TotalSeconds >= 20))
                            return Ricochet;
                        if (ActionReady(GaussRound) && (IsNotEnabled(CustomComboPreset.MCH_ST_Ricochet) || ricochetCharges < gaussRoundCharges) &&
                            (BarrelStabilizerCD > 0 || CombatEngageDuration().TotalSeconds >= 20))
                            return GaussRound;
                    }

                    //GCD
                    if (LevelChecked(OriginalHook(AirAnchor)) && AirAnchorCD <= 0.5)
                        return OriginalHook(AirAnchor);
                    if (LevelChecked(Drill) && DrillCD <= 0.5)
                        return Drill;
                    if (LevelChecked(ChainSaw) && ChainSawCD <= 0.5 && (BarrelStabilizerCD > 10 || CombatEngageDuration().TotalSeconds > 20) && BarrelStabilizerCD <= 117.5 && HasBattleTarget() && 
                        (DrillCD <= 10.8 || !gauge.IsOverheated || (BarrelStabilizerCD >= 100 && 1.5 * GetBuffStacks(Buffs.Overheated) > BarrelStabilizerCD - 100)))
                        return ChainSaw;

                    if (LevelChecked(HeatBlast) && gauge.IsOverheated)
                        return HeatBlast;

                    if (LevelChecked(SlugShot) && lastComboMove == SplitShot)
                        return OriginalHook(SlugShot);
                    if (LevelChecked(CleanShot) && lastComboMove == SlugShot)
                        return OriginalHook(CleanShot);
                }
                return actionID;
            }
        }


        internal class MCH_AoE : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_AoE;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is SpreadShot or Scattergun)
                {
                    MCHGauge? gauge = GetJobGauge<MCHGauge>();
                    var BarrelStabilizerCD = GetCooldownRemainingTime(BarrelStabilizer);

                    var gaussCharges = GetRemainingCharges(GaussRound);
                    var ricochetCharges = GetRemainingCharges(Ricochet);

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Cure) &&
                     IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.MCH_VariantCure))
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart) &&
                        CanWeave(actionID))
                        return Variant.VariantRampart;

                    if (HasEffect(Buffs.Flamethrower) || GetCooldownRemainingTime(FlameThrower) >= 59)
                        return FlameThrower;

                    if (CanWeave(actionID))
                    {
                        if (LevelChecked(Ricochet) && GetRemainingCharges(Ricochet) == GetMaxCharges(Ricochet))
                            return Ricochet;
                        if (LevelChecked(Ricochet) && GetRemainingCharges(GaussRound) == GetMaxCharges(GaussRound))
                            return GaussRound;

                        if (ActionReady(BarrelStabilizer) && IsEnabled(CustomComboPreset.MCH_BarrelStabilizer) && gauge.Heat <= 55)
                            return BarrelStabilizer;

                        if (ActionReady(Reassemble) && (!HasEffect(Buffs.Reassembled) || !WasLastAbility(Reassemble) ) && LevelChecked(ChainSaw) && GetCooldownRemainingTime(ChainSaw) <= 2 &&
                            IsOnCooldown(BioBlaster) && IsOnCooldown(OriginalHook(AirAnchor)) 
                            && !gauge.IsOverheated && BarrelStabilizerCD >= 100)
                            return Reassemble;

                        if (ActionReady(Hypercharge) && gauge.Heat >= 50 && LevelChecked(AutoCrossbow) && !gauge.IsOverheated && IsOnCooldown(OriginalHook(AirAnchor)))
                        {
                            if ((!LevelChecked(ChainSaw) || IsOnCooldown(ChainSaw)) && (!LevelChecked(BioBlaster) || IsOnCooldown(BioBlaster)) && (!LevelChecked(Drill) || IsOnCooldown(Drill)))
                                return Hypercharge;
                        }
                        if (ActionReady(Ricochet) && ricochetCharges >= gaussCharges)
                            return Ricochet;
                        if (ActionReady(GaussRound) && ricochetCharges < gaussCharges)
                            return GaussRound;

                        if (PlayerHealthPercentageHp() <= 24 && ActionReady(All.SecondWind))
                            return All.SecondWind;
                    }

                    if (ActionReady(OriginalHook(AirAnchor)))
                        return OriginalHook(AirAnchor);
                    if (BarrelStabilizerCD >= 5)
                    {
                        if (ActionReady(BioBlaster))
                            return BioBlaster;
                        if (ActionReady(ChainSaw))
                            return ChainSaw;
                    }
                    if (!LevelChecked(BioBlaster) && ActionReady(Drill))
                        return Drill;

                    if (gauge.IsOverheated && LevelChecked(AutoCrossbow))
                        return AutoCrossbow;
                    if (ActionReady(FlameThrower) && !gauge.IsOverheated && BarrelStabilizerCD >= 10)
                        return FlameThrower;
                }
                return actionID;
            }
        }

        internal class MCH_AutomatonQueen_QueenOverdrive : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_AutomatonQueen_QueenOverdrive;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is RookAutoturret or AutomatonQueen)
                {
                    MCHGauge? gauge = GetJobGauge<MCHGauge>();
                    if (gauge.IsRobotActive)
                        return OriginalHook(QueenOverdrive);
                }
                return actionID;
            }
        }

        internal class MCH_AirAnchor_Drill_Chainsaw : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_AirAnchor_Drill_Chainsaw;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Drill)
                {
                    if (ActionReady(OriginalHook(AirAnchor)))
                        return OriginalHook(AirAnchor);
                    if (ActionReady(Drill))
                        return Drill;
                    if (ActionReady(ChainSaw))
                        return ChainSaw;
                }
                return actionID;
            }
        }

        internal class MCH_Hypercharge_Protection : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_Hypercharge_Protection;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is All.HeadGraze && LocalPlayer.ClassJob.Id is MCH.JobID)
                {
                    if (IsOnCooldown(All.HeadGraze))
                        return Hypercharge;

                    var DrillCD = LevelChecked(Drill) ? GetCooldownRemainingTime(Drill) : 20;
                    var AirAnchorCD = LevelChecked(AirAnchor) ? GetCooldownRemainingTime(AirAnchor) : (LevelChecked(HotShot) ? GetCooldownRemainingTime(HotShot) : 40);
                    var ChainSawCD = LevelChecked(ChainSaw) ? GetCooldownRemainingTime(ChainSaw) : 60;

                    if (ActionReady(Hypercharge) && GetJobGauge<MCHGauge>().Heat >= 50 && DrillCD > 8 && AirAnchorCD > 8 && (ChainSawCD > 8 || IsOffCooldown(BarrelStabilizer)))
                        return Hypercharge;
                }
                return actionID;
            }
        }
        internal class MCH_Wildfire_FlameThrower : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_Wildfire_FlameThrower;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is FlameThrower)
                {
                    if (HasBattleTarget())
                        return OriginalHook(Wildfire);
                }
                return actionID;
            }
        }
        internal class MCH_ChainSaw_Ricochet : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_ChainSaw_Ricochet;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is ChainSaw)
                {
                    if (LevelChecked(ChainSaw) && GetCooldownRemainingTime(ChainSaw) <= 4.5)
                    {
                        if (CanWeave(SplitShot))
                        {
                            if (ActionReady(Reassemble) && (!HasEffect(Buffs.Reassembled) || !WasLastAbility(Reassemble)))
                                return Reassemble;
                            if (ActionReady(Ricochet))
                                return Ricochet;
                        }
                        return ChainSaw;
                    }
                    
                    if (CanWeave(SplitShot) && ActionReady(Ricochet) && GetCooldownRemainingTime(ChainSaw) >= 55)
                        return Ricochet;
                }
                return actionID;
            }
        }
    }
}