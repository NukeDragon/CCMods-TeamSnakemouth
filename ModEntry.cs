using HarmonyLib;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using NukeDragon.TeamSnakemouth.Cards;
using NukeDragon.TeamSnakemouth.Features;
using NukeDragon.TeamSnakemouth.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using MGColor = Microsoft.Xna.Framework.Color;

namespace NukeDragon.TeamSnakemouth;

public sealed class ModEntry : SimpleMod
{
  internal static ModEntry Instance { get; private set; } = null!;
  internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
  internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

  internal Harmony Harmony { get; }
  internal HookManager<ISnakemouthHook> HookManager { get; }
  internal IKokoroApi KokoroApi { get; }

  internal ISpriteEntry TPCost { get; }
  internal ISpriteEntry TPCostOff { get; }
  internal ISpriteEntry FiveTPCost { get; }
  internal ISpriteEntry FiveTPCostOff { get; }

  internal ISpriteEntry ExplorersPermitTPSprite { get; }
  internal ISpriteEntry ExplorersPermitSprite { get; }
  internal ISpriteEntry QueensPermitTPSprite { get; }
  internal ISpriteEntry QueensPermitSprite { get; }
  internal ISpriteEntry TardigradeShieldSprite { get; }
  internal ISpriteEntry ExtraFreezeSprite { get; }
  internal ISpriteEntry FrostbiteSprite { get; }
  internal ISpriteEntry PoisonAttackerSprite { get; }
  internal ISpriteEntry MeditationSprite { get; }
  internal ISpriteEntry PrayerSprite { get; }
  internal ISpriteEntry ReflectionSprite { get; }
  internal ISpriteEntry ElectricNeedlesSprite {  get; }
  internal ISpriteEntry EternalVenomSprite {  get; }
  internal ISpriteEntry FreezeResistanceSprite { get; }
  internal ISpriteEntry PoisonNeedlesSprite { get; }
  internal ISpriteEntry TPSaverSprite { get; }
  internal ISpriteEntry AntlionJawsSprite { get; }
  internal ISpriteEntry DefenseExchangeSprite { get; }

  internal IStatusEntry TP_Status { get; }
  internal IStatusEntry Frost_Status { get; }
  internal IStatusEntry Poison_Status { get; }
  internal IStatusEntry Taunted_Status { get; }
  internal IStatusEntry Concentration_Status { get; }
  internal IStatusEntry Permafrost_Status { get; }
  internal ISpriteEntry FrozenModifierSprite { get; }

  internal ISpriteEntry Icicle_Sprite { get; }
  internal ISpriteEntry Icicle_Icon { get; }

  internal Dictionary<Deck, IStatusEntry> Inspired_Status_Dictionary { get; }
  internal ISpriteEntry InspirationTemplate {  get; }
  internal Dictionary<Deck, IStatusEntry> Charged_Status_Dictionary { get;  }
  internal ISpriteEntry ChargedTemplate { get; }

  internal ISpriteEntry ChargeTeamActionSprite { get; }

  internal ISpriteEntry DefaultCardArt { get; }

  internal ISpriteEntry Vi_CardFrame { get; }
  internal ISpriteEntry Vi_Panel { get; }
  internal ISpriteEntry Vi_Gameover { get; }
  internal ISpriteEntry Vi_Neutral_0 { get; }
  internal ISpriteEntry Vi_Neutral_1 { get; }
  internal ISpriteEntry Vi_Neutral_2 { get; }
  internal ISpriteEntry Vi_Neutral_3 { get; }
  internal ISpriteEntry Vi_Neutral_4 { get; }
  internal ISpriteEntry Vi_Mini { get; }
  internal ISpriteEntry Vi_Squint_0 { get; }
  internal ISpriteEntry Vi_Squint_1 { get; }
  internal ISpriteEntry Vi_Squint_2 { get; }
  internal ISpriteEntry Vi_Squint_3 { get; }
  internal ISpriteEntry Vi_Squint_4 { get; }
  internal ISpriteEntry Vi_Angy_0 { get; }
  internal ISpriteEntry Vi_Angy_1 { get; }
  internal ISpriteEntry Vi_Angy_2 { get; }
  internal ISpriteEntry Vi_Angy_3 { get; }
  internal ISpriteEntry Vi_Angy_4 { get; }
  internal ISpriteEntry Vi_Angy_5 { get; }
  internal ISpriteEntry Vi_Angy_6 { get; }
  internal ISpriteEntry Vi_Angy_7 { get; }
  internal ISpriteEntry Vi_Happy_0 { get; }
  internal ISpriteEntry Vi_Happy_1 { get; }
  internal ISpriteEntry Vi_Happy_2 { get; }
  internal ISpriteEntry Vi_Happy_3 { get; }
  internal ISpriteEntry Vi_Happy_4 { get; }
  internal ISpriteEntry Vi_Sad_0 { get; }
  internal ISpriteEntry Vi_Sad_1 { get; }
  internal ISpriteEntry Vi_Sad_2 { get; }
  internal ISpriteEntry Vi_Sad_3 { get; }
  internal ISpriteEntry Vi_Sad_4 { get; }
  internal ISpriteEntry Vi_Shocked_0 { get; }
  internal ISpriteEntry Vi_Shocked_1 { get; }
  internal ISpriteEntry Vi_Shocked_2 { get; }
  internal ISpriteEntry Vi_Shocked_3 { get; }
  internal ISpriteEntry Vi_Shocked_4 { get; }
  internal ISpriteEntry Vi_Unamused_0 { get; }
  internal ISpriteEntry Vi_Unamused_1 { get; }
  internal ISpriteEntry Vi_Unamused_2 { get; }
  internal ISpriteEntry Vi_Unamused_3 { get; }
  internal ISpriteEntry Vi_Unamused_4 { get; }
  internal ICharacterEntry Vi_Char {  get; }
  internal IDeckEntry Vi_Deck { get; }

