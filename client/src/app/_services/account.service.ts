import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  // Creating a private subject to hold the current user
  private currentUserSource = new ReplaySubject<User>(1);

  // Exposing an observable to subscribe to changes in the current user
  currentUser$ = this.currentUserSource.asObservable();

  // Controller name for account-related services
  accountServiceControllerName = 'Account/';

  constructor(private http: HttpClient) {}

  // Method to log in a user
  login(model: any) {
    return this.http
      .post<User>(this.accountServiceControllerName + 'Login', model)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user)); // Store user details in local storage
            this.currentUserSource.next(user); // Emit the new user value to all subscribers
          }
        })
      );
  }

  // Method to register a new user
  register(model: any) {
    return this.http
      .post<User>(this.accountServiceControllerName + 'Register', model)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user)); // Store user details in local storage
            this.currentUserSource.next(user); // Emit the new user value to all subscribers
          }
        })
      );
  }

  // Method to set the current user manually (used in certain cases, e.g., after page refresh)
  setCurrentUser(user: User) {
    if(user)
    {
      user.roles = [];
      const roles = this.getDecodedToken(user.token).role;
      Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
      localStorage.setItem('user', JSON.stringify(user));
    }
    this.currentUserSource.next(user); // Emit the new user value to all subscribers
  }

  // Method to logout the current user
  logout() {
    localStorage.removeItem('user'); // Remove user details from local storage
    this.currentUserSource.next(null!); // Emit null value to all subscribers to indicate logout
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
