// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using Lite.StateMachine;

namespace Sample04.ResultTransitions;

/// <summary>State definitions.</summary>
public enum StateId
{
  State1,
  State2,
  State3,
}

public class Program
{
  private static async Task Main(string[] args)
  {
    await ResultsStateMachine.RunAsync();
  }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Ignore")]
public class ResultsStateMachine
{
  /// <summary>Example asynchronous run method.</summary>
  /// <returns>Task.</returns>
  public static async Task RunAsync()
  {
    var machine = new StateMachine<StateId>();
    machine.RegisterState<State1>(StateId.State1, onSuccess: StateId.State1, onError: StateId.State2, onFailure: null, subscriptionTypes: null);
    machine.RegisterState<State2>(StateId.State2, onSuccess: StateId.State1, onError: null, onFailure: StateId.State3, subscriptionTypes: null);
    machine.RegisterState<State3>(StateId.State3, subscriptionTypes: null);

    // Async Example!
    await machine.RunAsync(StateId.State1);
  }

  public class State1 : IState<StateId>
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

  public class State2 : IState<StateId>
  {
    public Task OnEnter(Context<StateId> context)
    {
      // Simulate a Failure state transition so we can continue.
      context.NextState(Result.Failure);

      Console.WriteLine($"[State2][OnEnter].OnSuccess goto: '{context.NextStates.OnSuccess}'");
      Console.WriteLine($"[State2][OnEnter].OnError goto:   '{context.NextStates.OnError}'");
      Console.WriteLine($"[State2][OnEnter].OnFailure goto: '{context.NextStates.OnFailure}'");

      return Task.CompletedTask;
    }

    public Task OnEntering(Context<StateId> context) => Task.CompletedTask;

    public Task OnExit(Context<StateId> context) => Task.CompletedTask;
  }

  public class State3 : IState<StateId>
  {
    public Task OnEnter(Context<StateId> context)
    {
      // Set Failure so we can continue.
      context.NextState(Result.Success);

      Console.WriteLine($"[State3][OnEnter].OnSuccess goto: '{context.NextStates.OnSuccess}'");
      Console.WriteLine($"[State3][OnEnter].OnError goto:   '{context.NextStates.OnError}'");
      Console.WriteLine($"[State3][OnEnter].OnFailure goto: '{context.NextStates.OnFailure}'");

      return Task.CompletedTask;
    }

    public Task OnEntering(Context<StateId> context) => Task.CompletedTask;

    public Task OnExit(Context<StateId> context) => Task.CompletedTask;
  }
}
