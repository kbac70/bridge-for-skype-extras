# bridge-for-skype-extras
Automatically exported from [code.google.com/p/bridge-for-skype-extras](https://code.google.com/p/bridge-for-skype-extras).

Helps with hosting .NET extensions within Skype

#Host your .NET plugins within Skype with ease!

##What does this project do?
The [Skype](http://www.skype.com) Extras Bridge is aiming to help .NET developers when deploying and hosting their binary plugins. So when:
  * you cannot export functions from your DLLs 
  * struggling with unloading .NET plugins from Skype's process space
  * want your .NET plugin to be hosted by the Skype Plugin Manager
... then this project is for you.

##How does it work?
The Extras Bridge dynamically linked library is being registered with Skype as a Type-B plugin. To fully support runtime load/unload feature, it spawns a child process Extras Host. Together they will act as lightweight mediator between Skype and your plugin implementation. The host process inspects all the dll assemblies, from within its own directory, looking for first non-abstract public class implementing [http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtensionUtils/IPluginFactory.cs IPluginFactory]. When found, it is going to use it to instantiate your [http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtensionUtils/ISkypePluginB.cs ISkypePluginB] implementation.

![](http://gadgets.kbac70.googlepages.com/skype.extras.implementation.jpg)

##Enjoy!
[Krzysztof Bącalski](http://kbac70.blogspot.com)
