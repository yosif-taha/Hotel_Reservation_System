namespace Hotel.Presentation.ViewModels.Response
{
    public enum ErrorType
    {
        None = 0,

        RoomNotFound = 101,
        InvalidRoomData = 102,
        RoomAlreadyExists = 103,
        RoomDeletionFailed = 104,
        RoomUpdateFailed = 105,
        UnauthorizedAccess = 106,
        DatabaseConnectionError = 107,
        InvalidRoomId = 108

    }
}