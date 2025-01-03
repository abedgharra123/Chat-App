import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user'; // Adjust the path as necessary
import { inject, Injectable, OnInit, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})


export class AccountService implements OnInit{
  readonly serverUrl = "https://localhost:5000";
  http = inject(HttpClient);
  user = signal<User | null>(null);
  users : any;

  ngOnInit(): void {
    const user = localStorage.getItem('user');
    if (user) {
      this.user.set(JSON.parse(user));
    }
  }

  login(model: any) {
    return this.http.post<User>(`${this.serverUrl}/api/user/login`, model)
      .subscribe(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.user.set(user);
      }
    ); 
  }

  logout() {
    localStorage.removeItem('user');
    this.user.set(null);
  }

  register(username: string, password: string) {
    return this.http.post<User>(`${this.serverUrl}/api/user/register`, { username, password })
      .subscribe(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.user.set(user);
      }
    );
  }

  getUsers() {
    this.http.get(`${this.serverUrl}/api/user/users`).subscribe(
      {
        error: (err) => console.log(err),
        next: result => this.users = result,
        complete: () => console.log("request completed successuly")
      }
    );
  }
}
