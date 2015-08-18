---
layout: default
---

# Users and groups

To simplify permission handling, Bazooka supports the concept of groups of Users. A Group is simply a collection of user that can be administrated from **Admin** -> **Groups** section.

From here you can 

- create a group
- add a user to a group
- remove a user from a group

### Active Directory groups

If you are using the active directory integration to handle user authentication you may be tempted to ask why it's not possible to use Active Directory groups to manage permissions. The answer is simple and primarily boils down to performances. 

Bazooka has to show you on the homepage all applications and enviroments which you have access to deploy. To do this it interrogates the database to discover ofr each enviroment if you are the owner, an authorizzed user or in an authorized group. Doing this with a call to Active directory resulted really slow but if there's interest and I found a better solution it may be implemented.
