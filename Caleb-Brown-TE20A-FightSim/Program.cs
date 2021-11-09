using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

string fighter1 = "", fighter2 = "e";
int round = 1;
int maxRound = 5;
int maxHp = 100;
int damage = 0, damage2 = 0;

int minDmg = 10, maxDmg = 25;
int nameChoice1 = 0, nameChoice2 = 0, nameChoice3 = 0;
int random = 0;
int wager = 0;
int wins = 0;
bool simStart = false, gameStart = true;
bool intInput = false;
double hp1 = 100, hp2 = 100;
double money = 500;
double hpUpgrades = 1, dmgUpgrades = 1, accuracyUpgrades = 1;
double hpCost = 100, dmgCost = 100, accuracyCost = 100;
double doubleRand = 0f;
double damageModifier1 = 1, damageModifier2 = 1;
double accuracy = 0.75f, hitChance2 = 0.75f;
double baseHitChance = accuracy;
double priceMultiplier = 1.1f;
double baseCost = 100;
ConsoleKey chooseFighter = ConsoleKey.E;
ConsoleKey endGame = ConsoleKey.E;
ConsoleKey bet = ConsoleKey.E;
ConsoleKey upgradeChoice = ConsoleKey.E;
ConsoleKey attackChoice = ConsoleKey.E;

Random generator = new Random();

string[] names = File.ReadAllLines(@"names.txt");

string[] eStats = File.ReadAllLines(@"stats.txt");

Enemy currentEnemy = new Enemy();
Enemy e1 = new Enemy();
Enemy e2 = new Enemy();
Enemy e3 = new Enemy();

int enemyRndNum = generator.Next(1, eStats.Length);
string enemyStats = eStats[enemyRndNum];

string[] enemyStatsSplit = enemyStats.Split(';');

