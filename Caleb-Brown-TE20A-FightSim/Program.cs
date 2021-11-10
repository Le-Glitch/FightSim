using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

int round = 1;
int maxRound = 5;
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
double doubleRand = 0;
double damageModifier1 = 1, damageModifier2 = 1;
double baseHitChance = 0.75;
double enemyBaseHitChance = 0;
double priceMultiplier = 1.1;
double baseCost = 100;
ConsoleKey chooseOpponent = ConsoleKey.E;
ConsoleKey endGame = ConsoleKey.E;
ConsoleKey bet = ConsoleKey.E;
ConsoleKey upgradeChoice = ConsoleKey.E;
ConsoleKey attackChoice = ConsoleKey.E;

Enemy e1 = new Enemy();
Enemy e2 = new Enemy();
Enemy e3 = new Enemy();
Enemy e4 = new Enemy();
Enemy e5 = new Enemy();
Enemy e6 = new Enemy();
Enemy e7 = new Enemy();
Enemy e8 = new Enemy();
Enemy e9 = new Enemy();
Enemy e10 = new Enemy();

Random generator = new Random();

string[] names = File.ReadAllLines(@"names.txt");

string[] eStats = File.ReadAllLines(@"stats.txt");

Player player = new Player();

//starting stats
player.hp = 100;
player.minDamage = 10;
player.maxDamage = 25;
player.accuracy = 0.75f;

Enemy enemy = new Enemy();

loadEnemies();

Enemy[] enemies = loadEnemies();

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

Enemy[] loadEnemies()
{

    string enemyStats = eStats[1];
    string[] enemyStatsSplit = enemyStats.Split(';');

    e1.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e1.hp);
    int.TryParse(enemyStatsSplit[2], out e1.minDamage);
    int.TryParse(enemyStatsSplit[3], out e1.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e1.accuracy);

    enemyStats = eStats[2];
    enemyStatsSplit = enemyStats.Split(';');

    e2.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e2.hp);
    int.TryParse(enemyStatsSplit[2], out e2.minDamage);
    int.TryParse(enemyStatsSplit[3], out e2.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e2.accuracy);

    enemyStats = eStats[3];
    enemyStatsSplit = enemyStats.Split(';');

    e3.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e3.hp);
    int.TryParse(enemyStatsSplit[2], out e3.minDamage);
    int.TryParse(enemyStatsSplit[3], out e3.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e3.accuracy);

    enemyStats = eStats[4];
    enemyStatsSplit = enemyStats.Split(';');

    e3.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e4.hp);
    int.TryParse(enemyStatsSplit[2], out e4.minDamage);
    int.TryParse(enemyStatsSplit[3], out e4.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e4.accuracy);

    enemyStats = eStats[5];
    enemyStatsSplit = enemyStats.Split(';');

    e3.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e5.hp);
    int.TryParse(enemyStatsSplit[2], out e5.minDamage);
    int.TryParse(enemyStatsSplit[3], out e5.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e5.accuracy);

    enemyStats = eStats[6];
    enemyStatsSplit = enemyStats.Split(';');

    e3.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e6.hp);
    int.TryParse(enemyStatsSplit[2], out e6.minDamage);
    int.TryParse(enemyStatsSplit[3], out e6.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e6.accuracy);

    enemyStats = eStats[7];
    enemyStatsSplit = enemyStats.Split(';');

    e3.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e7.hp);
    int.TryParse(enemyStatsSplit[2], out e7.minDamage);
    int.TryParse(enemyStatsSplit[3], out e7.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e7.accuracy);

    Enemy[] enemyArray = { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
    return enemyArray;
}



