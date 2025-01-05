import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor() { }
  accountService = inject(AccountService);
  username: string = '';
  password: string = '';
  password2: string = '';
  register() {
    if (this.password !== this.password2) {
      Swal.fire({
        icon: 'error',
        title: 'Passwords do not match',
        text: 'Please make sure the passwords match.',
        confirmButtonColor: '#d33'
      });
      return;
    }
    this.accountService.register({ username: this.username, password: this.password });
  }
}
