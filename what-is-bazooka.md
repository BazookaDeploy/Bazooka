---
layout: default
---

# What is Bazooka

Bazooka is an **Application Release Automation (ARA)** Tool. It's function and the reason it was developed is to help automate the deployment of applications through different enviroments.

### How does it work?

The main idea behing Bazooka is to package your applications in a versioned container that can be later used to deploy it in any enviroment. For more informations about the structure of these packages and how to create them, see the **Packaging section**.

Once your application is packaged you can deploy it in two ways:

1. by using the command-line tool, an easy way which requires no installation but useful only to try Bazooka and to understand how it works
2. by installing, configuring and using the web application version, which requires some time to setup but will let yoou fully automate and even schedule your deployments


### Caveats

Bazooka has been developed to deploy .NET applications on Windows. While there's no limit to the type of applications deployed (you can deploy node.js apps or almost everything else) currently the web application, the command-line client and the agents run only on Windows. If there is enough request the agents and the command-line client could be ported to Linux.
