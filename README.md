# ShoppingList
Looking for a self hosted shopping list app which can be used simultaneously by multiple members of a family or similar groups.

The solution has two components: App + Server

Attention! This is an early stage development.

## App
Written in C#/Xamarin currently only available as Android. iOS is available in the project but I have never build or tested it.

## Server
There is something like an example implementation in PHP. The server has to implement the [REST interface](../master/ShoppingListApp/ShoppingListApp/Services/REST/openapi.yaml).

## Features
* maintain multiple shopping lists
* two shopping list modes
  1. add/remove items
  2. shopping mode in which the app saves the order of removed items for later use
* every added item is stored for later use and easy "buy again" action
* share **all** shopping lists with other users by QR-Code (Attention! No right management implemented)

## Installation
Yeah...
