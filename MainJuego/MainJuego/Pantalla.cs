using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    public struct Posicion
    {
        public int x;
        public int y;

    }

    class Pantalla
    {
        public int anchoPantalla ;
        public int altoPantalla ;

        int anchoVentana=Console.LargestWindowWidth;
        int altoVentana=Console.LargestWindowHeight;

        char[,] matrizJuego;
        public Cangrejo enemigo;
        //int filaCangrejo = 5;
        //int columnasCangrejos = 40;



        public Pantalla(int _anchoPantalla,int _altoPantalla )
        {
            anchoPantalla = _anchoPantalla;
            altoPantalla = _altoPantalla;

            matrizJuego = new char[altoPantalla, anchoPantalla];
            Console.SetWindowSize(anchoVentana, altoVentana);
          
        }

        void DibujandoLimitesPantalla()
        {
            for(int i = 0; i < altoPantalla; i++)
            {
               
                  
                   matrizJuego[i, 0] = '|';
                   matrizJuego[i,altoPantalla-1] = '|';

            }
           for (int i2 = 0; i2 < anchoPantalla; i2++)
           {
                matrizJuego[0, i2] = '_';
              matrizJuego[altoPantalla - 1, i2] = '_';
            }
          
        }
        public void Clear_Matriz()
        {
          
            for (int i = 0; i < altoPantalla; i++)
            {

                for (int i2 = 0; i2 < anchoPantalla; i2++)
                {

                    matrizJuego[i, i2] = ' ';

                }
            }
            
        }
        public void AgregarElemento(Posicion posicionObjeto,char objeto)
        {
            if (posicionObjeto.x < anchoPantalla && posicionObjeto.y < altoPantalla)
            {
                matrizJuego[posicionObjeto.y, posicionObjeto.x] = objeto;
            }
           
        }
        public void EliminarElemento(Posicion posicionElemento)
        {
            
            matrizJuego[posicionElemento.x, posicionElemento.y] =' ' ;
        }
      
        public void EscrituraMatriz()
        {
            Console.SetCursorPosition(0, 0);
            DibujandoLimitesPantalla();
            
            for (int i = 0; i < altoPantalla; i++)
            {
                // 
                //Console.WriteLine("");
                //Console.WriteLine("");
             
                for (int i2 = 0; i2 < anchoPantalla; i2++)
                {
                    //Console.Write(' ');
                    Console.Write(matrizJuego[i, i2]);
                    //co[i, i2].Dibujar();
                }
                Console.WriteLine("");
            }
         
            Console.CursorVisible = false;
        }

       
      
    }
}
