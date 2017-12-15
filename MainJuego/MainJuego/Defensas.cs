using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    class Defensas
    {
        public char[] spriteDefensas_1;



       int cantidadFilas;
     public  int cantidadColumnas;
        public int cantidadTotal;
        public Posicion[] posicionDefensas;
        int contadorCuadradito = 0;
        public Defensas(Posicion posicion_defensas)
        {//■
            int contadorPartesX = 0;

            int contadorPartesY = 0;

            cantidadFilas = 2;
            cantidadColumnas = 5;

            contadorCuadradito = 0;
            cantidadTotal = cantidadFilas * cantidadColumnas;
            spriteDefensas_1 = new char[cantidadTotal];
            for (int i = 0; i < cantidadTotal; i++)
            {
                spriteDefensas_1[i] = '■';

            }


            posicionDefensas = new Posicion[cantidadTotal];
            for (int i = 0; i <cantidadFilas; i++)
            {
                contadorPartesX = 0;
                
                for (int i2 = 0; i2 < cantidadColumnas; i2++)
                {
              
                    posicionDefensas[contadorCuadradito].y = posicion_defensas.y+contadorPartesY;

                    posicionDefensas[contadorCuadradito].x = posicion_defensas.x + contadorPartesX;
                    contadorCuadradito += 1;
                    contadorPartesX +=1;
                }
                contadorPartesY += 1;
            }


        }


    }
}
