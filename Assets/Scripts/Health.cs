using System;

public class Health 
{
    public float maxValue = 100;
    public float Value { get; private set; }

    public Action OnDead;
    public Action<float> OnChange;

    public Health(float value)
    {
        Value = value;
        OnDead += () => { };
        OnChange += (val) => { };
    }

    public Health()
    {
        Value = maxValue;
        OnDead += () => { };
        OnChange += (val) => { };
    }

    public void Dead()
    {
        Value = 0;
        OnChange(Value);
        OnDead();
    }

    public void TakeDamage(float damage = 15)
    {
        Value -= damage;
        OnChange(Value);

        if (Value <= 0)
        {
            Value = 0;
            OnDead();
        }
    }

    public void Increase(float value = 50)
    {
        Value += value;
        OnChange(Value);

        if (Value > maxValue)
            Value = maxValue;
    }
}
