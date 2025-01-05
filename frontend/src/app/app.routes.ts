import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { routeGuardGuard } from './_guards/route-guard.guard';
import { ListUsersComponent } from './list-users/list-users.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '', 
        children: [
            {path: 'members',component: ListUsersComponent},    
        ],
        runGuardsAndResolvers: 'always',  
        canActivate: [routeGuardGuard]},

    {path: 'home',component: HomeComponent},
    {path: 'login',component: LoginComponent},
    {path: 'register',component: RegisterComponent},
    {path: '**',component: HomeComponent, pathMatch: 'full'}
];
