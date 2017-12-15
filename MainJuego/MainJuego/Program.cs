using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Collections;
using System.IO;
namespace MainJuego
{
    class Program
    {
     

 
        static void Main(string[] args)
        {
            //Archivos archivoScoreExterno = new Archivos();
            

            Menu MenuJuego = new Menu();
            int numeroEleccion=MenuJuego.SpawnMenu();
            if (numeroEleccion == 1)
            {
             InicializacionJuego();
            }else
            {
                int numHTP=MenuJuego.HowToPlay();

               
               if(numHTP==1)
                {
                    InicializacionJuego();
                }
            }
        }
    

        /// <summary>
        /// 
        /// </summary>
        static void InicializacionJuego()
        {
            bool JuegoActivo = true;
            Pantalla pantallaJuego;
            Defensas[] defensasLevel;


            Posicion posicionaSalidaEnemigo;
            Posicion posicionSalidaHeroe;
            Posicion posicionSalidaCangrejoEspecial;
            Posicion posicionGuiVidas;
            Posicion posicionScore;
            //****enemigos******
            int cantidadEnemigosFila = 6;//max 6 
            int cantidadEnemigosColumna = 5;
            int direccionXEnemigos = 1;
            int direccionYEnemigos = 0;
            int velocidadEnemigos = 1;
            int FireRateEnemigos = 25;
            int timeFireRateEnemigos = 0;
            int timeSpawnCangrejoEspecial = 0;
            int SpawnCangrejoEspecial = 250;
            int scoreCangrejo = 10;
            Disparo copiaDisparoEnemigo;
            List<Disparo> ListadisparosEnemigos = new List<Disparo>();
            CangrejoEspecial copiaCangrejoEspecial = new CangrejoEspecial();
            List<Cangrejo> listaEnemigos = new List<Cangrejo>();


            //heroe
            int velocidadHeroe = 2;
            int vidaJugador = 3;

            bool definicion = false;
            int score = 0;
            int scoreFinal = 0;
                 
            Random r = new Random();
            

            pantallaJuego = new Pantalla(50, 50);

           posicionaSalidaEnemigo.x = pantallaJuego.anchoPantalla / 2 - cantidadEnemigosColumna / 2;
            posicionaSalidaEnemigo.y = 1;

          
          listaEnemigos = CreacionEnemigos(cantidadEnemigosFila, cantidadEnemigosColumna, posicionaSalidaEnemigo, pantallaJuego.anchoPantalla, pantallaJuego.altoPantalla);
          

           // AgregarEnemigo(listaEnemigos,pantallaJuego);
            //*****jugador******
           posicionSalidaHeroe.x = pantallaJuego.anchoPantalla/2;
           posicionSalidaHeroe.y = pantallaJuego.altoPantalla-5;
          Player jugador = new Player(posicionSalidaHeroe);
          jugador.SetLimitsJugador(pantallaJuego.anchoPantalla, pantallaJuego.altoPantalla);

            //*****agregando jugador al screen********
          for (int ip = 0; ip < jugador.posicionJugador.Length; ip++)
          {
               pantallaJuego.AgregarElemento(jugador.posicionJugador[ip], jugador.spriteHeroe[ip]);
           }
            //*****agregando defensas***********
            defensasLevel = CreacionDefensas(pantallaJuego.anchoPantalla, pantallaJuego.altoPantalla);

            AgregarDefensas(defensasLevel, pantallaJuego);

            int tiempoRespawn_heroe = 20;//respawn del heroe

            int contadorEnemigos = cantidadEnemigosFila * cantidadEnemigosColumna;
            
            int[] direcciones_enemigos = new int[2];
            direcciones_enemigos[0] = direccionXEnemigos;
            direcciones_enemigos[1] = direccionYEnemigos;


            int c = 0;


            posicionGuiVidas.x = pantallaJuego.anchoPantalla - 2;
            posicionGuiVidas.y = 2;

            posicionScore.x = 2;
            posicionScore.y = 2;
            // int contadorCantidadNumeros = 0;
            pantallaJuego.EscrituraMatriz();

            //////////////////////////***********************************bucle juego***********************************************************************//////////////////////////

            while (JuegoActivo)
            {


                /////***************************************respawn disparo heroe******************************************

                pantallaJuego.Clear_Matriz();

            if (jugador.disparoActivo)
                {
                    jugador.timeFireRate += 1;


                    if (jugador.timeFireRate > jugador.fireRate)
                    {
                        jugador.disparoActivo = false;
                        jugador.timeFireRate = 0;
                    }

                }
         
                //*******************************************************respawn disparo enemigos**********************************************
                timeFireRateEnemigos += 1;

                if (timeFireRateEnemigos > FireRateEnemigos)
                {
                    Cangrejo enemigoDispara = EleccionDisparoEnemigo(listaEnemigos,listaEnemigos.Count);

                   copiaDisparoEnemigo = new Disparo('I', enemigoDispara.posicionEnemigo[1], 1);

                    pantallaJuego.AgregarElemento(copiaDisparoEnemigo.posicionDisparo, copiaDisparoEnemigo.sprite);

                   ListadisparosEnemigos.Add(copiaDisparoEnemigo);

                    timeFireRateEnemigos = 0;
                }
                //**************************************************respawn enemigoEspecial**************************************************
                timeSpawnCangrejoEspecial += 1;
                //Console.WriteLine("tiempoCangrejoEspecial " + timeSpawnCangrejoEspecial);
                if (copiaCangrejoEspecial.spriteCangrejoEspecial != "")
                {
                    copiaCangrejoEspecial.MoverCangrejoEspecial(1, 1);
                    for (int ie2 = 0; ie2 < copiaCangrejoEspecial.posicionCangrejoEspecial.Length; ie2++)
                    {
                        pantallaJuego.AgregarElemento(copiaCangrejoEspecial.posicionCangrejoEspecial[ie2], copiaCangrejoEspecial.spriteCangrejoEspecial[ie2]);

                    }

                }
                if (timeSpawnCangrejoEspecial > SpawnCangrejoEspecial)
                {
                    posicionSalidaCangrejoEspecial.x = 0;
                    posicionSalidaCangrejoEspecial.y = r.Next(3, 6);
                   copiaCangrejoEspecial.SpawnCangrejoEspecial("> O <", posicionSalidaCangrejoEspecial);
                    
                    for (int ie2 = 0; ie2 < copiaCangrejoEspecial.posicionCangrejoEspecial.Length; ie2++)
                        {
                      pantallaJuego.AgregarElemento(copiaCangrejoEspecial.posicionCangrejoEspecial[ie2], copiaCangrejoEspecial.spriteCangrejoEspecial[ie2]);

                        }
                    
                    timeSpawnCangrejoEspecial = 0;
                }
                
            for(int ic = 0; ic < copiaCangrejoEspecial.spriteCangrejoEspecial.Length;ic++)
                {
                    if (copiaCangrejoEspecial.posicionCangrejoEspecial[ic].x > pantallaJuego.anchoPantalla - 1)
                    {
                        copiaCangrejoEspecial.spriteCangrejoEspecial = "";

                    }
              
                        
                }


                //***************************************gui*************************************
                string vidasJugador_string = "vidas: " + vidaJugador.ToString();
                posicionGuiVidas.x = pantallaJuego.anchoPantalla - vidasJugador_string.Length-3;
                
                string score_string = "score: " + scoreFinal.ToString();
                posicionScore.x = score_string.Length-6;
               ImprimirStringScreen(pantallaJuego, vidasJugador_string, posicionGuiVidas);
                ImprimirStringScreen(pantallaJuego, score_string, posicionScore);
                //************************************************************************************ENEMIGOS*********************************************************************
                if (contadorEnemigos < 4)
                {
                      velocidadEnemigos = 2;
                }
          direcciones_enemigos = LimitesEnemigos(listaEnemigos,pantallaJuego.anchoPantalla,pantallaJuego.altoPantalla, direcciones_enemigos[0], direcciones_enemigos[1], velocidadEnemigos, posicionSalidaHeroe.y);
                bool perderMeta = ControlMetaEnemigos(listaEnemigos);

                if (!perderMeta)//si algun enemigo llego a la meta
                {
                    
                    MoviendoEnemigos(listaEnemigos, velocidadEnemigos, direcciones_enemigos[0], direcciones_enemigos[1]);

                    AgregarEnemigo(listaEnemigos, pantallaJuego);

                    //************************************************disparo enemigos******************************************* 
                    for (int i = 0; i < ListadisparosEnemigos.Count; i++)
                    {
                        bool contacto_bala_enemigo_heroe_activo = ContactoBalasEnemigosHeroe(jugador, ListadisparosEnemigos[i]);//contacto con heroe

                        bool contactoLadrillo_activo = ContactoBalasDefensas(ListadisparosEnemigos[i], defensasLevel, 3);//contacto defensas

                        bool balaEnemigoFueraEscenario = LimiteBalasEnemigosFueraEscenario(ListadisparosEnemigos[i], pantallaJuego.altoPantalla - 1);//fuera del escenario

                        if (contactoLadrillo_activo)
                        {
                            ListadisparosEnemigos.Remove(ListadisparosEnemigos[i]);
                            contactoLadrillo_activo = false;
                        }
                        else if (contacto_bala_enemigo_heroe_activo)
                        {
                            vidaJugador--;
                            ListadisparosEnemigos.Remove(ListadisparosEnemigos[i]);
                            jugador.spriteHeroe = "";//muerte del heroe
                            jugador.heroMuerto = true;
                            contacto_bala_enemigo_heroe_activo = false;


                        }
                        else if (balaEnemigoFueraEscenario)
                        {
                            ListadisparosEnemigos.Remove(ListadisparosEnemigos[i]);

                            balaEnemigoFueraEscenario = false;
                        }
                        else {//moves balas despues de controlar colisiones
                            ListadisparosEnemigos[i].MoverDisparoArribaAbajo();

                            pantallaJuego.AgregarElemento(ListadisparosEnemigos[i].posicionDisparo, ListadisparosEnemigos[i].sprite);

                        }
                    
                    }
                    
                }
                else
                {
                    definicion = false;
                    JuegoActivo = false;

                }

                //  Console.WriteLine("vidas" + jugador.vidas);

                // ****************************-----------------------************************



                //*******************************agregando defensas*******************************
                for (int i = 0; i < 3; i++)

                {
                    for (int ifila = 0; ifila < defensasLevel[i].cantidadTotal; ifila++)
                    {

                        pantallaJuego.AgregarElemento(defensasLevel[i].posicionDefensas[ifila], defensasLevel[i].spriteDefensas_1[ifila]);

                    }

                }

                
               
                //****************************jugador**********************************

                if (jugador.heroMuerto)
                {
                   

                    //   vidaJugador--;
                    if (vidaJugador <= 0)
                    {
                        definicion = false;
                        //     Console.WriteLine("perdisteeeee");
                        JuegoActivo = false;
                    }
                    else {

                        //heroe muerto//respawn tiempo
                        c += 1;
                        //               Console.WriteLine("TIEMPO " + c);
                        //             Console.WriteLine("muerto " + heroeMuerto);
                        if (c > tiempoRespawn_heroe)
                        {
                           
                            jugador = new Player(posicionSalidaHeroe);
                            jugador.SetLimitsJugador(50, 50);
                            c = 0;
                            jugador.heroMuerto = false;
                        }
                    }
                 }
                else {
                    jugador = InputHeroe(jugador, velocidadHeroe); //movimiento heroe

                    for (int ip = 0; ip < jugador.posicionJugador.Length; ip++)
                    {

                        pantallaJuego.AgregarElemento(jugador.posicionJugador[ip], jugador.spriteHeroe[ip]);//agregamos pantalla

                    }

                    ////**************************disparos jugador****************************
                    for (int i = 0; i < jugador.listaDisparoHeroe.Count; i++)
                    {
                        bool contactoBalaHeroeEnemigo_activo = ContactoBalasHeroeEnemigos(jugador.listaDisparoHeroe[i],listaEnemigos);//contacto con enemigo

                        bool ladrilloFuera_heroeDisparo = ContactoBalasDefensas(jugador.listaDisparoHeroe[i], defensasLevel, 3);//contacto con defensa

                        bool balaFueraEscenario_activo = LimiteBalasHeroeEscenario(jugador);//fuera del escenario
                        bool contactoBalaHeroeCangrejoEspecial_activo = false;

                        if (copiaCangrejoEspecial.spriteCangrejoEspecial != "")
                        {
                            contactoBalaHeroeCangrejoEspecial_activo=ContactoBalasHeroeCangrejoEspecial(copiaCangrejoEspecial, jugador.listaDisparoHeroe[i]);

                        }

                        if (ladrilloFuera_heroeDisparo)
                        {//contacto defensa
                            jugador.listaDisparoHeroe.Remove(jugador.listaDisparoHeroe[i]);
                            ladrilloFuera_heroeDisparo = false;
                            //   jugador.listaDisparoHeroe.Remove(jugador.listaDisparoHeroe[i]);
                        }
                        else if (contactoBalaHeroeEnemigo_activo)
                        {//contacto enemigo
                            jugador.listaDisparoHeroe.Remove(jugador.listaDisparoHeroe[i]);
                            contadorEnemigos--;
                            score += scoreCangrejo;
                            if (contadorEnemigos <= 0)
                            {
                                definicion = true;

                                JuegoActivo = false;
                            }

                            contactoBalaHeroeEnemigo_activo = false;

                        }else if (contactoBalaHeroeCangrejoEspecial_activo)
                        {
                            jugador.listaDisparoHeroe.Remove(jugador.listaDisparoHeroe[i]);
                            copiaCangrejoEspecial.scoreAcumuladoCangrejo += copiaCangrejoEspecial.scoreCangrejoEspecial;
                            copiaCangrejoEspecial.spriteCangrejoEspecial = "";
                            contactoBalaHeroeCangrejoEspecial_activo = false;
                        }
                        else if (balaFueraEscenario_activo)
                        {//fuera del escenario
                            jugador.listaDisparoHeroe.Remove(jugador.listaDisparoHeroe[i]);
                            balaFueraEscenario_activo = false;
                        }
                        else {
                            //moves disparo 
                            jugador.listaDisparoHeroe[i].MoverDisparoAbajoArriba();
                            pantallaJuego.AgregarElemento(jugador.listaDisparoHeroe[i].posicionDisparo, jugador.listaDisparoHeroe[i].sprite);
                        }
                    }
                }

    scoreFinal = score + copiaCangrejoEspecial.scoreAcumuladoCangrejo;
                pantallaJuego.EscrituraMatriz();
                
            }

           
          



            //pantallaJuego.Clear_Matriz();
            //pantallaJuego.EscrituraMatriz();
            //Posicion posicionDefinicion;
            //posicionDefinicion.y = pantallaJuego.altoPantalla / 2;
            // int numEleccion;
            //   Posicion posicionDefinicionPregunta;

            //    string nombreUsuario;
            if (definicion)
            {
                string textodef = "<WIN>";




                Console.WriteLine(textodef);

               

                DefinicionFinal();



            }
            else
            {

                string textodef = "<GAME OVER>";
                Console.WriteLine(textodef);
                DefinicionFinal();


              }
            Console.Clear();

            Console.ReadLine();



        }




