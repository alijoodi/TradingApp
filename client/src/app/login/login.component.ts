import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  model: any = {};
  loggedIn: boolean = false;
  constructor(private accountService: AccountService) {}

  login() {
    this.accountService.login(this.model).subscribe(
      (response) => {
        this.loggedIn = true;
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  logout() {
    this.loggedIn = false;
  }

  ngOnInit(): void {}
}
