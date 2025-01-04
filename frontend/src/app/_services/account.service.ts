import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user'; // Adjust the path as necessary
import { inject, Injectable, OnInit, signal } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import Swal from 'sweetalert2'; // SweetAlert for beautiful alerts

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

  register(model: any) {
    return this.http.post<User>(`${this.serverUrl}/api/user/register`, model)
      .pipe(
        catchError((error) => {
          // Show a styled alert to the user
          Swal.fire({
            icon: 'error',
            title: 'Registration Failed',
            text: error?.error?.message || 'Something went wrong. Please try again.',
            confirmButtonColor: '#d33'
          });
          return throwError(error); // Re-throw the error if needed
        })
      )
      .subscribe(
        (user) => {
          // Success logic
          localStorage.setItem('user', JSON.stringify(user));
          this.user.set(user);
          Swal.fire({
            icon: 'success',
            title: 'Registration Successful',
            text: 'Welcome to the platform!',
            confirmButtonColor: '#28a745'
          });
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
