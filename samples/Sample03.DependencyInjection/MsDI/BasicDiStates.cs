// Copyright Xeno Innovations, Inc. 2025
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;

namespace Sample03.DependencyInjection.MsDI;

#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type

/// <summary>State definitions.</summary>
public enum BasicStateId
{
  State1,
  State2,
  State3,
}

public class BasicDiState1(ICounterService msg, ILogger<BasicDiState1> log)
  : StateDiBase<BasicDiState1, BasicStateId>(msg, log);

public class BasicDiState2(ICounterService msg, ILogger<BasicDiState2> log)
  : StateDiBase<BasicDiState2, BasicStateId>(msg, log);

public class BasicDiState3(ICounterService msg, ILogger<BasicDiState3> log)
  : StateDiBase<BasicDiState3, BasicStateId>(msg, log);

#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
