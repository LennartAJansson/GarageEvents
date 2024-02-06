# Garage events
This solution contains a small experiment in how to use events at different levels. The solution consists of an imaginary garage with a door and lights. The solution is built to be used in > .NET8 and is using Microsoft.Extensions for logging, configuration, dependency injection etc.  

The applications to test the garage is made as two different solutions, one microservice oriented with four separate applications and one modular monolith.  

The door can be opened and closed and the light could be turned on and off. A Remote control is used to control the door and the light. It all circles around the events that are raised when the remote is triggered.  

The assembly GarageEvents contains different abstractions for the garage, the door, the light and the remote control.  
It also contains the implementation of the garage, the door, the light and a default behavior for a remote control. The default remote control is using plain events to signal the actions.  
  
The assembly GarageEvents.Nats contains an implementation of the IRemote interface that uses asynchronous event streaming to send the events between multiple applications. The idea is to be able to have one application for each part.  

To be able to run the sample you will need to have a nats server running. You can download the server from [nats.io](https://github.com/nats-io/nats-server/releases/latest). Remember to start it with the commandline switch `--js` to enable the streaming server.  
  
To show the full implementation there are four sample applications (think of them as microservices) all taking care of each part of the scenario, one for the remote, one for the garage, one for the door and one for the light and they are using the event stream to talk with each others. There is also an application called MonolithTest that will include all the functionality in the four microservicerelated applications, in this application it is easy to switch between internal and external events.  

This sample is not meant to be a full implementation of a garage, it is just a small experiment in how to use events at different levels. There is a lot of things that could be added to make it more realistic, like a sensor for the door, a timer for the light and so on. Furthermore it is not written to be an example of SOLID or any particular level of clean code. Lots of the code could even be considered ugly and in some cases perhaps even bad practice.  

But it could actually be implemented in real life by using one or a couple of Raspberry Pi's, some wirings, some sensors, a wifi connection and some relays.

If you have any questions or comments, please feel free to contact me.
