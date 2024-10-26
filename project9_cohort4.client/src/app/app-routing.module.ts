import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './batool/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  // {
  //   path: 'protected',
  //   component: /* Your Protected Component */,
  //   canActivate: [AuthGuard],
  // },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  // ...other routes
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
