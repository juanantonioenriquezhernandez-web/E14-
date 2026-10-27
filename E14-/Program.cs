using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("SISTEMA DE MONITOREO DE SENSORES INDUSTRIALES");
        Console.WriteLine("1. Sensor de temperatura");
        Console.WriteLine("2. Sensor ultrasónico");
        Console.WriteLine("3. Sensor de presión");
        Console.WriteLine("4. Sensor de presencia");
        Console.WriteLine("5. Sensor de distancia");
        Console.Write("Seleccione el tipo de sensor (1-5): ");

        // Lee solo una tecla sin esperar Enter
        char teclaOpcion = Console.ReadKey().KeyChar;
        Console.WriteLine(); // Imprime un salto de línea para mantener el orden visual

        // Convierte el caracter a entero (si es inválido, será 0 y caerá en el default)
        int.TryParse(teclaOpcion.ToString(), out int opcion);
        Console.WriteLine();

        switch (opcion)
        {
            case 1:
                SensorTemperatura temp1 = new SensorTemperatura();
                temp1.CapturarDatosBase();
                Console.Write("Ingrese la temperatura (°C): ");
                temp1.ValorMedio = Convert.ToDouble(Console.ReadLine());

                temp1.MostrarDatos();
                Console.WriteLine($"Temperatura: {temp1.ValorMedio} °C");
                Console.WriteLine($"Evaluación: {temp1.EvaluarTemperatura()}");
                break;

            case 2:
                SensorUltrasonico ultra1 = new SensorUltrasonico();
                ultra1.CapturarDatosBase();
                Console.Write("Ingrese la proximidad entre objetos (cm): ");
                ultra1.ValorMedio = Convert.ToDouble(Console.ReadLine());

                ultra1.MostrarDatos();
                Console.WriteLine($"Proximidad: {ultra1.ValorMedio} cm");
                Console.WriteLine($"Evaluación: {ultra1.EvaluarProximidad()}");
                break;

            case 3:
                SensorPresion pres1 = new SensorPresion();
                pres1.CapturarDatosBase();
                Console.Write("Ingrese la presión (bar): ");
                pres1.ValorMedio = Convert.ToDouble(Console.ReadLine());

                pres1.MostrarDatos();
                Console.WriteLine($"Presión: {pres1.ValorMedio} bar");
                Console.WriteLine($"Evaluación: {pres1.EvaluarPresion()}");
                break;

            case 4:
                SensorPresencia presencia1 = new SensorPresencia();
                presencia1.CapturarDatosBase();
                Console.Write("¿Se detecta una persona? (1 = Sí, 0 = No): ");

                // Lectura automática sin Enter
                char teclaPresencia = Console.ReadKey().KeyChar;
                Console.WriteLine();
                presencia1.ValorMedio = (teclaPresencia == '1') ? 1 : 0;

                presencia1.MostrarDatos();
                Console.WriteLine($"Detección: {(presencia1.ValorMedio == 1 ? "Persona detectada" : "Sin personas")}");
                Console.WriteLine($"Evaluación: {presencia1.EvaluarPresencia()}");
                break;

            case 5:
                SensorDistancia dist1 = new SensorDistancia();
                dist1.CapturarDatosBase();
                Console.Write("Ingrese la distancia (cm): ");
                dist1.ValorMedio = Convert.ToDouble(Console.ReadLine());

                dist1.MostrarDatos();
                Console.WriteLine($"Distancia: {dist1.ValorMedio} cm");
                Console.WriteLine($"Evaluación: {dist1.EvaluarDistancia()}");
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
}

// CLASE BASE
class Sensor
{
    public string Identificador { get; set; } = "";
    public double ValorMedio { get; set; }
    public bool Activo { get; set; }

    public void CapturarDatosBase()
    {
        Console.Write("Ingrese el identificador del sensor: ");
        Identificador = Console.ReadLine() ?? "Sin identificador";

        Console.Write("¿El sensor está activo? (1 = Sí, 0 = No): ");

        // Lectura automática sin Enter
        char teclaActivo = Console.ReadKey().KeyChar;
        Console.WriteLine();
        Activo = (teclaActivo == '1'); // Si teclea '1' es true, cualquier otra cosa es false
    }

    public void MostrarDatos()
    {
        Console.WriteLine("\nDATOS DEL SENSOR");
        Console.WriteLine($"Sensor: {Identificador}");
        Console.WriteLine($"Valor medido: {ValorMedio}");
        Console.WriteLine($"Estado: {ObtenerEstado()}");
    }

    public string ObtenerEstado()
    {
        return Activo ? "Sensor activo" : "Sensor inactivo";
    }
}

// CLASES DERIVADAS
class SensorTemperatura : Sensor
{
    public string EvaluarTemperatura()
    {
        if (ValorMedio > 80)
            return "REQUIERE ATENCIÓN: Supera el límite de 80 °C";
        else
            return "Dentro del criterio establecido";
    }
}

class SensorUltrasonico : Sensor
{
    public string EvaluarProximidad()
    {
        if (ValorMedio < 30)
            return "REQUIERE ATENCIÓN: Objeto demasiado cerca (menos de 30 cm)";
        else
            return "Dentro del criterio establecido";
    }
}

class SensorPresion : Sensor
{
    public string EvaluarPresion()
    {
        if (ValorMedio > 10)
            return "REQUIERE ATENCIÓN: Supera el límite de 10 bar";
        else
            return "Dentro del criterio establecido";
    }
}

class SensorPresencia : Sensor
{
    public string EvaluarPresencia()
    {
        if (ValorMedio == 1)
            return "REQUIERE ATENCIÓN: Hay una persona en el área";
        else
            return "Dentro del criterio establecido: área libre";
    }
}

class SensorDistancia : Sensor
{
    public string EvaluarDistancia()
    {
        if (ValorMedio < 20)
            return "REQUIERE ATENCIÓN: Objeto a menos de 20 cm";
        else
            return "Dentro del criterio establecido";
    }
}