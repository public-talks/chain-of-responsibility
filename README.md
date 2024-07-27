# Chain of Responsibility

Abstract: The Chain of Responsibility pattern may be a way to alleviate complex use cases into small, testable blocks.

## The Problem

Some use cases in your software will be complex and have lots of steps. This can result in lots of injected services, private methods, and large code blocks.

```csharp
public class MyService
{
    private readonly FirstDependency _first;
    private readonly SecondDependency _second;
    private readonly ThirdDependency _third;

    public MyService(FirstDependency first, SecondDependency second, ThirdDependency third)
    {
        _first = first;
        _second = second;
        _third = third;
    }

    public void DoSomething()
    {
        // implementation.
    }
}
```

or...

```csharp
public class MyService()
{
    public SomeObject DoSomething()
    {
        StepOne();

        StepTwo();
    }

    private SomeObject StepOne()
    {
        // implementation
    }

    private SomeObject StepTwo()
    {
        // implementation
    }
}
```

The problem is that these methods are very hard, if not impossible to test.

```csharp
[TestClass]
public class MyServiceTests
{
    private readonly Mock<FirstDependency> _first;
    private readonly Mock<SecondDependency> _second;
    private readonly Mock<ThirdDependency> _third;

    public MyServiceTests()
    {
        _first = new Mock<FirstDependency>();
        _second = new Mock<SecondDependency>();
        _third = new Mock<ThirdDependency>();
    }

    [TestMethod]
    public void MyMethodTests()
    {
        _first.Setup(x => x.Foo());
        _second.Setup(x => x.Foo());
        _third.Setup(x => x.Foo());
    }
}
```

Classes with lots of dependencies are tedious to mock and are brittle and easy to break. Private methods can not be tested at all.

All software should be testable first, testing is the best way to buy insurance for your application.

### Issues That Arise

- Services typically have abstract naming schemes and are non-declarative
- Aggregate Services just "rearrange" the Titanic deck chairs
- Classes with 5+ dependencies are hard to test and are brittle by nature
- Private methods are unable to be tested

### The Solution

Using the Chain of Responsibility pattern solves the problem of complex use cases while allowing for complete automated testing.

#### Code

All chains start with a payload object that is the primary delivery method.

```csharp
public abstract class ChainPayload
{
    public bool IsFaulted { get; set; }
}
```

All handlers will derive from a simple interface.

```csharp
public interface IChainHandler<T> where T : ChainPayload
{
    Task<T> Execute(T payload);
}
```

To keep with the ethos of Dependency Injection, our chains will each use the factory that we will implement ourselves.

```csharp
public interface IChainFactory<T> where T : ChainPayload
{
    Task<T> ExecuteChain(T payload);
}
```

Every handler will derive from a base handler that will execute a method and return the payload.

```csharp
public abstract class ChainHandler<T> : IChainHandler<T> where T : ChainPayload
{
    private readonly IChainHandler<T>? _chainHandler;

    protected ChainHandler(IChainHandler<T>? chainHandler)
    {
        _chainHandler = chainHandler;
    }

    public async Task<T> Execute(T payload)
    {
        if (payload.IsFaulted)
        {
            return payload;
        }

        var result = await DoWork(payload);

        return _chainHandler == null ? result : await _chainHandler.Execute(result);
    }

    protected abstract Task<T> DoWork(T payload);
}
```

#### Implementation

For each chain, you will simply need to:

- Create a payload object that contains all initial, temporary, and final data
- Create any handler required and implement the required method
- Create a factory object to initialize your chain, and register it with your DI container

## Chain Strategy

For dotnet, save the effort and use [ChainStrategy](https://github.com/mjbradvica/ChainStrategy). ChainStrategy comes with everything out of the box ready to go.

## Conclusion

Dealing with complex logic flows is hard enough, trying to design them in a way so they can be easily tested is harder. The Chain of Responsibility pattern is a time-tested way to both implement hard logic and allow for test automated testing.