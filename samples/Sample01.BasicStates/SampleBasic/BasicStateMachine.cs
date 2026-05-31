// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Lite.StateMachine;

namespace Sample01.BasicStates.SampleBasic;

/// <summary>State definitions.</summary>
public enum BasicStateId
{
  State1,
  State2,
  State3,
}

public class BasicStateMachine
{
  /// <summary>Example synchronous run method.</summary>
  public static void Run()
  {
    var machine = new StateMachine<BasicStateId>();
    machine.RegisterState<State1>(BasicStateId.State1, BasicStateId.State2);
    machine.RegisterState<State2>(BasicStateId.State2, BasicStateId.State3);
    machine.RegisterState<State3>(BasicStateId.State3);

    // Non-async Start your engine!
    var task = machine.RunAsync(BasicStateId.State1);
    task.GetAwaiter().GetResult();
  }

  /// <summary>Example asynchronous run method.</summary>
  /// <returns>Task.</returns>
  public static async Task RunAsync()
  {
    var machine = new StateMachine<BasicStateId>();
    machine.RegisterState<State1>(BasicStateId.State1, BasicStateId.State2);
    machine.RegisterState<State2>(BasicStateId.State2, BasicStateId.State3);
    machine.RegisterState<State3>(BasicStateId.State3);

    // Async Example!
    await machine.RunAsync(BasicStateId.State1);
  }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "ignore")]
public class State1 : IState<BasicStateId>
{
  public Task OnEntering(Context<BasicStateId> context)
  {
    Console.WriteLine($"[BasicState1][OnEntering]'");
    return Task.CompletedTask;
  }

  public Task OnEnter(Context<BasicStateId> context)
  {
    // Set success from OnEnter to transition to the next state, State2.
    context.NextState(Result.Success);
    Console.WriteLine($"[BasicState1][OnEnter].OnSuccess goto: '{context.NextStates.OnSuccess}'");
    Console.WriteLine($"[BasicState1][OnEnter].OnError goto:   '{context.NextStates.OnError}'");
    Console.WriteLine($"[BasicState1][OnEnter].OnFailure goto: '{context.NextStates.OnFailure}'");

    return Task.CompletedTask;
  }

  public Task OnExit(Context<BasicStateId> context)
  {
    Console.WriteLine($"[BasicState1][OnExit]'");
    return Task.CompletedTask;
  }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "ignore")]
public class State2 : IState<BasicStateId>
{
  public Task OnEntering(Context<BasicStateId> context)
  {
    Console.WriteLine($"[BasicState2][OnEntering]'");
    return Task.CompletedTask;
  }

  public Task OnEnter(Context<BasicStateId> context)
  {
    // Set success from OnEnter to transition to the next state, State2.
    context.NextState(Result.Success);
    Console.WriteLine($"[BasicState2][OnEnter].OnSuccess goto: '{context.NextStates.OnSuccess}'");
    Console.WriteLine($"[BasicState2][OnEnter].OnError goto:   '{context.NextStates.OnError}'");
    Console.WriteLine($"[BasicState2][OnEnter].OnFailure goto: '{context.NextStates.OnFailure}'");

    return Task.CompletedTask;
  }

  public Task OnExit(Context<BasicStateId> context)
  {
    Console.WriteLine($"[BasicState2][OnExit]'");
    return Task.CompletedTask;
  }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "ignore")]
public class State3 : IState<BasicStateId>
{
  public Task OnEntering(Context<BasicStateId> context)
  {
    Console.WriteLine($"[BasicState3][OnEntering]'");
    return Task.CompletedTask;
  }

  public Task OnEnter(Context<BasicStateId> context)
  {
    // Set success from OnEnter to transition to the next state, State2.
    context.NextState(Result.Success);
    Console.WriteLine($"[BasicState3][OnEnter].OnSuccess goto: '{context.NextStates.OnSuccess}'");
    Console.WriteLine($"[BasicState3][OnEnter].OnError goto:   '{context.NextStates.OnError}'");
    Console.WriteLine($"[BasicState3][OnEnter].OnFailure goto: '{context.NextStates.OnFailure}'");

    return Task.CompletedTask;
  }

  public Task OnExit(Context<BasicStateId> context)
  {
    Console.WriteLine($"[BasicState3][OnExit]'");
    return Task.CompletedTask;
  }
}