        static List<Cangrejo> CreacionEnemigos(int _cantidadEnemigosFila,int _cantidadEnemigosColumna,Posicion _posicionSalidaEnemigos,int _anchoPantalla,int _altoPantalla)
        {
            //Cangrejo[,] coleccionEnemigos = new Cangrejo[_cantidadEnemigosFila, _cantidadEnemigosColumna];
            List<Cangrejo> _listaEnemigos = new List<Cangrejo>(); 
            int _cantidadPartesEnemigos;
            int posInicialY = _posicionSalidaEnemigos.y;
            for (int i = 0; i < _cantidadEnemigosFila; i++)
            {
                _posicionSalidaEnemigos.x = 0;
                _cantidadPartesEnemigos = 0;
                for (int ic = 0; ic < _cantidadEnemigosColumna; ic++)
                {
                    _posicionSalidaEnemigos.x += _cantidadPartesEnemigos;
                    _posicionSalidaEnemigos.y = i+posInicialY;

                    switch (i)
                    {

                        case 0:
                        case 1:
                            Cangrejo copiaCangrejo;
                            copiaCangrejo = new Cangrejo(_posicionSalidaEnemigos, " ╔O╦ ");
                            _cantidadPartesEnemigos = copiaCangrejo.sprite.Length;
                            copiaCangrejo.SetLimite(_anchoPantalla - 1, _altoPantalla - 1);
                            _listaEnemigos.Add(copiaCangrejo);
                            //coleccionEnemigos[i, ic] = copiaCangrejo;

                            break;
                        case 2:
                        case 3:

                            copiaCangrejo = new Cangrejo(_posicionSalidaEnemigos, " │■│ ");
                            _cantidadPartesEnemigos = copiaCangrejo.sprite.Length;
                            copiaCangrejo.SetLimite(_anchoPantalla - 1, _altoPantalla - 1);
                            _listaEnemigos.Add(copiaCangrejo);
                            //     coleccionEnemigos[i, ic] = copiaCangrejo;

                            break;

                        case 4:
                        case 5:
                            copiaCangrejo = new Cangrejo(_posicionSalidaEnemigos, " >O< ");
                            copiaCangrejo.SetLimite(_anchoPantalla - 1, _altoPantalla - 1);
                            _cantidadPartesEnemigos = copiaCangrejo.sprite.Length;
                            //coleccionEnemigos[i, ic] = copiaCangrejo;
                            _listaEnemigos.Add(copiaCangrejo);

                            break;

                    }

                
                }

            }

            return _listaEnemigos;
        }

