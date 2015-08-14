---
layout: default
---

# The Install command

The **install** command can be launched in this way

   Bazooka.exe install
               -a YOURAPPLICATION
			   -v VERSION
			   -d DIRECTORY
			   -c CONFIGURATION
			   -r REPOSITORY
			   -p PARAMETERS

All parameters are mandatory and this is their meaning:

- **-a YOURAPPLICATION** : specifies what package to install, it's the name of the Nuget package that will be installed
- **-v VERSION** : it's the version of the package to install
- **-d DIRECTORY** : it's the directory where the application will be installed
- **-c CONFIGURATION** : it's the configuration, or enviroment, where you are installing the application
- **-r REPOSITORY**: it's the repository whre the package can be found
- **-p PARAMETERS**: are the parameters to pass to the install script if present. These are quotes-enclosed, space separated in the form "key=value"

The client will proceed to download the specified package with the specified version from the repository and then unpack its contents in the installation directory.

After that the client will search for config transforms for the specified enviroment and apply them. For example if you are installing in test and there is a Web.Test.config transform it will be applied to Web.config. Note that this transform must be a valid Xdt transform. 

If a file named Install.ps1 was contained in the package it will be executed with the parameters passed on the command line.