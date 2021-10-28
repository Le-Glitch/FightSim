using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

string fighter1 = "", fighter2 = "e";
int round = 1;
int maxRound = 5;
int maxHp1 = 100, maxHp2 = 100;
int damage1 = 0, damage2 = 0;
int minDmg1 = 10, minDmg2 = 10, maxDmg1 = 25, maxDmg2 = 25;
int nameChoice1 = 0, nameChoice2 = 0, nameChoice3 = 0;
int random = 0;
int wager = 0;
bool simStart = false, gameStart = true;
bool intInput = false;
double hp1 = 100, hp2 = 100;
double money = 500;
double hpUpgrades = 1, dmgUpgrades = 1, accuracyUpgrades = 1;
double hpCost = 100, dmgCost = 100, accuracyCost = 100;
double doubleRand = 0f;
double damageModifier1 = 1, damageModifier2 = 1;
double hitChance1 = 0.75f, hitChance2 = 0.75f;
double baseHitChance = hitChance1;
double priceMultiplier = 1.1f;
ConsoleKey chooseFighter = ConsoleKey.E;
ConsoleKey endGame = ConsoleKey.E;
ConsoleKey bet = ConsoleKey.E;
ConsoleKey upgradeChoice = ConsoleKey.E;
ConsoleKey attackChoice = ConsoleKey.E;

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
                maxHp1 += 50;
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
                baseHitChance = hitChance1;
                goto upgradeStart;
            }
        }

        //Opponent upgrades
        random = generator.Next(0, 11);
        //hp
        if (random >= 0 && random < 5)
        {
            maxHp2 += 25;
        }

        if (random >= 5 && random < 10)
        {
            maxHp2 += 50;
        }

        if (random == 10)
        {
            maxHp2 += 75;
        }

        random = generator.Next(0, 11);
        //damage
        if (random >= 0 && random < 5)
        {
            minDmg2 += 1;
            maxDmg2 += 1;
        }

        if (random >= 5 && random < 10)
        {
            minDmg2 += 5;
            maxDmg2 += 5;
        }

        if (random == 10)
        {
            minDmg2 += 10;
            maxDmg2 += 10;
        }
    }


    random = generator.Next(0, 11);
    //hitchance
    if (random >= 0 && random < 5)
    {
        hitChance2 += 0.01;
    }

    if (random >= 5 && random < 10)
    {
        hitChance2 += 0.05;
    }

    if (random == 10)
    {
        hitChance2 += 0.1;
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
        hp1 = maxHp1;
        hp2 = maxHp2;
        Console.Clear();
        Console.WriteLine($"Round {round}");
        Console.WriteLine();



        Console.WriteLine("Attacks:");
        Console.WriteLine("1. Big Punch");
        Console.WriteLine("High damage\n Low accuracy");
        Console.WriteLine();
        Console.WriteLine("2. Regular Punch");
        Console.WriteLine("Normal damage\n Normal accuracy");
        Console.WriteLine("3. Small Punch");
        Console.WriteLine("Low damage\n High accuracy");
        Console.WriteLine();
        Console.WriteLine("4. Focus");
        Console.WriteLine("Increase accuracy");
        Console.WriteLine();
        Console.WriteLine("5. Strength");
        Console.WriteLine("Increase damage");
        Console.WriteLine();
        Console.WriteLine("Press \"1\", \"2\", \"3\", \"4\" or \"5\" to select a move");

        attackChoice = Console.ReadKey().Key;
        //Attack choices
        if (attackChoice == ConsoleKey.D1)
        {
            damage1 = generator.Next(minDmg1, maxDmg1) + 10;
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance1 * 0.75)
            {
                hp2 -= Math.Round(damage1 * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{fighter1} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D2)
        {
            damage1 = generator.Next(minDmg1, maxDmg1);
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance1)
            {
                hp2 -= Math.Round(damage1 * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{fighter1} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D)
        {
            damage1 = generator.Next(minDmg1, maxDmg1) - 5;
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance1 * 1.1)
            {
                hp2 -= Math.Round(damage1 * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{fighter1} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D4)
        {
            hitChance1 += 0.05;
            Console.WriteLine(hitChance1);
        }

        if (attackChoice == ConsoleKey.D5)
        {
            damageModifier1 += 0.5;
            Console.WriteLine(damageModifier1);
        }

        doubleRand = generator.NextDouble();
        
        if(doubleRand <= 0.2)
        {
            damage1 = generator.Next(minDmg2, maxDmg2) + 10;
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance2 * 0.75)
            {
                hp2 -= Math.Round(damage2 * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{fighter2} missed!");
            }
        }
        
        if (doubleRand <= 0.4 && doubleRand > 0.4)
        {
            damage1 = generator.Next(minDmg2, maxDmg2);
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance2)
            {
                hp2 -= Math.Round(damage2 * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{fighter2} missed!");
            }
        }
        
        if (doubleRand <= 0.6 && doubleRand > 0.4)
        {
            damage1 = generator.Next(minDmg2, maxDmg2) -5;
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance2 * 1.1)
            {
                hp2 -= Math.Round(damage2 * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{fighter2} missed!");
            }
        }

        if (doubleRand <= 0.8 && doubleRand > 0.6)
        {
            hitChance1 += 0.05;
            Console.WriteLine(hitChance1);
        }

        if (doubleRand > 0.8)
        {
            damageModifier1 += 0.5;
            Console.WriteLine(damageModifier1);
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


    hitChance1 = baseHitChance;
    damageModifier1 = 1;
    hp1 = maxHp1;
    hp2 = maxHp2;
}