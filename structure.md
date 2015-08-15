---
layout: default
---

# Structure

The Bazooka web application is composed of three distinct pieces: the web site, the controller and the agents.

### The web site

The web site is the main part of Bazooka, it will allow you to configure your applications, set permissions, start a deployment and schedule it. It is a common ASP.NET MVC website using a SQL server database to store data.

The website can also act as a simple Nuget gallery to host your application packages, allowing you to hosting them without a separate gallery.

### The controller

The controller is a windows service, installed on the same machine as the website. Its main responsability is to execute jobs for the web applicaiton like a scheduled deploy, programmed cache cleanups and monthly log compactions.

### The agents

The last part of the system is the agent. An agent is a windows service installed on every machine where you will deploy an application. It's main responsability is to receive commands from the controller like installing a package or executing a script.
