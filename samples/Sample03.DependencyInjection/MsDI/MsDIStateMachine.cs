// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Lite.StateMachine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sample03.DependencyInjection.MsDI;

#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type

public static class MsDIStateMachine
{
  /// <summary>State definitions.</summary>
  public enum BasicStateId
  {
    State1,
    State2,
    State3,
  }

  public static async Task RunMsDiAsync()
  {
    // Assemble with Dependency Injection
    var services = new ServiceCollection()
      //// Register Services
      .AddLogging(b => b.AddSimpleConsole())
      .AddSingleton<ICounterService, CounterService>()
      //// Register States
      .AddTransient<State1>()
      .AddTransient<State2>()
      .AddTransient<State3>()
      .BuildServiceProvider();

    Func<Type, object?> factory = t => ActivatorUtilities.CreateInstance(services, t);

    var machine = new StateMachine<BasicStateId>(factory)
      .RegisterState<State1>(BasicStateId.State1, BasicStateId.State2)
      .RegisterState<State2>(BasicStateId.State2, BasicStateId.State3)
      .RegisterState<State3>(BasicStateId.State3);

    var result = await machine.RunAsync(BasicStateId.State1);

    Console.WriteLine("\n\nPost Execution Validations:");
    Console.WriteLine("---------------------------");

    var msgService = services.GetRequiredService<ICounterService>();
    Console.WriteLine($"* Message service Counter1: {msgService.Counter1} (expected 9)");

    // Ensure all states are registered
    var enums = Enum.GetValues<BasicStateId>().Cast<BasicStateId>();
    Console.WriteLine($"* State Machine Counts: {machine.States.Count()}. State Enum Count: {enums.Count()}");
    Console.WriteLine($"* All states registered: {enums.All(k => machine.States.Contains(k))}");

    // Ensure they're registered in order
    // Validates that States are registered for execution in the same order as the defined enums. StateId 1 => 2 => 3.
    Console.WriteLine($"* State registered in order: {enums.SequenceEqual(machine.States)}");
  }

  public class State1(ICounterService msg, ILogger<State1> log)
    : BaseDiState<State1, BasicStateId>(msg, log);

  public class State2(ICounterService msg, ILogger<State2> log)
    : BaseDiState<State2, BasicStateId>(msg, log);

  public class State3(ICounterService msg, ILogger<State3> log)
    : BaseDiState<State3, BasicStateId>(msg, log);
}

#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
