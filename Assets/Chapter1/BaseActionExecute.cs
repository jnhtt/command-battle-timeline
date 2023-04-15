using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter1
{

    public class ActionData
    {
        public GameObject actor;
        public List<GameObject> receiverList;
        public int actionValue;
    }

    public abstract class BaseActionExecute : MonoBehaviour
    {
        public virtual void Play(ActionData data, System.Action onFinish)
        {
        }
    }

}
