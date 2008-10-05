@echo --- cleaning published directory...
@del .\published\*.* /q /s
@echo --- copying images and end user licence...
@xcopy .\publish\*.* .\published\*.* /e /v /y 
@echo --- copying plugin dlls...
@xcopy .\release\*.dll .\published\*.dll /e /v /y 
@echo --- copying xtras host...
@xcopy .\..\..\SkypeExtrasHost\bin\Release\*.exe .\published\*.exe /e /v /y 
@echo --- copying xtras bridge...
@xcopy .\..\..\SkypeExtrasBridge\bin\Release\*.dll .\published\*.dll /e /v /y 
@echo --- synchronizing file timestamps...
@touchnow .\Published
@echo --- done!
@pause