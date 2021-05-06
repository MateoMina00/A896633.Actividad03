using System;
using System.Collections.Generic;
using System.Text;

namespace A896633.Actividad03
{
    class CuentasContables
    {
        //Propiedades
        public int NroAsiento { get; set; } //Seria un contador???
        public DateTime Fecha { get; set; }
        public int CodigoCuenta { get; set; } //Codigo de Cuenta = Codigo del archivo leido.
        public int Debe { get; set; }
        public int Haber { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }


        //Constructores
        public CuentasContables(string EntradaArchivo)  //Constructor a partir de leer archivo
        {
            //Formato Recibido:
            //CODIGO(STRING)|NOMBRE(STRING)|TIPO(STRING)
            var arrayString = EntradaArchivo.Split('|');

            if (int.TryParse(arrayString[0], out int CodigoCuentaint))
            {
                CodigoCuenta = CodigoCuentaint;
                Tipo = arrayString[2];
                Nombre = arrayString[1];
            }


            //tiene que salir un archivo en formato
            //NroAsiento|Fecha|CodigoCuenta|Debe|Haber
        }
        public void gestionarOperacion()
        {
            if (this.Tipo == "Activo")
            {
                Console.Clear();
                Console.WriteLine("Favor de ingresar fecha del asiento");
                this.Fecha = Helper.DameFecha();
                this.NroAsiento = (NroAsiento + 1);
                Console.Clear();
                Console.WriteLine("Ingrese monto");
                this.Debe = Helper.ValidarNumero();
                LibroContable.TotalDebe = this.Debe + LibroContable.TotalDebe;


            }

            if (this.Tipo == "Pasivo")
            {
                Console.Clear();
                Console.WriteLine("Favor de ingresar fecha del asiento");
                this.Fecha = Helper.DameFecha();
                this.NroAsiento = (NroAsiento + 1);
                Console.Clear();
                Console.WriteLine("Ingrese monto");
                this.Haber = Helper.ValidarNumero();
                LibroContable.TotalHaber = this.Haber + LibroContable.TotalHaber;


            }

            if (this.Tipo == "PatrimonioNeto")
            {
                Console.Clear();
                Console.WriteLine("Favor de ingresar fecha del asiento");
                this.Fecha = Helper.DameFecha();
                this.NroAsiento = (NroAsiento + 1);
                Console.Clear();
                Console.WriteLine("Ingrese monto");
                this.Haber = Helper.ValidarNumero();
                LibroContable.TotalHaber = this.Haber + LibroContable.TotalHaber;


            }

        }
        public string Serializar()
        {
            string linea = $"{NroAsiento}|{Fecha}|{CodigoCuenta}|{Debe}|{Haber}";
            return linea;
        }
    }
}
