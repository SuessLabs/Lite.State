// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;

namespace Sample03.DependencyInjection;

internal class Program
{
  private static async Task Main()
  {
    await DryIocDI.DryIocStateMachine.RunAsync();

    Console.Write("\n-=-=-=-=-=-=-\n\n");

    await MsDI.MsDIStateMachine.RunAsync();
  }
}
