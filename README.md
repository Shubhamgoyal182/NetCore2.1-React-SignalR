# NetCore2.1-React-SignalR
An .NET 2.1 code example building a React app inside an ASP.NET Core application, with SignalR integration for sending messages.

Notify.Api is the SignalR Server Hub

Notify.Api.clientapp is a react Application that connects to the Hub using.

## Documentation
See the [documentation](https://docs.microsoft.com/aspnet/signalr/overview/getting-started/introduction-to-signalr)

#### Get it on NuGet!

    Install-Package Microsoft.AspNetCore.SignalR.Core -Version 1.0.15
	
#### ASP.NET Core 2.1 Integration
From the above command you will be able to install SignalR package in your project. SignalR middlware requires some services which we have done by  making changes in our Startup class. Inside your ConfigureServices method, add the following code	:
    
	services.AddSignalR();
	
#####Hub
SignalR uses hubs to connect your api with a client web api. For this, we have created a new SignalR Hub which is very straight forward. Create a new class called MessageHub which will inherit the Hub Class.

    using Microsoft.AspNetCore.SignalR;

    namespace Notify.Api.Hubs
    {
         public class MessageHub : Hub
         { }
    }

##### Map Hub
At last, we need to register our hub to a route. The Client will use this route to connect to the specific hub. Again Startup file inside your Configure method, add the following code :

    app.UseSignalR(routes =>
    {
        routes.MapHub<MessageHub>("/message");
	});
This routes the MessageHub to /message.

##### Send Message API

Now we have to create an endpoint to fire our messages. for this we have create a controller called MessageController.
Here inside Create method we have injected MessageHub through DI through the IHubContext interface, now this will send a message to all clients that are listening to event "sendToClient”.
		
    [Route("/api/message")]
    [ApiController]
    public class MessageController : Controller
    {
        protected readonly IHubContext<MessageHub> _messageHub;
        public MessageController(IHubContext<MessageHub> messageHub)
        {
            _messageHub = messageHub;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessagePost messagePost)
        {
            await _messageHub.Clients.All.SendAsync("sendToClient", "The message '" +
            messagePost.Message + "' has been received");
            return Ok();
        }
    }
Build the solution and run it. you can test it by using postman.
#### Create React App 	
	
	 npx create-react-app clientapp --template typescript

#### SignalR Integration in React App 	
	
	 npm add @microsoft/signalr
	 
Open App.tsx file and replace  it with below code.

    import React, { useState, useEffect } from 'react';
	import './App.css';
	import * as signalR from "@microsoft/signalr";

	const App: React.FC = () => {

	  const hubConnection = new signalR.HubConnectionBuilder().withUrl("/message")
		.build();

	  hubConnection.start();

	  var list: string[] = [];

	  interface MessageProps {
		HubConnection: signalR.HubConnection
	  }

	  const Messages: React.FC<MessageProps> = (messageProps) => {

		const [date, setDate] = useState<Date>();

		useEffect(() => {
		  messageProps.HubConnection.on("sendToClient", message => {
			list.push(message);
			setDate(new Date());
		  })
		}, []);

		return <>{list.map((message, index) => <p key={`message${index}`}>{message}</p>)}</>
	  }

	  return <><Messages HubConnection={hubConnection}></Messages></>
	}

	export default App;