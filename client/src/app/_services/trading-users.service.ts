import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TradingUserDto } from '../_models/TradingUserDto';
import { Observable } from 'rxjs';
import { RegisterTradingUserDto } from '../_models/RegisterTradingUserDto';
import { DeactiveTradingUserByUsernameRequest } from '../_models/DeactiveTradingUserByUsernameRequest';

const user = JSON.parse(localStorage.getItem('user')!);
const token = user?.token ?? '';

@Injectable({
  providedIn: 'root',
})
export class TradingUsersService {
  controllerName = 'TradingUser/';

  constructor(private http: HttpClient) { }

  getTradingUsers(params: HttpParams) {
    return this.http.get<TradingUserDto[]>(`${this.controllerName}GetTradingUsers`, { observe: 'response', params });
  }


  getTradingUserById(id: number): Observable<TradingUserDto> {
    return this.http.get<TradingUserDto>(
      `${this.controllerName}GetTradingUserById`,
      { params: { id } }
    );
  }

  getTradingUserByUsername(username: string): Observable<TradingUserDto> {
    return this.http.get<TradingUserDto>(
      `${this.controllerName}GetTradingUserByUsername`,
      { params: { username } }
    );
  }

  deactiveTradingUserByUsername(username: string): Observable<TradingUserDto> {
    var userDeactiveModel: DeactiveTradingUserByUsernameRequest = { username: username };
    return this.http.put<TradingUserDto>(
      `${this.controllerName}DeactiveTradingUserByUsername`, userDeactiveModel
    );
  }

  deleteTradingUserByUsername(username: string) {
    const userDeactiveModel: DeactiveTradingUserByUsernameRequest = { username: username };
    return this.http
      .delete(`${this.controllerName}DeleteTradingUserByUsername`, {
        params: { username: userDeactiveModel.username }
      });
  }

  updateTradingUser(tradingUser: TradingUserDto): Observable<TradingUserDto> {
    return this.http.put<TradingUserDto>(
      `${this.controllerName}UpdateTradingUser`,
      tradingUser
    );
  }

  registerTradingUser(tradingUser: RegisterTradingUserDto): Observable<TradingUserDto> {
    return this.http.post<TradingUserDto>(
      `${this.controllerName}Register`,
      tradingUser
    );
  }
}