        static Defensas[] CreacionDefensas(int _anchoPantalla,int _altoPantalla)
        {

            Defensas[] ColeccionDefensas = new Defensas[3];
            Posicion posicionDefensas;
            for (int i = 0; i < 3; i++)
            {

                switch (i)
                {

                    case 0:
                        posicionDefensas.x = 5;
                        posicionDefensas.y = 35;
                        ColeccionDefensas[i] = new Defensas(posicionDefensas);
                        break;


                    case 1:

                        posicionDefensas.x = _anchoPantalla / 2 - ColeccionDefensas[0].cantidadColumnas + 4;
                        posicionDefensas.y = 35;
                        ColeccionDefensas[i] = new Defensas(posicionDefensas);
                        break;

                    case 2:
                        posicionDefensas.x = _anchoPantalla - ColeccionDefensas[0].cantidadColumnas - 3;
                        posicionDefensas.y = 35;
                        ColeccionDefensas[i] = new Defensas(posicionDefensas);

                        break;
                }

            }
            return ColeccionDefensas;
        } 


        static void MoviendoEnemigos(List <Cangrejo> grupoEnemigos, int velocidad, int _direccionX,int _direccionY)
        {
          
            
            
                for (int ic = 0; ic < grupoEnemigos.Count; ic++)
                {

                    grupoEnemigos[ic].Mover(_direccionY, velocidad, _direccionX);
                     
                }

      
        }


