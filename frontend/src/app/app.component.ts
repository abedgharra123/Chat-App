import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListUsersComponent } from './list-users/list-users.component';
import { NavComponent } from './nav/nav.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ListUsersComponent, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';
}
