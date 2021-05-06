using System;

namespace A896633.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sistema de Gestion Contable");
            Console.WriteLine("1. Operar cuentas contables. \n9. Salir");
            bool ciclo = false;
            int menu = Helper.validarNroMenu();
            do
            {
                switch (menu)
                {
                    case 1:
                        bool otroAsiento = false;
                        do
                        {

                            bool SalirOperacion = false; //ERROR MAÑANA--> EL PROBLEMA ESTA EN LIBROCONTABLE.pedirCodigoContrapartida--> EN LOS 2 IF.
                            do
                            {
                                Console.Clear();
                                LibroContable.mostrarCuentas();
                                Console.WriteLine("\n");
                                var cuenta = LibroContable.seleccionar(); //va a pedir que el usuario ingrese el codigo
                                cuenta.gestionarOperacion();

                                Console.Clear();
                                Console.WriteLine("Ingrese Contrapartida");
                                var contrapartida = LibroContable.seleccionar(cuenta); //Paso cuenta para que muestre y solo se pueda elegir el tipo correcto de asiento.
                                contrapartida.gestionarOperacion();
                                SalirOperacion = LibroContable.validar(cuenta, contrapartida);
                                if (!SalirOperacion)
                                {
                                    while (LibroContable.TotalDebe != LibroContable.TotalHaber)
                                    {
                                        var cuenta2 = LibroContable.GestionarDiferenciaDebeHaber(cuenta, contrapartida);

                                        cuenta2.gestionarOperacion();
                                        if (LibroContable.TotalDebe != LibroContable.TotalHaber)
                                        {
                                            Console.WriteLine($"Para poder registrar la operacion Debe = Haber. \nDebe: {LibroContable.TotalDebe} Haber: {LibroContable.TotalHaber}. \n Ingrese otro asiento");
                                            Console.ReadKey();
                                        }

                                        else
                                        {
                                            Console.WriteLine("La operación ha sido registrada");
                                            SalirOperacion = true;
                                        }
                                    }


                                }
                            } while (!SalirOperacion);
                            Console.WriteLine("Ingrese 1 para seguir operando. 9 para salir");
                            int ingreso = Helper.validarNroMenu();
                            if (ingreso == 9)
                            {
                                LibroContable.grabar();
                                otroAsiento = true;
                                ciclo = true;
                            }

                            else continue;
                        } while (!otroAsiento);
                        break;



                    case 9:
                        ciclo = true;
                        break;


                }
            } while (!ciclo);
        }
    }
}