        static int[] LimitesEnemigos(List<Cangrejo> grupoEnemigos, int anchoPantalla, int altoPantalla,int direccionActualX,int direccionActualY,int _velocidad,int metaJugadorPosicion)
        {
            int direccionX=direccionActualX;
            int direccionY = direccionActualY;
            int[] direcciones = new int[2];
            direcciones[0] = direccionX;
          

            for (int i = 0; i < grupoEnemigos.Count; i++)
            {
              
                    
                    for (int ip = 0; ip < grupoEnemigos[i].posicionEnemigo.Length; ip++)
                    {
                        if (grupoEnemigos[i].posicionEnemigo[ip].x +_velocidad> anchoPantalla-1)
                        {//considero velocidad por si cambia de valor la misma no se vaya fuera del rango la matriz
                            direccionX = -1;
                            direccionY = 1;//bajo un nivel grupo enemigos
                            direcciones[0] = direccionX;
                            direcciones[1] = direccionY;
                            return direcciones;//izquierda
                        }
                        else if (grupoEnemigos[i].posicionEnemigo[ip].x-_velocidad <= 2)
                        {
                            direccionX = 1;
                            direccionY = 0;//mantengo mismo nivel 
                            direcciones[0] = direccionX;
                            direcciones[1] = direccionY;
                            return direcciones;//derecha
                        }
                    if(grupoEnemigos[i].posicionEnemigo[ip].y >= metaJugadorPosicion && !grupoEnemigos[i].enemigoMuerto)
                        {
                            grupoEnemigos[i].meta = true;
                            //perdiste
                            //Console.WriteLine(grupoEnemigos[i, ic].meta);
                        }
                    }
              

            }
            direcciones[1] = 0;
            return direcciones;
        }

