namespace SharePlatformSystem.Core.Authorization
{
    public enum SharePlatformLoginResultType : byte
    {
        Success = 1,

        InvalidUserNameOrEmailAddress,
        
        InvalidPassword,
        
        UserIsNotActive,

        InvalidTenancyName,
        
        TenantIsNotActive,

        UserEmailIsNotConfirmed,
        
        UnknownExternalLogin,

        LockedOut,

        UserPhoneNumberIsNotConfirmed,
    }
}