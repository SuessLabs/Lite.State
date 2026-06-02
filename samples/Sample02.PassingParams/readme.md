# Sample 2 - Passing Parameters

This sample demonstrates how to pass the Context Parameters
(`context.Parameters`) using the `PropertyBag` class.

```cs
    var counter = 0;
    var ctxProperties = new PropertyBag()
    {
      { ParameterType.Counter, counter },     // (int)
      { ParameterType.LogOutput, logOutput }, // (bool)
    };
```
