# Why
This mod was created because MelonLoader does not have the ability to do this for you. Its quite annoying to need to recompile mods just to add more logging. So this mod allows you to just write every inch of logging you want and not have that logging spam the log when the user doesnt want it to.

# Usage
You will need to add a project reference to this mods dll. Once that is done, there is a few more steps:
- Add `using ComplexLogger;`
- Add `public static ComplexLogger<T> Logger = new();`
    - You dont need to use the `public` keyword.
    - `<T>` requires a class that extends `MelonBase`, which can be `MelonMod` or `MelonPlugin`

Once these things are added, you will be able to use the logger. No longer will you need to use defines for debug messages. No longer will you need to recompile a mod just to enable those define based debug messages.

You can also add color and other text changes using the `params object[]` array. This should be properly handled, but is currently not fully tested.

# Technical Details
This mod makes use of a generic class to handle extending the base MelonLogger. Note that you _cant_ use a class that doesnt extend the `MelonBase` class in some way as the class that you use to instantiate the ComplexLogger instance.

## Logging Sub Type
I also implemented a handy sub type that you can use to print separators and headers. In order to use them, you will need to use something along these examples:
- `Logger.Log(string.Empty, FlaggedLoggingLevel.Debug, null, LoggingSubType.Separator);`
- `Logger.Log("I am a header", FlaggedLoggingLevel.Debug, null, LoggingSubType.IntraSeparator);`
- `Logger.Log("This will print to the Unity console (or uConsole)", FlaggedLoggingLevel.Debug, null, LoggingSubType.uConsole);`

Note: I forgot to write a specific function to handle the sub type without the exception. In a future update, you will not need the `null` argument.

## Levels
- [ ] None
    - **Always Enabled**
- [ ] Trace
    - Use this for logs that you dont really need for most cases
- [ ] Debug
    - This is your bread and butter. You'll likely use only this level
- [ ] Verbose
    - This is for debug information that is only useful when the normal debug info just isnt enough
- [ ] Warning
    - Use this level for warnings that you dont absolutely need to be printed
- [ ] Error
    - **Always Enabled**
- [ ] Critical
    - **Always Enabled**
- [ ] Exception
    - **Always Enabled**

# Examples
- `Logger.Log($"Debug Message {string parsed thingy}", FlaggedLoggingLevel.Debug);`
- `Logger.Log($"ClassName.MethodName()::Something was null, null check already handled this", FlaggedLoggingLevel.Warning);`