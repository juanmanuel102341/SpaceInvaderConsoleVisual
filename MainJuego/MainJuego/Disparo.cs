using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    class Disparo
    {
       public char sprite=' ';
        public Posicion posicionDisparo;
        int velocidadDisparo;
        public Disparo(char _sprite, Posicion _posicionDisparo, int _velocidadDisparo)
        {
            sprite = _sprite;
            posicionDisparo = _posicionDisparo;

           // Console.WriteLine("bala 4 " + posicionDisparo.x);
            velocidadDisparo = _velocidadDisparo;
        }

        public void MoverDisparoAbajoArriba()
        {//heroe
            if (posicionDisparo.y > 1)
            {
                posicionDisparo.y -= velocidadDisparo;
                limitesDisparoAbajoArriba();

                // Console.WriteLine("pos disparo y " + posicionDisparo.y);
            }
            else
            {
                sprite = ' ';
            }
        }
        public void MoverDisparoArribaAbajo()
        {//enemigos
                posicionDisparo.y += velocidadDisparo;
            //   LimitesDisparoArribaAbajo();
            // Console.WriteLine("pos disparo x " + posicionDisparo.x);


        
                     
      }
        
       

        void limitesDisparoAbajoArriba()
        {
            if (posicionDisparo.y <1 )
            {
        //        EliminarDisparo();
            }
        }
        void LimitesDisparoArribaAbajo()
        {
          
        }
    }
}
