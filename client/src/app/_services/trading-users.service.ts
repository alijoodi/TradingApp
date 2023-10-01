import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TradingUserDto } from '../_models/TradingUserDto';
import { Observable } from 'rxjs';

const user = JSON.parse(localStorage.getItem('user')!);
const token = user?.token ?? '';

@Injectable({
  providedIn: 'root',
})
export class TradingUsersService {
  controllerName = 'TradingUser/';

  constructor(private http: HttpClient) {}

  getTradingUsers(): Observable<TradingUserDto[]> {
    return this.http.get<TradingUserDto[]>(
      `${this.controllerName}GetTradingUsers`
    );
  }

  getTradingUserById(id: number): Observable<TradingUserDto> {
    return this.http.get<TradingUserDto>(
      `${this.controllerName}GetTradingUserById`
    );
  }

  getTradingUserByUsername(username: string): Observable<TradingUserDto> {
    return this.http.get<TradingUserDto>(
      `${this.controllerName}GetTradingUserByUsername`
    );
  }
}
