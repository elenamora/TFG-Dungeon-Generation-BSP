using System;

[Serializable]
public class InventoryData
{
    public int coin;
    public int healthPotion;
    public int energyPotion;
    public int key;
    public int gem;

    public InventoryData(int coins, int health, int energy, int keys, int gems)
    {
        coin = coins;
        healthPotion = health;
        energyPotion = energy;
        key = keys;
        gem = gems;
    }
}
