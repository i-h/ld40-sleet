using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDisplay : MonoBehaviour {
    public static DialogDisplay Instance;
    void Awake()
    {
        Instance = this;
    }
    public Dialog DisplayDialog(Dialog d)
    {
        Dialog dInst = Instantiate<Dialog>(d);
        dInst.transform.parent = transform;
        
        dInst.Show();
        return dInst;
    }
}
