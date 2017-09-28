using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using USingleton;

namespace SimpleLocalization
{
    public class LocalizationManager : GameSingleton <LocalizationManager>
    {
        protected SimpleLocalizer _simpleLocalizer;

        protected virtual void Awake ()
        {
            _simpleLocalizer = new SimpleLocalizer ();
        }

        public void LoadJSON (string text)
        {
            Dictionary <string, object> data = MiniJSON.Json.Deserialize (text) as Dictionary <string, object>;

            foreach (var kv in data) {
                if (kv.Key != "meta" && kv.Value is Dictionary <string, object>) {
                    _simpleLocalizer.SetGroup (kv.Key, kv.Value as Dictionary <string, object>);
                }
            }
        }

        public string GetText (string groupKey, string key, object parameters = null, int threshold = 0)
        {
            return _simpleLocalizer.Get (groupKey, key, parameters, threshold);
        }

        public void Load (string path)
        {
            var text = (Resources.Load (path) as TextAsset).text;
            LoadJSON (text);
        }
    }
}