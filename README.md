# EnterpriseAppSettings
Every large platform needs settings management. This project aims to create a highly fexible configuration system usable on both simple and highly complex multi-tier applications. 

These are settings that are for the system/platform. They are managed centrally and ideally consumers will poll and reconfigure at runtime.

Enterprise App Settings allows for hierarchic grouping of configurations in anyway that works for you. By region/location, tenant, consumer type.

The source here includes datastore/database, REST service tier and consumer SDK/Tools.

The goal is to provide have a standard system that can work across a wide variety of datastores, service infrastructures and consumer cleint OS's and platforms.

_**Project in formation phase...come back in a week**_

I am populating the majority of this project based on code I have been implementing for years. Then it would be great to have contributors come and make more SDKs and help make this system better and applicable for more platforms and datastores.

## Task to complete for first working version##
- [x] SQL Server Database project
- [ ] Entity Framework Model layer
- [ ] Repository layer
- [ ] Unit of work layer
- [ ] .NET WebAPI Service
- [ ] .NET Client SDK
- [ ] .NET Core WebAPI Service
- [ ] .NET Core Docker Image
- [ ] Add Wiki Pages
- [ ] Create develop branch and promote

## Next to-do
- [ ] Basic Web Admin Interface
- [ ] Xamarin SDK
- [ ] Password type encryption

## Possible roadmap developements we need help with
* NoSQL datastore option
* JavaScript SDK
* iOS SDK
* Android SDK

# How it works #

Each 'App Setting' ultimately resolves to a _*Key-Value*_ pair. 

* SettingKey - A unique key\*
* SettingValue

\* - *unique in a given Group for a given Tenant. See App Setting Group and Tenant below*

However for a service or app that requests it's collection of settings we need a way of organising and filtering them, so that the consuming client receives only the _'resultant settings'_ they should.

When a consumer request it's collection of App Settings from the REST service, it will be required to pass the following:

* Group
* Tenant

but each 'App Setting' has the following additional attributes:

* App Setting Group
* App Setting Section
* App Setting Type
* Is Locked?
* Is Internal Only?


## App Setting Group

To enable this Enterprise App Settings implements a hierarchical grouping called 'App Setting Groups'. The top level parent is called _'Core'_ and any 'App Settings' in this Group will be applicable unless overridden in child 'App Setting Groups'

Settings in child groups will override/replace settings in parent groups. This is known as the _'Resultant Settings'_.

How you choose to organise your settings can be different for every project and can incorporate multiple dimensions.

A typical grouping I have used is by Location/Region and Consumer Type.

- _Core_
  - {REST Service}
    - {Region}
  - {Mobile Clients}
   - {Brand / Version}

For example

- Core
  - Services
    - AppSettings
      - NorthEurope1
      - NorthEurope2
      - EastUS1
      - EastUS2    
    - Billing
      - NorthEurope1
      - NorthEurope2
      - EastUS1
      - EastUS2
  - MobileClients
    - iOS
    - Android
      - KitKat
      - Nougat
    -WPhone

## App Setting Section

This attribute is really for Administration organisation. A large system can have hundred of configuration settings, so having the ability to organise them into _sections_ helps with finding settings and structuring the Administration UI design.

App Setting Sections are hierarchical allowing for Sections and Sub-sections. We would not recommend having more than 2 levels. Best to think of how you would implement the UI.

A typical organisation would be something like:

- General
- Database
- UI
  - Android specific
  - iOS Specific
  - Windows Phone Specific
- Security
- Misc


## App Setting Type

This indicates the type of data that is stored in the App Setting Value. This can be used to change the rendering of the content or to ensure you invoke the correct parsing handlers.

- TEXT
- HTML
- JSON
- XML
- NUMERIC
- PASSWORD

## Is Locked

This is a simple mechanism to ensure that some App Settings can not be changed through the Administration UI. Anything flagged as IsLocked will not be able to be changed or deleted through any of the REST services. The setting must be 'Unlocked' first.

Typically this flag would be set to True for certain critical App Settings that really should only be managed by the hard core infrastrutcure guys and if not set correctly will bring the whole system down!

## Is Internal Only

To ensure security and isolation there are two implementations of the 'App Settings' REST service.

Rest Service Type | Description
------ | -------------
Public | Will never publish any App Settings flagged as 'IsInternalOnly'. This service would typically be used by mobile devices and any 3rd party consuming clients.
Internal | This service will publish App Settings flagged as 'IsInternalOnly'. This service is typically only used by well trusted internal consuming apps or API services.




