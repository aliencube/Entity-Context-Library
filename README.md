# Entity Context Library #

**Entity Context Library (ECL)** provides a common and reusable interfaces for projects using [Entity Framework](http://www.asp.net/entity-framework).


## Package Status ##

* **Entity Context Library** [![](https://img.shields.io/nuget/v/Aliencube.EntityContextLibrary.svg)](https://www.nuget.org/packages/Aliencube.EntityContextLibrary/) [![](https://img.shields.io/nuget/dt/Aliencube.EntityContextLibrary.svg)](https://www.nuget.org/packages/Aliencube.EntityContextLibrary/)


## Getting Started ##

**ECL** provides of four distinctive interfaces &ndash; `IDbContextFactory`, `IRepositoryBase`, `IUnitOfWork` and `IUnitOfWorkManager`.


### `IDbContextFactory` ###

`IDbContextFactory` interface provides a simple interface to return a `DbContext` instance based on type. This can be useful when multiple database connection strings are used in one application. Here's a sample code snippet, assuming [`Autofac`](http://autofac.org) is used together, as an IoC container.

```csharp
using Autofac;
...

public static class Program
{
  private const string SERVICE_NAME = "MyService";

  public static void Main(string[] args)
  {
    var builder = new ContainerBuilder();

    // Register ApplicationDbContext with DbContextFactory.
    builder.RegisterType<DbContextFactory<ApplicationDbContext>>()
           .Named<IDbContextFactory>(SERVICE_NAME)
           .As<IDbContextFactory>();

    ...

    _container = builder.Build();
  }
}

```


### `IRepositoryBase` ###

`IRepositoryBase` interface provides a basic CRUD methods for each repository representing a table in a database. Therefore, each repository can just inherit the base repository class and use it. In addition to this, all methods like `Get`, `Add`, `Update` and `Delete` methods are overrideable, so you can redefine your way of `SELECT`, `INSERT`, `UPDATE` and `DELETE` actions. Here's a sample usage.

```csharp
public interface IProductRepository : IRepositoryBase<Product>
{
  // You can put as many methods as you want here.
}

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
  public ProductRepository(IDbContextFactory contextFactory)
    : base(contextFactory)
  {
  }

  // You can here implement methods defined in the interface above. 
}
```

Of course you can put as many methods as you want in the repository, like `AddRange(IEnumerable<Product> products)`.

With `Autofac`, you can put a line of code into the IoC container:

```csharp
// Register ProductRepository.
builder.Register(p => new ProductRepository(p.ResolveNamed<IDbContextFactory>(SERVICE_NAME)))
       .As<IProductRepository>();
```


### `IUnitOfWorkManager` ###

`IUnitOfWorkManager` interface only provides one method, `CreateInstance` to create and dispose `UnitOfWork` instance. With `Autofac`, you can put a line of code into the IoC container:

```csharp
// Register UnitOfWorkManager.
builder.Register(p => new UnitOfWorkManager(p.ResolveNamed<IDbContextFactory>(SERVICE_NAME)))
       .As<IUnitOfWorkManager>();
```

### `IUnitOfWork` ###

`IUnitOfWork` interface handles database transactions for `INSERT`, `UPDATE` and `DELETE`. Therefore it provides transaction related methods like `BeginTransaction`, `SaveChanges`, `Commit` and `Rollback`. You can use this within your database access layer like:

```csharp
// ProductQueryManager performs INSERT/UPDATE/DELETE actions.
public class ProductQueryManager
{
  private readonly IUnitOfWorkManager _uowm;
  private readonly IProductRepository _product;

  public ProductQueryManager(IUnitOfWorkManager uowm, IProductRepository product)
  {
    if (uowm == null)
    {
      throw new ArgumentNullException("uowm");
    }
    this._uowm = uowm;

    if (product == null)
    {
      throw new ArgumentNullException("product");
    }
    this._product = product;
  }

  // Adds a product into the table.
  public bool Add(Product product)
  {
    using (var uow = this._uowm.CreateInstance<ApplicationDbContext>())
    {
      uow.BeginTransaction();

      try
      {
        this._productRepository.Add(product);
        uow.Commit();
        return true;
      }
      catch (Exception ex)
      {
        uow.Rollback();

        //
        // Do some error handling logic here.
        //

        return false;
      }
    }
  }

  // Updates a product on the table.
  public bool Update(Product product)
  {
    using (var uow = this._uowm.CreateInstance<ApplicationDbContext>())
    {
      uow.BeginTransaction();

      try
      {
        this._productRepository.Update(product);
        uow.Commit();
        return true;
      }
      catch (Exception ex)
      {
        uow.Rollback();

        //
        // Do some error handling logic here.
        //

        return false;
      }
    }
  }

  // Deletes a product from the table.
  public bool Delete(Product product)
  {
    using (var uow = this._uowm.CreateInstance<ApplicationDbContext>())
    {
      uow.BeginTransaction();

      try
      {
        this._productRepository.Delete(product);
        uow.Commit();
        return true;
      }
      catch (Exception ex)
      {
        uow.Rollback();

        //
        // Do some error handling logic here.
        //

        return false;
      }
    }
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
