using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;
using XIVSlothCombo.Extensions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class AST
    {
        internal const byte JobID = 33;

        internal const uint
            //DPS
            Malefic = 3596,
            Malefic2 = 3598,
            Malefic3 = 7442,
            Malefic4 = 16555,
            FallMalefic = 25871,
            Gravity = 3615,
            Gravity2 = 25872,

            //Cards
            Draw = 3590,
            Play = 17055,
            Redraw = 3593,
            //Obsolete? Left just incase it's needed
            Balance = 4401,
            Bole = 4404,
            Arrow = 4402,
            Spear = 4403,
            Ewer = 4405,
            Spire = 4406,
            MinorArcana = 7443,
            //LordOfCrowns = 7444,
            //LadyOfCrown = 7445,
            Astrodyne = 25870,

            //Utility
            Divination = 16552,
            Lightspeed = 3606,

            //DoT
            Combust = 3599,
            Combust2 = 3608,
            Combust3 = 16554,

            //Healing
            Benefic = 3594,
            Benefic2 = 3610,
            AspectedBenefic = 3595,
            Helios = 3600,
            AspectedHelios = 3601,
            Ascend = 3603,
            EssentialDignity = 3614,
            CelestialOpposition = 16553,
            CelestialIntersection = 16556,
            Horoscope = 16557,
            Exaltation = 25873,
            Macrocosmos = 25874,
            Synastry = 3612,

            EarthlyStar = 7439,
            NeutralSect = 16559;

        //Action Groups
        internal static readonly List<uint>
            MaleficList = [Malefic, Malefic2, Malefic3, Malefic4, FallMalefic],
            GravityList = [Gravity, Gravity2];

        internal static class Buffs
        {
            internal const ushort
                AspectedBenefic = 835,
                AspectedHelios = 836,
                Horoscope = 1890,
                HoroscopeHelios = 1891,
                NeutralSect = 1892,
                NeutralSectShield = 1921,
                Divination = 1878,
                LordOfCrownsDrawn = 2054,
                LadyOfCrownsDrawn = 2055,
                ClarifyingDraw = 2713,
                Macrocosmos = 2718,
                //The "Buff" that shows when you're holding onto the card
                BalanceDrawn = 913,
                BoleDrawn = 914,
                ArrowDrawn = 915,
                SpearDrawn = 916,
                EwerDrawn = 917,
                SpireDrawn = 918,
                //The actual buff that buffs players
                BalanceDamage = 1882,
                BoleDamage = 1883,
                ArrowDamage = 1884,
                SpearDamage = 1885,
                EwerDamage = 1886,
                SpireDamage = 1887,
                Lightspeed = 841,
                SelfSynastry = 845,
                TargetSynastry = 846, 
                
                EarthlyDominance = 1224,
                GiantDominance = 1248,
                Intersection = 1889;
        }

        internal static class Debuffs
        {
            internal const ushort
                Combust = 838,
                Combust2 = 843,
                Combust3 = 1881;
        }

        //Debuff Pairs of Actions and Debuff
        internal static Dictionary<uint, ushort>
            CombustList = new() {
                { Combust,  Debuffs.Combust  },
                { Combust2, Debuffs.Combust2 },
                { Combust3, Debuffs.Combust3 }
            };

        public static ASTGauge Gauge => CustomComboFunctions.GetJobGauge<ASTGauge>();

        public static CardType DrawnCard { get; set; }

        public static class Config
        {
            public static UserInt
                AST_LucidDreaming = new("ASTLucidDreamingFeature"),
                AST_ST_SimpleHeals_Esuna = new("AST_ST_SimpleHeals_Esuna"),
                AST_DPS_CombustOption = new("AST_DPS_CombustOption"),
                AST_QuickTarget_Override = new("AST_QuickTarget_Override");
            public static UserBool
                AST_ST_SimpleHeals_Adv = new("AST_ST_SimpleHeals_Adv"),
                AST_ST_SimpleHeals_UIMouseOver = new("AST_ST_SimpleHeals_UIMouseOver"),
                AST_ST_DPS_CombustUptime_Adv = new("AST_ST_DPS_CombustUptime_Adv");
            public static UserFloat
                AST_ST_DPS_CombustUptime_Threshold = new("AST_ST_DPS_CombustUptime_Threshold");
        }

        internal class AST_Cards_DrawOnPlay : CustomCombo
        {

            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.AST_Cards_DrawOnPlay;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Play)
                {
                    if (GetRemainingCharges(Draw) >= 1 && Gauge.DrawnCard.Equals(CardType.NONE))
                        return Draw;
                }
                return actionID;
            }
        }

        internal class AST_ST_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.AST_ST_DPS;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (MaleficList.Contains(actionID) || (IsEnabled(CustomComboPreset.AST_AoE_DPS) && GravityList.Contains(actionID)))
                {
                    //Grab current DoT via OriginalHook, grab it's fellow debuff ID from Dictionary, then check for the debuff
                    uint dot = OriginalHook(Combust);
                    Status? dotDebuff = FindTargetEffect(CombustList[dot]);
                    float divinationCD = GetCooldownRemainingTime(Divination);
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                    if (ActionReady(Draw) && Gauge.DrawnCard.Equals(CardType.NONE) && GetRemainingCharges(Draw) == 2)
                        return Draw;

                    if (InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.AST_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart) &&
                        CanSpellWeave(actionID))
                            return Variant.VariantRampart;

                        Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                        if (IsEnabled(CustomComboPreset.AST_Variant_SpiritDart) &&
                            IsEnabled(Variant.VariantSpiritDart) &&
                            (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) &&
                            CanSpellWeave(actionID) &&
                            IsEnabled(CustomComboPreset.AST_AoE_DPS) && GravityList.Contains(actionID))
                            return Variant.VariantSpiritDart;



                        if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 2400)
                            return All.LucidDreaming;

                        if (ActionReady(OriginalHook(MinorArcana)) && Gauge.DrawnCrownCard is CardType.NONE && CanSpellWeave(actionID))
                            return OriginalHook(MinorArcana);

                        if (IsEnabled(CustomComboPreset.AST_DPS_LightSpeed) && ActionReady(Lightspeed) && CanSpellWeave(actionID) &&
                            (divinationCD < 5 || AllAttack))
                            return Lightspeed;

                        //Divination
                        if (IsEnabled(CustomComboPreset.AST_DPS_Divination) && ActionReady(Divination) && CanDelayedWeave(actionID) &&
                            GetRemainingCharges(Draw) <= 1 && ((IsOnCooldown(Lightspeed) && GetCooldownRemainingTime(Lightspeed) >= 55 && GetCooldownRemainingTime(Lightspeed) <= 86.2) || AllAttack))
                            return Divination;

                        //Astrodyne
                        if (ActionReady(Astrodyne) && !Gauge.ContainsSeal(SealType.NONE) && CanSpellWeave(actionID) &&
                            ((IsOnCooldown(Divination) && (divinationCD >= 90 || divinationCD <= 20)) || AllAttack))
                            return Astrodyne;

                        //Card Draw
                        if (ActionReady(Draw) && Gauge.DrawnCard is CardType.NONE && CanSpellWeave(actionID))
                            return Draw;

                        //Redraw Card
                        if (HasEffect(Buffs.ClarifyingDraw) && ActionReady(Redraw)) //&& divinationCD >= 5)
                        {
                            var cardDrawn = Gauge.DrawnCard;
                            bool canRedraw = (cardDrawn is CardType.BALANCE or CardType.BOLE && Gauge.Seals.Contains(SealType.SUN)) ||
                                (cardDrawn is CardType.ARROW or CardType.EWER && Gauge.Seals.Contains(SealType.MOON)) ||
                                (cardDrawn is CardType.SPEAR or CardType.SPIRE && Gauge.Seals.Contains(SealType.CELESTIAL));

                            if (Gauge.ContainsSeal(SealType.NONE) && IsOnCooldown(Divination) && cardDrawn is CardType.BOLE or CardType.EWER or CardType.SPIRE)
                                return Redraw;
                            if (canRedraw && CanSpellWeave(actionID))
                            {
                                if (divinationCD >= 100 && GetCooldownRemainingTime(Lightspeed) >= 75)
                                    return Redraw;
                                if (divinationCD < 100)
                                    return Redraw;
                            }
                        }

                        //Play Card
                        if (IsEnabled(CustomComboPreset.AST_DPS_AutoPlay) && ActionReady(Play) && Gauge.DrawnCard is not CardType.NONE && CanSpellWeave(actionID, 0.5))
                        {
                            //防溢出
                            if (GetRemainingCharges(Draw) == 2)
                                return OriginalHook(Play);
                            if (level < 50 || AllAttack)
                                return OriginalHook(Play);
                            //60s窗口
                            if ((IsOnCooldown(Divination) && divinationCD <= 62.5 && (GetRemainingCharges(Draw) == 1 || GetPartyMembers().Length == 4) && divinationCD >= 45))
                                return OriginalHook(Play);
                            //120s
                            if (IsOnCooldown(Lightspeed) && (divinationCD >= 95 || divinationCD <= 5))
                                return OriginalHook(Play);
                        }

                        //Minor Arcana / Lord of Lady，如果是王冠之贵妇还没有用掉，占卜前13秒扔了
                        if (Gauge.DrawnCrownCard is CardType.LADY && CanWeave(actionID) && IsOnCooldown(Divination) && divinationCD <= 13)
                            return OriginalHook(MinorArcana);

                        //Minor Arcana / Lord of Crowns
                        if (ActionReady(OriginalHook(MinorArcana)) && IsEnabled(CustomComboPreset.AST_DPS_LazyLord) && Gauge.DrawnCrownCard is CardType.LORD && CanSpellWeave(actionID) && HasBattleTarget() &&
                            (IsOnCooldown(Divination) && divinationCD >= 40 || AllAttack))
                            return OriginalHook(MinorArcana);

                        if (IsEnabled(CustomComboPreset.AST_DPS_Lucid) && ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= Config.AST_LucidDreaming && CanSpellWeave(actionID) &&
                            !ActionReady(EarthlyStar) && ((divinationCD <= 100 && divinationCD >= 60) || (divinationCD <= 42 && divinationCD >= 8)))
                            return All.LucidDreaming;

                        if (HasBattleTarget())
                        {
                            //Combust
                            if (IsEnabled(CustomComboPreset.AST_ST_DPS_CombustUptime) && !GravityList.Contains(actionID) && ActionReady(Combust))
                            {
                                float refreshtimer = Config.AST_ST_DPS_CombustUptime_Adv ? Config.AST_ST_DPS_CombustUptime_Threshold : 2;

                                if (IsEnabled(CustomComboPreset.AST_Variant_SpiritDart) && IsEnabled(Variant.VariantSpiritDart) &&
                                    (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) &&
                                    CanSpellWeave(actionID))
                                    return Variant.VariantSpiritDart;

                                if (!AllAttack && (dotDebuff is null || dotDebuff.RemainingTime <= refreshtimer || (IsMoving && IsOffCooldown(Malefic) && !HasEffect(Buffs.Lightspeed) && dotDebuff.RemainingTime <= 8) ||
                                    (divinationCD >= 100 && divinationCD <= 107.5 && dotDebuff.RemainingTime <= 15)) &&
                                    GetTargetHPPercent() > Config.AST_DPS_CombustOption && (level < 90 || EnemyHealthCurrentHp() >= 45000))
                                    return dot;
                                //AlterateMode idles as Malefic
                            }
                        }
                    }
                }
                return actionID;
            }
        }

        internal class AST_GCD_AoE_Heals : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.AST_GCD_AoE_Heals;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Helios)
                {
                    if (IsEnabled(CustomComboPreset.AST_AspectedHelios_NeutralSect) && ActionReady(NeutralSect) && HasBattleTarget())
                        return NeutralSect;

                    //Level check to exit if we can't use
                    if (LevelChecked(AspectedHelios) && LocalPlayer.CurrentMp >= 800)
                    {
                        if (!HasEffect(Buffs.AspectedHelios))
                            return AspectedHelios;
                        if (HasEffect(Buffs.NeutralSect) && !HasEffect(Buffs.NeutralSectShield))
                            return AspectedHelios;
                        if (HasBattleTarget() && GetBuffRemainingTime(Buffs.AspectedHelios) <= 3)
                            return AspectedHelios;
                        if (!HasBattleTarget() && GetBuffRemainingTime(Buffs.AspectedHelios) <= 7)
                            return AspectedHelios;
                    }
                }
                return actionID;
            }
        }

        internal class AST_ST_SimpleHeals : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.AST_ST_SimpleHeals;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Benefic)
                {
                    //Grab our target (Soft->Hard->Self)
                    GameObject? healTarget = GetHealTarget(Config.AST_ST_SimpleHeals_Adv && Config.AST_ST_SimpleHeals_UIMouseOver);

                    if (IsEnabled(CustomComboPreset.AST_ST_SimpleHeals_Esuna) && ActionReady(All.Esuna) && HasCleansableDebuff(healTarget) && 
                        GetTargetHPPercent(healTarget) >= Config.AST_ST_SimpleHeals_Esuna)
                        return All.Esuna;
                    if (LevelChecked(Benefic2) && LocalPlayer.CurrentMp >= 700 && !HasBattleTarget() && !IsMoving)
                        return Benefic2;
                    if (ActionReady(EssentialDignity) || GetCooldownChargeRemainingTime(EssentialDignity) >= 39)
                        return EssentialDignity;
                    if (ActionReady(Synastry))
                        return Synastry;
                    if (LevelChecked(Benefic2) && LocalPlayer.CurrentMp >= 700)
                        return Benefic2;
                }
                return actionID;
            }
        }

        internal class AST_Exaltation : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.AST_Exaltation;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                {
                    if (actionID == Exaltation)
                    {
                        if (ActionReady(Exaltation))
                            return Exaltation;
                        if (ActionReady(CelestialIntersection) && GetCooldownRemainingTime(Exaltation) >= 59.2)
                            return CelestialIntersection;
                    }
                    return actionID;
                }
            }
        }

        //写一个地星，王冠贵妇，天星冲日
        internal class AST_oGCD_AoE_Heals : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.AST_oGCD_AoE_Heals;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is EarthlyStar)
                {
                    //根据buff判定太慢了，补充一个作为保护
                    if (IsOnCooldown(EarthlyStar) && GetCooldownRemainingTime(EarthlyStar) >= 59.3)
                        return OriginalHook(EarthlyStar);

                    if (HasEffect(Buffs.GiantDominance))
                        return OriginalHook(EarthlyStar);

                    if (level >= 62 & IsOffCooldown(EarthlyStar))
                        return OriginalHook(EarthlyStar);

                    if (InCombat() && Gauge.DrawnCrownCard is CardType.LADY)
                        return OriginalHook(MinorArcana);

                    if (ActionReady(CelestialOpposition))
                        return CelestialOpposition;

                    if (HasEffect(Buffs.EarthlyDominance))
                        return CelestialOpposition;

                    if (level < 62)
                        return CelestialOpposition;
                }

                return actionID;
            }
        }
    }
}