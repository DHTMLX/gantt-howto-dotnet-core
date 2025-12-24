# dhtmlxGantt with ASP.NET Core

ASP.NET Core backend for dhtmlxGantt.

### Requirements

- MS VisualStudio 2022
- .NET 8.0 SDK or later

### How to run

#### Using Visual Studio 2022:

Requires MS Visual Studio 2022. The version of the MS Visual Studio should support .NET 8.0.

1. Clone the demo repository
2. Run the application pressing `ctrl + f5`, or clicking on the Run button on the top in the panel interface

#### Using Visual Studio Code:

Alternatively, install the dotnet runtime manually: https://learn.microsoft.com/en-us/dotnet/core/install/

To check if .NET is installed, open terminal and run:

```bash
dotnet --version
```

**Steps to run:**

1. Open the project folder in VS Code
2. Open the terminal (Ctrl+` or View → Terminal)
3. Navigate to the project folder and run:

```bash
cd DHX.Gantt
dotnet restore
dotnet run  # or use 'dotnet watch' for hot reload
```

Using `dotnet watch` enables automatic code reloading when you modify C# files.

4. Open the browser at https://localhost:7296 (or http://localhost:5296)

### Related resources

[Complete tutorial](https://docs.dhtmlx.com/gantt/integrations/dotnet/howtostart-dotnet-core/)

[DHTMLX Gantt product page](https://dhtmlx.com/docs/products/dhtmlxGantt/)

[Documentation](https://docs.dhtmlx.com/gantt/)

[Blog](https://dhtmlx.com/blog/)

[Forum](https://forum.dhtmlx.com/)
