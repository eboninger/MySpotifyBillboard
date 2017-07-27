import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { HomeComponent } from './home/home.component';
import { TrackListComponent } from './home/track-list/track-list.component';

import { appRoutes } from './routes';

import { KeyService } from './key.service';
import { UserDataService } from './user-data.service';
import { SerializeTracksService } from './home/serialize-tracks.service';
import { CookieService } from 'ngx-cookie-service';




@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PresignComponent,
    FinishAuthComponent,
    HomeComponent,
    TrackListComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    KeyService,
    UserDataService,
    SerializeTracksService,
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
