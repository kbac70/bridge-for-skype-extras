# Introduction #

[InACallPlugin](http://bridge-for-skype-extras.googlecode.com/svn/trunk/InACallPlugin/) project can be compiled into two types of plugins:
  1. Plugin A - executable
  1. Plugin B - binary (dll)
The [Plugin factory](http://bridge-for-skype-extras.googlecode.com/svn/trunk/InACallPlugin/PluginFactory.cs) implementation is capable of instantiating both types of plugins.

## Plugin A ##

Plugin A is an executable. You need to ensure that the project is compiled into the console application. The main startup point should be assigned to [PluginProgram class](http://bridge-for-skype-extras.googlecode.com/svn/trunk/InACallPlugin/PluginProgram.cs).

## Plugin B ##
Plugin B is a dynamically linked library. Therefore you need to compile the project into a class library.

### How do I configure the Skype Publishing Studio? ###
![http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.bplugin.jpg](http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.bplugin.jpg)
but this requires skype options
![http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.startpm.jpg](http://gadgets.kbac70.googlepages.com/skype.extras.pubstudio.startpm.jpg)