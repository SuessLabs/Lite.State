// Copyright Xeno Innovations, Inc. 2025
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Lite.StateMachine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample03.DependencyInjection.MsDI;

namespace Sample03.DependencyInjection;

internal class Program
{
  private static async Task Main()
  {
    await MsDIStateMachine.RunMsDiAsync();
  }
}
