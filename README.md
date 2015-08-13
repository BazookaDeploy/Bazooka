# Bazooka

[![Build status](https://ci.appveyor.com/api/projects/status/qkf670rlxsqrvvcw?svg=true)](https://ci.appveyor.com/project/Bjornej/bazooka)

Bazooka was born out of the necessity to deploy a lot of .net applications between different enviroments in a safe, reliable and repeatable way.


The main idea behind the project was to create a system that was able to package a compiled application, all the config transforms needed for the various enviroments it's going to be deployed in, installation scripts and tie it to a changeset in your source control.

As it turns out it's easy to do this by creating an automatic build that creates nuget packages containing everything needed and hosting them in a private nuget gallery.

Once the package is ready you can install your application by using Bazooka to download the package, extract it, apply config transforms based on the current enviroment and execute a optional powershell install script.

For further documentation go to http://bazookadeploy.github.io/Bazooka/



