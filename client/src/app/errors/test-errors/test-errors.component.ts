import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css'],
})
export class TestErrorsComponent implements OnInit {
  // Controller name for account-related services
  accountServiceControllerName = 'Buggy/';
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  getSecret() {
    this.http.get(this.accountServiceControllerName + 'GetSecret').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getNotFound() {
    this.http.get(this.accountServiceControllerName + 'GetNotFound').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getServerError() {
    this.http
      .get(this.accountServiceControllerName + 'GetServerError')
      .subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getBadRequest() {
    this.http
      .get(this.accountServiceControllerName + 'GetBadRequest')
      .subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getValidationError() {
    this.http.get('Account/Register', {}).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        this.validationErrors = error;
      }
    );
  }
}
