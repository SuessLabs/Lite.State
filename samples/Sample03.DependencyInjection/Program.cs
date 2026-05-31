// Copyright Xeno Innovations, Inc. 2025
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Lite.StateMachine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sample03.DependencyInjection;

internal class Program
{
  private static async Task Main()
  {
    await TestMicrosoftDependencyInjectionAsync();
  }

  private static async Task TestMicrosoftDependencyInjectionAsync()
  {
    // Assemble with Dependency Injection
    var services = new ServiceCollection()
      //// Register Services
      .AddLogging(b => b.AddSimpleConsole())
      .AddSingleton<ICounterService, CounterService>()
      //// Register States
      .AddTransient<BasicDiState1>()
      .AddTransient<BasicDiState2>()
      .AddTransient<BasicDiState3>()
      .BuildServiceProvider();

    Func<Type, object?> factory = t => ActivatorUtilities.CreateInstance(services, t);

    var machine = new StateMachine<BasicStateId>(factory)
      .RegisterState<BasicDiState1>(BasicStateId.State1, BasicStateId.State2)
      .RegisterState<BasicDiState2>(BasicStateId.State2, BasicStateId.State3)
      .RegisterState<BasicDiState3>(BasicStateId.State3);

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
}
