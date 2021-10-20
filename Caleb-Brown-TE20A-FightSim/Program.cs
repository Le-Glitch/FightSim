using System;
using System.IO;

int round = 1;
string fighter1 = "", fighter2 = "e";
int hp1 = 100, hp2 = 100;
int damage1 = 0, damage2 = 0;
int money = 1000;
bool simStart = false, mainMenu = true;
float hitChance = 0f;
int wager = 0;
ConsoleKey chooseWager = ConsoleKey.E;

Random generator = new Random();

Console.WriteLine("Hello and welcome to this epic and awesome battle simulator");
Console.WriteLine("You will partake in 5 different battles where you can wager for or against your fighter");
Console.WriteLine("After each round you can improve your fighter with the money you've accumulated");
Console.WriteLine("If you wish to participate in more simulations you are free to do so with your winning champion or a completely new one");
Console.ReadLine();
Console.Clear();

while (mainMenu == true)
{
    Start();
}

while (simStart == true)
{
    Fight();
}

void Start()
{
    while(fighter1 != ""){
    Console.WriteLine("Choose your figher's name");
    fighter1 = Console.ReadLine();
    }

    mainMenu = false;
    simStart = true;
}

void Fight()
{
    Console.WriteLine("Enter \"1\" or \"2\" to choose which fighter you want to wager for");
    while(chooseWager != ConsoleKey.D1 && chooseWager != ConsoleKey.D2)
    {
        chooseWager = Console.ReadKey().Key;
        Console.Clear();
    }
    

    while(hp1 > 0 && hp2 > 0)
    {

    }

    if(hp1 <= 0 && hp2 <= 0)
    {
        Console.WriteLine("Draw!");
        Console.WriteLine("Both fighters were knocked unconcious");
    }

    if(hp1 <= 0)
    {
        Console.WriteLine($"{fighter1} down");
        Console.WriteLine($"{fighter2} wins!");

        money += wager;
    }

    if(hp2 <= 0)
    {
        Console.WriteLine($"{fighter2} down");
        Console.WriteLine($"{fighter1} wins");

        money -= wager;
    }
}