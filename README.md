# NetCore2.1-React-SignalR
An .NET 2.1 code example building a React app inside an ASP.NET Core application, with SignalR integration for sending messages.

Notify.Api is the SignalR Server Hub

Notify.Api.clientapp is a react Application that connects to the Hub using.

## Documentation
See the [documentation](https://docs.microsoft.com/aspnet/signalr/overview/getting-started/introduction-to-signalr)

## Get it on NuGet!

    Install-Package Microsoft.AspNetCore.SignalR.Core -Version 1.0.15
	
## Create React App 	
	
	 npx create-react-app clientapp --template typescript

## SignalR Integration in React App 	
	
	 npm add @microsoft/signalr