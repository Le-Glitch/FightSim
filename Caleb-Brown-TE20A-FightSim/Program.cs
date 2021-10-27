using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

string fighter1 = "", fighter2 = "e";
int round = 1;
int maxRound = 5;
int hp1 = 100, hp2 = 100;
int damage1 = 0, damage2 = 0;
int damageModifier1 = 1, damageModifier2 = 1;
int minDmg1 = 5, minDmg2 = 5, maxDmg1 = 25, maxDmg2 = 25;
int nameChoice1 = 0, nameChoice2 = 0, nameChoice3 = 0;
int random = 0;
int wager = 0;
bool simStart = false, gameStart = true;
bool intInput = false;
double money = 500;
double hpUpgrades = 1, dmgUpgrades = 1, accuracyUpgrades = 1;
double hpCost = 100, dmgCost = 100, accuracyCost = 100;
double doubleRand = 0f;
double hitChance1 = 0.75f, hitChance2 = 0.75f;
double priceMultiplier = 1.1f;
ConsoleKey chooseFighter = ConsoleKey.E;
ConsoleKey endGame = ConsoleKey.E;
ConsoleKey bet = ConsoleKey.E;
ConsoleKey upgradeChoice = ConsoleKey.E;

Random generator = new Random();

string[] names = File.ReadAllLines(@"names.txt");

Console.WriteLine("Hello and welcome to this epic and awesome battle simulator");
Console.WriteLine("You will partake in 5 different battles where you can wager for or against your fighter");
Console.WriteLine("After each round you can improve your fighter with the money you've accumulated");
Console.WriteLine("Bare in mind that the more you play the more your opponent will also improve");
Console.WriteLine("If you wish to participate again you can do so while retaining your stats");
Console.ReadLine();
Console.Clear();

nextRound:
while (gameStart == true && round <= 5)
{
    Start();
}

while (simStart == true)
{
    Fight();
    goto nextRound;
}

if (round > maxRound)
{
    while (endGame != ConsoleKey.Enter && endGame != ConsoleKey.Escape)
    {
        Console.Clear();
        if (round == 6)
        {
            Console.WriteLine("You have completed the first 5 rounds");
        }
        else
        {
            Console.WriteLine("You have completed another 5 rounds");
        }

        Console.WriteLine("Press ENTER if you wish to continue playing or ESC if you wish to end the game");

        endGame = Console.ReadKey().Key;
        if (endGame == ConsoleKey.Enter)
        {
            maxRound += 5;
            gameStart = true;
            goto nextRound;
        }
    }

}

