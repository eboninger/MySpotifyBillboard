import { Routes } from  '@angular/router'
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { ListComponent } from './list/list.component';
import { DeauthorizeComponent } from './deauthorize/deauthorize.component'

export const appRoutes: Routes = [
    { path: '', component: PresignComponent },
    { path: 'list', component: ListComponent },
    { path: 'redirect', component: FinishAuthComponent },
    { path: 'deauthorize', component: DeauthorizeComponent },
    { path: '**', component: PresignComponent }
]