# Entity Context Library #

**Entity Context Library (ECL)** provides a common and reusable interfaces for projects using [Entity Framework](https://docs.efproject.net).

> NOTE: **ECL** `1.x` document can be found at [README-1.x.md](README-1.x.md).


## Package Status ##

[![Build status](https://ci.appveyor.com/api/projects/status/06bu85cjywdlfa51/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/entity-context-library/branch/dev) [![](https://img.shields.io/nuget/v/Aliencube.EntityContextLibrary.svg)](https://www.nuget.org/packages/Aliencube.EntityContextLibrary/)


## Entity Framework Support ##

**ECL** `2.x` supports Entity Framework 7.


## Getting Started ##

**ECL** provides of four distinctive interfaces &ndash; `IDbContextFactory`, `IUnitOfWork` and `IUnitOfWorkManager`.


### `IDbContextFactory` ###

`IDbContextFactory` provides a simple way to consolidate all registered `DbContext` instances. With an IoC container like `Autofac` in ASP.NET Core application, it can be done in `Startup.cs` like:

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
  ...

  services.AddEntityFramework()
      .AddRelational()
      .AddSqlServer()
      .AddDbContext<ProductDbContext>(o => o.UseSqlServer(productDbConnectionString)())
      .AddDbContext<UserDbContext>(o => o.UseSqlServer(userDbConnectionString)());

  var builder = new ContainerBuilder();
  builder.Register(c => new DbContextFactory(
                            c.Resolve<ProductDbContext>(),
                            c.Resolve<UserDbContext>()))
    .As<IDbContextFactory>()
    .PropertiesAutowired()
    .InstancePerLifetimeScope();

  builder.Populate(services);
  return builder.Build().Resolve<IServiceProvider>();
}
```


### `IUnitOfWork` ###

`IUnitOfWork` interface handles database transactions for `INSERT`, `UPDATE` and `DELETE`. Therefore it provides transaction related methods like `BeginTransaction`, `Commit` and `Rollback`. You can use this within your service layer or database access layer like:

```csharp
using (var uow = new UnitOfWork<ProductDbContext>(dbContextFactory))
{
  uow.BeginTransaction();
  
  try
  {
    // DO TRANSACTIONS

    context.SaveChanges();
    uow.Commit();
  }
  catch
  {
    uow.Rollback();
    throw;
  }
}
```

If your application handles multiple `DbContext` instances, you can try the following approach:

```csharp
var types = new List<Type>() { typeof(ProductDbContext), typeof(UserDbContext) };
using (var uow = new UnitOfWork<ProductDbContext>(dbContextFactory, types))
{
  uow.BeginTransaction();
  
  try
  {
    // DO TRANSACTIONS

    productDbContext.SaveChanges();
    userDbContext.SaveChanges();
    uow.Commit();
  }
  catch
  {
    uow.Rollback();
    throw;
  }
}
```


### `IUnitOfWorkManager` ###

`IUnitOfWorkManager` interface provides methods, `CreateInstance<TContext>()`, `CreateInstance(Type)` and `CreateInstance(IEnumearable<Type>)` to manage `UnitOfWork` instance. With `Autofac`, you can put a line of code into the IoC container:

```csharp
builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>();
```

Once the `UnitOfWorkManager` is registered, your application use `UnitOfWork` instance like:

```csharp
using (var uow = uowm.CreateInstance<ProductDbContext>())
{
  uow.BeginTransaction();
  
  try
  {
    // DO TRANSACTIONS

    productDbContext.SaveChanges();
    userDbContext.SaveChanges();
    uow.Commit();
  }
  catch
  {
    uow.Rollback();
    throw;
  }
}
```

If your application deals with mutliple `DbContext` instances, you can try instead:

```csharp
var types = new List<Type>() { typeof(ProductDbContext), typeof(UserDbContext) };
using (var uow = uowm.CreateInstance(types))
{
  uow.BeginTransaction();
  
  try
  {
    // DO TRANSACTIONS

    productDbContext.SaveChanges();
    userDbContext.SaveChanges();
    uow.Commit();
  }
  catch
  {
    uow.Rollback();
    throw;
  }
}
```


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work, please send us a pull request onto our `dev` branch for review.


## License ##

**Entity Context Library (ECL)** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2014 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
