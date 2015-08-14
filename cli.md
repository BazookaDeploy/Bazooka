---
layout: default
---

# The Command Line Client

Once you have created your packages you are ready to deploy them. One way to do this is to use the command line client.

NOTE: While the command line client works and can deploy application it is not the recommended way to deploy applications as it has some disadvantages:

- you have to launch it on the server where the application will run, especially if you are launching a windows service or are using installation and uninstallation scripts
- is uses the computer TEMP folder to cache and extract your packages so repeated usage will consume hard disk space
- depending on your installation and uninstallation scripts you may have to run it as administrator which is normally not suggested

Having said this it's fine to use it to test how Bazooka works but the suggested option is to use the web application.

The command line client takes parameters in the form:

    Bazooka.exe COMMAND OPTIONS
	
Where command can be install, uninstall, update or blast.

