# Sample 01 - Basic States

This project demonstrates the basics of Lite.StateMachine using a
.NET application, including how to manage and transition between different
states such as initialization, running, and shutdown.

It serves as a foundational example for understanding the lifecycle of a
state-based application and how to handle state management effectively.

There are 2 applications in this sample:

* **Basic State Machine** - Flat state machine with no nested states, demonstrating simple state transitions.
* **Composite State Machine** - More complex state machine with nested states, showcasing hierarchical state management.

## Basic State Machine

The file `BasicStateMachine.cs` contains the implementation of a simple state
machine with three states: `State1`, `State2`, and `State3` for verbosity.

Each of the states will be fully implemented with `OnEntering`, `OnEnter`, and `OnExit` transitions.

## Composite State Machine

The file `CompositeStateMachine.cs` contains a more complex state machine that includes nested states.
This example demonstrates how to manage hierarchical states, where a parent state can contain multiple
child states, allowing for more complex behavior and transitions.