  internal ISpriteEntry Leif_CardFrame { get; }
  internal ISpriteEntry Leif_Panel { get; }
  internal ISpriteEntry Leif_Gameover { get; }
  internal ISpriteEntry Leif_Neutral_0 { get; }
  internal ISpriteEntry Leif_Neutral_1 { get; }
  internal ISpriteEntry Leif_Neutral_2 { get; }
  internal ISpriteEntry Leif_Neutral_3 { get; }
  internal ISpriteEntry Leif_Mini { get; }
  internal ISpriteEntry Leif_Squint_0 { get; }
  internal ISpriteEntry Leif_Squint_1 { get; }
  internal ISpriteEntry Leif_Squint_2 { get; }
  internal ISpriteEntry Leif_Squint_3 { get; }
  internal ISpriteEntry Leif_Thinking_0 { get; }
  internal ISpriteEntry Leif_Thinking_1 { get; }
  internal ISpriteEntry Leif_Thinking_2 { get; }
  internal ISpriteEntry Leif_Thinking_3 { get; }
  internal ISpriteEntry Leif_Hurt_0 { get; }
  internal ISpriteEntry Leif_Hurt_1 { get; }
  internal ISpriteEntry Leif_Hurt_2 { get; }
  internal ISpriteEntry Leif_Hurt_3 { get; }
  internal ISpriteEntry Leif_Ready_0 { get; }
  internal ISpriteEntry Leif_Ready_1 { get; }
  internal ISpriteEntry Leif_Ready_2 { get; }
  internal ISpriteEntry Leif_Ready_3 { get; }
  internal ICharacterEntry Leif_Char { get; }
  internal IDeckEntry Leif_Deck { get; }

  internal ISpriteEntry Kabbu_CardFrame { get; }
  internal ISpriteEntry Kabbu_Panel { get; }
  internal ISpriteEntry Kabbu_Gameover { get; }
  internal ISpriteEntry Kabbu_Neutral_0 { get; }
  internal ISpriteEntry Kabbu_Neutral_1 { get; }
  internal ISpriteEntry Kabbu_Neutral_2 { get; }
  internal ISpriteEntry Kabbu_Neutral_3 { get; }
  internal ISpriteEntry Kabbu_Mini { get; }
  internal ISpriteEntry Kabbu_Squint_0 { get; }
  internal ISpriteEntry Kabbu_Squint_1 { get; }
  internal ISpriteEntry Kabbu_Squint_2 { get; }
  internal ISpriteEntry Kabbu_Squint_3 { get; }
  internal ISpriteEntry Kabbu_Angy_0 { get; }
  internal ISpriteEntry Kabbu_Angy_1 { get; }
  internal ISpriteEntry Kabbu_Angy_2 { get; }
  internal ISpriteEntry Kabbu_Angy_3 { get; }
  internal ISpriteEntry Kabbu_Explains_0 { get; }
  internal ISpriteEntry Kabbu_Explains_1 { get; }
  internal ISpriteEntry Kabbu_Explains_2 { get; }
  internal ISpriteEntry Kabbu_Explains_3 { get; }
  internal ISpriteEntry Kabbu_Sad_0 { get; }
  internal ISpriteEntry Kabbu_Sad_1 { get; }
  internal ISpriteEntry Kabbu_Sad_2 { get; }
  internal ISpriteEntry Kabbu_Sad_3 { get; }
  internal ISpriteEntry Kabbu_Thinking_0 { get; }
  internal ISpriteEntry Kabbu_Thinking_1 { get; }
  internal ISpriteEntry Kabbu_Thinking_2 { get; }
  internal ISpriteEntry Kabbu_Thinking_3 { get; }
  internal ICharacterEntry Kabbu_Char { get; }
  internal IDeckEntry Kabbu_Deck { get; }

