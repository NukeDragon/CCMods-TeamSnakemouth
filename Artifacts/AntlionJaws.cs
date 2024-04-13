using HarmonyLib;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NukeDragon.TeamSnakemouth.Cards;

namespace NukeDragon.TeamSnakemouth
{
  internal class AntlionJaws : Artifact, IArtifact, ISnakemouthHook
  {
    public Deck? AssignedDeck;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("AntlionJaws", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Kabbu_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.AntlionJawsSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "AntlionJaws", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "AntlionJaws", "description"]).Localize
      });
    }
    public override void OnReceiveArtifact(State state)
    {
      state.GetCurrentQueue().QueueImmediate<CardAction>(new AArtifactChooseCharacter { artifact = this });
    }
    public void ModifyAAttack(ref AAttack attack, State s, Combat c)
    {
      if (attack.whoDidThis == this.AssignedDeck && !attack.piercing)
      {
        attack.piercing = true;
        this.Pulse();
      }
    }
  }
}
