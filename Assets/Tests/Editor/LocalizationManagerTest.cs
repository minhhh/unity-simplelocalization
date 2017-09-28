using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;
using SimpleLocalization;

[IntegrationTest.DynamicTest ("IntegrationTest")]
[IntegrationTest.SucceedWithAssertions]
[IntegrationTest.Timeout (5)]
public class LocalizationManagerTest_Basic: MonoBehaviour
{
    public void Awake ()
    {
        var jsonString = "{" +
            "   \"meta\" : {" +
            "       \"version\" : \"1\"," +
            "       \"lang\" : \"en\"" +
            "   }," +
            "   \"system\" :  {" +
            "       \"ui_button_common_ok\" : \"OK\"," +
            "       \"ui_text_hello_1\" : \"Hello {0}, {1}\"," +
            "       \"ui_text_hello_2\" : \"Hello {first}, {last}\"," +
            "       \"choose_one\" : {" +
            "           \"1\" : \"Your number one\"" +
            "           \"2\" : \"Your number two\"" +
            "       }" +
            "       \"formula1\" : {" +
            "           \"1<=\" : \"Greater than or equal 1\"" +
            "           \"1>\" : \"Less than 1\"" +
            "       }" +
            "       \"formula2\" : {" +
            "           \"3>=\" : \"Less than or equal 3\"" +
            "           \"3<\" : \"Greater than 3\"" +
            "       }" +
            "       \"formula3\" : {" +
            "           \"-1>=\" : \"Less than or equal -1\"" +
            "           \"-1<\" : \"Greater than -1\"" +
            "       }" +
            "   }" +
            "}";

        LocalizationManager.CreateInstance ();
        LocalizationManager.Instance.LoadJSON (jsonString);

    }
        
    public void Start ()
    {
        Assert.AreEqual (
            "OK", 
            LocalizationManager.Instance.GetText (
                "system", 
                "ui_button_common_ok", 
                null, 
                0));
        IntegrationTest.Pass ();
    }
        
}
