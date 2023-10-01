import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, take, throwError } from 'rxjs';
import { NavigationExtras, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let currentUser: any;
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user: User) => (currentUser = user));
    const apiReq = req.clone({
      url: `https://localhost:5001/api/v1/${req.url}`,
      setHeaders: {
        Authorization: `Bearer ${currentUser?.token}`,
      },
    });

    return next.handle(apiReq).pipe(
      catchError((error) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateError = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateError.push(error.error.errors[key]);
                  }
                }
                throw modalStateError.flat();
              } else {
                this.toastr.error(error.statusText, error.status);
              }
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras: NavigationExtras = {
                state: { error: error.error },
              };
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error(error.statusText, error.status);
              break;
          }
        }
        console.log(error);
        return throwError(error);
      })
    );
  }
}
