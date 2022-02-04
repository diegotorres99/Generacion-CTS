using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculosCuentas
{
    public class Calculos
    {
        static void Main(string[] args)
        {
            string ibanAccount = "CR10010200009007408120";
            //string userAccount = "1351011123456";
            string cuentaInternaUsuario = "0000000022211";
            string cuentaCliente1 = "10000011502000281";
            string cuentaCliente2 = "10200009007408120";
            string CuentaCliente = generaCuentaCliente(cuentaInternaUsuario);
            //int mod = calcularModulo97("1227015202001093951055");
            ////int mod2 = Mod(11, 48);
            //string CuentaIbanGenerda = generaCuentaIban(cuentaCliente2);
            Console.WriteLine(CuentaCliente);
            //Console.WriteLine(mod2);
            Console.ReadKey();
        }

        /// <summary>
        /// Calcula el Mod 97 ya que en este caso la longitud del número supera lo permitido por C#.
        /// </summary>
        /// <param name="dividendo"> Número al cual se le aplicará módulo o resto. </param>
        /// <returns>Residuo del número ingresado</returns>
        public static int calculaMod97(string dividendo)
        {
            int parteDividendo, divisor = 97, longitudDivisionNumero = 9;
            string nuevoDividendo;

            //1) Mientras la longitud del dividendo sea mayor a 10 dígitos aplicamos el mod de cada parte del dividendo.
            while (dividendo.Length > longitudDivisionNumero)
            {
                parteDividendo = Convert.ToInt32(dividendo.Substring(0, longitudDivisionNumero));
                nuevoDividendo = (parteDividendo % divisor).ToString() + dividendo.Substring(longitudDivisionNumero);
                dividendo = nuevoDividendo;
            }
            return Convert.ToInt32(dividendo) % divisor;
        }

        /// <summary>
        /// Genera Cuenta IBAN apartir de Cuenta Cliente de longitud 17 dígitos.
        /// </summary>
        /// <param name="cuentaCliente"> Cuenta a realizar conversión a cuenta IBAN. </param>
        /// <returns> Cuenta IBAN generada apartir de Cuenta Cliente. </returns>
        public static string generaCuentaIban(string cuentaCliente)
        {
            int digitosVerificadores, ampliaCodigoEntidad = 0;
            string cuentaIban;

            //1) Concatenar a la derecha de la CC -> "CR00".
            string concatena = cuentaCliente + "CR00";

            //2) A la izquierda de CC se coloca un cero(0).
            string izquierda = ampliaCodigoEntidad + concatena;

            //3 Convertir letras en números C=12 R=27.
            string replacement = izquierda.Replace("CR", "1227");

            //4) Calcular el modulo 97 (ISO 7064).
            int modulo = calculaMod97(replacement);

            //5) Resta de 98.
            digitosVerificadores = 98 - modulo;

            //6) Si el resultado es un digito anteponer un cero(0).
            if (digitosVerificadores.ToString().Length == 1)
            {
                string nuevosDigitosVerificadores = "0" + digitosVerificadores;

                cuentaIban = "CR" + nuevosDigitosVerificadores + ampliaCodigoEntidad + cuentaCliente;
                if (verificaCuentaIban(cuentaIban) == 1)
                {
                    return cuentaIban;
                }
                return null;
            }
            else
            {
                cuentaIban = "CR" + digitosVerificadores + ampliaCodigoEntidad + cuentaCliente;
                if (verificaCuentaIban(cuentaIban) == 1)
                {
                    return cuentaIban;
                }
                return null;
            }
        }

        /// <summary>
        /// Verifica si la cuenta IBAN generada es correcta aplicando algoritmo de verificación.
        /// </summary>
        /// <param name="cuentaIban"> Cuenta IBAN a ser verificada. </param>
        /// <returns> 1 si Cuenta IBAN esta correctamente formada. </returns>
        public static int verificaCuentaIban(string cuentaIban)
        {
            string nuevoIban, remplazoIban;
            char quintoDigito;
            int verifica;

            //1) Mover los primeros 4 caracteres a la derecha del numero de cuenta.
            string moverDerecha = cuentaIban.Substring(0, 4);

            //2)Convertir las letras en numeros C=12 R=27.
            nuevoIban = cuentaIban.Substring(5);
            quintoDigito = cuentaIban.ElementAt(4);
            nuevoIban = quintoDigito + nuevoIban + moverDerecha;
            remplazoIban = nuevoIban.Replace("CR", "1227");

            //3) Aplicar modulo 97 y el residuo debe ser = 1.
            verifica = calculaMod97(remplazoIban);

            return verifica;
        }

        /// <summary>
        /// Genera Cuenta Cliente apartir de número de número de cuenta interno de entidad, longitud 13 digitos.
        /// </summary>
        /// <param name="cuentaUsuario"> Cuenta de usuario interna a ser procesada. </param>
        /// <returns> Cuenta cliente generada. </returns>
        public static string generaCuentaCliente(string cuentaUsuario)
        {
            if (cuentaUsuario.Length < 12)
            {
                return "Cuenta debe contener 13 digitos";
            }

            int multiplicador = 1, posicion, tempResult, resultadoFinal=0;
            string digitoVericador, multiplicando, numEntidad="502";

            // 1) Agregamos número de entidad a la izquierda de la cuenta.
            cuentaUsuario = numEntidad + cuentaUsuario;

            for (posicion = 0; posicion <= 15; posicion++)
            {   
                //2) Validamos rango del multiplicador de 1 a 9.
                if (multiplicador > 9) multiplicador = 1; 

                //3) Recorremos cada posición a multiplicar de la forma: multiplicando * multiplicador.
                multiplicando = cuentaUsuario.ElementAt(posicion).ToString();

                //4) Guardamos el resultado de cada una de las multiplicaciones para luego sumar las mismas.
                tempResult = Convert.ToInt32(multiplicando) * multiplicador++; // Incrementamos multiplicador.
                resultadoFinal += tempResult;
            }
            //6) Realizar mod 11.
            int res = Convert.ToInt32(resultadoFinal) % 11;

            //7) Si el número es de dos cifras se toma primer digito.
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

        public static int StringBuilderVerificaCuentaIban(string cuentaIban)
        {
            StringBuilder nuevoIban = new StringBuilder();
            StringBuilder remplazoIban = new StringBuilder();
            char quintoDigito;
            int verifica;

            //1) Mover los primeros 4 caracteres a la derecha del numero de cuenta.
            string moverDerecha = cuentaIban.Substring(0,4);

            //2) Convertir las letras en numeros C=12 R=27.
            quintoDigito = cuentaIban.ElementAt(4);
            nuevoIban = nuevoIban.Append(quintoDigito.ToString() + cuentaIban.Substring(5) + moverDerecha);
            remplazoIban = nuevoIban.Replace("CR", "1227");

            //3) Aplicar modulo 97 y el residuo debe ser = 1.
            verifica = calculaMod97(remplazoIban.ToString());

            return verifica;
        }

    }
    
}
