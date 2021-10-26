using System;
using System.IO;
using System.Collections.Generic;

int round = 1;
string fighter1 = "", fighter2 = "e";
int hp1 = 100, hp2 = 100;
int damage1 = 0, damage2 = 0;
int money = 1000;
bool simStart = false, gameStart = true;
double hitChance = 0f;
int wager = 0;
int damageModifier1 = 1, damageModifier2 = 1;
ConsoleKey chooseFighter = ConsoleKey.E;
int minDmg1 = 5, minDmg2 = 5, maxDmg1 = 50, maxDmg2 = 50;

int nameChoice1 = 0, nameChoice2 = 0, nameChoice3 = 0;

Random generator = new Random();

string[] names = File.ReadAllLines(@"names.txt");

Console.WriteLine("Hello and welcome to this epic and awesome battle simulator");
Console.WriteLine("You will partake in 5 different battles where you can wager for or against your fighter");
Console.WriteLine("After each round you can improve your fighter with the money you've accumulated");
Console.WriteLine("If you wish to participate again you can do so while retaining your stats");
Console.ReadLine();
Console.Clear();

while (gameStart == true)
{
    Start();
}

while (simStart == true)
{
    Fight();
}

void Start()
{
    while (fighter1 == "" || fighter1.Length > 16)
    {
        Console.Clear();
        Console.WriteLine("Choose your figher's name");
        fighter1 = Console.ReadLine();
    }

    while (nameChoice1 == nameChoice2 || nameChoice1 == nameChoice3 || nameChoice2 == nameChoice3)
    {
        nameChoice1 = generator.Next(0, names.Length);
        nameChoice2 = generator.Next(0, names.Length);
        nameChoice3 = generator.Next(0, names.Length);
    }

    while (chooseFighter != ConsoleKey.D1 && chooseFighter != ConsoleKey.D2 && chooseFighter != ConsoleKey.D3)
    {
        Console.Clear();
        Console.WriteLine("Choose an opponent by clicking \"1\", \"2\" or \"3\"");
        Console.WriteLine(names[nameChoice1]);
        Console.WriteLine(names[nameChoice2]);
        Console.WriteLine(names[nameChoice3]);
        chooseFighter = Console.ReadKey().Key;

        if (chooseFighter == ConsoleKey.D1)
        {
            fighter2 = names[nameChoice1];
        }

        if (chooseFighter == ConsoleKey.D2)
        {
            fighter2 = names[nameChoice2];
        }

        if (chooseFighter == ConsoleKey.D3)
        {
            fighter2 = names[nameChoice3];
        }
    }

    gameStart = false;
    simStart = true;
}

void Fight()
{

    Console.WriteLine($"Round {round}!");

    while (hp1 > 0 && hp2 > 0)
    {
        Console.Clear();
        damage1 = generator.Next(minDmg1, maxDmg1) * damageModifier1;
        damage2 = generator.Next(minDmg2, maxDmg2) * damageModifier2;

        // hitChance = generator.NextDouble();

        Console.WriteLine(damage1);
        Console.WriteLine(damage2);

        hp1 -= damage2;
        hp2 -= damage1;

        if (hp1 < 0)
        {
            hp1 = 0;
        }

        if (hp2 < 0)
        {
            hp2 = 0;
        }

        Console.WriteLine($"{fighter1} has {hp1}hp remaining");
        Console.WriteLine($"{fighter2} has {hp2}hp remaining");

        simStart = false;
        Console.ReadLine();
    }



    if (hp1 <= 0 && hp2 <= 0)
    {
        Console.WriteLine("Draw!");
        Console.WriteLine("Both fighters were knocked unconcious");
        Console.ReadLine();
    }

    if (hp1 <= 0)
    {
        Console.WriteLine($"{fighter1} down");
        Console.WriteLine($"{fighter2} wins!");
        Console.ReadLine();

    }

    if (hp2 <= 0)
    {
        Console.WriteLine($"{fighter2} down");
        Console.WriteLine($"{fighter1} wins");
        Console.ReadLine();
    }
}