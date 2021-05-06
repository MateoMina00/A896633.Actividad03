using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace A896633.Actividad03
{
    static class LibroContable
    {
        public static readonly Dictionary<int, CuentasContables> registro = new Dictionary<int, CuentasContables>();
        public static int TotalDebe;
        public static int TotalHaber;
        public static int ContadorAsientos = 0;
        const string nombreArchivo = "Diario.txt";

        static LibroContable()
        {
            //Formato
            //Codigo|Nombre|tipo
            string ruta = @"C:\Users\mateo\source\repos\CAI\A896633.Actividad03\Actividad 03 - Plan de cuentas.txt";
            if (File.Exists(ruta))
            {
                StreamReader reader = new StreamReader(ruta);
                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine(); //A partir de cada linea, tengo que construir un diccionario, que me permita validar que existe.                    
                    var asiento = new CuentasContables(linea); //Constructor con un parametro, es leyendo el archivo
                    registro.Add(asiento.CodigoCuenta, asiento); //Lo Agrego a la entrada.
                }

            }
            else Console.WriteLine("No se encontro archivo para leer");
        }
        public static void mostrarCuentas()
        {
            foreach (var cuenta in registro)
            {
                if (cuenta.Key == 0)
                    continue;
                else
                {
                    Console.WriteLine($"Codigo: {cuenta.Value.CodigoCuenta} - Nombre: {cuenta.Value.Nombre} -Tipo: {cuenta.Value.Tipo}");
                }
            }
        }
        public static CuentasContables seleccionar()
        {
            int nroCuentaIngresado;
            bool ciclo = false;
            do
            {
                Console.WriteLine("Favor de ingresar el codigo de cuenta que desea operar");
                nroCuentaIngresado = Helper.ValidarNumero();
                if (!LibroContable.registro.ContainsKey(nroCuentaIngresado))
                {
                    Console.WriteLine("Cuenta no existente.");
                }
                else ciclo = true;

            } while (!ciclo);
            return registro[nroCuentaIngresado];
        }
        public static CuentasContables seleccionar(CuentasContables contrapartida)
        {
            if (contrapartida.Tipo == "Activo") //Si es activo, suma en el debe. Entonces la contrapartida son de tipo Pasivo / PatrimonioNeto
            {
                Console.Clear();
                Console.WriteLine("Favor de seleccionar una cuenta en concepto de contrapartida. Ingrese su codigo");
                foreach (var asiento in LibroContable.registro)
                {
                    if (asiento.Value.Tipo == "Pasivo" || asiento.Value.Tipo == "PatrimonioNeto")
                        Console.WriteLine($"Codigo: {asiento.Value.CodigoCuenta} - Nombre: {asiento.Value.Nombre} - Tipo: {asiento.Value.Tipo}");
                    else continue;
                }
                int codigo = pedirCodigoContrapartida(contrapartida);
                return LibroContable.registro[codigo];

            }
            else
            {
                if (contrapartida.Tipo == "Pasivo" || contrapartida.Tipo == "PatrimonioNeto")
                {
                    Console.Clear();
                    Console.WriteLine("Favor de seleccionar una cuenta en concepto de contrapartida. Ingrese su codigo");
                    foreach (var asiento in LibroContable.registro)
                    {
                        if (asiento.Value.Tipo == "Activo")
                            Console.WriteLine($"Codigo: {asiento.Value.CodigoCuenta} - Nombre: {asiento.Value.Nombre} - Tipo: {asiento.Value.Tipo}");
                        else continue;
                    }

                }
                int codigo = pedirCodigoContrapartida(contrapartida);
                return LibroContable.registro[codigo];

            }

        }

        private static int pedirCodigoContrapartida(CuentasContables contrapartida)
        {
            int CodigoRetorno = 0;
            if (contrapartida.Tipo == "Activo")
            {
                int CodigoIngresado;
                bool ciclo = false;
                do
                {

                    CodigoIngresado = Helper.ValidarNumero();
                    bool existeRegistro = LibroContable.registro.ContainsKey(CodigoIngresado);
                    if (existeRegistro && LibroContable.registro[CodigoIngresado].Tipo == "Pasivo" || LibroContable.registro[CodigoIngresado].Tipo == "PatrimonioNeto")
                    {

                        Console.WriteLine("Codigo ingresado correctamente");
                        CodigoRetorno = CodigoIngresado;
                        ciclo = true;
                    }
                    else
                    {
                        Console.WriteLine("Error al ingresar codigo");
                    }

                } while (!ciclo);

            }
            else
            {
                bool ciclo = false;
                int CodigoIngresado;
                do
                {
                    CodigoIngresado = Helper.ValidarNumero();
                    bool existeRegistro = LibroContable.registro.ContainsKey(CodigoIngresado);

                    if (existeRegistro && LibroContable.registro[CodigoIngresado].Tipo == "Activo")
                    {
                        Console.WriteLine("Codigo ingresado correctamente");
                        ciclo = true;
                        CodigoRetorno = CodigoIngresado;

                    }
                    else
                    {
                        Console.WriteLine("Codigo no valido");
                    }

                } while (!ciclo);

            }

            return CodigoRetorno;
        }
        public static bool validar(CuentasContables cuenta, CuentasContables contrapartida)
        {
            bool validacion;
            if (LibroContable.TotalDebe == LibroContable.TotalHaber)
            {
                validacion = true;
                Console.Clear();
                Console.WriteLine($"La operacion ha sido completada.");
                Console.ReadKey();
            }
            else
            {

                Console.Clear();
                Console.WriteLine("Los montos del debe y haber no coinciden. Favor de revisar");
                Console.WriteLine($"Operacion 1--> Codigo:{cuenta.CodigoCuenta} - Nombre: {cuenta.Nombre} - Tipo {cuenta.Tipo} - Debe: {cuenta.Debe} - Haber: {cuenta.Haber}");
                Console.WriteLine($"Operacion 2--> Codigo:{contrapartida.CodigoCuenta} - Nombre: {contrapartida.Nombre} - Tipo: {contrapartida.Tipo} - Debe: {contrapartida.Debe} - Haber: {contrapartida.Haber}");
                Console.WriteLine("Pulse para continuar");
                Console.ReadKey();
                validacion = false;

            }
            return validacion;
        }
        public static CuentasContables GestionarDiferenciaDebeHaber(CuentasContables cuenta, CuentasContables contrapartida)
        {
            Console.Clear();
            if (LibroContable.TotalDebe < LibroContable.TotalHaber)
            {

                Console.WriteLine("Favor de seleccionar una cuenta. Ingrese su codigo");
                foreach (var asiento in LibroContable.registro)
                {
                    if (asiento.Value.Tipo == "Activo")
                        Console.WriteLine($"Codigo: {asiento.Value.CodigoCuenta} - Nombre: {asiento.Value.Nombre} - Tipo: {asiento.Value.Tipo}");
                    else continue;
                }
                bool ciclo = false;
                int codigo;
                do
                {
                    codigo = Helper.ValidarNumero();
                    if (LibroContable.registro[codigo].Tipo == "Activo")
                        ciclo = true;
                    else Console.WriteLine("Codigo incorrecto");
                } while (!ciclo);

                return LibroContable.registro[codigo];
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Favor de seleccionar una cuenta. Ingrese su codigo");
                foreach (var asiento in LibroContable.registro)
                {
                    if (asiento.Value.Tipo == "Pasivo" || asiento.Value.Tipo == "PatrimonioNeto")
                        Console.WriteLine($"Codigo: {asiento.Value.CodigoCuenta} - Nombre: {asiento.Value.Nombre} - Tipo: {asiento.Value.Tipo}");
                    else continue;
                }
                bool ciclo = false;
                int codigo;
                do
                {
                    codigo = Helper.ValidarNumero();
                    if (LibroContable.registro[codigo].Tipo == "Pasivo" || LibroContable.registro[codigo].Tipo == "PatrimonioNeto")
                        ciclo = true;
                    else Console.WriteLine("Codigo incorrecto");
                } while (!ciclo);

                return LibroContable.registro[codigo];
            }

        }
        public static void grabar()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                //Cuando modifico algo, voy y grabo todo de vuelta. (append false)
                foreach (var cuentas in registro.Values)
                {
                    var lineastring = cuentas.Serializar();
                    writer.WriteLine(lineastring);
                }
            }
        }
    }
}
