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
    await TestFlatStatesAsync();
  }

  private static async Task TestFlatStatesAsync()
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

    ////Assert.IsNotNull(result);
    ////AssertMachineNotNull(machine);

    var msgService = services.GetRequiredService<ICounterService>();
    ////Assert.AreEqual(9, msgService.Counter1, "Message service should have 9 from the 3 states.");

    // Ensure all states are registered
    var enums = Enum.GetValues<BasicStateId>().Cast<BasicStateId>();
    ////Assert.AreEqual(enums.Count(), machine.States.Count());
    ////Assert.IsTrue(enums.All(k => machine.States.Contains(k)));

    // Ensure they're registered in order
    ////Assert.IsTrue(enums.SequenceEqual(machine.States), "States should be registered for execution in the same order as the defined enums, StateId 1 => 2 => 3.");
  }
}
