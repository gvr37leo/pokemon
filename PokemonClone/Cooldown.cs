using static Utils;


public class Cooldown {

    public Action onFinish;
    public float cooldownTime = 1;
    public float remaining = 0;

    public Cooldown() {

    }

    public void Update(float dt) {
        var oldremaing = remaining;
        remaining -= dt;
        if(remaining <= 0 && oldremaing > 0) {
            onFinish();
        }
    }

    public void Start() {
        remaining = cooldownTime;
    }

    public bool IsReady() {
        return remaining <= 0;
    }

    public float PercentageComplete() {
        return Clamp01(to(remaining, cooldownTime) / cooldownTime);
    }
}