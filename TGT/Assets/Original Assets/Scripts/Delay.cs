using System.Collections;
using System.Collections.Generic;

public class Delay
{
    float time;
    float currentTime;
    
    public Delay(float cooldown)
    {
        time = cooldown;
        currentTime = cooldown;
    }

    /// Returns true if delay just passed
    public bool update(float deltaTime)
    {
        if (currentTime <= 0)
        {
            return true;
        }
        currentTime -= deltaTime;
        return false;
    }

    public bool isCountingDown()
    {
        return currentTime > 0;
    }

    public void reset()
    {
        currentTime = time;
    }
}
