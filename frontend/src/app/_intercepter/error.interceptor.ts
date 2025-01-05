import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  return next(req).pipe(
    catchError((error) => {
      // Show a styled alert to the user
      Swal.fire({
        icon: 'error',
        title: 'Request Failed',
        text: error?.error || 'Something went wrong. Please try again.',
        confirmButtonColor: '#d33'
      });
      return throwError(error); // Re-throw the error if needed
    }));
};
