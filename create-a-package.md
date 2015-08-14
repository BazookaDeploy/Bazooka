---
layout: default
---

# Create a package

Creating a package is really straightforward and can be easily automated and included in an automated build.

### The definition file

For each package to create (an application may consist of multiple packages, one for the web site, one for a windows service, ...) it's necessary to create a definition file, the **nuspec** file.

A **nuspec** file is simply an xml file with the nuspec extensions that tells Nuget how to create the package. To begin you can simply take this as an example and modify it for your project:

    <?xml version="1.0"?>
    <package >
      <metadata>
        <id>MyAwesomeWebSite</id>
        <version>$version$</version>
        <owners>Awesome Inc</owners>
        <description>My Awesome Web Site</description>
      </metadata>
      <files>
        <file src="PATH\TO\FOLDER\**\*.*" target="" />
      </files>
    </package>

To adapt this example to your specific case you have to modify:

<dl>
    <dt>id tag</dt>
    <dd>change the content of this tag to the name of the application contained in the package (no spaces or characters not allowed in URLS, this is a [nuget convention](https://docs.nuget.org/create/nuspec-reference))</dd>
    <dt>version tag</dt>
    <dd>the $version$ syntax means that this is a placeholder that will be replaced when building. Better leave this here and specify it later</dd>    
    <dt>owners tag</dt>
    <dd>Specifies the owner of this package. It's only a description and can be omitted.</dd>   
    <dt>description tag</dt>
    <dd>A description of the package. It's only a description and can be omitted.</dd>  
    <dt>files tag</dt>
    <dd>This tag and all it's children specify what files will be added to the package. You can replacce PATH\TO\FOLDER with the path where all the applications file can be found but leave the trailing \**\*.* as that means that all the files will be recursively added.</dd>
</dl>

### Creating your package

Once you have created the nuspec file to create the package you just have to call the Nuget executable ( downloadable from the [nuget repository](http://nuget.codeplex.com/releases/view/612846)) in this way

    nuget.exe pack YOURNUSPECFILE.nuspec -Properties "version=YOURPACKAGEVERSION"
    
replacing YOURNUSPECFILE.nuspec with the actual name of your nuspec file and YOURPACKAGEVERSION with the version of the package you want to build. This command will create a file with the nupkg extension in the current folder that contains all the applications file. If you want to inspect it you can simply change the extension to .zip and open it like a normal compressed archive.

### Saving your packages

Once you have created your packages you have to save them somewhere and there are four options:

- **save them in the Nuget gallery** : Tecnically feasible but not encouraged as the packages would be visible to everyone and you would not want that.
- **save them in a folder** : Nuget is able to use a folder as a package repository without problems and this options let you start easily but be wary: with many packages performances tend to degrade.
- **save them in a local nuget gallery** : You can easily install a private copy of the nuget gallery allowing you to view and interrogate all the packages while keeping them private
- **save them in the Bazooka web application** : The Bazooka web application can act as a simple nuget repository allowing you to host your packages without installing anything else. 

If you decided to use the folder solution you just need to copy the nupkg file. If instead you opted to use Bazooka, the Nuget gallery or a private gallery you can execute the command 

    nuget push FILENAME APIKEY -s GALLERYURL
    
where FILENAME is your nupkg file, APIKEY is the apikey used to indetify your user and GALLERYURL is the nuget gallery url.