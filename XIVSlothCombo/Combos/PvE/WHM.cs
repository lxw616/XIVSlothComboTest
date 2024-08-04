using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.Types;
using System.Collections.Generic;
using System.Linq;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;
using Status = Dalamud.Game.ClientState.Statuses.Status;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class WHM
    {
        public const byte ClassID = 6;
        public const byte JobID = 24;

        public const uint
            // Heals
            Cure = 120,
            Medica = 124,
            Cure2 = 135,
            Cure3 = 131,
            Regen = 137,
            AfflatusSolace = 16531,
            AfflatusRapture = 16534,
            Raise = 125,
            Benediction = 140,
            AfflatusMisery = 16535,
            Medica1 = 124,
            Medica2 = 133,
            Tetragrammaton = 3570,
            DivineBenison = 7432,
            Aquaveil = 25861,
            Asylum = 3569,
            // DPS
            Glare1 = 16533,
            Glare3 = 25859,
            Stone1 = 119,
            Stone2 = 127,
            Stone3 = 3568,
            Stone4 = 7431,
            Assize = 3571,
            Holy = 139,
            Holy3 = 25860,
            // DoT
            Aero = 121,
            Aero2 = 132,
            Dia = 16532,
            // Buffs
            ThinAir = 7430,
            PresenceOfMind = 136,
            PlenaryIndulgence = 7433;

        //Action Groups
        internal static readonly List<uint>
            StoneGlareList = [Stone1, Stone2, Stone3, Stone4, Glare1, Glare3];

        public static class Buffs
        {
            public const ushort
            Regen = 158,
            Medica2 = 150,
            PresenceOfMind = 157,
            ThinAir = 1217,
            DivineBenison = 1218,
            Aquaveil = 2708;
        }

        public static class Debuffs
        {
            public const ushort
            Aero = 143,
            Aero2 = 144,
            Dia = 1871;
        }

        //Debuff Pairs of Actions and Debuff
        internal static readonly Dictionary<uint, ushort>
            AeroList = new() {
                { Aero, Debuffs.Aero },
                { Aero2, Debuffs.Aero2 },
                { Dia, Debuffs.Dia }
            };



        public static class Config
        {
            internal static UserInt
                WHM_STDPS_Lucid = new("WHMLucidDreamingFeature"),
                WHM_STDPS_MainCombo_DoT = new("WHM_ST_MainCombo_DoT"),
                WHM_STHeals_Esuna = new("WHM_Cure2_Esuna");
            internal static UserBool
                WHM_ST_MainCombo_DoT_Adv = new("WHM_ST_MainCombo_DoT_Adv"),
                WHM_ST_MainCombo_Adv = new("WHM_ST_MainCombo_Adv"),
                WHM_STHeals_UIMouseOver = new("WHM_STHeals_UIMouseOver");
            internal static UserFloat
                WHM_ST_MainCombo_DoT_Threshold = new("WHM_ST_MainCombo_DoT_Threshold");
            public static UserBoolArray
                WHM_ST_MainCombo_Adv_Actions = new("WHM_ST_MainCombo_Adv_Actions");
        }

        internal class WHM_ST_MainCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WHM_ST_MainCombo;
            internal static int Glare3Count => ActionWatching.CombatActions.Count(x => x == OriginalHook(Glare3));
            internal static int DiaCount => ActionWatching.CombatActions.Count(x => x == OriginalHook(Dia));

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                bool ActionFound;

                if (Config.WHM_ST_MainCombo_Adv && Config.WHM_ST_MainCombo_Adv_Actions.Count > 0)
                {
                    bool onStones = Config.WHM_ST_MainCombo_Adv_Actions[0] && StoneGlareList.Contains(actionID);
                    bool onAeros = Config.WHM_ST_MainCombo_Adv_Actions[1] && AeroList.ContainsKey(actionID);
                    bool onStone2 = Config.WHM_ST_MainCombo_Adv_Actions[2] && actionID is Stone2;
                    ActionFound = onStones || onAeros || onStone2;
                }
                else ActionFound = StoneGlareList.Contains(actionID); //default handling

                if (ActionFound)
                {
                    WHMGauge? gauge = GetJobGauge<WHMGauge>();
                    bool liliesFull = gauge.Lily == 3;
                    bool liliesNearlyFull = gauge.Lily == 2 && gauge.LilyTimer >= 15000;
                    bool AllAttack = IsEnabled(CustomComboPreset.ALL_AllAttack);

                    if (CanSpellWeave(actionID))
                    {
                        bool lucidReady = ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= Config.WHM_STDPS_Lucid;
                        bool assizeEnabled = IsEnabled(CustomComboPreset.WHM_ST_MainCombo_Assize);
                        bool lucidEnabled = IsEnabled(CustomComboPreset.WHM_ST_MainCombo_Lucid);

                        if (IsEnabled(CustomComboPreset.WHM_DPS_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart) && CanSpellWeave(actionID))
                            return Variant.VariantRampart;

                        if (IsEnabled(CustomComboPreset.WHM_PresenceOfMind) && ActionReady(PresenceOfMind))
                            return PresenceOfMind;
                        if (assizeEnabled && ActionReady(Assize))
                            return Assize;
                        if (lucidEnabled && lucidReady)
                            return All.LucidDreaming;
                    }

                    if (InCombat())
                    {
                        // DoTs
                        if (IsEnabled(CustomComboPreset.WHM_ST_MainCombo_DoT) && LevelChecked(Aero) && HasBattleTarget())
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.WHM_DPS_Variant_SpiritDart) &&
                                IsEnabled(Variant.VariantSpiritDart) &&
                                (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) &&
                                CanSpellWeave(actionID))
                                return Variant.VariantSpiritDart;

                            uint dot = OriginalHook(Aero); //Grab the appropriate DoT Action
                            Status? dotDebuff = FindTargetEffect(AeroList[dot]); //Match it with it's Debuff ID, and check for the Debuff

                            // DoT Uptime & HP% threshold
                            float refreshtimer = Config.WHM_ST_MainCombo_DoT_Adv ? Config.WHM_ST_MainCombo_DoT_Threshold : 3;
                            if (!AllAttack && (dotDebuff is null || dotDebuff.RemainingTime <= refreshtimer || (IsMoving && IsOffCooldown(Stone1) && dotDebuff.RemainingTime <= 8)) &&
                                GetTargetHPPercent() > Config.WHM_STDPS_MainCombo_DoT && (level < 90 || EnemyHealthCurrentHp() >= 45000))
                                return OriginalHook(Aero);
                        }

                        if (IsEnabled(CustomComboPreset.WHM_ST_MainCombo_Misery) && LevelChecked(AfflatusMisery) && gauge.BloodLily >= 3)
                        {
                            if (liliesFull || liliesNearlyFull)
                                return AfflatusMisery;
                            if (GetCooldownRemainingTime(PresenceOfMind) >= 100)
                                return AfflatusMisery;
                        }
                        if (IsEnabled(CustomComboPreset.WHM_ST_MainCombo_LilyOvercap) && LevelChecked(AfflatusRapture) &&
                            (liliesFull || liliesNearlyFull))
                            return AfflatusRapture;
                        

                        return OriginalHook(Stone1);
                    }
                }

                return actionID;
            }
        }

        internal class WHM_AoEHeals : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WHM_AoEHeals;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Medica)
                {
                    if (ActionReady(ThinAir) && InCombat() && HasBattleTarget() && (!HasEffect(Buffs.ThinAir) || !WasLastAbility(ThinAir)))
                        return ThinAir;
                    if (LevelChecked(Medica2) && LocalPlayer.CurrentMp >= 1000)
                    {
                        if (!HasEffect(Buffs.Medica2))
                            return Medica2;
                        if (HasBattleTarget() && GetBuffRemainingTime(Buffs.Medica2) <= 3)
                            return Medica2;
                        if (!HasBattleTarget() && GetBuffRemainingTime(Buffs.Medica2) <= 7)
                            return Medica2;
                    }
                    if (LevelChecked(Cure3) && LocalPlayer.CurrentMp >= 1500)
                        return Cure3;
                }
                return actionID;
            }
        }

        internal class WHM_ST_Heals : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WHM_ST_Heals;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Cure)
                {
                    WHMGauge? gauge = GetJobGauge<WHMGauge>();

                    if (ActionReady(All.LucidDreaming) && LocalPlayer.CurrentMp <= 2600)
                        return All.LucidDreaming;

                    if (LevelChecked(Cure2) && LocalPlayer.CurrentMp >= 1000 && !HasBattleTarget() && !IsMoving)
                        return Cure2;
                    if (LevelChecked(AfflatusSolace) && gauge.Lily >= 2)
                        return AfflatusSolace;
                    if (ActionReady(Tetragrammaton) && gauge.Lily <= 1)
                        return Tetragrammaton;
                    if (LevelChecked(AfflatusSolace) && gauge.Lily >= 1)
                        return AfflatusSolace;
                    if (LevelChecked(Cure2) && LocalPlayer.CurrentMp >= 1000)
                        return Cure2;
                }
                return actionID;
            }
        }

        internal class WHM_AoE_DPS : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WHM_AoE_DPS;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Holy or Holy3)
                {
                    WHMGauge? gauge = GetJobGauge<WHMGauge>();

                    bool liliesFullNoBlood = gauge.Lily == 3 && gauge.BloodLily < 3;
                    bool liliesNearlyFull = gauge.Lily == 2 && gauge.LilyTimer >= 17000;

                    if (IsEnabled(CustomComboPreset.WHM_DPS_Variant_Rampart) &&
                        IsEnabled(Variant.VariantRampart) &&
                        IsOffCooldown(Variant.VariantRampart))
                        return Variant.VariantRampart;

                    Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                    if (IsEnabled(CustomComboPreset.WHM_DPS_Variant_SpiritDart) &&
                        IsEnabled(Variant.VariantSpiritDart) &&
                        (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3) &&
                        HasBattleTarget())
                        return Variant.VariantSpiritDart;

                    if (InCombat())
                    {
                        if (IsEnabled(CustomComboPreset.WHM_AoE_DPS_Assize) && ActionReady(Assize))
                            return Assize;
                        if (IsEnabled(CustomComboPreset.WHM_PresenceOfMind) && ActionReady(PresenceOfMind))
                            return PresenceOfMind;
                    }
                    
                    if (IsEnabled(CustomComboPreset.WHM_AoE_DPS_LilyOvercap) && LevelChecked(AfflatusRapture) &&
                        (liliesFullNoBlood || liliesNearlyFull))
                        return AfflatusRapture;
                    if (IsEnabled(CustomComboPreset.WHM_AoE_DPS_Misery) && LevelChecked(AfflatusMisery) &&
                        gauge.BloodLily >= 3 && HasBattleTarget())
                        return AfflatusMisery;
                }

                return actionID;
            }
        }

        internal class WHM_Aquaveil : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WHM_Aquaveil;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Aquaveil)
                {
                    if (GetCooldownRemainingTime(Aquaveil) >= 59.2)
                        return Aquaveil;
                    if (ActionReady(DivineBenison) && !ActionReady(Aquaveil))
                        return DivineBenison;
                }
                return actionID;
            }
        }

        internal class WHM_AfflatusRapture : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WHM_AfflatusRapture;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is AfflatusRapture)
                {
                    if (IsEnabled(CustomComboPreset.WHM_AfflatusRapture_Assize))
                    {
                        if (GetCooldownRemainingTime(Assize) >= 39)
                            return Assize;
                        if (ActionReady(Assize))
                            return Assize;
                    }
                    if (ActionReady(PlenaryIndulgence))
                        return PlenaryIndulgence;
                }
                return actionID;
            }
        }
    }
}
