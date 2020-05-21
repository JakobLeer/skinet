import { catchError } from 'rxjs/operators';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
      private router: Router,
      private toastr: ToastrService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const errorHandler = next.handle(req).pipe(
      catchError((error) => {
        if (error) {
            if (error.status === 400) {
                if (error.error.errors) {
                    // We have some error messages we'd like the sending form to take care of.
                    throw error.error;
                }
                else {
                    this.toastr.error(error.error.message, error.error.statusCode);
                }
            }
            if (error.status === 401) {
                this.toastr.error(error.error.message, error.error.statusCode);
            }
            if (error.status === 404) {
                this.router.navigateByUrl('/not-found');
            }
            if (error.status === 500) {
                const navigationExtras: NavigationExtras = {
                    state: {
                        error: error.error
                    }
                };
                this.router.navigateByUrl('/server-error', navigationExtras);
            }
            }

        return throwError(error);
      })
    );

    return errorHandler;
  }
}
