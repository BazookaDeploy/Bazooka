---
layout: default
---

# Packages

To be able to deploy a specific version of an application in an enviroment the first thing to do is to package your compiled application and slap a version number on it.

The simplest way to do this and create a package that can be used by Bazooka is to use a **Nuget** package. A Nuget package offers a lot of advantages:

- it's designed from the start to be versionable
- it's easy to use as it's practically a zip file with additional metadata
- it's easy to create from an automated build
- there is already a lot of tooling to deal with Nuget packages, like libraries and  local galleries

This choiche was almost natural as many others have made the same choiche like Chocolatey and OneGet.

### What should a package contain?

A package should contain everything needed to deploy you application which means:

- all compiled binaries
- all static assets (css, javascript, view files)
- optionally an installation and uninstallation Powershell script (these can be included in the package or configured in the web application)
- configuration files and optionally config transforms for any enviroment (these can be included in the package or configured in the web application)

### Versioning your packages

While Bazooka has no restrictions on version numbers used in packages Nuget has something to say about it. 

Nuget follows the [Semantic versioning](http://semver.org/) specification that describes how a version number can be composed and the relative ordering between version numbers. While this may seem limiting it's a simple enough scheme that allows for a good degree of flexibility to adapt the versioning to your current practices. 
