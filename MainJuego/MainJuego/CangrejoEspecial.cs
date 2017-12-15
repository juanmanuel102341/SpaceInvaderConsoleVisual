using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    class CangrejoEspecial
    {
        public string spriteCangrejoEspecial;
        public Posicion[] posicionCangrejoEspecial;
        int direccionCangrejoEspecial=0;
       public int scoreCangrejoEspecial=40;
        public int scoreAcumuladoCangrejo = 0;
        public CangrejoEspecial()
        {
            spriteCangrejoEspecial = "";

        }


        public void SpawnCangrejoEspecial(string _sprite, Posicion _posicionCangrejoEspecial)
        {
            int contadorPartes = 0;
            //" ╔O╦ "
            spriteCangrejoEspecial = _sprite;

            posicionCangrejoEspecial = new Posicion[spriteCangrejoEspecial.Length];
            for (int i = 0; i < posicionCangrejoEspecial.Length; i++)
            {
                posicionCangrejoEspecial[i].y = _posicionCangrejoEspecial.y;
                posicionCangrejoEspecial[i].x = _posicionCangrejoEspecial.x + contadorPartes;
                contadorPartes += 1;

            }


        }

        public void MoverCangrejoEspecial(int x, int _direccion)
        {
            direccionCangrejoEspecial = _direccion;

            for (int i = 0; i < posicionCangrejoEspecial.Length; i++)

            {
               
                    posicionCangrejoEspecial[i].x += x * direccionCangrejoEspecial;
               
             
              }

        }
      
       

    }
}
