import { BrowserModule } from '@angular/platform-browser';
import { NgModule, enableProdMode, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppRoutingModule } from './app-routing.module'
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

import { KeyService } from './key.service';
import { CookieModule } from 'ngx-cookie';
import { DeauthorizeUserService } from './deauthorize/deauthorize-user.service';




// uncomment line below to enable production mode
// enableProdMode();


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PresignComponent,
    FinishAuthComponent,
    DeauthorizeComponent,
    SpotifyLogoComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule.forRoot(),
    CookieModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    KeyService,
    DeauthorizeUserService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
