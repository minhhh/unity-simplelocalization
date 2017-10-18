# SimpleLocalization

> Straight-forward localization utility

SimpleLocalization is a localization library that support conditional branch selection of localized texts. It is oriented towards developers, using simple JSON format.

## Usage

The main entry point is the `LocalizationManager` singleton. First, you create the singleton at application startup.

```
LocalizationManager.CreateInstance ();
```

Then, you feed `LocalizationManager` with a JSON string which has specific localized texts.

After having the localized data, you can extract localized texts by key like so:

```
LocalizationManager.Instance.GetText (
    "system", // group name
    "ui_button_common_ok", // key
    null, // parameter
    0 // threshold
);
```

## JSON Data Structure

Our input localized text JSON has the following structure

```
{
   "meta" : {
       "version" : "1",
       "lang" : "en"
   },
   "system" :  {
       "ui_button_common_ok" : "OK",
       "ui_text_hello_1" : "Hello {0}, {1}",
       "ui_text_hello_2" : "Hello {first}, {last}",
       "choose_one" : {
           "1" : "Your number one"
           "2" : "Your number two"
       }
       "formula1" : {
           "1<=" : "Greater than or equal 1"
           "1>" : "Less than 1"
       }
       "formula2" : {
           "3>=" : "Less than or equal 3"
           "3<" : "Greater than 3"
       }
       "formula3" : {
           "-1>=" : "Less than or equal -1"
           "-1<" : "Greater than -1"
       }
   }
}
```

The first key of the JSON is `meta`. This specifies the version and language, in this case English or `en`.

The next key is the "group name". We partition localized keys in to groups so that we can overwrite or add groups without messing up other groups. By convention, the main group which contains most of the texts is called `system`

Inside group, we have key-value pairs. There are various types of pair

**Plain**: This type of pair is straightforward. You pass in the key, you get a text back. For example, `"ui_button_common_ok" : "OK"`

**Array parameter**: You pass an array as parameter. The array value will replace `{0}`, `{1}` and so on in the text. For example, `"ui_text_hello_1" : "Hello {0}, {1}"`

```
LocalizationManager.Instance.GetText (
        "system",
        "ui_text_hello_1",
        new string[] {
            "FirstName",
            "LastName"
        },
        0);
// Expect: Hello FirstName, LastName
```

**Dictionary parameter**: You pass a dictionary as parameter and the corresponding key in the text will be replaced by the dictionary's values. For example, `"ui_text_hello_2" : "Hello {first}, {last}"`

```
LocalizationManager.Instance.GetText (
        "system",
        "ui_text_hello_2",
        new Dictionary<string, object> { { "first", "Peter" }, { "last", "Bah" } },
        0);
// Expect: Hello Peter, Bah
```

**Substitute threshold parameter**: //TODO

**Formula threshold parameter**: //TODO


## Install
To include SimpleLocalization into your project, you can use `npm` method of unity package management described [here](https://github.com/minhhh/UBootstrap).

## Changelog

**0.0.2**

* Fix incorrect directory

**0.0.1**

* Initial commit

<br/>

