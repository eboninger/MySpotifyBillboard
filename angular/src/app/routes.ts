import { Routes } from  '@angular/router'
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { HomeComponent } from './home/home.component';

export const appRoutes: Routes = [
    { path: '', component: PresignComponent },
    { path: 'home', component: HomeComponent },
    { path: 'redirect', component: FinishAuthComponent }
]