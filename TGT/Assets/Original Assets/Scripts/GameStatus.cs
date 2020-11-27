using UnityEngine;
using System;
using System.Reflection;

public class GameStatus : MonoBehaviour {
    [SerializeField] private bool somethingHappenedVar;
    public bool somethingHappened {
        get => somethingHappenedVar;
        set => somethingHappenedVar = value;
    }

    [SerializeField] private int timesShotVar;
    public int timesShot {
        get => timesShotVar;
        set => timesShotVar = value;
    }

    public object this[string propertyName] {
        get => this.GetType().GetProperty(propertyName)?.GetValue(this, null);
        set => this.GetType().GetProperty(propertyName)?.SetValue(this, value, null);
    }
}
