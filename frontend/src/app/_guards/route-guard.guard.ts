import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import Swal from 'sweetalert2';

export const routeGuardGuard: CanActivateFn = (route, state) => {
  var acountService = inject(AccountService);
  if (!acountService.user()) {
    Swal.fire({
      icon: 'error',
      title: 'Access Denied',
      text: 'You need to be logged in to access this page.',
      confirmButtonColor: '#d33'
    });
    return false;
  }
  return true;
};
