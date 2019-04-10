AutoKeg
=======
Gluten free skunkworks

Runtime Requirements
--------------------
Rapsberry Pi with Armv7+

.NET Core 2.2 for Linux Arm

## Install Runtime
From the pi

* `sudo apt-get install curl libunwind8 gettext`
* `curl -sSL -o dotnet.tar.gz https://__url_to_latest_armhf_runtime__`

You can find the latest runtime link for .net core 2.2 here: https://github.com/dotnet/core-setup under Daily Builds Release/2.2.x. You'll need the tar archive for Linux(armhf).

* `sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet`
* `sudo ln -s /opt/dotnet/dotnet /usr/local/bin`

Test the installation by typing `dotnet --help`

Build Requirements
------------------
.NET Core SDK 2.2+

Build
-----
(from project root)

```
dotnet publish -c Release -r linux-arm --self-contained false
```

Deploy
-----
From project root:

`rsync -avzh AutoKeg.ISR.Service/bin/Release/netcoreapp2.2/linux-arm/publish/ you@urpiaddr:/path/to/folder`

Test
----
start application 

```
cd /path/to/folder
sudo ./autokeg
```
Its going to print what pin its listening on. In a different terminal connected to your raspberry pi, execute
```
sudo python square.py
```
you should see results.
