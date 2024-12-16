using System.Numerics;

public class Coin
{
    private static readonly long _digit = (long)1e5;
    private BigInteger _resource = new (0);

    public Coin()
    {
        this._resource = new (0);
    }
    public Coin(BigInteger resource)
    {
        this._resource = resource;
    }

    public Coin(long resource)
    {
        this._resource = new BigInteger(resource) * (long)_digit;
    }

    public Coin(double resource)
    {
        this._resource = new BigInteger(resource * (long)_digit);
    }

    public void Set(BigInteger value)
        => _resource = value * _digit;

    public long ToLong()
        => (_resource / _digit) >= long.MaxValue ? long.MaxValue : (long)(_resource / _digit);

    public static implicit operator BigInteger(Coin coin)
        => coin._resource;

    public static Coin operator +(Coin coin, double amount)
        => new(coin._resource + (long)(amount * _digit));

    public static Coin operator -(Coin coin, double amount)
        => new(coin._resource - (long)(amount * _digit));

    public static Coin operator *(Coin coin, double amount)
        => new(coin._resource * (long)(amount * _digit) / _digit);

    public static Coin operator /(Coin coin, double amount)
        => new(coin._resource / (long)(amount * _digit) * _digit);


    public static Coin operator +(Coin coin, BigInteger amount)
        => new(coin._resource + amount * _digit);

    public static Coin operator -(Coin coin, BigInteger amount)
        => new(coin._resource - amount * _digit);

    public static Coin operator *(Coin coin, BigInteger amount)
        => new(coin._resource * amount * _digit);

    public static Coin operator /(Coin coin, BigInteger amount)
        => new(coin._resource / amount * _digit);

    public static Coin operator +(Coin coin, long amount)
        => new(coin._resource + amount * _digit);

    public static Coin operator -(Coin coin, long amount)
        => new(coin._resource - amount * _digit);

    public static Coin operator *(Coin coin, long amount)
        => new(coin._resource * amount * _digit);

    public static Coin operator /(Coin coin, long amount)
        => new(coin._resource / amount * _digit);

    public static Coin operator ++(Coin coin)
        => coin + 1f;

    public static Coin operator --(Coin coin)
        => coin - 1f;

    public static bool operator >=(Coin coin, double amount)
        => (coin - amount)._resource >= 0;

    public static bool operator <=(Coin coin, double amount)
        => (coin - amount)._resource <= 0;
    
    public static bool operator >(Coin coin, double amount)
        => (coin - amount)._resource > 0;
    public static bool operator <(Coin coin, double amount)
        => (coin - amount)._resource < 0;

    public override string ToString()
        => (_resource / (int)_digit).ToString("#,0");
}
