# T5.PR1..Pr-ctica-1

 Cómo se ha integrado EF Core en el proyecto y qué ha cambiado respecto a la versión anterior

Voy a intentar explicar cómo se ha implementado Entity Framework Core (EF Core) en el proyecto `T5.PR1.Practica\_1` y qué diferencias hay con el anterior `T4.PR1.Practica\_1`. No tengo mucha experiencia, pero estoy empezando a entender cómo funciona, así que lo voy a narrar según lo veo.

---

## Cómo se ha integrado EF Core en el proyecto nuevo (`T5`)

### 1. Configuración en `Program.cs`

En el archivo `Program.cs` del proyecto nuevo, hay una línea que parece esencial para que EF Core funcione:

```csharp

builder.Services.AddDbContext<EcoEnergyDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("EcoEnergyDatabase")));
```

Esto configura el programa para usar una base de datos SQL Server con una conexión que está en el archivo de configuración. Más abajo, se inicializa la base de datos con:

```csharp

DbInitializer.Initialize(context);
```

Por lo que entiendo, esto crea la base de datos y carga datos al arrancar.

### 2. El contexto EcoEnergyDbContext

Existe una clase nueva llamada EcoEnergyDbContext que parece ser el núcleo de EF Core en este proyecto. Incluye unas propiedades para las entidades que se van a guardar:

```csharp

public DbSet<SimulationBD> Simulations { get; set; }

public DbSet<WaterConsumptionBD> WaterConsumptions { get; set; }

public DbSet<EnergyIndicatorBD> EnergyIndicators { get; set; }
```

Esto parece indicar a EF Core qué tablas debe manejar. También hay un método OnModelCreating que establece reglas, como limitar la longitud de un campo o hacerlo obligatorio.

### 3\. Los modelos y su uso

Los modelos ahora son clases como EnergyIndicatorBD o SimulationBD, y tienen atributos como [Required] sobre los campos. Por ejemplo:

```csharp

public class EnergyIndicatorBD

{

public int Id { get; set; }

[Required]

public int Year { get; set; }

[Required]

public double NetProduction { get; set; }

}
```

Esto parece ayudar a EF Core a crear las tablas y evitar datos incorrectos. Para guardar algo, se usa el contexto así:

```csharp

_context.EnergyIndicators.Add(NuevoIndicador);

_context.SaveChanges();
```

Y con eso los datos se almacenan en la base de datos.

### 4\. La API añadida

Hay controladores como EnergyIndicatorsController que funcionan como una API. Por ejemplo, para obtener todos los indicadores:

```csharp

[HttpGet]

public async Task<ActionResult<IEnumerable<EnergyIndicatorBD>>> GetEnergyIndicators()

{

return await \_context.EnergyIndicators.ToListAsync();

}
```

Esto parece permitir que se pidan datos desde fuera por internet, algo que no existía antes.

### 5\. Carga inicial de datos

La clase DbInitializer toma datos de archivos CSV, como indicadors\_energetics\_cat.csv, y los inserta en la base de datos al inicio:

```csharp

context.EnergyIndicators.AddRange(records);

context.SaveChanges();
```

Esto parece práctico para empezar con datos ya cargados.

## Qué ha cambiado respecto al proyecto anterior (T4)

### 1. Almacenamiento de datos

Antes, todo se guardaba en archivos. Los indicadores estaban en indicadors\_energetics\_cat.csv y .json, las simulaciones en simulaciones\_energia.csv, y los consumos de agua en un CSV y un XML. Para añadir un indicador, se escribía en el CSV:

```csharp

System.IO.File.AppendAllText(CsvPath, "\n" + newLine);
```

O en el XML para el agua:

```csharp

doc.Root.Add(element);

doc.Save(rutaXML);
```

Ahora, todo se almacena en una base de datos con EF Core, y los archivos solo se usan al principio para cargar datos.

### 2. Lectura de datos

En el anterior, para leer datos había que abrir los archivos y procesarlos manualmente. Por ejemplo, en ShowIndicadorsEnergeModel:

```csharp
var lineas = System.IO.File.ReadAllLines(CsvPath);
foreach (var linea in lineas.Skip(1)) { /\* Procesar datos \*/ }
```
O para el XML:

```csharp
XDocument doc = XDocument.Load(rutaXML);
```

Era un proceso manual y algo complicado. Ahora, con EF Core, solo se escribe:

```csharp

IndicadoresEnergeticos = \_context.EnergyIndicators.ToList();
```

Y la base de datos se encarga del resto.

### 3. Estructura de los modelos

Antes, los modelos como IndicadorEnergetic tenían nombres como CDEEBC\_ProdNeta y algunos campos opcionales que no siempre se usaban. Ahora, en EnergyIndicatorBD, los nombres son más claros, como NetProduction o GasolineConsumption, y todo está bien definido. El agua antes tenía campos como CodiComarca, pero ahora se reduce a Region y Municipality.

#### 1. Gestión de simulaciones

En el anterior, había clases como HydroelectricSystem que calculaban la energía y se guardaban en un CSV:

```csharp

sistema.Simulate(Parametro);

SimulationDataHandler.SaveSimulation(simulacion);
```

Ahora, eso se hace directamente en AddSimulationModel y se guarda en la base de datos:

```csharp

Simulation.GeneratedEnergy = Simulation.Type switch { /\* Cálculo \*/ };

_context.Simulations.Add(Simulation);
```

Las clases complejas ya no están, y todo parece más directo.
