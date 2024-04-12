﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace NukeDragon.TeamSnakemouth
{
  public interface ISnakemouthApi
  {
    IDeckEntry Vi_Deck { get; }
    IDeckEntry Kabbu_Deck { get; }
    IDeckEntry Leif_Deck { get; }
    IStatusEntry TP_Status { get; }
    IStatusEntry Frost_Status { get; }
    IStatusEntry Poison_Status { get; }
    IStatusEntry Charge_Status { get; }
    IStatusEntry Taunted_Status { get; }
    IStatusEntry Concentration_Status { get; }

    void RegisterHook(ISnakemouthHook hook, double priority);
    void UnregisterHook(ISnakemouthHook hook);
  }

  public interface ISnakemouthHook
  {
    void ModifyAAttack(ref AAttack attack, State s, Combat c) { }
  }
}
