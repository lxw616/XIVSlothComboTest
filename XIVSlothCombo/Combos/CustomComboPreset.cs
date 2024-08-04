using XIVSlothCombo.Attributes;
using XIVSlothCombo.Combos.PvE;
using XIVSlothCombo.Combos.PvP;
using static XIVSlothCombo.Combos.PvE.AST;

namespace XIVSlothCombo.Combos
{
    /// <summary> Combo presets. </summary>
    public enum CustomComboPreset
    {
        #region PvE Combos

        #region Misc

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", ADV.JobID)]
        AdvAny = 0,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", AST.JobID)]
        AstAny = AdvAny + AST.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", BLM.JobID)]
        BlmAny = AdvAny + BLM.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", BRD.JobID)]
        BrdAny = AdvAny + BRD.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DNC.JobID)]
        DncAny = AdvAny + DNC.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DOH.JobID)]
        DohAny = AdvAny + DOH.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DOL.JobID)]
        DolAny = AdvAny + DOL.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DRG.JobID)]
        DrgAny = AdvAny + DRG.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DRK.JobID)]
        DrkAny = AdvAny + DRK.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", GNB.JobID)]
        GnbAny = AdvAny + GNB.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", MCH.JobID)]
        MchAny = AdvAny + MCH.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", MNK.JobID)]
        MnkAny = AdvAny + MNK.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", NIN.JobID)]
        NinAny = AdvAny + NIN.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", PLD.JobID)]
        PldAny = AdvAny + PLD.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", RDM.JobID)]
        RdmAny = AdvAny + RDM.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", RPR.JobID)]
        RprAny = AdvAny + RPR.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SAM.JobID)]
        SamAny = AdvAny + SAM.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SCH.JobID)]
        SchAny = AdvAny + SCH.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SGE.JobID)]
        SgeAny = AdvAny + SGE.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SMN.JobID)]
        SmnAny = AdvAny + SMN.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", WAR.JobID)]
        WarAny = AdvAny + WAR.JobID,

        [CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", WHM.JobID)]
        WhmAny = AdvAny + WHM.JobID,

        [CustomComboInfo("Disabled", "This should not be used.", ADV.JobID)]
        Disabled = 99999,

        #endregion

        #region GLOBAL FEATURES

        [ReplaceSkill(All.Sprint)]
        [CustomComboInfo("Island Sanctuary Sprint Feature", "Replaces Sprint with Isle Sprint.\nOnly works at the Island Sanctuary. Icon does not change.\nDo not use with SimpleTweaks' Island Sanctuary Sprint fix.", ADV.JobID)]
        ALL_IslandSanctuary_Sprint = 100093,

        [CustomComboInfo("泄资源", "Use all your skills/resources.", ADV.JobID)]
        ALL_AllAttack = 100100,

        #region Global Tank Features
        [CustomComboInfo("Global Tank Features", "Features and options involving shared role actions for Tanks.\nCollapsing this category does NOT disable the features inside.", ADV.JobID)]
        ALL_Tank_Menu = 100099,

        [ReplaceSkill(All.LowBlow, PLD.ShieldBash)]
        [ParentCombo(ALL_Tank_Menu)]
        [CustomComboInfo("Tank: Interrupt Feature", "Replaces Low Blow (Stun) with Interject (Interrupt) when the target can be interrupted.\nPLDs can slot Shield Bash to have the feature to work with Shield Bash.", ADV.JobID)]
        ALL_Tank_Interrupt = 100000,

        [ReplaceSkill(All.Reprisal)]
        [ParentCombo(ALL_Tank_Menu)]
        [CustomComboInfo("Tank: Double Reprisal Protection", "Prevents the use of Reprisal when target already has the effect by replacing it with Stone.", ADV.JobID)]
        ALL_Tank_Reprisal = 100001,
        #endregion

        #region Global Healer Features
        [CustomComboInfo("Global Healer Features", "Features and options involving shared role actions for Healers.\nCollapsing this category does NOT disable the features inside.", ADV.JobID)]
        ALL_Healer_Menu = 100098,

        [ReplaceSkill(AST.Ascend, WHM.Raise, SCH.Resurrection, SGE.Egeiro)]
        [ParentCombo(ALL_Healer_Menu)]
        [CustomComboInfo("Healer: Raise Feature", "Changes the class' Raise Ability into Swiftcast.", ADV.JobID)]
        ALL_Healer_Raise = 100010,
        #endregion

        #region Global Magical Ranged Features
        [CustomComboInfo("Global Magical Ranged Features", "Features and options involving shared role actions for Magical Ranged DPS.\nCollapsing this category does NOT disable the features inside.", ADV.JobID)]
        ALL_Caster_Menu = 100097,

        [ReplaceSkill(All.Addle)]
        [ParentCombo(ALL_Caster_Menu)]
        [CustomComboInfo("Magical Ranged DPS: Double Addle Protection", "Prevents the use of Addle when target already has the effect by replacing it with Fell Cleave.", ADV.JobID)]
        ALL_Caster_Addle = 100020,

        [ReplaceSkill(RDM.Verraise, SMN.Resurrection, BLU.AngelWhisper)]
        [ConflictingCombos(SMN_Raise)]
        [ParentCombo(ALL_Caster_Menu)]
        [CustomComboInfo("Magical Ranged DPS: Raise Feature", "Changes the class' Raise Ability into Swiftcast or Dualcast in the case of RDM.", ADV.JobID)]
        ALL_Caster_Raise = 100021,
        #endregion

        #region Global Melee Features
        [CustomComboInfo("Global Melee DPS Features", "Features and options involving shared role actions for Melee DPS.\nCollapsing this category does NOT disable the features inside.", ADV.JobID)]
        ALL_Melee_Menu = 100096,

        [ReplaceSkill(All.Feint)]
        [ParentCombo(ALL_Melee_Menu)]
        [CustomComboInfo("Melee DPS: Double Feint Protection", "Prevents the use of Feint when target already has the effect by replacing it with Fire.", ADV.JobID)]
        ALL_Melee_Feint = 100030,

        [ReplaceSkill(All.TrueNorth)]
        [ParentCombo(ALL_Melee_Menu)]
        [CustomComboInfo("Melee DPS: True North Protection", "Prevents the use of True North when its buff is already active by replacing it with Fire.", ADV.JobID)]
        ALL_Melee_TrueNorth = 100031,

        #endregion

        #region Global Ranged Physical Features
        [CustomComboInfo("Global Physical Ranged Features", "Features and options involving shared role actions for Physical Ranged DPS.\nCollapsing this category does NOT disable the features inside.", ADV.JobID)]
        ALL_Ranged_Menu = 100095,

        [ReplaceSkill(MCH.Tactician, BRD.Troubadour, DNC.ShieldSamba)]
        [ParentCombo(ALL_Ranged_Menu)]
        [CustomComboInfo("Physical Ranged DPS: Double Mitigation Protection", "Prevents the use of Tactician/Troubadour/Shield Samba when target already has one of those three effects.", ADV.JobID)]
        ALL_Ranged_Mitigation = 100040,

        [ReplaceSkill(All.FootGraze)]
        [ParentCombo(ALL_Ranged_Menu)]
        [CustomComboInfo("Physical Ranged DPS: Ranged Interrupt Feature", "Replaces Foot Graze with Head Graze when target can be interrupted.", ADV.JobID)]
        ALL_Ranged_Interrupt = 100041,


        #endregion

        //Non-gameplay Features
        //[CustomComboInfo("Output Combat Log", "Outputs your performed actions to the chat.", ADV.JobID)]
        //AllOutputCombatLog = 100094,

        // Last value = 100094

        #endregion

        // Jobs

        #region ASTROLOGIAN

        #region DPS
        [ReplaceSkill(AST.Malefic, AST.Malefic2, AST.Malefic3, AST.Malefic4, AST.FallMalefic, AST.Gravity, AST.Gravity2)]
        [CustomComboInfo("一键DPS", "", AST.JobID)]
        AST_ST_DPS = 1001,

        [ReplaceSkill(AST.Gravity, AST.Gravity2)]
        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("AoE DPS", "Every option below (Lucid/AutoDraws/Astrodyne/etc) will also be added to Gravity", AST.JobID, 1, "", "")]
        AST_AoE_DPS = 1002,

        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("DoT", "烧灼, Combust", AST.JobID)]
        AST_ST_DPS_CombustUptime = 1003,

        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("光速", "LightSpeed", AST.JobID)]
        AST_DPS_LightSpeed = 1004,

        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("发卡", "Card Play", AST.JobID)]
        AST_DPS_AutoPlay = 1005,

        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("占卜", "Divination", AST.JobID)]
        AST_DPS_Divination = 1006,

        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("王冠之领主", "Lord Of Crowns", AST.JobID)]
        AST_DPS_LazyLord = 1007,

        [ParentCombo(AST_ST_DPS)]
        [CustomComboInfo("醒梦", "Lucid Dreaming", AST.JobID)]
        AST_DPS_Lucid = 1008,

        #endregion

        #region Healing
        [ReplaceSkill(AST.Helios)]
        [CustomComboInfo("GCD群奶", "GCD AOE Heals", AST.JobID)]
        AST_GCD_AoE_Heals = 1100,

        [ParentCombo(AST_GCD_AoE_Heals)]
        [CustomComboInfo("中间学派", "NeutralSect, only shows when we have a target. ", AST.JobID)]
        AST_AspectedHelios_NeutralSect = 1101,

        [ReplaceSkill(AST.EarthlyStar)]
        [CustomComboInfo("oGCD群奶", "地星-王冠贵妇-天星冲日, EarthlyStar-LadyOfCrowns-CelestialOpposition", AST.JobID)]
        AST_oGCD_AoE_Heals = 1102,

        [ReplaceSkill(AST.Benefic)]
        [CustomComboInfo("GCD单奶", "天赋秉异-星位合图-福星-吉星, EssentialDignity-Synastry-Benefic2-Benefic", AST.JobID)]
        AST_ST_SimpleHeals = 1103,

        [ParentCombo(AST_ST_SimpleHeals)]
        [CustomComboInfo("康复", "Esuna", AST.JobID)]
        AST_ST_SimpleHeals_Esuna = 1104,

        [ReplaceSkill(AST.Exaltation)]
        [CustomComboInfo("对T减伤", "擢升-天星交错, Exaltation-CelestialIntersection", AST.JobID)]
        AST_Exaltation = 1105,

        #endregion

        #region Utility
        [Variant]
        [VariantParent(AST_ST_DPS_CombustUptime, AST_AoE_DPS)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", AST.JobID)]
        AST_Variant_SpiritDart = 1151,

        [Variant]
        [VariantParent(AST_ST_DPS)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", AST.JobID)]
        AST_Variant_Rampart = 1152,

        #endregion

        #region Cards
        [ReplaceSkill(AST.Play)]
        [CustomComboInfo("抽卡发卡", "Draw-Play", AST.JobID)]
        AST_Cards_DrawOnPlay = 1200,

        [CustomComboInfo("Quick Target Cards", "Grabs a suitable target from the party list when you draw a card and targets them for you.", AST.JobID)]
        AST_Cards_QuickTargetCards = 1201,

        [ParentCombo(AST_Cards_QuickTargetCards)]
        [CustomComboInfo("Add Tanks/Healers to Auto-Target", "Targets a tank or healer if no DPS remain for quick target selection", AST.JobID)]
        AST_Cards_QuickTargetCards_TargetExtra = 1202,

        [ParentCombo(AST_Cards_QuickTargetCards)]
        [CustomComboInfo("按职业优先级派卡", ",近战：盘忍僧DK，远程：青机黑召。自己保底，单刷会自己吃了。Play base on jobs. Melee: SAM-NIN-MNK-DRK, Ranged: BLU-MCH-BLM-SMN", AST.JobID)]
        AST_Cards_QuickTargetCards_BaseOnJob = 1203,

        #endregion

        // Last value = 1203

        #endregion

        #region BLACK MAGE

        [ReplaceSkill(BLM.Fire)]
        [ConflictingCombos(BLM_Scathe_Xeno, BLM_ST_AdvancedMode)]
        [CustomComboInfo("Simple Mode - Single Target", "Replaces Fire with a full one-button single target rotation.\nThis is the ideal option for newcomers to the job.", BLM.JobID, -10, "", "")]
        BLM_ST_SimpleMode = 2012,

        #region Advanced ST

        [ReplaceSkill(BLM.Fire)]
        [ConflictingCombos(BLM_Scathe_Xeno, BLM_ST_SimpleMode)]
        [CustomComboInfo("Advanced Mode - Single Target", "Replaces Fire with a full one-button single target rotation.\nThese features are ideal if you want to customize the rotation.", BLM.JobID, -9, "", "")]
        BLM_ST_AdvancedMode = 2021,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Thunder I/III Option", "Adds Thunder I/Thunder III when the debuff isn't present or is expiring.", BLM.JobID)]
        BLM_ST_Adv_Thunder = 2029,

        [ParentCombo(BLM_ST_Adv_Thunder)]
        [CustomComboInfo("Thundercloud Spender Option", "Spends Thundercloud as soon as possible rather than waiting until Thunder is expiring.", BLM.JobID)]
        BLM_ST_Adv_Thunder_ThunderCloud = 2030,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Umbral Soul Option", "Uses Transpose/Umbral Soul when no target is selected.", BLM.JobID, 10, "", "")]
        BLM_Adv_UmbralSoul = 2035,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Movement Options", "Choose options to be used during movement.", BLM.JobID)]
        BLM_Adv_Movement = 2036,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Triplecast/Swiftcast Option", "Adds Triplecast/Swiftcast to the rotation.", BLM.JobID, -8, "", "")]
        BLM_Adv_Casts = 2039,

        [ParentCombo(BLM_Adv_Casts)]
        [CustomComboInfo("Pool Triplecast Option", "Keep one Triplecast charge for movement.", BLM.JobID)]
        BLM_Adv_Triplecast_Pooling = 2040,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Cooldown Options", "Select which cooldowns to add to the rotation.", BLM.JobID, -8, "", "")]
        BLM_Adv_Cooldowns = 2042,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Opener Option", "Adds the Lv.90 opener." +
            "\nWill default to the Standard opener when nothing is selected.", BLM.JobID, -10, "", "")]
        BLM_Adv_Opener = 2043,

        [ParentCombo(BLM_ST_AdvancedMode)]
        [CustomComboInfo("Rotation Option", "Choose which rotation to use." +
            "\nWill default to the Standard rotation when nothing is selected.", BLM.JobID, -9, "", "")]
        BLM_Adv_Rotation = 2045,

        #endregion

        [ReplaceSkill(BLM.Blizzard2, BLM.HighBlizzard2)]
        [ConflictingCombos(BLM_AoE_AdvancedMode)]
        [CustomComboInfo("Simple Mode - AoE", "Replaces Blizzard II with a full one-button AoE rotation.\nThis is the ideal option for newcomers to the job.", BLM.JobID, -8, "", "")]
        BLM_AoE_SimpleMode = 2008,

        #region Advanced AoE

        [ReplaceSkill(BLM.Blizzard2, BLM.HighBlizzard2)]
        [ConflictingCombos(BLM_AoE_SimpleMode)]
        [CustomComboInfo("Advanced Mode - AoE", "Replaces Blizzard II with a full one-button AoE rotation.\nThese features are ideal if you want to customize the rotation.", BLM.JobID, -8, "", "")]
        BLM_AoE_AdvancedMode = 2054,

        [ParentCombo(BLM_AoE_AdvancedMode)]
        [CustomComboInfo("Thunder Uptime Option", "Adds Thunder II/Thunder IV during Umbral Ice.", BLM.JobID, 1, "", "")]
        BLM_AoE_Adv_ThunderUptime = 2055,

        [ParentCombo(BLM_AoE_Adv_ThunderUptime)]
        [CustomComboInfo("Uptime in Astral Fire", "Maintains uptime during Astral Fire.", BLM.JobID, 1, "", "")]
        BLM_AoE_Adv_ThunderUptime_AstralFire = 2056,

        [ParentCombo(BLM_AoE_AdvancedMode)]
        [CustomComboInfo("Foul Option", "Adds Foul when available during Astral Fire.", BLM.JobID, 2, "", "")]
        BLM_AoE_Adv_Foul = 2044,

        [ParentCombo(BLM_AoE_AdvancedMode)]
        [CustomComboInfo("Umbral Soul Option", "Use Transpose/Umbral Soul when no target is selected.", BLM.JobID, 99, "", "")]
        BLM_AoE_Adv_UmbralSoul = 2049,

        [ParentCombo(BLM_AoE_AdvancedMode)]
        [CustomComboInfo("Cooldown Options", "Select which cooldowns to add to the rotation.", BLM.JobID, 1, "", "")]
        BLM_AoE_Adv_Cooldowns = 2052,

        #endregion

        #region Variant

        [Variant]
        [VariantParent(BLM_ST_SimpleMode, BLM_ST_AdvancedMode, BLM_AoE_SimpleMode)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", BLM.JobID)]
        BLM_Variant_Rampart = 2032,

        [Variant]
        [CustomComboInfo("Raise Option", "Turn Swiftcast into Variant Raise whenever you have the Swiftcast buff.", BLM.JobID)]
        BLM_Variant_Raise = 2033,

        [Variant]
        [VariantParent(BLM_ST_SimpleMode, BLM_ST_AdvancedMode, BLM_AoE_SimpleMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", BLM.JobID)]
        BLM_Variant_Cure = 2034,

        #endregion

        #region Miscellaneous
        [ReplaceSkill(BLM.Transpose)]
        [CustomComboInfo("Umbral Soul/Transpose Feature", "Replaces Transpose with Umbral Soul when Umbral Soul is available.", BLM.JobID)]
        BLM_UmbralSoul = 2001,

        [ReplaceSkill(BLM.LeyLines)]
        [CustomComboInfo("Between the Ley Lines Feature", "Replaces Ley Lines with Between the Lines when Ley Lines is active.", BLM.JobID)]
        BLM_Between_The_LeyLines = 2002,

        [ReplaceSkill(BLM.Blizzard, BLM.Freeze)]
        [CustomComboInfo("Blizzard I/III Feature", "Replaces Blizzard I with Blizzard III when out of Umbral Ice." +
            "\nReplaces Freeze with Blizzard II when synced below Lv.40.", BLM.JobID)]
        BLM_Blizzard_1to3 = 2003,

        [ReplaceSkill(BLM.Scathe)]
        [ConflictingCombos(BLM_ST_SimpleMode, BLM_ST_AdvancedMode)]
        [CustomComboInfo("Xenoglossy Feature", "Replaces Scathe with Xenoglossy when available.", BLM.JobID)]
        BLM_Scathe_Xeno = 2004,

        [ReplaceSkill(BLM.Fire)]
        [CustomComboInfo("Fire I/III Feature", "Replaces Fire I with Fire III outside of Astral Fire or when Firestarter is up.", BLM.JobID)]
        BLM_Fire_1to3 = 2005,

        [ReplaceSkill(BLM.AetherialManipulation)]
        [CustomComboInfo("Aetherial Manipulation Feature", "Replaces Aetherial Manipulation with Between the Lines when you are out of active Ley Lines and standing still.", BLM.JobID)]
        BLM_Aetherial_Manipulation = 2046,
        #endregion

        // Last value = 2057

        #endregion

        #region BLUE MAGE

        [ReplaceSkill(BLU.WaterCannon)]
        [CustomComboInfo("一键DPS", " ", BLU.JobID)]
        BLU_DPS = 70000,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("月之笛", "MoonFlute ", BLU.JobID)]
        BLU_DPS_MoonFlute = 70001,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("终极针", "FinalSting", BLU.JobID)]
        BLU_DPS_FinalSting = 70002,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("穿甲散弹", "Surpanakha", BLU.JobID)]
        BLU_DPS_Surpanakha = 70003,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("正义飞踢", "Jkick ", BLU.JobID)]
        BLU_DPS_Jkick = 70004,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("DoT", "魔法吐息必灭之炎苦闷之歌, BreathOfMagic-MortalFlame-SongOfTorment,", BLU.JobID)]
        BLU_DPS_DoT = 70005,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("鱼叉三段", "TripleTrident", BLU.JobID)]
        BLU_DPS_TripleTrident = 70006,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("彻骨雾寒", "ColdFog ", BLU.JobID)]
        BLU_DPS_ColdFog = 70007,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("超硬化", "Diamondback ", BLU.JobID)]
        BLU_DPS_Diamondback = 70008,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("龙之力", "DragonForce ", BLU.JobID)]
        BLU_DPS_DragonForce = 70009,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("玄结界", "ChelonianGate ", BLU.JobID)]
        BLU_DPS_ChelonianGate = 70010,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("白风 ", "WhiteWind ", BLU.JobID)]
        BLU_DPS_WhiteWind = 70011,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("臭气 ", "BadBreath ", BLU.JobID)]
        BLU_DPS_BadBreath = 70012,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("魔法或糖果手杖", "MagicHammer or CandyCane", BLU.JobID)]
        BLU_DPS_MagicHammer = 70013,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("吸血或魔之符文", "BloodDrain or DivinationRune", BLU.JobID)]
        BLU_DPS_BloodDrain = 70014,

        [ParentCombo(BLU_DPS)]
        [CustomComboInfo("斗争本能 ", "BasicInstinct ", BLU.JobID)]
        BLU_DPS_BasicInstinct = 70015,

        [ReplaceSkill(BLU.WhiteKnightsTour)]
        [CustomComboInfo("AOE", "oGCD飞翎羽启示录鬼宿脚冰弓，怒发月笛魔法吐息-芥末炸弹生成外设-白骑士-黑骑士之旅", BLU.JobID)]
        BLU_AOE = 70100,

        [ReplaceSkill(BLU.TheLook)]
        [CustomComboInfo("超振动", "水力吸引-寒冰咆哮-超振动-诡异射线", BLU.JobID)]
        BLU_Ultravibration = 70200,

        [ReplaceSkill(BLU.Bristle)]
        [CustomComboInfo("怒发冲冠  Bristle-DoT", "怒发冲冠-魔力吐息-怒发冲冠-必灭之炎", BLU.JobID)]
        BLU_Bristle_DoT = 70202,

        [ReplaceSkill(BLU.Rehydration)]
        [CustomComboInfo("即刻-补水, SwiftCast-Rehydration", "", BLU.JobID)]
        BLU_Rehydration = 70203,

        [ReplaceSkill(BLU.AngelWhisper)]
        [CustomComboInfo("即刻-复活, SwiftCast-AngelWhisper", "", BLU.JobID)]
        BLU_AngelWhisper = 70204,

        [BlueInactive(BLU.PerpetualRay)]
        [CustomComboInfo("永恒射线-锋利菜刀", "PerpetualRay-SharpenedKnife", BLU.JobID)]
        BLU_PerpetualRayStunCombo = 70205,

        [BlueInactive(BLU.DeepClean)]
        [CustomComboInfo("玩泥球-大扫除", "PeatPelt-DeepClean", BLU.JobID)]
        BLU_PeatPelt_DeepClean = 70206,

        [BlueInactive(BLU.JKick)]
        [CustomComboInfo("正义飞踢-若隐若现", "JKick-Loom", BLU.JobID)]
        BLU_JKick_Loom = 70207,

        // Last value = 70207

        #endregion

        #region BARD

        [CustomComboInfo("猛者强击", "BRD_RagingStrikes", BRD.JobID)]
        BRD_RagingStrikes = 3000, 

        [CustomComboInfo("唱歌", "Songs", BRD.JobID)]
        BRD_Songs = 3001,

        [CustomComboInfo("绝峰箭", "Apex Arrow", BRD.JobID)]
        BRD_ApexArrow = 3002,

        [ReplaceSkill(BRD.HeavyShot, BRD.BurstShot)]
        [CustomComboInfo("一键DPS", "", BRD.JobID)]
        BRD_ST_SimpleMode = 3100,

        [ParentCombo(BRD_ST_SimpleMode)]
        [CustomComboInfo("DoT", "", BRD.JobID)]
        BRD_ST_DoT = 3101,

        [ParentCombo(BRD_ST_SimpleMode)]
        [CustomComboInfo("旅神歌", "不勾选有保底，无歌会用", BRD.JobID)]
        BRD_ST_WanderersMinuet = 3102,

        [ParentCombo(BRD_ST_SimpleMode)]
        [CustomComboInfo("军神歌", "不勾选有保底，贤者剩3秒切军神", BRD.JobID)]
        BRD_ST_ArmysPaeon = 3103,

        [ParentCombo(BRD_ST_SimpleMode)]
        [CustomComboInfo("失血箭", "", BRD.JobID)]
        BRD_ST_Bloodletter = 3104,

        [ParentCombo(BRD_ST_SimpleMode)]
        [CustomComboInfo("双目标", "", BRD.JobID)]
        BRD_ST_TwoTarget = 3105,

        [ReplaceSkill(BRD.QuickNock, BRD.Ladonsbite)]
        [CustomComboInfo("AoE", "", BRD.JobID)]
        BRD_AoE_SimpleMode = 3200,

        [ParentCombo(BRD_AoE_SimpleMode)]
        [CustomComboInfo("Second Wind Option", "Uses Second Wind when below set HP percentage.", BRD.JobID)]
        BRD_AoE_SecondWind = 3201,

        [Variant]
        [VariantParent(BRD_ST_SimpleMode, BRD_AoE_SimpleMode)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", BRD.JobID)]
        BRD_Variant_Rampart = 3300,

        [Variant]
        [VariantParent(BRD_ST_SimpleMode, BRD_AoE_SimpleMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", BRD.JobID)]
        BRD_Variant_Cure = 3301,

        [ReplaceSkill(BRD.RagingStrikes)]
        [CustomComboInfo("猛者强击-光明神的最终乐章-战斗之声-纷乱箭", "", BRD.JobID)]
        BRD_Buffs = 3400,

        [ReplaceSkill(All.HeadGraze)]
        [CustomComboInfo("oGCD", "", BRD.JobID)]
        BRD_oGCD = 3401,

        // Last value = 3400,

        #endregion

        #region DANCER

        #region Single Target Multibutton
        [ReplaceSkill(DNC.Cascade)]
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Single Target Multibutton Feature", "Single target combo with Fan Dances and Esprit use.", DNC.JobID)]
        DNC_ST_MultiButton = 4000,

        [ParentCombo(DNC_ST_MultiButton)]
        [CustomComboInfo("ST Esprit Overcap Option", "Adds Saber Dance above the set Esprit threshold.", DNC.JobID)]
        DNC_ST_EspritOvercap = 4001,

        [ParentCombo(DNC_ST_MultiButton)]
        [CustomComboInfo("Fan Dance Overcap Protection Option", "Adds Fan Dance 1 when Fourfold Feathers are full.", DNC.JobID)]
        DNC_ST_FanDanceOvercap = 4003,

        [ParentCombo(DNC_ST_MultiButton)]
        [CustomComboInfo("Fan Dance Option", "Adds Fan Dance 3/4 when available.", DNC.JobID)]
        DNC_ST_FanDance34 = 4004,
        #endregion

        #region AoE Multibutton
        [ReplaceSkill(DNC.Windmill)]
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("AoE Multibutton Feature", "AoE combo with Fan Dances and Esprit use.", DNC.JobID)]
        DNC_AoE_MultiButton = 4010,

        [ParentCombo(DNC_AoE_MultiButton)]
        [CustomComboInfo("AoE Esprit Overcap Option", "Adds Saber Dance above the set Esprit threshold.", DNC.JobID)]
        DNC_AoE_EspritOvercap = 4011,

        [ParentCombo(DNC_AoE_MultiButton)]
        [CustomComboInfo("AoE Fan Dance Overcap Protection Option", "Adds Fan Dance 2 when Fourfold Feathers are full.", DNC.JobID)]
        DNC_AoE_FanDanceOvercap = 4013,

        [ParentCombo(DNC_AoE_MultiButton)]
        [CustomComboInfo("AoE Fan Dance Option", "Adds Fan Dance 3/4 when available.", DNC.JobID)]
        DNC_AoE_FanDance34 = 4014,
        #endregion

        #region Dance Features
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Dance Features", "Features and options involving Standard Step and Technical Step.\nCollapsing this category does NOT disable the features inside.", DNC.JobID)]
        DNC_Dance_Menu = 4020,

        #region Combined Dance Feature
        [ReplaceSkill(DNC.StandardStep)]
        [ParentCombo(DNC_Dance_Menu)]
        [ConflictingCombos(DNC_DanceStepCombo, DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Combined Dance Feature", "Standard And Technical Dance on one button (SS)." +
        "\nStandard > Technical." +
        "\nThis combos out into Tillana and Starfall Dance.", DNC.JobID)]
        DNC_CombinedDances = 4022,

        [ParentCombo(DNC_CombinedDances)]
        [CustomComboInfo("Devilment Plus Option", "Adds Devilment right after Technical finish.", DNC.JobID)]
        DNC_CombinedDances_Devilment = 4023,

        [ParentCombo(DNC_CombinedDances)]
        [CustomComboInfo("Flourish Plus Option", "Adds Flourish to the Combined Dance Feature.", DNC.JobID)]
        DNC_CombinedDances_Flourish = 4024,
        #endregion
        #endregion

        #region Flourishing Features
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Flourishing Features", "Features and options involving Fourfold Feathers and Flourish." +
        "\nCollapsing this category does NOT disable the features inside.", DNC.JobID)]
        DNC_FlourishingFeatures_Menu = 4030,

        [ReplaceSkill(DNC.Flourish)]
        [ParentCombo(DNC_FlourishingFeatures_Menu)]
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Flourishing Fan Dance Feature", "Replace Flourish with Fan Dance 3 & 4 during weave-windows, when Flourish is on cooldown.", DNC.JobID)]
        DNC_FlourishingFanDances = 4032,
        #endregion

        #region Fan Dance Combo Features
        [ParentCombo(DNC_FlourishingFeatures_Menu)]
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Fan Dance Combo Feature", "Options for Fan Dance combos." +
        "\nFan Dance 3 takes priority over Fan Dance 4.", DNC.JobID)]
        DNC_FanDanceCombos = 4033,

        [ReplaceSkill(DNC.FanDance1)]
        [ParentCombo(DNC_FanDanceCombos)]
        [CustomComboInfo("Fan Dance 1 -> 3 Option", "Changes Fan Dance 1 to Fan Dance 3 when available.", DNC.JobID)]
        DNC_FanDance_1to3_Combo = 4034,

        [ReplaceSkill(DNC.FanDance1)]
        [ParentCombo(DNC_FanDanceCombos)]
        [CustomComboInfo("Fan Dance 1 -> 4 Option", "Changes Fan Dance 1 to Fan Dance 4 when available.", DNC.JobID)]
        DNC_FanDance_1to4_Combo = 4035,

        [ReplaceSkill(DNC.FanDance2)]
        [ParentCombo(DNC_FanDanceCombos)]
        [CustomComboInfo("Fan Dance 2 -> 3 Option", "Changes Fan Dance 2 to Fan Dance 3 when available.", DNC.JobID)]
        DNC_FanDance_2to3_Combo = 4036,

        [ReplaceSkill(DNC.FanDance2)]
        [ParentCombo(DNC_FanDanceCombos)]
        [CustomComboInfo("Fan Dance 2 -> 4 Option", "Changes Fan Dance 2 to Fan Dance 4 when available.", DNC.JobID)]
        DNC_FanDance_2to4_Combo = 4037,
        #endregion

        // Devilment --> Starfall
        [ReplaceSkill(DNC.Devilment)]
        [ConflictingCombos(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Devilment to Starfall Feature", "Change Devilment into Starfall Dance after use.", DNC.JobID)]
        DNC_Starfall_Devilment = 4038,

        [ReplaceSkill(DNC.StandardStep, DNC.TechnicalStep)]
        [ConflictingCombos(DNC_CombinedDances)]
        [CustomComboInfo("Dance Step Combo Feature", "Change Standard Step and Technical Step into each dance step, while dancing." +
        "\nWorks with Simple Dancer and Simple Dancer AoE.", DNC.JobID)]
        DNC_DanceStepCombo = 4039,

        #region Simple Dancer (Single Target)
        [ReplaceSkill(DNC.Cascade)]
        [ConflictingCombos(DNC_ST_MultiButton, DNC_AoE_MultiButton, DNC_CombinedDances, DNC_FlourishingFeatures_Menu, DNC_Starfall_Devilment)]
        [CustomComboInfo("Simple Dancer (Single Target) Feature", "Single button, single target. Includes songs, flourishes and overprotections." +
        "\nConflicts with all other non-simple toggles, except 'Dance Step Combo'.", DNC.JobID)]
        DNC_ST_SimpleMode = 4050,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Interrupt Option", "Includes an interrupt in the rotation (if applicable to your current target).", DNC.JobID, 5, "", "")]
        DNC_ST_Simple_Interrupt = 4051,

        [ParentCombo(DNC_ST_SimpleMode)]
        [ConflictingCombos(DNC_ST_Simple_StandardFill)]
        [CustomComboInfo("Simple Standard Dance Option", "Includes Standard Step (and all steps) in the rotation.", DNC.JobID, 1, "", "")]
        DNC_ST_Simple_SS = 4052,

        [ParentCombo(DNC_ST_SimpleMode)]
        [ConflictingCombos(DNC_ST_Simple_SS)]
        [CustomComboInfo("Simple Standard Fill Option", "Adds ONLY Standard dance steps and Standard Finish to the rotation." +
        "\nStandard Step itself must be initiated manually when using this option.", DNC.JobID, 1, "", "")]
        DNC_ST_Simple_StandardFill = 4061,

        [ParentCombo(DNC_ST_SimpleMode)]
        [ConflictingCombos(DNC_ST_Simple_TechFill)]
        [CustomComboInfo("Simple Technical Dance Option", "Includes Technical Step, all dance steps and Technical Finish in the rotation.", DNC.JobID, 2, "", "")]
        DNC_ST_Simple_TS = 4053,

        [ParentCombo(DNC_ST_SimpleMode)]
        [ConflictingCombos(DNC_ST_Simple_TS)]
        [CustomComboInfo("Simple Tech Fill Option", "Adds ONLY Technical dance steps and Technical Finish to the rotation." +
        "\nTechnical Step itself must be initiated manually when using this option.", DNC.JobID, 2, "", "")]
        DNC_ST_Simple_TechFill = 4054,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Tech Devilment Option", "Includes Devilment in the rotation." +
        "\nWill activate only during Technical Finish if you're Lv70 or above." +
        "\nWill be used on cooldown below Lv70.", DNC.JobID, 2, "", "")]
        DNC_ST_Simple_Devilment = 4055,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Saber Dance Option", "Includes Saber Dance in the rotation when at or over the Esprit threshold.", DNC.JobID, 3, "", "")]
        DNC_ST_Simple_SaberDance = 4063,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Flourish Option", "Includes Flourish in the rotation.", DNC.JobID, 3, "", "")]
        DNC_ST_Simple_Flourish = 4056,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Feathers Option", "Expends a feather in the next available weave window when capped." +
        "\nWeaves feathers where possible during Technical Finish." +
        "\nWeaves feathers outside of burst when target is below set HP percentage (Set to 0 to disable)." +
        "\nWeaves feathers whenever available when under Lv.70.", DNC.JobID, 4, "", "")]
        DNC_ST_Simple_Feathers = 4057,

        /*
        [ParentCombo(DNC_ST_Simple_Feathers)]
        [CustomComboInfo("Simple Feather Pooling Option", "Expends a feather in the next available weave window when capped." +
        "\nWeaves feathers where possible during Technical Finish." +
        "\nWeaves feathers outside of burst when target is below set HP percentage.", DNC.JobID, 4, "", "")]
        DNC_ST_Simple_FeatherPooling = 4058,
        */

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Panic Heals Option", "Includes Curing Waltz and Second Wind in the rotation when available and your HP is below the set percentages.", DNC.JobID, 5, "", "")]
        DNC_ST_Simple_PanicHeals = 4059,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Improvisation Option", "Includes Improvisation in the rotation when available.", DNC.JobID, 5, "", "")]
        DNC_ST_Simple_Improvisation = 4060,

        [ParentCombo(DNC_ST_SimpleMode)]
        [CustomComboInfo("Simple Peloton Opener Option", "Uses Peloton when you are out of combat, do not already have the Peloton buff and are performing Standard Step with greater than 5s remaining of your dance." +
        "\nWill not override Dance Step Combo Feature.", DNC.JobID, 5, "", "")]
        DNC_ST_Simple_Peloton = 4062,
        #endregion

        #region Simple Dancer (AoE)
        [ReplaceSkill(DNC.Windmill)]
        [ConflictingCombos(DNC_ST_MultiButton, DNC_AoE_MultiButton, DNC_CombinedDances, DNC_FlourishingFeatures_Menu, DNC_Starfall_Devilment)]
        [CustomComboInfo("Simple Dancer (AoE) Feature", "Single button, AoE. Includes songs, flourishes and overprotections." +
        "\nConflicts with all other non-simple toggles, except 'Dance Step Combo'.", DNC.JobID)]
        DNC_AoE_SimpleMode = 4070,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Interrupt Option", "Includes an interrupt in the AoE rotation (if your current target can be interrupted).", DNC.JobID)]
        DNC_AoE_Simple_Interrupt = 4071,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [ConflictingCombos(DNC_AoE_Simple_StandardFill)]
        [CustomComboInfo("Simple AoE Standard Dance Option", "Includes Standard Step (and all steps) in the AoE rotation.", DNC.JobID, 1, "", "")]
        DNC_AoE_Simple_SS = 4072,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [ConflictingCombos(DNC_AoE_Simple_SS)]
        [CustomComboInfo("Simple AoE Standard Fill Option", "Adds ONLY Standard dance steps and Standard Finish to the AoE rotation." +
        "\nStandard Step itself must be initiated manually when using this option.", DNC.JobID, 2, "", "")]
        DNC_AoE_Simple_StandardFill = 4081,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [ConflictingCombos(DNC_AoE_Simple_TechFill)]
        [CustomComboInfo("Simple AoE Technical Dance Option", "Includes Technical Step, all dance steps and Technical Finish in the AoE rotation.", DNC.JobID, 3, "", "")]
        DNC_AoE_Simple_TS = 4073,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [ConflictingCombos(DNC_AoE_Simple_TS)]
        [CustomComboInfo("Simple AoE Tech Fill Option", "Adds ONLY Technical dance steps and Technical Finish to the AoE rotation." +
        "\nTechnical Step itself must be initiated manually when using this option.", DNC.JobID, 4, "", "")]
        DNC_AoE_Simple_TechFill = 4074,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Tech Devilment Option", "Includes Devilment in the AoE rotation." +
        "\nWill activate only during Technical Finish if you're Lv70 or above." +
        "\nWill be used on cooldown below Lv70.", DNC.JobID, 5, "", "")]
        DNC_AoE_Simple_Devilment = 4075,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Saber Dance Option", "Includes Saber Dance in the AoE rotation when at or over the Esprit threshold.", DNC.JobID, 6, "", "")]
        DNC_AoE_Simple_SaberDance = 4082,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Flourish Option", "Includes Flourish in the AoE rotation.", DNC.JobID, 6, "", "")]
        DNC_AoE_Simple_Flourish = 4076,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Feathers Option", "Expends a feather in the next available weave window when capped." +
        "\nWeaves feathers where possible during Technical Finish." +
        "\nWeaves feathers whenever available when under Lv.70.", DNC.JobID, 7, "", "")]
        DNC_AoE_Simple_Feathers = 4077,

        /*
        [ParentCombo(DNC_AoE_Simple_Feathers)]
        [CustomComboInfo("Simple AoE Feather Pooling Option", "Expends a feather in the next available weave window when capped.", DNC.JobID, 8, "", "")]
        DNC_AoE_Simple_FeatherPooling = 4078,
        */

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Panic Heals Option", "Includes Curing Waltz and Second Wind in the AoE rotation when available and your HP is below the set percentages.", DNC.JobID, 9, "", "")]
        DNC_AoE_Simple_PanicHeals = 4079,

        [ParentCombo(DNC_AoE_SimpleMode)]
        [CustomComboInfo("Simple AoE Improvisation Option", "Includes Improvisation in the AoE rotation when available.", DNC.JobID, 10, "", "")]
        DNC_AoE_Simple_Improvisation = 4080,
        #endregion

        #region Variant
        [Variant]
        [VariantParent(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", DNC.JobID)]
        DNC_Variant_Rampart = 4083,

        [Variant]
        [VariantParent(DNC_ST_SimpleMode, DNC_AoE_SimpleMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", DNC.JobID)]
        DNC_Variant_Cure = 4084,


        #endregion

        // Last value = 4084

        #endregion

        #region DARK KNIGHT

        [CustomComboInfo("嗜血", "BloodWeapon", DRK.JobID)]
        DRK_BloodWeapon = 5000,

        [CustomComboInfo("掠影示现", "LivingShadow", DRK.JobID)]
        DRK_LivingShadow = 5001,

        [ReplaceSkill(DRK.HardSlash)]
        [CustomComboInfo("一键DPS", "", DRK.JobID)]
        DRK_ST_DPS = 5100,

        [ParentCombo(DRK_ST_DPS)]
        [CustomComboInfo("暗影锋", "Edge of Darkness", DRK.JobID)]
        DRK_ST_EdgeofDarkness = 5102,

        [ParentCombo(DRK_ST_DPS)]
        [CustomComboInfo("腐秽大地", "Salted Earth", DRK.JobID)]
        DRK_ST_SaltedEarth = 5103, 

        [ParentCombo(DRK_ST_DPS)]
        [CustomComboInfo("跳斩", "Plunge", DRK.JobID)]
        DRK_ST_Plunge = 5104,

        [ParentCombo(DRK_ST_DPS)]
        [CustomComboInfo("双目标模式", "Two Target", DRK.JobID)]
        DRK_ST_TwoTarget = 5105,

        [ReplaceSkill(DRK.Unleash)]
        [CustomComboInfo("AoE", "", DRK.JobID)]
        DRK_AoE = 5200,

        [Variant]
        [VariantParent(DRK_ST_DPS, DRK_AoE)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", DRK.JobID)]
        DRK_Variant_SpiritDart = 5300,

        [Variant]
        [VariantParent(DRK_ST_DPS, DRK_AoE)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", DRK.JobID)]
        DRK_Variant_Cure = 5301,

        [Variant]
        [VariantParent(DRK_ST_DPS, DRK_AoE)]
        [CustomComboInfo("Ultimatum Option", "Use Variant Ultimatum on cooldown.", DRK.JobID)]
        DRK_Variant_Ultimatum = 5302,

        [ReplaceSkill(DRK.BloodWeapon)]
        [CustomComboInfo("掠影示现-嗜血-血乱", "LivingShadow_BloodWeapon_Delirium", DRK.JobID)]
        DRK_LivingShadow_BloodWeapon_Delirium = 5400,

        // Last value = 5302

        #endregion

        #region DRAGOON

        [CustomComboInfo("猛枪", "Lance Charge", DRG.JobID)]
        DRG_Lance = 6000,

        [ParentCombo(DRG_ST_AdvancedMode)]
        [CustomComboInfo("巨龙视线", "Dragon Sight", DRG.JobID)]
        DRG_ST_DragonSight = 6001,

        [CustomComboInfo("各种跳跃", "", DRG.JobID)]
        DRG_Dives = 6002,

        [ParentCombo(DRG_Dives)]
        [CustomComboInfo("会使用跳跃技能的最大距离", "Uses Spineshatter Dive, Dragonfire Dive, and Stardiver when in the target's target ring (1 yalm) and closer.", DRG.JobID)]
        DRG_Dives_Range = 6003,

        #region Advanced ST Dragoon

        [ReplaceSkill(DRG.TrueThrust)]
        [CustomComboInfo("一键DPS", "", DRG.JobID)]
        DRG_ST_AdvancedMode = 6100,

        [ParentCombo(DRG_ST_AdvancedMode)]
        [CustomComboInfo("坠星冲", "Stardiver", DRG.JobID)]
        DRG_ST_Stardiver = 6101,

        [ParentCombo(DRG_ST_AdvancedMode)]
        [CustomComboInfo("破碎冲", "Spineshatter Dive", DRG.JobID)]
        DRG_ST_Spineshatter = 6102,

        [ParentCombo(DRG_ST_AdvancedMode)]
        [CustomComboInfo("高跳和蓝龙炮", "Jump and Geirskogul", DRG.JobID)]
        DRG_ST_JumpAndGeirskogul = 6103,

        #endregion

        #region Advanced AoE Dragoon

        [ReplaceSkill(DRG.DoomSpike)]
        [CustomComboInfo("AoE", "", DRG.JobID)]
        DRG_AOE_AdvancedMode = 6201,

        [ParentCombo(DRG_AOE_AdvancedMode)]
        [CustomComboInfo("破碎冲", "Spineshatter Dive", DRG.JobID)]
        DRG_AoE_Spineshatter_Dive = 6202,

        [ParentCombo(DRG_AOE_AdvancedMode)]
        [CustomComboInfo("Combo Heals Option", "Adds Bloodbath and Second Wind to the combo, using them when below the HP Percentage threshold.", DRG.JobID)]
        DRG_AoE_ComboHeals = 6203,

        #endregion

        [Variant]
        [VariantParent(DRG_ST_AdvancedMode, DRG_AOE_AdvancedMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", DRG.JobID)]
        DRG_Variant_Cure = 6300,

        [Variant]
        [VariantParent(DRG_ST_AdvancedMode, DRG_AOE_AdvancedMode)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", DRG.JobID)]
        DRG_Variant_Rampart = 6301,

        [ReplaceSkill(DRG.Jump, DRG.HighJump)]
        [CustomComboInfo("高跳-幻象冲", "Jump-MirageDive", DRG.JobID)]
        DRG_Jump = 6400,

        // Last value = 6400

        #endregion

        #region GUNBREAKER

        [ConflictingCombos(GNB_BloodFest_NoMercy)]
        [CustomComboInfo("无情", "No Mercy", GNB.JobID)]
        GNB_NoMercy = 7000,

        #region ST
        [ReplaceSkill(GNB.KeenEdge)]
        [CustomComboInfo("一键DPS", "", GNB.JobID)]
        GNB_ST_MainCombo = 7001,

        [ParentCombo(GNB_ST_MainCombo)]
        [CustomComboInfo("倍功", "Double Down", GNB.JobID)]
        GNB_ST_DoubleDown = 7002,

        [ParentCombo(GNB_ST_MainCombo)]
        [CustomComboInfo("弓形冲波", "Bow Shock", GNB.JobID)]
        GNB_ST_BowShock = 7003,

        [ParentCombo(GNB_ST_MainCombo)]
        [CustomComboInfo("音速破", "Sonic Break", GNB.JobID)]
        GNB_ST_SonicBreak = 7004,

        [ParentCombo(GNB_ST_MainCombo)]
        [CustomComboInfo("粗分析", "Rough Divide", GNB.JobID)]
        GNB_ST_RoughDivide = 7005,

        [ParentCombo(GNB_ST_MainCombo)]
        [CustomComboInfo("直接用裂牙", "不管是不是在无情期间，有裂牙直接打", GNB.JobID)]
        GNB_ST_GnashingFang_Go = 7006,

        [ParentCombo(GNB_ST_MainCombo)]
        [CustomComboInfo("双目标模式", "", GNB.JobID)]
        GNB_ST_TwoTarget = 7007,

        #endregion

        #region AoE
        [ReplaceSkill(GNB.DemonSlice)]
        [CustomComboInfo("AoE DPS", "", GNB.JobID)]
        GNB_AoE_MainCombo = 7100,

        [ParentCombo(GNB_AoE_MainCombo)]
        [CustomComboInfo("弓形冲波", "Bow Shock", GNB.JobID)]
        GNB_AoE_BowShock = 7102,

        [ParentCombo(GNB_AoE_MainCombo)]
        [CustomComboInfo("倍功", "Double Down", GNB.JobID)]
        GNB_AoE_DoubleDown = 7103,

        #endregion

        [ReplaceSkill(GNB.Aurora)]
        [CustomComboInfo("极光-下踢-极光", "Aurora-LowBlow-Aurora", GNB.JobID)]
        GNB_AuroraProtection = 7200,

        [ConflictingCombos(GNB_NoMercy)]
        [ReplaceSkill(GNB.NoMercy)]
        [CustomComboInfo("血壤-无情", "只会在开场20秒有效", GNB.JobID)]
        GNB_BloodFest_NoMercy = 7201, 

        [Variant]
        [VariantParent(GNB_ST_MainCombo, GNB_AoE_MainCombo)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", GNB.JobID)]
        GNB_Variant_SpiritDart = 7300,

        [Variant]
        [VariantParent(GNB_ST_MainCombo, GNB_AoE_MainCombo)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", GNB.JobID)]
        GNB_Variant_Cure = 7301,

        [Variant]
        [VariantParent(GNB_ST_MainCombo, GNB_AoE_MainCombo)]
        [CustomComboInfo("Ultimatum Option", "Use Variant Ultimatum on cooldown.", GNB.JobID)]
        GNB_Variant_Ultimatum = 7302,

        // Last value = 7302

        #endregion

        #region MACHINIST

        [CustomComboInfo("枪管加热", "", MCH.JobID)]
        MCH_BarrelStabilizer = 8000,

        [ReplaceSkill(MCH.SplitShot, MCH.HeatedSplitShot)]
        [CustomComboInfo("一键DPS", "", MCH.JobID)]
        MCH_ST_DPS = 8100,

        [ParentCombo(MCH_ST_DPS)]
        [CustomComboInfo("机器人", "", MCH.JobID)]
        MCH_ST_AutomatonQueen = 8101,

        [ParentCombo(MCH_ST_DPS)]
        [CustomComboInfo("超荷", "", MCH.JobID)]
        MCH_ST_Hypercharge = 8102,

        [ParentCombo(MCH_ST_DPS)]
        [CustomComboInfo("弹射", "", MCH.JobID)]
        MCH_ST_Ricochet = 8103,

        [ParentCombo(MCH_ST_DPS)]
        [CustomComboInfo("野火", "", MCH.JobID)]
        MCH_ST_Wildfire = 8104,

        [ReplaceSkill(MCH.SpreadShot, MCH.Scattergun)]
        [CustomComboInfo("AoE", "", MCH.JobID)]
        MCH_AoE = 8200,

        #region Variant

        [Variant]
        [VariantParent(MCH_ST_DPS, MCH_AoE)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", MCH.JobID)]
        MCH_Variant_Rampart = 8300,

        [Variant]
        [VariantParent(MCH_ST_DPS, MCH_AoE)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", MCH.JobID)]
        MCH_Variant_Cure = 8301,

        #endregion

        [ReplaceSkill(MCH.RookAutoturret, MCH.AutomatonQueen)]
        [CustomComboInfo("机器人-提前结束", "AutomatonQueen-QueenOverdrive", MCH.JobID)]
        MCH_AutomatonQueen_QueenOverdrive = 8400,

        [ReplaceSkill(MCH.ChainSaw)]
        [CustomComboInfo("整备-回转飞锯-弹射", "在回转飞锯CD<=2或>=55出现]", MCH.JobID)]
        MCH_ChainSaw_Ricochet = 8401,

        [ReplaceSkill(MCH.FlameThrower)]
        [CustomComboInfo("野火-火焰喷射器", "", MCH.JobID)]
        MCH_Wildfire_FlameThrower = 8402,

        [ReplaceSkill(MCH.Drill)]
        [CustomComboInfo("空气锚-钻头-回转飞锯", "AirAnchor-Drill-Chainsaw", MCH.JobID)]
        MCH_AirAnchor_Drill_Chainsaw = 8403,

        [ReplaceSkill(All.HeadGraze)]
        [CustomComboInfo("超荷保护", "防止卡钻头三大件的CD，但如果枪管加热的CD转好了会忽视检测回转飞锯的CD", MCH.JobID)]
        MCH_Hypercharge_Protection = 8404, 

        // Last value = 8404

        #endregion

        #region MONK

        [CustomComboInfo("红莲极意", "RiddleOfFire", MNK.JobID)]
        MNK_RiddleOfFire = 9000,

        [ReplaceSkill(MNK.Bootshine)]
        [CustomComboInfo("一键DPS", "", MNK.JobID)]
        MNK_ST_SimpleMode = 9001,

        [ParentCombo(MNK_ST_SimpleMode)]
        [CustomComboInfo("震脚", "Perfect Balance", MNK.JobID)]
        MNK_ST_PerfectBalance = 9002,

        [ParentCombo(MNK_ST_SimpleMode)]
        [CustomComboInfo("义结金兰", "Brotherhood", MNK.JobID)]
        MNK_ST_Brotherhood = 9003,

        [ParentCombo(MNK_ST_SimpleMode)]
        [CustomComboInfo("疾风极意", "Riddle of Wind", MNK.JobID)]
        MNK_ST_RiddleOfWind = 9004,

        [ParentCombo(MNK_ST_SimpleMode)]
        [CustomComboInfo("不打DoT", "No DoT", MNK.JobID)]
        MNK_ST_NoDoT = 9005,

        [ParentCombo(MNK_ST_SimpleMode)]
        [CustomComboInfo("双目标模式", "", MNK.JobID)]
        MNK_ST_TwoTarget = 9006,

        [ReplaceSkill(MNK.ArmOfTheDestroyer)]
        [CustomComboInfo("AoE", "Replaces Arm Of The Destroyer with its combo chain.", MNK.JobID)]
        MNK_AoE_SimpleMode = 9100,

        [ParentCombo(MNK_AoE_SimpleMode)]
        [CustomComboInfo("内丹浴血", "Adds Bloodbath and Second Wind to the combo, using them when below the HP Percentage threshold.", MNK.JobID)]
        MNK_AoE_ComboHeals = 9101,

        [Variant]
        [VariantParent(MNK_ST_SimpleMode, MNK_AoE_SimpleMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", MNK.JobID)]
        MNK_Variant_Cure = 9200,

        [Variant]
        [VariantParent(MNK_ST_SimpleMode, MNK_AoE_SimpleMode)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", MNK.JobID)]
        MNK_Variant_Rampart = 9201,

        [CustomComboInfo("博兹雅", "Bozja", MNK.JobID)]
        MNK_Bozja = 9300,

        [ParentCombo(MNK_Bozja)]
        [CustomComboInfo("失传破甲", "", MNK.JobID)]
        MNK_Bozja_LostRendArmor = 9301,

        [ParentCombo(MNK_Bozja)]
        [CustomComboInfo("失传暗杀", "", MNK.JobID)]
        MNK_Bozja_LostAssassination = 9302,

        [ParentCombo(MNK_Bozja)]
        [CustomComboInfo("力泉后自动舍身", "", MNK.JobID)]
        MNK_Bozja_LostFontofPowerThenBannerOfHonoredSacrifice = 9303,

        [ReplaceSkill(MNK.PerfectBalance)]
        [CustomComboInfo("无目标震脚", "无目标开启震脚后，会切换成打AOE的GCD技能，方便BOSS上天落地后打必杀", MNK.JobID)]
        MNK_PerfectBalance = 9400,

        // Last value = 9400

        #endregion

        #region NINJA

        [ReplaceSkill(NIN.SpinningEdge)]
        [ConflictingCombos(NIN_ArmorCrushCombo, NIN_ST_AdvancedMode, NIN_KassatsuChiJin, NIN_KassatsuTrick)]
        [CustomComboInfo("Simple Mode - Single Target", "Replaces Spinning Edge with a one-button full single target rotation.\nThis is the ideal option for newcomers to the job.", NIN.JobID)]
        NIN_ST_SimpleMode = 10000,

        [ParentCombo(NIN_ST_SimpleMode)]
        [CustomComboInfo("Balance Opener Option", "Starts with the Balance opener.\nDoes pre-pull first, if you enter combat before hiding the opener will fail.\nLikewise, moving during TCJ will cause the opener to fail too.\nRequires you to be out of combat with majority of your cooldowns available for it to work.", NIN.JobID)]
        NIN_ST_SimpleMode_BalanceOpener = 10001,

        [ReplaceSkill(NIN.DeathBlossom)]
        [ConflictingCombos(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Simple Mode - AoE", "Turns Death Blossom into a one-button full AoE rotation.", NIN.JobID)]
        NIN_AoE_SimpleMode = 10002,

        [ReplaceSkill(NIN.SpinningEdge)]
        [ConflictingCombos(NIN_ST_SimpleMode)]
        [CustomComboInfo("Advanced Mode - Single Target", "Replace Spinning Edge with a one-button full single target rotation.\nThese features are ideal if you want to customize the rotation.", NIN.JobID)]
        NIN_ST_AdvancedMode = 10003,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Throwing Dagger Uptime Option", "Adds Throwing Dagger to Advanced Mode if out of melee range.", NIN.JobID)]
        NIN_ST_AdvancedMode_RangedUptime = 10004,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Mug Option", "Adds Mug to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Mug = 10005,

        [ConflictingCombos(NIN_ST_AdvancedMode_Mug_AlignBefore)]
        [ParentCombo(NIN_ST_AdvancedMode_Mug)]
        [CustomComboInfo("Align Mug with Trick Attack Option", "Only uses Mug whilst the target has Trick Attack, otherwise will use on cooldown.", NIN.JobID)]
        NIN_ST_AdvancedMode_Mug_AlignAfter = 10006,

        [ConflictingCombos(NIN_ST_AdvancedMode_Mug_AlignAfter)]
        [ParentCombo(NIN_ST_AdvancedMode_Mug)]
        [CustomComboInfo("Use Mug before Trick Attack Option", "Aligns Mug with Trick Attack but weaves it at least 1 GCD before Trick Attack.", NIN.JobID)]
        NIN_ST_AdvancedMode_Mug_AlignBefore = 10007,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Trick Attack Option", "Adds Trick Attack to Advanced Mode.", NIN.JobID)] //Has Config
        NIN_ST_AdvancedMode_TrickAttack = 10008,

        [ParentCombo(NIN_ST_AdvancedMode_TrickAttack)]
        [CustomComboInfo("Save Cooldowns Before Trick Attack Option", "Stops using abilities with longer cooldowns up to 15 seconds before Trick Attack comes off cooldown.", NIN.JobID)] //HasConfig
        NIN_ST_AdvancedMode_TrickAttack_Cooldowns = 10009,

        [ParentCombo(NIN_ST_AdvancedMode_TrickAttack)]
        [CustomComboInfo("Delayed Trick Attack Option", "Waits at least 8 seconds into combat before using Trick Attack.", NIN.JobID)]
        NIN_ST_AdvancedMode_TrickAttack_Delayed = 10010,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Ninjitsu Option", "Adds Ninjitsu to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Ninjitsus = 10011,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Hold 1 Charge", "Prevent using both charges of Mudra.", NIN.JobID)]
        NIN_ST_AdvancedMode_Ninjitsus_ChargeHold = 10012,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Fuma Shuriken", "Spends Mudra charges on Fuma Shuriken (only before Raiton is available).", NIN.JobID)]
        NIN_ST_AdvancedMode_Ninjitsus_FumaShuriken = 10013,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Raiton", "Spends Mudra charges on Raiton.", NIN.JobID)]
        NIN_ST_AdvancedMode_Ninjitsus_Raiton = 10014,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Suiton", "Spends Mudra charges on Suiton.", NIN.JobID)]
        NIN_ST_AdvancedMode_Ninjitsus_Suiton = 10015,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Huton", "Spends Mudra charges on Huton.", NIN.JobID)]
        NIN_ST_AdvancedMode_Ninjitsus_Huton = 10016,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Assassinate/Dream Within a Dream Option", "Adds Assassinate and Dream Within a Dream to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_AssassinateDWAD = 10017,

        [ConflictingCombos(NIN_KassatsuTrick, NIN_KassatsuChiJin)]
        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Kassatsu Option", "Adds Kassatsu to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Kassatsu = 10018,

        [ParentCombo(NIN_ST_AdvancedMode_Kassatsu)]
        [CustomComboInfo($"Use Hyosho Ranryu Option", "Spends Kassatsu on Hyosho Ranryu.", NIN.JobID)]
        NIN_ST_AdvancedMode_Kassatsu_HyoshoRaynryu = 10019,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Armor Crush Option", "Adds Armor Crush to Advanced Mode.", NIN.JobID)] //Has Config
        NIN_ST_AdvancedMode_ArmorCrush = 10020,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Huraijin Option", "Adds Huraijin to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Huraijin = 10021,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Bhavacakra Option", "Adds Bhavacakra to Advanced Mode.", NIN.JobID)] //Has Config
        NIN_ST_AdvancedMode_Bhavacakra = 10022,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Ten Chi Jin Option", "Adds Ten Chi Jin (the cooldown) to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_TCJ = 10023,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Meisui Option", "Adds Meisui to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Meisui = 10024,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Bunshin Option", "Adds Bunshin to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Bunshin = 10025,

        [ParentCombo(NIN_ST_AdvancedMode_Bunshin)]
        [CustomComboInfo("Phantom Kamaitachi Option", "Adds Phantom Kamaitachi to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Bunshin_Phantom = 10026,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Raiju Option", "Adds Fleeting/Forked Raiju to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Raiju = 10027,

        [ParentCombo(NIN_ST_AdvancedMode_Raiju)]
        [CustomComboInfo("Forked Raiju Gap-Closer Option", "Uses Forked Raiju when out of range.", NIN.JobID)]
        NIN_ST_AdvancedMode_Raiju_Forked = 10028,

        [ConflictingCombos(NIN_KassatsuChiJin, NIN_KassatsuTrick)]
        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Balance Opener Option", "Starts with the Balance opener.\nDoes pre-pull first, if you enter combat before hiding the opener will fail.\nLikewise, moving during TCJ will cause the opener to fail too.\nRequires you to be out of combat with majority of your cooldowns available for it to work.", NIN.JobID)]
        NIN_ST_AdvancedMode_BalanceOpener = 10029,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("True North Option", "Adds True North to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_TrueNorth = 10030,

        [ParentCombo(NIN_ST_AdvancedMode_TrueNorth)]
        [CustomComboInfo("Use Before Armor Crush Only Option", "Only triggers the use of True North before Armor Crush.", NIN.JobID)]
        NIN_ST_AdvancedMode_TrueNorth_ArmorCrush = 10031,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Second Wind Option", "Adds Second Wind to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_SecondWind = 10032,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Shade Shift Option", "Adds Shade Shift to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_ShadeShift = 10033,

        [ParentCombo(NIN_ST_AdvancedMode)]
        [CustomComboInfo("Bloodbath Option", "Adds Bloodbath to Advanced Mode.", NIN.JobID)]
        NIN_ST_AdvancedMode_Bloodbath = 10034,

        [ReplaceSkill(NIN.DeathBlossom)]
        [ConflictingCombos(NIN_AoE_SimpleMode)]
        [CustomComboInfo("Advanced Mode - AoE", "Replace Death Blossom with a one-button full AoE rotation.\nThese features are ideal if you want to customize the rotation.", NIN.JobID)]
        NIN_AoE_AdvancedMode = 10035,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Assassinate/Dream Within a Dream Option", "Adds Assassinate/Dream Within a Dream to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_AssassinateDWAD = 10036,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Ninjitsu Option", "Adds Ninjitsu to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Ninjitsus = 10037,

        [ParentCombo(NIN_AoE_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Hold 1 Charge", "Prevent using both charges of Mudra.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Ninjitsus_ChargeHold = 10038,

        [ParentCombo(NIN_AoE_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Katon", "Spends Mudra charges on Katon.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Ninjitsus_Katon = 10039,

        [ParentCombo(NIN_AoE_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Doton", "Spends Mudra charges on Doton.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Ninjitsus_Doton = 10040,

        [ParentCombo(NIN_AoE_AdvancedMode_Ninjitsus)]
        [CustomComboInfo("Use Huton", "Spends Mudra charges on Huton.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Ninjitsus_Huton = 10041,

        [ConflictingCombos(NIN_KassatsuTrick, NIN_KassatsuChiJin)]
        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Kassatsu Option", "Adds Kassatsu to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Kassatsu = 10042,

        [ParentCombo(NIN_AoE_AdvancedMode_Kassatsu)]
        [CustomComboInfo("Goka Mekkyaku Option", "Adds Goka Mekkyaku to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_GokaMekkyaku = 10043,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Huraijin Option", "Adds Huraijin to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Huraijin = 10044,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Hellfrog Medium Option", "Adds Hellfrog Medium to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_HellfrogMedium = 10045,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Ten Chi Jin Option", "Adds Ten Chi Jin (the cooldown) to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_TCJ = 10046,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Meisui Option", "Adds Meisui to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Meisui = 10047,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Bunshin Option", "Adds Bunshin to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Bunshin = 10048,

        [ParentCombo(NIN_AoE_AdvancedMode_Bunshin)]
        [CustomComboInfo("Phantom Kamaitachi Option", "Adds Phantom Kamaitachi to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Bunshin_Phantom = 10049,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Second Wind Option", "Adds Second Wind to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_SecondWind = 10050,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Shade Shift Option", "Adds Shade Shift to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_ShadeShift = 10051,

        [ParentCombo(NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Bloodbath Option", "Adds Bloodbath to Advanced Mode.", NIN.JobID)]
        NIN_AoE_AdvancedMode_Bloodbath = 10052,

        [ReplaceSkill(NIN.ArmorCrush)]
        [ConflictingCombos(NIN_ST_SimpleMode)]
        [CustomComboInfo("Armor Crush Combo Feature", "Replace Armor Crush with its combo chain.", NIN.JobID)]
        NIN_ArmorCrushCombo = 10053,

        [ConflictingCombos(NIN_ST_AdvancedMode_BalanceOpener, NIN_ST_AdvancedMode_BalanceOpener, NIN_ST_AdvancedMode_Kassatsu, NIN_AoE_AdvancedMode_Kassatsu, NIN_KassatsuChiJin)]
        [ReplaceSkill(NIN.Kassatsu)]
        [CustomComboInfo("Kassatsu to Trick Feature", "Replaces Kassatsu with Trick Attack while Suiton or Hidden is up.\nCooldown tracking plugin recommended.", NIN.JobID)]
        NIN_KassatsuTrick = 10054,

        [ReplaceSkill(NIN.TenChiJin)]
        [CustomComboInfo("Ten Chi Jin to Meisui Feature", "Replaces Ten Chi Jin (the move) with Meisui while Suiton is up.\nCooldown tracking plugin recommended.", NIN.JobID)]
        NIN_TCJMeisui = 10055,

        [ConflictingCombos(NIN_ST_AdvancedMode_BalanceOpener, NIN_ST_AdvancedMode_BalanceOpener, NIN_KassatsuTrick, NIN_ST_AdvancedMode_Kassatsu, NIN_AoE_AdvancedMode_Kassatsu)]
        [ReplaceSkill(NIN.Chi)]
        [CustomComboInfo("Kassatsu Chi/Jin Feature", "Replaces Chi with Jin while Kassatsu is up if you have Enhanced Kassatsu.", NIN.JobID)]
        NIN_KassatsuChiJin = 10056,

        [ReplaceSkill(NIN.Hide)]
        [CustomComboInfo("Hide to Mug/Trick Attack Feature", "Replaces Hide with Mug while in combat and Trick Attack whilst Hidden.", NIN.JobID)]
        NIN_HideMug = 10057,

        [ReplaceSkill(NIN.Huraijin)]
        [CustomComboInfo("Huraijin / Raiju Feature", "Replaces Huraijin with Forked and Fleeting Raiju when available.", NIN.JobID)]
        NIN_HuraijinRaiju = 10059,

        [ParentCombo(NIN_HuraijinRaiju)]
        [CustomComboInfo("Huraijin / Raiju Feature Option 1", "Replaces Huraijin with Fleeting Raiju when available.", NIN.JobID)]
        NIN_HuraijinRaiju_Fleeting = 10060,

        [ParentCombo(NIN_HuraijinRaiju)]
        [CustomComboInfo("Huraijin / Raiju Feature Option 2", "Replaces Huraijin with Forked Raiju when available.", NIN.JobID)]
        NIN_HuraijinRaiju_Forked = 10061,

        [ReplaceSkill(NIN.Ten, NIN.Chi, NIN.Jin)]
        [CustomComboInfo("Simple Mudras Feature", "Simplify the mudra casting to avoid failing.", NIN.JobID)]
        NIN_Simple_Mudras = 10062,

        [ReplaceSkill(NIN.TenChiJin)]
        [ParentCombo(NIN_TCJMeisui)]
        [CustomComboInfo("Ten Chi Jin Feature", "Turns Ten Chi Jin (the move) into Ten, Chi, and Jin.", NIN.JobID)]
        NIN_TCJ = 10063,

        [ReplaceSkill(NIN.Huraijin)]
        [CustomComboInfo("Huraijin / Armor Crush Combo Feature", "Replace Huraijin with Armor Crush after using Gust Slash.", NIN.JobID)]
        NIN_HuraijinArmorCrush = 10064,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus_Raiton)]
        [CustomComboInfo("Raiton Uptime Option", "Adds Raiton as an uptime feature.", NIN.JobID)]
        NIN_ST_AdvancedMode_Raiton_Uptime = 10065,

        [ParentCombo(NIN_ST_AdvancedMode_Bunshin_Phantom)]
        [CustomComboInfo("Phantom Kamaitachi Uptime Option", "Adds Phantom Kamaitachi as an uptime feature.", NIN.JobID)]
        NIN_ST_AdvancedMode_Phantom_Uptime = 10066,

        [ParentCombo(NIN_ST_AdvancedMode_Ninjitsus_Suiton)]
        [CustomComboInfo("Suiton Uptime Option", "Adds Suiton as an uptime feature.", NIN.JobID)]
        NIN_ST_AdvancedMode_Suiton_Uptime = 10067,

        [ParentCombo(NIN_ST_AdvancedMode_TrueNorth_ArmorCrush)]
        [CustomComboInfo("Dynamic True North Option", "Adds True North before Armor Crush when you are not in the correct position for the enhanced potency bonus.", NIN.JobID)]
        NIN_ST_AdvancedMode_TrueNorth_ArmorCrush_Dynamic = 10068,

        [Variant]
        [VariantParent(NIN_ST_SimpleMode, NIN_ST_AdvancedMode, NIN_AoE_SimpleMode, NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", NIN.JobID)]
        NIN_Variant_Cure = 10069,

        [Variant]
        [VariantParent(NIN_ST_SimpleMode, NIN_ST_AdvancedMode, NIN_AoE_SimpleMode, NIN_AoE_AdvancedMode)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", NIN.JobID)]
        NIN_Variant_Rampart = 10070,

        [CustomComboInfo("博兹雅", "Bozja", NIN.JobID)]
        NIN_Bozja = 10100, 

        [ParentCombo(NIN_Bozja)]
        [CustomComboInfo("失传破甲", "", NIN.JobID)]
        NIN_Bozja_LostRendArmor = 10101,

        [ParentCombo(NIN_Bozja)]
        [CustomComboInfo("失传暗杀", "", NIN.JobID)]
        NIN_Bozja_LostAssassination = 10102,

        // Last value = 10102

        #endregion

        #region PALADIN

        //// Last value = 11032

        [CustomComboInfo("战逃反应", "", PLD.JobID)]
        PLD_FightorFlight = 11000,

        [ReplaceSkill(PLD.FastBlade)]
        [CustomComboInfo("一键DPS", "", PLD.JobID)]
        PLD_ST_AdvancedMode = 11100,

        [ParentCombo(PLD_ST_AdvancedMode)]
        [CustomComboInfo("调停", "Intervene", PLD.JobID)]
        PLD_ST_AdvancedMode_Intervene = 11101, 

        [ReplaceSkill(PLD.TotalEclipse)]
        [CustomComboInfo("AoE", "", PLD.JobID)]
        PLD_AoE_AdvancedMode = 11200,

        [ReplaceSkill(PLD.Cover)]
        [CustomComboInfo("保护-深仁", " ", PLD.JobID)]
        PLD_Cover_Clemency = 11300,

        [Variant]
        [VariantParent(PLD_ST_AdvancedMode, PLD_AoE_AdvancedMode)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", PLD.JobID)]
        PLD_Variant_SpiritDart = 11400,

        [Variant]
        [VariantParent(PLD_ST_AdvancedMode, PLD_AoE_AdvancedMode)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", PLD.JobID)]
        PLD_Variant_Cure = 11401,

        [Variant]
        [VariantParent(PLD_ST_AdvancedMode, PLD_AoE_AdvancedMode)]
        [CustomComboInfo("Ultimatum Option", "Use Variant Ultimatum on cooldown.", PLD.JobID)]
        PLD_Variant_Ultimatum = 11402,

        //// Last value = 11032

        #endregion

        #region REAPER

        #region Single Target (Slice) Combo Section
        [ReplaceSkill(RPR.Slice, RPR.Harpe)]
        [CustomComboInfo("Slice Combo Feature", "Replace Slice with its combo chain.\nIf all sub options are toggled will turn into a full one button rotation (Advanced Reaper)", RPR.JobID)]
        RPR_ST_SliceCombo = 12001,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Positional Preference", "Choose positional order for all positional related features.\nSupports turning Slice/Shadow of Death into all positionals or Slice and Shadow of Death being two separate positionals.", RPR.JobID)]
        ReaperPositionalConfigST = 12000,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Soul Slice Option", "Adds Soul Slice to Slice Combo when Soul Gauge is 50 or less and when current target is afflicted with Death's Design.", RPR.JobID)]
        RPR_ST_SliceCombo_SoulSlice = 12002,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Shadow Of Death Option", "Adds Shadow of Death to Slice Combo if Death's Design is not present on current target, or is about to expire.", RPR.JobID)]
        RPR_ST_SliceCombo_SoD = 12003,

        [ParentCombo(RPR_ST_SliceCombo_SoD)]
        [CustomComboInfo("Double SoD Enshroud Option", "Uses Shadow of Death twice during the first of the two Enshroud Bursts during the 2-minute windows (Double Enshroud Burst).", RPR.JobID)]
        RPR_ST_SliceCombo_SoD_Double = 12004,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Stun Option", "Adds Leg Sweep to main combo when target is performing an interruptible cast.", RPR.JobID)]
        RPR_ST_SliceCombo_Stun = 12005,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Combo Heals Option", "Adds Bloodbath and Second Wind to the combo at 65%% and 40%% HP, respectively.", RPR.JobID)]
        RPR_ST_SliceCombo_ComboHeals = 12006,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Ranged Filler Option", "Replaces the combo chain with Harpe (or Harvest Moon, if available) when outside of melee range. Will not override Communio.", RPR.JobID)]
        RPR_ST_SliceCombo_RangedFiller = 12007,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Enshroud Option", "Adds Enshroud to the combo when at 50 Shroud or greater and when current target is afflicted with Death's Design.", RPR.JobID)]
        RPR_ST_SliceCombo_Enshroud = 12008,

        [ParentCombo(RPR_ST_SliceCombo_Enshroud)]
        [CustomComboInfo("Enshroud Burst (Double Enshroud) Option", "Uses Enshroud at 50 Shroud during Arcane Circle (mimics the 2-minute Double Enshroud window) and will use Enshroud for odd minute bursts.\nBelow level 88, will use Enshroud at 50 gauge.", RPR.JobID)]
        RPR_ST_SliceCombo_EnshroudPooling = 12009,

        [ParentCombo(RPR_ST_SliceCombo_GibbetGallows)]
        [CustomComboInfo("Lemure's Slice Option", "Adds Lemure's Slice to the combo when there are 2 Void Shroud charges.", RPR.JobID, 1, "", "")]
        RPR_ST_SliceCombo_GibbetGallows_Lemure = 12010,

        [ParentCombo(RPR_ST_SliceCombo_GibbetGallows)]
        [CustomComboInfo("Communio Finisher Option", "Adds Communio to the combo when there is 1 charge of Lemure Shroud left.", RPR.JobID, 1, "", "")]
        RPR_ST_SliceCombo_GibbetGallows_Communio = 12011,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Arcane Circle Option", "Adds Arcane Circle to the combo when available and when current target is afflicted with Death's Design.", RPR.JobID)]
        RPR_ST_SliceCombo_ArcaneCircle = 12012,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Plentiful Harvest Option", "Adds Plentiful Harvest to the combo when available.", RPR.JobID)]
        RPR_ST_SliceCombo_PlentifulHarvest = 12013,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Gibbet and Gallows Option", "Adds Gibbet and Gallows to the combo when current target is afflicted with Death's Design.", RPR.JobID)]
        RPR_ST_SliceCombo_GibbetGallows = 12014,

        [ParentCombo(RPR_ST_SliceCombo_GibbetGallows)]
        [CustomComboInfo("Void/Cross Reaping Option", "Adds Void Reaping and Cross Reaping to the to the the combo during Enshroud.\n(Disabling this may stop the one-button combo working during enshroud)", RPR.JobID)]
        RPR_ST_SliceCombo_GibbetGallows_VoidCross = 12065,

        [ReplaceSkill(RPR.ShadowOfDeath)]
        [ParentCombo(RPR_ST_SliceCombo_GibbetGallows)]
        [CustomComboInfo("Gibbet and Gallows on SoD Option", "Adds Gibbet and Gallows to Shadow of Death as well if chosen in positional preferences.", RPR.JobID)]
        RPR_ST_SliceCombo_GibbetGallows_OnSoD = 12015,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Gluttony and Blood Stalk Option", "Adds Gluttony and Blood Stalk to the combo when target is afflicted with Death's Design, and the skills are off cooldown and < 50 soul.", RPR.JobID)]
        RPR_ST_SliceCombo_GluttonyBloodStalk = 12016,

        [ParentCombo(RPR_ST_SliceCombo_GibbetGallows_Communio)]
        [CustomComboInfo("Communio Movement Option", "Uses Shadow of Death instead of Communio when moving.", RPR.JobID)]
        RPR_ST_SliceCombo_GibbetGallows_Communio_Movement = 12017,

        [ParentCombo(RPR_ST_SliceCombo)]
        [CustomComboInfo("Level 90 Opener Option", "Adds the Level 90 Opener to the main combo. Choose which Opener to use below.", RPR.JobID, -1, "", "")]
        RPR_ST_SliceCombo_Opener = 12018,
        #endregion

        #region AoE (Scythe) Combo Section
        [ReplaceSkill(RPR.SpinningScythe)]
        [CustomComboInfo("Scythe Combo Feature", "Replace Spinning Scythe with its combo chain.\nIf all sub options are toggled will turn into a full one button rotation (Simple AoE)", RPR.JobID)]
        RPR_AoE_ScytheCombo = 12020,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Positional Preference", "Choose positional order for all positional related features.\nSupports turning Slice/Shadow of Death into all positionals or Slice and Shadow of Death being two separate positionals.", RPR.JobID)]
        ReaperPositionalConfigAoE = 12030,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Soul Scythe Option", "Adds Soul Scythe to AoE combo when Soul Gauge is 50 or less and current target is afflicted with Death's Design.", RPR.JobID)]
        RPR_AoE_ScytheCombo_SoulScythe = 12021,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Whorl Of Death Option", "Adds Whorl of Death to AoE combo if Death's Design is not present on current target, or is about to expire.", RPR.JobID)]
        RPR_AoE_ScytheCombo_WoD = 12022,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Guillotine Option", "Adds Guillotine to AoE combo when under Soul Reaver and when current target is afflicted with Death's Design.\nWill use Grim Reaping during Enshroud.", RPR.JobID)]
        RPR_AoE_ScytheCombo_Guillotine = 12023,

        [ParentCombo(RPR_AoE_ScytheCombo_Guillotine)]
        [CustomComboInfo("Grim Reaping Option", "Adds Grim Reaping to the to the AoE combo during Enshroud.", RPR.JobID)]
        RPR_AoE_ScytheCombo_Guillotine_GrimReaping = 12066,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Arcane Circle Option", "Adds Arcane Circle to AoE combo when off cooldown.", RPR.JobID)]
        RPR_AoE_ScytheCombo_ArcaneCircle = 12024,

        [ParentCombo(RPR_AoE_ScytheCombo_ArcaneCircle)]
        [CustomComboInfo("Plentiful Harvest Option", "Adds Plentiful Harvest to AoE combo when off cooldown and ready.", RPR.JobID)]
        RPR_AoE_ScytheCombo_PlentifulHarvest = 12025,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Enshroud Option", "Adds Enshroud to the AoE combo when at 50 Shroud and greater and when current target is afflicted with Death's Design.", RPR.JobID)]
        RPR_AoE_ScytheCombo_Enshroud = 12026,

        [ParentCombo(RPR_AoE_ScytheCombo_Guillotine)]
        [CustomComboInfo("Lemure's Scythe Option", "Adds Lemure's Scythe to the AoE combo when there are 2 Void Shrouds.", RPR.JobID, 1, "", "")]
        RPR_AoE_ScytheCombo_Lemure = 12027,

        [ParentCombo(RPR_AoE_ScytheCombo_Guillotine)]
        [CustomComboInfo("Communio Finisher Option", "Adds Communio to the AoE combo when there is 1 Lemure Shroud left.", RPR.JobID, 2, "", "")]
        RPR_AoE_ScytheCombo_Communio = 12028,

        [ParentCombo(RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Gluttony and Grim Swathe Option", "Adds Gluttony and Grim Swathe to the AoE combo when current target is afflicted with Death's Design and Soul Gauge < 50.", RPR.JobID)]
        RPR_AoE_ScytheCombo_GluttonyGrimSwathe = 12029,
        #endregion

        #region Blood Stalk/Grim Swathe Combo Section
        [ReplaceSkill(RPR.BloodStalk, RPR.GrimSwathe)]
        [CustomComboInfo("Gluttony on Blood Stalk/Grim Swathe Feature", "Blood Stalk and Grim Swathe will turn into Gluttony when it is available.", RPR.JobID)]
        RPR_GluttonyBloodSwathe = 12041,

        [ParentCombo(RPR_GluttonyBloodSwathe)]
        [CustomComboInfo("Gibbet and Gallows/Guillotine on Blood Stalk/Grim Swathe Feature", "Adds Gibbet and Gallows on Blood Stalk.\nAdds Guillotine on Grim Swathe.", RPR.JobID)]
        RPR_GluttonyBloodSwathe_BloodSwatheCombo = 12040,

        [ParentCombo(RPR_GluttonyBloodSwathe)]
        [CustomComboInfo("Enshroud Combo Option", "Adds Enshroud combo (Void/Cross Reaping, Communio, and Lemure's Slice) on Blood Stalk and Grim Swathe.", RPR.JobID)]
        RPR_GluttonyBloodSwathe_Enshroud = 12042,
        #endregion

        #region Miscellaneous
        [ReplaceSkill(RPR.ArcaneCircle)]
        [CustomComboInfo("Arcane Circle Harvest Feature", "Replaces Arcane Circle with Plentiful Harvest when you have stacks of Immortal Sacrifice.", RPR.JobID)]
        RPR_ArcaneCirclePlentifulHarvest = 12051,

        [ReplaceSkill(RPR.HellsEgress, RPR.HellsIngress)]
        [CustomComboInfo("Regress Feature", "Changes both Hell's Ingress and Hell's Egress turn into Regress when Threshold is active.", RPR.JobID)]
        RPR_Regress = 12052,

        [ReplaceSkill(RPR.Slice, RPR.SpinningScythe, RPR.ShadowOfDeath, RPR.Harpe, RPR.BloodStalk)]
        [CustomComboInfo("Soulsow Reminder Feature", "Adds Soulsow to the skills selected below when out of combat. \nWill also add Soulsow to Harpe when in combat and no target is selected.", RPR.JobID)]
        RPR_Soulsow = 12053,

        [ReplaceSkill(RPR.Harpe)]
        [ParentCombo(RPR_Soulsow)]
        [CustomComboInfo("Harpe Harvest Moon Feature", "Replaces Harpe with Harvest Moon when you are in combat with Soulsow active.", RPR.JobID)]
        RPR_Soulsow_HarpeHarvestMoon = 12054,

        [ReplaceSkill(RPR.Harpe, RPR.Slice)]
        [ParentCombo(RPR_Soulsow)]
        [CustomComboInfo("Enhanced Harpe Option", "Prevent Harvest Moon replacing Harpe when Enhanced Harpe is active.", RPR.JobID)]
        RPR_Soulsow_HarpeHarvestMoon_EnhancedHarpe = 12055,

        [ReplaceSkill(RPR.Harpe, RPR.Slice)]
        [ParentCombo(RPR_Soulsow)]
        [CustomComboInfo("Combat Harpe Option", "Prevent Harvest Moon replacing Harpe when you are not in combat.", RPR.JobID)]
        RPR_Soulsow_HarpeHarvestMoon_CombatHarpe = 12056,

        [ReplaceSkill(RPR.Enshroud)]
        [CustomComboInfo("Enshroud Protection Feature", "Turns Enshroud into Gibbet/Gallows to protect Soul Reaver waste.", RPR.JobID)]
        RPR_EnshroudProtection = 12057,

        [ParentCombo(RPR_EnshroudProtection)]
        [CustomComboInfo("Positional Preference", "Choose positional order for all positional related features.\nSupports turning Slice/Shadow of Death into all positionals or Slice and Shadow of Death being two separate positionals.", RPR.JobID)]
        RPR_EnshroudProtection_Positional = 12059,

        [ReplaceSkill(RPR.Gibbet, RPR.Gallows, RPR.Guillotine)]
        [CustomComboInfo("Communio on Gibbet/Gallows and Guillotine Feature", "Adds Communio to Gibbet/Gallows and Guillotine.", RPR.JobID)]
        RPR_CommunioOnGGG = 12058,

        [ParentCombo(RPR_CommunioOnGGG)]
        [CustomComboInfo("Lemure's Slice/Scythe Option", "Adds Lemure's Slice to Gibbet/Gallows and Lemure's Scythe to Guillotine.", RPR.JobID)]
        RPR_LemureOnGGG = 12060,

        [ReplaceSkill(RPR.Enshroud)]
        [CustomComboInfo("Enshroud to Communio Feature", "Turns Enshroud to Communio when available to use.", RPR.JobID)]
        RPR_EnshroudCommunio = 12069,

        [ReplaceSkill(RPR.Slice, RPR.ShadowOfDeath, RPR.Enshroud)]
        [CustomComboInfo("True North Feature", "Adds True North to Slice, Shadow of Death, Enshroud, and Blood Stalk when under Gluttony and if Gibbet/Gallows options are selected to replace those skills.", RPR.JobID, 0)]
        RPR_TrueNorth = 12061,

        [ReplaceSkill(RPR.Harpe)]
        [ParentCombo(RPR_Soulsow)]
        [CustomComboInfo("Soulsow Reminder during Combat", "Adds Soulsow to Harpe during combat when no target is selected.", RPR.JobID)]
        RPR_Soulsow_Combat = 12062,

        [ReplaceSkill(RPR.Gibbet, RPR.Gallows)]
        [CustomComboInfo("Dynamic True North Feature", "Adds True North to Slice before Gibbet/Gallows when you are not in the correct position for the enhanced potency bonus.", RPR.JobID)]
        RPR_TrueNorthDynamic = 12063,

        [ParentCombo(RPR_TrueNorthDynamic)]
        [CustomComboInfo("Hold True North for Gluttony Option", "Will hold the last charge of True North for use with Gluttony, even when out of position for Gibbet/Gallows potency bonuses.", RPR.JobID)]
        RPR_TrueNorthDynamic_HoldCharge = 12064,

        [Variant]
        [VariantParent(RPR_ST_SliceCombo, RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", RPR.JobID)]
        RPR_Variant_Cure = 12067,

        [Variant]
        [VariantParent(RPR_ST_SliceCombo, RPR_AoE_ScytheCombo)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", RPR.JobID)]
        RPR_Variant_Rampart = 12068,
        #endregion

        // Last value = 12069

        #endregion

        #region RED MAGE

        /* RDM Feature Numbering
        Numbering Scheme: 13[Section][Feature Number][Sub-Feature]
        Example: 13110 (Section 1: Openers, Feature Number 1, Sub-feature 0)
        New features should be added to the appropriate sections.
        If more than 10 sub features, use the next feature number if available
        The three digets after RDM.JobID can be used to reorder items in the list
        */
        [CustomComboInfo("鼓励", "Embolden", RDM.JobID)]
        RDM_Embolden = 13000,

        [ReplaceSkill(RDM.Verthunder, RDM.Verthunder3)]
        [CustomComboInfo("一键DPS", "", RDM.JobID)]
        RDM_ST_DPS = 13001,

        [ParentCombo(RDM_ST_DPS)]
        [CustomComboInfo("魔元化", "Manafication", RDM.JobID)]
        RDM_ST_DPS_Manafication = 13002,

        [ParentCombo(RDM_ST_DPS)]
        [CustomComboInfo("魔回刺", "Riposte", RDM.JobID)]
        RDM_ST_DPS_Riposte = 13003,

        [ParentCombo(RDM_ST_DPS)]
        [CustomComboInfo("短兵相接", "Corpsacorps", RDM.JobID)]
        RDM_ST_DPS_Corpsacorps = 13004, 

        [ParentCombo(RDM_ST_DPS)]
        [CustomComboInfo("交剑", "Engagement", RDM.JobID)]
        RDM_ST_DPS_Engagement = 13005,

        [ParentCombo(RDM_ST_DPS)]
        [CustomComboInfo("双目标模式", "Two target", RDM.JobID)]
        RDM_ST_DPS_TwoTarget = 13006,

        [ParentCombo(RDM_ST_DPS)]
        [CustomComboInfo("开场用摇荡", "Use Jolt before engaging combat", RDM.JobID)]
        RDM_ST_DPS_JoltFirst = 13007,

        [ReplaceSkill(RDM.Scatter, RDM.Impact)]
        [CustomComboInfo("AOE", "是否使用交剑，短兵相接可以在上面一键DPS里面设置", RDM.JobID)]
        RDM_AOE = 13100,

        [CustomComboInfo("ULK文理回复", "实验中。。。Eureka Option", RDM.JobID)]
        RDM_ULK = 13200,

        [CustomComboInfo("赤治疗", "Vercure, 低于40%用赤治疗", RDM.JobID)]
        RDM_Vercure = 13201,

        [ReplaceSkill(RDM.Acceleration)]
        [CustomComboInfo("促进-即刻-促进", "Acceleration-SwiftCast-Acceleration", RDM.JobID)]
        RDM_Acceleration_SwiftCast = 13300, 

        #endregion

        #region SAGE

        /* SGE Feature Numbering
        Numbering Scheme: 14[Feature][Option][Sub-Option]
        Example: 14110 (Feature Number 1, Option 1, no suboption)
        New features should be added to the appropriate sections.
        */

        #region DPS Feature
        [ReplaceSkill(SGE.Dosis, SGE.Dosis2, SGE.Dosis3)]
        [CustomComboInfo("一键DPS", "", SGE.JobID)]
        SGE_ST_DPS = 14000,

        [ParentCombo(SGE_ST_DPS)]
        [CustomComboInfo("DoT", "", SGE.JobID)]
        SGE_ST_DPS_EDosis = 14001,

        [ParentCombo(SGE_ST_DPS)]
        [CustomComboInfo("根素", "Weaves Rhizomata when Addersgall gauge falls below the specified value.", SGE.JobID)]
        SGE_ST_DPS_Rhizo = 14002,

        [ParentCombo(SGE_ST_DPS)]
        [CustomComboInfo("醒梦", "Lucid Dreaming", SGE.JobID)]
        SGE_ST_DPS_Lucid = 14003,

        [ReplaceSkill(SGE.Dyskrasia, SGE.Dyskrasia2)]
        [CustomComboInfo("AoE", "", SGE.JobID)]
        SGE_AoE_DPS = 14100,

        #endregion

        #region Heal Feature
        [ReplaceSkill(SGE.Diagnosis)]
        [CustomComboInfo("对人加血", "灵橡清汁-均匀诊断", SGE.JobID)]
        SGE_ST_Heal = 14200,

        [ParentCombo(SGE_ST_Heal)]
        [CustomComboInfo("康复", "Esuna", SGE.JobID)]
        SGE_ST_Heal_Esuna = 14201,

        [ReplaceSkill(SGE.Prognosis)]
        [CustomComboInfo("GCD群奶", "", SGE.JobID)]
        SGE_AoE_Heal = 14300,

        [ReplaceSkill(SGE.Ixochole)]
        [CustomComboInfo("oGCD群奶", "自生-寄生清汁", SGE.JobID)]
        SGE_Physis_Ixochole = 14400,

        [ReplaceSkill(SGE.Haima)]
        [CustomComboInfo("对T减伤", "混合-输血", SGE.JobID)]
        SGE_Krasis_Haima = 14401,

        [ReplaceSkill(SGE.Haima)]
        [CustomComboInfo("活化-魂灵风息", "", SGE.JobID)]
        SGE_Zoe_Pneuma = 14402, 

        [ReplaceSkill(SGE.Soteria)]
        [CustomComboInfo("拯救-心关", "Soteria-Kardia", SGE.JobID)]
        SGE_Kardia = 14403,

        [ReplaceSkill(SGE.Taurochole)]
        [CustomComboInfo("对T减伤", "白牛清汁-输血", SGE.JobID)]
        SGE_Taurochole_Haima = 14404, 

        [Variant]
        [VariantParent(SGE_ST_DPS_EDosis, SGE_AoE_DPS)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", SGE.JobID)]
        SGE_DPS_Variant_SpiritDart = 14920,

        [Variant]
        [VariantParent(SGE_ST_DPS, SGE_AoE_DPS)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", SGE.JobID)]
        SGE_DPS_Variant_Rampart = 14930,
        #endregion

        // Last value = 14930

        #endregion

        #region SAMURAI

        #region Overcap Features
        [ReplaceSkill(SAM.Kasha, SAM.Gekko, SAM.Yukikaze)]
        [CustomComboInfo("Samurai Overcap Feature", "Adds Shinten onto main combo when Kenki is at the selected amount or more", SAM.JobID)]
        SAM_ST_Overcap = 15001,

        [ReplaceSkill(SAM.Mangetsu, SAM.Oka)]
        [CustomComboInfo("Samurai AoE Overcap Feature", "Adds Kyuten onto main AoE combos when Kenki is at the selected amount or more", SAM.JobID)]
        SAM_AoE_Overcap = 15002,
        #endregion

        #region Main Combo (Gekko) Features
        [ReplaceSkill(SAM.Gekko)]
        [CustomComboInfo("Gekko Combo", "Replace Gekko with its combo chain.\nIf all sub options are selected will turn into a full one button rotation (Advanced Samurai)", SAM.JobID)]
        SAM_ST_GekkoCombo = 15003,

        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("Enpi Uptime Feature", "Replace main combo with Enpi when you are out of range.", SAM.JobID)]
        SAM_ST_GekkoCombo_RangedUptime = 15004,

        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("Yukikaze Combo on Main Combo", "Adds Yukikaze combo to main combo. Will add Yukikaze during Meikyo Shisui as well", SAM.JobID)]
        SAM_ST_GekkoCombo_Yukikaze = 15005,

        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("Kasha Combo on Main Combo", "Adds Kasha combo to main combo. Will add Kasha during Meikyo Shisui as well.", SAM.JobID)]
        SAM_ST_GekkoCombo_Kasha = 15006,

        [ConflictingCombos(SAM_GyotenYaten)]
        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("Level 90 Samurai Opener", "Adds the Level 90 Opener to the main combo.\nOpener triggered by using Meikyo Shisui before combat. If you have any Sen, Hagakure will be used to clear them.\nWill work at any levels of Kenki, requires 2 charges of Meikyo Shisui and all CDs ready. If conditions aren't met it will skip into the regular rotation. \nIf the Opener is interrupted, it will exit the opener via a Goken and a Kaeshi: Goken at the end or via the last Yukikaze. If the latter, CDs will be used on cooldown regardless of burst options.", SAM.JobID)]
        SAM_ST_GekkoCombo_Opener = 15007,

        [ConflictingCombos(SAM_GyotenYaten)]
        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("Filler Combo Feature", "Adds selected Filler combos to main combo at the appropriate time.\nChoose Skill Speed tier with Fuka buff below.\nWill disable if you die or if you don't activate the opener.", SAM.JobID)]
        SAM_ST_GekkoCombo_FillerCombos = 15008,

        #region CDs on Main Combo
        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("CDs on Main Combo", "Collection of CD features on main combo.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs = 15099,

        [ParentCombo(SAM_ST_GekkoCombo_CDs)]
        [CustomComboInfo("Ikishoten on Main Combo", "Adds Ikishoten when at or below 50 Kenki.\nWill dump Kenki at 10 seconds left to allow Ikishoten to be used.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_Ikishoten = 15009,

        [ParentCombo(SAM_ST_GekkoCombo_CDs)]
        [CustomComboInfo("Iaijutsu on Main Combo", "Adds Midare: Setsugekka, Higanbana, and Kaeshi: Setsugekka when ready and when you're not moving to main combo.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_Iaijutsu = 15010,

        #region Ogi Namikiri on Main Combo
        [ParentCombo(SAM_ST_GekkoCombo_CDs)]
        [CustomComboInfo("Ogi Namikiri on Main Combo", "Ogi Namikiri and Kaeshi: Namikiri when ready and when you're not moving to main combo.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_OgiNamikiri = 15011,

        [ParentCombo(SAM_ST_GekkoCombo_CDs_OgiNamikiri)]
        [CustomComboInfo("Ogi Namikiri Burst Feature", "Saves Ogi Namikiri for even minute burst windows.\nIf you don't activate the opener or die, Ogi Namikiri will instead be used on CD.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_OgiNamikiri_Burst = 15012,
        #endregion

        [ParentCombo(SAM_ST_GekkoCombo_CDs)]
        [CustomComboInfo("Meikyo Shisui on Main Combo", "Adds Meikyo Shisui to main combo when off cooldown.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_MeikyoShisui = 15013,

        #region Meikyo Shisui on Main Combo
        [ParentCombo(SAM_ST_GekkoCombo_CDs_MeikyoShisui)]
        [CustomComboInfo("Meikyo Shisui Burst Feature", "Saves Meikyo Shisui for burst windows.\nIf you don't activate the opener or die, Meikyo Shisui will instead be used on CD.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_MeikyoShisui_Burst = 15014,
        #endregion

        [ParentCombo(SAM_ST_GekkoCombo_CDs)]
        [CustomComboInfo("Shoha on Main Combo", "Adds Shoha to main combo when there are three meditation stacks.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_Shoha = 15015,

        [ConflictingCombos(SAM_Shinten_Shoha_Senei)]
        [ParentCombo(SAM_ST_GekkoCombo_CDs)]
        [CustomComboInfo("Senei on Main Combo", "Adds Senei to main combo when off cooldown and above 25 Kenki.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_Senei = 15016,

        [ParentCombo(SAM_ST_GekkoCombo_CDs_Senei)]
        [CustomComboInfo("Senei Burst Feature", "Saves Senei for even minute burst windows.\nIf you don't activate the opener or die, Senei will instead be used on CD.", SAM.JobID)]
        SAM_ST_GekkoCombo_CDs_Senei_Burst = 15017,

        [ParentCombo(SAM_ST_Overcap)]
        [CustomComboInfo("Execute Feature", "Adds Shinten to the main combo when Kenki > 25 and your current target is below the HP percentage threshold.", SAM.JobID)]
        SAM_ST_Execute = 15046,
        #endregion

        #endregion

        #region Yukikaze/Kasha Combos
        [ReplaceSkill(SAM.Yukikaze)]
        [CustomComboInfo("Yukikaze Combo", "Replace Yukikaze with its combo chain.", SAM.JobID)]
        SAM_ST_YukikazeCombo = 15018,

        [ReplaceSkill(SAM.Kasha)]
        [CustomComboInfo("Kasha Combo", "Replace Kasha with its combo chain.", SAM.JobID)]
        SAM_ST_KashaCombo = 15019,
        #endregion

        #region AoE Combos
        [ReplaceSkill(SAM.Mangetsu)]
        [CustomComboInfo("Mangetsu Combo", "Replace Mangetsu with its combo chain.\nIf all sub options are toggled will turn into a full one button AoE rotation.", SAM.JobID)]
        SAM_AoE_MangetsuCombo = 15020,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [ConflictingCombos(SAM_AoE_OkaCombo_TwoTarget)]
        [CustomComboInfo("Oka to Mangetsu Combo", "Adds Oka combo after Mangetsu combo loop.\nWill add Oka if needed during Meikyo Shisui.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_Oka = 15021,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Iaijutsu on Mangetsu Combo", "Adds Tenka Goken, Midare: Setsugekka, and Kaeshi: Goken when ready and when you're not moving to Mangetsu combo.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_TenkaGoken = 15022,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Ogi Namikiri on Mangetsu Combo", "Adds Ogi Namikiri and Kaeshi: Namikiri when ready and when you're not moving to Mangetsu combo.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_OgiNamikiri = 15023,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Shoha 2 on Mangetsu Combo", "Adds Shoha 2 when you have 3 meditation stacks to Mangetsu combo.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_Shoha2 = 15024,

        [ConflictingCombos(SAM_Kyuten_Shoha2_Guren)]
        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Guren on Mangetsu Combo", "Adds Guren when it's off cooldown and you have 25 Kenki to Mangetsu combo.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_Guren = 15025,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Meikyo Shisui on Mangetsu Combo", "Adds Meikyo Shisui to Mangetsu combo.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_MeikyoShisui = 15039,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Ikishoten on Mangetsu Combo", "Adds Ikishoten when at or below 50 Kenki.\nWill dump Kenki at 10 seconds left to allow Ikishoten to be used.", SAM.JobID)]
        SAM_AOE_GekkoCombo_CDs_Ikishoten = 15040,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Hagakure on Mangetsu Combo", "Adds Hagakure to Mangetsu combo when there are three Sen.", SAM.JobID)]
        SAM_AoE_MangetsuCombo_Hagakure = 15041,

        [ReplaceSkill(SAM.Oka)]
        [CustomComboInfo("Oka Combo", "Replace Oka with its combo chain.", SAM.JobID)]
        SAM_AoE_OkaCombo = 15026,

        [ParentCombo(SAM_AoE_OkaCombo)]
        [ConflictingCombos(SAM_AoE_MangetsuCombo_Oka)]
        [CustomComboInfo("Oka Two Target Rotation Feature", "Adds the Yukikaze combo, Mangetsu combo, Senei, Shinten, and Shoha to Oka combo.\nUsed for two targets only and when Lv86 and above.", SAM.JobID)]
        SAM_AoE_OkaCombo_TwoTarget = 150261,
        #endregion

        #region Cooldown Features
        [ReplaceSkill(SAM.MeikyoShisui)]
        [CustomComboInfo("Jinpu/Shifu Feature", "Replace Meikyo Shisui with Jinpu, Shifu, and Yukikaze depending on what is needed.", SAM.JobID)]
        SAM_JinpuShifu = 15027,
        #endregion

        #region Iaijutsu Features
        [ReplaceSkill(SAM.Iaijutsu)]
        [CustomComboInfo("Iaijutsu Features", "Collection of Iaijutsu Features.", SAM.JobID)]
        SAM_Iaijutsu = 15028,

        [ParentCombo(SAM_Iaijutsu)]
        [CustomComboInfo("Iaijutsu to Tsubame-Gaeshi", "Replace Iaijutsu with  Tsubame-gaeshi when Sen is empty.", SAM.JobID)]
        SAM_Iaijutsu_TsubameGaeshi = 15029,

        [ParentCombo(SAM_Iaijutsu)]
        [CustomComboInfo("Iaijutsu to Shoha", "Replace Iaijutsu with Shoha when meditation is 3.", SAM.JobID)]
        SAM_Iaijutsu_Shoha = 15030,

        [ParentCombo(SAM_Iaijutsu)]
        [CustomComboInfo("Iaijutsu to Ogi Namikiri", "Replace Iaijutsu with Ogi Namikiri and Kaeshi: Namikiri when buffed with Ogi Namikiri Ready.", SAM.JobID)]
        SAM_Iaijutsu_OgiNamikiri = 15031,
        #endregion

        #region Shinten Features
        [ReplaceSkill(SAM.Shinten)]
        [CustomComboInfo("Shinten to Shoha", "Replace Hissatsu: Shinten with Shoha when Meditation is full.", SAM.JobID)]
        SAM_Shinten_Shoha = 15032,

        [ConflictingCombos(SAM_ST_GekkoCombo_CDs_Senei)]
        [ParentCombo(SAM_Shinten_Shoha)]
        [CustomComboInfo("Shinten to Senei", "Replace Hissatsu: Shinten with Senei when its cooldown is up.", SAM.JobID)]
        SAM_Shinten_Shoha_Senei = 15033,
        #endregion

        #region Kyuten Features
        [ReplaceSkill(SAM.Kyuten)]
        [CustomComboInfo("Kyuten to Shoha II", "Replace Hissatsu: Kyuten with Shoha II when Meditation is full.", SAM.JobID)]
        SAM_Kyuten_Shoha2 = 15034,

        [ConflictingCombos(SAM_AoE_MangetsuCombo_Guren)]
        [ParentCombo(SAM_Kyuten_Shoha2)]
        [CustomComboInfo("Kyuten to Guren", "Replace Hissatsu: Kyuten with Guren when its cooldown is up.", SAM.JobID)]
        SAM_Kyuten_Shoha2_Guren = 15035,
        #endregion

        #region Other
        [ConflictingCombos(SAM_ST_GekkoCombo_Opener, SAM_ST_GekkoCombo_FillerCombos)]
        [ReplaceSkill(SAM.Gyoten)]
        [CustomComboInfo("Gyoten Feature", "Hissatsu: Gyoten becomes Yaten/Gyoten depending on the distance from your target.", SAM.JobID)]
        SAM_GyotenYaten = 15036,

        [ReplaceSkill(SAM.Ikishoten)]
        [CustomComboInfo("Ikishoten Namikiri Feature", "Replace Ikishoten with Ogi Namikiri and then Kaeshi Namikiri when available.\nIf you have full Meditation stacks, Ikishoten becomes Shoha while you have Ogi Namikiri ready.", SAM.JobID)]
        SAM_Ikishoten_OgiNamikiri = 15037,

        [ReplaceSkill(SAM.Gekko, SAM.Yukikaze, SAM.Kasha)]
        [CustomComboInfo("True North Feature", "Adds True North on all single target combos if Meikyo Shisui's buff is on you.", SAM.JobID)]
        SAM_TrueNorth = 15038,

        [ParentCombo(SAM_ST_GekkoCombo)]
        [CustomComboInfo("Combo Heals Option", "Adds Bloodbath and Second Wind to the combo, using them when below the HP Percentage threshold.", SAM.JobID)]
        SAM_ST_ComboHeals = 15043,

        [ParentCombo(SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Combo Heals Option", "Adds Bloodbath and Second Wind to the combo, using them when below the HP Percentage threshold.", SAM.JobID)]
        SAM_AoE_ComboHeals = 15045,

        [Variant]
        [VariantParent(SAM_ST_GekkoCombo, SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", SAM.JobID)]
        SAM_Variant_Cure = 15047,

        [Variant]
        [VariantParent(SAM_ST_GekkoCombo, SAM_AoE_MangetsuCombo)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", SAM.JobID)]
        SAM_Variant_Rampart = 15048,

        [CustomComboInfo("失传破甲", "LostRendArmor", SAM.JobID)]
        SAM_Bozja_LostRendArmor = 15049,

        [CustomComboInfo("失传暗杀", "LostAssassination", SAM.JobID)]
        SAM_Bozja_LostAssassination = 15050, 
        #endregion

        // Last value = 15050

        #endregion

        #region SCHOLAR

        #region DPS
        [ReplaceSkill(SCH.Ruin, SCH.Broil, SCH.Broil2, SCH.Broil3, SCH.Broil4, SCH.Bio, SCH.Bio2, SCH.Biolysis)]
        [CustomComboInfo("一键DPS", "", SCH.JobID, 1)]
        SCH_DPS = 16000,

        [ParentCombo(SCH_DPS)]
        [CustomComboInfo("DoT", "", SCH.JobID)]
        SCH_DPS_Bio = 16001,

        [ParentCombo(SCH_DPS)]
        [CustomComboInfo("转化", "Dissipation", SCH.JobID, 170)]
        SCH_DPS_Dissipation = 16002, 

        [ParentCombo(SCH_DPS)]
        [CustomComboInfo("连环计", "Chain Stratagem", SCH.JobID, 120)]
        SCH_DPS_ChainStrat = 16003,

        [ParentCombo(SCH_DPS)]
        [CustomComboInfo("能量吸收", "EnergyDrain", SCH.JobID, 120)]
        SCH_DPS_EnergyDrain = 16004,
        

        [ParentCombo(SCH_DPS)]
        [CustomComboInfo("醒梦", "Lucid Dreaming", SCH.JobID)]
        SCH_DPS_Lucid = 16006,

        [ReplaceSkill(SCH.ArtOfWar, SCH.ArtOfWarII)]
        [CustomComboInfo("AoE", "", SCH.JobID)]
        SCH_AoE = 16100,

        #endregion

        #region Healing

        [ReplaceSkill(SCH.Succor)]
        [CustomComboInfo("GCD群奶", "异想的幻光-鼓舞激励之策-展开战术-秘策-士气高扬之策", SCH.JobID)]
        SCH_AoE_Heal = 16200,

        [ReplaceSkill(SCH.Indomitability)]
        [CustomComboInfo("oGCD群奶", "仙光的低语-异想的祥光-不屈不挠之策", SCH.JobID)]
        SCH_Indomitability = 16202, 

        [ReplaceSkill(SCH.Physick)]
        [CustomComboInfo("GCD单奶", "生命活性法-鼓舞激励之策-医术", SCH.JobID)]
        SCH_ST_Heal = 16203,

        [ParentCombo(SCH_ST_Heal)]
        [CustomComboInfo("康复", "Esuna", SCH.JobID)]
        SCH_ST_Heal_Esuna = 16204,

        [ReplaceSkill(SCH.Aetherpact)]
        [CustomComboInfo("对T加血", "深谋远虑之策-以太契约", SCH.JobID)]
        SCH_Excogitation_Aetherpactl = 16205,

        [ReplaceSkill(SCH.Protraction)]
        [CustomComboInfo("对T减伤", "生命回生法-深谋远虑之策", SCH.JobID)]
        SCH_Protraction = 16206,
        #endregion

        #region Utilities
        [Variant]
        [VariantParent(SCH_DPS_Bio, SCH_AoE)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", SCH.JobID)]
        SCH_DPS_Variant_SpiritDart = 16700,

        [Variant]
        [VariantParent(SCH_DPS, SCH_AoE)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", SCH.JobID)]
        SCH_DPS_Variant_Rampart = 16800,

        #endregion

        // Last value = 16800

        #endregion

        #region SUMMONER

        [ReplaceSkill(SMN.Ruin, SMN.Ruin2, SMN.Outburst, SMN.Tridisaster)]
        [ConflictingCombos(SMN_Simple_Combo)]
        [CustomComboInfo("Advanced Summoner Feature", "Advanced combo features for a greater degree of customisation.\nAccommodates SpS builds.\nRuin III is left unchanged for mobility purposes.", SMN.JobID)]
        SMN_Advanced_Combo = 17000,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Demi Attacks Combo Option", "Adds Deathflare, Ahk Morn and Revelation to the single target and AoE combos.", SMN.JobID, 11, "", "")]
        SMN_Advanced_Combo_DemiSummons_Attacks = 17002,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Egi Attacks Combo Option", "Adds Gemshine and Precious Brilliance to the single target and AoE combos, respectively.", SMN.JobID, 4, "", "")]
        SMN_Advanced_Combo_EgiSummons_Attacks = 17004,

        [ReplaceSkill(SMN.Fester)]
        [CustomComboInfo("Energy Drain to Fester Feature", "Change Fester into Energy Drain when out of Aetherflow stacks.", SMN.JobID, 6, "", "")]
        SMN_EDFester = 17008,

        [ReplaceSkill(SMN.Painflare)]
        [CustomComboInfo("Energy Siphon to Painflare Feature", "Change Painflare into Energy Siphon when out of Aetherflow stacks.", SMN.JobID, 7, "", "")]
        SMN_ESPainflare = 17009,

        // BONUS TWEAKS
        [CustomComboInfo("Carbuncle Reminder Feature", "Replaces most offensive actions with Summon Carbuncle when it is not summoned.", SMN.JobID, 8, "", "")]
        SMN_CarbuncleReminder = 17010,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Ruin IV Combo Option", "Adds Ruin IV to the single target and AoE combos.\nUses when moving during Garuda Phase and you have no attunement, when moving during Ifrit phase, or when you have no active Egi or Demi summon.", SMN.JobID)]
        SMN_Advanced_Combo_Ruin4 = 17011,

        [ParentCombo(SMN_EDFester)]
        [CustomComboInfo("Ruin IV Fester Option", "Changes Fester to Ruin IV when out of Aetherflow stacks, Energy Drain is on cooldown, and Ruin IV is available.", SMN.JobID)]
        SMN_EDFester_Ruin4 = 17013,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Energy Attacks Combo Option", "Adds Energy Drain and Fester to the single target combo.\nAdds Energy Siphon and Painflare to the AoE combo.\nWill be used on cooldown.", SMN.JobID, 1, "", "")]
        SMN_Advanced_Combo_EDFester = 17014,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Egi Summons Combo Option", "Adds Egi summons to the single target and AoE combos.\nWill prioritise the Egi selected below.\nIf no option is selected, the feature will default to summoning Titan first.", SMN.JobID, 3, "", "")]
        SMN_DemiEgiMenu_EgiOrder = 17016,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Searing Light Combo Option", "Adds Searing Light to the single target and AoE combos.\nWill be used on cooldown.", SMN.JobID, 9, "", "")]
        SMN_SearingLight = 17018,

        [ParentCombo(SMN_SearingLight)]
        [CustomComboInfo("Searing Light Burst Option", "Casts Searing Light only during Demi phases.\nReflects Demi choice selected under 'Pooled oGCDs Option'.\nNot recommended for SpS Builds.", SMN.JobID, 0, "")]
        SMN_SearingLight_Burst = 170181, // Genesis, why must you be like this -K

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Demi Summons Combo Option", "Adds Demi summons to the single target and AoE combos.", SMN.JobID, 10, "", "")]
        SMN_Advanced_Combo_DemiSummons = 17020,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Swiftcast Egi Ability Option", "Uses Swiftcast during the selected Egi summon.", SMN.JobID, 8, "", "")]
        SMN_DemiEgiMenu_SwiftcastEgi = 17023,

        [CustomComboInfo("Astral Flow/Enkindle on Demis Feature", "Adds Enkindle Bahamut, Enkindle Phoenix and Astral Flow to their relevant summons.", SMN.JobID, 11, "", "")]
        SMN_DemiAbilities = 17024,

        [ParentCombo(SMN_Advanced_Combo_EDFester)]
        [CustomComboInfo("Pooled oGCDs Option", "Pools damage oGCDs for use inside the selected Demi phase while under the Searing Light buff.", SMN.JobID, 1, "", "")]
        SMN_DemiEgiMenu_oGCDPooling = 17025,

        [ConflictingCombos(ALL_Caster_Raise)]
        [CustomComboInfo("Alternative Raise Feature", "Changes Swiftcast to Raise when on cooldown.", SMN.JobID, 8, "", "")]
        SMN_Raise = 17027,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Rekindle Combo Option", "Adds Rekindle to the single target and AoE combos.", SMN.JobID, 13, "", "")]
        SMN_Advanced_Combo_DemiSummons_Rekindle = 17028,

        [ReplaceSkill(SMN.Ruin4)]
        [CustomComboInfo("Ruin III Mobility Feature", "Puts Ruin III on Ruin IV when you don't have Further Ruin.", SMN.JobID, 9, "", "")]
        SMN_RuinMobility = 17030,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Lucid Dreaming Option", "Adds Lucid Dreaming to the single target combo when MP falls below the set value.", SMN.JobID, 2, "", "")]
        SMN_Lucid = 17031,

        [CustomComboInfo("Egi Abilities on Summons Feature", "Adds Egi Abilities (Astral Flow) to Egi summons when ready.\nEgi abilities will appear on their respective Egi summon ability, as well as Titan.", SMN.JobID, 12, "", "")]
        SMN_Egi_AstralFlow = 17034,

        [ParentCombo(SMN_SearingLight)]
        [CustomComboInfo("Use only on Single Target combo", "Prevent this feature from applying to the AoE combo.", SMN.JobID, 2, "", "")]
        SMN_SearingLight_STOnly = 17036,

        [ParentCombo(SMN_DemiEgiMenu_oGCDPooling)]
        [CustomComboInfo("Use only on Single Target combo", "Prevent this feature from applying to the AoE combo.", SMN.JobID, 3, "", "")]
        SMN_DemiEgiMenu_oGCDPooling_Only = 17037,

        [ParentCombo(SMN_DemiEgiMenu_SwiftcastEgi)]
        [CustomComboInfo("Use only on Single Target combo", "Prevent this feature from applying to the AoE combo.", SMN.JobID, 2, "", "")]
        SMN_DemiEgiMenu_SwiftcastEgi_Only = 17038,

        [ParentCombo(SMN_ESPainflare)]
        [CustomComboInfo("Ruin IV Painflare Option", "Changes Painflare to Ruin IV when out of Aetherflow stacks, Energy Siphon is on cooldown, and Ruin IV is up.", SMN.JobID)]
        SMN_ESPainflare_Ruin4 = 17039,

        [ParentCombo(SMN_Advanced_Combo)]
        [CustomComboInfo("Add Egi Astralflow", "Choose which Egi Astralflows to add to the rotation.", SMN.JobID, 0, "", "")]
        SMN_ST_Egi_AstralFlow = 17048,

        [ConflictingCombos(SMN_Advanced_Combo)]
        [ReplaceSkill(SMN.Ruin, SMN.Ruin2, SMN.Outburst, SMN.Tridisaster)]
        [CustomComboInfo("Simple Summoner Feature", "General purpose one-button combo.\nBursts on Bahamut phase.\nSummons Titan, Garuda, then Ifrit.\nSwiftcasts on Slipstream unless drifted.", SMN.JobID, -1, "", "")]
        SMN_Simple_Combo = 17041,

        [ParentCombo(SMN_DemiEgiMenu_oGCDPooling)]
        [CustomComboInfo("Burst Delay Option", "Only follows Burst Delay settings for the opener burst.\nThis Option is for high SPS builds.", SMN.JobID, 2, "", "")]
        SMN_Advanced_Burst_Delay_Option = 17043,

        [ParentCombo(SMN_DemiEgiMenu_oGCDPooling)]
        [CustomComboInfo("Any Searing Burst Option", "Checks for any Searing light for bursting rather than just your own.\nUse this option if partied with multiple SMN and are worried about your Searing being overwritten.", SMN.JobID, 1, "", "")]
        SMN_Advanced_Burst_Any_Option = 17044,

        [Variant]
        [VariantParent(SMN_Simple_Combo, SMN_Advanced_Combo)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", SMN.JobID)]
        SMN_Variant_Rampart = 17045,

        [Variant]
        [VariantParent(SMN_Raise)]
        [CustomComboInfo("Raise Option", "Turn Swiftcast into Variant Raise whenever you have the Swiftcast buff.", SMN.JobID)]
        SMN_Variant_Raise = 17046,

        [Variant]
        [VariantParent(SMN_Simple_Combo, SMN_Advanced_Combo)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", SMN.JobID)]
        SMN_Variant_Cure = 17047,



        // Last value = 17047 (170181)

        #endregion

        #region WARRIOR

        [CustomComboInfo("狂暴/原初的解放", "Berserk/InnerRelease", WAR.JobID)]
        WAR_InnerRelease = 18000,

        [CustomComboInfo("蛮荒崩裂", "Primal Rend", WAR.JobID)]
        WAR_PrimalRend = 18001,

        [ReplaceSkill(WAR.HeavySwing)]
        [CustomComboInfo("一键DPS", "", WAR.JobID)]
        WAR_ST = 18100,

        [ParentCombo(WAR_ST)]
        [CustomComboInfo("猛攻", "Onslaught", WAR.JobID)]
        WAR_ST_Onslaught = 18101,

        [ParentCombo(WAR_ST)]
        [CustomComboInfo("强行用暴风斩", "", WAR.JobID)]
        WAR_ST_StormsPath = 18102,

        [ParentCombo(WAR_ST)]
        [CustomComboInfo("双目标模式", "TwoTarget mode", WAR.JobID)]
        WAR_ST_TwoTarget = 18103,

        [ReplaceSkill(WAR.Overpower)]
        [CustomComboInfo("AoE", "", WAR.JobID)]
        WAR_AoE_Overpower = 18200,

        [ParentCombo(WAR_AoE_Overpower)]
        [CustomComboInfo("危险使用战栗直觉泰然", "Heal in danger", WAR.JobID)]
        WAR_AoE_Overpower_HealInDanger = 18201,

        [Variant]
        [VariantParent(WAR_ST, WAR_AoE_Overpower)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", WAR.JobID)]
        WAR_Variant_SpiritDart = 18300,

        [Variant]
        [VariantParent(WAR_ST, WAR_AoE_Overpower)]
        [CustomComboInfo("Cure Option", "Use Variant Cure when HP is below set threshold.", WAR.JobID)]
        WAR_Variant_Cure = 18301,

        [Variant]
        [VariantParent(WAR_ST, WAR_AoE_Overpower)]
        [CustomComboInfo("Ultimatum Option", "Use Variant Ultimatum on cooldown.", WAR.JobID)]
        WAR_Variant_Ultimatum = 18302,

        [ReplaceSkill(WAR.Onslaught)]
        [CustomComboInfo("猛攻-蛮荒崩裂", "Onslaught-PrimalRend", WAR.JobID)]
        WAR_Onslaught_PrimalRend = 18400,

        // Last value = 18028

        #endregion

        #region WHITE MAGE

        [CustomComboInfo("神速咏唱", "PresenceOfMind", WHM.JobID, 1, "", "")]
        WHM_PresenceOfMind = 19000,

        #region Single Target DPS Feature

        [ReplaceSkill(WHM.Stone1, WHM.Stone2, WHM.Stone3, WHM.Stone4, WHM.Glare1, WHM.Glare3)]
        [CustomComboInfo("一键DPS", "", WHM.JobID, 1, "", "")]
        WHM_ST_MainCombo = 19001,

        [ParentCombo(WHM_ST_MainCombo)]
        [CustomComboInfo("DoT", "", WHM.JobID, 2, "", "")]
        WHM_ST_MainCombo_DoT = 19002,

        [ParentCombo(WHM_ST_MainCombo)]
        [CustomComboInfo("法令", "Assize", WHM.JobID, 4, "", "")]
        WHM_ST_MainCombo_Assize = 19003,

        [ParentCombo(WHM_ST_MainCombo)]
        [CustomComboInfo("苦难之心", "Afflatus Misery", WHM.JobID, 4, "", "")]
        WHM_ST_MainCombo_Misery = 19004,

        [ParentCombo(WHM_ST_MainCombo)]
        [CustomComboInfo("治疗百合防溢出", "Lily Overcap Protection Option, Adds Afflatus Rapture to the single target combo when at three Lilies.", WHM.JobID, 5, "", "")]
        WHM_ST_MainCombo_LilyOvercap = 19005,

        [ParentCombo(WHM_ST_MainCombo)]
        [CustomComboInfo("醒梦", "Lucid Dreaming", WHM.JobID, 6, "", "")]
        WHM_ST_MainCombo_Lucid = 19006,

        #endregion

        #region AoE DPS Feature

        [ReplaceSkill(WHM.Holy, WHM.Holy3)]
        [CustomComboInfo("AoE", "", WHM.JobID, 7, "", "")]
        WHM_AoE_DPS = 19100,

        [ParentCombo(WHM_AoE_DPS)]
        [CustomComboInfo("法令", "Assize", WHM.JobID, 9, "", "")]
        WHM_AoE_DPS_Assize = 19102,

        [ParentCombo(WHM_AoE_DPS)]
        [CustomComboInfo("苦难之心", "Afflatus Misery", WHM.JobID, 10, "", "")]
        WHM_AoE_DPS_Misery = 19103,

        [ParentCombo(WHM_AoE_DPS)]
        [CustomComboInfo("治疗百合防溢出", "Lily Overcap Protection Option, Adds Afflatus Rapture to the AoE combo when at three Lilies.", WHM.JobID, 11, "", "")]
        WHM_AoE_DPS_LilyOvercap = 19104,

        #endregion

        #region Heals Feature

        [ReplaceSkill(WHM.Medica)]
        [CustomComboInfo("GCD群奶", "GCD AoE Heals", WHM.JobID, 20, "", "")]
        WHM_AoEHeals = 19200,

        [ReplaceSkill(WHM.AfflatusRapture)]
        [CustomComboInfo("全大赦-狂喜之心", " ", WHM.JobID, 20, "", "")]
        WHM_AfflatusRapture = 19202,

        [ParentCombo(WHM_AfflatusRapture)]
        [CustomComboInfo("法令-全大赦-狂喜之心", " ", WHM.JobID)]
        WHM_AfflatusRapture_Assize = 19203,

        [ReplaceSkill(WHM.Cure)]
        [CustomComboInfo("GCD单奶", "安慰之心-神名-安慰之心-救疗-治疗", WHM.JobID, 20, "", "")]
        WHM_ST_Heals = 19204,

        [ParentCombo(WHM_ST_Heals)]
        [CustomComboInfo("康复", "Esuna Option", WHM.JobID)]
        WHM_STHeals_Esuna = 19205,

        [ReplaceSkill(WHM.Aquaveil)]
        [CustomComboInfo("对T减伤", "水流幕-神祝祷", WHM.JobID)]
        WHM_Aquaveil = 19207,

        #endregion

        [Variant]
        [VariantParent(WHM_ST_MainCombo_DoT, WHM_AoE_DPS)]
        [CustomComboInfo("Spirit Dart Option", "Use Variant Spirit Dart whenever the debuff is not present or less than 3s.", WHM.JobID)]
        WHM_DPS_Variant_SpiritDart = 19300,

        [Variant]
        [VariantParent(WHM_ST_MainCombo, WHM_AoE_DPS)]
        [CustomComboInfo("Rampart Option", "Use Variant Rampart on cooldown.", WHM.JobID)]
        WHM_DPS_Variant_Rampart = 19301,

        // Last value = 19301

        #endregion

        // Non-combat

        #region DOH

        // [CustomComboInfo("Placeholder", "Placeholder.", DOH.JobID)]
        // DohPlaceholder = 50001,

        #endregion

        #region DOL

        [ReplaceSkill(DOL.AgelessWords, DOL.SolidReason)]
        [CustomComboInfo("[BTN/MIN] Eureka Feature", "Replaces Ageless Words and Solid Reason with Wise to the World when available", DOL.JobID)]
        DOL_Eureka = 51001,

        [ReplaceSkill(DOL.ArborCall, DOL.ArborCall2, DOL.LayOfTheLand, DOL.LayOfTheLand2)]
        [CustomComboInfo("[BTN/MIN] Locate & Truth Feature", "Replaces Lay of the Lands or Arbor Calls with Prospect/Triangulate and Truth of Mountains/Forests if not active.", DOL.JobID)]
        DOL_NodeSearchingBuffs = 51012,

        [ReplaceSkill(DOL.Cast)]
        [CustomComboInfo("[FSH] Cast to Hook Feature", "Replaces Cast with Hook when fishing", DOL.JobID)]
        FSH_CastHook = 51002,

        [CustomComboInfo("[FSH] Diving Feature", "Replace fishing abilities with diving abilities when underwater", DOL.JobID)]
        FSH_Swim = 51008,

        [ReplaceSkill(DOL.Cast)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("[FSH] Cast to Gig Option", "Replaces Cast with Gig when diving.", DOL.JobID)]
        FSH_CastGig = 51003,

        [ReplaceSkill(DOL.SurfaceSlap)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Surface Slap to Veteran Trade Option", "Replaces Surface Slap with Veteran Trade when diving.", DOL.JobID)]
        FSH_SurfaceTrade = 51004,

        [ReplaceSkill(DOL.PrizeCatch)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Prize Catch to Nature's Bounty Option", "Replaces Prize Catch with Nature's Bounty when diving.", DOL.JobID)]
        FSH_PrizeBounty = 51005,

        [ReplaceSkill(DOL.Snagging)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Snagging to Salvage Option", "Replaces Snagging with Salvage when diving.", DOL.JobID)]
        FSH_SnaggingSalvage = 51006,

        [ReplaceSkill(DOL.CastLight)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Cast Light to Electric Current Option", "Replaces Cast Light with Electric Current when diving.", DOL.JobID)]
        FSH_CastLight_ElectricCurrent = 51007,

        [ReplaceSkill(DOL.Mooch, DOL.MoochII)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Mooch to Shark Eye Option", "Replaces Mooch with Shark Eye when diving.", DOL.JobID)]
        FSH_Mooch_SharkEye = 51009,

        [ReplaceSkill(DOL.FishEyes)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Fish Eyes to Vital Sight Option", "Replaces Fish Eyes with Vital Sight when diving.", DOL.JobID)]
        FSH_FishEyes_VitalSight = 51010,

        [ReplaceSkill(DOL.Chum)]
        [ParentCombo(FSH_Swim)]
        [CustomComboInfo("Chum to Baited Breath Option", "Replaces Chum with Baited Breath when diving.", DOL.JobID)]
        FSH_Chum_BaitedBreath = 51011,

        // Last value = 51011

        #endregion

        #endregion

        #region PvP Combos

        #region PvP GLOBAL FEATURES
        [SecretCustomCombo]
        [CustomComboInfo("Emergency Heals Feature", "Uses Recuperate when your HP is under the set threshold and you have sufficient MP.", ADV.JobID, 1)]
        PvP_EmergencyHeals = 1100000,

        [SecretCustomCombo]
        [CustomComboInfo("Emergency Guard Feature", "Uses Guard when your HP is under the set threshold.", ADV.JobID, 2)]
        PvP_EmergencyGuard = 1100010,

        [SecretCustomCombo]
        [CustomComboInfo("Quick Purify Feature", "Uses Purify when afflicted with any selected debuff.", ADV.JobID, 4)]
        PvP_QuickPurify = 1100020,

        [SecretCustomCombo]
        [CustomComboInfo("Prevent Mash Cancelling Feature", "Stops you cancelling your guard if you're pressing buttons quickly.", ADV.JobID, 3)]
        PvP_MashCancel = 1100030,

        // Last value = 1100030
        // Extra 0 on the end keeps things working the way they should be. Nothing to see here.

        #endregion

        #region ASTROLOGIAN
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Fall Malefic into an all-in-one damage button.", AST.JobID)]
        ASTPvP_Burst = 111000,

        [ParentCombo(ASTPvP_Burst)]
        [CustomComboInfo("Double Cast Option", "Adds Double Cast to Burst Mode.", AST.JobID)]
        ASTPvP_DoubleCast = 111001,

        [ParentCombo(ASTPvP_Burst)]
        [CustomComboInfo("Card Option", "Adds Drawing and Playing Cards to Burst Mode.", AST.JobID)]
        ASTPvP_Card = 111002,

        [SecretCustomCombo]
        [CustomComboInfo("Double Cast Heal Feature", "Adds Double Cast to Aspected Benefic.", AST.JobID)]
        ASTPvP_Heal = 111003,

        // Last value = 111003
        #endregion

        #region BLACK MAGE
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Fire and Blizzard into all-in-one damage buttons.", BLM.JobID)]
        BLMPvP_BurstMode = 112000,

        [ParentCombo(BLMPvP_BurstMode)]
        [SecretCustomCombo]
        [CustomComboInfo("Night Wing Option", "Adds Night Wing to Burst Mode.", BLM.JobID)]
        BLMPvP_BurstMode_NightWing = 112001,

        [ParentCombo(BLMPvP_BurstMode)]
        [SecretCustomCombo]
        [CustomComboInfo("Aetherial Manipulation Option", "Uses Aetherial Manipulation to gap close if Burst is off cooldown.", BLM.JobID)]
        BLMPvP_BurstMode_AetherialManip = 112002,

        // Last value = 112002

        #endregion

        #region BARD
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Powerful Shot into an all-in-one damage button.", BRDPvP.JobID)]
        BRDPvP_BurstMode = 113000,

        [SecretCustomCombo]
        [ParentCombo(BRDPvP_BurstMode)]
        [CustomComboInfo("Silent Nocturne Option", "Adds Silent Nocturne to Burst Mode.", BRD.JobID)]
        BRDPvP_SilentNocturne = 113001,

        // Last value = 113001

        #endregion

        #region DANCER
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Fountain Combo into an all-in-one damage button.", DNC.JobID)]
        DNCPvP_BurstMode = 114000,

        [SecretCustomCombo]
        [ParentCombo(DNCPvP_BurstMode)]
        [CustomComboInfo("Honing Dance Option", "Adds Honing Dance to the main combo when in melee range (respects global offset).\nThis option prevents early use of Honing Ovation!\nKeep Honing Dance bound to another key if you want to end early.", DNC.JobID)]
        DNCPvP_BurstMode_HoningDance = 114001,

        [SecretCustomCombo]
        [ParentCombo(DNCPvP_BurstMode)]
        [CustomComboInfo("Curing Waltz Option", "Adds Curing Waltz to the combo when available, and your HP is at or below the set percentage.", DNC.JobID)]
        DNCPvP_BurstMode_CuringWaltz = 114002,

        // Last value = 114002

        #endregion

        #region DARK KNIGHT
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Souleater Combo into an all-in-one damage button.", DRK.JobID)]
        DRKPvP_Burst = 115000,

        [SecretCustomCombo]
        [ParentCombo(DRKPvP_Burst)]
        [CustomComboInfo("Plunge Option", "Adds Plunge to Burst Mode.", DRK.JobID)]
        DRKPvP_Plunge = 115001,

        [SecretCustomCombo]
        [ParentCombo(DRKPvP_Plunge)]
        [CustomComboInfo("Melee Plunge Option", "Uses Plunge whilst in melee range, and not just as a gap-closer.", DRK.JobID)]
        DRKPvP_PlungeMelee = 115002,

        [SecretCustomCombo]
        [ParentCombo(DRKPvP_Burst)]
        [CustomComboInfo("Salted Earth Option", "Adds Salted Earth to Burst mode.", DRK.JobID)]
        DRKPvP_SaltedEarth = 115003,

        // Last value = 115002

        #endregion

        #region DRAGOON
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Using Elusive Jump turns Wheeling Thrust Combo into all-in-one burst damage button.", DRG.JobID)]
        DRGPvP_Burst = 116000,

        [ParentCombo(DRGPvP_Burst)]
        [CustomComboInfo("Geirskogul Option", "Adds Geirskogul to Burst Mode.", DRG.JobID)]
        DRGPvP_Geirskogul = 116001,

        [ParentCombo(DRGPvP_Geirskogul)]
        [CustomComboInfo("Nastrond Option", "Adds Nastrond to Burst Mode.", DRG.JobID)]
        DRGPvP_Nastrond = 116002,

        [ParentCombo(DRGPvP_Burst)]
        [CustomComboInfo("Horrid Roar Option", "Adds Horrid Roar to Burst Mode.", DRG.JobID)]
        DRGPvP_HorridRoar = 116003,

        [ParentCombo(DRGPvP_Burst)]
        [CustomComboInfo("Sustain Chaos Spring Option", "Adds Chaos Spring to Burst Mode when below the set HP percentage.", DRG.JobID)]
        DRGPvP_ChaoticSpringSustain = 116004,

        [ParentCombo(DRGPvP_Burst)]
        [CustomComboInfo("Wyrmwind Thrust Option", "Adds Wyrmwind Thrust to Burst Mode.", DRG.JobID)]
        DRGPvP_WyrmwindThrust = 116006,

        [ParentCombo(DRGPvP_Burst)]
        [CustomComboInfo("High Jump Weave Option", "Adds High Jump to Burst Mode.", DRG.JobID)]
        DRGPvP_HighJump = 116007,

        [ParentCombo(DRGPvP_Burst)]
        [CustomComboInfo("Elusive Jump Burst Protection Option", "Disables Elusive Jump if Burst is not ready.", DRG.JobID)]
        DRGPvP_BurstProtection = 116008,

        // Last value = 116008

        #endregion

        #region GUNBREAKER

        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Solid Barrel Combo into an all-in-one damage button.", GNB.JobID)]
        GNBPvP_Burst = 117000,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Double Down Option", "Adds Double Down to Burst Mode while under the No Mercy buff.", GNB.JobID)]
        GNBPvP_DoubleDown = 117001,

        [SecretCustomCombo]
        [CustomComboInfo("Gnashing Fang Continuation Feature", "Adds Continuation onto Gnashing Fang.", GNB.JobID)]
        GNBPvP_GnashingFang = 117002,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Draw And Junction Option", "Adds Draw And Junction to Burst Mode.", GNB.JobID)]
        GNBPvP_DrawAndJunction = 117003,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Gnashing Fang Option", "Adds Gnashing Fang to Burst Mode while under the No Mercy buff.", GNB.JobID)]
        GNBPvP_ST_GnashingFang = 117004,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Continuation Option", "Adds Continuation to Burst Mode.", GNB.JobID)]
        GNBPvP_ST_Continuation = 117005,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Rough Divide Option", "Weaves Rough Divide when No Mercy Buff is about to expire.", GNB.JobID)]
        GNBPvP_RoughDivide = 117006,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Junction Cast DPS Option", "Adds Junction Cast (DPS) to Burst Mode.", GNB.JobID)]
        GNBPvP_JunctionDPS = 117007,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Junction Cast Healer Option", "Adds Junction Cast (Healer) to Burst Mode.", GNB.JobID)]
        GNBPvP_JunctionHealer = 117008,

        [ParentCombo(GNBPvP_Burst)]
        [CustomComboInfo("Junction Cast Tank Option", "Adds Junction Cast (Tank) to Burst Mode.", GNB.JobID)]
        GNBPvP_JunctionTank = 117009,

        // Last value = 117009

        #endregion

        #region MACHINIST
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Blast Charge into an all-in-one damage button.", MCHPvP.JobID)]
        MCHPvP_BurstMode = 118000,

        [SecretCustomCombo]
        [ParentCombo(MCHPvP_BurstMode)]
        [CustomComboInfo("Alternate Drill Option", "Saves Drill for use after Wildfire.", MCHPvP.JobID)]
        MCHPvP_BurstMode_AltDrill = 118001,

        [SecretCustomCombo]
        [ParentCombo(MCHPvP_BurstMode)]
        [CustomComboInfo("Alternate Analysis Option", "Uses Analysis with Air Anchor instead of Chain Saw.", MCHPvP.JobID)]
        MCHPvP_BurstMode_AltAnalysis = 118002,

        // Last value = 118002

        #endregion

        #region MONK
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Phantom Rush Combo into an all-in-one damage button.", MNK.JobID)]
        MNKPvP_Burst = 119000,

        [ParentCombo(MNKPvP_Burst)]
        [SecretCustomCombo]
        [CustomComboInfo("Thunderclap Option", "Adds Thunderclap to Burst Mode when not buffed with Wind Resonance.", MNK.JobID)]
        MNKPvP_Burst_Thunderclap = 119001,

        [ParentCombo(MNKPvP_Burst)]
        [SecretCustomCombo]
        [CustomComboInfo("Riddle of Earth Option", "Adds Riddle of Earth and Earth's Reply to Burst Mode when in combat.", MNK.JobID)]
        MNKPvP_Burst_RiddleOfEarth = 119002,

        [ParentCombo(MNKPvP_Burst)]
        [SecretCustomCombo]
        [CustomComboInfo("Six-sided Star Option", "Adds Six-sided Star to Burst Mode.", MNK.JobID)]
        MNKPvP_Burst_SixSidedStar = 119003,

        // Last value = 119003

        #endregion

        #region NINJA
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Aeolian Edge Combo into an all-in-one damage button.", NINPvP.JobID)]
        NINPvP_ST_BurstMode = 120000,

        [SecretCustomCombo]
        [CustomComboInfo("AoE Burst Mode", "Turns Fuma Shuriken into an all-in-one AoE damage button.", NINPvP.JobID)]
        NINPvP_AoE_BurstMode = 120001,

        [ParentCombo(NINPvP_ST_BurstMode)]
        [SecretCustomCombo]
        [CustomComboInfo("Meisui Option", "Uses Three Mudra on Meisui when HP is under the set threshold.", NINPvP.JobID)]
        NINPvP_ST_Meisui = 120002,

        [ParentCombo(NINPvP_AoE_BurstMode)]
        [SecretCustomCombo]
        [CustomComboInfo("Meisui Option", "Uses Three Mudra on Meisui when HP is under the set threshold.", NINPvP.JobID)]
        NINPvP_AoE_Meisui = 120003,

        // Last value = 120003

        #endregion

        #region PALADIN
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Royal Authority Combo into an all-in-one damage button.", PLD.JobID)]
        PLDPvP_Burst = 121000,

        [ParentCombo(PLDPvP_Burst)]
        [CustomComboInfo("Shield Bash Option", "Adds Shield Bash to Burst Mode.", PLD.JobID)]
        PLDPvP_ShieldBash = 121001,

        [ParentCombo(PLDPvP_Burst)]
        [CustomComboInfo("Confiteor Option", "Adds Confiteor to Burst Mode.", PLD.JobID)]
        PLDPvP_Confiteor = 121002,

        // Last value = 121002

        #endregion

        #region REAPER
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Slice Combo into an all-in-one damage button.\nAdds Soul Slice to the main combo.", RPR.JobID)]
        RPRPvP_Burst = 122000,

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst)]
        [CustomComboInfo("Death Warrant Option", "Adds Death Warrant onto the main combo when Plentiful Harvest is ready to use, or when Plentiful Harvest's cooldown is longer than Death Warrant's.\nRespects Immortal Sacrifice Pooling Option.", RPR.JobID)]
        RPRPvP_Burst_DeathWarrant = 122001,

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst)]
        [CustomComboInfo("Plentiful Harvest Opener Option", "Starts combat with Plentiful Harvest to immediately begin Limit Break generation.", RPR.JobID)]
        RPRPvP_Burst_PlentifulOpener = 122002,

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst)]
        [CustomComboInfo("Plentiful Harvest + Immortal Sacrifice Pooling Option", "Pools stacks of Immortal Sacrifice before using Plentiful Harvest.\nAlso holds Plentiful Harvest if Death Warrant is on cooldown.\nSet the value to 3 or below to use Plentiful Harvest as soon as it's available.", RPR.JobID)]
        RPRPvP_Burst_ImmortalPooling = 122003,

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst)]
        [CustomComboInfo("Enshrouded Burst Option", "Adds Lemure's Slice to the main combo during the Enshroud burst phase.\nContains burst options.", RPR.JobID)]
        RPRPvP_Burst_Enshrouded = 122004,

        #region RPR Enshrouded Option
        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst_Enshrouded)]
        [CustomComboInfo("Enshrouded Death Warrant Option", "Adds Death Warrant onto the main combo during the Enshroud burst when available.", RPR.JobID)]
        RPRPvP_Burst_Enshrouded_DeathWarrant = 122005,

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst_Enshrouded)]
        [CustomComboInfo("Communio Finisher Option", "Adds Communio onto the main combo when you have 1 stack of Enshroud remaining.\nWill not trigger if you are moving.", RPR.JobID)]
        RPRPvP_Burst_Enshrouded_Communio = 122006,
        #endregion

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst)]
        [CustomComboInfo("Ranged Harvest Moon Option", "Adds Harvest Moon onto the main combo when you're out of melee range, the GCD is not rolling and it's available for use.", RPR.JobID)]
        RPRPvP_Burst_RangedHarvest = 122007,

        [SecretCustomCombo]
        [ParentCombo(RPRPvP_Burst)]
        [CustomComboInfo("Arcane Crest Option", "Adds Arcane Crest to the main combo when under the set HP perecentage.", RPR.JobID)]
        RPRPvP_Burst_ArcaneCircle = 122008,

        // Last value = 122008

        #endregion

        #region RED MAGE
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Verstone/Verfire into an all-in-one damage button.", RDMPvP.JobID)]
        RDMPvP_BurstMode = 123000,

        [SecretCustomCombo]
        [ParentCombo(RDMPvP_BurstMode)]
        [CustomComboInfo("No Frazzle Option", "Prevents Frazzle from being used in Burst Mode.", RDMPvP.JobID)]
        RDMPvP_FrazzleOption = 123001,

        // Last value = 123001

        #endregion

        #region SAGE
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Dosis III into an all-in-one damage button.", SGE.JobID)]
        SGEPvP_BurstMode = 124000,

        [ParentCombo(SGEPvP_BurstMode)]
        [CustomComboInfo("Pneuma Option", "Adds Pneuma to Burst Mode.", SGE.JobID)]
        SGEPvP_BurstMode_Pneuma = 124001,

        // Last value = 124001

        #endregion

        #region SAMURAI

        #region Burst Mode
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Adds Meikyo Shisui, Midare: Setsugekka, Ogi Namikiri, Kaeshi: Namikiri and Soten to Meikyo Shisui.\nWill only cast Midare: Setsugekka and Ogi Namikiri when you're not moving.\nWill not use if target is guarding.", SAM.JobID)]
        SAMPvP_BurstMode = 125000,

        [SecretCustomCombo]
        [ParentCombo(SAMPvP_BurstMode)]
        [CustomComboInfo("Chiten Option", "Adds Chiten to Burst Mode when in combat and HP is below 95%.", SAM.JobID)]
        SAMPvP_BurstMode_Chiten = 125001,

        [SecretCustomCombo]
        [ParentCombo(SAMPvP_BurstMode)]
        [CustomComboInfo("Mineuchi Option", "Adds Mineuchi to Burst Mode.", SAM.JobID)]
        SAMPvP_BurstMode_Stun = 125002,

        [SecretCustomCombo]
        [ParentCombo(SAMPvP_BurstMode)]
        [CustomComboInfo("Burst Mode on Kasha Combo Option", "Adds Burst Mode to Kasha Combo instead.", SAM.JobID, 1)]
        SAMPvP_BurstMode_MainCombo = 125003,
        #endregion

        #region Kasha Features
        [SecretCustomCombo]
        [CustomComboInfo("Kasha Combo Features", "Collection of Features for Kasha Combo.", SAM.JobID)]
        SAMPvP_KashaFeatures = 125004,

        [SecretCustomCombo]
        [ParentCombo(SAMPvP_KashaFeatures)]
        [CustomComboInfo("Soten Gap Closer Option", "Adds Soten to the Kasha Combo when out of melee range.", SAM.JobID)]
        SAMPvP_KashaFeatures_GapCloser = 125005,

        [SecretCustomCombo]
        [ParentCombo(SAMPvP_KashaFeatures)]
        [CustomComboInfo("AoE Melee Protection Option", "Makes the AoE combos unusable if not in melee range of target.", SAM.JobID)]
        SAMPvP_KashaFeatures_AoEMeleeProtection = 125006,
        #endregion

        // Last value = 125006

        #endregion

        #region SCHOLAR
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Broil IV into all-in-one damage button.", SCH.JobID)]
        SCHPvP_Burst = 126000,

        [ParentCombo(SCHPvP_Burst)]
        [CustomComboInfo("Expedient Option", "Adds Expedient to Burst Mode to empower Biolysis.", SCH.JobID)]
        SCHPvP_Expedient = 126001,

        [ParentCombo(SCHPvP_Burst)]
        [CustomComboInfo("Biolysis Option", "Adds Biolysis use on cooldown to Burst Mode.", SCH.JobID)]
        SCHPvP_Biolysis = 126002,

        [ParentCombo(SCHPvP_Burst)]
        [CustomComboInfo("Deployment Tactics Option", "Adds Deployment Tactics to Burst Mode when available.", SCH.JobID)]
        SCHPvP_DeploymentTactics = 126003,

        // Last value = 126003

        #endregion

        #region SUMMONER
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Ruin III into an all-in-one damage button.\nOnly uses Crimson Cyclone when in melee range.", SMNPvP.JobID)]
        SMNPvP_BurstMode = 127000,

        [SecretCustomCombo]
        [ParentCombo(SMNPvP_BurstMode)]
        [CustomComboInfo("Radiant Aegis Option", "Adds Radiant Aegis to Burst Mode when available, and your HP is at or below the set percentage.", SMNPvP.JobID)]
        SMNPvP_BurstMode_RadiantAegis = 127001,

        // Last value = 127001

        #endregion

        #region WARRIOR
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Heavy Swing into an all-in-one damage button.", WARPvP.JobID)]
        WARPvP_BurstMode = 128000,

        [SecretCustomCombo]
        [ParentCombo(WARPvP_BurstMode)]
        [CustomComboInfo("Bloodwhetting Option", "Allows use of Bloodwhetting any time, not just between GCDs.", WARPvP.JobID)]
        WARPvP_BurstMode_Bloodwhetting = 128001,

        [SecretCustomCombo]
        [ParentCombo(WARPvP_BurstMode)]
        [CustomComboInfo("Blota Option", "Adds Blota to Burst Mode when not in melee range.", WARPvP.JobID)]
        WARPvP_BurstMode_Blota = 128003,

        [SecretCustomCombo]
        [ParentCombo(WARPvP_BurstMode)]
        [CustomComboInfo("Primal Rend Option", "Adds Primal Rend to Burst Mode.", WARPvP.JobID)]
        WARPvP_BurstMode_PrimalRend = 128004,

        // Last value = 128002

        #endregion

        #region WHITE MAGE
        [SecretCustomCombo]
        [CustomComboInfo("Burst Mode", "Turns Glare into an all-in-one damage button.", WHM.JobID)]
        WHMPvP_Burst = 129000,

        [ParentCombo(WHMPvP_Burst)]
        [CustomComboInfo("Misery Option", "Adds Afflatus Misery to Burst Mode.", WHM.JobID)]
        WHMPvP_Afflatus_Misery = 129001,

        [ParentCombo(WHMPvP_Burst)]
        [CustomComboInfo("Miracle of Nature Option", "Adds Miracle of Nature to Burst Mode.", WHM.JobID)]
        WHMPvP_Mirace_of_Nature = 129002,

        [ParentCombo(WHMPvP_Burst)]
        [CustomComboInfo("Seraph Strike Option", "Adds Seraph Strike to Burst Mode.", WHM.JobID)]
        WHMPvP_Seraph_Strike = 129003,

        [SecretCustomCombo]
        [CustomComboInfo("Aquaveil Feature", "Adds Aquaveil to Cure II when available.", WHM.JobID)]
        WHMPvP_Aquaveil = 129004,

        [SecretCustomCombo]
        [CustomComboInfo("Cure III Feature", "Adds Cure III to Cure II when available.", WHM.JobID)]
        WHMPvP_Cure3 = 129005,

        // Last value = 129005

        #endregion

        #endregion
    }
}

