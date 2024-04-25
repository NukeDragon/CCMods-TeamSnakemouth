using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class AInspire : CardAction
  {
    public int statusAmount = 0;

    public override Route? BeginWithRoute(G g, State state, Combat combat)
      => new CharacterChoiceRoute()
      {
        Chars = state.characters,
        statusAmount = statusAmount,
        dictionary = ModEntry.Instance.Inspired_Status_Dictionary,
        Mode = 1
      };

    public override List<Tooltip> GetTooltips(State s)
    {
      List<Tooltip> tooltips = [
        new GlossaryTooltip(key: $"{ModEntry.Instance.Package.Manifest.UniqueName}::InspireAction")
        {
          Icon = ModEntry.Instance.InspirationTemplate.Sprite,
          Title = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "inspire", "name"], new
          {
            amount = statusAmount
          }),
          Description = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "inspire", "description"], new
          {
            amount = statusAmount
          }),
          TitleColor = Colors.action
        }
        ];
      return tooltips;
    }

    public override Icon? GetIcon(State s) => new(ModEntry.Instance.InspirationTemplate.Sprite, statusAmount, Colors.textMain);
  }

  internal class ACharge : CardAction
  {
    public int statusAmount = 0;

    public override Route? BeginWithRoute(G g, State state, Combat combat)
      => new CharacterChoiceRoute()
      {
        Chars = state.characters,
        statusAmount = statusAmount,
        dictionary = ModEntry.Instance.Charged_Status_Dictionary,
        Mode = 1
      };

    public override List<Tooltip> GetTooltips(State s)
    {
      List<Tooltip> tooltips = [
        new GlossaryTooltip(key: $"{ModEntry.Instance.Package.Manifest.UniqueName}::ChargeAction")
        {
          Icon = ModEntry.Instance.ChargedTemplate.Sprite,
          Title = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "charge", "name"], new
          {
            amount = statusAmount
          }),
          Description = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "charge", "description"], new
          {
            amount = statusAmount
          }),
          TitleColor = Colors.action
        }
        ];
      return tooltips;
    }

    public override Icon? GetIcon(State s) => new(ModEntry.Instance.ChargedTemplate.Sprite, statusAmount, Colors.textMain);
  }

  internal class AArtifactChooseCharacter : CardAction
  {
    public Artifact? artifact;

    public override Route? BeginWithRoute(G g, State state, Combat combat)
      => new CharacterChoiceRoute()
      {
        Chars = state.characters,
        ArtifactChoice = artifact,
        Mode = 2
      };
  }
}
