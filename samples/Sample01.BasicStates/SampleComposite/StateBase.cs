// Copyright Xeno Innovations, Inc. 2025
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Lite.StateMachine;

namespace Sample01.BasicStates.SampleComposite;

/// <summary>
///   This is a base state class that can be used for all states in the sample.
///   It provides default implementations of the IState methods, which can be
///   overridden by derived classes as needed. This allows for code reuse and
///   consistency across all states in the state machine.
///
///   Note, that if 'OnEnter' is not provided, it will AUTO-SUCCEED and transition
///   to the next state (if any). This is a convenient default behavior for states
///   that do not have any specific logic to execute upon entering, but it can be
///   overridden if you need to perform some actions before transitioning to the
///   next state.
/// </summary>
/// <typeparam name="TStateClass">The type of the state class.</typeparam>
/// <typeparam name="TStateId">The type of the state identifier.</typeparam>
public class StateBase<TStateClass, TStateId> : IState<TStateId>
  where TStateId : struct, Enum
{
  #region Suppress CodeMaid Method Sorting

  [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "ignore")]
  public virtual Task OnEntering(Context<TStateId> context)
  {
    ////Console.WriteLine("[StateBase][OnEntering]");
    return Task.CompletedTask;
  }

  #endregion

  public virtual Task OnEnter(Context<TStateId> context)
  {
    ////Console.WriteLine("[StateBase][OnEnter]");
    context.NextState(Result.Success);
    return Task.CompletedTask;
  }

  public virtual Task OnExit(Context<TStateId> context)
  {
    ////Console.WriteLine("[StateBase][OnExit]");
    return Task.CompletedTask;
  }
}
