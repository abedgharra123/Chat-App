import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  accountService = inject(AccountService);
  constructor() { }
  username: string = '';
  password: string = '';
  login() {
    this.accountService.login({ username: this.username, password: this.password });
  }
}
