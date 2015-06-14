# Introduction #

The project comprises of several components:
  * Extras Bridge - XtrsBrdg.dll - written in C++
  * Extras Host - XtrsHost.exe - written in C#
  * Sample Plugin - InACall.dll - written in C#

All components are designed to execute within Microsoft Windows.

The aim of the project is to help with hosting .NET extensions within Skype.
For more details refer to UseCases

# Implementation #
The Extras Bridge dynamically linked library is being registered with Skype as a Type-B plugin. To fully support runtime load/unload feature, it spawns a child process Extras Host. Together they will act as lightweight mediator between Skype and your plugin implementation. The host process inspects all the dll assemblies, from within its own directory, looking for first non-abstract public class implementing [IPluginFactory](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtensionUtils/IPluginFactory.cs). When found, it is going to use it to instantiate your [ISkypePluginB](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtensionUtils/ISkypePluginB.cs) implementation.

## UML Implementation Diagram ##
![http://gadgets.kbac70.googlepages.com/skype.extras.implementation.jpg](http://gadgets.kbac70.googlepages.com/skype.extras.implementation.jpg)

### Intermodule Links ###
  * [ICollectionManager interface](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtrasBridge/ICollectionManager.h)
  * [ISkypePluginB interface](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtrasBridge/ISkypePluginB.h)
  * [Custom Inter-Process Communication Protocol](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtrasBridge/Protocol.h)
  * [IPluginFactory interface](http://bridge-for-skype-extras.googlecode.com/svn/trunk/SkypeExtensionUtils/IPluginFactory.cs)

# More UML Diagrams #
## Static Views ##
  * ExtrasBridgeClassDiagram
  * ExtrasHostClassDiagram

## Dynamic views ##
  * EnsureDllLoadedSequence
  * OpenPluginSeqence
  * SyncWriteSequence
  * ShutdownSequence
