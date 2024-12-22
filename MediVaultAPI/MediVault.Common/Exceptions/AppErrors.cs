namespace MediVault.Common.Exceptions;

public enum AppErrors
{
    UnknownError = -1,
    EnterUserName = 1,
    EnterPassword = 2,
    UserNameOrPasswordInvalid = 3,
    UserHasAlreadyBeenRegistered = 4,
    RefreshTokenExpired = 5,
    NoAuthorizationHeaderFound = 6,
    SessionExpired = 7
}