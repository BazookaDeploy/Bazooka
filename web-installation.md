---
layout: default
---

# Installation

First of all download the [last available release](https://github.com/BazookaDeploy/Bazooka/releases) then we will proceed to the installation and configuration of the database, the web  site and the controller.

### The database

First we are gonna create the database. The supported databases are Sql Server 2008 or SQL Server 2012. You can create a database, name it Bazooka and the create the structure by alternatively:

- using the sql script contained in the Dtabase zip package
- applying the dacpac inside the zip package

### The website

To install the web site just extract the content of the website zip package in your website configuration folder. Make sure that the user running the site has access to the Bazooka database created previously.

Now the only remaining thing to do is to edit the web.config  file:

- replace the connection string to point to the Bazooka database you have created previously
- if you want to authenticate your users with active directory  change in the appsettings section the value from false to true and change your domain.

### The controller

The last thing to install is the controller. The controller is a windows service based on Topshelf so the only thing to do is simply to extract the zip package and execute the following command:

    Controller.exe install
	
Now the only remaining thing to do is to change the connection string in the App.config file an mayhbe change the user the service is running under (your system administrator may prefer to create a dedicated user)