        static void AgregarDefensas(Defensas [] _grupoDefensas,Pantalla _pantalla)
        {
            for (int i = 0; i < 3; i++)

            {
                for (int ifila = 0; ifila < _grupoDefensas[0].cantidadTotal; ifila++)
                {


                    _pantalla.AgregarElemento(_grupoDefensas[i].posicionDefensas[ifila], _grupoDefensas[i].spriteDefensas_1[ifila]);

                }

            }


        }
        static void AgregarEnemigo(List<Cangrejo> grupoEnemigos,Pantalla _pantalla)
        {

                for (int ic = 0; ic < grupoEnemigos.Count; ic++)
                {
                    for (int ip = 0; ip < grupoEnemigos[ic].posicionEnemigo.Length; ip++)
                    {

                      _pantalla.AgregarElemento(grupoEnemigos[ic].posicionEnemigo[ip], grupoEnemigos[ic].sprite[ip]);

                    }
                }
           

        }


        
        static Player InputHeroe(Player jugador, int _velocidadHeroe)
        { 
            if (Console.KeyAvailable)
            {
                
                ConsoleKey tecla;

                tecla = Console.ReadKey(true).Key;
           

                //Thread.Sleep(2);
                switch (tecla)
                {
                    case ConsoleKey.LeftArrow:
                        // Thread.Sleep(250);
                        if (jugador.teclaXNegativo == false)//otro booleano para q se pueda mover izquierda
                        {
                            jugador.Mover(-1*_velocidadHeroe, 0);
                            jugador.teclaXNegativo = true;
                        }
                           
                        break;
                      case ConsoleKey.RightArrow:
                    

                        if (jugador.teclaXpos==false)
                        {
                            jugador.Mover(_velocidadHeroe, 0);
                            jugador.teclaXpos = true;
                        }
                      
                       
                        break;
                 
                      case ConsoleKey.Spacebar:



                        if (jugador.disparoActivo == false)
                        {
                            jugador.SpawnDisparoHeroe(jugador.posicionJugador[1]);
                            jugador.disparoActivo = true;
                        }
                        break;

                    


                }
                  return jugador;
            }
            else
            {
                jugador.teclaXNegativo = false;
                  jugador.teclaXpos = false;
                jugador.teclaYNegativo = false;
                jugador.teclaYpos = false;
                }

            return jugador;
        }
       
