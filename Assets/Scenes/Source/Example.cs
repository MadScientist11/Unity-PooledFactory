using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PooledFactory
{
    public class Example : MonoBehaviour
    {
        private FruitFactory _fruitFactory;
        public AllFruits _allFruits;

        private void Start()
        {
            _fruitFactory = new FruitFactory(_allFruits);
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                FruitType fruitType = EnumExtensions<FruitType>.Random;
                Vector2 insideUnitCircle = Random.insideUnitCircle.normalized * .25f;
                Fruit fruit = _fruitFactory.GetOrCreate(fruitType, transform,
                    transform.position + new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y));
                SendFruitFlying(fruit);
                Fruit fruit2 = _fruitFactory.GetOrCreate(fruitType, transform,
                    transform.position + new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y));
                SendFruitFlying(fruit2);
                Fruit fruit3 = _fruitFactory.GetOrCreate(fruitType, transform,
                    transform.position + new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y));
                SendFruitFlying(fruit3);
                yield return new WaitForSeconds(.1f);
            }
        }

        private void SendFruitFlying(Fruit fruit)
        {
            Vector2 insideUnitCircle = Random.insideUnitCircle.normalized * .25f;
            Vector3 normalized = new Vector3(insideUnitCircle.x, 1, insideUnitCircle.y).normalized;
            fruit.GetComponent<Rigidbody>()
                .AddForce(normalized * 70 * (Random.value * 0.5f + 0.5f), ForceMode.VelocityChange);
        }
    }
}