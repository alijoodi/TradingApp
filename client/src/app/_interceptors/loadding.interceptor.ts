import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { BusyService } from '../_services/busy.service';
import { Observable } from 'rxjs';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Call the `busy` method of the `BusyService` to indicate that a request is in progress
    this.busyService.busy();

    // Make the actual HTTP request by calling the `handle` method of the `next` parameter,
    // which represents the next interceptor or the backend handler
    return next.handle(request).pipe(
      finalize(() => {
        // After the request is complete, call the `idle` method of the `BusyService`
        // to indicate that no more requests are in progress
        this.busyService.idle();
      })
    );
  }

}
