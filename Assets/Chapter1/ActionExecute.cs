using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Chapter1
{
    public class ActionExecute : BaseActionExecute
    {
        [SerializeField] private PlayableDirector director;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        private ActionData actionData;
        private Coroutine coroutine;

        public override void Play(ActionData data, System.Action onFinish)
        {
            actionData = data;

            var animatorBinding = director.playableAsset.outputs.First(c => c.streamName == "AnimationTrack");
            director.SetGenericBinding(animatorBinding.sourceObject, data.actor.GetComponent<Animator>());

            var moveBinding = director.playableAsset.outputs.First(c => c.streamName == "MoveTrack");
            director.SetGenericBinding(moveBinding.sourceObject, data.actor.transform);

            var dir = actionData.actor.transform.position - actionData.receiverList[0].transform.position;
            start.position = actionData.actor.transform.position;
            end.position = actionData.receiverList[0].transform.position + 0.7f * dir.normalized;

            coroutine = StartCoroutine(PlayCoroutine(onFinish));
        }

        private IEnumerator PlayCoroutine(System.Action onFinish)
        {
            director.Play();
            yield return new WaitForSeconds((float)director.duration);
            onFinish?.Invoke();
        }

        public void ExecuteDamage()
        {
            foreach (var r in actionData.receiverList)
            {
                var damageValue = CaluculateDamage();
                if (damageValue > 0)
                {

                    var a = r.GetComponent<Animator>();
                    a.SetTrigger("Damage");

                    var prefab = Resources.Load<GameObject>("EffectHit");
                    var eff = GameObject.Instantiate(prefab);
                    eff.transform.position = r.transform.position + 0.5f * Vector3.up;
                }

                var damagePrefab = Resources.Load<GameObject>("DamageText");
                var damage = GameObject.Instantiate(damagePrefab);
                damage.transform.position = r.transform.position + 1f * Vector3.up;
                var txt = damage.GetComponent<DamageText>();
                txt?.SetDamage(damageValue);
            }
        }

        private int CaluculateDamage()
        {
            return UnityEngine.Random.Range(0, 100);
        }
    }
}
