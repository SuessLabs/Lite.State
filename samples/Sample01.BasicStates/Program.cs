// Copyright Xeno Innovations, Inc. 2026
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;

namespace Sample01.BasicStates;

internal class Program
{
  private static async Task Main(string[] args)
  {
    Console.WriteLine("Regular State Sync: Starting...");
    SampleBasic.BasicStateMachine.Run();
    Console.WriteLine("Regular State Sync: DONE!");

    Console.WriteLine("\n\nRegular State Async: Starting...");
    await SampleBasic.BasicStateMachine.RunAsync();
    Console.WriteLine("Regular State Async: DONE!");

    Console.WriteLine("\n\n-=-=-=-=-=-\n\n");

    Console.WriteLine("Composite Sync: Starting...");
    SampleComposite.CompositeStateMachine.Run();
    Console.WriteLine("Composite Sync: DONE!");
  }
}