void Start()
{

    Console.WriteLine(money);
    Console.ReadLine();


    //Lets the player choose their characters name within certain parameters
    while (fighter1 == "" || fighter1.Length > 16)
    {
        Console.Clear();
        Console.WriteLine("Choose your figher's name");
        if (fighter1.Length > 16)
        {
            Console.WriteLine("Your fighter's name cannot be longer than 16 characters");
        }
        fighter1 = Console.ReadLine();
    }


    //generates random names to pick for opponent
    nameChoice1 = generator.Next(0, names.Length);
    nameChoice2 = generator.Next(0, names.Length);
    nameChoice3 = generator.Next(0, names.Length);

    while (nameChoice1 == nameChoice2 || nameChoice1 == nameChoice3 || nameChoice2 == nameChoice3)
    {
        nameChoice1 = generator.Next(0, names.Length);
        nameChoice2 = generator.Next(0, names.Length);
        nameChoice3 = generator.Next(0, names.Length);
    }

    //Gives the player a choice between 3 different names for the opponent
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


    //Fighter upgrades
    if (round > 1)
    {
    upgradeStart:

        Console.Clear();
        priceMultiplier = 1.1f;
        hpCost *= Math.Pow(priceMultiplier, hpUpgrades);
        hpCost = Math.Round(hpCost, 0);

        priceMultiplier = 1.1f;
        dmgCost *= Math.Pow(priceMultiplier, dmgUpgrades);
        dmgCost = Math.Round(dmgCost, 0);

        priceMultiplier = 1.1f;
        accuracyCost *= Math.Pow(priceMultiplier, accuracyUpgrades);
        accuracyCost = Math.Round(accuracyCost, 0);

        while (upgradeChoice != ConsoleKey.Enter)
        {
            Console.WriteLine($"You can now upgrade {fighter1}");
            Console.WriteLine("Press \"1\", \"2\", \"3\" or ENTER to continue");
            Console.WriteLine($"1. Health {hpCost}");
            Console.WriteLine($"2. Damage {dmgCost}");
            Console.WriteLine($"3. Accuracy {accuracyCost}");

            upgradeChoice = Console.ReadKey().Key;

            if (upgradeChoice == ConsoleKey.D1)
            {
                money -= hpCost;
                hp1 += 50;
                goto upgradeStart;
            }

            if (upgradeChoice == ConsoleKey.D2)
            {
                money -= dmgCost;
                minDmg1 += 5;
                maxDmg1 += 5;
                goto upgradeStart;
            }

            if (upgradeChoice == ConsoleKey.D3)
            {
                money -= accuracyCost;
                hitChance1 += 0.05;
                goto upgradeStart;
            }
        }
    }

    //Opponent upgrades
    random = generator.Next(0, 4);
    //hp
    if (random == 0)
    {
        hp2 += 25;
    }
    
    if (random == 1 || random == 2)
    {
        hp2 += 50;
    }
    
    if(random == 3)
    {
        hp2 += 75;
    }
    
    random = generator.Next(0, 4);
    //damage
    if (random == 0)
    {
        minDmg2 += 1;
        maxDmg2 += 1;
    }
    
    if (random == 1 || random == 2)
    {
        minDmg2 += 5;
        maxDmg2 += 5;
    }
    
    if (random == 3)
    {
        minDmg2 += 10;
        maxDmg2 += 10;
    }
    
    random = generator.Next(0, 4);
    //hitchance
    if (random == 0)
    {
        hitChance2 += 0;
    }

    if (random == 1)
    {
        hitChance2 += 0.1;
    }
    
    if (random == 2)
    {
        hitChance2 += 0.5;
    }

    //Betting on which fighter will win
    while (wager <= money && bet != ConsoleKey.D1 && bet != ConsoleKey.D2)
    {
        Console.Clear();
        Console.WriteLine("You can now bet for one of the fighters");
        Console.WriteLine("Press \"1\" to bet on your fighter or \"2\" to bet on your opponent");

        bet = Console.ReadKey().Key;

        while (bet == ConsoleKey.D1 && intInput == false)
        {
            Console.Clear();
            Console.WriteLine("Enter the amount you wish to bet");
            intInput = int.TryParse(Console.ReadLine(), out wager);
        }
        while (bet == ConsoleKey.D2 && intInput == false)
        {
            Console.Clear();
            Console.WriteLine("Enter the amount you wish to bet");
            intInput = int.TryParse(Console.ReadLine(), out wager);
            wager *= -1;
        }

        money -= wager;
    }


    chooseFighter = ConsoleKey.E;

    gameStart = false;
    simStart = true;
}

void Fight()
{

    //fighting cycle that repeats to not allow you to proceed until one fighter wins or both run out of hp
    while (hp1 > 0 && hp2 > 0)
    {
        Console.Clear();
        Console.WriteLine($"Round {round}");

        damage1 = generator.Next(minDmg1, maxDmg1) * damageModifier1;
        damage2 = generator.Next(minDmg2, maxDmg2) * damageModifier2;



        doubleRand = generator.NextDouble();
        if (doubleRand < hitChance2)
        {
            hp1 -= damage2;
        }
        else
        {
            Console.WriteLine($"{fighter2} missed");
        }

        doubleRand = generator.NextDouble();
        if (doubleRand < hitChance1)
        {
            hp2 -= damage1;
        }
        else
        {
            Console.WriteLine($"{fighter1} missed");
        }

        if (hp1 < 0)
        {
            hp1 = 0;
        }

        if (hp2 < 0)
        {
            hp2 = 0;
        }

        Console.WriteLine($"{fighter1} has {hp1}hp remaining");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine($"{fighter2} has {hp2}hp remaining");


        Thread.Sleep(TimeSpan.FromSeconds(3));
    }



    //Different endings to the round depending on how much hp each fighter has
    //Draw
    if (hp1 <= 0 && hp2 <= 0)
    {
        Console.WriteLine("Draw!");
        Console.WriteLine("Both fighters were knocked unconcious");
        Console.ReadLine();
        money += wager;
    }

    //Fighter 2 wins
    else if (hp1 <= 0)
    {
        Console.WriteLine($"{fighter1} down");
        Console.WriteLine($"{fighter2} wins!");
        Console.ReadLine();
        money -= wager;
    }

    //Fighter 1 wins
    else if (hp2 <= 0)
    {
        Console.WriteLine($"{fighter2} down");
        Console.WriteLine($"{fighter1} wins");
        Console.ReadLine();
        money += wager * 2;
    }

    if (round < maxRound)
    {
        Console.WriteLine("Next round will start once you pick your next opponent");
        Console.ReadLine();
    }
    round++;
    gameStart = true;
    simStart = false;



    hp1 = 100;
    hp2 = 100;
}