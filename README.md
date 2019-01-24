# AutoKeg
Gluten free skunkworks

---
Build Requirements
---
Mono version 15.18+

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