int.TryParse(enemyStatsSplit[0], out e1.hp);
int.TryParse(enemyStatsSplit[1], out e1.minDamage);
int.TryParse(enemyStatsSplit[2], out e1.maxDamage);
double.TryParse(enemyStatsSplit[3], out e1.accuracy);





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

    bool win = Fight();

    if (win == true)
    {
        wins++;
    }

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
    bool inputCheck = false;


    //Lets the player choose their characters name within certain parameters
    while (fighter1 == "" || fighter1.Length > 16)
    {
        Console.Clear();
        Console.WriteLine("Choose your figher's name");
        if (inputCheck == true && fighter1 == "")
        {
            Console.WriteLine("Your fighter's name cannot be blank");
        }
        if (fighter1.Length > 16)
        {
            Console.WriteLine("Your fighter's name cannot be longer than 16 characters");
        }
        fighter1 = Console.ReadLine();
        inputCheck = true;
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
            currentEnemy = e1;
        }

        if (chooseFighter == ConsoleKey.D2)
        {
            fighter2 = names[nameChoice2];
            currentEnemy = e2;
        }

        if (chooseFighter == ConsoleKey.D3)
        {
            fighter2 = names[nameChoice3];
            currentEnemy = e3;
        }
    }


    //Fighter upgrades
    if (round > 1)
    {
    upgradeStart:

        Console.Clear();

        while (upgradeChoice != ConsoleKey.Enter || money < hpCost && money < dmgCost && money < accuracyCost)
        {
            Console.WriteLine($"You can now upgrade {fighter1}");
            Console.WriteLine("Press \"1\", \"2\", \"3\" or ENTER to continue");

            hpCost = baseCost;
            priceMultiplier = 1.1f;
            hpCost *= Math.Pow(priceMultiplier, hpUpgrades);
            hpCost = Math.Round(hpCost, 0);

            Console.WriteLine($"1. Health {hpCost}");

            hpCost = baseCost;
            priceMultiplier = 1.1f;
            dmgCost *= Math.Pow(priceMultiplier, dmgUpgrades);
            dmgCost = Math.Round(dmgCost, 0);

            Console.WriteLine($"2. Damage {dmgCost}");

            hpCost = baseCost;
            priceMultiplier = 1.1f;
            accuracyCost *= Math.Pow(priceMultiplier, accuracyUpgrades);
            accuracyCost = Math.Round(accuracyCost, 0);

            Console.WriteLine($"3. Accuracy {accuracyCost}");
            Console.WriteLine();
            Console.WriteLine($"You have ${money} remaining");

            upgradeChoice = Console.ReadKey().Key;

            if (upgradeChoice == ConsoleKey.D1)
            {
                money -= hpCost;
                maxHp += 50;
                hpUpgrades += 1;
                goto upgradeStart;
            }

            if (upgradeChoice == ConsoleKey.D2)
            {
                money -= dmgCost;
                minDmg += 5;
                maxDmg += 5;
                dmgUpgrades += 1;
                goto upgradeStart;
            }

            if (upgradeChoice == ConsoleKey.D3)
            {
                money -= accuracyCost;
                accuracy += 0.05;
                baseHitChance = accuracy;
                accuracyUpgrades += 1;
                goto upgradeStart;
            }
        }

        //Opponent upgrades
        random = generator.Next(0, 11);
        //hp
        if (random >= 0 && random < 5)
        {
            currentEnemy.hp += 25;
        }

        if (random >= 5 && random < 10)
        {
            currentEnemy.hp += 50;
        }

        if (random == 10)
        {
            currentEnemy.hp += 75;
        }

        random = generator.Next(0, 11);
        //damage
        if (random >= 0 && random < 5)
        {
            currentEnemy.minDamage += 1;
            currentEnemy.maxDamage += 1;
        }

        if (random >= 5 && random < 10)
        {
            currentEnemy.minDamage += 5;
            currentEnemy.maxDamage += 5;
        }

        if (random == 10)
        {
            currentEnemy.minDamage += 10;
            currentEnemy.maxDamage += 10;
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

bool Fight()
{
    bool win;
    bool broke = false;

    hp1 = maxHp;
    hp2 = currentEnemy.hp;

    //fighting cycle that repeats to not allow you to proceed until one fighter wins or both run out of hp
    while (hp1 > 0 && hp2 > 0)
    {
        Console.Clear();
        Console.WriteLine($"Round {round}");
        Console.WriteLine();



        Console.WriteLine("Attacks:");
        Console.WriteLine("1. Big Punch");
        Console.WriteLine("High damage\n Low accuracy");
        Console.WriteLine();
        Console.WriteLine("2. Regular Punch");
        Console.WriteLine("Normal damage\n Normal accuracy");
        Console.WriteLine();
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

        Console.Clear();
        //Attack choices
        if (attackChoice == ConsoleKey.D1)
        {
            damage = generator.Next(minDmg, maxDmg) + 10;
            doubleRand = generator.NextDouble();
            if (doubleRand < accuracy * 0.75)
            {
                hp2 -= Math.Round(damage * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{fighter1} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D2)
        {
            damage = generator.Next(minDmg, maxDmg);
            doubleRand = generator.NextDouble();
            if (doubleRand < accuracy)
            {
                hp2 -= Math.Round(damage * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{fighter1} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D3)
        {
            damage = generator.Next(minDmg, maxDmg) - 5;
            doubleRand = generator.NextDouble();
            if (doubleRand < accuracy * 1.1)
            {
                hp2 -= Math.Round(damage * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{fighter1} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D4)
        {
            accuracy += 0.05;
            Console.WriteLine(accuracy);
        }

        if (attackChoice == ConsoleKey.D5)
        {
            damageModifier1 += 0.5;
            Console.WriteLine(damageModifier1);
        }

        doubleRand = generator.NextDouble();

        if (doubleRand <= 0.2)
        {
            damage2 = generator.Next(currentEnemy.minDamage, currentEnemy.maxDamage) + 10;
            Console.WriteLine($"Big punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance2 * 0.75)
            {
                hp1 -= Math.Round(damage2 * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{fighter2} missed!");
            }
        }

        else if (doubleRand <= 0.4)
        {
            damage2 = generator.Next(currentEnemy.minDamage, currentEnemy.maxDamage);
            Console.WriteLine($"regular punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance2)
            {
                hp1 -= Math.Round(damage2 * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{fighter2} missed!");
            }
        }
        else if (doubleRand <= 0.6)
        {
            damage2 = generator.Next(currentEnemy.minDamage, currentEnemy.maxDamage) - 5;
            Console.WriteLine($"Small punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < hitChance2 * 1.1)
            {
                hp1 -= Math.Round(damage2 * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{fighter2} missed!");
            }
        }
        else if (doubleRand <= 0.8)
        {
            hitChance2 += 0.05;
            Console.WriteLine(hitChance2);
        }

        if (doubleRand > 0.8)
        {
            damageModifier2 += 0.5;
            Console.WriteLine(damageModifier2);
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

        Console.WriteLine(doubleRand);
        Console.WriteLine(damage2);
        Console.ReadLine();
        //Thread.Sleep(TimeSpan.FromSeconds(3));
    }


    //Different endings to the round depending on how much hp each fighter has
    //Draw
    if (hp1 <= 0 && hp2 <= 0)
    {
        Console.WriteLine("Draw!");
        Console.WriteLine("Both fighters were knocked unconcious");
        Console.ReadLine();
        money += wager;
        win = false;
    }

    //Fighter 2 wins
    else if (hp1 <= 0)
    {
        Console.WriteLine($"{fighter1} down");
        Console.WriteLine($"{fighter2} wins!");
        Console.ReadLine();
        win = false;
    }

    //Fighter 1 wins
    else if (hp2 <= 0)
    {
        Console.WriteLine($"{fighter2} down");
        Console.WriteLine($"{fighter1} wins");
        Console.ReadLine();
        money += wager * 2;
        win = true;
    }

    else
    {
        Console.WriteLine("Somehow you broke the system, well done");
        Console.WriteLine("Though I will have to reprimand you");

        broke = true;
        win = false;
    }

    if (round < maxRound)
    {
        Console.WriteLine("Next round will start once you pick your next opponent");
        Console.ReadLine();
    }

    if (broke == true)
    {

    }

    round++;
    gameStart = true;
    simStart = false;


    accuracy = baseHitChance;
    damageModifier1 = 1;
    return win;
}