
![image](https://github.com/user-attachments/assets/848a43d1-59dc-468b-b3d0-6d99a1fb83fa)

Wumpus World en Windows Forms
=================================

El objetivo de este proyecto fue enlazar la lógica de un proyecto anterior (Wumpus en Linea de Comandos) a Windows Forms y así darle una interfaz gráfica.

## ¿Cómo se juega?
- El objetivo es encontrar el oro (💰) en el laberinto sin caer en pozos (🕳️) ni ser atrapado por el Wumpus (🦖).
- Te mueves usando las teclas **WASD** o las flechas del teclado.
- El personaje es representado por 👤.
- Si sientes un "Hedor" hay un Wumpus cerca. Si sientes una "Brisa" hay un pozo cerca.
- El juego termina si encuentras el oro (ganas), caes en un pozo o te encuentra el Wumpus (pierdes).

## Estructura del proyecto
- **WumpusWinForms**: Interfaz gráfica del juego.
- **Wumpus.Logic**: Lógica del juego y del laberinto.

## Requisitos
- .NET Framework 4.7.2

## Ejecución
Abre la solución en Visual Studio y ejecuta el proyecto `WumpusWinForms`.
