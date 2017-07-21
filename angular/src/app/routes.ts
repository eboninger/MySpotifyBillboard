import { Routes } from  '@angular/router'
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component'


export const appRoutes: Routes = [
    { path: '', component: PresignComponent },
    { path: 'redirect', component: FinishAuthComponent }
]