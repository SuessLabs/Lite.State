# Sample 01 - Basic States

This project demonstrates the basics of Lite.StateMachine using a
.NET application, including how to manage and transition between different
states such as initialization, running, and shutdown.

It serves as a foundational example for understanding the lifecycle of a
state-based application and how to handle state management effectively.

There are 2 applications in this sample:

* **Basic State Machine** - Flat state machine with no nested states, demonstrating simple state transitions.
  * NOTE: _**You MUST**_ handle the `context.NextState` property during the `OnEnter` transition to signify that the state has completed successfully.
* **Composite State Machine** - More complex state machine with nested states, showcasing hierarchical state management.
  * NOTE: _**You MUST**_ handle the `context.NextState` property during the `OnExit` transition to signify that the child states have completed successfully.

## Basic State Machine

The file `BasicStateMachine.cs` contains the implementation of a simple state
machine with three states: `State1`, `State2`, and `State3` for verbosity.

Each of the states will be fully implemented with `OnEntering`, `OnEnter`, and `OnExit` transitions.

## Composite State Machine

The file `CompositeStateMachine.cs` contains a more complex state machine that includes nested states.
This example demonstrates how to manage hierarchical states, where a parent state can contain multiple
child states, allowing for more complex behavior and transitions.

### Handling 'context.NextState'

NOTE:

Composite states can have multiple child states, MUST handle the `context.NextState` property
during the `OnExit` transition to signify that the child states have completed successfully.

This is a crucial step for _Composite States_ and differs from a regular "State" who performs
the `context.NextState` assignment during the `OnEnter` transition.
