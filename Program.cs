using System.Runtime.ExceptionServices;


Duelist[] duelists = {
    new Duelist(1.0 / 3, "Aaron"),
    new Duelist(1.0 / 2, "Bob"),
    new Duelist(0.995, "Charlie")
};

for (int i = 0; i < 10000; i++) {
    match(duelists);
}

foreach (Duelist duelist in duelists) {
    Console.WriteLine(duelist.getStats());
}

static void match(Duelist[] duelists) {

    while (true)
    {
        foreach (Duelist duelist in duelists)
        {
            //get greatest
            Duelist greatest = getGreatest(duelists);
            //check if won
            if (greatest.getName() == "unknown" && duelist.isAlive())
            {
                duelist.win();
                return;
            }
            else
            {
                //shoot
                duelist.shootAtTarget(greatest);
            }
        }
    }

}

static Duelist getGreatest(Duelist[] duelists) {
    Duelist greatest = new Duelist();
    foreach (Duelist duelist in duelists)
    {
        if (duelist.getChance() > greatest.getChance() && duelist.isAlive() && duelist.getName()!=greatest.getName())
        {
            greatest = duelist;
        }
    }
    return greatest;
}

class Duelist {
    private string name;
    private double chance;
    private bool alive;
    private int wonAmt;

    public Duelist(double _c = 0, string _s = "unknown") {
        chance = _c;
        alive = true;
        name = _s;
        wonAmt = 0;
    }

    public void shootAtTarget(Duelist target) {
        Random rand = new Random();
        double roll = rand.NextDouble();
        if (roll < chance) {
            target.die();
        }
    }

    public string getStats() {
        return name + " has won " + wonAmt + " games, totaling " + (wonAmt / 10000 * 100) + "% win rate.";
    }

    public void win() { wonAmt++; }

    private void die() { alive = false; }

    public bool isAlive() { return alive; }

    public double getChance() {  return chance; }

    public string getName() { return name; }
}