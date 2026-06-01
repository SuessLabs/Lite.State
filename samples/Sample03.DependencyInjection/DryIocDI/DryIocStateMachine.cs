// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DryIoc;
using Lite.StateMachine;
using Microsoft.Extensions.Logging;

namespace Sample03.DependencyInjection.DryIocDI;

#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type

public static class DryIocStateMachine
{
  /// <summary>State definitions.</summary>
  public enum BasicStateId
  {
    State1,
    State2,
    State3,
  }

  public static async Task RunAsync()
  {
    var container = new Container(rules => rules.With(FactoryMethod.ConstructorWithResolvableArguments));

    // Register Services
    container.Register<IEventAggregator, EventAggregator>(Reuse.Singleton);
    container.Register<ICounterService, CounterService>(Reuse.Singleton);

    // Register Logger
    //// For use with NLog:
    //// var loggerFactory = new NLogLoggerFactory();
    container.RegisterInstance<ILoggerFactory>(LoggerFactory.Create(builder =>
     {
       builder.SetMinimumLevel(LogLevel.Trace);
       builder.AddSimpleConsole(options =>
       {
         options.IncludeScopes = true;
         options.SingleLine = true;
       });
     }));
    container.Register(typeof(ILogger<>), typeof(Logger<>), Reuse.Transient);

    // Register States
    container.Register<State1>(Reuse.Transient);
    container.Register<State2>(Reuse.Transient);
    container.Register<State3>(Reuse.Transient);

    // Resolve dependency services for post-run evaluations
    Func<Type, object?> factory = t => container.Resolve(t);
    var aggregator = container.Resolve<IEventAggregator>();
    var counterService = container.Resolve<ICounterService>();

    // Create State Machine
    var machine = new StateMachine<BasicStateId>(factory)
      .RegisterState<State1>(BasicStateId.State1, BasicStateId.State2)
      .RegisterState<State2>(BasicStateId.State2, BasicStateId.State3)
      .RegisterState<State3>(BasicStateId.State3);

    var result = await machine.RunAsync(BasicStateId.State1);

    Console.WriteLine("Post Execution Validations:");
    Console.WriteLine("---------------------------");

    Console.WriteLine($"* Counter service Counter1: {counterService.Counter1} (expected 9)");

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

public class BaseDiState<TStateClass, TStateId>(ICounterService msg, ILogger<TStateClass> logger)
  : IState<TStateId>
  where TStateId : struct, Enum
{
  private readonly ILogger<TStateClass> _logger = logger;
  private readonly ICounterService _msgService = msg;

  /// <summary>Gets or sets a value indicating whether output transitions for debugging tests.</summary>
  public bool HasExtraLogging { get; set; } = false;

  public ILogger<TStateClass> Log => _logger;

  public ICounterService MessageService => _msgService;

  #region Suppress CodeMaid Method Sorting

  [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "ignore")]
  public virtual Task OnEntering(Context<TStateId> context)
  {
    _msgService.Counter1++;
    _logger.LogInformation("[OnEntering]");

    if (HasExtraLogging)
      Debug.WriteLine($"[{GetType().Name}] [OnEntering]");

    return Task.CompletedTask;
  }

  #endregion Suppress CodeMaid Method Sorting

  public virtual Task OnEnter(Context<TStateId> context)
  {
    _msgService.Counter1++;
    _logger.LogInformation("[OnEnter] => OK");

    if (HasExtraLogging)
      Debug.WriteLine($"[{GetType().Name}] [OnEnter] => OK");

    context.NextState(Result.Success);
    return Task.CompletedTask;
  }

  public virtual Task OnExit(Context<TStateId> context)
  {
    _msgService.Counter1++;
    _logger.LogInformation("[OnExit]");

    if (HasExtraLogging)
      Debug.WriteLine($"[{GetType().Name}] [OnExit]");

    context.NextState(Result.Success);
    return Task.CompletedTask;
  }
}

#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
