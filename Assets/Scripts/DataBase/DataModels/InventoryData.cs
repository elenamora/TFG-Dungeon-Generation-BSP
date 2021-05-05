using System;

[Serializable]
public class InventoryData
{
    public int coins;
    public int healthPotions;
    public int energyPotions;
    public int keys;
    public int gems;

    public InventoryData(int coins, int health, int energy, int key, int gem)
    {
        this.coins = coins;
        healthPotions = health;
        energyPotions = energy;
        keys = key;
        gems = gem;
    }
}
