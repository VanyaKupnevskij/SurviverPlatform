using System;

public class Energy 
{
    public float maxValue = 100;
    public float Value { get; private set; }

    public Action<float> OnChange;
    public Action OnAccamulateUlta;

    public Energy(float value)
    {
        Value = value;
        OnChange += (val) => { };
        OnAccamulateUlta += () => { };
    }
    
    public Energy()
    {
        Value = maxValue;
        OnChange += (val) => { };
        OnAccamulateUlta += () => { };
    }

    public void Increase(float value = 10)
    {
        Value += value;
        OnChange(Value);

        if (Value >= maxValue)
        {
            Value = maxValue;
            OnAccamulateUlta();
        }
    }

    public void Decrease(float value = 10)
    {
        Value -= value;
        OnChange(Value);

        if (Value < 0)
            Value = 0;
    }
}
