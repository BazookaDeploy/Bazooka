---
layout: default
---

# The Uninstall command

The **uninstall** command can be launched in this way

   Bazooka.exe uninstall
               -a YOURAPPLICATION
			   -v VERSION
			   -d DIRECTORY
			   -c CONFIGURATION
			   -p PARAMETERS

All parameters are mandatory and this is their meaning:

- **-a YOURAPPLICATION** : specifies what package to uninstall, it's the name of the Nuget package that will be uninstalled
- **-v VERSION** : it's the version of the package to uninstall
- **-d DIRECTORY** : it's the directory where the application is installed and where it wiil be removed
- **-c CONFIGURATION** : it's the configuration, or enviroment, where you are uninstalling the application
- **-p PARAMETERS**: are the paramters to pass to the uninstall script if present. These are quotes-enclosed, space separated in the form "key=value"

The client will search in the specified directory for a package with the specified name and version. If a package is not found it will throw an error.

If a package is found, it will search for a Uninstall.ps1 script and, if found, will invoke it passing the specified parameters. After that all files that were contained in the package will be deleted along with the package itself.
