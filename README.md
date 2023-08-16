# VBLua.Core - C# .NET Utilities

VBLua.Core is a collection of utility functions and extensions for C# .NET projects. These utilities aim to simplify various tasks, such as type conversion, audio playback, speech synthesis, JSON handling, and more.

## Table of Contents

- [Type Conversion](#type-conversion)
- [Audio and Speech](#audio-and-speech)
- [File Compression](#file-compression)
- [Message Handling](#message-handling)
- [Image Loading](#image-loading)
- [JSON Loaders](#json-loaders)
- [Script Execution](#script-execution)
- [Collections](#collections)
- [Data Manipulation](#data-manipulation)

## Type Conversion

The utilities provide easy ways to convert between different data types.

- `Check()`: Check if an object has a valid type.
- `CheckType(Type type)`: Check if an object has a specific type.
- `ToStr()`: Convert various types to strings.
- `ToInt()`: Convert various types to integers.
- `ToDouble()`: Convert integers and decimals to double.
- `ToDec()`: Convert integers and doubles to decimals.
- `ToBool()`: Convert strings to booleans.

## Audio and Speech

- `Mp3(string audioFile, string path = "")`: Play an MP3 audio file.
- `Say(string txt, int volume)`: Synthesize speech from text.
- `SayAsync(string txt, int volume)`: Asynchronously synthesize speech.
- `LoadAllAsync(Control[] controls)`: Load images into PictureBox controls.

## File Compression

Simplify file compression and decompression tasks.

- `Unzip(string fileName, string path)`: Extract files from a ZIP archive.
- `Zip(string path, string destination)`: Create a ZIP archive from a directory.

## Message Handling

Easily display messages using message boxes.

- `Msg(string txt)`: Show a message box with a given text.
- `Msg(List<string> list, string comment = "")`: Display a list of messages in a message box.

## Image Loading

Load images into PictureBox controls.

- `LoadAllAsync(Control[] controls)`: Load images asynchronously into PictureBox controls.

## JSON Loaders

Utilities to load and serialize JSON data.

- `FromJson( array)`: Deserialize JSON into object arrays or lists.
- `ToJson(text)`: Serialize object arrays to JSON strings.

## Script Execution

Execute Lua scripts with customizable options.

- `Execute(ref Script script, bool useOwnSyntax = false, bool localFile = false)`: Execute Lua scripts.
- `Preload(ref Lua lua)`: Preload Lua engine with common namespaces.
- `LoadImports(ref Lua lua, string[] importList)`: Load custom namespaces into Lua engine.
- `Log(ref Script script, bool localFile = false)`: Execute with log script output(Unstable).

## Collections

Various collection-related utilities.

- `ToText(array)`: Convert object arrays to string arrays.
- `ToStringTable(ScriptData)`: Convert code source to a key-value pair table.
---

## Elements

## Buttons

The provided code demonstrates the creation and usage of different types of buttons, such as standard and round buttons. It showcases how to set up an `onPress` event handler and change the button's label after clicking.

## TextField

This section showcases the creation of a native text field using the `native.newTextField` function. An event listener is added to print the entered text when the user finishes editing.

## ScrollWidget

The code references a `createScrollWidget` function to create a scrollable widget. It adds an event listener to log the current scroll position after the scrolling ends.

## Switch

A switch widget is created using the `widget.newSwitch` function. It demonstrates how to handle the `onPress` event to determine if the switch is turned on or off.

## Slider

The `widget.newSlider` function is used to create a slider widget. An event listener is added to log the slider's value when the interaction ends.

## PickerWheel

A picker wheel widget is created using the `widget.newPickerWheel` function. The provided code contains a commented-out event listener example that captures selected values from the picker wheel.

## ToggleButton

A toggle button is created using the `widget.newSwitch` function with the `"toggle"` style. Similar to the switch widget, it showcases how to handle the `onPress` event for toggling.

## Stepper

The `widget.newStepper` function is used to create a stepper widget. An event listener is mentioned to handle changes in the stepper's value.

## TabBar

The code demonstrates the creation of a tab bar using the `widget.newTabBar` function. It showcases how to set up tab buttons with labels and icons.

## ImageRect

A rectangular image is displayed using the `display.newImageRect` function. The code sets the image's position and dimensions.

## Image

An image is displayed using the `display.newImage` function. The code sets the image's position.

## Custom Widgets

The code includes two custom functions to create custom widgets: `createStar` and `createCircle`. These functions return display objects for a star and a circle, respectively. They showcase how to create custom shapes with image fill and stroke.
