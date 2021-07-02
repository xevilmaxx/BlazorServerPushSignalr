# BlazorServerPushSignalr
Sample of how to use Blazor + SignalR tech (C# Net 5.0). Mainly how to have a static object to use in any place server side in order to push signalR notification to connected clients.

## Solution based on:
- - -
https://newbedev.com/how-to-get-signalr-hub-context-in-a-asp-net-core
- - -
https://stackoverflow.com/questions/51968201/invoking-signalr-hub-not-working-for-asp-net-core-web-api/51981886#51981886
- - -

### I assume you know how Blazor app is basically structured

### Main Points to focalize on:
- [x] Startup.cs
- [x] Hubs/SignalrHub0.cs
- [x] Pages/Index.razor
- [x] Controllers/IndexController.cs
- [x] Statics/SignalrServerSide.cs

### Basic Idea:
- - -
You have your Index.razor (layout only)
**Index.razor** is controlled by **IndexController.cs**


### Basic Workflow

- - -

In **Startup.cs** you will map your Hub (**SignalrHub0.cs**) on certain endpoint.

Backend of startup will automatically call constructor of **SignalrHub0.cs** with needed data.

- - -

You will also need to define a Hosted Service (**SignalrServerSide.cs**) which we will use later as main endpoint to access HubClients at any point directly from backend.

Again, **Startup.cs** backend will keep updated static variable in SignalrServerSide.cs with its strange logics.

- - -

Now all you need to do to get all currently connected clients at any point on server side:

**SignalrServerSide.HubContext**

- - -

If you wish to recycle logics you already defined in SignalrHub0.cs

Thanks to its constructor you can simply do this:

**_ = new SignalrHub0(SignalrServerSide.HubContext).SendMessageAsync("MyRecycledMessage");**
