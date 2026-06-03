using UnityEngine;

public abstract class Command
{
    #region Fields
    private float timeRegistered;



    #endregion

    #region Properties
    public float TimeRegistered { get => timeRegistered; set => timeRegistered = value; }


    #endregion


    public abstract void Do();

    public abstract void Undo();

}
