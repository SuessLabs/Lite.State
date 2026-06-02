// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lite.StateMachine.Tests.StateTests;

[TestClass]
public class NextStateResultTests : TestBase
{
  /// <summary>State definitions.</summary>
  public enum StateId
  {
    State1,
    State2,
    State3,
  }

  [TestMethod]
  public async Task NextState_Error_Failure__SuccessTestAsync()
  {
    var machine = new StateMachine<StateId>();
    machine.RegisterState<State1>(StateId.State1, onSuccess: StateId.State1, onError: StateId.State2, onFailure: null, subscriptionTypes: null);
    machine.RegisterState<State2>(StateId.State2, onSuccess: StateId.State1, onError: null, onFailure: StateId.State3, subscriptionTypes: null);
    machine.RegisterState<State3>(StateId.State3, subscriptionTypes: null);

    // Async Example!
    await machine.RunAsync(StateId.State1);

    // Assert Results
    AssertMachineNotNull(machine);

    // Ensure all states are registered
    var enums = Enum.GetValues<StateId>().Cast<StateId>();
    Assert.IsNotNull(enums);
    Assert.HasCount(enums.Count(), machine.States);
    Assert.IsTrue(enums.All(k => machine.States.Contains(k)));
  }

  private class State1 : IState<StateId>
  {
    public Task OnEnter(Context<StateId> context)
    {
      // Simulate an Error state transition so we can continue.
      context.NextState(Result.Error);

      Console.WriteLine($"[State1][OnEnter].OnSuccess goto: '{context.NextStates.OnSuccess}'");
      Console.WriteLine($"[State1][OnEnter].OnError goto:   '{context.NextStates.OnError}'");
      Console.WriteLine($"[State1][OnEnter].OnFailure goto: '{context.NextStates.OnFailure}'");
      return Task.CompletedTask;
    }

    public Task OnEntering(Context<StateId> context) => Task.CompletedTask;

    public Task OnExit(Context<StateId> context) => Task.CompletedTask;
  }

  private class State2 : IState<StateId>
  {
    public Task OnEnter(Context<StateId> context)
    {
      // Simulate a Failure state transition so we can continue.
      context.NextState(Result.Failure);
      return Task.CompletedTask;
    }

    public Task OnEntering(Context<StateId> context) => Task.CompletedTask;

    public Task OnExit(Context<StateId> context) => Task.CompletedTask;
  }

  private class State3 : IState<StateId>
  {
    public Task OnEnter(Context<StateId> context)
    {
      // Set Failure so we can continue.
      context.NextState(Result.Success);
      return Task.CompletedTask;
    }

    public Task OnEntering(Context<StateId> context) => Task.CompletedTask;

    public Task OnExit(Context<StateId> context) => Task.CompletedTask;
  }
}
