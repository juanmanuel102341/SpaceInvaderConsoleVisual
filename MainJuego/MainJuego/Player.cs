using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainJuego
{
    class Player
    {
        public Posicion[] posicionJugador;

        public string spriteHeroe;
        //public Disparo[] conjuntosDisparos = new Disparo[1000];
        public List<Disparo> listaDisparoHeroe = new List<Disparo>();


        public char spriteDisparoHeroe = ' ';
        int anchoMaximo;
       // int altoMaximo;
        //int velocidaDisparo;
        public int contadorDisparos = 0;
        public Disparo disparoHeroe;

        public int timeFireRate = 0;
        public int fireRate = 10;

        public bool disparoActivo = false;
        //public bool teclaActiva;
        public bool teclaXpos = false;
        public bool teclaXNegativo = false;
        public bool teclaYpos = false;
        public bool teclaYNegativo = false;
        public bool heroMuerto = false;

        public int acumulador_tecla = 0;
        public bool velocidadDseactivada = false;

//        int velocidadX = 0;
        public Player(Posicion _posicionJugador)
        {
            disparoActivo = false;
            teclaXpos = false;
            teclaXNegativo = false;
            teclaYpos = false;
            teclaYNegativo = false;
            heroMuerto = false;

            int contadorPartes = 0;
            spriteHeroe = "╠■╣";
            posicionJugador = new Posicion[spriteHeroe.Length];
            for (int i = 0; i < posicionJugador.Length; i++)
            {
                posicionJugador[i].y = _posicionJugador.y;

                posicionJugador[i].x = _posicionJugador.x + contadorPartes;

                contadorPartes++;

            }
            //  Console.WriteLine("pos heroe x " + posicionJugador[0].x);
            // Console.WriteLine("pos heroe x " + posicionJugador[1].x);
            //Console.WriteLine("pos heroe x " + posicionJugador[2].x);
        }

        public void Mover(int _movX, int _movY)
        {
            //velocidadX = _movX;

            bool activoLimite = limites(_movX);

            //  Console.WriteLine("velolcidad "+ _movX);
            if (activoLimite)
            {
                //limite alcanzado  
            }
            else
            {
                for (int i = 0; i < posicionJugador.Length; i++)
                {
                    posicionJugador[i].x += _movX;

                }



            }
        }
        public void Ataque()
        {

        }
        public void Muerte()
        {

        }
        public void SpawnDisparoHeroe(Posicion _posicionSalida)
        {

            disparoHeroe = new Disparo('I', _posicionSalida, 1);
            listaDisparoHeroe.Add(disparoHeroe);

            // Console.WriteLine("pos bala3 " + _posicionSalida.x);
            // Console.WriteLine("pos y " + posicionJugador[1].y);
            contadorDisparos++;


        }
        public void MoverDisparo(Disparo _disparo)
        {

            _disparo.MoverDisparoAbajoArriba();

        }
        public void SetLimitsJugador(int max_ancho, int max_alto)
        {
            anchoMaximo = max_ancho;
            //altoMaximo = max_alto;

        }

        bool limites(int movX)
        {
            //bool[] coleccionBooleanos = new bool[2];
            // 0 x
            bool limiteActivo = false;

            for (int i = 0; i < posicionJugador.Length; i++)
            {


                if (posicionJugador[i].x + movX >= anchoMaximo - 4)
                {
                    //coleccionBooleanos[0] = true;
                    limiteActivo = true;
                    return limiteActivo;

                }
                else if (posicionJugador[i].x + movX < 1)
                {
                    //coleccionBooleanos[0] = true;
                    limiteActivo = true;
                    return limiteActivo;

                }
                else
                {
                    //coleccionBooleanos[0] = false;
                    limiteActivo = false;
                    return limiteActivo;
                }


            }
            return limiteActivo;
        }

    }
}