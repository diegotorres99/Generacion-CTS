using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculosCuentas
{
    class Program
    {
        static void Main(string[] args)
        {
            string userAccount = "5011351011123456";
            string userAccount2 = "5020000000022211";
            string resp =   generarCuentaCliente(userAccount2);
            Console.WriteLine(resp);
            Console.ReadKey();
        }
        public static string generarCuentaCliente(string cuentaUsuario) //16 a recibir
        {
            if (cuentaUsuario.Length < 16)
            {
                return "Cuenta debe contener 16 digitos";
            }
            int multiplicador = 1, posicion = 0, tempResult = 0, result=0;
            string digitoVericador = String.Empty, multiplicando = String.Empty;

            for (posicion = 0; posicion < cuentaUsuario.Length; posicion++)
            {   //Validamos rango del multiplicador de 1 a 9
                if (multiplicador == 9) {
                    multiplicador = 1; 
                }
                //Recorremos cada posicion a multiplicar
                multiplicando = cuentaUsuario.ElementAt(posicion).ToString();
                //Guardamos el resultado de cada una de las multiplicaciones para luego sumar las mismas
                tempResult = Convert.ToInt32(multiplicando) * multiplicador++; //Incrementamos multiplicador 
                multiplicando = string.Empty;
                result = result + tempResult;
            }
            //realizar mod 11
            var res = Convert.ToInt32(result);
            res %= 11;
            if (res > 9)
            {
                digitoVericador = res.ToString().ElementAt(0).ToString();
            }
            else
            {
                digitoVericador = res.ToString();
            }
            return cuentaUsuario + digitoVericador;
        }
    }
    
}
