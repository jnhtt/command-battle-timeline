using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter1
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshPro damageText;
        [SerializeField] private float lifeTime;
        [SerializeField] private float speed;

        private float timer;

        public void SetDamage(int damage)
        {
            string mes = damage == 0 ? "Miss" : damage.ToString();

            damageText.SetText(damage.ToString());
        }

        private void Update()
        {
            if (lifeTime < timer)
            {
                GameObject.Destroy(gameObject);
                return;
            }
            float deltaTime = Time.deltaTime;
            timer += deltaTime;
            transform.position += deltaTime * speed * Vector3.up;
        }
    }
}
