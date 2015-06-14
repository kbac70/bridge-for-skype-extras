### Why all this bridge stuff? ###
Well, this is to help .NET guys to develop plugins for Skype. As there is no official way of exporting unmanaged functions from the C# project.


### I am a VB programmer, why would I deal with the bridge? ###

The important stuff about the .NET framework is that, there always has to be a single (master) application domain. Skype so far, does not host any of the .NET framework components. Therefore if you load your .NET plugin into the SkypePM process' space, it will remain there until the SkypePM restarts. The bridge infrastructure spawns the [ExtrasHost process](ArchitectureOverview.md), which acts as the host not only for the plugin but also for the .NET framework. As you can see now, the bridge can easily unload both host and .NET plugin, by simply asking the host to terminate.


### What happens if the Skype user who installed .NET plugin does not have .NET installed? ###
The bridge is going to check `%WINDOWS%\Microsoft.NET\Framework` for valid installation. If such does not exist it will prompt the user and ask her to install the framework from the specified URL. Check [DotNetCheck class](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtrasBridge/DotNetCheck.cpp) for more details.


### How is my plugin going to be loaded? ###
Here are a few things to remember:
  1. You need to implement IPluginFactory interface
  1. The class implementing the interface has to be public with default c-tor
  1. The class has to be part of the class library
  1. The class library needs to be placed in the same directory as the bridge and the host
This way the host will be able to inspect the dlls and instantiate the plugin library class to be able then to obtain valid instance of the plugin.

![http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.dir.jpg](http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.dir.jpg)


### What about my plugin dependencies? ###
Do not worry the main dependencies are preloaded for you. This includes:
  * Skype Interop wrappers
  * Skype extension utils
If you require other dependencies you need to load them yourself.



### How do I add my menu items in PluginB? ###
PluginB is a dll which is hosted by the Skype Plugin Manager. If you want to contribute your entry points (like menu items etc.), you can simply instantiate Skype via its COM interfaces, within implementation of the [Open](http://code.google.com/p/bridge-for-skype-extras/source/browse/trunk/SkypeExtensionUtils/ISkypePluginB.cs) method.

### What is the Extras.Utils.dll? ###
It is a library I have committed to hold some global utility classes and interface definitions. This help with dependency management as the [Architecture](ArchitectureOverview.md) points out.


### The .NET plugin does not expose all interfaces? ###
Yep, there is this limitation at the moment. For example [IHostUIServices](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtensionUtils/IHostUIServices.cs) is not being managed and exposed. If you really need it - **contribute!**


### My plugin is being terminated on shutdown... ###
It might happen when your plugin takes very long to get uninitialized. The bridge will wait for up to three seconds for the plugin to finish. When after this period of time the plugin is not terminated the host will get killed forcefully. Check SyncWriteSequence for more details.


### Can my plugin use other languages? ###
Check build events of the [InTheCall](http://bridge-for-skype-extras.googlecode.com/svn/trunk/InACall/) module. It uses satellites for this very reason. It uses the utilities to synchronize thread culture with the language used by the Skype client.
![http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.satellite.jpg](http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.satellite.jpg)
```
md "$(TargetDir)\pl"
"C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\resgen.exe" "$(ProjectDir)\Plugin\Strings.pl.resx" "$(TargetDir)\pl\Strings.pl.resources"
"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\al.exe" /t:lib /embed:"$(TargetDir)\pl\Strings.pl.resources" /culture:pl /out:"$(ProjectDir)\bin\$(ConfigurationName)\pl\$(TargetName).resources.dll" /template:"$(TargetPath)"
del "$(TargetDir)\pl\Strings.pl.resources"
```


### Why the bridge does not register its COM components? ###
Very good question. The bridge COM objects are coded to the specification as provided by Skype. The idea is that SkypePM needs a solid mechanism for life time management for created objects - COM offers reference counting. And requires a standard way to query for an object - GUID. Because Skype PM creates COM plugins by calling DllInitSkypePluginB instead of CoCreateInstance, this means that the objects do not have to be registered to remain available to Skype.