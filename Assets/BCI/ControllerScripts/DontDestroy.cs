using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance = null;
    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //    return;
        //}
        //Destroy(this.gameObject);

        //EKL Edit
        if(instance==null)
        {
            //Set instance
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance!=this)
        {
            //Replace this with the updated one....see if this makes everything worse.
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

    }

}
