# Blazor Template with Authentication, Role-Based Authorization and User Management
## Features
- Authentication using Microsoft's Identity API, IEmailSender implementations
- Role-Based Authorization
- 2FA
- Example EF core code-first models (Project and Client) with:
  - Foreign Key relationships (Navigation Properties)
  - Validation Annotations
  - Class extensions 
- Full CRUD for example models (Project and Client)
- Open Iconic icons
- Reusable custom components for List pages and Management (edit, create) Pages
- Reusable 3rd party components
  - Nice Sortable, Filterable, Searchable data tables from BlazorTable
  - Nice customized Autocompletes from ZBlazor
- Service Base provides flexible, generic data access methods and can be extended
- DB Context, Services managed by Dependency Injection Container
  - Connection String read from configuration

## Getting Started
Under the WebUI project root directory, add a file called `appsettings.json` with the following, replacing the placeholder values:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "YOU CONNECTION STRING"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "AdminUserName": "admin",
    "AdminUserEmail": "YOUR EMAIL",
    "AdminUserPassword": "YOUR PASSWORD"
  }
}

```
TIP: you can also create an `appsettings.Development.json` file, any values in it will override `appsettings.json` in a Development environment.

## Sending Email
The default email sender for this application uses SMTP and should be configured in appsettings.json or environment variables. The configuration for SMTP2Go, the provider that TechRiver has set up for this project should look like this:

```
  "SMTP": {
    "Address": "mail.smtp2go.com",
    "Port": "2525",
    "Account": "",
    "Password": "",
    "SenderEmail": "",
    "SenderName": ""
  }
```
> Note: `Account` and `Password` should be the name and password of a SMTP user that you created in SMTP2Go settings: https://app.smtp2go.com/settings/users/, while `SenderEmail` and `SenderName` are values you can choose (make sure you have added the domain of `SenderEmail` to your Sender Domains in SMTP2Go settings: https://app.smtp2go.com/settings/sender_domains/)

This app can alternatively be configured to use the SendGrid API to send email (see /Email/SendGridEmailSender.cs).
In development, add your SendGrid API key to the projects User Secrets like so:

```
{
  "SendGridUser": "NameOfYourKey",
  "SendGridKey": "SG.*******************************.******************************************"
}
```
>Note: SendGrid is no longer the default provider and you will need to change `Startup.cs` in order to use SendGrid's API. The default provider is SMTP2Go via username/password authenticated SMTP as described above.


## Creating Roles and Admin User on Startup
Startup expects a section called "AppSettings" in `appsettings.json`, with the following fields (replace the values with your own):

```
 "AppSettings": {
    "AdminUserName": "adminUserName",
    "AdminUserEmail": "adminEmail@example.com",
    "AdminUserPassword": "strongPassword!"
  }
```
*Note: the AdminUserName is currently unused, as authentication fails if the UserName is different from the Email 
(this is a asp.net Identity bug)*

## Where are all the identity pages? Only a couple of them are present in the project.
The Identity code is hidden in a .dll by default, you can scaffold out (and override) any of the components you'd like by right clicking the project
in Visual Studio, Selecting Add->New Scaffolded Item...->Identity->Add then choosing the components you'd like to scaffold.
*Note: select your application's DBContext when doing this, don't create a new one.*
#   H o p e - B a c k u p  
 