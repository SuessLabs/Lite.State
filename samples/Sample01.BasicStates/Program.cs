// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sample01.BasicStates;

internal class Program
{
  private static async Task Main(string[] args)
  {
    Console.WriteLine("Running Basic State Machine: Synchronous sample...");

    Samples.BasicStateMachine.Run();

    Console.WriteLine("\n\nRunning Basic State Machine: Asynchronous sample...");
    await Samples.BasicStateMachine.RunAsync();
  }
}
