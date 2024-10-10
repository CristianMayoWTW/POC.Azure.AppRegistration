# POC.Azure.AppRegistration

## Overview
This document provides an overview of the Proof of Concept (POC) solution developed to demonstrate the feasibility of retrieving WTW Employee details thru Azure Entra ID's App Registration. The POC aims to validate the core concepts and assess the potential for further development.

## Objectives
- Connect ASP.NET Core Web API using Microsoft Graph with Azure App Registration in Microsoft Entra ID (formerly Azure Active Directory)
- Retrieve WTW Employee details using the BenefitsAccess Admin username (in a form of email address), individually or by batch


## Setup and Installation
All you need to do is run/debug the project via Visual Studio and test the endpoint thru the Swagger Dashboard.

Note: Ensure that the below required C# Libraries were downloaded and restored in the project:
- `Azure.Identity`
- `Microsoft.Graph`
- `Microsoft.Identity.Client`


## Limitations
- Technical constraints: Client Secret from the Azure Entra ID's App Registration was not committed to this repository, hence, you have to access the App Resitration itself to retrieve the  Client Secret. Access and Permission to the Azure Entra ID is also required.

## Contact
Identity and Commuications (InC) Team
>- Email Address: `WTW.BDA.Group.Marketplace.Identity.Team@willistowerswatson.com`
>- MS Team Channel — BenefitsAccess: `@INC Team`