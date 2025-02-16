namespace T_Tier.BLL.Wrappers;

public enum ResponseTypeEnum
{
    Success = 200,
    Created = 201,
    InvalidInput = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    Error = 500
}