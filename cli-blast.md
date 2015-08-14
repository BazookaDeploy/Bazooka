---
layout: default
---

# The Blast command


The **blast** command can be launched in this way

   Bazooka.exe blast
			   -d DIRECTORY
			   -p PARAMETERS

All parameters are mandatory and this is their meaning:

- **-d DIRECTORY** : it's the directory where the application will be uninstalled
- **-p PARAMETERS**: are the parameters to pass to the uninstall script if present. These are quotes-enclosed, space separated in the form "key=value"

The blast option is a simpler version of the uninstall options. Given a directory will search for a package and uninstall it. 

If zero or more than one package is found it will throw an error.