using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using System.Collections.Generic;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class SCH
    {
        public const byte ClassID = 26;
        public const byte JobID = 28;

        internal const uint

            // Heals
            Physick = 190,
            Adloquium = 185,
            Succor = 186,
            Lustrate = 189,
            SacredSoil = 188,
            Indomitability = 3583,
            Excogitation = 7434,
            Consolation = 16546,
            Resurrection = 173,

            // Offense
            Bio = 17864,
            Bio2 = 17865,
            Biolysis = 16540,
            Ruin = 17869,
            Ruin2 = 17870,
            Broil = 3584,
            Broil2 = 7435,
            Broil3 = 16541,
            Broil4 = 25865,
            EnergyDrain = 167,
            ArtOfWar = 16539,
            ArtOfWarII = 25866,

            // Faerie
            SummonSeraph = 16545,
            SummonEos = 17215,
            WhisperingDawn = 16537,
            FeyIllumination = 16538,
            Dissipation = 3587,
            Aetherpact = 7437,
            FeyBlessing = 16543,

            // Other
            Protraction = 25867,
            EmergencyTactics = 3586,
            Aetherflow = 166,
            Recitation = 16542,
            ChainStratagem = 7436,
            DeploymentTactics = 3585;

        //Action Groups
        internal static readonly List<uint>
            BroilList = [Ruin, Broil, Broil2, Broil3, Broil4],
            AetherflowList = [EnergyDrain, Lustrate, SacredSoil, Indomitability, Excogitation],
            FairyList = [WhisperingDawn, FeyBlessing, FeyIllumination, Dissipation, Aetherpact];

        internal static class Buffs
        {
            internal const ushort
                Galvanize = 297,
                Galvanize2 = 1331,
                Recitation = 1896;
        }

        internal static class Debuffs
        {
            internal const ushort
                Bio1 = 179,
                Bio2 = 189,
                Biolysis = 1895,
                ChainStratagem = 1221;
        }

        //Debuff Pairs of Actions and Debuff
        internal static readonly Dictionary<uint, ushort>
            BioList = new() {
                { Bio, Debuffs.Bio1 },
                { Bio2, Debuffs.Bio2 },
                { Biolysis, Debuffs.Biolysis }
            };

        // Class Gauge

        private static SCHGauge Gauge => CustomComboFunctions.GetJobGauge<SCHGauge>();

        private static bool HasAetherflow(this SCHGauge gauge) => (gauge.Aetherflow > 0);

        internal enum OpenerState
        {
            PreOpener,
            InOpener,
            PostOpener,
        }

        public static class Config
        {
            #region DPS
            public static UserInt
                SCH_ST_DPS_AltMode = new("SCH_ST_DPS_AltMode"),
                SCH_ST_DPS_LucidOption = new("SCH_ST_DPS_LucidOption"),
                SCH_ST_DPS_BioOption = new("SCH_ST_DPS_BioOption");

            public static UserBool
                SCH_ST_DPS_Adv = new("SCH_ST_DPS_Adv"),
                SCH_ST_DPS_Bio_Adv = new("SCH_ST_DPS_Bio_Adv"),
                SCH_ST_DPS_EnergyDrain_Adv = new("SCH_ST_DPS_EnergyDrain_Adv");
            public static UserFloat
                SCH_ST_DPS_Bio_Threshold = new("SCH_ST_DPS_Bio_Threshold"),
                SCH_ST_DPS_EnergyDrain = new("SCH_ST_DPS_EnergyDrain");
            public static UserBoolArray
                SCH_ST_DPS_Adv_Actions = new("SCH_ST_DPS_Adv_Actions");
            #endregion

            #region Healing
            public static UserInt
                SCH_ST_Heal_EsunaOption = new("SCH_ST_Heal_EsunaOption");
            public static UserBool
                SCH_ST_Heal_Adv = new("SCH_ST_Heal_Adv"),
                SCH_ST_Heal_UIMouseOver = new("SCH_ST_Heal_UIMouseOver");
            #endregion
        }

        internal class SCH_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_DPS;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                bool ActionFound;

                if (Config.SCH_ST_DPS_Adv && Config.SCH_ST_DPS_Adv_Actions.Count > 0)
                {
                    bool onBroils = Config.SCH_ST_DPS_Adv_Actions[0] && BroilList.Contains(actionID);
                    bool onBios = Config.SCH_ST_DPS_Adv_Actions[1] && BioList.ContainsKey(actionID);
                    bool onRuinII = Config.SCH_ST_DPS_Adv_Actions[2] && actionID is Ruin2;
                    ActionFound = onBroils || onBios || onRuinII;
                }
                else ActionFound = BroilList.Contains(actionID); //default handling

                if (ActionFound)
                {
                    uint dot = OriginalHook(Bio); //Grab the appropriate DoT Action
                    Status? dotDebuff = FindTargetEffect(BioList[dot]); //Match it with it's Debuff ID, and check for the Debuff
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                    if (!HasPetPresent() && GetCooldownRemainingTime(Dissipation) <= 149)
                        return SummonEos;

                    if (InCombat() && HasBattleTarget())
                    {

                        // Aetherflow
                        if (ActionReady(Aetherflow) && !Gauge.HasAetherflow() && (CanSpellWeave(actionID) || LocalPlayer.CurrentMp <= 2400))
                            return Aetherflow;

                        if (CanSpellWeave(actionID))
                        {
                            if (IsEnabled(CustomComboPreset.SCH_DPS_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart))
                                return Variant.VariantRampart;

                            // Dissipation
                            if (IsEnabled(CustomComboPreset.SCH_DPS_Dissipation) && ActionReady(Dissipation) && !Gauge.HasAetherflow() && HasPetPresent() && CanSpellWeave(actionID))
                                return Dissipation;

                            if (IsEnabled(CustomComboPreset.SCH_DPS_ChainStrat) && ActionReady(ChainStratagem) && !TargetHasEffectAny(Debuffs.ChainStratagem) && CanSpellWeave(actionID))
                                return ChainStratagem;

                            // Energy Drain
                            if (IsEnabled(CustomComboPreset.SCH_DPS_EnergyDrain) && ActionReady(EnergyDrain) && Gauge.HasAetherflow() && CanSpellWeave(actionID))
                            {
                                //等于防溢出，但似乎只能检测有没有豆子，不知道数量
                                //float edTime = Config.SCH_ST_DPS_EnergyDrain_Adv ? Config.SCH_ST_DPS_EnergyDrain : 10f;
                                if (GetCooldownRemainingTime(Aetherflow) <= 4.8)
                                    return EnergyDrain;
                                /*
                                //转化cd快好就泻豆子
                                if (level >= 60 && GetCooldownRemainingTime(Dissipation) <= 10 && (GetCooldownRemainingTime(ChainStratagem) >= 20 || level < 66) && GetCooldownRemainingTime(ChainStratagem) >= 40)
                                    return EnergyDrain;

                                if (IsEnabled(CustomComboPreset.SCH_DPS_EnergyDrain_BurstSaver) && (GetCooldownRemainingTime(ChainStratagem) >= 100 || level < 66) && Gauge.Aetherflow >= Config.SCH_ST_DPS_AetherFlow + 1)
                                    return EnergyDrain;
                                */
                            }

                            // Lucid Dreaming
                            if (IsEnabled(CustomComboPreset.SCH_DPS_Lucid) && ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= Config.SCH_ST_DPS_LucidOption && CanSpellWeave(actionID))
                                return All.LucidDreaming;
                        }

                        if (IsEnabled(CustomComboPreset.SCH_DPS_Variant_SpiritDart) && IsEnabled(Variant.VariantSpiritDart))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if ((sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) && CanSpellWeave(actionID))
                                return Variant.VariantSpiritDart;
                        }
                        //Bio/Biolysis
                        if (IsEnabled(CustomComboPreset.SCH_DPS_Bio) && LevelChecked(Bio))
                        {
                            float refreshtimer = Config.SCH_ST_DPS_Bio_Adv ? Config.SCH_ST_DPS_Bio_Threshold : 3;
                            if (!AllAttack && (dotDebuff is null || dotDebuff?.RemainingTime <= refreshtimer) &&
                                GetTargetHPPercent() > Config.SCH_ST_DPS_BioOption && (level < 90 || EnemyHealthCurrentHp() >= 45000))
                                return dot; //Use appropriate DoT Action
                        }
                        //Ruin 2 Movement 
                        if (IsOffCooldown(Ruin) && IsMoving)
                        {
                            if (IsEnabled(CustomComboPreset.SCH_DPS_Bio) && LevelChecked(Bio))
                            {
                                if (!AllAttack && dotDebuff.RemainingTime <= 7.5 &&
                                    GetTargetHPPercent() > Config.SCH_ST_DPS_BioOption && (level < 90 || EnemyHealthCurrentHp() >= 45000))
                                    return dot; //Use appropriate DoT Action
                            }
                            if (LevelChecked(Ruin2))
                                return OriginalHook(Ruin2);
                        }
                    }
                }
                return actionID;
            }
        }

        /*
        * SCH_AoE
        * Overrides main AoE DPS ability, Art of War
        * Lucid Dreaming and Aetherflow weave options
       */
        internal class SCH_AoE : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_AoE;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is ArtOfWar or ArtOfWarII)
                {
                    if (!HasPetPresent() && GetCooldownRemainingTime(Dissipation) <= 149)
                        return SummonEos;

                    if (IsEnabled(CustomComboPreset.SCH_DPS_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart) &&
                        CanSpellWeave(actionID))
                        return Variant.VariantRampart;

                    Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                    if (IsEnabled(CustomComboPreset.SCH_DPS_Variant_SpiritDart) &&
                        IsEnabled(Variant.VariantSpiritDart) &&
                        (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) &&
                        HasBattleTarget() &&
                        CanSpellWeave(actionID))
                        return Variant.VariantSpiritDart;

                    // Aetherflow
                    if (ActionReady(Aetherflow) && !Gauge.HasAetherflow() && CanSpellWeave(actionID))
                        return Aetherflow;

                    // Lucid Dreaming
                    if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 4000 && CanSpellWeave(actionID))
                        return All.LucidDreaming;
                }
                return actionID;
            }
        }

        internal class SCH_Indomitability : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_Indomitability;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Indomitability)
                {
                    //连点保护
                    if (GetCooldownRemainingTime(WhisperingDawn) >= 59)
                        return WhisperingDawn;
                    if (GetCooldownRemainingTime(FeyBlessing) >= 59)
                        return FeyBlessing;

                    //有秘策时候优先级提高
                    if (ActionReady(Indomitability) && Gauge.HasAetherflow() && HasEffect(Buffs.Recitation))
                        return Indomitability;

                    if (ActionReady(WhisperingDawn) && HasPetPresent())
                        return WhisperingDawn;
                    if (ActionReady(FeyBlessing) && HasPetPresent())
                        return FeyBlessing;
                }
                return actionID;
            }
        }

        internal class SCH_ST_Heal : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_ST_Heal;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Physick)
                {
                    //Grab our target (Soft->Hard->Self)
                    GameObject? healTarget = GetHealTarget(Config.SCH_ST_Heal_Adv && Config.SCH_ST_Heal_UIMouseOver);
                    if (PlayerHealthPercentageHp() <= 30 && LocalPlayer.CurrentMp <= 2800 && (GetDebuffRemainingTime(43) >= 60 || GetDebuffRemainingTime(44) >= 60))
                    {
                        if (ActionReady(Lustrate) && Gauge.HasAetherflow())
                            return Lustrate;
                        if (ActionReady(Aetherflow) && !Gauge.HasAetherflow())
                            return Aetherflow;
                        if (ActionReady(Dissipation) && !Gauge.HasAetherflow() && HasPetPresent())
                            return Dissipation;
                        if (ActionReady(All.LucidDreaming))
                            return All.LucidDreaming;
                        if (!HasPetPresent() && GetCooldownRemainingTime(Dissipation) <= 149)
                            return SummonEos;
                    }
                    if (IsEnabled(CustomComboPreset.SCH_ST_Heal_Esuna) && ActionReady(All.Esuna) &&
                        GetTargetHPPercent(healTarget) >= Config.SCH_ST_Heal_EsunaOption && HasCleansableDebuff(healTarget))
                        return All.Esuna;

                    if (LevelChecked(Adloquium) && LocalPlayer.CurrentMp >= 1000 && !HasBattleTarget() && !IsMoving)
                        return Adloquium;
                    if (ActionReady(Aetherflow) && !Gauge.HasAetherflow() && InCombat())
                        return Aetherflow;

                    if (ActionReady(Lustrate) && Gauge.HasAetherflow())
                        return Lustrate;
                    if (LevelChecked(Adloquium) && LocalPlayer.CurrentMp >= 1000)
                        return Adloquium;
                }
                return actionID;
            }
        }
        internal class SCH_AoE_Heal : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_AoE_Heal;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Succor)
                {
                    if (HasPetPresent() && ActionReady(FeyIllumination) && (HasBattleTarget() || !InCombat()))
                        return FeyIllumination;
                    if (ActionReady(DeploymentTactics))
                    {
                        if (HasEffect(Buffs.Galvanize) || HasEffect(Buffs.Galvanize2))
                            return DeploymentTactics;
                        return Adloquium;
                    }
                    if (ActionReady(Recitation) && HasBattleTarget() && IsOnCooldown(DeploymentTactics) && (!WasLastSpell(Adloquium) || GetCooldownRemainingTime(Succor) == 0))
                        return Recitation;
                }
                return actionID;
            }
        }
        internal class SCH_Excogitation_Aetherpactl : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_Excogitation_Aetherpactl;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Aetherpact)
                {
                    if (GetCooldownRemainingTime(Excogitation) >= 44)
                        return Excogitation;
                    if (ActionReady(Excogitation) && Gauge.HasAetherflow())
                        return Excogitation;
                }
                return actionID;
            }
        }

        internal class SCH_Protraction : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SCH_Protraction;
            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Protraction)
                {
                    if (GetCooldownRemainingTime(Protraction) >= 59.2)
                        return Protraction;
                    if (!ActionReady(Protraction) && ActionReady(Excogitation) && Gauge.HasAetherflow())
                        return Excogitation;
                }
                return actionID;
            }
        }
    }
}
