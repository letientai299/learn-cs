# Reading Pro C# - 11th ed

[The book](https://learning.oreilly.com/library/view/pro-c-10/9781484278697).

## Todo

- [ ] Best practice in organizing class, namespace, folder and sub projects.
- [x] Learn how to config formatter

  How to deal with long args list? -> Use `csharpier`.

  - [x] Config vim
  - [x] Config for CI, git hooks, ...

- [ ] Use packet instead of nuget?

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

  - This version of the book uses an outdated convention of private variable.
    It's not recommended to prefix them with undrescore anymore.

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

### 8. Working with Interfaces

- Interface can have `static` constructor?
  -> Yes, to support setting default `static` members.
- Interface with default implementation is explicit (can only be used as an
  interface object, can't directly call method without casting).
- Explicit interface implementation is to solve the [diamond problem][diamond]

[diamond]: https://en.wikipedia.org/wiki/Multiple_inheritance#The_diamond_problem

> TODO (tai): `yield` and guard clause
> https://codealoc.wordpress.com/2012/05/17/ienumerable-guard-clause-best-practice

### 9. Understanding Object Lifetime

why don't call `Dispose` in `Finalize`:

https://stackoverflow.com/questions/732864/finalize-vs-dispose#comment544454_732878

> Dispose may also dispose managed resources, which you don't want to touch from
> your finalizer, as they may already have been finalized themselves.

There's `Lazy<>` util class to defer object creation.

### 10. Collections and Generics

Nothing fancy.

### 11. Advanced C# Language Features

Many code examples are too verbose:

- The explicit and implicit conversion would be simpler to demonstate (with less
  code) by using concept like Celsius to Fahrenheit degree instead of Square and
  Rectangle.
- The `ReverseDigits` is bad in term of performance. I know that the target
  audience of the book might not be skilled, but it's hard to ignore.

`ref` vs pointer: https://stackoverflow.com/a/430115/3869533

- `ref` can be moved/relocated by GC
- Pointer is static, thus, need `fixed` keywords to prevent outer object from
  being moved.

### 12. Delegates, Events, and Lambda Expressions

`delegate`, `event` and then lambda. All are partly syntactic sugar and
partly compiler magic with some special treatment for builtin types.

### 13. LINQ to Objects

I can't help compare LINQ to SQL.

- Like `offset` and `limit` in SQL, `Skip` and `Take` in LINQ is slow because it
  needs to execute the filter on all skipped values before reaching the wanted
  parts.
- `Chunk` is not better, it's basically a pre-processed list.

> TODO (tai): I need to find a way to do seek based pagination with LINQ. That means I need to
> find the "indexing" equivalent in LINQ.

### 14. Processes, AppDomains, and Load Contexts

> TODO (tai): what was changed between .NET an .NET Core, and why?

### 15. Multithreaded, Parallel, and Async Programming

Don't use `async void`, or excpetion won't be catchable.
Use `async Task<T>` or `async ValueTask<T>` instead.

### 16. Building and Configuring Class Libraries

> TODO (tai): here,

## Part IV. Advanced C# Programming

### 10. Collections and Generics

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
