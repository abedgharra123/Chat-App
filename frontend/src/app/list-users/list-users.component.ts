import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-users',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list-users.component.html',
  styleUrl: './list-users.component.css'
})
export class ListUsersComponent implements OnInit{
  http = inject(HttpClient);
  users : any;


  ngOnInit(): void {
    this.http.get("https://localhost:5000/api/user/users").subscribe(
      {
        error: (err) => console.log(err),
        next: result => this.users = result,
        complete: () => console.log("request completed successuly")
      }
    )
  }

  
}
