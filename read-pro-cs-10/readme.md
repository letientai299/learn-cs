# Reading Pro C# - 11th ed

[The book](https://learning.oreilly.com/library/view/pro-c-10/9781484278697).

## Todo

- [ ] Best practice in organizing class, namespace, folder and sub projects.
- [x] Learn how to config formatter

  How to deal with long args list? -> Use `csharpier`.

  - [x] Config vim
  - [x] Config for CI, git hooks, ...

## Terms

Collected through the book.

- CLR: Common Language Runtime
- CLS: Common Language Specification
- CTS: Common Type System
- BCL: Base Class Libraries
- DLR: Dynamic Language Runtime
- TPL: Task Parallel Library
- CIL: Common Intermediate Language
- ADO.NET: DB access abstraction, similar to JDBC.
  - Active Data Object
- ASP.NET: Active server page network enabled technology. Nowadays, .NET is a
  brand instead of an acronym.
- GAC: Global Assembly Cache
- NRT: Nullable Reference Type

## Front matter

Should revisit this part often to see if I should skip some part about out dated
tech and learn similar but more modern one.

https://learning.oreilly.com/library/view/pro-c-10/9781484278697/html/340876_11_En_BookFrontmatter_OnlinePDF.xhtml

## Part I. Introducing C# and .NET 6

### 1. Introducing C# and .NET 6

Done

### 2. Building C# Applications

Done

## Part II. Core C# Programming

### 3. Core C# Programming Constructs, Part 1

`System.ValueType` descendants are allocated on stack, must be enum, struct or
primitive.

### 4. Core C# Programming Constructs, Part 2

Why introduce the `Index` type? -> to support `^i` operator.

https://stackoverflow.com/a/388781/3869533

> The ref modifier means that:
>
> - The value is already set and
> - The method can read and modify it.
>
> The out modifier means that:
>
> - The Value isn't set and can't be read by the method until it is set.
> - The method must set it before returning.

The `in` param modifier

> In general, most developers agree that introducing of in could be seen as a
> mistake. It's a rather exotic language feature and can only be useful in
> high-perf edge cases.

`in` should mostly be used with **readonly**/immutable struct, otherweise, the
performance might be worse. Anyway, need to benchmark before optimizing.

The book is out of date regarding how to deconstruct an object. Now, any class
or struct can be deconstructed provided it implement a `Deconstruct` method
folowing c# rules.

Oh, I also learnd about the Primary Constructor feature from VS refactoring
suggestion. Having a mixed feeling about it:

- doens't support access modifier
- is mutable.

I'm not sure yet when to use it, compare to `record`.

## Part III. Object Oriented Programming with C#

### 5. Understanding Encapsulation

- `static` field's value can be obtained at runtime.
- `static` constructor: only one, run before any other constructor and first
  access to any `static` member.
- `private protected`: within inheritance chain in the same assembly.

- [ ] TODO: https://haacked.com/archive/2012/07/05/turkish-i-problem-and-why-you-should-care.aspx/

### 6. Understanding Inheritance and Polymorphism

- The _shortcut_ demo of overriding `Equals` that use `ToString` is bad, because
  it's slow, requires more memory for building string.

### 7. Understanding Structured Exception Handling

Java's exception doesn't translate to fatal/non-fatal. Unchecked exception
simply means don't need to catch it at the call site (let it bubble up) but we
can if needed.

C#'s unchecked exception can't be caught and always crash the program.

- [ ] TODO: https://www.artima.com/articles/the-trouble-with-checked-exceptions

Exception C# has a `HelpLink` property! Quite thoughtful.

## Part IV. Advanced C# Programming

ch 10 to 15

## Part V. Programming with .NET Core Assemblies

ch 16 to 18

## Part VI. File Handling, Object Serialization, and Data Access

ch 19 to 20

## Part VII. Entity Framework Core

ch 21 to 24

## Part VIII. Windows Client Development

ch 25 to 29

## Part IX. ASP.NET Core

ch 30 to 34
