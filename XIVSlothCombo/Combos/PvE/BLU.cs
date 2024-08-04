using Dalamud.Game.ClientState.Conditions;
using System;
using System.Linq;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.Data;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class BLU
    {
        public const byte JobID = 36;

        public const uint
            Loom = 11401, 
            RubyDynamics = 34571,
            CondensedLibra = 18321,
            Launcher = 18313,
            FireAngon = 11425,
            Missile = 11405,
            WaterCannon = 11385,
            RoseOfDestruction = 23275,
            ShockStrike = 11429,
            FeatherRain = 11426,
            JKick = 18325,
            Eruption = 11427,
            SharpenedKnife = 11400,
            GlassDance = 11430,
            SonicBoom = 18308,
            Surpanakha = 18323,
            Nightbloom = 23290,
            MoonFlute = 11415,
            Whistle = 18309,
            Tingle = 23265,
            TripleTrident = 23264,
            MatraMagic = 23285,
            FinalSting = 11407,
            Bristle = 11393,
            PhantomFlurry = 23288,
            PerpetualRay = 18314,
            AngelWhisper = 18317,
            SongOfTorment = 11386,
            RamsVoice = 11419,
            Ultravibration = 23277,
            Devour = 18320,
            Offguard = 11411,
            BadBreath = 11388,
            MagicHammer = 18305,
            WhiteKnightsTour = 18310,
            BlackKnightsTour = 18311,
            PeripheralSynthesis = 23286,
            BasicInstinct = 23276,
            HydroPull = 23282,

            SaintlyBeam = 23270,
            DeepClean = 34570,
            PeatPelt = 34569,
            Quasar = 18324,
            ConvictionMarcato = 34574,
            WingedReprobation = 34576,
            Apokalypsis = 34581,
            SeaShanty = 34580,
            Rehydration = 34566,
            ChelonianGate = 23273,
            BeingMortal = 34582,
            CandyCane = 34578,
            DivinationRune = 34572,
            MortalFlame = 34579,
            BreathofMagic = 34567,
            GoblinPunch = 34563,
            DragonForce = 23280,
            MightyGuard = 11417,
            DivineCataract = 23274,
            TheLook = 11399,
            PhantomFlurry2 = 23289,
            Diamondback = 11424,
            AetherialMimicry = 18322,
            BothEnds = 23287,
            ColdFog = 23267,
            WhiteDeath = 23268,
            RevengeBlast = 18316,
            ChocoMeteor = 23284,
            VeiloftheWhorl = 11431,
            MountainBuster = 11428,
            ToadOil = 11410,
            BombToss = 11396,
            BloodDrain = 11395,
            WhiteWind = 11406,
            FlyingSardine = 11423,
            PeculiarLight = 11421,
            MustardBomb = 23279;

        public static class Buffs
        {
            public const ushort
                MoonFlute = 1718,
                Bristle = 1716,
                WaningNocturne = 1727,
                PhantomFlurry = 2502,
                Tingle = 2492,
                Whistle = 2118,
                TankMimicry = 2124,
                DPSMimicry = 2125,
                BasicInstinct = 2498,
                WingedReprobation = 3640,

                HealerMimicry = 2126,
                DeepClean = 3637,
                ConvictionMarcato = 3641,
                Apokalypsis = 3644,
                DragonForce = 2500,
                MightyGuard = 1719,
                ChelonianGate = 2496,
                AuspiciousTrance = 2497,
                Diamondback = 1722,
                DevourHP = 2120,
                SurpanakhaFury = 2130,
                TouchofFrost = 2494,
                ToadOil = 1737;
        }

        public static class Debuffs
        {
            public const ushort
                Slow = 9,
                Bind = 13,
                Stun = 142,
                Bleeding = 1714,
                SongOfTorment = 273,
                DeepFreeze = 1731,
                Offguard = 1717,
                Malodorous = 1715,
                Conked = 2115,
                Lightheaded = 2501,
                MortalFlame = 3643,
                BreathOfMagic = 3712,
                AstralAttenuation = 2121,
                UmbralAttenuation = 2122,
                PhysicalAttenuation = 2123,
                BreathofMagic = 3712,
                PeculiarLight = 1721,
                MustardBomb = 2499,
                PeatPelt = 3636,
                Begrimed = 3636;
        }

        public static class Config
        {
            public const string
                BLU_DPS_FinalSting = "BLU_DPS_FinalSting",
                BLU_DPS_WhiteWind = "BLU_DPS_WhiteWind";
        }

        internal class BLU_Ultravibration : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_Ultravibration;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is TheLook)
                {
                    if (IsSpellActive(Ultravibration) && IsOffCooldown(Ultravibration) && TargetHasEffectAny(Debuffs.DeepFreeze))
                        return Ultravibration;
                    if (!InMeleeRange() && IsSpellActive(HydroPull))
                        return HydroPull;
                    if (IsSpellActive(RamsVoice))
                        return RamsVoice;
                }
                return actionID;
            }
        }

        internal class BLU_JKick_Loom : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_JKick_Loom;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is JKick)
                {
                    if (IsSpellActive(Loom) && (IsOnCooldown(JKick) || !IsSpellActive(JKick) || IsNotEnabled(CustomComboPreset.BLU_DPS_Jkick)))
                        return Loom;
                }
                return actionID;
            }
        }

        internal class BLU_PeatPelt_DeepClean : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_PeatPelt_DeepClean;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is DeepClean)
                {
                    if (IsSpellActive(DeepClean) && TargetHasEffect(Debuffs.PeatPelt))
                        return DeepClean;
                    if (IsSpellActive(PeatPelt) && IsSpellActive(DeepClean) && GetBuffRemainingTime(Buffs.DeepClean) <= 5)
                        return PeatPelt;
                }
                return actionID;
            }
        }

        internal class BLU_AOE : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_AOE;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is WhiteKnightsTour)
                {
                    var HasMoonFluteEffect = HasEffect(Buffs.MoonFlute);
                    var MoonFluteRemainingTime = GetBuffRemainingTime(Buffs.MoonFlute);
                    var TargetDistance = GetTargetDistance();

                    //oGCD
                    if (HasEffect(Buffs.Apokalypsis) || GetCooldownRemainingTime(Apokalypsis) >= 118.5)
                        return Apokalypsis;
                    if (HasEffect(Buffs.PhantomFlurry) || GetCooldownRemainingTime(PhantomFlurry) >= 118.5)
                    {
                        if (GetCooldownRemainingTime(PhantomFlurry) <= 116)
                            return PhantomFlurry2;
                        else
                            return PhantomFlurry;
                    }

                    if (CanSpellWeave(actionID, 0.3))
                    {
                        if (IsSpellActive(Eruption) && IsOffCooldown(Eruption))
                            return Eruption;
                        if (IsSpellActive(FeatherRain) && IsOffCooldown(FeatherRain))
                            return FeatherRain;
                        if (IsSpellActive(GlassDance) && IsOffCooldown(GlassDance))
                            return GlassDance;
                    }

                    //GCD
                    if (IsSpellActive(Devour) && ActionReady(Devour) && InCombat() && HasEffect(Buffs.TankMimicry) && TargetDistance <= 3)
                        return Devour;
                    if (IsSpellActive(DeepClean) && TargetHasEffect(Debuffs.PeatPelt))
                        return DeepClean;
                    if (IsSpellActive(PeatPelt) && IsSpellActive(DeepClean) && GetBuffRemainingTime(Buffs.DeepClean) <= 3 && LocalPlayer.CurrentHp / LocalPlayer.MaxHp <= 95)
                        return PeatPelt;
                    if (IsSpellActive(BreathofMagic) && !TargetHasEffectAny(Debuffs.BreathofMagic) && HasEffect(Buffs.Bristle))
                        return BreathofMagic;

                    if (IsSpellActive(RamsVoice) && GetDebuffRemainingTime(Debuffs.DeepFreeze) <= 3)
                        return RamsVoice;

                    if (IsSpellActive(BreathofMagic) && !TargetHasEffectAny(Debuffs.BreathofMagic))
                    {
                        if (IsSpellActive(Bristle) && !HasEffect(Buffs.Bristle))
                            return Bristle;
                    }

                    if (IsSpellActive(Launcher) && LocalPlayer.CurrentMp >= 8000)
                        return Launcher;

                    if (IsSpellActive(HydroPull) && (!HasBattleTarget() || !InMeleeRange()))
                        return HydroPull;

                    if (IsSpellActive(MustardBomb) && IsSpellActive(PeripheralSynthesis) && GetDebuffRemainingTime(Debuffs.MustardBomb) <= 3)
                    {
                        if (TargetHasEffect(Debuffs.Lightheaded))
                            return MustardBomb;
                        else
                            return PeripheralSynthesis;
                    }
                    if (IsSpellActive(RubyDynamics) && IsOffCooldown(RubyDynamics))
                        return RubyDynamics;
                    if (IsSpellActive(WhiteKnightsTour) && TargetHasEffect(Debuffs.Bind))
                        return WhiteKnightsTour;
                    if (IsSpellActive(BlackKnightsTour) && TargetHasEffect(Debuffs.Slow))
                        return BlackKnightsTour;
                    if (IsSpellActive(ChocoMeteor))
                        return ChocoMeteor;
                    if (IsSpellActive(PeripheralSynthesis) && !WasLastSpell(PeripheralSynthesis) && !TargetHasEffect(Debuffs.Lightheaded))
                        return PeripheralSynthesis;
                    if (IsSpellActive(RamsVoice) && !IsSpellActive(WhiteKnightsTour))
                        return RamsVoice;
                }
                return actionID;
            }
        }

        internal class BLU_PerpetualRayStunCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_PerpetualRayStunCombo;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                return (actionID is PerpetualRay && (TargetHasEffectAny(Debuffs.Stun) || WasLastAction(PerpetualRay)) && IsSpellActive(SharpenedKnife) && InMeleeRange()) ? SharpenedKnife : actionID;
            }
        }

        internal class BLU_Bristle_DoT : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_Bristle_DoT;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Bristle)
                {
                    if (HasEffect(Buffs.Bristle))
                    {
                        if (IsSpellActive(BreathofMagic) && !TargetHasEffectAny(Debuffs.BreathofMagic))
                            return BreathofMagic;
                        if (IsSpellActive(MortalFlame))
                            return MortalFlame;
                    }
                }
                return actionID;
            }
        }

        internal class BLU_Rehydration : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_Rehydration;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                return (actionID is Rehydration && ActionReady(All.Swiftcast)) ? All.Swiftcast : actionID;
            }
        }

        internal class BLU_AngelWhisper : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_AngelWhisper;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                return (actionID is AngelWhisper && ActionReady(All.Swiftcast) ? All.Swiftcast : actionID);
            }
        }

        internal class BLU_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BLU_DPS;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is WaterCannon)
                {
                    var TargetDistance = GetTargetDistance();
                    //int PartyMemberLength = GetPartyMembers().Length;
                    var TargetHp = EnemyHealthCurrentHp();
                    //float NightBloomCD = GetCooldownRemainingTime(Nightbloom);
                    var GCDTotalCD = 2.4;// LocalPlayer.TotalCastTime;
                    var HasMoonFluteEffect = HasEffect(Buffs.MoonFlute) || WasLastSpell(MoonFlute);
                    var ShockStrikeCD = GetCooldownRemainingTime(ShockStrike);
                    var MoonFluteRemainingTime = GetBuffRemainingTime(Buffs.MoonFlute);
                    var HasBristleOrWhistle = InCombat() && HasBattleTarget() ? (WasLastSpell(Whistle) || WasLastSpell(Bristle) || HasEffect(Buffs.Whistle) || HasEffect(Buffs.Bristle)) : (HasEffect(Buffs.Whistle) || HasEffect(Buffs.Bristle));

                    //斗争本能
                    if (IsSpellActive(BasicInstinct) && IsEnabled(CustomComboPreset.BLU_DPS_BasicInstinct) && HasCondition(ConditionFlag.BoundByDuty) && !HasEffect(Buffs.BasicInstinct) && (GetPartyMembers().Length == 0 || (GetPartySlot(2).IsDead && GetPartySlot(3).IsDead && GetPartySlot(4).IsDead)))
                        return BasicInstinct;

                    //终极针
                    if (IsSpellActive(FinalSting) && IsEnabled(CustomComboPreset.BLU_DPS_FinalSting) && GetTargetHPPercent() <= PluginConfiguration.GetCustomIntValue(Config.BLU_DPS_FinalSting))
                    {
                        if (IsSpellActive(Whistle) && !HasEffect(Buffs.Whistle) && !WasLastSpell(Whistle))
                            return Whistle;
                        if (IsSpellActive(MoonFlute) && !HasMoonFluteEffect)
                            return MoonFlute;
                        if (CanSpellWeave(actionID) && ActionReady(All.Swiftcast) && (WasLastSpell(MoonFlute) || HasMoonFluteEffect) && (WasLastSpell(Whistle) || HasEffect(Buffs.Whistle)))
                            return All.Swiftcast;
                        return FinalSting;
                    }

                    if (ActionReady(All.LucidDreaming) && InCombat() && LocalPlayer.CurrentMp <= 5000)
                        return All.LucidDreaming;

                    if (ActionReady(All.Swiftcast) && HasBattleTarget() && CanDelayedWeave(actionID, 2, 1.25))
                    {
                        if (IsEnabled(CustomComboPreset.BLU_DPS_Diamondback) && IsSpellActive(Diamondback) && LocalPlayer.CurrentMp >= 3000)
                            return All.Swiftcast;
                        if (IsEnabled(CustomComboPreset.BLU_DPS_DragonForce) && IsSpellActive(DragonForce) && GetCooldownRemainingTime(DragonForce) <= GCDTotalCD)
                            return All.Swiftcast;
                        if (IsEnabled(CustomComboPreset.BLU_DPS_ChelonianGate) && IsSpellActive(ChelonianGate) && GetCooldownRemainingTime(ChelonianGate) <= GCDTotalCD)
                            return All.Swiftcast;
                    }

                    if (IsSpellActive(Diamondback) && IsEnabled(CustomComboPreset.BLU_DPS_Diamondback) && LocalPlayer.CurrentMp >= 3000)
                        return Diamondback;
                    if (IsSpellActive(DragonForce) && IsEnabled(CustomComboPreset.BLU_DPS_DragonForce))
                        return DragonForce;
                    if (IsSpellActive(ChelonianGate) && IsEnabled(CustomComboPreset.BLU_DPS_ChelonianGate))
                        return OriginalHook(ChelonianGate);

                    //玄天武水壁
                    if (HasEffect(Buffs.AuspiciousTrance) && TargetDistance <= 10)
                        return DivineCataract;
                    //穿甲散弹
                    if ((HasEffect(Buffs.SurpanakhaFury) || (WasLastAbility(Surpanakha) && GetRemainingCharges(Surpanakha) >= 2)) && GetRemainingCharges(Surpanakha) > 0 &&
                        HasBattleTarget() && TargetDistance <= 16)
                        return Surpanakha;
                    //启示录
                    if (HasEffect(Buffs.Apokalypsis) || (GetCooldownRemainingTime(Apokalypsis) >= 118.5 && IsSpellActive(Apokalypsis)))
                        return Apokalypsis;
                    //鬼宿脚状态
                    if (HasEffect(Buffs.PhantomFlurry) || GetCooldownRemainingTime(PhantomFlurry) >= 118.5)
                    {
                        if (GetCooldownRemainingTime(PhantomFlurry) <= 116 || (HasMoonFluteEffect && IsSpellActive(Apokalypsis) && GetCooldownRemainingTime(Apokalypsis) <= 10))
                            return PhantomFlurry2;
                        else
                            return PhantomFlurry;
                    }

                    if (IsSpellActive(Apokalypsis) && IsOffCooldown(Apokalypsis) && HasMoonFluteEffect && !WasLastSpell(MoonFlute) && MoonFluteRemainingTime <= 1.35)
                        return Apokalypsis;
                    if (IsSpellActive(PhantomFlurry) && IsOffCooldown(PhantomFlurry) && TargetDistance <= 8 && HasMoonFluteEffect && !WasLastSpell(MoonFlute) && MoonFluteRemainingTime <= 1)
                        return PhantomFlurry;

                    if (IsSpellActive(ShockStrike) && IsOffCooldown(ShockStrike) && (IsEnabled(CustomComboPreset.BLU_DPS_MoonFlute) || HasMoonFluteEffect) &&
                        (!IsSpellActive(MoonFlute) || HasMoonFluteEffect || (GetCooldownRemainingTime(Nightbloom) >= 30 && !IsSpellActive(BreathofMagic))) && 
                        ((WasLastSpell(MoonFlute) && CanSpellWeave(actionID, 0.01)) || CanSpellWeave(actionID, 0.3)))
                        return ShockStrike;

                    if (InCombat() && (CanSpellWeave(actionID, 0.3) || (WasLastSpell(GoblinPunch) && CanSpellWeave(actionID))))
                    {
                        if (!IsMoving && CanSpellWeave(actionID, 0.8))
                        {
                            if (IsSpellActive(PeculiarLight) && IsOffCooldown(PeculiarLight) && TargetDistance <= 6 && ShockStrikeCD > 2)
                                return PeculiarLight;
                            if (IsSpellActive(Offguard) && IsOffCooldown(Offguard) && GetDebuffRemainingTime(Debuffs.Offguard) <= 1 + GCDTotalCD)
                                return Offguard;
                        }

                        //cd30s
                        if (IsSpellActive(Eruption) && IsOffCooldown(Eruption) && ShockStrikeCD >= 12 && HasBattleTarget())
                            return Eruption;
                        if (IsSpellActive(FeatherRain) && IsOffCooldown(FeatherRain) && ShockStrikeCD >= 12 && HasBattleTarget())
                            return FeatherRain;


                        if (ShockStrikeCD >= 10 || HasMoonFluteEffect)
                        {
                            if (HasMoonFluteEffect)
                            {
                                //穿甲散弹
                                if (IsSpellActive(Surpanakha) && IsEnabled(CustomComboPreset.BLU_DPS_Surpanakha) && TargetDistance <= 16 &&
                                    GetRemainingCharges(Surpanakha) >= 3 && GetCooldownChargeRemainingTime(Surpanakha) <= 2 + GCDTotalCD)
                                {
                                    if (IsOffCooldown(All.Swiftcast) && !HasEffect(Buffs.TankMimicry) && (!WasLastSpell(GoblinPunch) || !WasLastSpell(MatraMagic)) && MoonFluteRemainingTime >= 3.33)
                                        return All.Swiftcast;
                                    if ((LocalPlayer.CurrentCastTime <= 0.01f || (IsOnCooldown(All.Swiftcast) && GetCooldownRemainingTime(All.Swiftcast) < 89 && LocalPlayer.CurrentCastTime <= 1.0))
                                        && CanSpellWeave(actionID, 1.2) && GetCooldownChargeRemainingTime(Surpanakha) <= 2 && (MoonFluteRemainingTime >= 2 || !IsSpellActive(MoonFlute)))
                                        return Surpanakha;
                                }
                                //cd120s
                                if (IsSpellActive(Nightbloom) && IsOffCooldown(Nightbloom) && TargetDistance <= 10)
                                    return Nightbloom;
                                if (IsSpellActive(BothEnds) && IsOffCooldown(BothEnds))
                                    return BothEnds;

                                if (IsSpellActive(BeingMortal) && IsOffCooldown(BeingMortal) && TargetDistance <= 10)
                                    return BeingMortal;
                                if (IsSpellActive(SeaShanty) && IsOffCooldown(SeaShanty) && TargetDistance <= 10)
                                    return SeaShanty;
                            }

                            //60s
                            if (IsSpellActive(JKick) && IsEnabled(CustomComboPreset.BLU_DPS_Jkick) && IsOffCooldown(JKick) && TargetDistance <= 1
                                && (!WasLastSpell(GoblinPunch) || CanWeave(actionID, 1.3)))
                                return JKick;
                            if (IsSpellActive(Quasar) && IsOffCooldown(Quasar))
                                return Quasar;

                            //90s
                            if (IsSpellActive(GlassDance) && IsOffCooldown(GlassDance) && HasBattleTarget())
                                return GlassDance;
                            if (IsSpellActive(VeiloftheWhorl) && IsOffCooldown(VeiloftheWhorl))
                                return VeiloftheWhorl;

                            if (IsSpellActive(PhantomFlurry) && IsOffCooldown(PhantomFlurry) && TargetDistance <= 8 && HasMoonFluteEffect && !WasLastSpell(MoonFlute) && CanSpellWeave(actionID, 1.3) && 
                                MoonFluteRemainingTime >= 2.5  && IsSpellActive(Apokalypsis) && GetCooldownRemainingTime(Apokalypsis) <= 2.5)
                                return PhantomFlurry;
                        }
                        if (ActionReady(All.LucidDreaming) && !HasMoonFluteEffect && LocalPlayer.CurrentMp <= 8500 && CanSpellWeave(actionID))
                            return All.LucidDreaming;
                    }

                    //gcd杂项
                    if (IsSpellActive(MightyGuard) && !HasEffect(Buffs.MightyGuard) && HasEffect(Buffs.BasicInstinct))
                        return MightyGuard;
                    if (IsSpellActive(FlyingSardine) && CanInterruptEnemy())
                        return FlyingSardine;
                    if (IsSpellActive(WhiteWind) && IsEnabled(CustomComboPreset.BLU_DPS_WhiteWind) && LocalPlayer.CurrentMp >= 1500 && !HasMoonFluteEffect &&
                        PlayerHealthPercentageHp() <= PluginConfiguration.GetCustomIntValue(Config.BLU_DPS_WhiteWind))
                        return WhiteWind;
                    if (IsSpellActive(ToadOil) && ((InCombat() && !HasBattleTarget() && GetBuffRemainingTime(Buffs.ToadOil) <= 120) || !HasEffect(Buffs.ToadOil)) && !IsMoving)
                        return ToadOil;
                    if (IsSpellActive(BadBreath) && IsEnabled(CustomComboPreset.BLU_DPS_BadBreath) && !TargetHasEffectAny(Debuffs.Malodorous) && !IsMoving)
                        return BadBreath;
                    if (IsSpellActive(MagicHammer) && IsEnabled(CustomComboPreset.BLU_DPS_MagicHammer) && IsOffCooldown(MagicHammer) && LocalPlayer.CurrentMp <= 8500 && !IsMoving)
                        return MagicHammer;
                    if (IsSpellActive(CandyCane) && IsEnabled(CustomComboPreset.BLU_DPS_MagicHammer) && IsOffCooldown(CandyCane) && LocalPlayer.CurrentMp <= 8500 && !IsMoving)
                        return CandyCane;
                    if (IsSpellActive(Devour) && InCombat() && HasEffect(Buffs.TankMimicry) && TargetDistance <= 3 && !IsMoving &&
                        GetBuffRemainingTime(Buffs.DevourHP) <= GCDTotalCD * 2 && !HasMoonFluteEffect)
                        return Devour;
                    if (IsEnabled(CustomComboPreset.BLU_DPS_BloodDrain))
                    {
                        if (IsSpellActive(DivinationRune) && LocalPlayer.CurrentMp <= 5000)
                            return DivinationRune;
                        if (IsSpellActive(BloodDrain) && (LocalPlayer.CurrentMp <= 5000))
                            return BloodDrain;
                        if (IsSpellActive(BloodDrain) && InCombat() && !IsSpellActive(SonicBoom) && TargetDistance > 4.5)
                            return BloodDrain;
                    }

                    if (IsSpellActive(ColdFog) && IsEnabled(CustomComboPreset.BLU_DPS_ColdFog) && IsOffCooldown(ColdFog))
                        return ColdFog;

                    //gcd伤害类
                    //开怪前或者无目标
                    if (!InCombat())// || (InCombat() && !HasBattleTarget()))
                    {
                        if (!HasBristleOrWhistle)
                        {
                            if (IsSpellActive(Bristle) && IsSpellActive(MortalFlame))
                                return Bristle;
                            if (IsSpellActive(Whistle))
                                return Whistle;
                        }
                        if (IsSpellActive(Tingle) && !HasEffect(Buffs.Tingle) && HasEffect(Buffs.Whistle))
                            return Tingle;
                    }
                    if (InCombat() && !HasBattleTarget())
                    {
                        if (IsSpellActive(Whistle) && !HasBristleOrWhistle)
                            return Whistle;
                    }

                    //起手
                    if (IsSpellActive(CondensedLibra) && !IsMoving && !TargetHasEffectAny(Debuffs.AstralAttenuation) && !TargetHasEffectAny(Debuffs.UmbralAttenuation) && !TargetHasEffectAny(Debuffs.PhysicalAttenuation))
                        return CondensedLibra;

                    if (IsSpellActive(RevengeBlast) && LocalPlayer.CurrentHp / LocalPlayer.MaxHp <= 0.2 && TargetDistance <= 3)
                        return RevengeBlast;

                    if (IsSpellActive(RoseOfDestruction) && IsOffCooldown(RoseOfDestruction) && !HasMoonFluteEffect)
                        return RoseOfDestruction;

                    if (IsSpellActive(MoonFlute) && IsEnabled(CustomComboPreset.BLU_DPS_MoonFlute) && (IsSpellActive(BreathofMagic) || GetCooldownRemainingTime(Nightbloom) <= 12))
                    {
                        if ((IsSpellActive(WingedReprobation) && IsOffCooldown(WingedReprobation)) || (IsSpellActive(TripleTrident) && IsOffCooldown(TripleTrident)))
                        {
                            if (IsSpellActive(Whistle) && !HasBristleOrWhistle && ShockStrikeCD <= 2.1 + GCDTotalCD * 2)
                                return Whistle;
                            if (IsSpellActive(Tingle) && (HasEffect(Buffs.Whistle) || WasLastSpell(Whistle)) && !(HasEffect(Buffs.Tingle) || WasLastSpell(Tingle)) && ShockStrikeCD <= 2.1 + GCDTotalCD * 1)
                                return Tingle;
                        }
                        if (!HasBristleOrWhistle && ShockStrikeCD <= 2.1 + GCDTotalCD * 1)
                        {
                            if (IsSpellActive(Whistle) && IsSpellActive(GoblinPunch) && !IsSpellActive(MortalFlame))
                                return Whistle;
                            if (IsSpellActive(Bristle))
                                return Bristle;
                        }
                        if (HasBristleOrWhistle && !HasMoonFluteEffect && ShockStrikeCD <= 2.1)
                            return MoonFlute;
                    }

                    //月笛爆发期
                    if (HasMoonFluteEffect)
                    {
                        if (IsEnabled(CustomComboPreset.BLU_DPS_DoT))
                        {
                            if (IsSpellActive(BreathofMagic) && (FindTargetEffectAny(Debuffs.BreathofMagic)?.RemainingTime ?? 0) <= 3 + GCDTotalCD)
                            {
                                if (!HasBristleOrWhistle)
                                    return Bristle;
                                if (HasEffect(Buffs.Bristle) || WasLastSpell(Bristle))
                                    return BreathofMagic;
                            }
                            if (IsSpellActive(MortalFlame) && !TargetHasEffectAny(Debuffs.MortalFlame) && CombatEngageDuration().TotalSeconds <= 25)
                            {
                                if (!HasBristleOrWhistle)
                                    return Bristle;
                                if (HasEffect(Buffs.Bristle) || WasLastSpell(Bristle))
                                    return MortalFlame;
                            }
                        }

                        if (IsSpellActive(TripleTrident) && IsEnabled(CustomComboPreset.BLU_DPS_TripleTrident) &&
                            TargetDistance <= 3 && IsOffCooldown(TripleTrident) && (HasEffect(Buffs.Whistle) || WasLastSpell(Whistle)) && (HasEffect(Buffs.Tingle) || WasLastSpell(Tingle)))
                            return TripleTrident;
                        if (IsSpellActive(MatraMagic) && HasEffect(Buffs.DPSMimicry))
                        {
                            if (IsOffCooldown(MatraMagic) && ((HasMoonFluteEffect && MoonFluteRemainingTime <= 3.5) || (HasEffect(Buffs.Bristle) || WasLastSpell(Bristle))))
                                return MatraMagic;
                            if (IsSpellActive(Bristle) && !HasBristleOrWhistle && GetCooldownRemainingTime(MatraMagic) <= GCDTotalCD && MoonFluteRemainingTime >= 5.1)
                                return Bristle;
                        }
                        if (IsSpellActive(ConvictionMarcato) && HasEffect(Buffs.ConvictionMarcato) && !WasLastSpell(ConvictionMarcato))
                            return ConvictionMarcato;
                        if (IsSpellActive(RoseOfDestruction) && IsOffCooldown(RoseOfDestruction))
                            return RoseOfDestruction;
                        if (IsSpellActive(WingedReprobation) && IsOffCooldown(WingedReprobation) && GetBuffStacks(Buffs.WingedReprobation) >= 3)
                            return WingedReprobation;
                        if (IsSpellActive(SongOfTorment) && IsEnabled(CustomComboPreset.BLU_DPS_DoT))
                        {
                            if (GetDebuffRemainingTime(Debuffs.Bleeding) <= 3 + GCDTotalCD)
                                return SongOfTorment;
                            if (GetDebuffRemainingTime(Debuffs.Bleeding) <= 24)
                            {
                                if (!HasBristleOrWhistle && MoonFluteRemainingTime >= 3 + GCDTotalCD)
                                    return Bristle;
                                if (MoonFluteRemainingTime >= 3)
                                    return SongOfTorment;
                            }
                        }
                        if (IsSpellActive(WingedReprobation) && IsOffCooldown(WingedReprobation))
                            return WingedReprobation;
                    }

                    //非爆发期
                    if (IsSpellActive(WingedReprobation) && IsOffCooldown(WingedReprobation) && !IsSpellActive(BreathofMagic))
                    {
                        var NightBloomCD = GetCooldownRemainingTime(Nightbloom);
                        if (NightBloomCD <= 95 && NightBloomCD >= 77)
                            return WingedReprobation;
                    }

                    if (IsSpellActive(RoseOfDestruction) && IsOffCooldown(RoseOfDestruction))
                        return RoseOfDestruction;
                    //鱼叉三段
                    if (IsSpellActive(TripleTrident) && IsEnabled(CustomComboPreset.BLU_DPS_TripleTrident) && GetCooldownRemainingTime(TripleTrident) <= 2 * GCDTotalCD &&
                        (ShockStrikeCD >= 10 || !IsEnabled(CustomComboPreset.BLU_DPS_MoonFlute)))
                    {
                        if (IsOffCooldown(TripleTrident) && TargetDistance <= 3 &&
                            (HasEffect(Buffs.Whistle) || WasLastSpell(Whistle)) && (HasEffect(Buffs.Tingle) || WasLastSpell(Tingle)))
                            return TripleTrident;
                        if (!HasBristleOrWhistle && !HasMoonFluteEffect && GetCooldownRemainingTime(TripleTrident) <= 2 * GCDTotalCD)
                            return Whistle;
                        if (!(HasEffect(Buffs.Tingle) || WasLastSpell(Tingle)) && !HasMoonFluteEffect && GetCooldownRemainingTime(TripleTrident) <= GCDTotalCD)
                            return Tingle;
                    }

                    if (IsSpellActive(SongOfTorment) && IsEnabled(CustomComboPreset.BLU_DPS_DoT) && ShockStrikeCD >= 18)
                    {
                        if (!HasBristleOrWhistle && GetDebuffRemainingTime(Debuffs.Bleeding) <= 3 + GCDTotalCD)
                            return Bristle;
                        if ((HasEffect(Buffs.Bristle) || WasLastSpell(Bristle)) && GetDebuffRemainingTime(Debuffs.Bleeding) <= 3)
                            return SongOfTorment;
                    }

                    if (HasEffect(Buffs.TouchofFrost))
                        return OriginalHook(ColdFog);

                    if (IsSpellActive(ConvictionMarcato) && HasEffect(Buffs.ConvictionMarcato) && !WasLastSpell(ConvictionMarcato))
                        return ConvictionMarcato;

                    if (IsSpellActive(WingedReprobation) && !HasMoonFluteEffect && GetCooldownRemainingTime(WingedReprobation) <= 2 * GCDTotalCD && GetBuffStacks(Buffs.WingedReprobation) <= 2)
                    {
                        if (IsOffCooldown(WingedReprobation) && (ShockStrikeCD > 2.3 + GCDTotalCD * 1 || !IsEnabled(CustomComboPreset.BLU_DPS_MoonFlute)) &&
                            (HasEffect(Buffs.Whistle) || WasLastSpell(Whistle)) && (HasEffect(Buffs.Tingle) || WasLastSpell(Tingle)))
                            return WingedReprobation;
                        if (!HasBristleOrWhistle && GetCooldownRemainingTime(WingedReprobation) <= 2 * GCDTotalCD)
                            return Whistle;
                        if (!(HasEffect(Buffs.Tingle) || WasLastSpell(Tingle)) && GetCooldownRemainingTime(WingedReprobation) <= GCDTotalCD)
                            return Tingle;
                    }

                    //替换音爆类
                    if (IsSpellActive(SharpenedKnife) && TargetHasEffectAny(Debuffs.Stun))
                        return SharpenedKnife;
                    /*
                    if (IsSpellActive(PerpetualRay) && !TargetHasEffectAny(Debuffs.Stun))
                        return PerpetualRay;
                    */

                    if (IsSpellActive(ChocoMeteor) && !HasMoonFluteEffect)
                        return ChocoMeteor;

                    if (TargetDistance <= 4)
                    {
                        if (IsSpellActive(GoblinPunch))
                            return GoblinPunch;
                        if (IsSpellActive(SharpenedKnife))
                            return SharpenedKnife;
                    }

                    if (IsSpellActive(SonicBoom))
                        return SonicBoom;
                    if (IsSpellActive(ConvictionMarcato))
                        return ConvictionMarcato;
                    if (IsSpellActive(RamsVoice))
                        return RamsVoice;
                }
                return actionID;
            }
        }
    }
}