# Learn C#

Note, code samples and utils while learning C#.

## TODO

- [ ] Code org:
  - [ ] Split by solutions, dirs, or namespaces? How to make sense for all of
        this? What is reasonable?
  - [ ] `Directory.Build.Props`, nested?
    - Why can't we build `./example/APIs/Minimal` using `dotnet build` within
      that folder?

## Dependency Injection

- Among all the construtors, the one with **most parameters** and is
  **resolvable** will be used.
- Throw on unable to resolve: ambigous or lacking of dependencies.

TODO:

- Currently, MS examples show how to use it via `Host` builder. How to use DI
  without those APIs? What are the bare minimum packages to import?
