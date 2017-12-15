using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    class Cangrejo
    {
       public int contador = 0;
      public Posicion [] posicionEnemigo;
        public bool enemigoMuerto = false;
        public bool meta = false;
        public string sprite;
        int direccion = 1;
        int limiteAncho;
        int limiteAlto;
     //   public Disparo spriteDisparoEnemigo;
        public Cangrejo(Posicion _posicionEnemigo, string _sprite)
       {    int contadorPartes = 0;
            //" ╔O╦ "
            sprite = _sprite;

            posicionEnemigo = new Posicion[sprite.Length];
            for (int i = 0; i < posicionEnemigo.Length; i++)
            {
                posicionEnemigo[i].y = _posicionEnemigo.y;
                posicionEnemigo[i].x =_posicionEnemigo.x + contadorPartes;
                contadorPartes+=1;

            }

           

        }


        public void Mover(int y, int x,int _direccion)
        {
            direccion = _direccion;
            for (int i = 0; i < posicionEnemigo.Length; i++)
            {
                posicionEnemigo[i].x += x * direccion;
                posicionEnemigo[i].y += y;
            } 
        }
        public void SetLimite(int _limiteAncho,int _limiteAlto)
        {
            limiteAncho = _limiteAncho;
            limiteAlto = _limiteAlto;
        }
       
        public void Ataque()
        {
           
        }

        public void Muerte()
        {
           
        }
        public void DisparoEnemigo(Cangrejo _enemigo)
        {
            //spriteDisparoEnemigo = new Disparo('*',_enemigo.posicionEnemigo,1);

        }
     public void MoverDisparoEnemigo(Disparo disparo)
        {
            //disparo.MoverDisparo();
        }
    
    public void MuerteEnemigo(Cangrejo _cangrejo)
        {
           
        }
    }
}
