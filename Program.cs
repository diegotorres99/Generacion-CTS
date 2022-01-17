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
            string userAccount = "1351011123456";
            string userAccount2 = "5020000000022211";
            string CuentaCliente = generarCuentaCliente(userAccount);
         //   string CuentaIBAN = generarCuentaIBAN(CuentaCliente);
            Console.WriteLine(CuentaCliente.Count());
            Console.ReadKey();
        }

        private static string generarCuentaIBAN(string cuentaCliente)
        {
            //1-Concateana CC -> "CR00";
            //2-A la izquierda de CC se coloca 0
            //3- Convertir letras en números C=12 R=27
            //Resta de 98
            //Si es un digito anteponer un 0
            var auxCuentaCliente = "1227000" + cuentaCliente;
            //Convertir letras en números C=12 R=27
            var res = Convert.ToInt64(auxCuentaCliente);
            res = res %= 97;
            res = res -= 98;
            if (res<9)
            {

            }

            return null;
        }


  

        public static string generarCuentaCliente(string cuentaUsuario) //13 a recibir
        {
            if (cuentaUsuario.Length < 12)
            {
                return "Cuenta debe contener 13 digitos";
            }
            int multiplicador = 1, posicion = 0, tempResult = 0, result=0;
            string digitoVericador = String.Empty, multiplicando = String.Empty;
            cuentaUsuario = "501"+cuentaUsuario;
            for (posicion = 0; posicion <= 15; posicion++)
            {   //Validamos rango del multiplicador de 1 a 9
                if (multiplicador == 10) {
                    multiplicador = 1; 
                }
                //Recorremos cada posicion a multiplicar
                multiplicando = cuentaUsuario.ElementAt(posicion).ToString();
                //Guardamos el resultado de cada una de las multiplicaciones para luego sumar las mismas
                tempResult = Convert.ToInt32(multiplicando) * multiplicador++; //Incrementamos multiplicador 
                multiplicando = string.Empty;
                result = result + tempResult;
                tempResult = 0;
            }
            //realizar mod 11
            var res = Convert.ToInt32(result);
            res %= 97;
            if (res > 9)
            {
                digitoVericador = res.ToString().ElementAt(0).ToString();
            }
            digitoVericador = res.ToString();

            return cuentaUsuario + digitoVericador;
        }
    }
    
}
