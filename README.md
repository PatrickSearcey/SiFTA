# Site Information & Funding Tracking Application (SIFTA) #

## Summary ##

SIFTA is a web application designed to help Admin of various water science centers keep track of their customers, agreements with those customers, and related funding. The web application portion is a simple asp.net webform CRUD application with a MS SQL Database. 
#### Version History ####
* Originally spreadsheets turned into an access database by Kevin S.
* (Version 1.0) Converted into a web application using MS SQL by Brian Reece and Justin Robertson (Limited to Texas)
* (Version 2.0) Database fleshed out and web application sterilized of dependencies on specific water science center to make National Product. Updated by Justin Robertson with Brian Reece as contact.
* (Version 3.0) Database schema cleaned up and relationships enforced, major improvements to UI and reporting capabilities. Updated by Justin Robertson with Brian Reece and Ramona Neafie as contacts. Current Version. 

## Setup ##

#### Requirements ####
* Visual Studio
* Telerik ASP.NET Ajax controls
#### Dependencies (NuGet) ####
* DocumentFormat.OpenXml
* DocX
* EPPlus
* Qr Code library
#### Server Configuration ####
##### Production #####
* Web Server: IGSKIACWVMGS014
    * File Directory: D:\siftaroot\NationalFunding
* Database Server: IGSKIACWVMGS014
    * Databse: siftadb
    * Connection String: Data Source=IGSKIACWVMGS014;Initial Catalog=siftadb;Integrated Security=True
* Deployment instructions
##### Development #####
* Web Server: IGSKIACWVMGS018
    * File Directory: D:\siftaroot\NationalFunding
* Database Server: IGSKIACWVMGS014
    * Databse: siftadb_dev
    * Connection String: Data Source=IGSKIACWVMGS014;Initial Catalog=siftadb_dev;Integrated Security=True
* Deployment instructions

## Contacts ##
##### Project Owner #####
Brian Reece bdreece@usgs.gov
##### Project Manager #####
Ramona Neafie rjneafie@usgs.gov 
##### Lead Developer #####
Justin Robertson jkrobertson@usgs.gov
##### Supporting Developers #####
Deanna Terry dterry@usgs.gov (Design & Layout)