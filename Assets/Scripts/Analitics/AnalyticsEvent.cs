using System;
using System.Collections.Generic;
using UnityEngine;

namespace Analitics
{
    public class AnalyticsEvent : MonoBehaviour
    {
        [SerializeField] private List<PropertyUpdate> updateProperties;
        [SerializeField] private bool destroyOnTrigger;

        private void Start()
        {
            if (MyAnalytics.Instance == null)
                Debug.LogError("Cannot access analytics. Analytics Envent:" + gameObject.name);
        }

        public void InvokeEvent()
        {
            if (MyAnalytics.Instance == null)
            {
                Debug.LogError("Cannot access analytics. Analytics Envent:" + gameObject.name);
                return;
            }

            foreach (var property in updateProperties)
            {
                ExecuteProperty(property);
            }

            if (destroyOnTrigger)
                Destroy(gameObject);
        }

        public static void ExecuteProperty(PropertyUpdate propertyUpdate)
        {
            switch (propertyUpdate.Actions)
            {
                case BasicActions.UpdateAdd:
                    int initialVal = MyAnalytics.Instance.GetProperty(propertyUpdate.Key);
                    MyAnalytics.Instance.SetProperty(propertyUpdate.Key, initialVal + propertyUpdate.Val);
                    break;
                case BasicActions.SafeUpdateAdd:
                    int val = 0;
                    if (MyAnalytics.Instance.ContainsProperty(propertyUpdate.Key))
                        val = MyAnalytics.Instance.GetProperty(propertyUpdate.Key);
                    MyAnalytics.Instance.SetProperty(propertyUpdate.Key, val + propertyUpdate.Val);
                    break;
                case BasicActions.UpdateSet:
                    MyAnalytics.Instance.SetProperty(propertyUpdate.Key, propertyUpdate.Val);
                    break;
                case BasicActions.AddRecord:
                    MyAnalytics.Instance.AddRecord(propertyUpdate.Key, propertyUpdate.Val);
                    break;
                default:
                    Debug.LogWarning("Unimplemented type property");
                    break;
            }
        }

        [Serializable]
        public enum BasicActions
        {
            UpdateAdd,
            UpdateSet,
            SafeUpdateAdd,
            AddRecord
        }

        [Serializable]
        public struct PropertyUpdate
        {
            [SerializeField] private string key;
            [SerializeField] private int val;
            [SerializeField] private BasicActions actions;


            public PropertyUpdate(string key, int val, BasicActions actions)
            {
                this.key = key;
                this.val = val;
                this.actions = actions;
            }

            public string Key => key;
            public int Val => val;
            public BasicActions Actions => actions;
        }
    }
}