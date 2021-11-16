using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

int round = 1;
int maxRound = 5;

int random = 0;
int wager = 0;
int wins = 0;
bool simStart = false, gameStart = true;
bool intInput = false;
double money = 500;
double hpUpgrades = 1, dmgUpgrades = 1, accuracyUpgrades = 1;
double hpCost = 100, dmgCost = 100, accuracyCost = 100;
double doubleRand = 0;
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

Enemy enemyChoice1 = new Enemy(), enemyChoice2 = new Enemy(), enemyChoice3 = new Enemy();

Random generator = new Random();

string[] eStats = File.ReadAllLines(@"stats.txt");

Player player = new Player();

//starting stats
player.name = "";
player.maxHp = 100;
player.minDamage = 10;
player.maxDamage = 25;
player.damageModifier = 1;
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
    int.TryParse(enemyStatsSplit[1], out e1.maxHp);
    int.TryParse(enemyStatsSplit[2], out e1.minDamage);
    int.TryParse(enemyStatsSplit[3], out e1.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e1.accuracy);

    enemyStats = eStats[2];
    enemyStatsSplit = enemyStats.Split(';');

    e2.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e2.maxHp);
    int.TryParse(enemyStatsSplit[2], out e2.minDamage);
    int.TryParse(enemyStatsSplit[3], out e2.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e2.accuracy);

    enemyStats = eStats[3];
    enemyStatsSplit = enemyStats.Split(';');

    e3.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e3.maxHp);
    int.TryParse(enemyStatsSplit[2], out e3.minDamage);
    int.TryParse(enemyStatsSplit[3], out e3.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e3.accuracy);

    enemyStats = eStats[4];
    enemyStatsSplit = enemyStats.Split(';');

    e4.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e4.maxHp);
    int.TryParse(enemyStatsSplit[2], out e4.minDamage);
    int.TryParse(enemyStatsSplit[3], out e4.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e4.accuracy);

    enemyStats = eStats[5];
    enemyStatsSplit = enemyStats.Split(';');

    e5.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e5.maxHp);
    int.TryParse(enemyStatsSplit[2], out e5.minDamage);
    int.TryParse(enemyStatsSplit[3], out e5.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e5.accuracy);

    enemyStats = eStats[6];
    enemyStatsSplit = enemyStats.Split(';');

    e6.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e6.maxHp);
    int.TryParse(enemyStatsSplit[2], out e6.minDamage);
    int.TryParse(enemyStatsSplit[3], out e6.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e6.accuracy);

    enemyStats = eStats[7];
    enemyStatsSplit = enemyStats.Split(';');

    e7.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e7.maxHp);
    int.TryParse(enemyStatsSplit[2], out e7.minDamage);
    int.TryParse(enemyStatsSplit[3], out e7.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e7.accuracy);

    e8.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e8.maxHp);
    int.TryParse(enemyStatsSplit[2], out e8.minDamage);
    int.TryParse(enemyStatsSplit[3], out e8.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e8.accuracy);

    e9.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e9.maxHp);
    int.TryParse(enemyStatsSplit[2], out e9.minDamage);
    int.TryParse(enemyStatsSplit[3], out e9.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e9.accuracy);

    e10.name = enemyStatsSplit[0];
    int.TryParse(enemyStatsSplit[1], out e10.maxHp);
    int.TryParse(enemyStatsSplit[2], out e10.minDamage);
    int.TryParse(enemyStatsSplit[3], out e10.maxDamage);
    double.TryParse(enemyStatsSplit[4], out e10.accuracy);

    Enemy[] enemyArray = { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
    return enemyArray;
}