          static bool ContactoBalasHeroeEnemigos(Disparo _disparo,List <Cangrejo>_conjuntosEnemigos)
        {




            for (int i3 = 0; i3 < _conjuntosEnemigos.Count; i3++)
            {
                for (int i4 = 0; i4 < _conjuntosEnemigos[i3].posicionEnemigo.Length; i4++)
                {

                    if (_disparo.posicionDisparo.y == _conjuntosEnemigos[i3].posicionEnemigo[i4].y && _disparo.posicionDisparo.x == _conjuntosEnemigos[i3].posicionEnemigo[i4].x)
                    {
                        _conjuntosEnemigos[i3].enemigoMuerto = true;
                        _conjuntosEnemigos.Remove(_conjuntosEnemigos[i3]);
                        
                        //_conjuntosEnemigos[i2, i3].enemigoMuerto = true;
                        return true;
                    }
                }
            }                               
          
            
            return false;

           }


        static bool ContactoBalasHeroeCangrejoEspecial(CangrejoEspecial _cangrejoEspecial,Disparo _disparoHeroe)
        {
            for (int ic = 0; ic < _cangrejoEspecial.posicionCangrejoEspecial.Length; ic++) {

                if (_cangrejoEspecial.posicionCangrejoEspecial[ic].x == _disparoHeroe.posicionDisparo.x && _cangrejoEspecial.posicionCangrejoEspecial[ic].y == _disparoHeroe.posicionDisparo.y)
                {
                    return true;
                }
                    
               }

            return false;
        }
        static bool LimiteBalasHeroeEscenario(Player _jugador)
        {
            for(int i = 0; i < _jugador.listaDisparoHeroe.Count; i++)
            {

                if (_jugador.listaDisparoHeroe[i].posicionDisparo.y < 1)
                {
                    return true;
                }

            }
            return false;
        }
        static bool ContactoBalasEnemigosHeroe(Player _jugador, Disparo _disparoEnemigo)
        {
           
            for (int ic = 0; ic < _jugador.posicionJugador.Length; ic++)
            {
              //  Console.WriteLine("pos x jugador " + _jugador.posicionJugador[ic].x);
             

                //Console.WriteLine("pos y DISPARO " + _disparoEnemigo.posicionDisparo.y);
                if (_jugador.posicionJugador[ic].x == _disparoEnemigo.posicionDisparo.x && _jugador.posicionJugador[ic].y == _disparoEnemigo.posicionDisparo.y)
                {
                 //   Console.WriteLine("heroe muerto");
                //    _jugador.spriteHeroe="";
              

                 //   _disparoEnemigo.sprite = ' ';
                    return true;
                }
              
            }
            return false;
        }
   
   
        static bool AvisoTeclas(bool _teclaActivo)
        {

            return _teclaActivo;
        }
     



