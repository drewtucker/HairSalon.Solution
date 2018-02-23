# HairSalon

#### Webpage for a hair salon. Helps keep track Stylists and their respective Clients. 2/23/2018

#### By **Drew Tucker**

## Description

A website that allows users to enter a new stylist with relevant information, such as their name, phone number, and email address. Clients can then be added to each Stylist, however each Client can only have one Stylist. This project is practice in creating and manipulating data in databases.


### Specs

# Behavior
* Program gives users the option of adding a new Stylist via form entry. Users will enter the Stylists name, phone number, and email address.

* Once a new Stylist has been entered, users will be given the option of clicking on that Stylist to see their contact info.

* Once a Stylist has been clicked, users are given the option of adding a new Client to that particular Stylist. The Client needs only a name and phone number.

* If a new Stylist or Client is entered without all form fields filled out, user will then be prompted to enter the relevant info. Input- Name: "", Phone Number: "", Result: "Please fill out all fields."

* Users will be given the option of deleting Stylists or Clients.

## Setup/Installation Requirements
(Requires Microsoft .NET Framework & MAMP)

# In MYSQL
> CREATE DATABASE drew_tucker;
> USE drew_tucker;
> CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), phone_number INT(11), email VARCHAR(255), experience INT(3));
> CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), phone_number INT(11));


1. Clone this repository from GitHub.

2. Open the Mono command prompt and navigate to the 'HairSalon' directory of the cloned 'HairSalon.Solution' folder.

3. Enter 'dotnet build' followed by 'dotnet run' in the Mono command prompt.

4. Enter http://localhost:5000/ in your web browser and enjoy!

## Technologies Used
* HTML
* CSS
* Bootstrap
* GitHub
* C#
* MVC
* MAMP

## Support and contact details

_Email Drew Tucker at dtuck43@gmail.com with any questions, comments, or concerns._

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2018 **_{Drew Tucker}_**
