export interface RegisterTradingUserDto {
    name: string;
    family: string;
    mobileNumber: string | null;
    email: string;
    username: string;
    password: string;
}
