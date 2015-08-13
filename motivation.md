---
layout: default
---

# Motivation

Deploying an application manually is a complex process:

1. Get all the application files
2. Build the application with your build system
3. Copy the build output to the desired server/network share
4. Modify the configuration files for the enviroment, changing connection strings and other parameters
5. Execute all other needed steps as configuring site options, updating the database, ...

As you can see there are multiple steps and each one is subject to error:

1. You forget to update your local copy with the latest changes from your VCS or you have locally some files which were not checked in, so the application you're building is different from the one everyone else was testing
2. You may have subtle differences in your local configuration like a different compiler or forget to run a part of the build, like javascript minification and concatenation
3. You may not have access to the flder used for publishing in production or the one who has is currently on vacation
4. Changing the configuration files manually for each enviroment has some risks like forgetting to change a connection string that now points to a database in another enviroment, or worse you don't remember all the modifications to the configuration so you never touch the configs
5. You may forget to update the database with the new table or sotred procedure you have just created or forget to restart the web server.

Each of these step is necessary for a pubblication but each of these steps introduce complexity in your deployment process and a risk of making an error, especially when tired or under pressure. Now multiply these steps for maybe three or four enviroments (Test, UAT, Staging and Production) and for ten to twenty applications (not too many, especially if you're going the microservices road) and you have a problem on your hands not to mention all the time subtracted to other activities.

The only solution to this problem is to automate completely your application build and deployment.