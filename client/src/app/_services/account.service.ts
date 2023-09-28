import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  baseUrl = 'https://localhost:5001/api/';
  apiVersion = 'v1/';
  accountServiceControllerName = 'Account/';
  serviceUrl =
    this.baseUrl + this.apiVersion + this.accountServiceControllerName;

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post<User>(this.serviceUrl + 'Login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(model:any) {
    return this.http.post<User>(this.serviceUrl + "Register",model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null!);
  }
}
