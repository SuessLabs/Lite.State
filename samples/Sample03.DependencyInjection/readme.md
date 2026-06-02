# Sample 3  - State Machine with Dependency Injection

This project demonstrates using Microsoft.Extensions.DependencyInjection to manage dependencies throughout state machine transitions.

The sample includes a simple state machine that uses dependency injection to manage the `CounterService` required by the state machine. It increments a counter for each state's transition and elements of the state (`OnEntering`, `OnEnter`, `OnExit` and outputs the resuts to command line.

