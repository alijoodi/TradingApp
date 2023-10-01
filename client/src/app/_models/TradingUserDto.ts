export interface TradingUserDto {
  id: number;
  name: string;
  family: string;
  mobileNumber: string | null;
  email: string;
  username: string;
  isActive: boolean;
}
