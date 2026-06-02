// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Lite.StateMachine;

namespace Sample01.BasicStates.SampleComposite;

public enum CompositeL3
{
  State1,
  State2,
  State2_Sub1,
  State2_Sub2,
  State2_Sub2_Sub1,
  State2_Sub2_Sub2,
  State2_Sub2_Sub3,
  State2_Sub3,
  State3,
}

public class CompositeStateMachine
{
  /// <summary>Example synchronous run method.</summary>
  public static void Run()
  {
    // Non-async Start your engine!
    var machine = GenerateStateMachineL3(new StateMachine<CompositeL3>());
    var task = machine.RunAsync(CompositeL3.State1);
    task.GetAwaiter().GetResult();
  }

  /// <summary>Example asynchronous run method.</summary>
  /// <returns>Task.</returns>
  public static async Task RunAsync()
  {
    // Async Example!
    var machine = GenerateStateMachineL3(new StateMachine<CompositeL3>());
    await machine.RunAsync(CompositeL3.State1);
  }

  private static StateMachine<CompositeL3> GenerateStateMachineL3(StateMachine<CompositeL3> machine)
  {
    machine
      .RegisterState<State1>(CompositeL3.State1, CompositeL3.State2)
      .RegisterComposite<State2>(CompositeL3.State2, initialChildStateId: CompositeL3.State2_Sub1, onSuccess: CompositeL3.State3)
      .RegisterSubState<State2_Sub1>(CompositeL3.State2_Sub1, parentStateId: CompositeL3.State2, onSuccess: CompositeL3.State2_Sub2)
      .RegisterSubComposite<State2_Sub2>(CompositeL3.State2_Sub2, parentStateId: CompositeL3.State2, initialChildStateId: CompositeL3.State2_Sub2_Sub1, onSuccess: CompositeL3.State2_Sub3)
      .RegisterSubState<State2_Sub2_Sub1>(CompositeL3.State2_Sub2_Sub1, parentStateId: CompositeL3.State2_Sub2, onSuccess: CompositeL3.State2_Sub2_Sub2)
      .RegisterSubState<State2_Sub2_Sub2>(CompositeL3.State2_Sub2_Sub2, parentStateId: CompositeL3.State2_Sub2, onSuccess: CompositeL3.State2_Sub2_Sub3)
      .RegisterSubState<State2_Sub2_Sub3>(CompositeL3.State2_Sub2_Sub3, parentStateId: CompositeL3.State2_Sub2, onSuccess: null)
      .RegisterSubState<State2_Sub3>(CompositeL3.State2_Sub3, parentStateId: CompositeL3.State2, onSuccess: null)
      .RegisterState<State3>(CompositeL3.State3, onSuccess: null);

    return machine;
  }
}

#pragma warning disable SA1124 // Do not use regions
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable IDE0130 // Namespace does not match folder structure

public class State1()
  : StateBase<State1, CompositeL3>()
{
  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State1][OnEnter]");
    return base.OnEnter(context);
  }
}

/// <summary>Level-1: Composite.</summary>
public class State2()
  : StateBase<State2, CompositeL3>()
{
  #region CodeMaid - DoNotReorder

  public override Task OnEntering(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2][OnEntering]");
    return base.OnEntering(context);
  }

  #endregion CodeMaid - DoNotReorder

  public override Task OnEnter(Context<CompositeL3> context)
  {
    // NOTE:
    //  We're a parent composite state. The 'context.NextState' is
    //  not used here as we will call it in the OnExit after all child
    //  states have completed.
    Console.WriteLine($"[State2][OnEnter]**");
    return Task.CompletedTask;
  }

  public override Task OnExit(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2][OnExit]**");

    // NOTE:
    //  As this is a parent Composite state, we MUST call NextState to trigger
    //  the parent state to move to the next state to signify that the child states have completed.
    context.NextState(Result.Success);
    return base.OnExit(context);
  }
}

/// <summary>Sublevel-2: State.<summary>
public class State2_Sub1()
  : StateBase<State2_Sub1, CompositeL3>()
{
  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub1][OnEnter]");
    return base.OnEnter(context);
  }
}

/// <summary>Sublevel-2: Composite.</summary>
public class State2_Sub2()
  : StateBase<State2_Sub2, CompositeL3>()
{
  #region CodeMaid - DoNotReorder

  public override Task OnEntering(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub2][OnEntering]");
    return base.OnEntering(context);
  }

  #endregion CodeMaid - DoNotReorder

  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub2][OnEnter]**");
    Console.WriteLine($"[State2_Sub2][OnEnter] CurrentStateId:   {context.CurrentStateId} ({CompositeL3.State2_Sub2})");
    Console.WriteLine($"[State2_Sub2][OnEnter] PreviousStateId:  {context.PreviousStateId} ({CompositeL3.State2_Sub1})");
    Console.WriteLine($"[State2_Sub2][OnEnter] LastChildStateId: {context.LastChildStateId} ({CompositeL3.State2_Sub2_Sub3})");

    // NOTE:
    //  We're a parent composite state. The 'context.NextState' is
    //  not used here as we will call it in the OnExit after all child
    //  states have completed.
    ////return base.OnEnter(context);
    return Task.CompletedTask;
  }

  public override Task OnExit(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub2][OnExit]");
    Console.WriteLine($"[State2_Sub2][OnExit] CurrentStateId:   {context.CurrentStateId} ({CompositeL3.State2_Sub2})");
    Console.WriteLine($"[State2_Sub2][OnExit] PreviousStateId:  {context.PreviousStateId} ({CompositeL3.State2_Sub1})");
    Console.WriteLine($"[State2_Sub2][OnExit] LastChildStateId: {context.LastChildStateId} ({CompositeL3.State2_Sub2_Sub3})");

    // NOTE:
    //  As this is a parent Composite state, we MUST call NextState to trigger
    //  the parent state to move to the next state to signify that the child states have completed.
    context.NextState(Result.Success);
    return base.OnExit(context);
  }
}

/// <summary>Sublevel-3: State.</summary>
public class State2_Sub2_Sub1()
  : StateBase<State2_Sub2_Sub1, CompositeL3>()
{
  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub2_Sub1][OnEnter] (success)");
    return base.OnEnter(context);
  }
}

/// <summary>Sublevel-3: State.</summary>
/// <remarks>NOTE: We are auto-succeeding and not populating OnEnter.</remarks>
public class State2_Sub2_Sub2()
  : StateBase<State2_Sub2_Sub2, CompositeL3>()
{
}

/// <summary>Sublevel-3: Last State.</summary>
public class State2_Sub2_Sub3()
  : StateBase<State2_Sub2_Sub3, CompositeL3>()
{
  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub2_Sub3][OnEnter] (Success)");
    return base.OnEnter(context);
  }
}

/// <summary>Sublevel-2: Last State.</summary>
public class State2_Sub3()
  : StateBase<State2_Sub3, CompositeL3>()
{
  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State2_Sub3][OnEnter]");
    return base.OnEnter(context);
  }
}

/// <summary>Make sure not child-created context is there.</summary>
public class State3()
  : StateBase<State3, CompositeL3>()
{
  public override Task OnEnter(Context<CompositeL3> context)
  {
    Console.WriteLine($"[State3][OnEnter]");
    return base.OnEnter(context);
  }
}

#pragma warning restore IDE0130 // Namespace does not match folder structure
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1124 // Do not use regions