void Start()
{
    bool inputCheck = false;
    Console.WriteLine($"Current wins: {wins}");


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

    enemyChoice1 = enemies[generator.Next(0, eStats.Length - 1)];
    enemyChoice2 = enemies[generator.Next(0, eStats.Length - 1)];
    enemyChoice3 = enemies[generator.Next(0, eStats.Length - 1)];

    //generates a set of random opponents
    while (enemyChoice1.name == enemyChoice2.name || enemyChoice1.name == enemyChoice3.name || enemyChoice2.name == enemyChoice3.name)
    {
        enemyChoice1 = enemies[generator.Next(0, eStats.Length - 1)];
        enemyChoice2 = enemies[generator.Next(0, eStats.Length - 1)];
        enemyChoice3 = enemies[generator.Next(0, eStats.Length - 1)];
    }

    //Gives the player a choice between 3 different names for the opponent
    while (chooseOpponent != ConsoleKey.D1 && chooseOpponent != ConsoleKey.D2 && chooseOpponent != ConsoleKey.D3)
    {
        Console.Clear();
        Console.WriteLine("Choose an opponent by clicking \"1\", \"2\" or \"3\"\n");
        Console.WriteLine(enemyChoice1.name);
        Console.WriteLine($"{enemyChoice1.maxHp}hp\n{enemyChoice1.minDamage}-{enemyChoice1.maxDamage} damage\n{enemyChoice1.accuracy * 100}% accuracy\n");

        Console.WriteLine(enemyChoice2.name);
        Console.WriteLine($"{enemyChoice2.maxHp}hp\n{enemyChoice2.minDamage}-{enemyChoice2.maxDamage} damage\n{enemyChoice2.accuracy * 100}% accuracy\n");

        Console.WriteLine(enemyChoice3.name);
        Console.WriteLine($"{enemyChoice3.maxHp}hp\n{enemyChoice3.minDamage}-{enemyChoice3.maxDamage} damage\n{enemyChoice3.accuracy * 100}% accuracy");
        chooseOpponent = Console.ReadKey().Key;

        if (chooseOpponent == ConsoleKey.D1)
        {
            enemy = enemyChoice1;
            enemyBaseHitChance = enemyChoice1.accuracy;
        }

        if (chooseOpponent == ConsoleKey.D2)
        {
            enemy = enemyChoice2;
            enemyBaseHitChance = enemyChoice2.accuracy;
        }

        if (chooseOpponent == ConsoleKey.D3)
        {
            enemy = enemyChoice3;
            enemyBaseHitChance = enemyChoice3.accuracy;
        }
    }


    //Fighter upgrades
    if (round > 1)
    {
    upgradeStart:



        while (upgradeChoice != ConsoleKey.Enter || money > hpCost && money > dmgCost && money > accuracyCost)
        {
            Console.Clear();
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
                e.maxHp = (int)(e.hp * 1.1);
            }
        }

        if (random >= 5 && random < 10)
        {
            foreach (Enemy e in enemies)
            {
                e.maxHp = (int)(e.hp * 1.25);
            }
        }

        if (random == 10)
        {
            foreach (Enemy e in enemies)
            {
                e.maxHp = (int)(e.hp * 1.5);
            }
        }

        random = generator.Next(0, 11);
        //damage
        if (random >= 0 && random < 5)
        {
            foreach (Enemy e in enemies)
            {
                e.minDamage = (int)(e.minDamage * 1.1);
                e.maxDamage = (int)(e.maxDamage * 1.1);
            }
        }

        if (random >= 5 && random < 10)
        {
            foreach (Enemy e in enemies)
            {
                e.minDamage = (int)(e.minDamage * 1.3);
                e.maxDamage = (int)(e.maxDamage * 1.3);
            }
        }

        if (random == 10)
        {
            foreach (Enemy e in enemies)
            {
                e.minDamage = (int)(e.minDamage * 1.5);
                e.maxDamage = (int)(e.maxDamage * 1.5);
            }
        }

        random = generator.Next(0, 11);
        //accuracy
        if (random >= 0 && random < 5)
        {
            foreach (Enemy e in enemies)
            {
                e.accuracy *= 1.05;
                e.accuracy = Math.Round(e.accuracy, 2);
            }
        }

        if (random >= 5 && random < 10)
        {
            foreach (Enemy e in enemies)
            {
                e.accuracy *= 1.1;
                e.accuracy = Math.Round(e.accuracy, 2);
            }
        }

        if (random == 10)
        {
            foreach (Enemy e in enemies)
            {
                e.accuracy *= 1.25;
                e.accuracy = Math.Round(e.accuracy, 2);
            }
        }
    }


    //Betting on which fighter will win
    while (intInput == false && (bet != ConsoleKey.D1 || bet != ConsoleKey.D2))
    {
        Console.Clear();
        if (wager > money)
        {
            Console.WriteLine("You can't bet with more money than you have!");
        }

        Console.WriteLine("You can now bet for one of the fighters");
        Console.WriteLine("Press \"1\" to bet on your fighter or \"2\" to bet on your opponent");
        Console.WriteLine($"You have ${money}");

        bet = Console.ReadKey().Key;

        while ((bet == ConsoleKey.D1 && intInput == false) || wager > money)
        {
            Console.Clear();
            Console.WriteLine("Enter the amount you wish to bet");
            intInput = int.TryParse(Console.ReadLine(), out wager);
        }
        while ((bet == ConsoleKey.D2 && intInput == false) || wager > money)
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
    while (player.hp > 0 && enemy.hp > 0)
    {
        Console.Clear();
        Console.WriteLine($"Round {round}");
        Console.WriteLine();


        while (attackChoice != ConsoleKey.D1 && attackChoice != ConsoleKey.D2 && attackChoice != ConsoleKey.D3 && attackChoice != ConsoleKey.D4 && attackChoice != ConsoleKey.D5)
        {
            Console.WriteLine("Attacks:");
            Console.WriteLine("1. Big Punch");
            Console.WriteLine(" High damage\n   Low accuracy");
            Console.WriteLine();
            Console.WriteLine("2. Regular Punch");
            Console.WriteLine(" Normal damage\n Normal accuracy");
            Console.WriteLine();
            Console.WriteLine("3. Small Punch");
            Console.WriteLine(" Low damage\n    High accuracy");
            Console.WriteLine();
            Console.WriteLine("4. Focus");
            Console.WriteLine(" Increase accuracy");
            Console.WriteLine();
            Console.WriteLine("5. Strength");
            Console.WriteLine(" Increase damage");
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
                    enemy.hp -= (int)Math.Round(player.damage * player.damageModifier, 0);
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
                    enemy.hp -= (int)Math.Round(player.damage * player.damageModifier, 0);
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
                    enemy.hp -= (int)Math.Round(player.damage * player.damageModifier, 0);
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
                player.damageModifier += 0.5;
                Console.WriteLine(player.damageModifier);
            }
        }
        doubleRand = generator.NextDouble();

        if (doubleRand <= 0.2)
        {
            enemy.damage = generator.Next(enemy.minDamage, enemy.maxDamage) + 10;
            Console.WriteLine($"Big punch {doubleRand}");
            doubleRand = generator.NextDouble();
            if (doubleRand < enemy.accuracy * 0.75)
            {
                player.hp -= (int)Math.Round(enemy.damage * enemy.damageModifier, 0);
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
                player.hp -= (int)Math.Round(enemy.damage * enemy.damageModifier, 0);
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
                player.hp -= (int)Math.Round(enemy.damage * enemy.damageModifier, 0);
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
            enemy.damageModifier += 0.5;
            Console.WriteLine(enemy.damageModifier);
        }

        if (player.hp < 0)
        {
            player.hp = 0;
        }

        if (enemy.hp < 0)
        {
            enemy.hp = 0;
        }

        Console.WriteLine($"{player.name} has {player.hp}hp remaining");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine($"{enemy.name} has {enemy.hp}hp remaining");

        Console.WriteLine(doubleRand);
        Console.WriteLine(enemy.damage);
        Console.ReadLine();
        //Thread.Sleep(TimeSpan.FromSeconds(3));
    }


    //Different endings to the round depending on how much hp each fighter has
    //Draw
    if (player.hp <= 0 && enemy.hp <= 0)
    {
        Console.WriteLine("Draw!");
        Console.WriteLine("Both fighters were knocked unconcious");
        Console.ReadLine();
        money += wager;
        win = false;
    }

    //Fighter 2 wins
    else if (player.hp <= 0)
    {
        Console.WriteLine($"{player.name} down");
        Console.WriteLine($"{enemy.name} wins!");
        Console.ReadLine();
        win = false;
    }

    //Fighter 1 wins
    else if (enemy.hp <= 0)
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
    player.damageModifier = 1;
    wager = 0;
    return win;
}