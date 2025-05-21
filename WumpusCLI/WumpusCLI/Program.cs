using System;
using System.Linq;
using Wumpus.Logic;

namespace WumpusFINAL
{
    class Program
    {
        static void Main(string[] args)
        {
            MostrarTituloAscii();
            Console.WriteLine("Presiona cualquier tecla para comenzar...");
            Console.ReadKey();
            Console.Clear();

            bool jugarDeNuevo = true;
            while (jugarDeNuevo)
            {
                Juego miJuego = new Juego();

                Console.WriteLine($"Iniciando partida en un laberinto de {miJuego.LaberintoActual.Tamano}x{miJuego.LaberintoActual.Tamano}...");

                while (miJuego.EstadoActual == EstadoJuego.Jugando)
                {
                    Console.Clear();
                    MostrarEstadoConsola(miJuego);
                    MostrarLeyenda();
                    MostrarInstrucciones();

                    Console.Write("Tu movimiento: ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    miJuego.ProcesarMovimiento(keyInfo.KeyChar);
                }

                Console.Clear();
                MostrarResultadoFinalConsola(miJuego);

                Console.WriteLine("\n--- Fin del Juego ---");
                Console.WriteLine("");
                Console.WriteLine("1. Quieres intentarlo de nuevo?");
                Console.WriteLine("2. Salir del juego");

                char opcion;
                do
                {
                    opcion = Console.ReadKey(true).KeyChar;
                    if (opcion != '1' && opcion != '2')
                    {
                        Console.WriteLine("\nOpción inválida. Presiona '1' para jugar de nuevo o '2' para salir.");
                    }
                } while (opcion != '1' && opcion != '2');

                if (opcion == '2')
                {
                    jugarDeNuevo = false;
                    Console.WriteLine("\nGracias por jugar!!");
                }
                Console.Clear();
            }
        }

        static void MostrarTituloAscii()
        {
            Console.WriteLine(@"___  ___                _       _    _                                 ");
            Console.WriteLine(@"|  \/  |               | |     | |  | |                                ");
            Console.WriteLine(@"| .  . |_   _ _ __   __| | ___ | |  | |_   _ _ __ ___  _ __  _   _ ___ ");
            Console.WriteLine(@"| |\/| | | | | '_ \ / _` |/ _ \  |/\| | | | | '_ ` _ \| '_ \| | | / __|");
            Console.WriteLine(@"| |  | | |_| | | | | (_| | (_) \  /\  / |_| | | | | | | |_) | |_| \__ \");
            Console.WriteLine(@"\_|  |_/\__,_|_| |_|\__,_|\___/ \/  \/ \__,_|_| |_| |_| .__/ \__,_|___/");
            Console.WriteLine(@"                                                      | |              ");
            Console.WriteLine(@"                                                      |_|              ");
            Console.WriteLine();
        }

        static void MostrarEstadoConsola(Juego juego)
        {
            Console.WriteLine($"--- Mundo del Wumpus ({juego.LaberintoActual.Tamano}x{juego.LaberintoActual.Tamano}) ---");
            for (int y = 0; y < juego.LaberintoActual.Tamano; y++)
            {
                for (int x = 0; x < juego.LaberintoActual.Tamano; x++)
                {
                    if (y == juego.JugadorActual.Y && x == juego.JugadorActual.X)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("J ");
                        Console.ResetColor();
                    }
                    else
                    {
                        switch (juego.LaberintoActual.Casillas[x, y])
                        {
                            case ContenidoCasilla.Visitado:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("x ");
                                Console.ResetColor();
                                break;
                            default:
                                Console.Write(". ");
                                break;
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");

            var (hedor, brisa) = juego.ObtenerPistasActuales();
            if (brisa)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(">>> Sientes una Brisa <<<");
                Console.ResetColor();
            }
            else { Console.WriteLine(); }
            if (hedor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>> Sientes un Hedor <<<");
                Console.ResetColor();
            }
            else { Console.WriteLine(); }
            Console.WriteLine("----------------------");
        }

        static void MostrarLeyenda()
        {
            Console.WriteLine("Leyenda:");
            Console.WriteLine("  J = Jugador");
            Console.WriteLine("  . = No explorada");
            Console.WriteLine("  x = Visitada");
            Console.WriteLine("----------------------");
        }

        static void MostrarInstrucciones()
        {
            Console.WriteLine("\nControles:");
            Console.WriteLine("W: Arriba | A: Izquierda | S: Abajo | D: Derecha");
        }

        static void MostrarResultadoFinalConsola(Juego juego)
        {
            Console.WriteLine($"--- Resultado Final ({juego.LaberintoActual.Tamano}x{juego.LaberintoActual.Tamano}) ---");
            for (int y = 0; y < juego.LaberintoActual.Tamano; y++)
            {
                for (int x = 0; x < juego.LaberintoActual.Tamano; x++)
                {
                    char simbolo = '.';
                    ConsoleColor colorOriginal = Console.ForegroundColor;

                    if (y == juego.JugadorActual.Y && x == juego.JugadorActual.X)
                    {
                        simbolo = 'J';
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (x == juego.LaberintoActual.WumpusX && y == juego.LaberintoActual.WumpusY)
                    {
                        simbolo = 'W';
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (x == juego.LaberintoActual.OroX && y == juego.LaberintoActual.OroY)
                    {
                        simbolo = 'O';
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (juego.LaberintoActual.Pozos.Any(p => p.x == x && p.y == y))
                    {
                        simbolo = 'P';
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (juego.LaberintoActual.Casillas[x, y] == ContenidoCasilla.Visitado)
                    {
                        simbolo = 'x';
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write(simbolo + " ");
                    Console.ForegroundColor = colorOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");
            Console.WriteLine($"\n{juego.MensajeResultado}");
        }
    }
}
