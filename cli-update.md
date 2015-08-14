---
layout: default
---

# The update command

The **update** command can be launched in this way

   Bazooka.exe update
               -a YOURAPPLICATION
			   -v VERSION
			   -d DIRECTORY
			   -c CONFIGURATION
			   -r REPOSITORY
			   -p PARAMETERS

All parameters are mandatory and this is their meaning:

- **-a YOURAPPLICATION** : specifies what package to update, it's the name of the Nuget package that will be updated
- **-v VERSION** : it's the version of the package to update
- **-d DIRECTORY** : it's the directory where the application will be updated
- **-c CONFIGURATION** : it's the configuration, or enviroment, where you are updating the application
- **-r REPOSITORY**: it's the repository whre the package can be found
- **-p PARAMETERS**: are the parameters to pass to the install and uninstall scripts if present. These are quotes-enclosed, space separated in the form "key=value"

The update option will, simply put, perform an **uninstall** of the currently installed package followed by an **install** of the new version.