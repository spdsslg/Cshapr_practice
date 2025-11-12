namespace PracticeToLab3;

public class CounterEventArgs : EventArgs
{
    public int Value { get; set; }
    public bool InitValue { get; set; } = false;
}

public class Counter
{
    public event EventHandler<CounterEventArgs>? ThresholdReached;
    public int Threshold{get; set;}
    public int Value{get; set;}
    public int Id{get;}
    private static int _nextId = 0;
    private bool _reached = false;

    public Counter(int threshold, int val)
    {
        Threshold = threshold;
        Value = val;
        Id = _nextId++;
        if (val >= threshold && !_reached)
        {
            _reached = true;
        }
    }

    public void Check()
    {
        if (_reached)
        {
            OnThresholdReached(new CounterEventArgs{Value = this.Value , InitValue = true});
        }
    }

    public void Increment(int step = 1)
    {
        if (Value+step >= Threshold && !_reached)
        {
            Value += step;
            OnThresholdReached(new CounterEventArgs{Value = this.Value});
            _reached = true;
        }
        else if (!_reached)
        {
            Value += step;
        }
    }

    protected virtual void OnThresholdReached(CounterEventArgs e)
    {
        ThresholdReached?.Invoke(this, e);
    }
}