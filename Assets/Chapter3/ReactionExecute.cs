using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Chapter3
{
    public class ReactionExecute : BaseActionExecute
    {
        [SerializeField] private PlayableDirector director;
        [SerializeField] private Transform startDamage;
        [SerializeField] private Transform endDamage;
        [SerializeField] private Transform hitEffectTransform;
        [SerializeField] private TMPro.TextMeshPro damageText;

        private ActionData actionData;
        private Coroutine coroutine;

        public override void Play(ActionData data, System.Action onFinish)
        {
            actionData = data;

            var animatorBinding = director.playableAsset.outputs.First(c => c.streamName == "AnimationTrack");
            director.SetGenericBinding(animatorBinding.sourceObject, data.actor.GetComponent<Animator>());

            startDamage.position = actionData.actor.transform.position + 1.2f * Vector3.up;
            endDamage.position = actionData.actor.transform.position + 2f * Vector3.up;

            hitEffectTransform.position = actionData.actor.transform.position + 0.7f * Vector3.up;

            damageText.SetText(data.actionValue > 0 ? data.actionValue.ToString() : "MISS");

            coroutine = StartCoroutine(PlayCoroutine(onFinish));
        }

        private IEnumerator PlayCoroutine(System.Action onFinish)
        {
            director.Play();
            yield return new WaitForSeconds((float)director.duration);
            onFinish?.Invoke();
        }
    }
}
