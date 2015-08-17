---
layout: default
---

# Install an agent

An agent must be installed on every machine where you want to deploy an application. An agent is a windows service that can receive request from the controller and execute them allowing it to:

- install a package
- uninstall a package
- update a package
- execute a powersshell script

To be able to deploy an application effectively you should review with your system administrator the necessary permits for the user running the agent (directory and file access, starting and stopping services, ...)

First thing to do is to reserve the port for the service so it will be able to respond to incoming request. By default the port is 9000 but you can change it in the app.config. Execute the followinf command substituting the username you have chosen for the service.

    netsh http add urlacl url=http://+:9000/ user=DOMAIN\user

Now that the port is reserved the only thing left is to install the service. Unzip the content of the zip package downloaded previously in afolder, open a terminal in that folder and execute

    Agent.exe install -username DOMAIN\user -password PASSWORD

substituting the chosen username and password. **NOTE** : a common error that happens when installing an agent is forgetting to open the firewall  between the controller and the agent. 

Done! The agent is installed and ready to work. Next we will see how to configure the website to use the agent.
