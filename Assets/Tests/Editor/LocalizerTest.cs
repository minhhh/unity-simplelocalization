using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;
using SimpleLocalization;

public class LocalizerTest
{
    SimpleLocalizer simpleLocalizer;

    [SetUp]
    public void Setup ()
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

        var dict = Json.Deserialize (jsonString) as Dictionary<string,object>;
        simpleLocalizer = new SimpleLocalizer ();
        simpleLocalizer.SetGroup ("system", dict ["system"] as Dictionary<string, object>);
    }

    [Test]
    public void LocalizerReturnSimpleText ()
    {
        Assert.AreEqual (
            "OK", 
            simpleLocalizer.Get (
                "system", 
                "ui_button_common_ok", 
                null, 
                0));
    }

    [Test]
    public void LocalizerReturnNonExistentKey ()
    {
        Assert.AreEqual (
            "__foobar", 
            simpleLocalizer.Get (
                "system", 
                "foobar", 
                null, 
                0));
    }

    [Test]
    public void LocalizerWithParameterArray ()
    {
        Assert.AreEqual (
            "Hello FirstName, LastName", 
            simpleLocalizer.Get (
                "system", 
                "ui_text_hello_1", 
                new string[] {
                    "FirstName",
                    "LastName"
                },
                0));
    }

    [Test]
    public void LocalizerWithParameterDict ()
    {
        Assert.AreEqual (
            "Hello Peter, Bah",
            simpleLocalizer.Get (
                "system",
                "ui_text_hello_2",
                new Dictionary<string, object> { { "first", "Peter" }, { "last", "Bah" } },
                0));
    }
        
    [Test]
    public void LocalizerWithNumberThreshold ()
    {
        Assert.AreEqual (
            "Your number one",
            simpleLocalizer.Get (
                "system",
                "choose_one",
                null,
                1));
        Assert.AreEqual (
            "Your number two",
            simpleLocalizer.Get (
                "system",
                "choose_one",
                null,
                2));
    }

    [Test]
    public void LocalizerWithFormulaThreshold ()
    {
        Assert.AreEqual (
            "Your number one",
            simpleLocalizer.Get (
                "system",
                "choose_one",
                null,
                1));
        Assert.AreEqual (
            "Your number two",
            simpleLocalizer.Get (
                "system",
                "choose_one",
                null,
                2));
        Assert.AreEqual (
            "Greater than or equal 1",
            simpleLocalizer.Get (
                "system",
                "formula1",
                null,
                1));
        Assert.AreEqual (
            "Greater than or equal 1",
            simpleLocalizer.Get (
                "system",
                "formula1",
                null,
                2));
        Assert.AreEqual (
            "Less than 1",
            simpleLocalizer.Get (
                "system",
                "formula1",
                null,
                0));
        Assert.AreEqual (
            "Less than or equal 3",
            simpleLocalizer.Get (
                "system",
                "formula2",
                null,
                3));
        Assert.AreEqual (
            "Greater than 3",
            simpleLocalizer.Get (
                "system",
                "formula2",
                null,
                4));
        Assert.AreEqual (
            "Less than or equal 3",
            simpleLocalizer.Get (
                "system",
                "formula2",
                null,
                2));
        Assert.AreEqual (
            "Less than or equal -1",
            simpleLocalizer.Get (
                "system",
                "formula3",
                null,
                -1));
        Assert.AreEqual (
            "Less than or equal -1",
            simpleLocalizer.Get (
                "system",
                "formula3",
                null,
                -2));
        Assert.AreEqual (
            "Greater than -1",
            simpleLocalizer.Get (
                "system",
                "formula3",
                null,
                0));
    }
}
