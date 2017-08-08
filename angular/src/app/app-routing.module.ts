import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PresignComponent } from './presign/presign.component'
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component'
import { DeauthorizeComponent } from './deauthorize/deauthorize.component'

@NgModule({
    imports: [
        RouterModule.forRoot([
            { path: '', component: PresignComponent },
            { path: 'list', loadChildren: './list/list.module#ListModule' },
            { path: 'redirect', component: FinishAuthComponent },
            { path: 'deauthorize/:spotifyId', component: DeauthorizeComponent },
            { path: '**', component: PresignComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {

}