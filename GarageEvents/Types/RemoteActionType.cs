namespace GarageEvents.Types;
//Enum to represent the different actions for the garage
public enum RemoteActionType { RefreshCmd, OpenDoorCmd, CloseDoorCmd, LightsOnCmd, LightsOffCmd, DoorIsOpen, DoorIsClosed, LightIsOn, LightIsOff }


//Delegate for the garage event
public delegate void RemoteActionDelegate(object sender, RemoteActionMessage action);

