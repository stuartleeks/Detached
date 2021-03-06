
# EntityFrameworkCore.Detached

Loads and saves entire detached entity graphs (the entity with their child entities and lists). 
Inspired by [GraphDiff](https://github.com/refactorthis/GraphDiff).
The idea is also add some plugins to simplificate some repetitive tasks, like auditing and pagination.

WARNING: This is a development project, not guaranteed to work for your particular purposes.
If you'd like to participate or need some support, please drop me an email: mail@leonardoporro.com.ar
or join https://github.com/leonardoporro/EntityFrameworkCore.Detached.
Thanks in advance for your help!

# Features
* Loading entity graphs.
* Updating entity graphs.
* Plugins:
	- Audit: Sets CreatedBy, CreatedDate, ModifiedBy, ModifiedDate.
	- ManyToMany Patch: Copies an intermediate table back and forth to a given collection
	simulating a skip-level navigation. Will be removed when EF Core supports many to many relations.
	- PartialKey: Allows adding [PKey(n)] attribute to define composite keys.
	- Pagination: Loads pages of data in an homogeneus way.
    - [Here your own plugin!].
	 
# Basic Usage
```csharp
// Create a detached wrapper for your context.
IDetachedContext<YourDbContext> context = new DetachedContext<YourDbContext>(new YourDbContext());

// Get some detached entity root. e.g.: Company has an [Owned] list of Employees.
Company company = new Company {
	Employees = new[] {
    	new Employee { Id = 1, Name = "John Snow" },
        new Employee { Id = 0, Name = "Daenerys Targaryen" } //new!
    }
}

// Save the detached entity to the database.
await context.UpdateAsync(company);
await context.SaveChangesAsync();  
```
You can also get detached root entities:

```csharp
DetachedContext context = new DetachedContext(new YourDbContext());

// This loads the company with Id: 1 along with the employees (and any other relation).
Company company = await context.LoadAsync<Company>(1); 

```

History:
- 1.0.0-preview2: 
 * Working on a cool Angular 2 demo.

- 1.0.0-preview1:
 * New service oriented architecture.
 * Plugins!
 * Better many to many patch.

- 1.0.0-alpha12: 
 * Added projections to LoadAsync and LoadPageAsync.

- 1.0.0-alpha10:
 * LoadPageAsync: Loads paged results (with filtered and sorted items) automatically.

- 1.0.0-alpha8:
 * DeleteAsync: Deletes an entity by key (pending since alpha3).
 * LoadAsync error fixed when the key value passed and the property type was not matching.

- 1.0.0-alpha5: 
 * Audit. Supports automatic setting properties marked as [CreatedBy] [CreatedDate]
 * [ModifiedBy] and [ModifiedDate]. An instance of IDetachedSessionInfoProvider is needed to get the current logged user name.
 * Supports dependency injection.

- 1.0.0-alpha4
 * [ManyToMany] PATCH to work with a simple 1-level many to many association such as User -> Roles.
 (while we wait for the feature).

- 1.0.0-alpha3
 * [Owned] and [Associated] attributes to define scope when loading a graph.
 * LoadAsync<TEntity>(key): loads a single detached root by its key.
 * LoadAsync<TEntity>(filter): loads a single detached root filtered by an expression.
 * UpdateAsync: persists a detached root (with its children) to the database. 



# Configuration:
To be able to process the [Owned] and [Associated] attributes, a custom convention builder is needed.
You can replace the ICoreConventionSetBuilder service by overriding OnConfiguring in your DbContext:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseDetached().UseSqlServer(...);
}
```
Or if you are using your own service collection: 

```csharp

            var serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .AddEntityFrameworkDetached()
                 .BuildServiceProvider();
```

# Nuget package:
https://www.nuget.org/packages/EntityFrameworkCore.Detached/