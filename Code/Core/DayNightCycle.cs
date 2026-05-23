using System;

namespace VSurvival.Code.Core;

public sealed class DayNightCycle
{
    public float DayLengthMinutes { get; set; } = 20f;
    public float TimeOfDay01 { get; private set; } = 0.25f;

    public event Action<float>? OnTimeChanged;

    public void Tick(float deltaSeconds)
    {
        var dayLengthSeconds = Math.Max(60f, DayLengthMinutes * 60f);
        TimeOfDay01 += deltaSeconds / dayLengthSeconds;

        if (TimeOfDay01 > 1f)
        {
            TimeOfDay01 -= 1f;
        }

        OnTimeChanged?.Invoke(TimeOfDay01);
    }

    public bool IsNight => TimeOfDay01 >= 0.72f || TimeOfDay01 <= 0.20f;
}
