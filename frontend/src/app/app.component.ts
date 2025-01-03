import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListUsersComponent } from './list-users/list-users.component';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { LoginComponent } from "./login/login.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ListUsersComponent, NavComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Chat';
  accountService = inject(AccountService);

}
