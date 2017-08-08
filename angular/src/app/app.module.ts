import { BrowserModule } from '@angular/platform-browser';
import { NgModule, enableProdMode, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { ListModule } from './list/list.module'

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { DeauthorizeComponent } from './deauthorize/deauthorize.component'
import { SpotifyLogoComponent } from './spotify-logo/spotify-logo.component';

import { appRoutes } from './routes';

import { KeyService } from './key.service';
import { CookieService } from 'ngx-cookie-service';
import { DeauthorizeUserService } from './user/deauthorize-user.service';




// uncomment line below to enable production mode
// enableProdMode();


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PresignComponent,
    FinishAuthComponent,
    DeauthorizeComponent,
    SpotifyLogoComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ListModule,
    NgbModule.forRoot(),
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    KeyService,
    CookieService,
    DeauthorizeUserService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
