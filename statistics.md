---
layout: default
---

# Statistics

Sometimes it's useful to have a look at the aggregated data for your deploys, so Bazooka implements a statistics page.

For now the info it exposes are :

- an aggregation over the number of times each application has been deployed in every enviroment (useful to understand if an application is deployed in test maany times but rarely reaches UAT, maybe a sign that there is a quality problem)
- an aggregation over the number of times a user has deployed an application (admittedly less useful, but for example it shows if a user deploys much more the anyone else maybe indicating he is testing his changes directly in the test enviroment)

