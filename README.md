# AutoKeg
Gluten free skunkworks

---
Runtime Requirements
---
Mono version 15.18+
Follow instructions here https://www.mono-project.com/download/stable/#download-lin-raspbian for install on raspberry pi.
You only need to install `mono-runtime` 

---
Build Requirements
---
Mono version 15.18+
Msbuild version 15.8+

---
Build
---
(from project root)

```
msbuild /t:restore
msbuild
```
  
 ---
 Deploy
 ---
 I've been using rsync to deploy.
 
 `rsync -avzh bin/Debug/net471/ you@urpiaddr:/path/to/folder`
 
 ---
 Test
 ---
 start application 
 
 ```
 cd /path/to/folder
 sudo mono autokeg.exe
 ```
 Its going to print what pin its listening on. In a different terminal connected to your raspberry pi, execute
 ```
 sudo python square.py
 ```
 you should see results.
