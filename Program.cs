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

    while (aliveAmt(duelists)>1)
    {
        //Console.WriteLine("Round Start!");
        foreach (Duelist duelist in duelists)
        {
            if (!duelist.isAlive()) { continue; }
            //Console.WriteLine(duelist.getName()+"'s turn!");
            //get greatest
            Duelist greatest = getGreatest(duelists,duelist);

            //Console.WriteLine("current strongest is "+greatest.getName());

            if (duelist.isAlive())
            {
                //shoot
                duelist.shootAtTarget(greatest);
            }
        }
    }
    getWinner(duelists);
    foreach (Duelist duelist in duelists) { duelist.revive(); }
}

static int aliveAmt(Duelist[] duelists) {
    int amt = 0;
    foreach (Duelist duelist in duelists) {
        if (duelist.isAlive()) { amt++; }
    }
    return amt;
}

static void getWinner(Duelist[] duelists) {
    foreach (Duelist duelist in duelists)
    {
        if (duelist.isAlive()) {
            //Console.WriteLine(duelist.getName()+" wins!\n");
            duelist.win();
            return;
        }
    }
}

static Duelist getGreatest(Duelist[] duelists, Duelist turner) {
    Duelist greatest = new Duelist();
    foreach (Duelist duelist in duelists)
    {
        if (duelist.getChance() > greatest.getChance() && duelist.isAlive() && duelist.getName()!=turner.getName())
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
        //Console.WriteLine(getName() + " shoots at "+target.getName());
        Random rand = new Random();
        double roll = rand.NextDouble();
        if (roll < chance) {
            //Console.WriteLine(getName() + " hits! " + target.getName()+" dies.");
            target.die();
        }
    }

    public string getStats() {
        return name + " has won " + wonAmt + " games, totaling " + (wonAmt / 10000.0 * 100.0) + "% win rate.";
    }

    public void win() { wonAmt++; }

    private void die() { alive = false; }

    public void revive() { alive = true; }

    public bool isAlive() { return alive; }

    public double getChance() {  return chance; }

    public string getName() { return name; }
}