      static bool ContactoBalasDefensas(Disparo _disparo, Defensas [] _conjuntoDefensas, int cantidadTotalDefensas)
        {
            for (int i0 = 0; i0 < cantidadTotalDefensas; i0++) {

                for (int i = 0; i < _conjuntoDefensas[i0].posicionDefensas.Length; i++)
                {
                        if(_conjuntoDefensas[i0].spriteDefensas_1[i]!=' ') {
                        if (_disparo.posicionDisparo.y == _conjuntoDefensas[i0].posicionDefensas[i].y && _disparo.posicionDisparo.x == _conjuntoDefensas[i0].posicionDefensas[i].x)
                        {

                          //  _disparo.sprite = ' ';
                            _conjuntoDefensas[i0].spriteDefensas_1[i] = ' ';
                            return true;
                        }
                      }
                    }
                }
            return false;
        }


        static Cangrejo EleccionDisparoEnemigo(List<Cangrejo> enemigo, int cantidadEnemigos_total)
        {
            Random random = new Random();
            int numAleatorio;
            //int elegidoEnemigoFila;

            numAleatorio = random.Next(0, cantidadEnemigos_total);
          
            while (enemigo[numAleatorio].enemigoMuerto)
            {
                for (int i = 0; i < cantidadEnemigos_total; i++)
                {
                    numAleatorio = random.Next(1, cantidadEnemigos_total);

                }
            }
            //enemigo vivo 
          

            return enemigo[numAleatorio];
        }


        static bool ControlMetaEnemigos(List<Cangrejo> _enemigo)
        {
            //establece si algun enemigo vivo valga la redundancia llego a la meta


            for (int i = 0; i < _enemigo.Count; i++)
            {

                if (_enemigo[i].meta)
                {

                    return true;
                }
                
                
              
              
            }
            return false;
        
        }
        static bool LimiteBalasEnemigosFueraEscenario(Disparo _disparoEnemigo,int altoPantalla)
        {
            if (_disparoEnemigo.posicionDisparo.y > altoPantalla - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void ImprimirStringScreen(Pantalla matrizPrincipal,string objetoString,Posicion posicionObjeto)
        {
         //   int contadorX=0;
            for (int i = 0; i < objetoString.Length; i++)
            {
               
                    matrizPrincipal.AgregarElemento(posicionObjeto, objetoString[i]);
                    posicionObjeto.x += 1;
                
            }
        }


        static void DefinicionFinal()
        {
            Console.WriteLine("");
            string textoPregunta = " 1)Play Again";
            

            Console.WriteLine(textoPregunta);
            Console.WriteLine("");
            int numeroElegido = 0;
            string entrada = "";
            entrada = Console.ReadLine();

            while (entrada == "")
            {
                Console.WriteLine("please insert a number");

                entrada =Console.ReadLine();
            }
             numeroElegido =Convert.ToInt32(entrada);

            while (numeroElegido != 1)
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

                if (numeroElegido == 1)
                {
                    InicializacionJuego();
                }
                
            }


        
    }

}
