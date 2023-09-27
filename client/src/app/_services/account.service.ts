import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  apiVersion = 'v1/';
  accountServiceControllerName = 'Account/';
  serviceUrl =
    this.baseUrl + this.apiVersion + this.accountServiceControllerName;

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.serviceUrl + 'Login', model);
  }
}
