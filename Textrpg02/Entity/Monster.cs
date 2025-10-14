
namespace TextRPG.Entity
{
    internal class Monster
    {
       
    public int Id { get; set; }
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
    public int DropExp { get; set; }
    public int DropGold { get; set; }


    public Monster(int id, string name, int hp, int atk, int def, int spd, int exp, int gold)
    {
        Id = id;
        Name = name;
        Hp = hp;
        Atk = atk;
        Def = def;
        Spd = spd;
        DropExp = exp;
        DropGold = gold;
    }

    public Monster Clone()
        => new Monster(Id, Name, Hp, Atk, Def, Spd, DropExp, DropGold);

        //매번 새로운 몬스터를 뽑아야 하기 때문에 클론 생성해서 뽑는것으로 처리


    }

}
