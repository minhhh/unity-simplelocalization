using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

namespace SimpleLocalization
{
    public class SimpleLocalizer
    {
        static protected Regex conditionRegex = new Regex ("^(-?\\d+)([<>]=?)$");
        protected Dictionary<string, Dictionary<string, object>> _dict;

        public SimpleLocalizer ()
        {
            _dict = new Dictionary<string, Dictionary<string, object>> ();
        }

        public void SetGroup (string groupKey, Dictionary<string, object> groupData)
        {
            _dict [groupKey] = groupData;
        }

        public string Get (string groupKey, string key, object parameters, int threshold)
        {
            try {
                var sentence = _SearchKey (groupKey, key);
                if (sentence is Dictionary<string, object>) {
                    sentence = _Condition (sentence as Dictionary<string, object>, threshold);
                } else if (!(sentence is string)) {
                    throw new ArgumentException ();
                }

                return _Replace (sentence as string, parameters);
            } catch (Exception e) {
                Debug.LogWarningFormat ("{0}::{1} {2} {3} {4}", this.GetType ().FullName, "Get", groupKey, key, e.StackTrace);
            }
            return "__" + key;
        }


        protected string _Replace (string sentence, object parameters)
        {
            if (parameters == null) {
                return sentence;
            }
            if (parameters is Dictionary <string, object>) {
                var parameterDic = parameters as Dictionary <string, object>;
                foreach (var kv in parameterDic) {
                    Regex rgx = new Regex ("(\\{" + kv.Key + "\\})");
                    sentence = rgx.Replace (sentence, kv.Value.ToString ());
                }
            } else if (typeof(IEnumerable).IsAssignableFrom (parameters.GetType ()) && !(parameters is string)) {
                var parameterArr = parameters as IEnumerable;
                int index = 0;
                foreach (var parameter in parameterArr) {
                    Regex rgx = new Regex ("(\\{" + index + "\\})");
                    sentence = rgx.Replace (sentence, parameter.ToString ());
                    index++;
                }
            }
            return sentence;
        }

        protected string _Condition (Dictionary<string, object> branch, int threshold)
        {
            foreach (var kv in branch) {
                int fig;
                if (int.TryParse (kv.Key, out fig)) {
                    if (fig == threshold) {
                        return kv.Value.ToString ();
                    }
                } else {
                    var match = conditionRegex.Match (kv.Key);
                    if (match != null) {
                        var lhs = int.Parse (match.Groups [1].ToString ());
                        string op = match.Groups [2].ToString ();

                        switch (op) {
                        case "<":
                            if (lhs < threshold) {
                                return kv.Value.ToString ();
                            }
                            break;
                        case ">":
                            if (lhs > threshold) {
                                return kv.Value.ToString ();
                            }
                            break;
                        case "<=":
                            if (lhs <= threshold) {
                                return kv.Value.ToString ();
                            }
                            break;
                        case ">=":
                            if (lhs >= threshold) {
                                return kv.Value.ToString ();
                            }
                            break;
                        }
                    }
                }
            }
            return "";
        }

        protected object _SearchKey (string groupKey, string key)
        {
            return _dict [groupKey] [key];
        }
    }

}