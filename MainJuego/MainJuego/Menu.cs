using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    class Menu
    {

        public Menu()
        {
            
          

        }
        public int SpawnMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("SPACE INVADERS");
            Console.WriteLine("");

            Console.WriteLine("1)PLAY");
            Console.WriteLine("");
            Console.WriteLine("2)HOW TO PLAY");
            int numeroElegido = 0;
            string entrada = "";
            entrada = Console.ReadLine();

            while (entrada == "")
            {
                Console.WriteLine("please insert a number");

                entrada =Console.ReadLine();
            }
             numeroElegido =Convert.ToInt32(entrada);

            while (numeroElegido != 1 && numeroElegido != 2)
            {
               
                Console.WriteLine("please insert correct number");


                entrada = Console.ReadLine();
                while (entrada == "")
                {//ingreso nuevamente vacio


                    Console.WriteLine("please insert a number");
                    entrada = Console.ReadLine();
                }

                numeroElegido = Convert.ToInt32(entrada);
            }
                return numeroElegido;


        }

        public int HowToPlay()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("MOVE= AROWS LEFT, RIGHT, press DOWN ");
            Console.WriteLine("");
            Console.WriteLine("SHOOT= SPACE BAR");

            Console.WriteLine("press  1)play" );
            int _numeroElegido;
            string _entrada="";
            while (_entrada == "")
            {
                Console.WriteLine("please insert a number");

                _entrada = Console.ReadLine();
            }
            _numeroElegido = Convert.ToInt32(_entrada);
            while (_numeroElegido != 1)
            {

                Console.WriteLine("please insert correct number");


                _entrada = Console.ReadLine();
                while (_entrada == "")
                {//ingreso nuevamente vacio


                    Console.WriteLine("please insert a number");
                    _entrada = Console.ReadLine();
                }

                _numeroElegido = Convert.ToInt32(_entrada);
            }
            return _numeroElegido;


        }
    }
}
