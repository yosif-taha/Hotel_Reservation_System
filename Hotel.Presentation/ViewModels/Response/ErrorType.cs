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
        InvalidRoomId = 108,


        //Reservation
        ReservationNotFound = 201,
        InvalidReservationData = 202,
        ReservationAlreadyExists = 203,
        ReservationCreationFailed = 204,
        ReservationUpdateFailed = 205,
        ReservationCancellationFailed = 206,
        ReservationConflict = 207,
        InvalidReservationId = 208,
        ReservationDateInvalid = 209,
        ReservationExpired = 210,

        //Offer
        OfferNotFound = 301,
        InvalidOfferData = 302,
        OfferAlreadyExists = 303,
        OfferCreationFailed = 304,
        OfferUpdateFailed = 305,
        OfferCancellationFailed = 306,
        OfferConflict = 307,
        InvalidOfferId = 308,
        OfferDateInvalid = 309,
        OfferExpired = 310,
        OfferBusinessError = 311,
        InvalidFacilityData = 312,
        FacilityNotFound = 313,
        FacilityAlreadyExists = 314,
        InvalidFacilityId = 315
    }
}