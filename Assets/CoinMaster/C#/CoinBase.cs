using System.Numerics;

public class Coin
{
    private static readonly double digit = 1e5;
    private BigInteger resource = new (0);

    public Coin()
    {
        this.resource = new (0);
    }
    public Coin(BigInteger resource)
    {
        this.resource = resource;
    }

    public static implicit operator BigInteger(Coin coin)
        => coin.resource;

    public static Coin operator +(Coin coin, double amount)
        => new(coin.resource + (long)(amount * digit));

    public static Coin operator -(Coin coin, double amount)
        => new(coin.resource - (long)(amount * digit));

    public static Coin operator *(Coin coin, double amount)
        => new(coin.resource * (long)(amount * digit) / (long)digit);

    public static Coin operator /(Coin coin, double amount)
        => new(coin.resource / (long)(amount * digit) * (long)digit);

    public static Coin operator ++(Coin coin)
        => coin + 1;

    public static Coin operator --(Coin coin)
        => coin - 1;

    public static bool operator >=(Coin coin, double amount)
        => (coin - amount).resource >= 0;

    public static bool operator <=(Coin coin, double amount)
        => (coin - amount).resource <= 0;

    public override string ToString()
        => $"{resource / (int)digit}";
}