  public PDamMod frozen = (PDamMod)195;
  public MissileType ice = (MissileType)195;

  internal static IReadOnlyList<Type> Vi_StarterCard_Types { get; } = [
    typeof(TornadoBarrage),
    typeof(PoisonSting)
  ];
  internal static IReadOnlyList<Type> Vi_OtherCard_Types { get; } = [
    typeof(SecretStash),
    typeof(CrisbeeDonut),
    typeof(SpoiledStash),
    typeof(PoisonPincer),
    typeof(PoisonBomb),
    typeof(CycloneBarrage),
    typeof(TangyCarpaccio),
    typeof(MiracleShake),
    typeof(PoisonDart),
    typeof(PrepareDarts),
    typeof(HardCharge),
    typeof(OverextendedBlast),
    typeof(VenomBurst),
    typeof(HurricaneBlast)
  ];
  internal static IReadOnlyList<Type> Leif_StarterCard_Types { get; } = [
    typeof(IcicleShot),
    typeof(Defrost)
  ];
  internal static IReadOnlyList<Type> Leif_OtherCard_Types { get; } = [
    typeof(Empower),
    typeof(FrozenCoffin),
    typeof(MagicFocus),
    typeof(Concentrate),
    typeof(Icicle),
    typeof(IceRain),
    typeof(Rejuvenate),
    typeof(FrostEjection),
    typeof(Permafrost),
    typeof(BubbleShield),
    typeof(Energize),
    typeof(ChargeUp),
    typeof(TeamCharge)
  ];
  internal static IReadOnlyList<Type> Kabbu_StarterCard_Types { get; } = [
    typeof(PiercingBlow),
    typeof(Taunt)
  ];
  internal static IReadOnlyList<Type> Kabbu_OtherCard_Types { get; } = [
    typeof(RallyCry),
    typeof(Meditate),
    typeof(BraceSelf),
    typeof(RecoveryShot),
    typeof(Sturdy),
    typeof(PepTalk),
    typeof(ExperienceShot),
    typeof(HeavyBlow),
    typeof(DeepTaunt),
    typeof(PebbleToss),
    typeof(BoulderToss),
    typeof(Barricade),
    typeof(DashThrough),
    typeof(TeamPlan)
  ];

  internal static IEnumerable<Type> Snakemouth_AllCard_Types
      => Vi_OtherCard_Types.Concat(Vi_StarterCard_Types).Concat(Leif_StarterCard_Types).Concat(Kabbu_StarterCard_Types).Concat(Kabbu_OtherCard_Types).Concat(Leif_OtherCard_Types);

  internal static IReadOnlyList<Type> Snakemouth_Starter_Artifact_Types { get; } = [
     typeof(ExplorersPermit)
 ];
  internal static IReadOnlyList<Type> Snakemouth_Artifact_Types { get; } = [
     typeof(QueensPermit)
 ];
  internal static IReadOnlyList<Type> Vi_Artifact_Types { get; } = [
    typeof(PoisonAttacker),
    typeof(EternalVenom),
    typeof(ElectricNeedles),
    typeof(PoisonNeedles),
  ];
  internal static IReadOnlyList<Type> Leif_Artifact_Types { get; } = [
    typeof(Frostbite),
    typeof(ExtraFreeze),
    typeof(FreezeResistance),
    typeof(TPSaver)
  ];
  internal static IReadOnlyList<Type> Kabbu_Artifact_Types { get; } = [
   typeof(TardigradeShield),
   typeof(Reflection),
    typeof(AntlionJaws),
    typeof(DefenseExchange)
];
  internal static IEnumerable<Type> Snakemouth_AllArtifact_Types
      => Snakemouth_Artifact_Types.Concat(Snakemouth_Starter_Artifact_Types).Concat(Kabbu_Artifact_Types).Concat(Leif_Artifact_Types).Concat(Vi_Artifact_Types);

