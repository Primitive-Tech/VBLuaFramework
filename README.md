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

The utilities offer audio playback and speech synthesis capabilities.

- `Mp3(string audioFile, string path = "")`: Play an MP3 audio file.
- `Say(string txt, int volume = 100)`: Synthesize speech from text.
- `SayAsync(string txt, int volume = 100)`: Asynchronously synthesize speech.
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

- `FromJson(string array)`: Deserialize JSON into object arrays or lists.
- `ToJson(object[] array)`: Serialize object arrays to JSON strings.

## Script Execution

Execute Lua scripts with customizable options.

- `Execute(ref Script script, bool useOwnSyntax = false, bool localFile = false)`: Execute Lua scripts.
- `Preload(ref Lua lua)`: Preload Lua engine with common namespaces.
- `LoadImports(ref Lua lua, string[] importList)`: Load custom namespaces into Lua engine.
- `Log(ref Script script, bool localFile = false)`: Execute and log script output.

## Collections

Various collection-related utilities.

- `ToText(object[] array)`: Convert object arrays to string arrays.
- `ToDataframe(Script Data)`: Convert code source to a data frame.
- `ToStringTable(Script Data, char chunkBorder = '^')`: Convert code source to a key-value pair table.

## Data Manipulation

Utilities for manipulating data.

- `ToDataframe(Script Data)`: Convert code source to a data frame.
- `ToStringTable(Script Data, char chunkBorder = '^')`: Convert code source to a key-value pair table.
- `Msg(string title, string txt)`: Show a message box with a title and text.

---

These utilities provided by VBLua.Core aim to simplify common tasks in C# .NET projects. Feel free to explore and use these utilities to enhance your project development.