void Start()
{
    bool inputCheck = false;


    //Lets the player choose their characters name within certain parameters
    while (player.name == "" || player.name.Length > 16)
    {
        Console.Clear();
        Console.WriteLine("Choose your figher's name");
        if (inputCheck == true && player.name == "")
        {
            Console.WriteLine("Your fighter's name cannot be blank");
        }
        if (player.name.Length > 16)
        {
            Console.WriteLine("Your fighter's name cannot be longer than 16 characters");
        }
        player.name = Console.ReadLine();
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
    while (chooseOpponent != ConsoleKey.D1 && chooseOpponent != ConsoleKey.D2 && chooseOpponent != ConsoleKey.D3)
    {
        Console.Clear();
        Console.WriteLine("Choose an opponent by clicking \"1\", \"2\" or \"3\"");
        Console.WriteLine(e1.name);
        System.Console.WriteLine();
        Console.WriteLine($"{e1.hp}hp\n{e1.minDamage}-{e1.maxDamage}damage\n{e1.accuracy * 100}& accuracy\n");

        Console.WriteLine(e2.name);
        Console.WriteLine($"{e2.hp}hp\n{e2.minDamage}-{e2.maxDamage}damage\n{e2.accuracy * 100}& accuracy\n");

        Console.WriteLine(e3.name);
        Console.WriteLine($"{e3.hp}hp\n{e3.minDamage}-{e3.maxDamage}damage\n{e3.accuracy * 100}& accuracy");
        chooseOpponent = Console.ReadKey().Key;

        if (chooseOpponent == ConsoleKey.D1)
        {
            enemy = e1;
            enemyBaseHitChance = e1.accuracy;
        }

        if (chooseOpponent == ConsoleKey.D2)
        {
            enemy = e2;
            enemyBaseHitChance = e2.accuracy;
        }

        if (chooseOpponent == ConsoleKey.D3)
        {
            enemy = e3;
            enemyBaseHitChance = e3.accuracy;
        }
    }


    //Fighter upgrades
    if (round > 1)
    {
    upgradeStart:

        Console.Clear();

        while (upgradeChoice != ConsoleKey.Enter || money < hpCost && money < dmgCost && money < accuracyCost)
        {
            Console.WriteLine($"You can now upgrade {player.name}");
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
                player.maxHp += 50;
                hpUpgrades += 1;
                goto upgradeStart;
            }

            if (upgradeChoice == ConsoleKey.D2)
            {
                money -= dmgCost;
                player.minDamage += 5;
                player.maxDamage += 5;
                dmgUpgrades += 1;
                goto upgradeStart;
            }

            if (upgradeChoice == ConsoleKey.D3)
            {
                money -= accuracyCost;
                player.accuracy += 0.05;
                baseHitChance = player.accuracy;
                accuracyUpgrades += 1;
                goto upgradeStart;
            }
        }

        //Opponent upgrades
        random = generator.Next(0, 11);
        //hp
        if (random >= 0 && random < 5)
        {
            foreach (Enemy e in enemies)
            {
                e.hp = (int)(e.hp * 1.1);
            }
        }

        if (random >= 5 && random < 10)
        {
            foreach (Enemy e in enemies)
            {
                e.hp = (int)(e.hp * 1.25);
            }
        }

        if (random == 10)
        {
            foreach (Enemy e in enemies)
            {
                e.hp = (int)(e.hp * 1.5);
            }
        }

        random = generator.Next(0, 11);
        //damage
        if (random >= 0 && random < 5)
        {
            enemy.minDamage += 1;
            enemy.maxDamage += 1;
        }

        if (random >= 5 && random < 10)
        {
            enemy.minDamage += 5;
            enemy.maxDamage += 5;
        }

        if (random == 10)
        {
            enemy.minDamage += 10;
            enemy.maxDamage += 10;
        }
    }


    random = generator.Next(0, 11);
    //accuracy
    if (random >= 0 && random < 5)
    {
        enemy.accuracy += 0.01;
    }

    if (random >= 5 && random < 10)
    {
        enemy.accuracy += 0.05;
    }

    if (random == 10)
    {
        enemy.accuracy += 0.1;
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


    chooseOpponent = ConsoleKey.E;

    gameStart = false;
    simStart = true;
}

bool Fight()
{
    bool win;
    bool broke = false;

    player.hp = player.maxHp;
    enemy.hp = enemy.maxHp;

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
            player.damage = generator.Next(player.minDamage, player.maxDamage) + 10;
            doubleRand = generator.NextDouble();
            if (doubleRand < player.accuracy * 0.75)
            {
                hp2 -= Math.Round(player.damage * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{player.name} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D2)
        {
            player.damage = generator.Next(player.minDamage, player.maxDamage);
            doubleRand = generator.NextDouble();
            if (doubleRand < player.accuracy)
            {
                hp2 -= Math.Round(player.damage * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{player.name} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D3)
        {
            player.damage = generator.Next(player.minDamage, player.maxDamage) - 5;
            doubleRand = generator.NextDouble();
            if (doubleRand < player.accuracy * 1.1)
            {
                hp2 -= Math.Round(player.damage * damageModifier1, 0);
            }
            else
            {
                Console.WriteLine($"{player.name} missed!");
            }
        }

        if (attackChoice == ConsoleKey.D4)
        {
            player.accuracy += 0.05;
            Console.WriteLine(player.accuracy);
        }

        if (attackChoice == ConsoleKey.D5)
        {
            damageModifier1 += 0.5;
            Console.WriteLine(damageModifier1);
        }

        doubleRand = generator.NextDouble();

        if (doubleRand <= 0.2)
        {
            enemy.damage = generator.Next(enemy.minDamage, enemy.maxDamage) + 10;
            Console.WriteLine($"Big punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < enemy.accuracy * 0.75)
            {
                hp1 -= Math.Round(enemy.damage * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{enemy.name} missed!");
            }
        }

        else if (doubleRand <= 0.4)
        {
            enemy.damage = generator.Next(enemy.minDamage, enemy.maxDamage);
            Console.WriteLine($"regular punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < enemy.accuracy)
            {
                hp1 -= Math.Round(enemy.damage * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{enemy.name} missed!");
            }
        }
        else if (doubleRand <= 0.6)
        {
            enemy.damage = generator.Next(enemy.minDamage, enemy.maxDamage) - 5;
            Console.WriteLine($"Small punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < enemy.accuracy * 1.1)
            {
                hp1 -= Math.Round(enemy.damage * damageModifier2, 0);
            }
            else
            {
                Console.WriteLine($"{enemy.name} missed!");
            }
        }
        else if (doubleRand <= 0.8)
        {
            enemy.accuracy += 0.05;
            Console.WriteLine(enemy.accuracy);
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

        Console.WriteLine($"{player.name} has {hp1}hp remaining");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine($"{player.name} has {hp2}hp remaining");

        Console.WriteLine(doubleRand);
        Console.WriteLine(enemy.damage);
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
        Console.WriteLine($"{player.name} down");
        Console.WriteLine($"{enemy.name} wins!");
        Console.ReadLine();
        win = false;
    }

    //Fighter 1 wins
    else if (hp2 <= 0)
    {
        Console.WriteLine($"{enemy.name} down");
        Console.WriteLine($"{player.name} wins");
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


    player.accuracy = baseHitChance;
    damageModifier1 = 1;
    return win;
}