  public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
  {
    Instance = this;
    Harmony = new(package.Manifest.UniqueName);
    KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
    HookManager = new();

    ArtifactBlacklist.ApplyPatches(Harmony);
    CharacterBabblePatch.ApplyPatches(Harmony);
    MapExitPatches.ApplyPatches(Harmony);
    FrostManager.ApplyPatches(Harmony);
    FrozenManager.ApplyPatches(Harmony);;
    AAttackPatches.ApplyPatches(Harmony);
    CardPatches.ApplyPatches(Harmony);
    BeforeAfterTurnPatch.ApplyPatches(Harmony);
    PoisonManager.ApplyPatches(Harmony);
    ChargeManager.ApplyPatches(Harmony);
    PDamModPatches.ApplyPatches(Harmony);
    MGPatches.ApplyPatches(Harmony);
    IceMissileManager.ApplyPatches(Harmony);
    _ = new FlashFreezeTrigger();
    _ = new InspirationManager();
    _ = new ChargeManager();

    this.AnyLocalizations = new JsonLocalizationProvider(
        tokenExtractor: new SimpleLocalizationTokenExtractor(),
        localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
    );
    this.Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
        new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyLocalizations)
    );

    TPCost = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/TPCost.png"));
    TPCostOff = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/TPCostOff.png"));
    FiveTPCost = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/5TPCost.png"));
    FiveTPCostOff = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/5TPCostOff.png"));
    InspirationTemplate = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/InspiredTemplate.png"));
    ChargedTemplate = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/ChargeTemplate.png"));
    ChargeTeamActionSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/actionicons/chargeteam_icon.png"));
    ExplorersPermitTPSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/ExplorerPermitTP.png"));
    ExplorersPermitSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/ExplorerPermit.png"));
    QueensPermitTPSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/QueensPermitTP.png"));
    QueensPermitSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/QueensPermit.png"));
    TardigradeShieldSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/TardigradeShield.png"));
    ExtraFreezeSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/ExtraFreeze.png"));
    FrostbiteSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/Frostbite.png"));
    PoisonAttackerSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/PoisonAttacker.png"));
    MeditationSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/Meditation.png"));
    PrayerSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/Prayer.png"));
    ReflectionSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/Reflection.png"));
    ElectricNeedlesSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/ElectricNeedles.png"));
    EternalVenomSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/EternalVenom.png"));
    FreezeResistanceSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/FreezeResistance.png"));
    PoisonNeedlesSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/PoisonNeedles.png"));
    TPSaverSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/TPSaver.png"));
    AntlionJawsSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/AntlionJaws.png"));
    DefenseExchangeSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/DefenseExchange.png"));
    FrozenModifierSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/modifiers/frozen_modifier_icon.png"));
    Icicle_Sprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/drones/missile_ice.png"));
    Icicle_Icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/actionicons/missile_ice.png"));
    DefaultCardArt = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/default_cardbackground.png"));
    Vi_CardFrame = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_cardframe.png"));
    Vi_Panel = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_panel.png"));
    Vi_Gameover = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_gameover.png"));
    Vi_Neutral_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_neutral_0.png"));
    Vi_Neutral_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_neutral_1.png"));
    Vi_Neutral_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_neutral_2.png"));
    Vi_Neutral_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_neutral_3.png"));
    Vi_Neutral_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_neutral_4.png"));
    Vi_Mini = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_mini.png"));
    Vi_Squint_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_squint_0.png"));
    Vi_Squint_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_squint_1.png"));
    Vi_Squint_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_squint_2.png"));
    Vi_Squint_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_squint_3.png"));
    Vi_Squint_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_squint_4.png"));
    Vi_Angy_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_0.png"));
    Vi_Angy_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_1.png"));
    Vi_Angy_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_2.png"));
    Vi_Angy_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_3.png"));
    Vi_Angy_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_4.png"));
    Vi_Angy_5 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_5.png"));
    Vi_Angy_6 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_6.png"));
    Vi_Angy_7 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_angy_7.png"));
    Vi_Happy_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_happy_0.png"));
    Vi_Happy_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_happy_1.png"));
    Vi_Happy_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_happy_2.png"));
    Vi_Happy_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_happy_3.png"));
    Vi_Happy_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_happy_4.png"));
    Vi_Sad_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_sad_0.png"));
    Vi_Sad_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_sad_1.png"));
    Vi_Sad_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_sad_2.png"));
    Vi_Sad_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_sad_3.png"));
    Vi_Sad_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_sad_4.png"));
    Vi_Shocked_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_shocked_0.png"));
    Vi_Shocked_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_shocked_1.png"));
    Vi_Shocked_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_shocked_2.png"));
    Vi_Shocked_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_shocked_3.png"));
    Vi_Shocked_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_shocked_4.png"));
    Vi_Unamused_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_unamused_0.png"));
    Vi_Unamused_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_unamused_1.png"));
    Vi_Unamused_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_unamused_2.png"));
    Vi_Unamused_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_unamused_3.png"));
    Vi_Unamused_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/vi/violet_unamused_4.png"));
    Leif_CardFrame = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_cardframe.png"));
    Leif_Panel = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_panel.png"));
    Leif_Gameover = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_gameover.png"));
    Leif_Neutral_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_neutral_0.png"));
    Leif_Neutral_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_neutral_1.png"));
    Leif_Neutral_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_neutral_2.png"));
    Leif_Neutral_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_neutral_3.png"));
    Leif_Mini = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_mini.png"));
    Leif_Squint_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_squint_0.png"));
    Leif_Squint_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_squint_1.png"));
    Leif_Squint_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_squint_2.png"));
    Leif_Squint_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_squint_3.png"));
    Leif_Thinking_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_thinky_0.png"));
    Leif_Thinking_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_thinky_1.png"));
    Leif_Thinking_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_thinky_2.png"));
    Leif_Thinking_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_thinky_3.png"));
    Leif_Hurt_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_hurt_0.png"));
    Leif_Hurt_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_hurt_1.png"));
    Leif_Hurt_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_hurt_2.png"));
    Leif_Hurt_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_hurt_3.png"));
    Leif_Ready_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_ready_0.png"));
    Leif_Ready_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_ready_1.png"));
    Leif_Ready_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_ready_2.png"));
    Leif_Ready_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/leif/leif_ready_3.png"));
    Kabbu_CardFrame = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_cardframe.png"));
    Kabbu_Panel = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_panel.png"));
    Kabbu_Gameover = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_gameover.png"));
    Kabbu_Neutral_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_neutral_0.png"));
    Kabbu_Neutral_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_neutral_1.png"));
    Kabbu_Neutral_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_neutral_2.png"));
    Kabbu_Neutral_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_neutral_3.png"));
    Kabbu_Mini = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_mini.png"));
    Kabbu_Squint_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_squint_0.png"));
    Kabbu_Squint_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_squint_1.png"));
    Kabbu_Squint_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_squint_2.png"));
    Kabbu_Squint_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_squint_3.png"));
    Kabbu_Angy_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_angy_0.png"));
    Kabbu_Angy_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_angy_1.png"));
    Kabbu_Angy_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_angy_2.png"));
    Kabbu_Angy_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_angy_3.png"));
    Kabbu_Explains_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_explains_0.png"));
    Kabbu_Explains_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_explains_1.png"));
    Kabbu_Explains_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_explains_2.png"));
    Kabbu_Explains_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_explains_3.png"));
    Kabbu_Sad_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_sad_0.png"));
    Kabbu_Sad_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_sad_1.png"));
    Kabbu_Sad_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_sad_2.png"));
    Kabbu_Sad_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_sad_3.png"));
    Kabbu_Thinking_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_thinking_0.png"));
    Kabbu_Thinking_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_thinking_1.png"));
    Kabbu_Thinking_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_thinking_2.png"));
    Kabbu_Thinking_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/kabbu/kabbu_thinking_3.png"));

    DB.missiles[ice] = Icicle_Sprite.Sprite;

    Missile.missileData.Add(ice, new Missile.MissileMetadata()
    {
      key = "missile_ice",
      exhaustColor = new Color("ffffff"),
      icon = Icicle_Icon.Sprite,
      baseDamage = 2
    });

    TP_Status = helper.Content.Statuses.RegisterStatus("TP", new()
    {
      Definition = new()
      {
        icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/TP.png")).Sprite,
        color = new("ffffff"),
      },
      Name = this.AnyLocalizations.Bind(["status", "TP", "name"]).Localize,
      Description = this.AnyLocalizations.Bind(["status", "TP", "description"]).Localize
    });
    Frost_Status = helper.Content.Statuses.RegisterStatus("Frost", new()
    {
      Definition = new()
      {
        icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/Frost.png")).Sprite,
        color = new("c6f1ff"),
      },
      Name = this.AnyLocalizations.Bind(["status", "Frost", "name"]).Localize,
      Description = this.AnyLocalizations.Bind(["status", "Frost", "description"]).Localize
    });
    Poison_Status = helper.Content.Statuses.RegisterStatus("Poison", new()
    {
      Definition = new()
      {
        icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/Poison.png")).Sprite,
        color = new("6a458f"),
      },
      Name = this.AnyLocalizations.Bind(["status", "Poison", "name"]).Localize,
      Description = this.AnyLocalizations.Bind(["status", "Poison", "description"]).Localize
    });
    Taunted_Status = helper.Content.Statuses.RegisterStatus("Taunted_Status", new()
    {
      Definition = new()
      {
        icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/Taunted.png")).Sprite,
        color = new("dc1f1f"),
      },
      Name = this.AnyLocalizations.Bind(["status", "Taunted", "name"]).Localize,
      Description = this.AnyLocalizations.Bind(["status", "Taunted", "description"]).Localize
    });
    Concentration_Status = helper.Content.Statuses.RegisterStatus("Concentration", new()
    {
      Definition = new()
      {
        icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/Concentration.png")).Sprite,
        color = new("df7126"),
      },
      Name = this.AnyLocalizations.Bind(["status", "Concentration", "name"]).Localize,
      Description = this.AnyLocalizations.Bind(["status", "Concentration", "description"]).Localize
    });
    Permafrost_Status = helper.Content.Statuses.RegisterStatus("Permafrost", new()
    {
      Definition = new()
      {
        icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/statuses/Permafrost.png")).Sprite,
        color = new("76ade5"),
      },
      Name = this.AnyLocalizations.Bind(["status", "Permafrost", "name"]).Localize,
      Description = this.AnyLocalizations.Bind(["status", "Permafrost", "description"]).Localize
    });

    Inspired_Status_Dictionary = new Dictionary<Deck, IStatusEntry>();
    Charged_Status_Dictionary = new Dictionary<Deck, IStatusEntry>();
    helper.Events.OnModLoadPhaseFinished += (_, phase) =>
    {
      foreach (Deck deck in NewRunOptions.allChars)
      {
        DeckDef meta = DB.decks[deck];
        var charname = Loc.T($"char.{deck.Key()}");
        Color color = meta.color;
        Texture2D textureInspired = InspirationTemplate.ObtainTexture();
        var data = new MGColor[textureInspired.Width * textureInspired.Height];
        textureInspired.GetData(data);
        for (var i = 0; i < data.Length; i++)
         data[i] = new MGColor(
           (float)(data[i].R / 255.0 * color.r),
           (float)(data[i].G / 255.0 * color.g),
           (float)(data[i].B / 255.0 * color.b),
           (float)(data[i].A / 255.0 * color.a)
           );
        var texture2 = new Texture2D(MG.inst.GraphicsDevice, textureInspired.Width, textureInspired.Height);
        texture2.SetData(data);
        var sprite = helper.Content.Sprites.RegisterSprite(() => texture2);
        IStatusEntry entry = helper.Content.Statuses.RegisterStatus($"{charname}IsInspired", new()
        {
          Definition = new()
          {
            icon = sprite.Sprite,
            color = color,
          },
          Name = this.AnyLocalizations.Bind(["status", "Inspiration", "name"], new
          {
            name = charname
          }).Localize,
          Description = this.AnyLocalizations.Bind(["status", "Inspiration", "description"], new
          {
            name = charname
          }).Localize,
        });
        Inspired_Status_Dictionary.Add(deck, entry);
        Texture2D textureCharge = ChargedTemplate.ObtainTexture();
        var data2 = new MGColor[textureCharge.Width * textureCharge.Height];
        textureCharge.GetData(data2);
        for (var i = 0; i < data2.Length; i++)
          data2[i] = new MGColor(
            (float)(data2[i].R / 255.0 * color.r),
            (float)(data2[i].G / 255.0 * color.g),
            (float)(data2[i].B / 255.0 * color.b),
            (float)(data2[i].A / 255.0 * color.a)
            );
        var textureCharged2 = new Texture2D(MG.inst.GraphicsDevice, textureCharge.Width, textureCharge.Height);
        textureCharged2.SetData(data2);
        var sprite2 = helper.Content.Sprites.RegisterSprite(() => textureCharged2);
        IStatusEntry entry2 = helper.Content.Statuses.RegisterStatus($"{charname}Charge", new()
        {
          Definition = new()
          {
            icon = sprite2.Sprite,
            color = color,
          },
          Name = this.AnyLocalizations.Bind(["status", "Charge", "name"], new
          {
            name = charname
          }).Localize,
          Description = this.AnyLocalizations.Bind(["status", "Charge", "description"], new
          {
            name = charname
          }).Localize,
        });
        Charged_Status_Dictionary.Add(deck, entry2);
      }
    };
    helper.Events.OnModLoadPhaseFinished -= Events_OnModLoadPhaseFinished;
    Vi_Deck = Helper.Content.Decks.RegisterDeck("VioletDeck", new DeckConfiguration()
    {
      Definition = new DeckDef()
      {
        color = new Color("ffed2a"),
        titleColor = new Color("000000")
      },
      DefaultCardArt = DefaultCardArt.Sprite,
      BorderSprite = Vi_CardFrame.Sprite,
      Name = this.AnyLocalizations.Bind(["character", "Vi", "name"]).Localize,
    });
    Kabbu_Deck = Helper.Content.Decks.RegisterDeck("KabbuDeck", new DeckConfiguration()
    {
      Definition = new DeckDef()
      {
        color = new Color("4a9b0f"),
        titleColor = new Color("000000")
      },
      DefaultCardArt = DefaultCardArt.Sprite,
      BorderSprite = Kabbu_CardFrame.Sprite,
      Name = this.AnyLocalizations.Bind(["character", "Kabbu", "name"]).Localize,
    });
    Leif_Deck = Helper.Content.Decks.RegisterDeck("LeifDeck", new DeckConfiguration()
    {
      Definition = new DeckDef()
      {
        color = new Color("c6f1ff"),
        titleColor = new Color("000000")
      },
      DefaultCardArt = DefaultCardArt.Sprite,
      BorderSprite = Leif_CardFrame.Sprite,
      Name = this.AnyLocalizations.Bind(["character", "Leif", "name"]).Localize,
    });
    Vi_Char = Helper.Content.Characters.RegisterCharacter("Vi", new CharacterConfiguration()
    {
      Deck = Vi_Deck.Deck,
      Starters = new StarterDeck() { cards = new List<Card> { new TornadoBarrage(), new PoisonSting() }, artifacts = new List<Artifact> { new ExplorersPermit() } },
      Description = this.AnyLocalizations.Bind(["character", "Vi", "description"]).Localize,
      BorderSprite = Vi_Panel.Sprite,
      NeutralAnimation = new()
      {
        Deck = Vi_Deck.Deck,
        LoopTag = "neutral",
        Frames = new[]
        {
          Vi_Neutral_0.Sprite,
          Vi_Neutral_1.Sprite,
          Vi_Neutral_2.Sprite,
          Vi_Neutral_3.Sprite,
          Vi_Neutral_4.Sprite
        }
      },
      MiniAnimation = new()
      {
        Deck = Vi_Deck.Deck,
        LoopTag = "mini",
        Frames = new[] 
        {
          Vi_Mini.Sprite
        }
      }
    });
    Kabbu_Char = Helper.Content.Characters.RegisterCharacter("Kabbu", new CharacterConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      Starters = new StarterDeck() { cards = new List<Card> { new PiercingBlow(), new Taunt() }, artifacts = new List<Artifact> { new ExplorersPermit() } },
      Description = this.AnyLocalizations.Bind(["character", "Kabbu", "description"]).Localize,
      BorderSprite = Kabbu_Panel.Sprite,
      NeutralAnimation = new()
      {
        Deck = Kabbu_Deck.Deck,
        LoopTag = "neutral",
        Frames = new[]
       {
          Kabbu_Neutral_0.Sprite,
          Kabbu_Neutral_1.Sprite,
          Kabbu_Neutral_2.Sprite,
          Kabbu_Neutral_3.Sprite
        }
      },
      MiniAnimation = new()
      {
        Deck = Kabbu_Deck.Deck,
        LoopTag = "mini",
        Frames = new[]
       {
          Kabbu_Mini.Sprite
        }
      }
    });
    Leif_Char = Helper.Content.Characters.RegisterCharacter("Leif", new CharacterConfiguration()
    {
      Deck = Leif_Deck.Deck,
      Starters = new StarterDeck() { cards = new List<Card> { new IcicleShot(), new Defrost() }, artifacts = new List<Artifact> { new ExplorersPermit() } },
      Description = this.AnyLocalizations.Bind(["character", "Leif", "description"]).Localize,
      BorderSprite = Leif_Panel.Sprite,
      NeutralAnimation = new()
      {
        Deck = Leif_Deck.Deck,
        LoopTag = "neutral",
        Frames = new[]
        {
          Leif_Neutral_0.Sprite,
          Leif_Neutral_1.Sprite,
          Leif_Neutral_2.Sprite,
          Leif_Neutral_3.Sprite
        }
      },
      MiniAnimation = new()
      {
        Deck = Leif_Deck.Deck,
        LoopTag = "mini",
        Frames = new[]
        {
          Leif_Mini.Sprite
        }
      }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_neutral", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,

      LoopTag = "neutral",

      Frames = new[]
        {
                Vi_Neutral_0.Sprite,
                Vi_Neutral_1.Sprite,
                Vi_Neutral_2.Sprite,
                Vi_Neutral_3.Sprite,
                Vi_Neutral_4.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_mini", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "mini",
      Frames = new[]
        {
                Vi_Mini.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_squint", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "squint",
      Frames = new[]
        {
                Vi_Squint_0.Sprite,
                Vi_Squint_1.Sprite,
                Vi_Squint_2.Sprite,
                Vi_Squint_3.Sprite,
                Vi_Squint_4.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_gameover", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "gameover",
      Frames = new[]
        {
                Vi_Gameover.Sprite,
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_angy", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "angy",
      Frames = new[]
        {
                Vi_Angy_0.Sprite,
                Vi_Angy_1.Sprite,
                Vi_Angy_2.Sprite,
                Vi_Angy_3.Sprite,
                Vi_Angy_4.Sprite,
                Vi_Angy_5.Sprite,
                Vi_Angy_6.Sprite,
                Vi_Angy_7.Sprite
        }
    }); Helper.Content.Characters.RegisterCharacterAnimation("vi_happy", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "happy",
      Frames = new[]
        {
                Vi_Happy_0.Sprite,
                Vi_Happy_1.Sprite,
                Vi_Happy_2.Sprite,
                Vi_Happy_3.Sprite,
                Vi_Happy_4.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_sad", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "sad",
      Frames = new[]
        {
                Vi_Sad_0.Sprite,
                Vi_Sad_1.Sprite,
                Vi_Sad_2.Sprite,
                Vi_Sad_3.Sprite,
                Vi_Sad_4.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_shocked", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "shocked",
      Frames = new[]
        {
                Vi_Shocked_0.Sprite,
                Vi_Shocked_1.Sprite,
                Vi_Shocked_2.Sprite,
                Vi_Shocked_3.Sprite,
                Vi_Shocked_4.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("vi_unamused", new CharacterAnimationConfiguration()
    {
      Deck = Vi_Deck.Deck,
      LoopTag = "unamused",
      Frames = new[]
        {
                Vi_Unamused_0.Sprite,
                Vi_Unamused_1.Sprite,
                Vi_Unamused_2.Sprite,
                Vi_Unamused_3.Sprite,
                Vi_Unamused_4.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("leif_neutral", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,

      LoopTag = "neutral",

      Frames = new[]
        {
                Leif_Neutral_0.Sprite,
                Leif_Neutral_1.Sprite,
                Leif_Neutral_2.Sprite,
                Leif_Neutral_3.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("leif_mini", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,
      LoopTag = "mini",
      Frames = new[]
        {
                Leif_Mini.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("leif_squint", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,
      LoopTag = "squint",
      Frames = new[]
        {
                Leif_Squint_0.Sprite,
                Leif_Squint_1.Sprite,
                Leif_Squint_2.Sprite,
                Leif_Squint_3.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("leif_gameover", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,
      LoopTag = "gameover",
      Frames = new[]
        {
                Leif_Gameover.Sprite,
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("leif_thinking", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,
      LoopTag = "thinking",
      Frames = new[]
        {
                Leif_Thinking_0.Sprite,
                Leif_Thinking_1.Sprite,
                Leif_Thinking_2.Sprite,
                Leif_Thinking_3.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("leif_hurt", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,
      LoopTag = "hurt",
      Frames = new[]
        {
                Leif_Hurt_0.Sprite,
                Leif_Hurt_1.Sprite,
                Leif_Hurt_2.Sprite,
                Leif_Hurt_3.Sprite
        }
    }); 
    Helper.Content.Characters.RegisterCharacterAnimation("leif_ready", new CharacterAnimationConfiguration()
    {
      Deck = Leif_Deck.Deck,
      LoopTag = "ready",
      Frames = new[]
        {
                Leif_Ready_0.Sprite,
                Leif_Ready_1.Sprite,
                Leif_Ready_2.Sprite,
                Leif_Ready_3.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_neutral", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,

      LoopTag = "neutral",

      Frames = new[]
        {
                Kabbu_Neutral_0.Sprite,
                Kabbu_Neutral_1.Sprite,
                Kabbu_Neutral_2.Sprite,
                Kabbu_Neutral_3.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_mini", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "mini",
      Frames = new[]
        {
                Kabbu_Mini.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_squint", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "squint",
      Frames = new[]
        {
                Kabbu_Squint_0.Sprite,
                Kabbu_Squint_1.Sprite,
                Kabbu_Squint_2.Sprite,
                Kabbu_Squint_3.Sprite
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_gameover", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "gameover",
      Frames = new[]
        {
                Kabbu_Gameover.Sprite,
            }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_angy", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "angy",
      Frames = new[]
        {
                Kabbu_Angy_0.Sprite,
                Kabbu_Angy_1.Sprite,
                Kabbu_Angy_2.Sprite,
                Kabbu_Angy_3.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_explains", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "explaining",
      Frames = new[]
        {
                Kabbu_Explains_0.Sprite,
                Kabbu_Explains_1.Sprite,
                Kabbu_Explains_2.Sprite,
                Kabbu_Explains_3.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_sad", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "sad",
      Frames = new[]
        {
                Kabbu_Sad_0.Sprite,
                Kabbu_Sad_1.Sprite,
                Kabbu_Sad_2.Sprite,
                Kabbu_Sad_3.Sprite
        }
    });
    Helper.Content.Characters.RegisterCharacterAnimation("kabbu_thinking", new CharacterAnimationConfiguration()
    {
      Deck = Kabbu_Deck.Deck,
      LoopTag = "thinking",
      Frames = new[]
        {
                Kabbu_Thinking_0.Sprite,
                Kabbu_Thinking_1.Sprite,
                Kabbu_Thinking_2.Sprite,
                Kabbu_Thinking_3.Sprite
        }
    });

    foreach (var cardType in Snakemouth_AllCard_Types)
      AccessTools.DeclaredMethod(cardType, nameof(ICard.Register))?.Invoke(null, [helper]);

    foreach (var artifactType in Snakemouth_AllArtifact_Types)
      AccessTools.DeclaredMethod(artifactType, nameof(IArtifact.Register))?.Invoke(null, [helper]);
  }

  private void Events_OnModLoadPhaseFinished1(object? sender, ModLoadPhase e)
  {
    throw new NotImplementedException();
  }

  private void Events_OnModLoadPhaseFinished(object? sender, ModLoadPhase e)
  {
    throw new NotImplementedException();
  }
}
