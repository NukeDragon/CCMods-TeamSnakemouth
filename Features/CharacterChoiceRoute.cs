using daisyowl.text;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class CharacterChoiceRoute : Route
  {
    private const UK ChoiceKey = (UK)574389;

    public List<Character> Chars = [];

    public int Mode = 0;

    public int statusAmount = 1;

    public Artifact? ArtifactChoice;

    public Dictionary<Deck, IStatusEntry>? dictionary;

    public override bool GetShowOverworldPanels()
    => true;

    public override bool CanBePeeked()
      => true;

    public override void Render(G g)
    {
      if (Chars.Count == 0) { g.CloseRoute(this); return; }
      SharedArt.DrawEngineering(g);

      int centerX = 240;
      int topY = 44;
      int buttonWidth = 60;
      int buttonHeight = 24;

      Draw.Text("PICK A CHARACTER", centerX, topY, font: DB.stapler, color: Colors.textMain, align: TAlign.Center);

      for (int i = 0; i < Chars.Count; i++)
      {
        Character c = Chars[i];
        Deck deck = c.deckType.GetValueOrDefault();
        int buttonX = centerX - 110 + i * 80;
        int buttonY = topY + 100;
        var buttonRect = new Rect(buttonX, buttonY, buttonWidth, buttonHeight);
        var buttonResult = SharedArt.ButtonText(g, Vec.Zero, new UIKey(ChoiceKey, i), Loc.T($"char.{deck.Key()}"), textColor: DB.decks[deck].color ,rect: buttonRect, onMouseDown: new MouseDownHandler(() => OnChoice(g, c)));
      }
    }

    private void OnChoice(G g, Character character)
    {
      if (Mode  == 0) { return; }
      if (Mode == 1)
      {
        if (dictionary == null) { return; }
        if (g.state.route is not Combat combat) return;
        IStatusEntry? entry;
        Deck deck = character.deckType.GetValueOrDefault();
        if (!dictionary.TryGetValue(deck, out entry)) return;
        if (entry == null) { return;}
        combat.Queue(new AStatus()
        {
          status = entry.Status,
          statusAmount = statusAmount,
          targetPlayer = true
        });
        g.CloseRoute(this);
      }
      if (Mode == 2)
      {
        if (ArtifactChoice is Meditation)
        {
          Meditation? artifact1 = ArtifactChoice as Meditation;
          artifact1!.AssignedDeck = character.deckType;
          foreach (Character character1 in g.state.characters)
          {
            if (character1.artifacts.Contains(artifact1)) character1.artifacts.Remove(artifact1);
          }
          if (g.state.artifacts.Contains(artifact1)) g.state.artifacts.Remove(artifact1);
          character.artifacts.Add(artifact1);
        }
        if (ArtifactChoice is Prayer)
        {
          Prayer? artifact1 = ArtifactChoice as Prayer;
          artifact1!.AssignedDeck = character.deckType;
          foreach (Character character1 in g.state.characters)
          {
            if (character1.artifacts.Contains(artifact1)) character1.artifacts.Remove(artifact1);
          }
          if (g.state.artifacts.Contains(artifact1)) g.state.artifacts.Remove(artifact1);
          character.artifacts.Add(artifact1);
        }
        if (ArtifactChoice is Reflection)
        {
          Reflection? artifact1 = ArtifactChoice as Reflection;
          artifact1!.AssignedDeck = character.deckType;
          foreach (Character character1 in g.state.characters)
          {
            if (character1.artifacts.Contains(artifact1)) character1.artifacts.Remove(artifact1);
          }
          if (g.state.artifacts.Contains(artifact1)) g.state.artifacts.Remove(artifact1);
          character.artifacts.Add(artifact1);
        }
        if (ArtifactChoice is PoisonAttacker)
        {
          PoisonAttacker? artifact1 = ArtifactChoice as PoisonAttacker;
          artifact1!.AssignedDeck = character.deckType;
          foreach (Character character1 in g.state.characters)
          {
            if (character1.artifacts.Contains(artifact1)) character1.artifacts.Remove(artifact1);
          }
          if (g.state.artifacts.Contains(artifact1)) g.state.artifacts.Remove(artifact1);
          character.artifacts.Add(artifact1);
        }
        if (ArtifactChoice is AntlionJaws)
        {
          AntlionJaws? artifact1 = ArtifactChoice as AntlionJaws;
          artifact1!.AssignedDeck = character.deckType;
          foreach (Character character1 in g.state.characters)
          {
            if (character1.artifacts.Contains(artifact1)) character1.artifacts.Remove(artifact1);
          }
          if (g.state.artifacts.Contains(artifact1)) g.state.artifacts.Remove(artifact1);
          character.artifacts.Add(artifact1);
        }
        if (ArtifactChoice is DefenseExchange)
        {
          DefenseExchange? artifact1 = ArtifactChoice as DefenseExchange;
          artifact1!.AssignedDeck = character.deckType;
          foreach (Character character1 in g.state.characters)
          {
            if (character1.artifacts.Contains(artifact1)) character1.artifacts.Remove(artifact1);
          }
          if (g.state.artifacts.Contains(artifact1)) g.state.artifacts.Remove(artifact1);
          character.artifacts.Add(artifact1);
        }
        g.CloseRoute(this);
      }
    }
  }
}
