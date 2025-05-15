export interface IAuthResponse
{
    JwtToken: string;
    ValidUser:boolean;
    ValidPassword:boolean;
    IsActive:boolean;
    StatusCode:number;
    StatusMessage:string;
}