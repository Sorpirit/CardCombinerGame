using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Analitics
{
    public class MyAnalytics : MonoBehaviour
    {
        private const string analiticsPath = @"/baseAnalitics.dat";
        private const bool loadOnAweke = true;
        private const bool saveOnDestroy = true;

        private AnaliticsData _data;
        
        public static MyAnalytics Instance { get; private set; }
        

        private void Awake()
        {
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                if (loadOnAweke)
                {
                    Load();
                    ChekForUpdates();
                }
            } 
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        public void Save()
        {
            Debug.Log(LogAnalytics());
            _data.Properties.Remove("Fail"); 
            
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + analiticsPath;
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            
            formatter.Serialize(stream,_data);
            stream.Close();
        }

        public void Load()
        {
            string path = Application.persistentDataPath + analiticsPath;
            if (!File.Exists(path))
            {
                Debug.LogWarning("Cannot read analytic");
                _data = new AnaliticsData();
                _data.Clear();
                _data.Properties.Add(AnalyticsConstants.VERSION,AnalyticsConstants.CURRENT_VERSION); 
                _data.Properties.Add("Fail",1); 
                return;
            }
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            _data = formatter.Deserialize(stream) as AnaliticsData ?? new AnaliticsData();
            stream.Close();
        }

        public void ResetData()
        {
            _data.Clear();
            _data.Properties.Add(AnalyticsConstants.VERSION,AnalyticsConstants.CURRENT_VERSION);
        }
        
        public void ChekForUpdates()
        {
            
            if (!_data.Properties.ContainsKey(AnalyticsConstants.VERSION))
            {
                ResetData();
                return;
            }

            //Update
            if (_data.Properties[AnalyticsConstants.VERSION] != AnalyticsConstants.CURRENT_VERSION)
            {
                Debug.LogWarning("New version of analytics detected. Deleting previous versions...");
                ResetData();
            }
        }

        public bool ContainsProperty(string key) => _data.Properties.ContainsKey(key);

        public void SetProperty(string key, int value)
        {
            if (!_data.Properties.ContainsKey(key))
                _data.Properties.Add(key, value);
            else
                _data.Properties[key] = value;
        }
        
        public int GetProperty(string key) => _data.Properties[key];

        public int SafeGetProperty(string key,int defaultValue = 0)
        {
            if (!_data.Properties.ContainsKey(key))
            {
                _data.Properties.Add(key,defaultValue);
                return defaultValue;
            }

            return GetProperty(key);
        }

        public void RemovePropriety(string key) => _data.Properties.Remove(key);

        public void AddRecord(string key,int value)
        {
            if(!_data.Records.ContainsKey(key))
                _data.Records.Add(key,new List<int>());
            
            _data.Records[key].Add(value);
        }

        public void RemoveRecord(string key,int index)
        {
            if(!_data.Records.ContainsKey(key) || _data.Records[key].Count <= index)
                return;
            
            _data.Records[key].RemoveAt(index);
        }
        
        public string LogAnalytics()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Analytics log result:\n\n");
            builder.Append("Key,Value\n");
            foreach (var property in _data.Properties)
            {
                builder.Append($"{property.Key},{property.Value}\n");
            }
            builder.Append("Key,Records\n");
            foreach (var property in _data.Records)
            {
                builder.Append($"{property.Key},{string.Join(",",property.Value)}\n\n");
            }
            return builder.ToString();
        }
        
    }
}