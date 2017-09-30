using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Temporary Status Effect", menuName = "Stats/Temporary Status Effect")]
public class TemporaryStatEffect : StatEffect
{
    [SerializeField]
    protected float time;
    [SerializeField]
    protected float valueReduction;

    public float Time
    {
        get { return time; }
        set { time = value; }
    }

    public float ValueReduction
    {
        get { return valueReduction; }
        set { valueReduction = value; }
    }

    public override void Update()
    {
        if (time <= 0 || Mathf.Abs(Value) < 0.001f)
        {
            Remove = true;
            return;
        }

        time -= UnityEngine.Time.deltaTime;
        Value -= Mathf.Sign(Value) * valueReduction * UnityEngine.Time.deltaTime;
    }
}